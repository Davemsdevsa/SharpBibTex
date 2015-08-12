namespace SharpBibTex
{
    partial class BibTexRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public BibTexRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl1 = this.Factory.CreateRibbonDialogLauncher();
            this.tbBibTex = this.Factory.CreateRibbonTab();
            this.grpBibTex = this.Factory.CreateRibbonGroup();
            this.cmdButton = this.Factory.CreateRibbonButton();
            this.cmdFromFile = this.Factory.CreateRibbonButton();
            this.tbBibTex.SuspendLayout();
            this.grpBibTex.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbBibTex
            // 
            this.tbBibTex.Groups.Add(this.grpBibTex);
            this.tbBibTex.Label = "BibTex";
            this.tbBibTex.Name = "tbBibTex";
            // 
            // grpBibTex
            // 
            this.grpBibTex.DialogLauncher = ribbonDialogLauncherImpl1;
            this.grpBibTex.Items.Add(this.cmdButton);
            this.grpBibTex.Items.Add(this.cmdFromFile);
            this.grpBibTex.Name = "grpBibTex";
            // 
            // cmdButton
            // 
            this.cmdButton.Label = "Clipboard";
            this.cmdButton.Name = "cmdButton";
            this.cmdButton.ShowImage = true;
            this.cmdButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cmdButton_Click);
            // 
            // cmdFromFile
            // 
            this.cmdFromFile.Label = "From File";
            this.cmdFromFile.Name = "cmdFromFile";
            this.cmdFromFile.ShowImage = true;
            this.cmdFromFile.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cmdFromFile_Click);
            // 
            // BibTexRibbon
            // 
            this.Name = "BibTexRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tbBibTex);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.BibTexRibbon_Load);
            this.tbBibTex.ResumeLayout(false);
            this.tbBibTex.PerformLayout();
            this.grpBibTex.ResumeLayout(false);
            this.grpBibTex.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tbBibTex;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpBibTex;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton cmdButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton cmdFromFile;
    }

    partial class ThisRibbonCollection
    {
        internal BibTexRibbon BibTexRibbon
        {
            get { return this.GetRibbon<BibTexRibbon>(); }
        }
    }
}
