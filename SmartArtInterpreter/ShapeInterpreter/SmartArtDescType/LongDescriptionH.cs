using SmartArtInterpreter.Converter;
using SmartArtInterpreter.ShapeInterpreter.SmartArtDescription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Office = Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using ShapeColor = SmartArtInterpreter.Converter.ColorFinder;

namespace SmartArtInterpreter.ShapeInterpreter.SmartArtDescType
{
    class LongDescriptionH
    {
        /*
         * that Class creats the long descriptions for the categories:
         *      hierarchy
         */
        //TODO: Verbesserung: eine "Musterklasse" anlegen für alle Lanbeschreibungen
        //attributes ---------------------------------------
        protected SmartArtInterpreter SInter;
        private string mainPointDesc;
        private string subPointDesc;

        //methods ------------------------------------------
        public LongDescriptionH(SmartArtInterpreter SInter)
        {
            this.SInter = SInter;
        }

        public string GetLongDescription(PowerPoint.Shape smartArtShape)
        {
            //SetColorStyle(smartArtShape.SmartArt.Color.Name);   Methode die zu den Akzenten passende beschreibungen ausgibt

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

        private string ReplacePlaceholder(string text)
        {
            /*
             * for the "2. get the special part"-Step:
             * possible placeholder: #Farbgestaltung#, #AnzahlLevel1Punkte#
             */
            if (SInter.GetCountMainPoints() > 1)
            {
                text = text.Replace("#AnzahlHauptpunke#", SInter.GetCountMainPoints().ToString() + " Oberbegiffe");
            }
            else
            {
                text = text.Replace("#AnzahlHauptpunke#", SInter.GetCountMainPoints().ToString() + " Oberbegiff");
            }        

            return text;
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
            int mainPointNr = 0;
            string description = "";
            Office.SmartArtNodes allNodes = smartArt.SmartArt.AllNodes;
            foreach (Office.SmartArtNode node in allNodes)
            {
                string text = "";
                
                if (node.Level == 1)
                {
                    //MainPoint
                    //Placeholder: #wievielterLevel1Punkt#, #InhaltHauptpunkt#
                    mainPointNr++;
                    if (node.TextFrame2.HasText == Office.MsoTriState.msoTrue)
                    {
                        text = GetMainPointDesc();
                        
                        
                        // #wievielterLevel1Punkt# ---
                        if (SInter.GetCountMainPoints() > 1)
                        {
                            text = text.Replace("#wievielterHP#", mainPointNr.ToString());
                        }
                        else
                        {
                            text = text.Replace("#wievielterHP#", mainPointNr.ToString());
                        }
                        
                        // #InhaltHauptpunkt# ---
                        text = text.Replace("#InhaltHauptpunkt#", node.TextFrame2.TextRange.Text);
                        //text = text.Replace("#InhaltHauptpunkt#", GetStylesFromWord(node.TextFrame2.TextRange));

                        // #Farbe# ----
                        text = GetColorStyleOfNode(node, text);
                    }
                    else
                    {
                        text = "Hauptpunkt" + mainPointNr + "HAT KEINEN TEXT.";
                    }
                }
                else
                {
                    //SubPoint
                    //Placeholder: #LevelNummer#, #InhaltUnterpunkt#, #Leveldarueber#
                    if (node.TextFrame2.HasText == Office.MsoTriState.msoTrue)
                    {
                        text = GetSubPointDesc();
                        // #LevelNummer# ---
                        text = text.Replace("#Level#", node.Level.ToString());
                        // #InhaltUnterpunkt# ---
                        text = text.Replace("#InhaltUnterpunkt#", node.TextFrame2.TextRange.Text);
                        //text = text.Replace("#InhaltUnterpunkt#", GetStylesFromWord(node.TextFrame2.TextRange));
                        // #Leveldarueber# ---
                        text = text.Replace("#Leveldarueber#", (node.Level - 1).ToString());

                        // #Farbe# ----
                        text = GetColorStyleOfNode(node, text);
                    }
                    else
                    {
                        text = "Unterpunkt: HAT KEINEN TEXT.";
                    }
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

        //private string GetStylesFromWord(Office.TextRange2 text)
        //{
        //    MDFormConverter converter = new MDFormConverter();
        //    List<Office.TextRange2> words = new List<Office.TextRange2>();
        //    string paraText = "";
        //    Office.MsoTriState bold = Office.MsoTriState.msoFalse;
        //    Office.MsoTriState italic = Office.MsoTriState.msoFalse;
            
        //    //int count = 0;
        //    //foreach (Office.TextRange2 word in text.Words)
        //    //{
        //    //    words.Add(word);
        //    //}
        //    //if (words.Count > 0)
        //    //{
        //    //    foreach (Office.TextRange2 word in words)
        //    //    {
        //    //        /*
        //    //         * it looks like: "word before""end" "start""word"
        //    //         */
        //    //        string start = "";
        //    //        string end = "";
        //    //        count++;

        //            //TODO: Verbesserung: Links erkennen
        //            //TODO: geht noch nicht
        //    //        if ((word.Font.Bold == Office.MsoTriState.msoFalse)
        //    //            && (word.Font.Italic == Office.MsoTriState.msoFalse))
        //    //        {
        //    //            if ((bold == Office.MsoTriState.msoTrue)
        //    //                && (italic == Office.MsoTriState.msoTrue))
        //    //            {
        //    //                end = converter.GetBoldItalicMark();
        //    //            }
        //    //        }
        //    //        else if ((word.Font.Bold == Office.MsoTriState.msoFalse))
        //    //        {
        //    //            if (bold == Office.MsoTriState.msoTrue)
        //    //            {
        //    //                end = converter.GetBoldMark();
        //    //            }
        //    //        }
        //    //        else if (word.Font.Italic == Office.MsoTriState.msoFalse)
        //    //        {
        //    //            if (italic == Office.MsoTriState.msoTrue)
        //    //            {
        //    //                end = converter.GetItalicMark();
        //    //            }
        //    //        }
        //    //         Set the Start
        //    //        if ((word.Font.Bold == Office.MsoTriState.msoTrue)
        //    //             && (word.Font.Italic == Office.MsoTriState.msoTrue))
        //    //        {
        //    //            if ((bold == Office.MsoTriState.msoFalse)
        //    //                    && (italic == Office.MsoTriState.msoFalse))
        //    //            {
        //    //                  start = converter.GetBoldItalicMark();
        //    //            }
        //    //        }
        //    //        else if (word.Font.Bold == Office.MsoTriState.msoTrue)
        //    //        {
        //    //            if (bold == Office.MsoTriState.msoFalse)
        //    //           {
        //    //                 start = converter.GetBoldMark();
        //    //            }
        //    //        }
        //    //        else if (word.Font.Italic == Office.MsoTriState.msoTrue)
        //    //        {
        //    //            if (italic == Office.MsoTriState.msoFalse)
        //    //            {
        //    //                 start = converter.GetItalicMark();
        //    //            }
        //    //        }
        //    //        paraText = end + " " + start + word.Text;
        //    //        if (count == words.Count)
        //    //        {
        //    //            if ((bold == Office.MsoTriState.msoTrue)
        //    //               && (italic == Office.MsoTriState.msoTrue))
        //    //            {
        //    //                  paraText += converter.GetBoldItalicMark();
        //    //            }
        //    //            else if (bold == Office.MsoTriState.msoTrue)
        //    //            {
        //    //                  paraText += converter.GetBoldMark();
        //    //            }
        //    //            else if (italic == Office.MsoTriState.msoTrue)
        //    //            {
        //    //                  paraText += converter.GetItalicMark();
        //    //            }
        //    //        }
        //    //    }               
        //    //} Variante 2:
        //    //        if (!(word.Font.Bold == Office.MsoTriState.msoFalse)
        //    //                        && !(word.Font.Italic == Office.MsoTriState.msoFalse)
        //    //                        && !(count == words.Count()))
        //    //            {
        //    //                if (!bold && !italic)
        //    //                {
        //    //                    start = converter.GetBoldItalicMark();
        //    //                }
        //    //                else if (italic && !bold)
        //    //                {
        //    //                    start = converter.GetBoldItalicMark();
        //    //                    end = converter.GetItalicMark();
        //    //                }
        //    //                else if (bold && !italic)
        //    //                {
        //    //                    start = converter.GetBoldItalicMark();
        //    //                    end = converter.GetBoldMark();
        //    //                }
        //    //                bold = true;
        //    //                italic = true;
        //    //            }
        //    //            else if (!(word.Font.Bold == Office.MsoTriState.msoFalse) && !(count == words.Count()))
        //    //            {
        //    //                if (!bold)
        //    //                {
        //    //                    if (italic)
        //    //                    {
        //    //                        end = converter.GetItalicMark();
        //    //                    }
        //    //                    start = converter.GetBoldMark();
        //    //                }
        //    //                else if (italic)
        //    //                {
        //    //                    end = converter.GetBoldItalicMark();
        //    //                    start = converter.GetBoldMark();
        //    //                }
        //    //                bold = true;
        //    //                italic = false;
        //    //            }
        //    //            else if (!(word.Font.Italic == Office.MsoTriState.msoFalse) && !(count == words.Count()))
        //    //            {
        //    //                if (!italic)
        //    //                {
        //    //                    if (bold)
        //    //                    {
        //    //                        end = converter.GetBoldMark();
        //    //                    }
        //    //                    start = converter.GetItalicMark();
        //    //                }
        //    //                else if (bold)
        //    //                {
        //    //                    end = converter.GetBoldItalicMark();
        //    //                    start = converter.GetItalicMark();
        //    //                }
        //    //                italic = true;
        //    //                bold = false;
        //    //            }
        //    //            else
        //    //            {
        //    //                if (italic && bold)
        //    //                {
        //    //                    end = converter.GetBoldItalicMark();
        //    //                    italic = false;
        //    //                    bold = false;
        //    //                }
        //    //                else if (bold)
        //    //                {
        //    //                    end = converter.GetBoldMark();
        //    //                    bold = false;
        //    //                }
        //    //                else if (italic)
        //    //                {
        //    //                    end = converter.GetItalicMark();
        //    //                    italic = false;
        //    //                }
        //    //            }
        //    //            paraText += end + start + singleWord;
        //    //        }

        //    //    }
        //    //}
        //    return paraText;
        //}
               
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

    }
}
