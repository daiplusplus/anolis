using System;
using System.Collections.Generic;
using System.Text;
using Anolis.Packages.Operations;
using System.Collections.ObjectModel;
using System.Xml;

using Cult = System.Globalization.CultureInfo;

namespace Anolis.Packages {
	
	public class PresetCollection : Collection<Preset> {
		
	}
	
	public class Preset {
		
		public Preset(String name) {
			Name = name;
		}
		
		public Preset(Group rootGroup, XmlElement presetElement) {
			
			Name = presetElement.GetAttribute("name");
			
			String groupId = presetElement.GetAttribute("bmpGroup");
			Group = rootGroup.Find( groupId ) as Group;
			if( !Group.IsResGroup ) throw new PackageValidationException(SF("bmpGroup", groupId) + ", it must be marked as IsResGroup");
			if( Group == null ) throw new PackageValidationException(SF("bmpGroup", groupId));
			
			String welcomeId = presetElement.GetAttribute("welcome");
			WelcomeScreen = rootGroup.Find( welcomeId ) as ResPatchOperation;
			if( WelcomeScreen == null ) throw new PackageValidationException(SF("welcome", welcomeId));
			
			String wallpaperId = presetElement.GetAttribute("wallpaper");
			Wallpaper = rootGroup.Find( wallpaperId ) as WallpaperExtraOperation;
			if( Wallpaper == null ) throw new PackageValidationException(SF("wallpaper", wallpaperId));
			
			String visualStyleId = presetElement.GetAttribute("visualStyle");
			VisualStyle = rootGroup.Find( visualStyleId ) as VisualStyleExtraOperation;
			if( VisualStyle == null ) throw new PackageValidationException(SF("visualStyle", visualStyleId));
		}
		
		private String SF(String type, String id) {
			
			return String.Format(Cult.InvariantCulture, "Invalid {0} '{1}' in preset {2}", type, id, Name);
		}
		
		public String                    Name          { get; private set; }
		
		public Group                     Group         { get; set; }
		public ResPatchOperation         WelcomeScreen { get; set; }
		public WallpaperExtraOperation   Wallpaper     { get; set; }
		public VisualStyleExtraOperation VisualStyle   { get; set; }
		
		public override String ToString() {
			return Name;
		}
		
#region Getters
		
		public static void GetPackageItems(Group rootGroup, out List<Group> bmps, out List<ResPatchOperation> rops, out List<WallpaperExtraOperation> wops, out List<VisualStyleExtraOperation> vops) {
			
			Predicate<ResPatchOperation> predicate = op => op.Path.EndsWith("system32\\logonui.exe", StringComparison.OrdinalIgnoreCase); // HACK: This is the best test I could think of without altering the schema
			
			bmps = GetBmpGroups(rootGroup);
			rops = GetOperations<ResPatchOperation>        (rootGroup, predicate);
			wops = GetOperations<WallpaperExtraOperation>  (rootGroup, null);
			vops = GetOperations<VisualStyleExtraOperation>(rootGroup, null);
		}
		
		private static List<Group> GetBmpGroups(Group inGroup) {
			
			List<Group> groups = new List<Group>();
			
			GetBmpGroups( inGroup, groups );
			
			return groups;
		}
		
		private static void GetBmpGroups(Group group, List<Group> list) {
			
			foreach(Group child in group.Children) {
				
				if( child.IsResGroup ) list.Add( child );
				else {
					GetBmpGroups( child, list );
				}
			}
			
		}
		
		private static List<T> GetOperations<T>(Group rootGroup, Predicate<T> predicate) where T : class {
			
			List<T> ops = new List<T>();
			
			GetOperations(predicate, rootGroup, ops );
			
			return ops;
		}
		
		private static void GetOperations<T>(Predicate<T> predicate, Group group, List<T> list) where T : class {
			
			foreach(Operation op in group.Operations) {
				
				T top = op as T;
				if(top == null) continue;
					
				if( predicate != null) { 
					if( predicate(top) ) {
						list.Add( top );
					}
				} else {
					list.Add( top );
				}
			}
			
			foreach(Group g in group.Children) {
				GetOperations(predicate, g, list );
			}
			
		}
		
	#endregion
		
	}
	
}
