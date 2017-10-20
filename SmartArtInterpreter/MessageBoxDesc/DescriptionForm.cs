using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlWriter = SmartArtInterpreter.ShapeInterpreter.SmartArtDescription.XMLWriter;
using DescFormManager = SmartArtInterpreter.MessageBoxDesc.DescriptionFormManager;

namespace SmartArtInterpreter.MessageBoxDesc
{
    public partial class DescriptionForm : Form
    {

        //attributes ---------------------------------------
        private string category;
        private string subCategory;
        private string startDescription;
        private string shortDescription;
        private string longDescription;
        private string mainPointDescription;
        private string subPointDescription;
        private bool foundDescription;

        private string[][] shortDescPlaceholder;
        private string[][] longDescPlaceholder;
        private string[][] mainPointDescPlaceholder;
        private string[][] subPointDescPlaceholder;


        //method ---------------------------------------
        public DescriptionForm()
        {
            InitializeComponent();
        }

        private void DescriptionFormLPCPR_Load(object sender, EventArgs e)
        {
            /*
             * Load the existing content into the form
             */

            //// Header --------------------
            subNameLabel.Text = GetSubCategory();
            startDescriptionLabel.Text = GetStartDescription();

            //// Tab Shortdescription ------
            txtShortDesc.Text = GetShortDescription();
            //TODO: Verbesserung: Informationen für den Nutzer in extra XML auslagern
            toolTipShortDesc.SetToolTip(this.shortDescInfo, "Der von Ihnen eingegebene Text wird in der Kurzbeschreibung angezeigt.");
            toolTipShortDesc.SetToolTip(this.txtShortDesc, "Der von Ihnen eingegebene Text wird in der Kurzbeschreibung angezeigt.");

            //// Tab Longdescription -------       
            txtLongDesc.Text = GetLongDescription();
            toolTipLongDesc.SetToolTip(this.longDesc1, "Der von Ihnen eingegebene Text soll den allgemeinen Aufbau der SmartArt beschreiben, ohne bereits auf den Inhalt der einzelnen Punkte einzugehen.");
            toolTipLongDesc.SetToolTip(this.txtLongDesc, "Der von Ihnen eingegebene Text soll den allgemeinen Aufbau der SmartArt beschreiben, ohne bereits auf den Inhalt der einzelnen Punkte einzugehen.");
            LoadPlaceholderPanal(LongDescTableLayout, longDescPlaceholder, txtLongDesc);

            //// Tab EachPointDescription -----------
            txtMainPointDesc.Text = GetMainPointDescription();
            toolTipMainPoint.SetToolTip(this.longDesc2, "Dieser Text wird für jeden Hauptpunkt wiederholt.");
            toolTipMainPoint.SetToolTip(this.txtMainPointDesc, "Dieser Text wird für jeden Hauptpunkt wiederholt.");
            LoadPlaceholderPanal(MainPointTableLayoutPanel, mainPointDescPlaceholder, txtMainPointDesc);
            txtSubPointDesc.Text = GetSubPointDescription();
            toolTipSubPoint.SetToolTip(this.longDesc3, "Dieser Text wird für jeden Unterpunkt wiederholt.");
            toolTipSubPoint.SetToolTip(this.txtSubPointDesc, "Dieser Text wird für jeden Unterpunkt wiederholt.");
            LoadPlaceholderPanal(SubPointTableLayoutPanel, subPointDescPlaceholder, txtSubPointDesc);

            SaveStateLabel_Font();

        }

        //// left part --------------------------------------------
        private void LoadImageInImageBox(string path, string name)
        {

            imageBoxLabel.Text = name;
            SmartArtPictureBox.Load(path);
            SmartArtPictureBox.SizeMode = PictureBoxSizeMode.Zoom;         
        }

