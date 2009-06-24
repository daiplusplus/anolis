using System;
using System.IO;
using System.Xml;

using P = System.IO.Path;

namespace Anolis.Core.Packages.Operations {
	
	/// <summary>Creates an entry in the Add/Remove programs list and finalises the Backup directory</summary>
	public class UninstallationOperation : Operation {
		
		public UninstallationOperation(Package package, Group group, XmlElement operationElement) : base(package, group, operationElement) {
			
			
		}
		
		public UninstallationOperation(Package package, Group parent) : base(package, parent, (String)null) {
			
			
		}
		
		public override void Execute() {
			
			// add the registry key and copy this Anolis.Installer program to the Backup directory. The uninstallation command will supply the path to the uninstall.xml file
			
		}
		
		public override void Backup(Group backupGroup) {
			
		}
		
		public override void Write(XmlElement parent) {
			
			CreateElement(parent, "uninstallation");
		}
		
		public override String OperationName {
			get { return "File"; }
		}
		
		public override Boolean Merge(Operation operation) {
			
			// there can only be one Uninstallation operation
			
			throw new PackageException("There can only be one active UninstallationOperation in a package");
			
		}
	}
	
}
