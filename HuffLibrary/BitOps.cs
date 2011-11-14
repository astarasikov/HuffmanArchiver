// 
// BitStream.cs
// 
// Author:
// Alexander Tarasikov <alexander.tarasikov@gmail.com>
// 
// Copyright (c) 2010
// 
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// 

using System;
using System.IO;

namespace HuffLibrary {
	public class BitIO {
		Stream stream;
		bool ownStream = false, IsOut = false, open = false;
		private BitIO () {
		}
		public BitIO (Stream stream, bool IsOut) {
			this.stream = stream;
			open = true;
			this.IsOut = IsOut;
			if (!IsOut)
				bi = stream.ReadByte ();
		}

		public BitIO (string FileName, bool IsOut) {
			ownStream = true;
			open = true;
			if (IsOut)
				stream = new FileStream (FileName, FileMode.Create, FileAccess.Write);
			else
				stream = new FileStream (FileName, FileMode.Open, FileAccess.Read);
		}

		public void Close() {
			open = false;
			if (IsOut)
				BitFlush ();
			if (ownStream)
				stream.Close ();
		}

		byte buffer = 0, bits = 0;

		public bool CanRead {
			get { return this.open && !IsOut; }
		}
		public bool CanWrite {
			get { return this.open && IsOut; }
		}

		public void WriteBit (int bit) {
			if (!open)
				throw new InvalidOperationException("Cannot write to the disposing stream");
			if (!IsOut)
				throw new NotSupportedException ("Cannot write to the read-only bit stream");
			if (bits == 8) {
				bits = 0;
				stream.WriteByte (buffer);
				buffer = 0;
			}
			bits++;
			buffer <<= 1;
			if (bit > 0)
				buffer |= 0x1;
		}

		private void BitFlush () {
			buffer <<= (8 - bits);
			stream.WriteByte (buffer);
		}

		byte bc = 0;
		int bi = 0;
		public int ReadBit () {
			if (!open)
				throw new InvalidOperationException("Cannot read from the disposing stream");
			if (IsOut)
				throw new NotSupportedException ("Cannot read from a write-only bit stream");
			bc++;
			int ret = (bi & 0x80) > 0 ? 1 : 0;
			bi <<= 1;
			if (bc == 8) {
				bc = 0;
				bi = stream.ReadByte ();
				if (bi == -1) {
					return 2;
				}

				else
					
					bi &= 0xff;
			}
			return ret;
		}
	}
}