        public void LoadImagesFlowLayoutPanel(List<string> imagePaths)
        {
            
            if (imagePaths.Count > 0)
            {
                // show the first element in the PictureBox
                smartArtsFoundLabel.Text = "SmartArts in dieser Präsentation:";
                
                string[] first = imagePaths.First().Split('.');
                string[] firstName = first[0].Split('\\');
                LoadImageInImageBox(imagePaths.First(), firstName.Last());

                foreach (string path in imagePaths)
                {
                    Label pathLabel = new Label();
                    string[] name = path.Split('.');
                    string[] imageName = name[0].Split('\\');
                    pathLabel.Text = imageName.Last();
                    pathLabel.Size = new System.Drawing.Size(258, 25);

                    pathLabel.Click += (x, y) => LoadImageInImageBox(path, pathLabel.Text);
                    ImagesFlowLayoutPanel.Controls.Add(pathLabel);
                }
            }else{
                smartArtsFoundLabel.Text = "";
                imageBoxLabel.Text = "Es wurden kein SmartArts zu dieser Unterkategorie gefunden.";
            }
        }

        //// Tab ShortDescription ----------------------------------

        private void TxtShortDesc_TextChanged(object sender, EventArgs e)
        {
            /*
             * looking for changes and set the "saveStateLabel"
             */
            if (GetShortDescription() != txtShortDesc.Text)
            {
                txtShortDesc.Modified = true;
                SaveStateLabel_Font();
            }
            else
            {
                txtShortDesc.Modified = false;
                SaveStateLabel_Font();
            }
        }

        //// Tab LongDescription ----------------------------------

        private void TxtLongDesc_TextChanged(object sender, EventArgs e)
        {
            /*
             * looking for changes and set the "saveStateLabel"
             */
            if (GetLongDescription() != txtLongDesc.Text)
            {
                txtLongDesc.Modified = true;
                SaveStateLabel_Font();
            }
            else
            {
                txtLongDesc.Modified = false;
                SaveStateLabel_Font();
            }
        }

        private void LoadPlaceholderPanal(TableLayoutPanel panal, string[][] descType, TextBox txtBox)
        {   
            if (descType != null)
            {
                panal.ColumnCount = 2;
                panal.RowCount = descType.GetLength(0);

                for (int rowcount = 0; rowcount < descType.GetLength(0); rowcount++)
                {
                    Label name = new Label();
                    name.Text = descType[rowcount][0];
                    name.AutoSize = true;
                    name.Click += (x, y) => NameLabel_Click(txtBox, name.Text);
                    Label info = new Label();
                    info.Text = descType[rowcount][1];
                    info.AutoSize = true;
                    panal.Controls.Add(name, 0, rowcount);
                    panal.Controls.Add(info, 1, rowcount);
                }
            }
            else
            {
                panal.ColumnCount = 1;
                panal.RowCount = 1;
                Label empty = new Label();
                empty.Text = "Es sind keine Platzhalter vorhanden.";
                empty.AutoSize = true;
                panal.Controls.Add(empty, 0, 0);
            }
        }

        private void NameLabel_Click(TextBox txtBox, string name)
        {
            /*
             * manage the insert of a Placeholder
             * and set the curser on the right position
             */
            var selectionIndex = txtBox.SelectionStart;
            txtBox.Text = txtBox.Text.Insert(txtBox.SelectionStart, name);
            txtBox.SelectionStart = selectionIndex + name.Length;
        }
        
        //// Tab EachPoint ------------------------------------------

        private void TxtMainPointDesc_TextChanged(object sender, EventArgs e)
        {
            /*
             * looking for changes and set the "saveStateLabel"
             */
            if (GetMainPointDescription() != txtMainPointDesc.Text)
            {
                txtMainPointDesc.Modified = true;
                SaveStateLabel_Font();
            }
            else
            {
                txtMainPointDesc.Modified = false;
                SaveStateLabel_Font();
            }
        }

