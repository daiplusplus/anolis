using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32.SafeHandles;
using Anolis.Core.Native;
using System.IO;
using System.ComponentModel;

namespace Anolis.Core.Utility {
	
/*	internal static class NativeMethods {
		[Flags]
		public enum FileAccess : uint {
			DeviceAttributes = 0,
			GenericRead = 0x80000000,
			GenericWrite = 0x40000000
		}

		public enum CreationDisposition {
			CreateNew = 1,
			CreateAlways = 2,
			OpenExisting = 3,
			OpenAlways = 4,
			TruncateExisting = 5
		}

		[Flags]
		public enum FileShareMode {
			Read = 1,
			Write = 2,
			Delete = 4

		}

		[Flags]
		public enum FileFlagsAndAttributes : uint {
			ReadOnly = 0x1,
			Hidden = 0x2,
			System = 0x4,
			Archive = 0x20,
			Normal = 0x80,
			FlagWriteThrough = ,
			FlagRandomAccess = 
		}

		public enum PageProtect {
			ReadOnly = 0x2,
			ReadWrite = 0x4,
		}

		[Flags]
		public enum MapAccess {
			Write = 0x2,
			Read = 0x4,
			AllAccess = Write | Read
		}

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern SafeFileHandle CreateFile(
		   string lpFileName,
		   FileAccess dwDesiredAccess,
		   FileShareMode dwShareMode,
		   IntPtr lpSecurityAttributes,
		   CreationDisposition dwCreationDisposition,
		   FileFlagsAndAttributes dwFlagsAndAttributes,
		   IntPtr hTemplateFile);

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern SafeFileMappingHandle CreateFileMapping(SafeFileHandle hFile, IntPtr lpFileMappingAttributes, PageProtect flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr MapViewOfFile(SafeFileMappingHandle hFileMappingObject, MapAccess dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, IntPtr dwNumberOfBytesToMap);

		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

		[DllImport("kernel32.dll"), ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseHandle(IntPtr hObject);
	} */
	
	public enum FileMapMode {
		Read  = 1,
		Write = 2,
		ReadWrite = Read | Write
	}
	
	/// <summary>Represents a memory-mapped file.</summary>
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	public sealed class FileMapping : IDisposable {
		
		private const FileAccess GenericRead  = unchecked( (FileAccess)0x80000000 );
		private const FileAccess GenericWrite = (FileAccess)0x40000000;
		
		private const UInt32 FileFlagWriteThrough = 0x80000000;
		private const UInt32 FileFlagRandomAccess = 0x10000000;
		
		private readonly SafeFileHandle        _file;
		private readonly SafeFileMappingHandle _mapping;
		private readonly IntPtr                _view;
		private readonly Int64                 _size;
		private          Boolean               _disposed;
		
		/// <summary>Initializes a new instance of the <see cref="FileMapping"/> class.</summary>
		/// <param name="fileName">The file that should be memory-mapped</param>
		public FileMapping(String fileName, FileMapMode mode) {
			
			if(fileName == null)
				throw new ArgumentNullException("fileName");
			
			FileMode          fileMode = FileMode.Open;
			FileAccess        fileAccess;
			FileShare         fileShare;
			FileMapProtection mapProtection;
			FileMapAccess     mapAccess;
			
			switch(mode) {
				case FileMapMode.Read:
					fileAccess    = FileAccess.Read | GenericRead;
					fileShare     = FileShare.Read;
					mapProtection = FileMapProtection.PageReadOnly;
					mapAccess     = FileMapAccess.FileMapRead;
					break;
					
				case FileMapMode.Write:
					fileAccess    = FileAccess.Write | GenericWrite;
					fileShare     = FileShare.None;
					mapProtection = FileMapProtection.PageWriteCopy;
					mapAccess     = FileMapAccess.FileMapWrite;
					break;
					
				case FileMapMode.ReadWrite:
				default:
					fileAccess    = FileAccess.ReadWrite | GenericRead | GenericWrite;
					fileShare     = FileShare.None;
					mapProtection = FileMapProtection.PageReadWrite;
					mapAccess     = FileMapAccess.FileMapAllAccess;
					break;
			}
			
			
			// Get the size.
			// This throws a proper FileNotFoundException if the file doesn't exist so we don't need to check that anymore.
			_size = new FileInfo(fileName).Length;
			
			try {
				_file = NativeMethods.CreateFile(fileName, fileAccess, fileShare, IntPtr.Zero, fileMode, FileAttributes.Normal, IntPtr.Zero );
				if(_file.IsInvalid)
					throw new Win32Exception();
				
				_mapping = NativeMethods.CreateFileMapping(_file, IntPtr.Zero, mapProtection, 0, 0, null);
				if(_mapping.IsInvalid)
					throw new Win32Exception();
				
				_view = NativeMethods.MapViewOfFile(_mapping, mapAccess, 0, 0, IntPtr.Zero);
				if(_view == IntPtr.Zero)
					throw new Win32Exception();
				
			} catch {
				
				if(_view != IntPtr.Zero)
					NativeMethods.UnmapViewOfFile(_view);
				if(_mapping != null)
					_mapping.Dispose();
				if(_file != null)
					_file.Dispose();
				throw;
			}
		}
		
		/// <summary>Cleans up resources on finalization.</summary>
		~FileMapping() {
			Dispose(false);
		}
		
		/// <summary>Gets a memory-mapped view to the file's contents.</summary>
		/// <value>A pointer to the first byte of the memory-mapped file.</value>
		public IntPtr View {
			get {
				EnsureNotDisposed();
				return _view;
			}
		}
		
		/// <summary>Gets the size of the memory-mapped file.</summary>
		public Int64 Size {
			get {
				EnsureNotDisposed();
				return _size;
			}
		}
		
		public SafeFileHandle FileHandle {
			get {
				EnsureNotDisposed();
				return _file;
			}
		}
		
		/// <summary>Cleans up resources used by the <see cref="FileMapping"/> class.</summary>
		/// <param name="disposing"><see langword="true"/> to clean up both managed and unmanaged resources; <see langword="false" /> to clean up only unmanaged resources.</param>
		private void Dispose(Boolean disposing) {
			
			if(!_disposed) {
				
				if(_view != IntPtr.Zero)
					NativeMethods.UnmapViewOfFile(_view);
				
				if(disposing) {
					if(_mapping != null)
						_mapping.Dispose();
					if(_file != null)
						_file.Dispose();
				}
				
				_disposed = true;
			}
		}
		
		private void EnsureNotDisposed() {
			if(_disposed)
				throw new ObjectDisposedException(typeof(FileMapping).FullName);
		}
		
		/// <summary>Cleans up resources used by the <see cref="FileMapping"/> class.</summary>
		public void Dispose() {
			
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		
	}
}
