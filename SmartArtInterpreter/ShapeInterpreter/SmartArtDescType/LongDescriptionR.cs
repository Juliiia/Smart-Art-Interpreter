using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using XMLHelper = SmartArtInterpreter.ShapeInterpreter.SmartArtDescription.XMLHelper;
using System.Text.RegularExpressions;
using System.Drawing;
using ShapeColor = SmartArtInterpreter.Converter.ColorFinder;

namespace SmartArtInterpreter.ShapeInterpreter.SmartArtDescType
{
    class LongDescriptionR
    {
        //attributes ---------------------------------------
        protected SmartArtInterpreter SInter;
        //private string colorStyle; TODO: kann evtl. entfernt werden auch mit Getter und Setter
        private string mainPointDesc;
        private string subPointDesc;

        //methods ------------------------------------------
        public LongDescriptionR(SmartArtInterpreter SInter)
        {
            this.SInter = SInter;
        }

        public string GetLongDescription(PowerPoint.Shape smartArtShape)
        {
            /*
             * this methode collegt all parts
             * completLongDescription = speicalDescription + endDescription
             * endDescription contains the description of each Point
             */
            // 1. get the first part without paceholder
            string firstPartDescription = SInter.GetShortDescriptionfirstPart();
            
            // 2. get the special part 
            string specialDescription = GetContentFromXML();
            
            // 3. get each listpoint
            string endDescription = GetDescrForEachPoint(smartArtShape);
            return firstPartDescription + specialDescription + "\n\n" + endDescription;
        }

        private string GetContentFromXML(){
            /*
             * get the complet content
             * 1. get the content of the "<description>"-Tag
             * 2. get the content of the "<eachMainPoint>"-Tag
             * 3. get the content of the "<eachSubPoint>"-Tag
             */

            XMLHelper xmlhelper = XMLHelper.getInstance;

            string specialDescription = xmlhelper.GetSpecialLongDescription(SInter.GetCategory(), SInter.GetSubCategory());
            if (specialDescription.Contains(";"))
            {
                string[] specialDescArray = Regex.Split(specialDescription, ";");
                SetMainPointDesc(specialDescArray[1]);
                SetSubPointDesc(specialDescArray[2]);
                return specialDescArray[0];
            }
            else
            {
                //ther musst be a errormessage
                return specialDescription;
            }
        }

        private string GetDescrForEachPoint(PowerPoint.Shape smartArt){
            /*
             * Creat the description for each node
             * 1. get node level
             * 2. has Text?
             * 3. get node content
             * 4. get node color
             */
            System.Diagnostics.Debug.WriteLine(SInter.GetSubCategory());
            int pointNr = 0;
            string description = "";

            Office.SmartArtNodes allNodes = smartArt.SmartArt.AllNodes;       
            foreach (Office.SmartArtNode node in allNodes)
            {
                if (node.Hidden != Office.MsoTriState.msoTrue)
                {
                    string text = "";
                    if (node.Level == 1)
                    {
                        //MainPoint
                        pointNr++;
                        if (node.TextFrame2.HasText == Office.MsoTriState.msoTrue)
                        {
                            text = GetMainPointDesc();
                            text = text.Replace("#wievielterHP#", pointNr.ToString());
                            System.Diagnostics.Debug.WriteLine(node.TextFrame2.TextRange.Text);
                            text = GetColorStyleOfNode(node, text);

                            text = text.Replace("#InhaltHauptpunkt#", node.TextFrame2.TextRange.Text);
                            if ((pointNr % 2) == 1)
                            {
                                text = text.Replace("#links/rechts#", "links");
                            }
                            else
                            {
                                text = text.Replace("#links/rechts#", "rechts");
                            }
                        }
                    }
                    else
                    {
                        //SubPoint
                        if (node.TextFrame2.HasText == Office.MsoTriState.msoTrue)
                        {
                            text = GetSubPointDesc();
                            text = text.Replace("#Level#", node.Level.ToString());
                            text = text.Replace("#InhaltUnterpunkt#", node.TextFrame2.TextRange.Text);
                            if (text.IndexOf("#Farbe#") > 0)
                            {
                                text = GetColorStyleOfNode(node, text);
                            }
                        }
                    }
                    text = System.Text.RegularExpressions.Regex.Replace(text, "\r\n", "");
                    description += text + "\n";
                }
            }
            return description;
        }

        private string GetColorStyleOfNode(Office.SmartArtNode node, string text)
        {
            try
            {
                switch (node.Shapes.Fill.Type)
                {
                    case Office.MsoFillType.msoFillTextured:
                        //Texture
                        //System.Diagnostics.Debug.WriteLine("Textur Type: " + node.Shapes.Fill.TextureType);
                        text = text.Replace("#Farbe#", "texturierten");
                        break;
                    case Office.MsoFillType.msoFillSolid:
                        //one Backgroundcolor                  
                        string colorName = ShapeColor.GetColorName(node.Shapes.Fill.ForeColor.RGB);
                        text = text.Replace("#Farbe#", colorName);

                        break;
                    case Office.MsoFillType.msoFillGradient:
                        //two and more Backgroundcolors
                        switch (node.Shapes.Fill.GradientStyle)
                        {
                            case Office.MsoGradientStyle.msoGradientHorizontal:
                                //System.Diagnostics.Debug.WriteLine("horizontaler Farbverlauf");
                                text = text.Replace("#Farbe#", "horizontal farbverlaufenden");
                                break;
                            case Office.MsoGradientStyle.msoGradientVertical:
                                //System.Diagnostics.Debug.WriteLine("vertikaler Farbverlauf");
                                text = text.Replace("#Farbe#", "vertikal farbverlaufenden");
                                break;
                            default:
                                //System.Diagnostics.Debug.WriteLine("zwei oder mehrfarbig: " + node.Shapes.Fill.GradientStyle);
                                text = text.Replace("#Farbe#", "farbverlaufenden");
                                break;
                        }
                        break;
                    case Office.MsoFillType.msoFillPatterned:
                        //TODO: Verbesserung: Musterart erkennen und übersetzen
                        //if (node.Shapes.Fill.Pattern.ToString().IndexOf("Percent") > 0)
                        //{
                        //    System.Diagnostics.Debug.WriteLine("im Hintergrund liegt ein Punktmuster");
                        //}
                        //System.Diagnostics.Debug.WriteLine(node.Shapes.Fill.BackColor.Type);
                        text = text.Replace("#Farbe#", "gemusterten");
                        break;
                }
            }catch(Exception e){
                text = "";
            }
            return text;
        }

        //Getter and Setter ///////////////////////////////////////////////
        private void SetMainPointDesc(string desc){
            desc = System.Text.RegularExpressions.Regex.Replace(desc, "  +", "");
            this.mainPointDesc = desc;
        }
        private string GetMainPointDesc()
        {
            return mainPointDesc;
        }

        private void SetSubPointDesc(string desc)
        {
            desc = System.Text.RegularExpressions.Regex.Replace(desc, "  +", "");
            this.subPointDesc = desc;
        }
        private string GetSubPointDesc()
        {
            return subPointDesc;
        }

    }
}
