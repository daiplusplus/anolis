using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

using W3b.TarLzma;

using Anolis.Tools.UxPatcher.Properties;
using Anolis.Core.Packages;
using Env = Anolis.Core.Utility.Environment;

namespace Anolis.Tools.UxPatcher {
	
	/// <summary>This class is an ugly mixture of general stuff and special-case programming. Gah.</summary>
	public static class Patcher {
		
		/// <summary>Entrypoint</summary>
		public static void Patch(InstallationInfo info) {
			
			Status = "Extracting";
			
			ExtractFiles( info.ExtractDirectory );
			
			if(info.SystemRestore) {
				
				Status = "Creating Restore Point";
				
				PackageUtility.CreateSystemRestorePoint("UxTheme Patch", PackageUtility.SystemRestoreType.ApplicationInstall, PackageUtility.SystemRestoreEventType.BeginSystemChange);
				
			}
			
			//////////////////////////////
			
			Status = "Patching";
			
			PackageUtility.AllowProtectedRenames();
			
			if( info.Install == Install.NT522X64English ) {
				PatchX64(info);
			} else {
				PatchX86(info);
			}
			
			Directory.Delete( info.ExtractDirectory, true );
			
			//////////////////////////////
			
			if(info.SystemRestore) {
				
				Status = "Finishing Restore Point";
				
				PackageUtility.CreateSystemRestorePoint("UxTheme Patch", PackageUtility.SystemRestoreType.ApplicationInstall, PackageUtility.SystemRestoreEventType.EndSystemChange);
				
			}
			
			Status = "Restart Your Computer";
			
		}
		
		private static void PatchX86(InstallationInfo info) {
			
			String srcFilename = Path.Combine( info.ExtractDirectory, GetUxFilename(info.Install) );
			
			String dstFilename = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.System), GetUxFilename(info.Install) );
			
			if( File.Exists( dstFilename ) ) File.Delete( dstFilename );
			
			File.Move( srcFilename, dstFilename );
			
			String finalName = Path.Combine(  Environment.GetFolderPath(Environment.SpecialFolder.System), "UxTheme.dll" );
			
