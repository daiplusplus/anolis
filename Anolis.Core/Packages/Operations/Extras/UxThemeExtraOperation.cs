using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;

using P = System.IO.Path;
using Env = Anolis.Core.Utility.Environment;
using W3b.TarLzma;

namespace Anolis.Core.Packages.Operations {
	
	public class UxThemeExtraOperation : ExtraOperation {
		
		public UxThemeExtraOperation(Package package, XmlElement element) :  base(ExtraType.UxTheme, package, element) {
		}
		
		protected override Boolean CanMerge {
			get {
				return false;
			}
		}
		
		public override void Execute() {
			
			// Path contains a path to a TarLZMA archive containing the files
			// Deepxw made a better patcher that uses a heuristic approach I want to implement in pure C#
			// until then, here's a really bad way to do it
			
			Replace();
			
		}
		
#region 100% Totally The Wrong Way
		
		private void Replace() {
			
			InstallationInfo info = GetInstallationInfo();
			
			// extract the files
			ExtractFiles( Path, info.ExtractDirectory );
			
			if( info.Install == Install.NT522X64English ) {
				
				ReplaceX64(info);
				
			} else {
				
				ReplaceX86(info);
			}
			
			Directory.Delete( info.ExtractDirectory, true );
			
		}
		
		private static void ReplaceX86(InstallationInfo info) {
			
			String srcFilename = P.Combine( info.ExtractDirectory, GetUxFilename(info.Install) );
			
			String dstFilename = P.Combine( Environment.GetFolderPath(Environment.SpecialFolder.System), GetUxFilename(info.Install) );
			
			if( File.Exists( dstFilename ) ) File.Delete( dstFilename );
			
			File.Move( srcFilename, dstFilename );
			
			String finalName = P.Combine(  Environment.GetFolderPath(Environment.SpecialFolder.System), "UxTheme.dll" );
			
			PackageUtility.AddPfroEntry( dstFilename, finalName );
		}
		
		private static void ReplaceX64(InstallationInfo info) {
			
			///////////////////////////////////////
			// Patch the x64 version
			
			ReplaceX86(info);
			
			///////////////////////////////////////
			// Patch the SysWow64 (x86) version
			
			// I can't tell an easy way to get the exact SysWow64 path... so just use an ugly hardhack
			
			String srcFilename = P.Combine( info.ExtractDirectory, GetUxFilename(Install.NT522English) );
			
			String sysWow64Path = Environment.GetFolderPath(Environment.SpecialFolder.System);
			sysWow64Path = Directory.GetParent(sysWow64Path).FullName;
			sysWow64Path = P.Combine( sysWow64Path, "SysWow64");
			
			String dstFilename = P.Combine( sysWow64Path, GetUxFilename(info.Install) );
			
			File.Move( srcFilename, dstFilename );
			
			String finalName = P.Combine(  Environment.GetFolderPath(Environment.SpecialFolder.System), "UxTheme.dll" );
			
			PackageUtility.AddPfroEntry( dstFilename, finalName );
			
		}
		
		private static String GetUxFilename(Install install) {
			switch(install) {
				case Install.NT513English:
					return "nt51_x86_en.dll";
				case Install.NT513German:
					return "nt51_x86_de.dll";
				case Install.NT513Spanish:
					return "nt51_x86_es.dll";
				case Install.NT522English:
					return "nt52_x86_en.dll";
				case Install.NT522German:
					return "nt52_x86_de.dll";
				case Install.NT522X64English:
					return "nt52_x64_en.dll";
				default:
					return null;
			}
		}
		
		private static void ExtractFiles(String archivePath, String destinationDirectory) {
			
			DirectoryInfo dest = new DirectoryInfo( destinationDirectory );
			if( !dest.Exists ) dest.Create();
			
			using( FileStream fs = new FileStream( archivePath, FileMode.Open, FileAccess.Read)) {
				
				TarLzmaDecoder decoder = new TarLzmaDecoder( fs );
				decoder.Extract( destinationDirectory );
				// for the limited number of files this is fairly quick, no need for progress events
				
			}
			
		}
		
		private static InstallationInfo GetInstallationInfo() {
			
			if( Env.IsX64 ) { // implies NT5.2
				
				Boolean isEnglish = (GetLanguage() == Language.English);
				
				return new InstallationInfo( Install.NT522X64English, !isEnglish ); // use English under all circumstances
				
			}
			
			if( Env.OSVersion.Version.Major == 5 ) {
				
				if( Env.OSVersion.Version.Minor == 1 && Env.ServicePack == 3 ) {
					// it's Windows XP SP3 (implies x86)
					
					// determine language
					Language lang = GetLanguage();
					switch(lang) {
						case Language.English:
							return new InstallationInfo( Install.NT513English, false );
						case Language.German:
							return new InstallationInfo( Install.NT513German , false );
						case Language.Spanish:
							return new InstallationInfo( Install.NT513Spanish, false );
						default:
							return new InstallationInfo( Install.NT513English, true );
					}
					
				}
				
				if( Env.OSVersion.Version.Minor == 2 && Env.ServicePack == 2 ) {
					// it's Windows Server 2003 (implies x86 because x64 is filtered earlier)
					
					// determine language
					Language lang = GetLanguage();
					switch(lang) {
						case Language.English:
							return new InstallationInfo( Install.NT522English, false );
						case Language.German:
							return new InstallationInfo( Install.NT522German , false );
						default:
							return new InstallationInfo( Install.NT522English, true );
					}
					
				}
				
			}
			
			return new InstallationInfo( Install.NotSupported, false );
		}
		
		private static Language GetLanguage() {
			
			UInt16 priLang = PackageUtility.GetSystemInstallPriLanguage();
			
			if( ((ushort)priLang & (ushort)Language.English) == (ushort)Language.English ) return Language.English;
			if( ((ushort)priLang & (ushort)Language.Spanish) == (ushort)Language.Spanish ) return Language.Spanish;
			if( ((ushort)priLang & (ushort)Language.German)  == (ushort)Language.German  ) return Language.German;
			
			return Language.Other;
		}
		
		private enum Install {
			NotSupported    = 0,
			NT513English    = 1,
			NT513German     = 2,
			NT513Spanish    = 3,
			NT522English    = 4,
			NT522German     = 5,
			NT522X64English = 6
		}
		
		private enum Language : ushort {
			English   = 0x09,
			German    = 0x07,
			Spanish   = 0x0A,
			Other     = 0x00
		}
		
		private struct InstallationInfo {
			
			public Install Install;
			public Boolean DifferentLanguage;
			public String  ExtractDirectory;
			
			public InstallationInfo(Install install, Boolean differentLanguage) {
				Install           = install;
				DifferentLanguage = differentLanguage;
				ExtractDirectory  = P.Combine( P.GetTempPath(), P.GetRandomFileName() );
				// GetRandomFileName doesn't create anything unlike GetTempFileName
			}
			
		}
		
#endregion
		
		
		
	}
}
