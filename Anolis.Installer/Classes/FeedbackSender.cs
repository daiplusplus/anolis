using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

using Anolis.Packages;
using Anolis.Packages.Utility;

using Env = Anolis.Core.Utility.Environment;

namespace Anolis.Installer {
	
	public static class FeedbackSender {
		
		public static Boolean SendFeedback(Uri uri, String message, Log log, PackageExecutionSettingsInfo executionInfo) {	
			
			String data = GatherDictionary(message, log, executionInfo );
			
			return SendString( data, uri );
		}
		
		private static String CTS(CultureInfo cult) {
			
			return cult.ThreeLetterISOLanguageName + " - " + cult.LCID.ToString();
		}
		
		private static Boolean SendString(String body, Uri uri) {
			
			Byte[] bytes = Encoding.UTF8.GetBytes( body );
			
//			try {
				
				WebRequest request = HttpWebRequest.Create( uri );
				request.ContentType = "application/x-www-form-urlencoded";
				request.Method = "POST";
				request.ContentLength = bytes.Length;
				
				Stream bodyStream = request.GetRequestStream();
				bodyStream.Write( bytes, 0, bytes.Length );
				bodyStream.Close();
				
				WebResponse response = request.GetResponse();
				if( response == null ) return false;
				
				using(StreamReader rdr = new StreamReader( response.GetResponseStream() )) {
					
					String responseText = rdr.ReadToEnd().Trim();
					
					if( responseText == "OK" ) return true;
				}
			
// Don't catch exception, so BackgroundWorker will catch it
//			} catch(WebException) {
//				return false;
//			}
			
			return false;
		}
		