        private void TxtSubPointDesc_TextChanged(object sender, EventArgs e)
        {
            /*
             * looking for changes and set the "saveStateLabel"
             */
            if (GetSubPointDescription() != txtSubPointDesc.Text)
            {
                txtSubPointDesc.Modified = true;
                SaveStateLabel_Font();
            }
            else
            {
                txtSubPointDesc.Modified = false;
                SaveStateLabel_Font();
            }
        }

        //// ------------------------------------------------------

        private void SaveStateLabel_Font()
        {
            if (txtShortDesc.Modified || txtLongDesc.Modified || txtMainPointDesc.Modified || txtSubPointDesc.Modified)
            {
                saveStateLabel.Text = "Änderungen sind noch nicht gespeichert";
                saveStateLabel.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                saveStateLabel.Text = "alles gespeichert";
                saveStateLabel.ForeColor = System.Drawing.Color.Green;
            }
        }

        private bool checkUsedPlaceholder()
        {
            /*
             * check if the used placeholder are correct
             */
            int errorCount = 0;
            // shortDesc
            if (TextBoxUseAllowedPlaceholder(txtShortDesc, shortDescPlaceholder))
            {
                txtShortDesc.ForeColor = Color.Black;
            }
            else
            {
                txtShortDesc.ForeColor = Color.Red;
                errorCount++;
            }
            // longDesc
            if (TextBoxUseAllowedPlaceholder(txtLongDesc, longDescPlaceholder))
            {
                txtLongDesc.ForeColor = Color.Black;
            }
            else
            {
                txtLongDesc.ForeColor = Color.Red;
                errorCount++;
            }
            //// MainPointDesc
            if (TextBoxUseAllowedPlaceholder(txtMainPointDesc, mainPointDescPlaceholder))
            {
                txtMainPointDesc.ForeColor = Color.Black;
            }
            else
            {
                txtMainPointDesc.ForeColor = Color.Red;
                errorCount++;
            }
            //// SubPointDesc
            if (TextBoxUseAllowedPlaceholder(txtSubPointDesc, subPointDescPlaceholder))
            {
                txtSubPointDesc.ForeColor = Color.Black;
            }
            else
            {
                txtSubPointDesc.ForeColor = Color.Red;
                errorCount++;
            }

            if (errorCount != 0)
            {
                return false;
            }
            else
            {
                return true;
            }            
        }

        private bool TextBoxUseAllowedPlaceholder(TextBox txtBox, string[][] placeholder)
        {
            string[] allWordsFromText = Regex.Split(txtBox.Text, " ");
            int contains = 0;
            for (int i = 0; i < allWordsFromText.Length; i++)
            {
                if (allWordsFromText[i].Contains("#"))
                {
                    contains ++;
                    //there musst be a Placeholder
                    allWordsFromText[i] = allWordsFromText[i].Replace(".", "");
                    allWordsFromText[i] = allWordsFromText[i].Replace(",", "");
                    allWordsFromText[i] = allWordsFromText[i].Replace(":", "");
                    allWordsFromText[i] = allWordsFromText[i].Replace("'", "");
                    if (placeholder != null)
                    {
                        for (int n = 0; n < placeholder.Length; n++)
                        {
                            if (placeholder[n][0].Contains(allWordsFromText[i]))
                            {
                                contains--;
                            }
                        }
                    }
                }
            
            }
            if(contains == 0){
                return true;
            }else{
                return false;
            }
        }

        private bool TextboxHasContent()
        {
            int errorCount = 0;

            if (txtShortDesc.Text == "")
            {
                shortDescInfo.ForeColor = Color.Red;
                errorCount++;
            }
            else
            {
                shortDescInfo.ForeColor = Color.Black;
            }

            if (txtLongDesc.Text == "")
            {
                longDesc1.ForeColor = Color.Red;
                errorCount++;
            }
            else
            {
                longDesc1.ForeColor = Color.Black;
            }

            if (txtMainPointDesc.Text == "")
            {
                longDesc2.ForeColor = Color.Red;
                errorCount++;
            }
            else
            {
                longDesc2.ForeColor = Color.Black;
            }

            if (txtSubPointDesc.Text == "")
            {
                longDesc3.ForeColor = Color.Red;
                errorCount++;
            }
            else
            {
                longDesc3.ForeColor = Color.Black;
            }

            if (errorCount != 0)
            {
                return false;
            }
            else
            {
                return true;
            }        
        }

