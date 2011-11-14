// 
//  HuffOps.cs
//  
//  Author:
//       Alexander Tarasikov <alexander.tarasikov@gmail.com>
// 
//  Copyright (c) 2010
// 
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//  
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// 

using System;
using System.IO;
using System.Collections.Generic;
using HuffLibrary;
using System.Threading;

namespace HuffLibrary {

	public class Compressor {
		public delegate void UpdateCRC (int value);

		private static byte[] sign = { 0x5a, 0x52, 0x41, 0x48 };

		public static void HuffConsole (string InputFile, string OutputFile) {
			try {
			Stream ifstream = new FileStream (InputFile, FileMode.Open, FileAccess.Read);
			Stream ofstream = new FileStream (OutputFile, FileMode.Create, FileAccess.ReadWrite);
			CRCCalc crc = new CRCCalc ();
			
			//Writing file signature
			ofstream.Write (sign, 0, sign.Length);
			//Padding for the header
			for (int i = 0; i < 12; i++)
				ofstream.WriteByte (0x0);
			
			Thread myCompressor = new Thread (o => Huff (ifstream, ofstream, val => crc.UpdateByte ((byte)val)));
			myCompressor.Start ();
			
			while (myCompressor.IsAlive) {
				Console.Write ("\rCompressed {0} out of {1}", ifstream.Position, ifstream.Length);
				Thread.Sleep (100);
			}
			//Writing file length to the header
			ofstream.Seek (4, SeekOrigin.Begin);
			byte[] buffer = BitConverter.GetBytes (ofstream.Length);
			ofstream.Write (buffer, 0, buffer.Length);
			
			//Writitng file crc immediately after the length
			buffer = BitConverter.GetBytes (crc.GetCRC ());
			ofstream.Write (buffer, 0, buffer.Length);
			PrintHelper.Notify ("\nOriginal file CRC32 is {0:X8}\n", crc.GetCRC ());
			ofstream.Close();
			ifstream.Close ();
			}
			catch (Exception e) {
				throw e;
			}
		}

		public static void Huff (Stream sin, Stream sout, UpdateCRC callback) {
			Tree t = new Tree ();
			int rd, i, tmp;
			Stack<int> ret;
			BitIO bw = new BitIO (sout, true);
			while ((rd = sin.ReadByte ()) != -1) {
				tmp = rd;
				callback ((byte)tmp);
				if (!t.contains (rd)) {
					ret = t.GetCode (257);
					while (ret.Count > 0)
						bw.WriteBit (ret.Pop ());
					for (i = 0; i < 8; i++) {
						bw.WriteBit ((int)(rd & 0x80));
						rd <<= 1;
					}
					t.UpdateTree (tmp);
				}				
				else {
					ret = t.GetCode (tmp);
					while (ret.Count > 0)
						bw.WriteBit (ret.Pop ());
					t.UpdateTree (tmp);
				}
			}
			
			ret = t.GetCode (256);
			while (ret.Count > 0) {
				bw.WriteBit (ret.Pop ());
			}
		}

		public static void Huff (Stream sin, Stream sout) {
			Tree t = new Tree ();
			int rd, i, tmp;
			Stack<int> ret;
			BitIO bw = new BitIO (sout, true);
			while ((rd = sin.ReadByte ()) != -1) {
				tmp = rd;
				if (!t.contains (rd)) {
					ret = t.GetCode (257);
					while (ret.Count > 0)
						bw.WriteBit (ret.Pop ());
					for (i = 0; i < 8; i++) {
						bw.WriteBit ((int)(rd & 0x80));
						rd <<= 1;
					}
					t.UpdateTree (tmp);
				}
				
				else {
					ret = t.GetCode (tmp);
					while (ret.Count > 0)
						bw.WriteBit (ret.Pop ());
					t.UpdateTree (tmp);
				}
			}
			
			ret = t.GetCode (256);
			while (ret.Count > 0)
				bw.WriteBit (ret.Pop ());
			bw.Close();
		}

		public static void UnHuffConsole (string InputFile, string OutputFile) {
			Stream ifstream = new FileStream (InputFile, FileMode.Open, FileAccess.Read);
			Stream ofstream = new FileStream (OutputFile, FileMode.Create, FileAccess.ReadWrite);
			CRCCalc crcCalc = new CRCCalc ();
			uint crc_old, crc_new;
			
			for (int i = 0; i < sign.Length; i++)
				if (ifstream.ReadByte () != sign[i]) {
					ifstream.Close ();
					ofstream.Close ();
					throw new IOException ("The supplied file is not a valid huff archive");
				}
			
			//Read file length
			byte[] buffer = new byte[8];
			ifstream.Read (buffer, 0, 8);
			long size = BitConverter.ToInt64 (buffer, 0);
			if (size != ifstream.Length) {
				ifstream.Close ();
				ofstream.Close ();
				throw new IOException ("Invalid file length");
			}
			
			//Read file crc
			ifstream.Read (buffer, 0, 4);
			crc_old = BitConverter.ToUInt32 (buffer, 0);
			PrintHelper.Notify ("Stored crc is {0:X8}\n", crc_old);
			
			Thread myDecompressor = new Thread (o => 
			                                    {
				try {
				UnHuff (ifstream, ofstream, x => crcCalc.UpdateByte ((byte)x));
				}
				catch (Exception e) {
					PrintHelper.Err("\n" + e.Message);
				}
			});
				myDecompressor.Start ();
			while (myDecompressor.IsAlive) {
				PrintHelper.Notify ("\rDecompressed {0} bytes out of {1}", ifstream.Position, ifstream.Length);
				Thread.Sleep (100);
			}
			
			crc_new = crcCalc.GetCRC ();
			PrintHelper.Notify ("\nDecompressed CRC32 is {0:X8}\n", crc_new);
			if (crc_new == crc_old)
				PrintHelper.Notify ("Checksums match - OK\n");
			else
				PrintHelper.Err ("Checksum mismatch - decompressed" + " file may contain corrupted data\n");
				ofstream.Close();
				ifstream.Close();
		}

		public static void UnHuff (Stream InStream, Stream OutStream, UpdateCRC callback) {
			int i = 0, count = 0, sym;
			Tree t = new Tree ();
			BitIO bitIO = new BitIO (InStream, false);
			while ((i = bitIO.ReadBit ()) != 2) {
				if ((sym = t.DecodeBinary (i)) != Tree.CharIsEof) {
					OutStream.WriteByte ((byte)sym);
					callback (sym);
					count++;
				}
			}
		}

		public static void UnHuff (Stream InStream, Stream OutStream) {
			int i = 0, count = 0;
			Tree t = new Tree ();
			BitIO bitIO = new BitIO (InStream, false);
			while ((i = bitIO.ReadBit ()) != 2) {
				if ((count = t.DecodeBinary (i)) != Tree.CharIsEof)
					OutStream.WriteByte ((byte)count);
			}
		}
	}
}
