using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml;

using Anolis.Core;
using Anolis.Core.Data;
using Anolis.Core.Utility;

using P = System.IO.Path;

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Anolis.Core.Packages.Operations {
	
	public class ResPatchOperation : PatchOperation {
		
		public ResPatchOperation(Group parent, XmlElement operationElement) : base(parent, operationElement) {
			
			ResourceSet   = new PatchResourceSet( base.Condition, parent, ConditionHash );
			_resourceSets = new List<PatchResourceSet>() { ResourceSet };
			
			foreach(XmlNode node in operationElement.ChildNodes) {
				
				if(node.NodeType != XmlNodeType.Element) continue;
				
				XmlElement child = node as XmlElement;
				
				if( child.Name != "res" ) continue;
				
				PatchResource res = new PatchResource() {
					Type   = child.GetAttribute("type"),
					Name   = child.GetAttribute("name"),
					Lang   = child.GetAttribute("lang"),
					Source = child.GetAttribute("src"),
					Add    = child.GetAttribute("add") == "true" || child.GetAttribute("add") == "1"
				};
				
				if( !res.Source.StartsWith("comp:", StringComparison.Ordinal) )
					res.Source = P.Combine( parent.Package.RootDirectory.FullName, res.Source );
				
				ResourceSet.Resources.Add( res );
			}
			
		}
		
		public ResPatchOperation(Group parent, String path) : base(parent, path) {
			
			ResourceSet   = new PatchResourceSet( base.Condition, parent, null );
			_resourceSets = new List<PatchResourceSet>() { ResourceSet };
		}
		
		public override String OperationName {
			get { return "Res patch"; }
		}
		
		public override Boolean SupportsCDImage {
			get { return true; }
		}
		
		public override Boolean CustomEvaluation {
			get { return true; }
		}
		
		public PatchResourceSet ResourceSet { get; private set; }
		
		private List<PatchResourceSet> _resourceSets;
		
		public override Boolean Merge(Operation operation) {
			
			ResPatchOperation op = operation as ResPatchOperation;
			if( op == null ) return false;
			
			if( !String.Equals( Path, op.Path, StringComparison.OrdinalIgnoreCase ) ) return false;
			
			// add the incoming operation's sets to this own
			_resourceSets.AddRange( op._resourceSets );
			
			return true;
		}
		
		protected override Boolean PatchFile(String fileName) {
			
			List<PatchResource> patchResources = new List<PatchResource>();
			foreach(PatchResourceSet set in _resourceSets) {
				
				if( EvaluatePatchResourceSet( set, fileName ) ) {
					
					// HACK: This just adds them together into a massive list. If the same name is mentioned it'll be overwritten several times
					// fortunately it isnt' very expensive as only the last "final" one counts, but could do with filtering at this stage maybe?
					
					patchResources.AddRange( set.Resources );
					
				} else {
					Package.Log.Add( LogSeverity.Info, "Expression evaluation non-one: " + set.Condition.ExpressionString + ", did not process " + set.Resources.Count + " resources" );
				}
				
			}
			
			if( patchResources.Count == 0 ) {
				
				Package.Log.Add( LogSeverity.Warning, "No resources to patch: " + fileName );
				return false;
			}
			
			try {
				
				// for now, use lazy-load under all circumstances. In future analyse the Resources list to see if it's necessary or not
				// but the performance impact is minimal and it's the safest option, so keep it as it is
				using(ResourceSource source = ResourceSource.Open(fileName, false, ResourceSourceLoadMode.LazyLoadData)) {
					
					List<String> tempFiles = new List<String>();
					
					foreach(PatchResource res in patchResources) {
						
						if( res.Source.StartsWith("comp:", StringComparison.OrdinalIgnoreCase) ) {
							
							CompositedImage comp = new CompositedImage( res.Source, Package.RootDirectory );
							
							DirectoryInfo packageTempDirectory = new DirectoryInfo( P.Combine( Package.RootDirectory.FullName, "Temp" ) );
							if( !packageTempDirectory.Exists ) packageTempDirectory.Create();
							
							// I think not using the *.bmp extension messes up Bitmap import
							String tempFileName = PackageUtility.GetUnusedFileName( P.Combine( packageTempDirectory.FullName, P.GetFileName(Path) + res.Name ) + ".bmp" );
							
							comp.Save( tempFileName, System.Drawing.Imaging.ImageFormat.Bmp );
							
							res.File = tempFileName;
							tempFiles.Add( tempFileName );
							
						} else {
							res.File = res.Source;
						}
						
						if( !File.Exists( res.File ) ) {
							Package.Log.Add( LogSeverity.Error, "Data File not found: " + res.File );
							continue;
						}
						
						ResourceTypeIdentifier typeId = ResourceTypeIdentifier.CreateFromString( res.Type, true );
						ResourceIdentifier     nameId = ResourceIdentifier.CreateFromString( res.Name );
						UInt16                 langId = String.IsNullOrEmpty( res.Lang ) ? UInt16.MaxValue : UInt16.Parse( res.Lang, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture );
						
						
						if(	langId == UInt16.MaxValue ) { // if the lang="" attribute was not specified
							
							ResourceName name = source.GetName(typeId, nameId);
							
							if(name == null) { // if the specified name does not exist
								
								if( res.Add ) {
									
									UInt16 sysLang = (UInt16)CultureInfo.InvariantCulture.LCID;
									
									ResourceData data = ResourceData.FromFileToAdd( res.File, sysLang, source );
									source.Add( typeId, nameId, sysLang, data );
									
								} else {
									// Error
									
									String sourcePath = source.Name;
									
									Anolis.Core.Source.FileResourceSource frs = source as Anolis.Core.Source.FileResourceSource;
									if( frs != null ) sourcePath = frs.FileInfo.FullName;
									
									Package.Log.Add( LogSeverity.Warning, "Resource name not found: " + sourcePath + '\\' + typeId.ToString() + '\\' + nameId.FriendlyName );
								}
								
								
							} else {
								
								foreach(ResourceLang lang in name.Langs) {
									
									ResourceData data = ResourceData.FromFileToUpdate( res.File, lang );
									lang.SwapData( data );
									
								}
								
							}
							
						} else { // if the lang="" attribute was specified
							
							ResourceLang lang = source.GetLang( typeId, nameId, langId );
							if(lang == null) {
								
								ResourceData data = ResourceData.FromFileToAdd( res.File, langId, source );
								source.Add( typeId, nameId, langId, data );
								
							} else {
								
								ResourceData data = ResourceData.FromFileToUpdate( res.File, lang );
								lang.SwapData( data );
							}
							
						}
						
					}//foreach
					
					// note that Win32ResourceSource now recomptues the PE checksum by itself
					source.CommitChanges();
					
					foreach(String tempFile in tempFiles) File.Delete( tempFile );
					
					return true;
					
				}//using source
				
			} catch(AnolisException aex) {
				
				Package.Log.Add( LogSeverity.Error, "Patch Exception: " + aex.Message );
				
				if( File.Exists( fileName ) ) File.Delete( fileName );
				
				throw;
			}
			
		}
		
		public override void Write(XmlElement parent) {
			
			foreach(PatchResourceSet set in this._resourceSets) {
				
				XmlElement element = CreateElement(parent, "patch", "path", Path);
				if( set.Condition != null ) {
					XmlAttribute attrib = element.Attributes.GetNamedItem("condition") as XmlAttribute;
					if( attrib != null ) {
						attrib.Value = set.Condition.ExpressionString;
					} else {
						AddAttribute( element, "condition", set.Condition.ExpressionString );
					}
				}
				
				foreach(PatchResource res in set.Resources) {
					
					XmlElement re = CreateElement(element, "res");
					AddAttribute(re, "type", res.Type);
					AddAttribute(re, "name", res.Name);
					AddAttribute(re, "lang", res.Lang);
					AddAttribute(re, "src", res.Source);
					if( res.Add ) AddAttribute(re, "add", "true");
					
				}
				
			}
		}
		
		private Boolean EvaluatePatchResourceSet(PatchResourceSet set, String fileName) {
			
			// you can't move this to PatchResourceSet.Evaluate because it contains references to protected members
			
			if( set.ParentGroup != null ) {
				
				if( set.ParentGroup.Evaluate() != EvaluationResult.True ) return false;
			}
			
			if( !String.IsNullOrEmpty( set.ConditionHash ) ) {
				
				String currentHash = PackageUtility.GetMD5Hash( fileName );
				if( !String.Equals( set.ConditionHash, currentHash, StringComparison.OrdinalIgnoreCase ) ) {
					
					return false;
				}
				
			}
			
			if( set.Condition != null ) {
				
				Dictionary<String,Double> symbols = GetSymbols( fileName );
				
				try {
					Double result = set.Condition.Evaluate( symbols );
					
					if(result != 1) return false;
					
				} catch(ExpressionException ex) {
					
					if( Package != null ) Package.Log.Add( LogSeverity.Warning, "Expression evaluation exception: " + ex.Message );
					
					return false;
				}
				
			}
			
			return true;
			
		}
		
	}
	
	public class PatchResourceSet {
		
		public PatchResourceSet(Expression expression, Group parentGroup, String conditionHash) {
			
			Resources  = new Collection<PatchResource>();
			
			Condition     = expression;
			ParentGroup   = parentGroup;
			ConditionHash = conditionHash;
		}
		
		public Group      ParentGroup   { get; private set; }
		
		public Expression Condition     { get; private set; }
		
		public String     ConditionHash { get; private set; }
		
		public Collection<PatchResource> Resources { get; private set; }
		
	}
	
	public class PatchResource {
		
		public String Type { get; set; }
		
		public String Name { get; set; }
		
		public String Lang { get; set; }
		
		public String Source { get; set; }
		
		/// <summary>True if the resource is to be added as opposed to updated.</summary>
		public Boolean Add { get; set; }
		
		internal String File { get; set; }
	}
	
}
