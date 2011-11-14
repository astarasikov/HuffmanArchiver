// 
//  CRCCalc.cs
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

namespace HuffLibrary
{
	public class CRCCalc
	{
		static uint poly = 0x82608edb;
		static uint[] table = new uint[256];
		static CRCCalc() {
			for (uint i = 0; i < 256; i++) {
                uint cs = i;
                for (uint j = 0; j < 8; j++)
                cs = (cs & 1) > 0 ? (cs >> 1) ^ poly : cs >> 1;
                table[i] = cs;
            }
		}
		
		uint crc;
		public uint GetCRC() { return crc ;}
				
		public CRCCalc () {
			crc = 0xffffffff;
		}
		
		public uint UpdateByte(byte b) {
			crc = table[(crc ^ b) & 0xff] ^ (crc >> 8);
            crc ^= 0xffffffff;
			return crc;
		}
	}
}
