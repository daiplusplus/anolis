using System;
using Anolis.Core.Source;
using Anolis.Core.Native;

namespace Anolis.Core.Utility {
	
	public static class Environment {
		
		private static Boolean? _isWow64;
		private static Int32?   _spLevel;
		
		/// <summary>Returns true if this process is running as x86 on an x64 system (i.e. it's running under Wow64).</summary>
		public static Boolean IsWow64 {
			get {
				
				if( _isWow64 == null ) {
					
					if( CanCallIsWow64Process() ) {
						
						Boolean retval;
						
						NativeMethods.IsWow64Process( System.Diagnostics.Process.GetCurrentProcess().Handle, out retval );
						
						_isWow64 = retval;
						
					} else if( CanCallGetNativeSystemInfo() ) {
						
						// the longer way
						
						SystemInfo si, siN;
						
						NativeMethods.GetNativeSystemInfo(out siN);
						NativeMethods.GetSystemInfo      (out si);
						
						_isWow64 = si.wProcessorArchitecture != siN.wProcessorArchitecture;
						
					}
					
					_isWow64 = false;
					
				}
				
				return _isWow64.Value;
				
			}
		}
		
		private static Boolean CanCallIsWow64Process() {
			
			// The IsWow64 function exists on: (NT5.1 SP2+), (NT5.2, SP1+), (NT6.0, RTM+)
			
			OperatingSystem os = System.Environment.OSVersion;
			
			if( os.Platform == PlatformID.Win32NT ) {
				
				if(os.Version.Major == 5) {
					
					if(os.Version.Minor == 1) return ServicePack >= 2;
					
					if(os.Version.Minor == 2) return ServicePack >= 1;
					
				}
				
				return os.Version.Major >= 6;
				
			}
			
			return false;
			
		}
		
		
		public static Int32 ServicePack {
			get {
				
				if(_spLevel == null) {
					
					OperatingSystem os = System.Environment.OSVersion;
					
					if(os.ServicePack.Length == 0) return (_spLevel = 0).Value;
					
					String numberPart = os.ServicePack.Substring(13);
					
					Int32 sp;
					if(Int32.TryParse(numberPart, out sp)) return (_spLevel = sp).Value;
					
					// build a number string
					String n = String.Empty;
					for(int i=0;i<os.ServicePack.Length;i++) if( Char.IsDigit( os.ServicePack[i] ) ) n += os.ServicePack[i];
					if(Int32.TryParse(numberPart, out sp)) return (_spLevel = sp).Value;
					
					return -1; // don't throw exceptions from property accessors which should have no side-effects
					
				}
				
				return _spLevel.Value;
			}
		}
		
		private static Boolean CanCallGetNativeSystemInfo() {
			
			// NT5.1 RTM or later; no word on how to detect Windows 2000 Limited Edition's WOW64, but I think it's safe to presume this software will never be run
			
			// an alternative is to call GetProcAddress...
			
			OperatingSystem os = System.Environment.OSVersion;
			
			if( os.Platform != PlatformID.Win32NT ) return false;
			
			if( os.Version.Major == 5 ) return os.Version.Minor >= 1;
			return os.Version.Major >= 6;
			
		}
		
	}
}
