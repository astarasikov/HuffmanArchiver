// 
//  MainWindow.cs
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
using Gtk;
using Gdk;
using HuffLibrary;
using GVisHuff;
using System.Text;
using System.Collections.Generic;
using System.Threading;

public static class CharEx {
	public static bool IsLatin (this char c) {
		if (char.IsLetter (c) && c <= 128)
			return true;
		return false;
	}
}


public partial class MainWindow : Gtk.Window {

	HuffLibrary.Tree SenderTree = new HuffLibrary.Tree ();
	HuffLibrary.Tree ReceiverTree = new HuffLibrary.Tree ();
	Gtk.Window aboutWindow;
	Queue<char> messageQueue = new Queue<char> ();
	int frame_delay = 500;
	//delay between drawing the frames. updated via the GUI menu
	bool tree_updating = false;
	//To indicate the tree is being drawn and must
	//not be interrupted
	bool ansi_mode = true;
	//shows whether to fake the tree into accepting
	//only lower bytes of Unicode so as its operation
	//is more transparent for the user
	GVisHuff.DrawSettings draw_options = new GVisHuff.DrawSettings ();
	//The thread for drawing the receiver tree
	System.Threading.Thread receiver_thread = null;

	private void PostInit () {
		UpdateSenderTree ();
		UpdateReceiverTree ();
	}

	public MainWindow () : base(Gtk.WindowType.Toplevel) {
		Build ();
		PostInit ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a) {
		if (tree_updating && receiver_thread != null)
			receiver_thread.Abort();
		Application.Quit ();
		a.RetVal = true;
	}

	protected virtual void OnQuitActionActivated (object sender, System.EventArgs e) {
		if (tree_updating && receiver_thread != null)
			receiver_thread.Abort();
		Application.Quit ();
	}

	protected virtual void OnAboutActionActivated (object sender, System.EventArgs e) {
		if (null != aboutWindow)
			aboutWindow.ShowAll ();
		else
			(aboutWindow = new HSEAboutDialog ()).ShowAll ();
	}

	private void SendByte (int Data) {
		ReceiverTree.UpdateTree (Data);
		Stack<int> code = ReceiverTree.GetCode (Data);
		while (code.Count > 0) {
			string num = code.Pop ().ToString ();
			Gtk.Application.Invoke ((o, e) => { BinEntry.Text += num; });
		}
	}

	private void SendMessage () {
		int high, low;
		while (messageQueue.Count > 0) {
			char c = messageQueue.Dequeue ();
			high = (c >> 8) & 0xff;
			low = c & 0xff;
			if (!ansi_mode)
				SendByte (high);
			SendByte (low);
			Gtk.Application.Invoke ((o, e) => {
				BinEntry.Text += " ";
				DecEntry.Text += c;
				DecEntry.Position = BinEntry.Position = -1;
				UpdateReceiverTree ();
			});
			System.Threading.Thread.Sleep (frame_delay);
		}
		tree_updating = false;
	}

	protected virtual void OnSendButtonClicked (object sender, System.EventArgs e) {
		if (tree_updating)
			return;
		tree_updating = true;
		(receiver_thread = new Thread (() => SendMessage ())).Start ();
	}

	protected void UpdateSenderTree () {
		TreeGraphics.AdjustWindowSize (SenderTree, SenderTreeDrawer);
		TreeGraphics.DrawTree (SenderTree, SenderTreeDrawer, draw_options);
	}

	protected void UpdateReceiverTree () {
		TreeGraphics.AdjustWindowSize (ReceiverTree, ReceiverTreeDrawer);
		TreeGraphics.DrawTree (ReceiverTree, ReceiverTreeDrawer, draw_options);
	}

	protected virtual void OnSenderTreeDrawerExposeEvent (object o, Gtk.ExposeEventArgs args) {
		TreeGraphics.DrawTree (SenderTree, SenderTreeDrawer, draw_options);
	}

	protected virtual void OnReceiverTreeDrawerExposeEvent (object o, Gtk.ExposeEventArgs args) {
		TreeGraphics.DrawTree (ReceiverTree, ReceiverTreeDrawer, draw_options);
	}

	protected virtual void OnRedoActionActivated (object sender, System.EventArgs e) {
		if (tree_updating)
			return;
		SenderTree = new HuffLibrary.Tree ();
		ReceiverTree = new HuffLibrary.Tree ();
		SenderEntry.Text = DecEntry.Text = BinEntry.Text = "";
		UpdateReceiverTree ();
		UpdateSenderTree ();
	}

	protected virtual void OnSenderEntryKeyReleaseEvent (object o, Gtk.KeyReleaseEventArgs args) {
		if (tree_updating)
			return;
		char c = (char)Gdk.Keyval.ToUnicode (args.Event.KeyValue);
		if (!char.IsLetterOrDigit (c) && !char.IsWhiteSpace (c))
			return;
		if (ansi_mode && !c.IsLatin ())
			return;
		SenderEntry.Text += c.ToString ();
		SenderEntry.Position = -1;
		if (!ansi_mode)
			SenderTree.UpdateTree ((c >> 8) & 0xff);
		SenderTree.UpdateTree (c & 0xff);
		messageQueue.Enqueue (c);
		UpdateSenderTree ();
	}

	protected virtual void On100MsActionActivated (object sender, System.EventArgs e) {
		frame_delay = 100;
	}

	protected virtual void On250MsActionActivated (object sender, System.EventArgs e) {
		frame_delay = 250;
	}

	protected virtual void On500MsActionActivated (object sender, System.EventArgs e) {
		frame_delay = 500;
	}

	protected virtual void On1000MsActionActivated (object sender, System.EventArgs e) {
		frame_delay = 1000;
	}

	protected virtual void OnDrawGridActionToggled (object sender, System.EventArgs e) {
		if (draw_options.draw_grid == DrawGridAction.Active)
			return;
		draw_options.draw_grid = DrawGridAction.Active;
		UpdateReceiverTree ();
		UpdateSenderTree ();
	}

	void SaveToFile (HuffLibrary.Tree HTree, string title) {
		Gtk.FileChooserDialog dlg = new Gtk.FileChooserDialog (title, this, FileChooserAction.Save, Gtk.Stock.Cancel, ResponseType.Cancel, Gtk.Stock.Ok, ResponseType.Ok);
		Gtk.FileFilter filter = new Gtk.FileFilter ();
		filter.AddPattern ("*.png");
		dlg.Filter = filter;
		int ret = dlg.Run ();
		if (ret == (int)Gtk.ResponseType.Ok) {
			string filename = dlg.Filename;
			if (!filename.ToLower ().Contains (".png"))
				filename += ".png";
			TreeGraphics.DrawToFile (HTree, draw_options, filename);
		}
		dlg.Destroy ();
	}

	protected virtual void OnSaveAsActionActivated (object sender, System.EventArgs e) {
		SaveToFile (SenderTree, "Save sender tree image");
		SaveToFile (ReceiverTree, "Save receiver tree image");
	}

	protected virtual void OnANSIOnlyActionToggled (object sender, System.EventArgs e) {
		ansi_mode = ANSIOnlyAction.Active;
	}
}
