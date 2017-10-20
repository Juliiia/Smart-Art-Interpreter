namespace SmartArtInterpreter.SmartArtRibbon
{
    partial class SmartArtConverter : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public SmartArtConverter()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">"true", wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls "false".</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für Designerunterstützung -
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmartArtConverter));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.box1 = this.Factory.CreateRibbonBox();
            this.kontrolleStart = this.Factory.CreateRibbonButton();
            this.SmartArtDesc = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.openFolderButton = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.box1.SuspendLayout();
            this.group2.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Groups.Add(this.group2);
            this.tab1.Label = "SmartArt Converter";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.box1);
            this.group1.Items.Add(this.SmartArtDesc);
            this.group1.Name = "group1";
            // 
            // box1
            // 
            this.box1.Items.Add(this.kontrolleStart);
            this.box1.Name = "box1";
            // 
            // kontrolleStart
            // 
            this.kontrolleStart.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.kontrolleStart.Image = ((System.Drawing.Image)(resources.GetObject("kontrolleStart.Image")));
            this.kontrolleStart.Label = "Konvertieren";
            this.kontrolleStart.Name = "kontrolleStart";
            this.kontrolleStart.ShowImage = true;
            this.kontrolleStart.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.kontrolleStart_Click);
            // 
            // SmartArtDesc
            // 
            this.SmartArtDesc.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.SmartArtDesc.Image = ((System.Drawing.Image)(resources.GetObject("SmartArtDesc.Image")));
            this.SmartArtDesc.Label = "Alle SmartArt-Beschreibungen";
            this.SmartArtDesc.Name = "SmartArtDesc";
            this.SmartArtDesc.ShowImage = true;
            this.SmartArtDesc.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.SmartArtDesc_Click);
            // 
            // group2
            // 
            this.group2.Items.Add(this.openFolderButton);
            this.group2.Name = "group2";
            // 
            // openFolderButton
            // 
            this.openFolderButton.Label = "Zielordner öffnen";
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.openFolderButton_Click);
            // 
            // SmartArtConverter
            // 
            this.Name = "SmartArtConverter";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.SmartArtConverter_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.box1.ResumeLayout(false);
            this.box1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton SmartArtDesc;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton kontrolleStart;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton openFolderButton;
    }

    //partial class ThisRibbonCollection
    //{
    //    internal SmartArtConverter SmartArtConverter
    //    {
    //        get { return this.GetRibbon<SmartArtConverter>(); }
    //    }
    //}
}
