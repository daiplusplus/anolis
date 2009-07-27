using System;
using System.Collections.Generic;
using System.Xml;
using System.Drawing;

using Anolis.Core.Utility;
using Cult    = System.Globalization.CultureInfo;
using Env     = Anolis.Core.Utility.Environment;
using N       = System.Globalization.NumberStyles;
using Symbols = System.Collections.Generic.Dictionary<System.String,System.Double>;

namespace Anolis.Core.Packages {
	
	public abstract class PackageBase {
		
		/// <summary>Is null if this is the package.</summary>
		public Package    Package   { get; internal set; }
		
		public String     Id        { get; set; }
		public String     Name      { get; set; }
		
		public Expression Condition { get; set; }
		
		protected PackageBase(Package package, XmlElement element) {
			
			Package = package;
			
			Package thisPackage = this as Package;
			if( thisPackage != null ) Package = thisPackage;
			
			Id      = element.GetAttribute("id");
			Name    = element.GetAttribute("name");
			
			String conditionExpr = element.GetAttribute("condition");
			if(conditionExpr.Length > 0)
				Condition = new Expression( conditionExpr );
			
		}
		
		protected PackageBase(Package package) {
			Package = package;
		}
		
		public abstract EvaluationResult Evaluate();
		
		protected EvaluationResult EvaluateCondition(Dictionary<String,Double> symbols) {
			
			if( Condition == null ) return EvaluationResult.True;
			
			try {
				Double result = Condition.Evaluate( symbols );
				
				if(result == 1) return EvaluationResult.True;
				return EvaluationResult.False;
				
			} catch(ExpressionException ex) {
				
				if( Package != null ) Package.Log.Add( LogSeverity.Warning, "Expression evaluation exception: " + ex.Message );
				
				return EvaluationResult.Error;
			}
		}
		
		private Symbols _baseSymbols;
		
		private Boolean ShouldCreateBaseSymbols() {
			
			if( _baseSymbols == null ) return true;
			
			if( Package.ExecutionInfo == null ) return true;
			
			Double cdImage;
			if( _baseSymbols.TryGetValue("cdImage", out cdImage) ) {
				return cdImage == -1;
			}
			
			return false;
		}
		
		protected Symbols GetSymbols() {
			
			if( ShouldCreateBaseSymbols() ) {
				
				// during the check before Execution mode is decided, it needs to tell if it's right for this system so it can tell the user it can only be installed on a CD Image
				if( Package.ExecutionInfo == null || Package.ExecutionInfo.ExecutionMode == PackageExecutionMode.Regular ) {
					
					_baseSymbols = new Symbols() {
						
						{"osversion"   , Env.OSVersion.Version.Major + ( (Double)Env.OSVersion.Version.Minor ) / 10 },
						{"servicepack" , Env.ServicePack },
						{"architecture", Env.IsX64 ? 64 : 32 },
						{"installlang" , Cult.InstalledUICulture.LCID },
						{"cdImage"     , 0 }
					};
					
				} else {
					
					CDImageOperatingSystem cdo = Package.ExecutionInfo.CDImage.OperatingSystem;
					Version ver = cdo.OSVersion.Version;
					
					_baseSymbols = new Symbols() {
						
						{"osversion"   , ver.Major + ( (Double)ver.Minor ) / 10 },
						{"servicepack" , cdo.ServicePack },
						{"architecture", cdo.IsX64 ? 64 : 32 },
						{"installlang" , cdo.Language.LCID },
						{"cdImage"     , 1 }
					};
					
				}
				
			}
			
			return _baseSymbols.Clone();
		}
		
		private static Dictionary<String,Symbols> _allSymbols = new Dictionary<String,Symbols>();
		
		protected Symbols GetSymbols(String fileName) {
			
			/////////////////////////////////
			// Check it hasn't already been done
			String allSymbolsKey = fileName.ToUpperInvariant();
			if( _allSymbols.ContainsKey( allSymbolsKey ) ) {
				
				return _allSymbols[ allSymbolsKey ];
			}
			
			Symbols symbols = GetSymbols();
			
			
			try {
				
				AddVSVersionToSymbols( fileName, symbols );
				
				AddPEMachineTypeToSymbols( fileName, symbols );
				
			} catch(AnolisException aex) {
				
				LogItem item = new LogItem( LogSeverity.Error, aex, "Could not build symbols for \"" + fileName + "\" due to an exception: " + aex.Message );
				Package.Log.Add( item );
			}
			
			
			_allSymbols.Add( allSymbolsKey, symbols );
			
			return symbols;
		}
		
		private static void AddPEMachineTypeToSymbols(String fileName, Dictionary<String,Double> symbols) {
			
			Anolis.Core.Native.MachineType type = Miscellaneous.GetMachineType( fileName );
			switch(type) {
				case Anolis.Core.Native.MachineType.Amd64:
					symbols.Add("peType", 64);
					break;
				case Anolis.Core.Native.MachineType.I386:
					symbols.Add("peType", 86);
					break;
				default:
					symbols.Add("peType", -1);
					break;
			}
			
		}
		
		private static void AddVSVersionToSymbols(String fileName, Dictionary<String,Double> symbols) {
			
			// this is gonna be expensive...
			
			// retrieve the VS_VERSION_INFO
			
			Version version = GetFileVersion( fileName );
			
			Double major = version.Major;
			Double minor = version.Minor;
			
			symbols.Add("fileVersion",  major + (minor / 10f) );
			
		}
		
		internal static Version GetFileVersion(String fileName) {
			
			Anolis.Core.Data.VersionResourceData vd = GetVSVersion( fileName );
			if( vd == null ) return null;
			
			Dictionary<String,String> table = vd.GetStringTable();
			String fileVersion;
			if( !table.TryGetValue("FileVersion", out fileVersion) ) return null;
			
			if( String.IsNullOrEmpty( fileVersion ) ) return null;
			
			Version vs = new Version( fileVersion );
			
			return vs;
			
		}
		
		private static Anolis.Core.Data.VersionResourceData GetVSVersion(String fileName) {
			
			using(ResourceSource src = ResourceSource.Open( fileName, true, ResourceSourceLoadMode.LazyLoadData )) {
				
				ResourceType vsType = null;
				foreach(ResourceType type in src.AllTypes) {
					if( type.Identifier.KnownType == Win32ResourceType.Version ) {
						vsType = type;
						break;
					}
				}
				
				if( vsType == null ) return null;
					
				foreach(ResourceName name in vsType.Names) {
					foreach(ResourceLang lang in name.Langs) {
						
						return lang.Data as Anolis.Core.Data.VersionResourceData;
					}
				}
				
				return null;
				
			}
			
		}
		
	}
	
}