        private void descSaveButton_Click(object sender, EventArgs e)
        {
            /*
             * 1. check if any textbox is empty
             * 2. check if the used placeholder right
             * 3. insert the new content
             */
            XmlWriter xmlWriter = XmlWriter.GetInstance;
            string errorMessage = "";
            // 1. step
            if (TextboxHasContent())
            {
                // 2. step
                if (checkUsedPlaceholder())
                {
                    // 3. step
                    if (GetFoundDescription())
                    {
                        /*
                         * Starts only if the XML contains the old description
                         */
                        if (saveStateLabel.ForeColor == System.Drawing.Color.Red)
                        {
                            if (txtShortDesc.Modified)
                            {
                                if (!(xmlWriter.ChangeShortDescInnerText(GetCategory(), GetSubCategory(), txtShortDesc.Text)))
                                {
                                    errorMessage = "Die Kurzbeschreibung konnte nicht abgespeichert werden";
                                }
                                else
                                {
                                    txtShortDesc.Modified = false;
                                }
                            }
                            if (txtLongDesc.Modified)
                            {
                                if (!(xmlWriter.ChangeLongDescInnerText(GetCategory(), GetSubCategory(), "description", txtLongDesc.Text)))
                                {
                                    errorMessage += "Die Langbeschreibung konnte nicht abgespeichert werden";
                                }
                                else
                                {
                                    txtLongDesc.Modified = false;
                                }
                            }
                            if (txtMainPointDesc.Modified)
                            {
                                if (!(xmlWriter.ChangeLongDescInnerText(GetCategory(), GetSubCategory(), "eachMainPoint", txtMainPointDesc.Text)))
                                {
                                    errorMessage += "Die Hauptpunktbeschreibung konnte nicht abgespeichert werden";
                                }
                                else
                                {
                                    txtMainPointDesc.Modified = false;
                                }
                            }
                            if (txtSubPointDesc.Modified)
                            {
                                if (!(xmlWriter.ChangeLongDescInnerText(GetCategory(), GetSubCategory(), "eachSubPoint", txtSubPointDesc.Text)))
                                {
                                    errorMessage += "Die Unterpunktbeschreibung konnte nicht abgespeichert werden";
                                }
                                else
                                {
                                    txtSubPointDesc.Modified = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        /*
                         * starts only if the XML does not contains the special SmartArtDescription
                         */
                        //// Tab Shortdescription ------
                        SetShortDescription(txtShortDesc.Text);

                        //// Tab Longdescription -------
                        SetLongDescription(txtLongDesc.Text);
                        SetMainPointDescription(txtMainPointDesc.Text);
                        SetSubPointDescription(txtSubPointDesc.Text);

                        // creat a new XML part
                        if (!(xmlWriter.InsertNewSubCategory(GetCategory(), GetSubCategory(), GetShortDescription(), GetLongDescription(), GetMainPointDescription(), GetSubPointDescription())))
                        {
                            errorMessage += "Beim Anlegen der neuen XML-Knoten ist was schief gegangen";
                        }
                        txtShortDesc.Modified = false;
                        txtLongDesc.Modified = false;
                        txtMainPointDesc.Modified = false;
                        txtSubPointDesc.Modified = false;

                    }
                }
                else
                {
                    errorMessage += "Mit den Platzhaltern stimmt etwas nicht. \n Bitte kontrollieren Sie ihre Eingaben.";
                }
            }
            else
            {
                errorMessage += "Sie haben nicht alle Textfelder ausgefüllt. \n Bitte füllen Sie alle Felder.";
            }
            //--------------------------------------    
                if (errorMessage != "")
                {
                    MessageBox.Show(errorMessage, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SaveStateLabel_Font();
                }else{
                    SaveStateLabel_Font();
                }
        }

        private void deleteDesc_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Möchten Sie die gesamte Beschreibung für diese Smartart-Grafik unwiederruflich löschen?", "Beschreibung löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                //Delete the description from XML
                XmlWriter xmlWriter = XmlWriter.GetInstance;
                xmlWriter.DeleteSmartArtDescription(GetCategory(), GetSubCategory());
                //Call "ListAllSmartArts"-Form to reload
                ListAllSmartArts.GetInstanz.ReloadTreeView();
                //Close the window
                disposHappen = true;
                DescFormManager formManger = DescFormManager.GetInstanz;
                formManger.CloseDescForm(GetSubCategory());
                Close();
            }
        }

// this attribut is importen for the "Despose"-method ---------------
        bool disposHappen = true;
// i did not find a better way ------------------------

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            /*
             * the "DescriptionFormManager" have to delet that form from the list
             */

            if (saveStateLabel.ForeColor == System.Drawing.Color.Red)
            {
                var result = MessageBox.Show("Ihre Änderungen wurden nocht nicht gespeichert und gehen automatisch verloren. \n Möchten Sie dennoch die Maske schließen?", "Ohne Speichern schließen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    disposHappen = true;
                    DescFormManager formManger = DescFormManager.GetInstanz;
                    formManger.CloseDescForm(GetSubCategory());
                    base.OnFormClosed(e);
                }
                else
                {
                    disposHappen = false;
                    return;
                }
            }
            else {
                disposHappen = true;
                DescFormManager formManger = DescFormManager.GetInstanz;
                formManger.CloseDescForm(GetSubCategory());
                base.OnFormClosed(e);
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposHappen)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
        }

        //Getter and Setter ///////////////////////////////////////////////
        public void SetSubCategory(string name)
        {
            this.subCategory = name;
        }
        public string GetSubCategory()
        {
            return subCategory;
        }

        public void SetCategory(string name)
        {
            this.category = name;
        }
        private string GetCategory()
        {
            return this.category;
        }

        public void SetStartDescription(string description)
        {
            this.startDescription = description;
        }
        private string GetStartDescription()
        {
            return this.startDescription;
        }

        public void SetShortDescription(string description)
        {
            this.shortDescription = description;
        }
        private string GetShortDescription()
        {
            return this.shortDescription;
        }

        public void SetLongDescription(string description)
        {
            this.longDescription = description;
        }
        private string GetLongDescription()
        {
            return this.longDescription;
        }

        public void SetMainPointDescription(string description)
        {
            this.mainPointDescription = description;
        }
        private string GetMainPointDescription()
        {
            return this.mainPointDescription;
        }

        public void SetSubPointDescription(string description)
        {
            this.subPointDescription = description;
        }
        private string GetSubPointDescription()
        {
            return this.subPointDescription;
        }

        public void SetFoundDescription(bool found)
        {
            this.foundDescription = found;
        }
        private bool GetFoundDescription()
        {
            return this.foundDescription;
        }

        public void SetShortDescPlaceholder(string[][] newArray)
        {
            this.shortDescPlaceholder = newArray;
        }
        public void SetLongDescPlaceholder(string[][] newArray)
        {
            this.longDescPlaceholder = newArray;
        }
        public void SetMainPointDescPlaceholder(string[][] newArray)
        {
            this.mainPointDescPlaceholder = newArray;
        }
        public void SetSubPointDescPlaceholder(string[][] newArray)
        {
            this.subPointDescPlaceholder = newArray;
        }
    }
}
