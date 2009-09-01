using System;
using System.IO;

using Anolis.Core;
using Anolis.Core.Data;

namespace Anolis.Resourcer.CommandLine {
	
	public static class StatelessResourceEditor {
		
		public static StatelessResult ProcessBatch(String batchfile) {
			
			return new StatelessResult("Not implemented");
		}
		
		public static StatelessResult PerformOneOff(CommandLineParser args) {
			
			// one-off command:
			// Anolis.Resourcer.exe -op:add -src:"C:\dest.exe" -type:ICONDIR -name:NAME  -lang:1033  -file:"C:\foo\icon.ico"
			// Anolis.Resourcer.exe -op:upd -src:"C:\dest.exe" -type:ICONDIR -name:NAME [-lang:1033] -file:"C:\foo\icon.ico"
			// Anolis.Resourcer.exe -op:ext -src:"C:\dest.exe" -type:ICONDIR -name:NAME  -lang:1033  -file:"C:\foo\icon.ico"
			// Anolis.Resourcer.exe -op:del -src:"C:\dest.exe" -type:ICONDIR -name:NAME [-lang:1033]
			
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
			
			if(operation == null || resSource == null || dataType == null || dataName == null) return new StatelessResult(false, "Syntax error: not all arguments set");
			
			switch(operation.Argument) {
				case "add":
				case "upd":
				case "ext": 
				case "del":
					break;
				default:
					return new StatelessResult("Syntax error: invalid operation value, must be one of 'add', 'del', 'upd', or 'ext'.");
			}
			
			// resolve fileNames
			if(                      !String.IsNullOrEmpty( resSource.Argument ) ) resSource.Argument = Path.GetFullPath( resSource.Argument );
			if( dataFile  != null && !String.IsNullOrEmpty(  dataFile.Argument ) )  dataFile.Argument = Path.GetFullPath(  dataFile.Argument );
			
			if( !File.Exists( resSource.Argument ) ) return new StatelessResult("File not found: " + resSource.Argument);
			
			if( dataLang != null ) {
				
				String num = dataLang.Argument;
				
				if( !UInt16.TryParse( num, out dataLangN ) ) return new StatelessResult("Syntax error: langId is not an unsigned integer less than 2^16");
			}
			
			// Combinations
			
			//////// Add
			if( operation.Argument == "add" && dataLang == null )                  return new StatelessResult("LangId not specified for add");
			if( operation.Argument == "add" && !File.Exists( dataFile.Argument ) ) return new StatelessResult("File not found: " + dataFile.Argument);
			
			//////// Update
			if( operation.Argument == "upd" && !File.Exists( dataFile.Argument ) ) return new StatelessResult("File not found: " + dataFile.Argument);
			
			//////// Extract
			
			//////// Delete
			if( operation.Argument != "del" && dataFile == null) return new StatelessResult("A file must be specified");
			
			///////////////////////////////////////
			
			ResourceTypeIdentifier typeId = ResourceTypeIdentifier.CreateFromString( dataType.Argument, true );
			ResourceIdentifier     nameId = ResourceIdentifier    .CreateFromString( dataName.Argument );
			UInt16                 langId = dataLang != null ? dataLangN : UInt16.MinValue;
			
			switch(operation.Argument) {
				case "del":
					
					return OneOffDelete( resSource.Argument, typeId, nameId );
					
				case "add":
					
					return OneOffAdd( resSource.Argument, typeId, nameId, langId, dataFile.Argument );
					
				case "upd":
					
					return dataLang != null ?
						OneOffUpdate(resSource.Argument, typeId, nameId, langId, dataFile.Argument) :
						OneOffUpdate(resSource.Argument, typeId, nameId,         dataFile.Argument);
					
				case "ext":
					
					return OneOffExtract(resSource.Argument, typeId, nameId, langId, dataFile.Argument);
			}
			
			return new StatelessResult("No valid operation specified, must be one of 'add', 'del', 'upd', or 'ext'.");
		}
		
#region Delete
		
		private static StatelessResult OneOffDelete( String sourceFilename, ResourceTypeIdentifier typeId, ResourceIdentifier nameId ) {
			
			ResourceSource source = ResourceSource.Open( sourceFilename, false, ResourceSourceLoadMode.EnumerateOnly );
			
			// delete all the ResourceLangs that match this typeId/nameId criterion. When reloaded the ResourceName should no-longer exist
			
			ResourceName name = source.GetName( typeId, nameId );
			if(name == null) return new StatelessResult("Could not find name " + GetResPath(typeId, nameId, null) + " to delete.");;
			
			foreach(ResourceLang lang in name.Langs) source.Remove( lang );
			
			source.CommitChanges();
			
			return StatelessResult.Success;
		}
		
