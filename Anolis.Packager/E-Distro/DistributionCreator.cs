using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Anolis.Core;
using Anolis.Core.Source;
using ILMerging;

using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace Anolis.Packager {
	
	public static class DistributionCreator {
		
		public static void CreateDistribution(String outputFileName, String originalInstallerFileName, params String[] embeddedResourceFileNames) {
			
			String resourceAssembly = Path.Combine( Path.GetDirectoryName( outputFileName ), "Packages.dll" );
			
			CreateResourceAssembly( resourceAssembly, embeddedResourceFileNames );
			
			MergeAssemblies( outputFileName, originalInstallerFileName, resourceAssembly );
			
			File.Delete( resourceAssembly );
		}
		
		/// <summary>Creates a new assembly file containing the specified resource files.</summary>
		private static void CreateResourceAssembly(String outputFileName, params String[] resourceFileNames) {
			
			String source;
			
			using(Stream sourceStream = GetResource( s => s.EndsWith("Anolis.EmptyAssembly.cs") )) {
				
				if(sourceStream == null) throw new AnolisException("Couldn't find EmptyAssembly source");
				
				StreamReader rdr = new StreamReader(sourceStream);
				source = rdr.ReadToEnd();
				
			}
			
			CompilerParameters options = new CompilerParameters();
			options.EmbeddedResources.AddRange( resourceFileNames );
			options.OutputAssembly = outputFileName;
			
			CSharpCodeProvider prov = new CSharpCodeProvider();
			CompilerResults result = prov.CompileAssemblyFromSource( options, source );
			
			if( result.Errors.Count > 0 ) {
			
				throw new AnolisException("Compilation of Resource Assembly Failed");
			}
			
		}
		
		/// <summary>The created assembly has the same 'kind' as the primary assembly.</summary>
		/// <param name="assemblyFileNames">The first assembly is the primary assembly.</param>
		public static void MergeAssemblies(String outputFileName, params String[] assemblyFileNames) {
			
			ILMerge merge = new ILMerge();
			merge.OutputFile = outputFileName;
			merge.TargetKind = ILMerge.Kind.SameAsPrimaryAssembly;
			merge.SetInputAssemblies( assemblyFileNames );
			
			merge.Merge();
			
		}

#if NEVER
		/// <summary>Creates a new assembly file containing the specified resource files.</summary>
		private static void CreateResourceAssembly(String outputFileName, params String[] resourceFileNames) {
			
			/////////////////////////////////////////////////////
			// This method originally used al.exe to create a satellite assembly, however:
			// a) al.exe needs to be redistributed with the application, which may violate al.exe's EULA
			// b) the generated assemblies weren't compatible with ILMerge as they lacked an AssemblyNode (as is my understanding)
			
			// so it's really just easier to use csc.exe which is on every computer that has the .NET Framework anyway...
			
			
			
			
			///////////////////
			// Dump al.exe to disk
			
			String alExe = Path.Combine( Path.GetDirectoryName( outputFileName ), "al.exe" );
			
			ExtractAL( alExe );
			
			///////////////////
			// Build Args
			
			String args = "/out:\"" + outputFileName + "\" /target:lib";
			
			StringBuilder sb = new StringBuilder();
			foreach(String resourceFileName in resourceFileNames) {
				sb.Append(" /embedresource:\"");
				sb.Append( resourceFileName );
				sb.Append("\",\"");
				sb.Append( Path.GetFileName( resourceFileName ) ); // it must end with ".anop" for PackageUtility to pick it up
				sb.Append('"');
			}
			args += sb.ToString();
			
			ProcessStartInfo alStart = new ProcessStartInfo(alExe, args);
			alStart.CreateNoWindow = true;
			alStart.WindowStyle    = ProcessWindowStyle.Hidden;
			
			Process alProc = Process.Start( alStart );
			if(!alProc.WaitForExit(5000)) {
				
				throw new AnolisException("al.exe did not terminate within 5 seconds");
			}
			
			if( !File.Exists( outputFileName ) ) throw new AnolisException("Resource Assembly was not created");
			
			///////////////////
			// Delete al.exe
			
			File.Delete( alExe );
			
		}
		
		private static void ExtractAL(String destinationFileName) {
			
			using(Stream alResStream = GetResource( s => s.EndsWith("al.exe", StringComparison.Ordinal) ))
			using(FileStream fs = new FileStream(destinationFileName, FileMode.Create, FileAccess.Write, FileShare.None)) {
				
				if( alResStream == null ) throw new AnolisException("Couldn't find al.exe resource stream");
				
				Byte[] buffer = new Byte[0x1000];
				Int32 read;
				while ((read = alResStream.Read(buffer, 0, buffer.Length)) > 0)
					fs.Write(buffer, 0, read);
				
			}
			
		}
		
#endif
		
		private static CilResourceSource     _thisAssembly;
		private static ManagedResourceInfo[] _thisAssemblyRes;
		
		private static Stream GetResource(Predicate<String> predicate) {
			
			if( _thisAssembly == null ) {
				_thisAssembly    = new CilResourceSource( System.Reflection.Assembly.GetExecutingAssembly().Location );
				_thisAssemblyRes = _thisAssembly.GetResourceInfo();
			}
			
			foreach(ManagedResourceInfo info in _thisAssemblyRes) {
				
				if( predicate( info.Name ) ) {
					
					return _thisAssembly.GetResourceStream( info );
				}
			}
			
			return null;
		}
		
	}
}
