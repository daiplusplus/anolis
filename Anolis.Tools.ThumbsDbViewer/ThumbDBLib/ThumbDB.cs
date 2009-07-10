using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Text;

namespace ThumbDBLib {
	
	public struct CatalogHeader {
		public short num1;
		public short num2;
		public int thumbCount;
		public int thumbWidth;
		public int thumbHeight;
	}

	public struct CatalogItem {
		public int num1;
		public int itemID;
		public short num3;
		public short num4;
		public short num5;
		public short num6;
		public string filename;
		public short num7;
	}
	
	public class ThumbDB {
		private ArrayList _catalogItems = new ArrayList();
		private String _thumbDBFile;
		
		public ThumbDB(String thumbDBFile) {
			_thumbDBFile = thumbDBFile;
			LoadCatalog();
		}
		
		/// <summary>Returns a string array of the list of thumbnail files. You can match these with the actual filenames in that folder. So, if you have a filename called "MyPictures.jpg", that's exactly what you'll have in array returned by GetThumbFiles().</summary>
		public string[] GetThumbFileNames() {
			
			String[] files = new String[_catalogItems.Count];
			int index = 0;
			
			foreach(CatalogItem item in _catalogItems) {
				files[index] = item.filename;
				index++;
			}
			return files;
		}
		
		/// <summary>This method returns the raw contents of the internal file, with the first 12 bytes stripped off. The first 12 bytes are 3 ints. The first, I don't know what it is. The second is the itemID. The third is the size of the image data. None of this is needed to create the image. You can write this raw data out to a JPEG file which can then be read by any standard image viewing software.</summary>
		public byte[] GetThumbData(string filename) {
			
			IStorageWrapper wrapper = new IStorageWrapper(_thumbDBFile, false);
			
			foreach(CatalogItem catItem in _catalogItems) {
				
				if(catItem.filename == filename) {
					
					string streamName = BuildReverseString(catItem.itemID);
					FileObject fileObject = wrapper.OpenUCOMStream(null, streamName);
					byte[] rawJPGData = new byte[fileObject.Length];
					fileObject.Read(rawJPGData, 0, (int)fileObject.Length);
					fileObject.Close();
					
					// 3 ints of header data need to be removed
					// Don't know what first int is.
					// 2nd int is thumb index
					// 3rd is size of thumbnail data.
					byte[] jpgData = new byte[rawJPGData.Length - 12];
					for(int index = 12;index < jpgData.Length;index++) {
						jpgData[index - 12] = rawJPGData[index];
					}
					return jpgData;
				}
			}
			return null;
		}
		
		/// <summary>Returns the thumbnail image from the raw data retrieved by GetThumbData.</summary>
		public Image GetThumbnailImage(string filename) {
			
			byte[] thumbData = GetThumbData(filename);
			if(null == thumbData) {
				return null;
			}
			MemoryStream ms = new MemoryStream(thumbData);
			Image img = Image.FromStream(ms);
			return img;
		}
		
		private void LoadCatalog() {
			
			IStorageWrapper wrapper    = new IStorageWrapper(_thumbDBFile, false);
			FileObject      fileObject = wrapper.OpenUCOMStream(null, "Catalog");
			String          textData   = String.Empty;
			
			if(fileObject != null) {
				
				Byte[] fileData = new Byte[fileObject.Length];
				fileObject.Read(fileData, 0, (int)fileObject.Length);
				
				MemoryStream  ms = new MemoryStream(fileData);
				BinaryReader  br = new BinaryReader(ms);
				CatalogHeader ch = new CatalogHeader();
				
				ch.num1 = br.ReadInt16();
				ch.num2 = br.ReadInt16();
				ch.thumbCount = br.ReadInt32();
				ch.thumbWidth = br.ReadInt32();
				ch.thumbHeight = br.ReadInt32();
				
				for(int index = 0;index < ch.thumbCount;index++) {
					
					CatalogItem item = new CatalogItem();
					item.num1   = br.ReadInt32();
					item.itemID = br.ReadInt32();
					item.num3   = br.ReadInt16();
					item.num4   = br.ReadInt16();
					item.num5   = br.ReadInt16();
					item.num6   = br.ReadInt16();
					ushort usChar;
					while((usChar = br.ReadUInt16()) != 0x0000) {
						
						byte[] aChar = new byte[2];
						aChar[0] = (byte)(usChar & 0x00FF);
						aChar[1] = (byte)((usChar & 0xFF00) >> 8);
						item.filename += Encoding.Unicode.GetString(aChar);
					}
					item.num7 = br.ReadInt16();
					_catalogItems.Add(item);
				}
			}
		}
		
		private string BuildReverseString(int itemID) {
			string itemString = itemID.ToString();
			string reverse = "";
			for(int index = itemString.Length - 1;index >= 0;index--) {
				reverse += itemString[index];
			}
			return reverse;
		}
		
		
	}
}
