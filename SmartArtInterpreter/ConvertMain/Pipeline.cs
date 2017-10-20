using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Office = Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using SmartArtInter = SmartArtInterpreter.ShapeInterpreter.SmartArtInterpreter;
using MDFormConverter = SmartArtInterpreter.Converter.MDFormConverter;
using System.Drawing;
using Microsoft.Office.Interop.PowerPoint;

namespace SmartArtInterpreter.ConvertMain
{
    class Pipeline
    {
        /*
         * create the documents and the images in the special folder
         * 1.   set all paths
         *          a.  creat a new image folder if not existing
         * 2.   delete existing "smartart"-images
         * 3.   get each SmartArt:
         *          a.  get the short and long description
         *          b.  export the smartartgraphic
         *          c.  get the link for the main document
         * 4.   save all three documents
         *          
         */
        
        // Singleton //////////////////////////
        private static Pipeline _inst = null;
        private Pipeline() { }

        public static Pipeline GetInstanz
        { 
            get
            {
                if (_inst == null)
                {
                    _inst = new Pipeline();
                }
                return _inst;
            }
        }

        ////////////////////////////////////////////

        //TODO: HTML generieren im alten projekt in der ConverterMain.cs

        //attributes ---------------------------------------
        string presentationPath;
        string imagePath;
        protected static string IMAGEFOLDERNAME = "bilder";
        protected static string SMARTARTSHORTDESCFILE = "SmartArtKurzbeschreibungen.md";
        protected static string SMARTARTLONGDESCFILE = "SmartArtLangbeschreibungen.md";
        protected static string MAINFILE = "uebersetzung.md";

        //methods ------------------------------------------
        public void AllSlides(PowerPoint.Presentation presentation)
        {
            /*
             * Find the path
             * Iterat throw all slides
             */
            SetPresentationPath(presentation.Path.ToString());
            SetImagePath(GetImageFolder());
            MDFormConverter MDForm = new MDFormConverter();
            DeleteExistingSmartArtImages();

            PowerPoint.Slides slides = presentation.Slides;
            // just for texting and output 
            string textmain = "";
            string textShortDesc = "";
            string textLongDesc = "";

            foreach (PowerPoint.Slide slide in slides)
            {
                textmain += MDForm.GetNumberform(slide.SlideNumber, true);
                // get all shapes and test for type
                int smartArtNrPerSlide = 0;
                foreach (PowerPoint.Shape element in slide.Shapes)
                {
                    // Documentation of MsoShapeType see https://msdn.microsoft.com/EN-US/library/ms251190
                    switch (element.Type)
                    {
                        case Office.MsoShapeType.msoSmartArt:
                            // Get the Text ------------------------
                            smartArtNrPerSlide ++;
                            SmartArtInter smartArtInter = new SmartArtInter();
                            string title = MDForm.GetSmartArtTitle(element.SmartArt.Layout.Name, slide.SlideNumber, smartArtNrPerSlide);
                            textShortDesc += title+ "\r" +smartArtInter.GetSmartArtShortDescription(element) +"\n";
                            textLongDesc += title + "\r" + smartArtInter.GetSmartArtLongDescription(element) + "\n";
                            
                            // Get the Image -----------------------
                            string imageName = "smartart_" +slide.SlideNumber.ToString()+ "_" +smartArtNrPerSlide.ToString() + ".png";
                            string imagePath = GetImageFolder() + "\\" + imageName;
                            element.Export(imagePath, PpShapeFormat.ppShapeFormatPNG, (int)presentation.PageSetup.SlideWidth , (int)presentation.PageSetup.SlideHeight, PpExportMode.ppScaleToFit);
                            
                            // Get the main Text -------------------
                            string titleID = title.Replace("## ", "");
                            titleID = titleID.Replace(" ", "-");
                            titleID = titleID.Replace("\n", "");
                            titleID = titleID.Replace(":", "");
                            titleID = titleID.ToLower();                         
                            textmain += MDForm.GetLinkToShortDesc(titleID);
                            textmain += MDForm.GetLinkToLongDesc(IMAGEFOLDERNAME + "/" + imageName, titleID);

                            break;
                        default:
                            break;
                    }

                }
                SaveMDINFile(textmain, textShortDesc, textLongDesc);
                //System.Diagnostics.Debug.WriteLine(textShortDesc + textLongDesc);
            }
        }       

        private void SaveMDINFile(string main, string allShortDesc, string allLongDesc)
        {
            /*
             * Save each Description
             */
            System.IO.File.WriteAllText(presentationPath + "\\" + SMARTARTSHORTDESCFILE, allShortDesc);
            System.IO.File.WriteAllText(presentationPath + "\\" + SMARTARTLONGDESCFILE, allLongDesc);
            System.IO.File.WriteAllText(presentationPath + "\\" + MAINFILE, main);
        }

        private string GetImageFolder()
        {
            /*
             * create the imageFolder if not exist
             */
            string imageFolder = GetPresentationPath() + "\\" + IMAGEFOLDERNAME;
            bool exists = System.IO.Directory.Exists(imageFolder);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(imageFolder);
            }
            return imageFolder;
        }

        private void DeleteExistingSmartArtImages()
        {
            string [] allFiles = Directory.GetFiles(GetImagePath());
                if(allFiles != null){
                    foreach(string fileName in allFiles){
                        if(fileName.Contains("smartart")){
                            System.IO.File.Delete(fileName);
                        }
                    }
                }
        }

        // GETTER SETTER ///////////////////////////////////////////
        private void SetPresentationPath(string newPath)
        {
            this.presentationPath = newPath;
        }
        private string GetPresentationPath() 
        {
            return this.presentationPath;
        }

        private void SetImagePath(string newPath)
        {
            this.imagePath = newPath;
        }
        private string GetImagePath()
        {
            return this.imagePath;
        }

    }
}