			PackageUtility.AddPfroEntry( dstFilename, finalName );
		}
		
		private static void PatchX64(InstallationInfo info) {
			
			///////////////////////////////////////
			// Patch the x64 version
			
			PatchX86(info);
			
			///////////////////////////////////////
			// Patch the SysWow64 (x86) version
			
			// I can't tell an easy way to get the exact SysWow64 path... so just use an ugly hardhack
			
			String srcFilename = Path.Combine( info.ExtractDirectory, GetUxFilename(Install.NT522English) );
			
			String sysWow64Path = Environment.GetFolderPath(Environment.SpecialFolder.System);
			sysWow64Path = Directory.GetParent(sysWow64Path).FullName;
			sysWow64Path = Path.Combine( sysWow64Path, "SysWow64");
			
			String dstFilename = Path.Combine( sysWow64Path, GetUxFilename(info.Install) );
			
			File.Move( srcFilename, dstFilename );
			
			String finalName = Path.Combine(  Environment.GetFolderPath(Environment.SpecialFolder.System), "UxTheme.dll" );
			
			PackageUtility.AddPfroEntry( dstFilename, finalName );
			
		}
		
		private static String GetUxFilename(Install install) {
			switch(install) {
				case Install.NT513English:
					return "nt51_x86_en.dll";
				case Install.NT513German:
					return "nt51_x86_de.dll";
				case Install.NT513Spanish:
					return "nt51_x86_es.dll";
				case Install.NT522English:
					return "nt52_x86_en.dll";
				case Install.NT522German:
					return "nt52_x86_de.dll";
				case Install.NT522X64English:
					return "nt52_x64_en.dll";
				default:
					return null;
			}
		}
		
		private static void OnPatchEvent() {
			if( PatchEvent != null ) PatchEvent(null, EventArgs.Empty);
		}
		
		private static String _status;
		public static String Status {
			get {
				return _status;
			}
			set {
				_status = value;
				OnPatchEvent();
			}
		}
		
		public static event EventHandler PatchEvent;
		
		// Ensure this is being ran on a supported system and with admin privs
		// Extract the files to a temporary directory
		// Determine what files are needed to patch
		// patch 'em
		
		private static void ExtractFiles(String destinationDirectory) {
			
			// The TarLZMA is within the Resources block in this program
			
			DirectoryInfo dest = new DirectoryInfo( destinationDirectory );
			if( !dest.Exists ) dest.Create();
			
			using( MemoryStream ms = new MemoryStream( Resources.Files_tar ) ) {
				
				TarLzmaDecoder decoder = new TarLzmaDecoder( ms );
				decoder.Extract( destinationDirectory );
				// for the limited number of files this is fairly quick, no need for progress events
				
			}
			
		}
		
	}
	
	public static class PatchUtility {
		
		public static void Restart() {
			PackageUtility.InitRestart();
		}
		
		// TODO: A "can run" program that ensures it's NT5.1/5.2 and not under Wow64
		
		public static String GetSystemInfo() {
			
			StringBuilder sb = new StringBuilder();
			sb.Append("Windows ");
			if( Env.OSVersion.Version.Major == 5 ) {
				
				switch(Env.OSVersion.Version.Minor) {
					case 1:
						sb.Append("XP "); break;
					case 2:
						sb.Append("Server 2003 "); break;
					case 0:
					default:
						sb.Append("Unsupported"); break;
				}
				
				switch(Env.ServicePack) {
					case -1:
						sb.Append("Unknown SP "); break;
					case 0:
						sb.Append("RTM "); break;
					default:
						sb.Append("SP" + Env.ServicePack + " "); break;
				}
				
				if( Env.IsX64 ) {
					sb.Append("x64 - ");
				} else {
					sb.Append("x86 - ");
				}
				
				// and now the language
				
				// a hack-ish way is to just use the Culture API in the BCL
				CultureInfo cult = CultureInfo.GetCultureInfo( NativeMethods.GetSystemDefaultUILanguage() );
				sb.Append( cult.Parent.EnglishName );
				
			} else {
				
				sb.Append("Unsupported");
			}
			
			return sb.ToString();
		}
		
		public static InstallationInfo GetInstallationInfo() {
			
			if( Env.IsX64 ) { // implies NT52
				
				Boolean isEnglish = (GetLanguage() == Language.English);
				
				return new InstallationInfo( Install.NT522X64English, !isEnglish ); // use English under all circumstances
				
			}
			
			if( Env.OSVersion.Version.Major == 5 ) {
				
				if( Env.OSVersion.Version.Minor == 1 && Env.ServicePack == 3 ) {
					// it's Windows XP SP3 (implies x86)
					
					// determine language
					Language lang = GetLanguage();
					switch(lang) {
						case Language.English:
							return new InstallationInfo( Install.NT513English, false );
						case Language.German:
							return new InstallationInfo( Install.NT513German , false );
						case Language.Spanish:
							return new InstallationInfo( Install.NT513Spanish, false );
						default:
							return new InstallationInfo( Install.NT513English, true );
					}
					
				}
				
				if( Env.OSVersion.Version.Minor == 2 && Env.ServicePack == 2 ) {
					// it's Windows Server 2003 (implies x86 because x64 is filtered earlier)
					
					// determine language
					Language lang = GetLanguage();
					switch(lang) {
						case Language.English:
							return new InstallationInfo( Install.NT522English, false );
						case Language.German:
							return new InstallationInfo( Install.NT522German , false );
						default:
							return new InstallationInfo( Install.NT522English, true );
					}
					
				}
				
			}
			
			return new InstallationInfo( Install.NotSupported, false );
		}
		
		private static Language GetLanguage() {
			
			UInt16 lang = NativeMethods.GetSystemDefaultUILanguage();
			// the primary lang id is the lower 10 bits
			
			UInt16 priLang = GetPriLang(lang);
			
			if( ((ushort)priLang & (ushort)Language.English) == (ushort)Language.English ) return Language.English;
			if( ((ushort)priLang & (ushort)Language.Spanish) == (ushort)Language.Spanish ) return Language.Spanish;
			if( ((ushort)priLang & (ushort)Language.German)  == (ushort)Language.German  ) return Language.German;
			
			return Language.Other;
		}
		
		private static UInt16 GetPriLang(UInt16 lang) {
			// based on the PRIMARYLANGID macro
			return (ushort)( (ushort)lang & (ushort)0x3FF ); // gah, casting overload
		}
		
	}
	
	internal class NativeMethods {
		
		[DllImport("kernel32.dll", CharSet=CharSet.Unicode, BestFitMapping=false, ThrowOnUnmappableChar=true, SetLastError=true)]
		public static extern UInt16 GetSystemDefaultUILanguage();
		
	}
	
	public struct InstallationInfo {
		
		public Install Install;
		public Boolean DifferentLanguage;
		public String  ExtractDirectory;
		public Boolean SystemRestore;
		
		public InstallationInfo(Install install, Boolean differentLanguage) {
			Install           = install;
			DifferentLanguage = differentLanguage;
			ExtractDirectory  = Path.Combine( Path.GetTempPath(), Path.GetRandomFileName() );
			SystemRestore     = true;
		}
		
		public override String ToString() {
			
			switch(Install) {
				case Install.NT513English:
					return "Windows XP SP3 x86 - English";
				case Install.NT513German:
					return "Windows XP SP3 x86 - German";
				case Install.NT513Spanish:
					return "Windows XP SP3 x86 - Spanish";
				case Install.NT522English:
					return "Windows Server 2003 SP2 x86 - English";
				case Install.NT522German:
					return "Windows Server 2003 SP2 x86 - Spanish";
				case Install.NT522X64English:
					return "Windows Server 2003 SP2 x64 - English";
				case Install.NotSupported:
				default:
					return "Not Supported";
			}
			
		}
		
	}
	
	public enum Install {
		NotSupported    = 0,
		NT513English    = 1,
		NT513German     = 2,
		NT513Spanish    = 3,
		NT522English    = 4,
		NT522German     = 5,
		NT522X64English = 6
	}
	
	public enum Language : ushort {
		English   = 0x09,
		German    = 0x07,
		Spanish   = 0x0A,
		Other     = 0x00
	}
	
}
