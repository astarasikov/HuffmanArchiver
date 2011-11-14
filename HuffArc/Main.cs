// 
//  Main.cs
//  
//  Author:
//       Alexander Tarasikov <alexander.tarasikov@gmail.com>
// 
//  Copyright (c) 2010 2009
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
using HuffLibrary;

namespace HuffArc {
	class MainClass {
		static void CompressFile (string filename) {
			if (System.IO.File.Exists (filename + ".har")) {
				PrintHelper.Warn ("File {0} already exists, won't compress\n", filename + ".har\n");
				return;
			}
			try {
				HuffLibrary.Compressor.HuffConsole (filename, filename + ".har");
			}
			catch (Exception e) {
				PrintHelper.Err("Unable to compress the file due to the error: " + e.Message);
			}
		}

		static void DecompressFile (string filename) {
			string outname = filename;
			if (filename.EndsWith (".har") && filename.Length > 4)
				outname = filename.Substring (0, filename.Length - 4);
			if (System.IO.File.Exists (outname)) {
				PrintHelper.Warn ("File {0} already exists, won't decompress\n", outname);
				return;
			}
			try {
				HuffLibrary.Compressor.UnHuffConsole (filename, outname);
			}
			catch (Exception e) {
				PrintHelper.Err("Unable to decompress the file due to the error: " + e.Message);
			}
		}

		static void PrintHelp () {
			PrintHelper.Notify ("HuffArc, an adaptive Huffman implementation" + "\nUsage:" + "\n\t-d, --decompress file_name" + "\n\t-c, --compress file_name" + "\n\t-v, --version\n");
		}

		public static void Main (string[] args) {
			if (args.Length == 0) {
				PrintHelp ();
				return;
			}
			bool done = false;
			for (int i = 0; !done && i != args.Length; i++) {
				switch (args[i]) {
					case "-c":
						goto case "--compress";
					case "--compress":
						if (i < args.GetUpperBound (0))
							CompressFile (args[i + 1]);
						else
							goto default;
						done = true;
						break;
					case "-d":
						goto case "--decompress";
					case "--decompress":
						if (i < args.GetUpperBound (0))
							DecompressFile (args[i + 1]);
						else
							goto default;
						done = true;
						break;
					case "-v":
						goto case "--version";
					case "--version":
						PrintHelper.Notify ("HuffArc, a demo application for the HuffLibrary version 1.0");
						done = true;
						break;
					default:
						PrintHelp ();
						break;
				}
			}
		}
	}
}
