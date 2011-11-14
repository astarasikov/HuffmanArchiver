// 
//  TreeGraphics.cs
//  
//  Author:
//       Alexander Tarasikov <alexander.tarasikov@gmail.com>
// 
//  Copyright (c) 2009-2010
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
using Gdk;
using Gtk;
using HuffLibrary;

namespace GVisHuff {
	public struct DrawSettings {
		public bool draw_grid;
	}

	/// <summary>
	/// The method measures the dimensions of the provided Huffman Tree
	/// </summary>
	public static class TreeGraphics {

		//HACK : Hardcoded values
		const int cellsize = 30, nodesize = 25;

		public static void GetAreaSize (HuffLibrary.Node Root, out int Width, out int Height, int CellSize) {
			Width = Height = 0;
			priv_GetAreaSize (Root, 0, 0, 0, ref Width, ref Height, CellSize);
		}
		private static int priv_GetAreaSize (HuffLibrary.Node Root, int Padding, int Level, int Parent, ref int Width, ref int Height, int CellSize) {
			int ret = Level;
			Width = Math.Max (Width, (Padding + 1) * CellSize);
			Height = Math.Max (Height, (Level - Parent + 1) * 2 * CellSize);
			if (Root.right != null)
				ret = priv_GetAreaSize (Root.right, Padding + 1, Level + 1, Level, ref Width, ref Height, CellSize);
			if (Root.left != null)
				ret = priv_GetAreaSize (Root.left, Padding + 1, ret + 1, Level, ref Width, ref Height, CellSize);
			return ret;
		}

		public static void DrawGrid (Gdk.Drawable wnd, int Size) {
			int Width, Height;
			var gc = new Gdk.GC (wnd);
			
			Gdk.Color line_color = new Gdk.Color (0, 255, 255);
			gc.RgbFgColor = line_color;
			
			wnd.GetSize (out Width, out Height);
			for (int i = 0; i < Width; i += Size)
				wnd.DrawLine (gc, i, 0, i, Height);
			for (int i = 0; i < Height; i += Size)
				wnd.DrawLine (gc, 0, i, Width, i);
		}

		public static void AdjustWindowSize (HuffLibrary.Tree HTree, DrawingArea Canvas) {
			int ImageWidth, ImageHeight;
			Canvas.SetSizeRequest (0, 0);
			//Reduce window size in case our tree is small
			GetAreaSize (HTree.GetRootNode (), out ImageWidth, out ImageHeight, cellsize);
			int width, height;
			Canvas.GdkWindow.GetSize (out width, out height);
			Canvas.SetSizeRequest (ImageWidth, ImageHeight);
			if (height > ImageHeight)
				Canvas.GdkWindow.Resize (width, ImageHeight);
		}

		public static void DrawToFile (HuffLibrary.Tree HTree, DrawSettings Options, string FileName) {
			int width, height;
			GetAreaSize (HTree.GetRootNode (), out width, out height, 30);
			Gdk.Pixmap canvas = new Pixmap (null, width, height, 24);
			var bg_gc = new Gdk.GC (canvas);
			bg_gc.RgbFgColor = new Color (0, 0, 0);
			canvas.DrawRectangle (bg_gc, true, 0, 0, width, height);
			if (Options.draw_grid)
				DrawGrid (canvas, 30);
			var gc = new Gdk.GC (canvas);
			gc.RgbFgColor = new Color (255, 255, 0);
			priv_DrawList (gc, canvas, HTree.GetRootNode (), 0, 0, 0);
			Gdk.Pixbuf buffer = Gdk.Pixbuf.FromDrawable (canvas, Screen.Default.RgbColormap, 0, 0, 0, 0, width, height);
			buffer.Save (FileName, "png");
		}

		public static void DrawTree (HuffLibrary.Tree HTree, Gtk.DrawingArea Canvas, DrawSettings Options) {
			Canvas.GdkWindow.BeginPaintRegion (Canvas.GdkWindow.ClipRegion);
			Canvas.GdkWindow.Background = new Color (0, 0, 0);
			if (Options.draw_grid)
				DrawGrid (Canvas.GdkWindow, cellsize);
			DrawList (Canvas.GdkWindow, HTree.GetRootNode ());
			Canvas.GdkWindow.EndPaint ();
		}

		public static void DrawList (Gdk.Drawable Canvas, Node Root) {
			var gc = new Gdk.GC (Canvas);
			gc.RgbFgColor = new Color (255, 255, 0);
			priv_DrawList (gc, Canvas, Root, 0, 0, 0);
		}

		private static int priv_DrawList (Gdk.GC gc, Gdk.Drawable wnd, Node Root, int Padding, int Level, int Parent) {
			if (Root == null)
				return Level;
			int ret = Level;
			wnd.DrawRectangle (gc, false, Padding * cellsize, Level * cellsize, nodesize, nodesize);
			
			//FIXME: make node size configurable via UI, huh?
			if (Parent != Level) {
				wnd.DrawLine (gc, Padding * cellsize - 1, Level * cellsize + (nodesize >> 1), Padding * cellsize - 2, Level * cellsize + (nodesize >> 1));
				wnd.DrawLine (gc, Padding * cellsize - 2, Level * cellsize + (nodesize >> 1), Padding * cellsize - 2, Parent * cellsize + (nodesize >> 1));
				wnd.DrawLine (gc, Padding * cellsize - 4, Parent * cellsize + (nodesize >> 1), Padding * cellsize - 2, Parent * cellsize + (nodesize >> 1));
			}
			
			string msg = null;
			
			switch (Root.sym) {
				case 259:
					msg = "INT";
					break;
				case 258:
					msg = "RT";
					break;
				case 257:
					msg = "NYT";
					break;
				case 256:
					msg = "EOF";
					break;
				default:
					
					char c = (char)Root.sym;
					if (char.IsLetterOrDigit (c))
						msg = c.ToString ();
					else
						msg = string.Format("{0:x}h", Root.sym);
					break;
			}
			Pango.Context pango_ctx = PangoHelper.ContextGet ();
			Pango.Layout pango_lay = new Pango.Layout (pango_ctx);
			pango_lay.SetText (msg);
			pango_lay.FontDescription = Pango.FontDescription.FromString ("monospace 9");
			wnd.DrawLayout (gc, Padding * cellsize + 1, Level * cellsize + 1, pango_lay);
			
			if (Root.right != null)
				ret = priv_DrawList (gc, wnd, Root.right, Padding + 1, Level + 1, Level);
			if (Root.left != null)
				ret = priv_DrawList (gc, wnd, Root.left, Padding + 1, ret + 1, Level);
			return ret;
		}
		
	}
}
