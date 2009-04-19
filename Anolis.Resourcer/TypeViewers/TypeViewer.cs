//#define MEDIAVIEWER

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Anolis.Core;
using Anolis.Core.Data;

namespace Anolis.Resourcer.TypeViewers {
	
	public class TypeViewerFactory : FactoryBase {
		
		private static TypeViewerWrapper[] _viewers;
		
		public static TypeViewerWrapper[] GetViewers() {
			
			if( _viewers == null ) {
				
				List<TypeViewer> list = new List<TypeViewer>();
				Prepopulate( list );
				LoadFromAssemblies( list );
				
				TypeViewerWrapper[] viewers = TypeViewerWrapper.FromArray( list.ToArray() );
				
				_viewers = viewers;
			}
			
			return _viewers;
			
		}
		
		private static void Prepopulate(List<TypeViewer> viewers) {
			
			viewers.AddRange(
				new TypeViewer[] {
					new TextViewer(),
					new RawViewer(),
					new ImageViewer(),
					new SgmlViewer(),
					
					new IconCursorViewer(),
					new VersionViewer(),
					new MenuDialogViewer(),
					new StringTableViewer(),
#if MEDIAVIEWER
					new MediaViewer(),
#endif
				}
			);
			
		}
		
		private static void LoadFromAssemblies(List<TypeViewer> viewers) {
			
			FactoryBase.LoadFactoriesFromAssemblies<TypeViewer>( viewers );
		}
		
	}
	
	public class TypeViewer : UserControl { // can't define as abstract thanks to the WinForms designer
		
		public virtual void RenderResource(ResourceData resource) {
			throw new NotImplementedException();
		}
		
		/// <summary>This Type Viewer can handle the specified resource data. Also if this viewer is the recommended viewer for that type.</summary>
		public virtual TypeViewerCompatibility CanHandleResource(ResourceData data) {
			throw new NotImplementedException();
		}
		
		public virtual String ViewerName { get { throw new NotImplementedException(); } }
		
	}
	
	public class TypeViewerWrapper {
		
		public TypeViewerCompatibility Recommended { get; set; }
		public TypeViewer Viewer { get; private set; }
		public TypeViewerWrapper(TypeViewer viewer) {
			Viewer = viewer;
		}
		public override string ToString() {
			return Viewer.ViewerName +
				(Recommended == TypeViewerCompatibility.Ideal ? " (Recommended)" : 
				(Recommended == TypeViewerCompatibility.None  ? " (Not recommended)" : ""));
		}
		public static TypeViewerWrapper[] FromArray(params TypeViewer[] viewers) {
			
			TypeViewerWrapper[] retval = new TypeViewerWrapper[viewers.Length];
			for(int i=0;i<viewers.Length;i++) retval[i] = new TypeViewerWrapper( viewers[i] );
			
			return retval;
		}
	}
	
	public enum TypeViewerCompatibility {
		Ideal,
		Works,
		None
	}
	
}
