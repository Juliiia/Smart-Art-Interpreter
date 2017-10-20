using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using XMLHelper = SmartArtInterpreter.ShapeInterpreter.SmartArtDescription.XMLHelper;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using System.Windows.Forms;
using Pipeline = SmartArtInterpreter.ConvertMain.Pipeline;
using ListAllSmartArts = SmartArtInterpreter.MessageBoxDesc.ListAllSmartArts;
using System.Text.RegularExpressions;
using DescFormManager = SmartArtInterpreter.MessageBoxDesc.DescriptionFormManager;


namespace SmartArtInterpreter.SmartArtRibbon
{
    public partial class SmartArtConverter
    {
        private void SmartArtConverter_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void kontrolleStart_Click(object sender, RibbonControlEventArgs e)
        {
            /*
             * 1. ask "XMLHelper": Does the XML contains all SmartArts?
             * 2.   Yes: Alert: "all ok" and ask for continue
             *      No: Alert: SmartArt which is not found and ask for insert the SmartArt
             */
            XMLHelper controlXML = XMLHelper.getInstance;
            bool folder = controlXML.LoadAllSmartArtImagesInHiddenFolder(Globals.ThisAddIn.Application.ActivePresentation);
            while (!(folder == true)) { }
            string[] notExistingDescs = controlXML.XMLContainsAllSmartArts(Globals.ThisAddIn.Application.ActivePresentation);
            //return: a array with the elements: "SlideNr","subCategory","category"
            if (notExistingDescs == null)
            {
                /*
                 * if the XML contains all SmartArts
                 */
                var result = MessageBox.Show("Alle SmartArts besitzen eine Beschreibung. \n Möchten Sie mit der Konvertierung beginnen?", 
                    "Alles da", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    /*
                     * delete the hidden folder and start the process
                     */
                    controlXML.DeleteHiddenFolder(Globals.ThisAddIn.Application.ActivePresentation);
                    Pipeline pipeline = Pipeline.GetInstanz;
                    pipeline.AllSlides(Globals.ThisAddIn.Application.ActivePresentation);
                }
            }
            else
            {
                /*
                 * if the XML does not contain all SmartArts
                 */
                var result = MessageBox.Show("Es fehlen die Beschreibungen für: \n\n" + GetListViewForm(notExistingDescs) + "\n Möchten Sie die fehlende Beschreibungen sofort anlegen?",
                    "Es fehlt was", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    /*
                     * Open the "DescriptionForm" MessageBox(s) 
                     */
                    DescFormManager formManager = DescFormManager.GetInstanz;
                    foreach (string part in notExistingDescs)
                    {
                        String[] eachSmartArtInfo = Regex.Split(part, ",");
                        //eachSmartArtInfo[0] = "SlideNr"
                        //eachSmartArtInfo[1] = "smartArtPerSlide"
                        //eachSmartArtInfo[2] = "subCategory"
                        //eachSmartArtInfo[3] = "category"
                        formManager.GetDescForm(eachSmartArtInfo[3], eachSmartArtInfo[2]);
                    }
                }
            }
        }


        private void SmartArtDesc_Click(object sender, RibbonControlEventArgs e)
        {
            XMLHelper xmlHelper = XMLHelper.getInstance;
            xmlHelper.LoadAllSmartArtImagesInHiddenFolder(Globals.ThisAddIn.Application.ActivePresentation);
            ListAllSmartArts box = ListAllSmartArts.GetInstanz;
            box.ShowDialog();
        }

        // HelperMethod ////////////////////////////////////////////////////

        private string GetListViewForm(string[] array)
        {
            /*
             * creat a string-list to display the elements in the messagebox
             */
            string list = "";
            foreach (string line in array)
            {
                string[] lineParts = Regex.Split(line, ",");
                list += "Folie " +lineParts[0]+ " Grafik " +lineParts[1]+ ": " +lineParts[2]+ " (" +lineParts[3]+ ") \n";
            }
            return list;
        }

        private void openFolderButton_Click(object sender, RibbonControlEventArgs e)
        {
            System.Diagnostics.Process.Start(Globals.ThisAddIn.Application.ActivePresentation.Path.ToString());
            
        }
    }
}
