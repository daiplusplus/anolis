using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Anolis.Core;
using System.Text;

namespace Anolis.Console.Scripting {
	
	public class Script {
		
		// I don't like how ResHacker refered to these as "Scripts", more of a "operations list"
		// if it were a proper script it'd be turing-complete
		
		private Script() {
			
		}
		
		public static Script FromCmdArgs(String[] args) {
			
			
			return null;
		}
		
		public static Script FromFile(String filename) {
			
			if(!File.Exists(filename)) throw new FileNotFoundException("The specified script file was not found", filename);
			
			
			return null;
		}
		
		public Collection<Operation> Operations { get; set; }
		
		public void Execute() {
			
		}
		
		public Log Log { get; private set; }
		
	}
	
	public class Log {
		
	}
	
	public class Operation {
		
		private Script _script;
		
		public Operation(Script script, Command command, ResourceSource targetFile, String saveTargetas, ResourceSource sourceFile, String resourceName) {
			
			_script = script;
			
			Filter = new ResourceNameFilter( resourceName );
			
		}
		
		// TODO: Overloaded constructor for Extract
		
		public Command            Command      { get; private set; }
		public ResourceSource     TargetFile   { get; private set; }
		public FileInfo           TargetSaveTo { get; private set; }
		public ResourceSource     SourceFile   { get; private set; }
		public ResourceNameFilter Filter       { get; private set; }
		
	}
	
	public enum Command {
		Add,
		AddSkip,
		AddOverwrite,
		Modify,
		Extract,
		Delete
	}
	
	public class ResourceNameFilter {
		
		public ResourceNameFilter(String resourceNameFilter) {
			
			Type = GetTypeId( resourceNameFilter );
			Name = GetNameId( resourceNameFilter );
			Lang = GetLangId( resourceNameFilter );
			
			if(Type == null && (Name != null || Lang != null)) throw new ScriptException("Malformed resourceName filter");
			if(Name == null && Lang != null)                   throw new ScriptException("Malformed resourceName filter");
			
		}
		
		private static ResourceTypeIdentifier GetTypeId(String resourceNameFilter) {
			
			String typeStr = resourceNameFilter.Substring(0, resourceNameFilter.IndexOf(',') );
			
			if(typeStr.Length == 0) return null;
			
			UInt16 typeIdInt = 0;
			if(UInt16.TryParse(typeStr, out typeIdInt)) {
				return new ResourceTypeIdentifier( new IntPtr( typeIdInt ) );
			} else {
				return new ResourceTypeIdentifier( typeStr );
			}
			
		}
		
		private static ResourceIdentifier GetNameId(String resourceNameFilter) {
			
			int comma1Idx = resourceNameFilter.IndexOf(',');
			int comma2Idx = resourceNameFilter.IndexOf(',', comma1Idx );
			
			String nameStr = resourceNameFilter.Substring( comma2Idx );
			if(nameStr.Length == 0) return null;
			
			UInt16 nameIdInt = 0;
			if(UInt16.TryParse(nameStr, out nameIdInt)) {
				return new ResourceIdentifier( nameIdInt );
			} else {
				return new ResourceIdentifier( nameStr );
			}
			
		}
		
		private static UInt16? GetLangId(String resourceNameFilter) {
			
			StringBuilder sb = new StringBuilder();
			
			Int32 i = resourceNameFilter.IndexOf(',',  resourceNameFilter.IndexOf(',') );
			while(resourceNameFilter[i] != ',') sb.Append( resourceNameFilter[i++] );
			
			String langId = sb.ToString();
			if(langId.Length == 0) return null;
			
			UInt16 langIdInt = 0;
			if(UInt16.TryParse(langId, out langIdInt)) return langIdInt;
			
			return null;
			
		}
		
		public ResourceTypeIdentifier Type { get; private set; }
		public ResourceIdentifier     Name { get; private set; }
		public UInt16?                Lang { get; private set; }
		
		public Boolean IsSpecific {
			get {
				return Type != null && Name != null && Lang != null;
			}
		}
		
	}
	
	[Serializable]
	public class ScriptException : Exception {
		
		public ScriptException() {
		}
		
		public ScriptException(String message) : base(message) {
		}
		
		public ScriptException(String message, Exception inner) : base(message, inner) {
		}
		
		protected ScriptException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) {
		}
	}
	
}
