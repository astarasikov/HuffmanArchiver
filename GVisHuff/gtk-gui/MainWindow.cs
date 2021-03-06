// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------



public partial class MainWindow {
    
    private Gtk.UIManager UIManager;
    
    private Gtk.Action FileAction;
    
    private Gtk.Action jumpToAction;
    
    private Gtk.Action quitAction;
    
    private Gtk.Action HelpAction;
    
    private Gtk.Action saveAction;
    
    private Gtk.Action helpAction;
    
    private Gtk.Action OptionsAction;
    
    private Gtk.Action GraphicsAction;
    
    private Gtk.Action AnimationDelayAction;
    
    private Gtk.Action mediaPlayAction;
    
    private Gtk.RadioAction d500MsAction;
    
    private Gtk.RadioAction d1000MsAction;
    
    private Gtk.RadioAction d250MsAction;
    
    private Gtk.RadioAction d100MsAction;
    
    private Gtk.ToggleAction DrawGridAction;
    
    private Gtk.ToggleAction ANSIOnlyAction;
    
    private Gtk.VBox MainLayout;
    
    private Gtk.MenuBar MainMenu;
    
    private Gtk.HBox SenderHBox;
    
    private Gtk.VBox SenderVBox;
    
    private Gtk.HBox MsgEncHBox;
    
    private Gtk.Entry SenderEntry;
    
    private Gtk.Button SendButton;
    
    private Gtk.ScrolledWindow SenderScrWnd;
    
    private Gtk.DrawingArea SenderTreeDrawer;
    
    private Gtk.VBox ReceiverVBox;
    
    private Gtk.HBox DecHBox;
    
    private Gtk.Label label3;
    
    private Gtk.Entry DecEntry;
    
    private Gtk.ScrolledWindow ReceiverScrWnd;
    
    private Gtk.DrawingArea ReceiverTreeDrawer;
    
    private Gtk.HBox BinHBox;
    
    private Gtk.Label BinLabel;
    
    private Gtk.Entry BinEntry;
    
    private Gtk.Label HotkeyHintLabel;
    
