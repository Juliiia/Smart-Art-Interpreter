using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using XMLHelper = SmartArtInterpreter.ShapeInterpreter.SmartArtDescription.XMLHelper;
using System.Text.RegularExpressions;
using ShapeColor = SmartArtInterpreter.Converter.ColorFinder;

namespace SmartArtInterpreter.ShapeInterpreter.SmartArtDescType
{
    class LongDescriptionM
    {
        /*
         * that Class creats the long descriptions for the categories:
         *      matrix
         */
        //attributes ---------------------------------------
        protected SmartArtInterpreter SInter;
        //private string colorStyle; TODO: kann evtl. enftern werden auch mit getter und Setter
        private string mainPointDesc;
        private string subPointDesc;

        //methods ------------------------------------------
        public LongDescriptionM(SmartArtInterpreter SInter)
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
            //SetColorStyle(smartArtShape.SmartArt.Color.Name);   

            // 1. get the first part without paceholder
            string firstPartDescription = SInter.GetShortDescriptionfirstPart();

            // 2. get the special part 
            string specialDescription = GetContentFromXML();
            specialDescription = ReplacePlaceholder(specialDescription);

            // 3. get each listpoint
            string endDescription = GetDescrForEachPoint(smartArtShape);

            return firstPartDescription + specialDescription + "\n\n" + endDescription;
        }

        private string GetContentFromXML()
        {
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

        private string GetDescrForEachPoint(PowerPoint.Shape smartArt)
        {
            /*
             * Creat the description for each node
             * 1. get node level
             * 2. has Text?
             * 3. get node content
             * 4. get node color
             */
            //TODO: Bilder behandeln
            int subPointNr = 0;
            int mainPointNr = 0;
            string description = "";
            Office.SmartArtNodes allNodes = smartArt.SmartArt.AllNodes;
            foreach (Office.SmartArtNode node in allNodes)
            {
                string text = "";
                switch (node.Level)
                {
                    case 1:
                        //MainPoint
                        if (node.TextFrame2.HasText == Office.MsoTriState.msoTrue)
                        {
                            mainPointNr++;
                            text = GetMainPointDesc();
                            text = text.Replace("#InhaltHauptpunkt#", node.TextFrame2.TextRange.Text);
                            if (text.Contains("#XYposition#") && SInter.GetCountMainPoints() == 4)
                            {
                                switch (mainPointNr)
                                {
                                    case 1:
                                        text = text.Replace("#XYposition#", "oben links");
                                        break;
                                    case 2:
                                        text = text.Replace("#XYposition#", "oben rechts");
                                        break;
                                    case 3:
                                        text = text.Replace("#XYposition#", "unten links");
                                        break;
                                    case 4:
                                        text = text.Replace("#XYposition#", "unten links");
                                        break;
                                }
                            }
                            // #Farbe# ----
                            text = GetColorStyleOfNode(node, text);
                        }
                        break;
                    case 2:
                        if (node.TextFrame2.HasText == Office.MsoTriState.msoTrue)
                        {
                            subPointNr++;
                            text = GetSubPointDesc();
                            text = text.Replace("#Level#", "2");
                            if (text.Contains("#XYposition#") && SInter.GetCountMainPoints()==1)
                            {
                                //switch to get the right position
                                switch (subPointNr)
                                {
                                    case 1:
                                        text = text.Replace("#XYposition#", "oben links");
                                        break;
                                    case 2:
                                        text = text.Replace("#XYposition#", "oben rechts");
                                        break;
                                    case 3:
                                        text = text.Replace("#XYposition#", "unten links");
                                        break;
                                    case 4:
                                        text = text.Replace("#XYposition#", "unten links");
                                        break;
                                }
                            }
                            text = text.Replace("#InhaltUnterpunkt#", node.TextFrame2.TextRange.Text);
                        }
                        break;
                    default:
                        text = GetSubPointDesc();
                        text = text.Replace("#Level#", node.Level.ToString()); 
                        text = text.Replace("#XYposition#", "");
                        text = text.Replace("#InhaltUnterpunkt#", node.TextFrame2.TextRange.Text);
                        break;
                }
                text = System.Text.RegularExpressions.Regex.Replace(text, "\r\n", "");
                description += text + "\n";
            }
            return description;
        }

        private string GetColorStyleOfNode(Office.SmartArtNode node, string text)
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
            return text;
        }

        private string ReplacePlaceholder(string text)
        {
            /*
             * for the "2. get the special part"-Step:
             * possible placeholder: 
             */
            

            return text;
        }

        //Getter and Setter ///////////////////////////////////////////////
        private void SetMainPointDesc(string desc)
        {
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

        //private void SetColorStyle(string style)
        //{
        //    this.colorStyle = style;
        //}
        //private string GetColorStyle()
        //{
        //    return colorStyle;
        //}
    }
}
