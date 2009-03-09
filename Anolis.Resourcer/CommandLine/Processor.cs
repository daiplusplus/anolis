using System;
using System.IO;

using Anolis.Core;
using Anolis.Core.Data;

namespace Anolis.Resourcer.CommandLine {
	
	public static class StatelessResourceEditor {
		
		public static Int32 ProcessBatch(String batchfile) {
			
			return 0;
		}
		
		public static Int32 PerformOneOff(CommandLineParser args) {
			
			// one-off command:
			// Anolis.Resourcer.exe -op:add -src:"C:\dest.exe" -type:ICONGROUP -name:NAME  -lang:1033  -file:"C:\foo\icon.ico"
			// Anolis.Resourcer.exe -op:upd -src:"C:\dest.exe" -type:ICONGROUP -name:NAME [-lang:1033] -file:"C:\foo\icon.ico"
			// Anolis.Resourcer.exe -op:ext -src:"C:\dest.exe" -type:ICONGROUP -name:NAME  -lang:1033  -file:"C:\foo\icon.ico"
			// Anolis.Resourcer.exe -op:del -src:"C:\dest.exe" -type:ICONGROUP -name:NAME [-lang:1033]
			
			CommandLineFlag
				operation = args.GetFlag("op"),
				resSource = args.GetFlag("src"),
				dataType  = args.GetFlag("type"),
				dataName  = args.GetFlag("name"),
				dataLang  = args.GetFlag("lang"),
				dataFile  = args.GetFlag("file");
			UInt16 dataLangN = 0;
			
			///////////////////////////////////////
			// Validation
			
			// Individual arguments
			
			if(operation == null || resSource == null || dataType == null || dataName == null) return 1;
			
			switch(operation.Argument) {
				case "add":
				case "upd":
				case "ext": 
				case "del":
					break;
				default:
					return 1;
			}
			
			if( !File.Exists( resSource.Argument ) ) return 2;
			
			if( dataLang != null ) {
				
				String num = dataLang.Argument;
				
				if( !UInt16.TryParse( num, out dataLangN ) ) return 1;
			}
			
			// Combinations
			
			//////// Add
			if( operation.Argument == "add" && dataLang == null )                  return 1;
			if( operation.Argument == "add" && !File.Exists( dataFile.Argument ) ) return 2;
			
			//////// Update
			if( operation.Argument == "upd" && !File.Exists( dataFile.Argument ) ) return 2;
			
			//////// Extract
			
			//////// Delete
			if(!operation.Argument.StartsWith("del") && dataFile == null)          return 1;
			
			///////////////////////////////////////
			
			ResourceTypeIdentifier typeId = ResourceTypeIdentifier.CreateFromString( dataType.Argument, true );
			ResourceIdentifier     nameId = ResourceIdentifier    .CreateFromString( dataName.Argument );
			UInt16                 langId = dataLang != null ? dataLangN : UInt16.MinValue;
			
			switch(operation.Argument) {
				case "del":
					
					return OneOffDelete( resSource.Argument, typeId, nameId );
					
				case "upd":
					
					//ResourceData data = ResourceData.FromFile( dataFile.Argument, 
					
					break;
				case "ext":
					
					
					
					break;
			}
			
			return 0;
		}
		
#region Delete
		
		private static Int32 OneOffDelete( String sourceFilename, ResourceTypeIdentifier typeId, ResourceIdentifier nameId ) {
			
			ResourceSource source = ResourceSource.Open( sourceFilename, false, ResourceSourceLoadMode.EnumerateOnly );
			
			// delete all the ResourceLangs that match this typeId/nameId criterion. When reloaded the ResourceName should no-longer exist
			
			ResourceName name = GetName( source, typeId, nameId );
			
			foreach(ResourceLang lang in name.Langs) source.Remove( lang );
			
			source.CommitChanges();
			
			return 0;
		}
		
#endregion
			
#region Add
		
		private static Int32 OneOffAdd( String sourceFilename, ResourceTypeIdentifier typeId, ResourceIdentifier nameId, UInt16 langId, String dataFilename ) {
			
			ResourceSource source = ResourceSource.Open( sourceFilename, false, ResourceSourceLoadMode.Blind );
			
			ResourceData data = ResourceData.FromFileToAdd( dataFilename, langId, source );
			
			source.Add( typeId, nameId, langId, data );
			
			source.CommitChanges();
			
			return 0;
		}
		
#endregion
		
		private static ResourceName GetName(ResourceSource source, ResourceTypeIdentifier typeId, ResourceIdentifier nameId) {
			
			if(source.LoadMode == 0) throw new ArgumentException("The specified ResourceSource has not enumerated its resources");
			
			foreach(ResourceType type in source.AllTypes) {
				
				if( type.Identifier == typeId ) {
					
					foreach(ResourceName name in type.Names) {
						
						if(name.Identifier == nameId) {
							
							return name;
						}
					}
					
				}//if
				
			}//foreach
			
			return null;
			
		}
		
	}
}