    protected virtual void Build() {
        Stetic.Gui.Initialize(this);
        // Widget MainWindow
        this.UIManager = new Gtk.UIManager();
        Gtk.ActionGroup w1 = new Gtk.ActionGroup("Default");
        this.FileAction = new Gtk.Action("FileAction", Mono.Unix.Catalog.GetString("File"), null, null);
        this.FileAction.ShortLabel = Mono.Unix.Catalog.GetString("File");
        w1.Add(this.FileAction, null);
        this.jumpToAction = new Gtk.Action("jumpToAction", Mono.Unix.Catalog.GetString("Restart"), null, "gtk-jump-to");
        this.jumpToAction.ShortLabel = Mono.Unix.Catalog.GetString("Restart");
        w1.Add(this.jumpToAction, null);
        this.quitAction = new Gtk.Action("quitAction", Mono.Unix.Catalog.GetString("Quit"), null, "gtk-quit");
        this.quitAction.ShortLabel = Mono.Unix.Catalog.GetString("Quit");
        w1.Add(this.quitAction, null);
        this.HelpAction = new Gtk.Action("HelpAction", Mono.Unix.Catalog.GetString("Help"), null, "none");
        this.HelpAction.ShortLabel = Mono.Unix.Catalog.GetString("About...");
        w1.Add(this.HelpAction, null);
        this.saveAction = new Gtk.Action("saveAction", Mono.Unix.Catalog.GetString("Take snapshot"), null, "gtk-save");
        this.saveAction.ShortLabel = Mono.Unix.Catalog.GetString("Take snapshot");
        w1.Add(this.saveAction, null);
        this.helpAction = new Gtk.Action("helpAction", Mono.Unix.Catalog.GetString("About..."), null, "gtk-help");
        this.helpAction.ShortLabel = Mono.Unix.Catalog.GetString("About...");
        w1.Add(this.helpAction, null);
        this.OptionsAction = new Gtk.Action("OptionsAction", Mono.Unix.Catalog.GetString("Options"), null, null);
        this.OptionsAction.ShortLabel = Mono.Unix.Catalog.GetString("Options");
        w1.Add(this.OptionsAction, null);
        this.GraphicsAction = new Gtk.Action("GraphicsAction", Mono.Unix.Catalog.GetString("Graphics"), null, null);
        this.GraphicsAction.ShortLabel = Mono.Unix.Catalog.GetString("Graphics");
        w1.Add(this.GraphicsAction, null);
        this.AnimationDelayAction = new Gtk.Action("AnimationDelayAction", Mono.Unix.Catalog.GetString("Animation Delay"), null, null);
        this.AnimationDelayAction.ShortLabel = Mono.Unix.Catalog.GetString("Animation Delay");
        w1.Add(this.AnimationDelayAction, null);
        this.mediaPlayAction = new Gtk.Action("mediaPlayAction", Mono.Unix.Catalog.GetString("Frame Delay"), null, "gtk-media-play");
        this.mediaPlayAction.ShortLabel = Mono.Unix.Catalog.GetString("Frame Delay");
        w1.Add(this.mediaPlayAction, null);
        this.d500MsAction = new Gtk.RadioAction("d500MsAction", Mono.Unix.Catalog.GetString("500 ms"), null, null, 0);
        this.d500MsAction.Group = new GLib.SList(System.IntPtr.Zero);
        this.d500MsAction.ShortLabel = Mono.Unix.Catalog.GetString("500 ms");
        w1.Add(this.d500MsAction, null);
        this.d1000MsAction = new Gtk.RadioAction("d1000MsAction", Mono.Unix.Catalog.GetString("1000 ms"), null, null, 0);
        this.d1000MsAction.Group = this.d500MsAction.Group;
        this.d1000MsAction.ShortLabel = Mono.Unix.Catalog.GetString("1000 ms");
        w1.Add(this.d1000MsAction, null);
        this.d250MsAction = new Gtk.RadioAction("d250MsAction", Mono.Unix.Catalog.GetString("250 ms"), null, null, 0);
        this.d250MsAction.Group = this.d500MsAction.Group;
        this.d250MsAction.ShortLabel = Mono.Unix.Catalog.GetString("250 ms");
        w1.Add(this.d250MsAction, null);
        this.d100MsAction = new Gtk.RadioAction("d100MsAction", Mono.Unix.Catalog.GetString("100 ms"), null, null, 0);
        this.d100MsAction.Group = this.d500MsAction.Group;
        this.d100MsAction.ShortLabel = Mono.Unix.Catalog.GetString("100 ms");
        w1.Add(this.d100MsAction, null);
        this.DrawGridAction = new Gtk.ToggleAction("DrawGridAction", Mono.Unix.Catalog.GetString("Draw Grid"), null, null);
        this.DrawGridAction.ShortLabel = Mono.Unix.Catalog.GetString("Draw Grid");
        w1.Add(this.DrawGridAction, null);
        this.ANSIOnlyAction = new Gtk.ToggleAction("ANSIOnlyAction", Mono.Unix.Catalog.GetString("ANSI Only"), null, null);
        this.ANSIOnlyAction.Active = true;
        this.ANSIOnlyAction.ShortLabel = Mono.Unix.Catalog.GetString("ANSI Only");
        w1.Add(this.ANSIOnlyAction, null);
        this.UIManager.InsertActionGroup(w1, 0);
        this.AddAccelGroup(this.UIManager.AccelGroup);
        this.Name = "MainWindow";
        this.Title = Mono.Unix.Catalog.GetString("GVisHuff - Adaptive Huffman Animator");
        this.WindowPosition = ((Gtk.WindowPosition)(4));
        // Container child MainWindow.Gtk.Container+ContainerChild
        this.MainLayout = new Gtk.VBox();
        this.MainLayout.Name = "MainLayout";
        this.MainLayout.Spacing = 6;
        // Container child MainLayout.Gtk.Box+BoxChild
        this.UIManager.AddUiFromString("<ui><menubar name='MainMenu'><menu name='FileAction' action='FileAction'><menuitem name='jumpToAction' action='jumpToAction'/><menuitem name='saveAction' action='saveAction'/><separator/><menuitem name='quitAction' action='quitAction'/></menu><menu name='OptionsAction' action='OptionsAction'><menu name='mediaPlayAction' action='mediaPlayAction'><menuitem name='d100MsAction' action='d100MsAction'/><menuitem name='d250MsAction' action='d250MsAction'/><menuitem name='d500MsAction' action='d500MsAction'/><menuitem name='d1000MsAction' action='d1000MsAction'/></menu><menuitem name='DrawGridAction' action='DrawGridAction'/><menuitem name='ANSIOnlyAction' action='ANSIOnlyAction'/></menu><menu name='HelpAction' action='HelpAction'><menuitem name='helpAction' action='helpAction'/></menu></menubar></ui>");
        this.MainMenu = ((Gtk.MenuBar)(this.UIManager.GetWidget("/MainMenu")));
        this.MainMenu.Name = "MainMenu";
        this.MainLayout.Add(this.MainMenu);
        Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.MainLayout[this.MainMenu]));
        w2.Position = 0;
        w2.Expand = false;
        w2.Fill = false;
        // Container child MainLayout.Gtk.Box+BoxChild
        this.SenderHBox = new Gtk.HBox();
        this.SenderHBox.Name = "SenderHBox";
        this.SenderHBox.Homogeneous = true;
        this.SenderHBox.Spacing = 6;
        // Container child SenderHBox.Gtk.Box+BoxChild
        this.SenderVBox = new Gtk.VBox();
        this.SenderVBox.Name = "SenderVBox";
        this.SenderVBox.Spacing = 6;
        // Container child SenderVBox.Gtk.Box+BoxChild
        this.MsgEncHBox = new Gtk.HBox();
        this.MsgEncHBox.Name = "MsgEncHBox";
        this.MsgEncHBox.Spacing = 6;
        // Container child MsgEncHBox.Gtk.Box+BoxChild
        this.SenderEntry = new Gtk.Entry();
        this.SenderEntry.CanFocus = true;
        this.SenderEntry.Name = "SenderEntry";
        this.SenderEntry.IsEditable = false;
        this.SenderEntry.InvisibleChar = '●';
        this.MsgEncHBox.Add(this.SenderEntry);
        Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.MsgEncHBox[this.SenderEntry]));
        w3.Position = 0;
        // Container child MsgEncHBox.Gtk.Box+BoxChild
        this.SendButton = new Gtk.Button();
        this.SendButton.CanFocus = true;
        this.SendButton.Name = "SendButton";
        this.SendButton.UseUnderline = true;
        // Container child SendButton.Gtk.Container+ContainerChild
        Gtk.Alignment w4 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w5 = new Gtk.HBox();
        w5.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w6 = new Gtk.Image();
        w6.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-media-play", Gtk.IconSize.Menu, 16);
        w5.Add(w6);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w8 = new Gtk.Label();
        w8.LabelProp = Mono.Unix.Catalog.GetString("Send Message");
        w8.UseUnderline = true;
        w5.Add(w8);
        w4.Add(w5);
        this.SendButton.Add(w4);
        this.MsgEncHBox.Add(this.SendButton);
        Gtk.Box.BoxChild w12 = ((Gtk.Box.BoxChild)(this.MsgEncHBox[this.SendButton]));
        w12.Position = 1;
        w12.Expand = false;
        w12.Fill = false;
        this.SenderVBox.Add(this.MsgEncHBox);
        Gtk.Box.BoxChild w13 = ((Gtk.Box.BoxChild)(this.SenderVBox[this.MsgEncHBox]));
        w13.Position = 0;
        w13.Expand = false;
        w13.Fill = false;
        // Container child SenderVBox.Gtk.Box+BoxChild
        this.SenderScrWnd = new Gtk.ScrolledWindow();
        this.SenderScrWnd.CanFocus = true;
        this.SenderScrWnd.Name = "SenderScrWnd";
        this.SenderScrWnd.ShadowType = ((Gtk.ShadowType)(1));
        // Container child SenderScrWnd.Gtk.Container+ContainerChild
        Gtk.Viewport w14 = new Gtk.Viewport();
        w14.ShadowType = ((Gtk.ShadowType)(0));
        // Container child GtkViewport.Gtk.Container+ContainerChild
        this.SenderTreeDrawer = new Gtk.DrawingArea();
        this.SenderTreeDrawer.Name = "SenderTreeDrawer";
        w14.Add(this.SenderTreeDrawer);
        this.SenderScrWnd.Add(w14);
        this.SenderVBox.Add(this.SenderScrWnd);
        Gtk.Box.BoxChild w17 = ((Gtk.Box.BoxChild)(this.SenderVBox[this.SenderScrWnd]));
        w17.Position = 1;
        this.SenderHBox.Add(this.SenderVBox);
        Gtk.Box.BoxChild w18 = ((Gtk.Box.BoxChild)(this.SenderHBox[this.SenderVBox]));
        w18.Position = 0;
        // Container child SenderHBox.Gtk.Box+BoxChild
        this.ReceiverVBox = new Gtk.VBox();
        this.ReceiverVBox.Name = "ReceiverVBox";
        this.ReceiverVBox.Spacing = 6;
        // Container child ReceiverVBox.Gtk.Box+BoxChild
        this.DecHBox = new Gtk.HBox();
        this.DecHBox.Name = "DecHBox";
        this.DecHBox.Spacing = 6;
        // Container child DecHBox.Gtk.Box+BoxChild
        this.label3 = new Gtk.Label();
        this.label3.Name = "label3";
        this.label3.LabelProp = Mono.Unix.Catalog.GetString("Decoded message:");
        this.DecHBox.Add(this.label3);
        Gtk.Box.BoxChild w19 = ((Gtk.Box.BoxChild)(this.DecHBox[this.label3]));
        w19.Position = 0;
        w19.Expand = false;
        w19.Fill = false;
        // Container child DecHBox.Gtk.Box+BoxChild
        this.DecEntry = new Gtk.Entry();
        this.DecEntry.CanFocus = true;
        this.DecEntry.Name = "DecEntry";
        this.DecEntry.IsEditable = false;
        this.DecEntry.InvisibleChar = '●';
        this.DecHBox.Add(this.DecEntry);
        Gtk.Box.BoxChild w20 = ((Gtk.Box.BoxChild)(this.DecHBox[this.DecEntry]));
        w20.Position = 1;
        this.ReceiverVBox.Add(this.DecHBox);
        Gtk.Box.BoxChild w21 = ((Gtk.Box.BoxChild)(this.ReceiverVBox[this.DecHBox]));
        w21.Position = 0;
        w21.Expand = false;
        w21.Fill = false;
        // Container child ReceiverVBox.Gtk.Box+BoxChild
        this.ReceiverScrWnd = new Gtk.ScrolledWindow();
        this.ReceiverScrWnd.CanFocus = true;
        this.ReceiverScrWnd.Name = "ReceiverScrWnd";
        this.ReceiverScrWnd.ShadowType = ((Gtk.ShadowType)(1));
        // Container child ReceiverScrWnd.Gtk.Container+ContainerChild
        Gtk.Viewport w22 = new Gtk.Viewport();
        w22.ShadowType = ((Gtk.ShadowType)(0));
        // Container child GtkViewport1.Gtk.Container+ContainerChild
        this.ReceiverTreeDrawer = new Gtk.DrawingArea();
        this.ReceiverTreeDrawer.Name = "ReceiverTreeDrawer";
        w22.Add(this.ReceiverTreeDrawer);
        this.ReceiverScrWnd.Add(w22);
        this.ReceiverVBox.Add(this.ReceiverScrWnd);
        Gtk.Box.BoxChild w25 = ((Gtk.Box.BoxChild)(this.ReceiverVBox[this.ReceiverScrWnd]));
        w25.Position = 1;
        this.SenderHBox.Add(this.ReceiverVBox);
        Gtk.Box.BoxChild w26 = ((Gtk.Box.BoxChild)(this.SenderHBox[this.ReceiverVBox]));
        w26.Position = 1;
        this.MainLayout.Add(this.SenderHBox);
        Gtk.Box.BoxChild w27 = ((Gtk.Box.BoxChild)(this.MainLayout[this.SenderHBox]));
        w27.Position = 1;
        // Container child MainLayout.Gtk.Box+BoxChild
        this.BinHBox = new Gtk.HBox();
        this.BinHBox.Name = "BinHBox";
        this.BinHBox.Spacing = 6;
        // Container child BinHBox.Gtk.Box+BoxChild
        this.BinLabel = new Gtk.Label();
        this.BinLabel.Name = "BinLabel";
        this.BinLabel.LabelProp = Mono.Unix.Catalog.GetString("Data transferred");
        this.BinHBox.Add(this.BinLabel);
        Gtk.Box.BoxChild w28 = ((Gtk.Box.BoxChild)(this.BinHBox[this.BinLabel]));
        w28.Position = 0;
        w28.Expand = false;
        w28.Fill = false;
        // Container child BinHBox.Gtk.Box+BoxChild
        this.BinEntry = new Gtk.Entry();
        this.BinEntry.CanFocus = true;
        this.BinEntry.Name = "BinEntry";
        this.BinEntry.IsEditable = false;
        this.BinEntry.InvisibleChar = '●';
        this.BinHBox.Add(this.BinEntry);
        Gtk.Box.BoxChild w29 = ((Gtk.Box.BoxChild)(this.BinHBox[this.BinEntry]));
        w29.Position = 1;
        this.MainLayout.Add(this.BinHBox);
        Gtk.Box.BoxChild w30 = ((Gtk.Box.BoxChild)(this.MainLayout[this.BinHBox]));
        w30.Position = 2;
        w30.Expand = false;
        w30.Fill = false;
        // Container child MainLayout.Gtk.Box+BoxChild
        this.HotkeyHintLabel = new Gtk.Label();
        this.HotkeyHintLabel.Name = "HotkeyHintLabel";
        this.HotkeyHintLabel.LabelProp = Mono.Unix.Catalog.GetString("<Ctl-S> to capture the screen, <Ctl-Q> to quit");
        this.MainLayout.Add(this.HotkeyHintLabel);
        Gtk.Box.BoxChild w31 = ((Gtk.Box.BoxChild)(this.MainLayout[this.HotkeyHintLabel]));
        w31.PackType = ((Gtk.PackType)(1));
        w31.Position = 3;
        w31.Expand = false;
        w31.Fill = false;
        this.Add(this.MainLayout);
        if ((this.Child != null)) {
            this.Child.ShowAll();
        }
        this.DefaultWidth = 697;
        this.DefaultHeight = 538;
        this.Show();
        this.DeleteEvent += new Gtk.DeleteEventHandler(this.OnDeleteEvent);
        this.jumpToAction.Activated += new System.EventHandler(this.OnRedoActionActivated);
        this.quitAction.Activated += new System.EventHandler(this.OnQuitActionActivated);
        this.saveAction.Activated += new System.EventHandler(this.OnSaveAsActionActivated);
        this.helpAction.Activated += new System.EventHandler(this.OnAboutActionActivated);
        this.d500MsAction.Activated += new System.EventHandler(this.On500MsActionActivated);
        this.d1000MsAction.Activated += new System.EventHandler(this.On1000MsActionActivated);
        this.d250MsAction.Activated += new System.EventHandler(this.On250MsActionActivated);
        this.d100MsAction.Activated += new System.EventHandler(this.On100MsActionActivated);
        this.DrawGridAction.Toggled += new System.EventHandler(this.OnDrawGridActionToggled);
        this.ANSIOnlyAction.Toggled += new System.EventHandler(this.OnANSIOnlyActionToggled);
        this.SenderEntry.KeyReleaseEvent += new Gtk.KeyReleaseEventHandler(this.OnSenderEntryKeyReleaseEvent);
        this.SendButton.Clicked += new System.EventHandler(this.OnSendButtonClicked);
        this.SenderTreeDrawer.ExposeEvent += new Gtk.ExposeEventHandler(this.OnSenderTreeDrawerExposeEvent);
        this.ReceiverTreeDrawer.ExposeEvent += new Gtk.ExposeEventHandler(this.OnReceiverTreeDrawerExposeEvent);
    }
}
