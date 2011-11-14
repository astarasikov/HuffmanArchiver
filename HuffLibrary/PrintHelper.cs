// 
//  PrintHelper.cs
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

namespace HuffLibrary {

	public static class PrintHelper {
		public static void Warn (string Message) {
			Write (Message, ConsoleColor.Yellow);
		}

		public static void Warn (string Message, params object[] data) {
			Write (Message, ConsoleColor.Yellow, data);
		}

		public static void Err (string Message) {
			Write (Message, ConsoleColor.Red);
		}

		public static void Err (string Message, params object[] data) {
			Write (Message, ConsoleColor.Red, data);
		}

		public static void Notify (string Message) {
			Write (Message, ConsoleColor.Cyan);
		}

		public static void Notify (string Message, params object[] data) {
			Write (Message, ConsoleColor.Cyan, data);
		}

		private static void Write (string Message, ConsoleColor color, params object[] data) {
			ConsoleColor ck = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.Write (Message, data);
			Console.ForegroundColor = ck;
		}

		private static void Write (string Message, ConsoleColor color) {
			ConsoleColor ck = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.Write (Message);
			Console.ForegroundColor = ck;
		}
	}
}
