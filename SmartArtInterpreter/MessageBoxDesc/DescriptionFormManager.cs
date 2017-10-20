using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Office = Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using DescForm = SmartArtInterpreter.MessageBoxDesc.DescriptionForm;
using XMLHelper = SmartArtInterpreter.ShapeInterpreter.SmartArtDescription.XMLHelper;
using System.IO;

namespace SmartArtInterpreter.MessageBoxDesc
{
    /*
     * Manage all DescriptionForms
     * create a new DescriptionForm for a special subCategory if does not exist
     * Fill in the existing content
     */
    class DescriptionFormManager
    {
        // Singelton //////////////////////////////////
        private static DescriptionFormManager _inst = null;

        private DescriptionFormManager() {
            xmlHelper = XMLHelper.getInstance;
        }

        public static DescriptionFormManager GetInstanz
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new DescriptionFormManager();
                }
                return _inst;
            }
        }
        ///////////////////////////////////////////////
        //attributes ---------------------------------------
        IList<DescForm> allForms = new List<DescForm>();
        XMLHelper xmlHelper;

        //method -------------------------------------------
        public void GetDescForm(string category, string subCategory)
        {
            /*
             * call that methode if the XML does not contains the description
             */
            foreach (DescForm form in allForms)
            {
                if (form.GetSubCategory() == subCategory)
                {
                    form.BringToFront();
                    return;
                }
            }
            DescForm newForm = new DescForm();
            FillForm(newForm, category, subCategory);
            newForm.LoadImagesFlowLayoutPanel(FindImagePath(subCategory));
            allForms.Add(newForm);
            newForm.Show();
        }

        public void GetDescForm(string subCategory)
        {
            /*
             * if the description exsist 
             */
            //TODO: bild laden einbauen
            foreach (DescForm form in allForms)
            {
                if (form.GetSubCategory() == subCategory)
                {
                    form.BringToFront();
                    return;
                }
            }
            DescForm newForm = new DescForm();
            FillFormWithXML(newForm, subCategory);
            newForm.LoadImagesFlowLayoutPanel(FindImagePath(subCategory));
            allForms.Add(newForm);
            newForm.Show();
        }

        //TODO: Load Image box
        private void FillFormWithXML(DescForm newForm, string subCategory)
        {
            /*
             * If the XML does contains that special SmartArt
             * ask the "XMLHelper" and fill the form with the content
             */
            
            string category = xmlHelper.GetCategoryFromSubCategory(subCategory);
            string[] longDescParts = Regex.Split(xmlHelper.GetSpecialLongDescription(category, subCategory), ";");
            newForm.SetCategory(category);
            newForm.SetSubCategory(subCategory);
            newForm.SetFoundDescription(true);
            //descType have to be one of that: "startDescription", "longDesciption", "MainPointDescription", "SubPointDescription"
            newForm.SetLongDescPlaceholder(xmlHelper.GetListOfPlaceholderWithInfo(category, "shortDesciption"));
            newForm.SetLongDescPlaceholder(xmlHelper.GetListOfPlaceholderWithInfo(category, "longDesciption"));
            newForm.SetMainPointDescPlaceholder(xmlHelper.GetListOfPlaceholderWithInfo(category, "MainPointDescription"));
            newForm.SetSubPointDescPlaceholder(xmlHelper.GetListOfPlaceholderWithInfo(category, "SubPointDescription"));

            string startDescription = xmlHelper.GetStartDescription();
            startDescription = startDescription.Replace("#Kategorie#", category);
            startDescription = startDescription.Replace("#Unterkategorie#", subCategory);
            newForm.SetStartDescription(startDescription);

            // shortDesc Tab ------------------------------
            newForm.SetShortDescription(xmlHelper.GetSpecialShortDescription(category, subCategory));

            // longDesc Tab -------------------------------
            if (longDescParts.Length == 3)
            {
                newForm.SetLongDescription(longDescParts[0]);
                newForm.SetMainPointDescription(longDescParts[1]);
                newForm.SetSubPointDescription(longDescParts[2]);
            }
        } 

        private void FillForm(DescForm newForm, string category, string subCategory)
        {
            /*
             * If the XML does not contains that special SmartArt
             */
            newForm.SetCategory(category);
            newForm.SetSubCategory(subCategory);
            newForm.SetFoundDescription(false);
            //descType have to be one of that: "startDescription", "longDesciption", "MainPointDescription", "SubPointDescription"
            newForm.SetLongDescPlaceholder(xmlHelper.GetListOfPlaceholderWithInfo(category, "shortDesciption"));
            newForm.SetLongDescPlaceholder(xmlHelper.GetListOfPlaceholderWithInfo(category, "longDesciption"));
            newForm.SetMainPointDescPlaceholder(xmlHelper.GetListOfPlaceholderWithInfo(category, "MainPointDescription"));
            newForm.SetSubPointDescPlaceholder(xmlHelper.GetListOfPlaceholderWithInfo(category, "SubPointDescription"));

            string startDescription = xmlHelper.GetStartDescription();
            startDescription = startDescription.Replace("#Kategorie#", category);
            startDescription = startDescription.Replace("#Unterkategorie#", subCategory);
            newForm.SetStartDescription(startDescription);
        }

        private List<string> FindImagePath(string subCategory)
        {
            /*
             * Set the picture in the PictureBox
             * 1. such the folder with the actuell presentation
             * 2. such the "Bilder" folder
             * 3. such the special picture mit dem Namen ("smartart_" +slide.SlideNumber.ToString()+ "_" +smartArtNrPerSlide.ToString() + ".png";)
             */
            string presentationFolderPath = Globals.ThisAddIn.Application.ActivePresentation.Path.ToString();
            string presentationImageFolderPath = Path.Combine(presentationFolderPath, "ImagesToDescrip");
            
            string[] images = Directory.GetFiles(presentationImageFolderPath, "*.png");
                List<string> foundImages = new List<string>();
                for (int i = 0; i < images.Length; i++)
                {
                    if (images[i].IndexOf(subCategory) > 0)
                    {
                        foundImages.Add(images[i]);
                    }
                }
                return foundImages;
        }

        public void CloseDescForm(string subCategory)
        {
            foreach (DescForm form in allForms)
            {
                if (form.GetSubCategory() == subCategory)
                {
                    allForms.Remove(form);
                    return;
                }
            }
        }

    }
}