		private static String GatherDictionary(String message, Log log, PackageExecutionSettingsInfo executionInfo) {
			
			// Gather information from a variety of sources
			Dictionary<String,String> dict = new Dictionary<String,String>();
			dict.Add("Environment.OSVersion.Version"      , Environment.OSVersion.Version.ToString() );
			dict.Add("Environment.OSVersion.Platform"     , Environment.OSVersion.Platform.ToString() );
			dict.Add("Environment.OSVersion.ServicePack"  , Environment.OSVersion.ServicePack);
			dict.Add("Environment.OSVersion.VersionString", Environment.OSVersion.VersionString);
			
			dict.Add("Environment.CurrentDirectory", Environment.CurrentDirectory );
			dict.Add("Environment.ProcessorCount"  , Environment.ProcessorCount.ToString() );
			
			dict.Add("Env.IsWow64"    , Env.IsWow64 ? "true" : "false" );
			dict.Add("Env.IsX64"      , Env.IsX64   ? "true" : "false" );
			dict.Add("Env.ServicePack", Env.ServicePack.ToString() );
			dict.Add("Env.Location"   , Env.Location );
			
			dict.Add("CultureInfo.InstalledUICulture", CTS( CultureInfo.InstalledUICulture ) );
			dict.Add("CultureInfo.CurrentCulture"    , CTS( CultureInfo.CurrentCulture ) );
			dict.Add("CultureInfo.CurrentUICulture"  , CTS( CultureInfo.CurrentUICulture ) );
			
			//
			
			dict.Add("PackageInfo.Source"         , PackageInfo.Source.ToString() );
			dict.Add("PackageInfo.SystemRestore"  , PackageInfo.SystemRestore   ? "true" : "false" );
			dict.Add("PackageInfo.LiteMode"       , PackageInfo.LiteMode        ? "true" : "false" );
			dict.Add("PackageInfo.IgnoreCondition", PackageInfo.IgnoreCondition ? "true" : "false" );
			dict.Add("PackageInfo.I386Install"    , PackageInfo.I386Install     ? "true" : "false" );
			dict.Add("PackageInfo.I386Directory"  , PackageInfo.I386Directory == null ? "" : PackageInfo.I386Directory.FullName ); // is this too much info?
			
			if( PackageInfo.Package == null ) {
				
				dict.Add("PackageInfo.Package"        , "null");
				
			} else {
				
				dict.Add("PackageInfo.Package.Name"   , PackageInfo.Package.Name);
				dict.Add("PackageInfo.Package.Version", PackageInfo.Package.Version.ToString() );
			}
			
			//
			
			if( executionInfo == null ) {
				
				dict.Add("ExecutionInfo", "null");
				
			} else {
				
				dict.Add("ExecutionInfo.MakeBackup"     , executionInfo.MakeBackup              ? "true" : "false" );
				dict.Add("ExecutionInfo.BackupDirectory", executionInfo.BackupDirectory == null ? "" : executionInfo.BackupDirectory.FullName );
				dict.Add("ExecutionInfo.ApplyToDefault" , executionInfo.ApplyToDefault          ? "true" : "false" );
				dict.Add("ExecutionInfo.CDImage"        , executionInfo.CDImage == null         ? "null" : executionInfo.CDImage.RootDirectory.FullName );
				dict.Add("ExecutionInfo.CreateSystemRestorePoint", executionInfo.CreateSystemRestorePoint ? "true" : "false" );
				dict.Add("ExecutionInfo.ExecutionMode"  , executionInfo.ExecutionMode.ToString() );
				dict.Add("ExecutionInfo.LiteMode"       , executionInfo.LiteMode                ? "true" : "false" );
				dict.Add("ExecutionInfo.RequiresRestart", executionInfo.RequiresRestart         ? "true" : "false" );
			}
			
			//
			
			dict.Add("InstallationInfo.FailedCondition"    , InstallationInfo.FailedCondition          ? "true" : "false" );
			dict.Add("InstallationInfo.InstallationAborted", InstallationInfo.InstallationAborted      ? "true" : "false" );
			dict.Add("InstallationInfo.ToolsDestination"   , InstallationInfo.ToolsDestination == null ? "null" : InstallationInfo.ToolsDestination.FullName );
			dict.Add("InstallationInfo.UseSelector"        , InstallationInfo.UseSelector      == null ? "null" : InstallationInfo.UseSelector.Value ? "true" : "false" );
			dict.Add("InstallationInfo.WizStyle"           , InstallationInfo.WizStyle.ToString() );
			dict.Add("InstallationInfo.ProgramMode"        , InstallationInfo.ProgramMode.ToString() );
			
			//
			
			dict.Add("InstallationResources.IsCustomized", InstallerResources.IsCustomized ? "true" : "false" );
			if( InstallerResources.IsCustomized ) {
				
				dict.Add("InstallationResources.CustomizedSettings.InstallerName"     , InstallerResources.CustomizedSettings.InstallerName );
				dict.Add("InstallationResources.CustomizedSettings.InstallerFullName" , InstallerResources.CustomizedSettings.InstallerFullName );
				dict.Add("InstallationResources.CustomizedSettings.InstallerDeveloper", InstallerResources.CustomizedSettings.InstallerDeveloper );
				dict.Add("InstallationResources.CustomizedSettings.InstallerWebsite"  , InstallerResources.CustomizedSettings.InstallerWebsite );
			}
			
			dict.Add("InstallerResources.CurrentLanguage.LanguageName", InstallerResources.CurrentLanguage.LanguageName );
			dict.Add("InstallerResources.CurrentLanguage.LcidName"    , InstallerResources.CurrentLanguage.LcidName );
			dict.Add("DateTime.Now"       , DateTime.Now.ToString("o") ); // o == roundtrip, includes timezone
			
			// sort by keyname
			List<String> allKeys = new List<String>();
			allKeys.AddRange( dict.Keys );
			allKeys.Sort();
			
			StringBuilder sb = new StringBuilder();
			
			foreach(String key in allKeys) {
				
				String value = dict[key];
				
				sb.Append( key );
				sb.Append(" : ");
				sb.AppendLine( dict[key] );
			}
			
			sb.AppendLine("Message:");
			sb.AppendLine( message );
			
			sb.AppendLine();
			
			if( log == null ) {
				
				sb.AppendLine("Log Null");
				
			} else {
			
				sb.AppendLine("Notable Messages:");
				foreach(LogItem item in log) {
					if( item.Severity != LogSeverity.Info ) item.Write( sb );
				}
				
			}
			
			return sb.ToString();
		}
		
		public static void SendErrorReport(Uri uri) {
			
			String path = InstallationInfo.FeedbackErrorReportPath;
			
			if( path == null ) return;
			if( !File.Exists( path ) ) return;
			
			String body = File.ReadAllText(path);
			
			body = "Fatal Exception Report:\r\n" + body;
			
			SendString(body, uri);
		}
		
	}
}