		private static StatelessResult OneOffDelete( String sourceFilename, ResourceTypeIdentifier typeId, ResourceIdentifier nameId, UInt16 langId) {
			
			ResourceSource source = ResourceSource.Open( sourceFilename, false, ResourceSourceLoadMode.Blind );
			
			source.Remove( typeId, nameId, langId );
			
			source.CommitChanges();
			
			return StatelessResult.Success;
		}
		
#endregion
		
#region Add
		
		private static StatelessResult OneOffAdd( String sourceFilename, ResourceTypeIdentifier typeId, ResourceIdentifier nameId, UInt16 langId, String dataFilename ) {
			
			ResourceSource source = ResourceSource.Open( sourceFilename, false, ResourceSourceLoadMode.EnumerateOnly );
			
			ResourceData data = ResourceData.FromFileToAdd( dataFilename, langId, source );
			
			source.Add( typeId, nameId, langId, data );
			
			source.CommitChanges();
			
			return StatelessResult.Success;
		}
		
#endregion
		
		
#region Update
		
		private static StatelessResult OneOffUpdate( String sourceFilename, ResourceTypeIdentifier typeId, ResourceIdentifier nameId, UInt16 langId, String dataFilename ) {
			
			ResourceSource source = ResourceSource.Open( sourceFilename, false, ResourceSourceLoadMode.LazyLoadData );
			
			ResourceLang lang = source.GetLang(typeId, nameId, langId);
			if( lang == null ) return new StatelessResult("Could not find lang " + GetResPath(typeId, nameId, langId) + " to update.");
			
			ResourceData newData = ResourceData.FromFileToUpdate( dataFilename, lang );
			lang.SwapData( newData );
			
			source.CommitChanges();
			
			return StatelessResult.Success;
		}
		
		private static StatelessResult OneOffUpdate( String sourceFilename, ResourceTypeIdentifier typeId, ResourceIdentifier nameId, String dataFilename ) {
			
			ResourceSource source = ResourceSource.Open( sourceFilename, false, ResourceSourceLoadMode.LazyLoadData );
			
			// for each lang in name, update
			ResourceName name = source.GetName(typeId, nameId);
			if(name == null) return new StatelessResult("Could not find name " + GetResPath(typeId, nameId, null) + " to extract.");
			
			foreach(ResourceLang lang in name.Langs) {
				
				ResourceData newData = ResourceData.FromFileToUpdate( dataFilename, lang );
				lang.SwapData( newData );
				
			}
			
			source.CommitChanges();
			
			return StatelessResult.Success;
			
		}
		
#endregion
		
#region Extract
		
		private static StatelessResult OneOffExtract( String sourceFilename, ResourceTypeIdentifier typeId, ResourceIdentifier nameId, UInt16 langId, String dataFilename) {
			
			ResourceSource source = ResourceSource.Open( sourceFilename, true, ResourceSourceLoadMode.LazyLoadData );
			
			ResourceLang lang = source.GetLang( typeId, nameId, langId );
			
			if(lang == null) return new StatelessResult("Could not find lang " + GetResPath(typeId, nameId, langId) + " to extract.");
			
			lang.Data.Save( dataFilename );
			
			return StatelessResult.Success;
		}
		
#endregion
		
		private static String GetResPath(ResourceTypeIdentifier typeId, ResourceIdentifier nameId, UInt16? langId) {
			
			String ret = String.Empty;
			
			if(typeId.KnownType == Win32ResourceType.Custom) {
				
				ret += typeId.StringId;
				
			} else {
				
				ret += typeId.IntegerId.Value.ToStringInvariant();
			}
			
			ret += '\\' + nameId.FriendlyName;
			
			if( langId != null ) ret += '\\' + langId.Value.ToStringInvariant();
			
			return ret;
		}
		
	}
	
	public class StatelessResult {
		
		public StatelessResult(Boolean success) {
			
			WasSuccess   = success;
			ErrorMessage = null;
		}
		public StatelessResult(Boolean success, String errorMessage) {
			
			WasSuccess   = success;
			ErrorMessage = errorMessage;
		}
		public StatelessResult(String errorMessage) {
			
			WasSuccess   = false;
			ErrorMessage = errorMessage;
		}
		
		public Boolean WasSuccess   { get; set; }
		public String  ErrorMessage { get; set; }
		
		public static readonly StatelessResult Success = new StatelessResult(true);
	}
	
}
