using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using Size  = System.Drawing.Size;
using Point = System.Drawing.Point;

namespace Anolis.Resourcer.Controls {
	
	public class AnimationControl : Control {
		
		private String _filename;
		
		private const Int32 _iccAnimateClass = 0x80;
		
		public AnimationControl() {
			
			TabStop = false;
			
			SetStyle(ControlStyles.Selectable | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, false);
			SetStyle(ControlStyles.ResizeRedraw                                                                     , true);
		}
		
		protected override void Dispose(Boolean disposing) {
			
			Close();
			
			base.Dispose(disposing);
			
		}
		
#region Relevant Properties
		
		private Boolean _centerVideo;
		
		public Boolean CenterVideo {
			get { return _centerVideo; }
			set {
				
				_centerVideo = value;
				
				RecreateHandle();
				
				// apparently you need to force a resize to center the animation
				if(value) {
					
					Size = new Size( Size.Width - 1, Size.Height - 1 );
					Size = new Size( Size.Width + 1, Size.Height + 1 );
				}
				
			}
		}
		
		private Boolean _transparentVideo;
		
		public Boolean TransparentVideo {
			get { return _transparentVideo; }
			set {
				_transparentVideo = value;
				RecreateHandle();
			}
		}
		
		private Boolean _multithreadedVideo = true;
		
		public Boolean MultithreadedVideo {
			get { return _multithreadedVideo; }
			set {
				_multithreadedVideo = value;
				RecreateHandle();
			}
		}
		
#endregion
		
		protected override CreateParams CreateParams {
			get {
				
				CreateParams paras = base.CreateParams;
				paras.ClassName = "SysAnimate32";
				
				if(CenterVideo)         paras.Style |= (int)AnimationControlStyle.Center;
				if(TransparentVideo)    paras.Style |= (int)AnimationControlStyle.Transparent;
				if(!MultithreadedVideo) paras.Style |= (int)AnimationControlStyle.UseTimer;
				
				return paras;
			}
		}
		
		protected override void CreateHandle() {
			
			if( !RecreatingHandle ) {
				
				NativeMethods.InitCommonControlsExInfo arg = new NativeMethods.InitCommonControlsExInfo( _iccAnimateClass );
				NativeMethods.InitCommonControlsEx( arg );
				
			}
			
			base.CreateHandle();
		}
		
		protected override void OnHandleCreated(EventArgs e) {
			
			// if the handle is recreated whilst a file is playing it won't be re-opened
			
			Boolean reopen = IsOpen;
			Boolean replay = IsPlaying;
			
			IsOpen    = false;
			IsPlaying = false;
			
			base.OnHandleCreated(e);
			
			if( reopen ) Open( _filename );
			
			if( replay ) Play();
			
		}
		
		private IntPtr SendMessage(AnimationControlMessage message, UInt32 a, Int32 b) {
			
			UIntPtr ai = new UIntPtr(a);
			IntPtr  bi = new IntPtr(b);
			
			return NativeMethods.SendMessage( Handle, (uint)message, ai, bi );
			
		}
		
		private IntPtr SendMessage(AnimationControlMessage message, UInt32 a, String b) {
			
			UIntPtr ai = new UIntPtr(a);
			
			return NativeMethods.SendMessage( Handle, (uint)message, ai, b );
			
		}
		
		private const           Int32  WM_NCHITTEST = 0x84;
		private static readonly IntPtr HT_CLIENT    = new IntPtr( 0x01 );
		private static readonly IntPtr HT_BORDER    = new IntPtr( 0x12 );
		
		protected override void WndProc(ref Message m) {
			
			if( m.Msg == WM_NCHITTEST ) {
				
				Int32 l = m.LParam.ToInt32();
				
				Point oldP = new Point( l & 0xFFFF, l >> 0x10 );
				
				Point newP = PointToClient( oldP );
				
				m.Result = ClientRectangle.Contains( newP ) ? HT_CLIENT : HT_BORDER;
				
			} else {
				
				base.WndProc(ref m);
				
			}
			
		}
		
#region Transport Control and Media Methods
		
		public Boolean IsOpen {
			get;
			private set;
		}
		
		private Boolean _isPlaying;
		
		public Boolean IsPlaying {
			get {
				
				if( NativeMethods.IsVistaOrLater ) {
					
					IntPtr result = SendMessage(AnimationControlMessage.IsPlaying, 0, 0);
					Int32 res = result.ToInt32();
					return res != 0;
					
				} else return _isPlaying;
				
			}
			set {
				_isPlaying = value;
			}
		}
		
		public void Open(String filename) {
			
			if( !IsHandleCreated || DesignMode ) return;
			
			Close();
			
			IntPtr result = SendMessage( OpenMessage, 0, filename );
			Int32 res = result.ToInt32();
			
			_filename = filename;
			
			IsOpen = res != 0;
			
		}
		
		///////////////////////////////////
		
		public void Play() {
			
			Play( -1, 0, -1 );
			
		}
		
		/// <param name="repetitions">-1 to replay indefinitely</param>
		/// <param name="fromFrame">0 to start from the first frame</param>
		/// <param name="toFrame">-1 to end with the last frame in the video</param>
		public void Play(Int32 repetitions, Int16 fromFrame, Int16 toFrame) {
			
			if( !IsHandleCreated || !IsOpen ) return;
			
			UInt32 a = (uint)repetitions;
			Int32 b = NativeMethods.MakeInt32(fromFrame, toFrame);
			
			SendMessage(AnimationControlMessage.Play, a, b );
			
		}
		
		public void Stop() {
			
			if( !IsHandleCreated || !IsOpen ) return;
			
			SendMessage(AnimationControlMessage.Stop, 0, 0 );
			
			IsPlaying = false;
			
		}
		
		/// <summary>Stops and closes the current animation</summary>
		public void Close() {
			
			if( !IsHandleCreated || !IsOpen ) return;
			
			Stop();
			
			IsOpen = false;
			
			SendMessage( OpenMessage, 0, 0 );
			
			Invalidate();
		}
		
		public void Seek(Int16 frame) {
			
			Play(1, frame, frame);
		}
		
#endregion
		
		[Flags]
		private enum AnimationControlStyle : ushort {
			None        = 0,
			Center      = 1,
			Transparent = 2,
			Autoplay    = 4,
			UseTimer    = 8
		}
		
		private const Int16 WM_USER = 0x0400;
		private static readonly Boolean _isWide = Marshal.SystemDefaultCharSize != 1;
		
		private static AnimationControlMessage OpenMessage {
			get { return _isWide ? AnimationControlMessage.OpenW : AnimationControlMessage.OpenA; }
		}
		
		private enum AnimationControlMessage : ushort {
			OpenA     = WM_USER + 100,
			OpenW     = WM_USER + 103,
			Play      = WM_USER + 101,
			Stop      = WM_USER + 102,
//			/// <remarks>Requires Windows Vista or later</remarks>
			IsPlaying = WM_USER + 104
		}
		
	}
	
	
	
	
	
}
