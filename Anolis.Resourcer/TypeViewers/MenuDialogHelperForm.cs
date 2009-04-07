using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class MenuDialogHelperForm : Form {
		
		public MenuDialogHelperForm() {
			InitializeComponent();
		}
		
		public MainMenu DialogMenu { get { return __menu; } }
		
	}
}
