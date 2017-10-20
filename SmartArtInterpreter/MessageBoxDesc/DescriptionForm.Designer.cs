namespace SmartArtInterpreter.MessageBoxDesc
{
    partial class DescriptionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.subNameLabel = new System.Windows.Forms.Label();
            this.startDescriptionLabel = new System.Windows.Forms.Label();
            this.DescTabs = new System.Windows.Forms.TabControl();
            this.shortDesc = new System.Windows.Forms.TabPage();
            this.shortDescList = new System.Windows.Forms.Label();
            this.txtShortDesc = new System.Windows.Forms.TextBox();
            this.shortDescInfo = new System.Windows.Forms.Label();
            this.longDesc = new System.Windows.Forms.TabPage();
            this.LongDescTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.LongDescPlaceholderLabel = new System.Windows.Forms.Label();
            this.txtLongDesc = new System.Windows.Forms.TextBox();
            this.longDesc1 = new System.Windows.Forms.Label();
            this.PointDesc = new System.Windows.Forms.TabPage();
            this.SubPointTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.MainPointTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSubPointDesc = new System.Windows.Forms.TextBox();
            this.longDesc3 = new System.Windows.Forms.Label();
            this.txtMainPointDesc = new System.Windows.Forms.TextBox();
            this.longDesc2 = new System.Windows.Forms.Label();
            this.descSaveButton = new System.Windows.Forms.Button();
            this.saveStateLabel = new System.Windows.Forms.Label();
            this.toolTipShortDesc = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipLongDesc = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipMainPoint = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipSubPoint = new System.Windows.Forms.ToolTip(this.components);
            this.deleteDesc = new System.Windows.Forms.Button();
            this.SmartArtPictureBox = new System.Windows.Forms.PictureBox();
            this.imageBoxLabel = new System.Windows.Forms.Label();
            this.ImagesFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.smartArtsFoundLabel = new System.Windows.Forms.Label();
            this.DescTabs.SuspendLayout();
            this.shortDesc.SuspendLayout();
            this.longDesc.SuspendLayout();
            this.PointDesc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SmartArtPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // subNameLabel
            // 
            this.subNameLabel.AutoSize = true;
            this.subNameLabel.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subNameLabel.Location = new System.Drawing.Point(8, 7);
            this.subNameLabel.Name = "subNameLabel";
            this.subNameLabel.Size = new System.Drawing.Size(50, 18);
            this.subNameLabel.TabIndex = 0;
            this.subNameLabel.Text = "label1";
            // 
            // startDescriptionLabel
            // 
            this.startDescriptionLabel.AutoSize = true;
            this.startDescriptionLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startDescriptionLabel.Location = new System.Drawing.Point(294, 9);
            this.startDescriptionLabel.MaximumSize = new System.Drawing.Size(400, 80);
            this.startDescriptionLabel.MinimumSize = new System.Drawing.Size(400, 80);
            this.startDescriptionLabel.Name = "startDescriptionLabel";
            this.startDescriptionLabel.Size = new System.Drawing.Size(400, 80);
            this.startDescriptionLabel.TabIndex = 1;
            this.startDescriptionLabel.Tag = "";
            this.startDescriptionLabel.Text = "startDesciptionLabel";
            // 
            // DescTabs
            // 
            this.DescTabs.Controls.Add(this.shortDesc);
            this.DescTabs.Controls.Add(this.longDesc);
            this.DescTabs.Controls.Add(this.PointDesc);
            this.DescTabs.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescTabs.Location = new System.Drawing.Point(287, 100);
            this.DescTabs.Name = "DescTabs";
            this.DescTabs.SelectedIndex = 0;
            this.DescTabs.Size = new System.Drawing.Size(506, 459);
            this.DescTabs.TabIndex = 2;
            // 
            // shortDesc
            // 
            this.shortDesc.Controls.Add(this.shortDescList);
            this.shortDesc.Controls.Add(this.txtShortDesc);
            this.shortDesc.Controls.Add(this.shortDescInfo);
            this.shortDesc.Location = new System.Drawing.Point(4, 25);
            this.shortDesc.Name = "shortDesc";
            this.shortDesc.Padding = new System.Windows.Forms.Padding(3);
            this.shortDesc.Size = new System.Drawing.Size(498, 430);
            this.shortDesc.TabIndex = 0;
            this.shortDesc.Text = "Kurzbeschreibung";
            this.shortDesc.UseVisualStyleBackColor = true;
            // 
            // shortDescList
            // 
            this.shortDescList.AutoSize = true;
            this.shortDescList.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shortDescList.Location = new System.Drawing.Point(7, 222);
            this.shortDescList.Name = "shortDescList";
            this.shortDescList.Size = new System.Drawing.Size(381, 16);
            this.shortDescList.TabIndex = 2;
            this.shortDescList.Text = "Es folgt eine automatische erstellte Listendarstellung des Inhalts\r\n";
            // 
            // txtShortDesc
            // 
            this.txtShortDesc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShortDesc.Location = new System.Drawing.Point(7, 26);
            this.txtShortDesc.Multiline = true;
            this.txtShortDesc.Name = "txtShortDesc";
            this.txtShortDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtShortDesc.Size = new System.Drawing.Size(485, 189);
            this.txtShortDesc.TabIndex = 1;
            this.txtShortDesc.TextChanged += new System.EventHandler(this.TxtShortDesc_TextChanged);
            // 
            // shortDescInfo
            // 
            this.shortDescInfo.AutoSize = true;
            this.shortDescInfo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shortDescInfo.Location = new System.Drawing.Point(7, 7);
            this.shortDescInfo.Name = "shortDescInfo";
            this.shortDescInfo.Size = new System.Drawing.Size(188, 16);
            this.shortDescInfo.TabIndex = 0;
            this.shortDescInfo.Text = "spezielle Kurzbeschreibung:";
            // 
            // longDesc
            // 
            this.longDesc.Controls.Add(this.LongDescTableLayout);
            this.longDesc.Controls.Add(this.LongDescPlaceholderLabel);
            this.longDesc.Controls.Add(this.txtLongDesc);
            this.longDesc.Controls.Add(this.longDesc1);
            this.longDesc.Location = new System.Drawing.Point(4, 25);
            this.longDesc.Name = "longDesc";
            this.longDesc.Padding = new System.Windows.Forms.Padding(3);
            this.longDesc.Size = new System.Drawing.Size(498, 430);
            this.longDesc.TabIndex = 1;
            this.longDesc.Text = "Langbeschreibung";
            this.longDesc.UseVisualStyleBackColor = true;
            // 
            // LongDescTableLayout
            // 
            this.LongDescTableLayout.AutoScroll = true;
            this.LongDescTableLayout.BackColor = System.Drawing.Color.WhiteSmoke;
            this.LongDescTableLayout.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.LongDescTableLayout.ColumnCount = 2;
            this.LongDescTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.08247F));
            this.LongDescTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.91753F));
            this.LongDescTableLayout.Location = new System.Drawing.Point(7, 236);
            this.LongDescTableLayout.Name = "LongDescTableLayout";
            this.LongDescTableLayout.RowCount = 1;
            this.LongDescTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LongDescTableLayout.Size = new System.Drawing.Size(485, 188);
            this.LongDescTableLayout.TabIndex = 4;
            // 
            // LongDescPlaceholderLabel
            // 
            this.LongDescPlaceholderLabel.AutoSize = true;
            this.LongDescPlaceholderLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LongDescPlaceholderLabel.Location = new System.Drawing.Point(9, 217);
            this.LongDescPlaceholderLabel.Name = "LongDescPlaceholderLabel";
            this.LongDescPlaceholderLabel.Size = new System.Drawing.Size(283, 16);
            this.LongDescPlaceholderLabel.TabIndex = 3;
            this.LongDescPlaceholderLabel.Text = "Folgende Platzhalter können verwendet werden:";
            // 
            // txtLongDesc
            // 
            this.txtLongDesc.AutoCompleteCustomSource.AddRange(new string[] {
            "#Farbgestaltung#",
            "#AnzahlHauptpunkte#"});
            this.txtLongDesc.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtLongDesc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtLongDesc.Location = new System.Drawing.Point(7, 26);
            this.txtLongDesc.Multiline = true;
            this.txtLongDesc.Name = "txtLongDesc";
            this.txtLongDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLongDesc.Size = new System.Drawing.Size(485, 189);
            this.txtLongDesc.TabIndex = 1;
            this.txtLongDesc.TextChanged += new System.EventHandler(this.TxtLongDesc_TextChanged);
            // 
            // longDesc1
            // 
            this.longDesc1.AutoSize = true;
            this.longDesc1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.longDesc1.Location = new System.Drawing.Point(7, 7);
            this.longDesc1.Name = "longDesc1";
            this.longDesc1.Size = new System.Drawing.Size(191, 16);
            this.longDesc1.TabIndex = 0;
            this.longDesc1.Text = "spezielle Langbeschreibung:";
            // 
            // PointDesc
            // 
            this.PointDesc.Controls.Add(this.SubPointTableLayoutPanel);
            this.PointDesc.Controls.Add(this.label2);
            this.PointDesc.Controls.Add(this.MainPointTableLayoutPanel);
            this.PointDesc.Controls.Add(this.label1);
            this.PointDesc.Controls.Add(this.txtSubPointDesc);
            this.PointDesc.Controls.Add(this.longDesc3);
            this.PointDesc.Controls.Add(this.txtMainPointDesc);
            this.PointDesc.Controls.Add(this.longDesc2);
            this.PointDesc.Location = new System.Drawing.Point(4, 25);
            this.PointDesc.Name = "PointDesc";
            this.PointDesc.Padding = new System.Windows.Forms.Padding(3);
            this.PointDesc.Size = new System.Drawing.Size(498, 430);
            this.PointDesc.TabIndex = 2;
            this.PointDesc.Text = "Beschreibung pro Punkt";
            this.PointDesc.UseVisualStyleBackColor = true;
            // 
            // SubPointTableLayoutPanel
            // 
            this.SubPointTableLayoutPanel.AutoScroll = true;
            this.SubPointTableLayoutPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.SubPointTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.SubPointTableLayoutPanel.ColumnCount = 2;
            this.SubPointTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.08247F));
            this.SubPointTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.91753F));
            this.SubPointTableLayoutPanel.Location = new System.Drawing.Point(6, 320);
            this.SubPointTableLayoutPanel.Name = "SubPointTableLayoutPanel";
            this.SubPointTableLayoutPanel.RowCount = 1;
            this.SubPointTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.SubPointTableLayoutPanel.Size = new System.Drawing.Size(485, 98);
            this.SubPointTableLayoutPanel.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(283, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Folgende Platzhalter können verwendet werden:";
            // 
            // MainPointTableLayoutPanel
            // 
            this.MainPointTableLayoutPanel.AutoScroll = true;
            this.MainPointTableLayoutPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MainPointTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.MainPointTableLayoutPanel.ColumnCount = 2;
            this.MainPointTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.08247F));
            this.MainPointTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.91753F));
            this.MainPointTableLayoutPanel.Location = new System.Drawing.Point(6, 106);
            this.MainPointTableLayoutPanel.Name = "MainPointTableLayoutPanel";
            this.MainPointTableLayoutPanel.Padding = new System.Windows.Forms.Padding(1);
            this.MainPointTableLayoutPanel.RowCount = 1;
            this.MainPointTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainPointTableLayoutPanel.Size = new System.Drawing.Size(485, 98);
            this.MainPointTableLayoutPanel.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Folgende Platzhalter können verwendet werden:";
            // 
            // txtSubPointDesc
            // 
            this.txtSubPointDesc.Location = new System.Drawing.Point(6, 239);
            this.txtSubPointDesc.Multiline = true;
            this.txtSubPointDesc.Name = "txtSubPointDesc";
            this.txtSubPointDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSubPointDesc.Size = new System.Drawing.Size(485, 60);
            this.txtSubPointDesc.TabIndex = 9;
            this.txtSubPointDesc.TextChanged += new System.EventHandler(this.TxtSubPointDesc_TextChanged);
            // 
            // longDesc3
            // 
            this.longDesc3.AutoSize = true;
            this.longDesc3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.longDesc3.Location = new System.Drawing.Point(6, 220);
            this.longDesc3.Name = "longDesc3";
            this.longDesc3.Size = new System.Drawing.Size(285, 16);
            this.longDesc3.TabIndex = 8;
            this.longDesc3.Text = "Beschreibung pro vorhandenen Unterpunkt:";
            // 
            // txtMainPointDesc
            // 
            this.txtMainPointDesc.Location = new System.Drawing.Point(6, 26);
            this.txtMainPointDesc.Multiline = true;
            this.txtMainPointDesc.Name = "txtMainPointDesc";
            this.txtMainPointDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMainPointDesc.Size = new System.Drawing.Size(485, 60);
            this.txtMainPointDesc.TabIndex = 7;
            this.txtMainPointDesc.TextChanged += new System.EventHandler(this.TxtMainPointDesc_TextChanged);
            // 
            // longDesc2
            // 
            this.longDesc2.AutoSize = true;
            this.longDesc2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.longDesc2.Location = new System.Drawing.Point(6, 7);
            this.longDesc2.Name = "longDesc2";
            this.longDesc2.Size = new System.Drawing.Size(200, 16);
            this.longDesc2.TabIndex = 6;
            this.longDesc2.Text = "Beschreibung pro Hauptpunkt:";
            // 
            // descSaveButton
            // 
            this.descSaveButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descSaveButton.Location = new System.Drawing.Point(718, 565);
            this.descSaveButton.Name = "descSaveButton";
            this.descSaveButton.Size = new System.Drawing.Size(75, 26);
            this.descSaveButton.TabIndex = 3;
            this.descSaveButton.Text = "Speichern";
            this.descSaveButton.UseVisualStyleBackColor = true;
            this.descSaveButton.Click += new System.EventHandler(this.descSaveButton_Click);
            // 
            // saveStateLabel
            // 
            this.saveStateLabel.AutoSize = true;
            this.saveStateLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveStateLabel.Location = new System.Drawing.Point(300, 570);
            this.saveStateLabel.Name = "saveStateLabel";
            this.saveStateLabel.Size = new System.Drawing.Size(42, 16);
            this.saveStateLabel.TabIndex = 4;
            this.saveStateLabel.Text = "label1";
            this.saveStateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // toolTipShortDesc
            // 
            this.toolTipShortDesc.ShowAlways = true;
            // 
            // deleteDesc
            // 
            this.deleteDesc.AutoSize = true;
            this.deleteDesc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteDesc.Location = new System.Drawing.Point(566, 565);
            this.deleteDesc.Name = "deleteDesc";
            this.deleteDesc.Size = new System.Drawing.Size(146, 26);
            this.deleteDesc.TabIndex = 5;
            this.deleteDesc.Text = "Beschreibung löschen";
            this.deleteDesc.UseVisualStyleBackColor = true;
            this.deleteDesc.Click += new System.EventHandler(this.deleteDesc_Click);
            // 
            // SmartArtPictureBox
            // 
            this.SmartArtPictureBox.Location = new System.Drawing.Point(11, 151);
            this.SmartArtPictureBox.Name = "SmartArtPictureBox";
            this.SmartArtPictureBox.Size = new System.Drawing.Size(260, 189);
            this.SmartArtPictureBox.TabIndex = 6;
            this.SmartArtPictureBox.TabStop = false;
            // 
            // imageBoxLabel
            // 
            this.imageBoxLabel.AutoSize = true;
            this.imageBoxLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageBoxLabel.Location = new System.Drawing.Point(12, 135);
            this.imageBoxLabel.MaximumSize = new System.Drawing.Size(257, 30);
            this.imageBoxLabel.MinimumSize = new System.Drawing.Size(257, 10);
            this.imageBoxLabel.Name = "imageBoxLabel";
            this.imageBoxLabel.Size = new System.Drawing.Size(257, 16);
            this.imageBoxLabel.TabIndex = 7;
            this.imageBoxLabel.Text = "imageBoxLabel\r\n";
            // 
            // ImagesFlowLayoutPanel
            // 
            this.ImagesFlowLayoutPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImagesFlowLayoutPanel.Location = new System.Drawing.Point(11, 368);
            this.ImagesFlowLayoutPanel.Name = "ImagesFlowLayoutPanel";
            this.ImagesFlowLayoutPanel.Size = new System.Drawing.Size(260, 191);
            this.ImagesFlowLayoutPanel.TabIndex = 8;
            // 
            // smartArtsFoundLabel
            // 
            this.smartArtsFoundLabel.AutoSize = true;
            this.smartArtsFoundLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smartArtsFoundLabel.Location = new System.Drawing.Point(11, 349);
            this.smartArtsFoundLabel.Name = "smartArtsFoundLabel";
            this.smartArtsFoundLabel.Size = new System.Drawing.Size(47, 16);
            this.smartArtsFoundLabel.TabIndex = 9;
            this.smartArtsFoundLabel.Text = "label3";
            // 
            // DescriptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 603);
            this.Controls.Add(this.smartArtsFoundLabel);
            this.Controls.Add(this.ImagesFlowLayoutPanel);
            this.Controls.Add(this.imageBoxLabel);
            this.Controls.Add(this.SmartArtPictureBox);
            this.Controls.Add(this.deleteDesc);
            this.Controls.Add(this.saveStateLabel);
            this.Controls.Add(this.descSaveButton);
            this.Controls.Add(this.DescTabs);
            this.Controls.Add(this.startDescriptionLabel);
            this.Controls.Add(this.subNameLabel);
            this.Name = "DescriptionForm";
            this.Text = "Maske";
            this.Load += new System.EventHandler(this.DescriptionFormLPCPR_Load);
            this.DescTabs.ResumeLayout(false);
            this.shortDesc.ResumeLayout(false);
            this.shortDesc.PerformLayout();
            this.longDesc.ResumeLayout(false);
            this.longDesc.PerformLayout();
            this.PointDesc.ResumeLayout(false);
            this.PointDesc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SmartArtPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label subNameLabel;
        private System.Windows.Forms.Label startDescriptionLabel;
        private System.Windows.Forms.TabControl DescTabs;
        private System.Windows.Forms.TabPage shortDesc;
        private System.Windows.Forms.Label shortDescList;
        private System.Windows.Forms.TextBox txtShortDesc;
        private System.Windows.Forms.Label shortDescInfo;
        private System.Windows.Forms.TabPage longDesc;
        private System.Windows.Forms.Label longDesc1;
        private System.Windows.Forms.TextBox txtLongDesc;
        private System.Windows.Forms.Button descSaveButton;
        private System.Windows.Forms.Label saveStateLabel;
        private System.Windows.Forms.ToolTip toolTipShortDesc;
        private System.Windows.Forms.ToolTip toolTipLongDesc;
        private System.Windows.Forms.ToolTip toolTipMainPoint;
        private System.Windows.Forms.ToolTip toolTipSubPoint;
        private System.Windows.Forms.Button deleteDesc;
        private System.Windows.Forms.TableLayoutPanel LongDescTableLayout;
        private System.Windows.Forms.Label LongDescPlaceholderLabel;
        private System.Windows.Forms.TabPage PointDesc;
        private System.Windows.Forms.TextBox txtSubPointDesc;
        private System.Windows.Forms.Label longDesc3;
        private System.Windows.Forms.TextBox txtMainPointDesc;
        private System.Windows.Forms.Label longDesc2;
        private System.Windows.Forms.TableLayoutPanel SubPointTableLayoutPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel MainPointTableLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox SmartArtPictureBox;
        private System.Windows.Forms.Label imageBoxLabel;
        private System.Windows.Forms.FlowLayoutPanel ImagesFlowLayoutPanel;
        private System.Windows.Forms.Label smartArtsFoundLabel;
    }
}