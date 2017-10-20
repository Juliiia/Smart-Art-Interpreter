using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using SmartArt = SmartArtInterpreter.ShapeInterpreter.SmartArtInterpreter;

namespace SmartArtInterpreter
{
    /*
     * entry-point
     * that class gets all slides select the shapes
     * and starts the spacial prozess for each spacial shape
     */
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            //this.Application.SlideShowNextSlide += Application_StartConverter;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

   /*
        public void Application_StartConverter(PowerPoint.SlideShowWindow Wn)
        {
            // actual shown slide
            PowerPoint.Slide slide = Wn.View.Slide;
            // just for texting and output 
            string text = "";
            text += slide.SlideNumber + "-------------------------------- \n";
            // get all shapes and test for type
            foreach (PowerPoint.Shape element in slide.Shapes)
            {
                // Documentation of MsoShapeType see https://msdn.microsoft.com/EN-US/library/ms251190
                switch (element.Type)
                {
                    case Office.MsoShapeType.msoTextBox:
                                  
                        break;
                    case Office.MsoShapeType.msoPicture:
                        
                        break;
                    case Office.MsoShapeType.msoSmartArt:
                        SmartArt smartArtInter = new SmartArt();
                        text += smartArtInter.GetSmartArtCompleteDescription(element);
                        break;
                    case Office.MsoShapeType.msoTable:
                        
                        break;
                    case Office.MsoShapeType.msoDiagram:
                        
                        break;
                    case Office.MsoShapeType.msoPlaceholder:
                        //ask for SmartArts

                        break;
                    default:
                        break;
                }

            }
            System.Diagnostics.Debug.WriteLine(text);
        }
*/
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
    }
}
