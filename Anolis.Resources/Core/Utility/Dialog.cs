using System;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Text;

using Anolis.Core.Native;

namespace Anolis.Core.Utility {
	
	public class DialogBase {
		
		public String Text     { get; set; }
		public Size   Size     { get; set; }
		public Point  Location { get; set; }
		public Object WindowClass { get; set; }
		
	}
	
	public class Dialog : DialogBase {
		
		private Dialog() {
			Controls = new DialogControlCollection();
		}
		
		internal Dialog(DlgTemplate template) : this() {
			
			Size        = new Size(template.cx, template.cy);
			Location    = new Point(template.x, template.y);
			Text        = template.exTitle;
			WindowClass = template.exWindowClass;
		}
		
		internal Dialog(DlgTemplateEx template) : this() {
			
			Size        = new Size(template.cx, template.cy);
			Location    = new Point(template.x, template.y);
			Text        = template.title;
			WindowClass = template.windowClass;
		}
		
		public DialogControlCollection Controls { get; private set; }
		
		
		public DialogMenu Menu        { get; set; }
		
	}
	
	public class DialogControlCollection : Collection<DialogControl> {
	}
	
	public class DialogControl : DialogBase {
		
		internal DialogControl(DlgItemTemplate template) {
			Size        = new Size(template.cx, template.cy);
			Location    = new Point(template.x, template.y);
			Text        = (template.exTitle == null ? "" : template.exTitle.ToString());
			WindowClass = template.exWindowClass;
		}
		
		internal DialogControl(DlgItemTemplateEx template) {
			Size        = new Size(template.cx, template.cy);
			Location    = new Point(template.x, template.y);
			Text        = (template.title == null ? "" : template.title.ToString());
			WindowClass = template.windowClass;
			
		}
		
		
		
/*		private void SetClass(UInt16 windowClass) {
			
			KnownWindowClass winClass = (KnownWindowClass)windowClass;
			switch(winClass) {
				case KnownWindowClass.Button:
					Class = ControlClass.
					
				case KnownWindowClass.Edit:
					return new TextBox() { Multiline = true };
					
				case KnownWindowClass.Static:
					return new Label();
					
				case KnownWindowClass.ListBox:
					return new ListBox() { IntegralHeight = false };
					
				case KnownWindowClass.ScrollBar:
					//return new ScrollBar();
					return new Button();
					
				case KnownWindowClass.ComboBox:
					return new ComboBox() { IntegralHeight = false };
			}
			
		}
		
		public ControlClass Class { get; set; }  */
		
		public override string ToString() {
			
			if(WindowClass == null) return "Null - " + Text;
			
			return WindowClass.ToString() + " - " + Text;
		}
		
	}
	
/*	public enum ControlClass {
		Label,
		Button,
		PictureBox,
		
	}
	
	public enum KnownWindowClass {
		Button    = 0x0080,
		Edit      = 0x0081,
		Static    = 0x0082,
		ListBox   = 0x0083,
		ScrollBar = 0x0084,
		ComboBox  = 0x0085
	} */
	
	
	public class DialogMenu {
		
		public DialogMenu(DialogMenuItem root) {
			Root = root;
		}
		
		public DialogMenuItem Root { get; set; }
		
	}
	
	public class DialogMenuItemCollection : Collection<DialogMenuItem> {
	}
	
	public class DialogMenuItem {
		
		public DialogMenuItem(String text) {
			
			Text = text;
			
			Children = new DialogMenuItemCollection();
		}
		
		public String Text { get; set; }
		
		public DialogMenuItemCollection Children { get; private set; }
		
		// Type
		public Boolean Bitmap       { get; set; }
		public MenuBreakStyle Break { get; set; }
		public Boolean OwnerDraw    { get; set; }
		public Boolean IsSeparator  { get; set; }
		public Boolean RightOrder   { get; set; }
		public Boolean RightJustify { get; set; }
		public Boolean RadioCheck   { get; set; }
		
		// State
		
		public Boolean IsChecked     { get; set; }
		public Boolean IsDefault     { get; set; }
		public Boolean IsDisabled    { get; set; }
		public Boolean IsHighlighted { get; set; }
		
	}
	
	public enum MenuBreakStyle {
		None,
		Break,
		BarBreak
	}
	
}
