using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using System.IO;
using XMLHelper = SmartArtInterpreter.ShapeInterpreter.SmartArtDescription.XMLHelper;
using SmartArtInterpreter.Converter;

namespace SmartArtInterpreter.ShapeInterpreter.SmartArtDescType
{
    /*
     * this class generate the special short description
     * check if the XML contains that smartArt
     * 1. "GetShortDescription": collegt all parts in the correct order
     * 2. "GetStart": read the description-tag and get the shortDescription-tag from the helper-method "GetSpecialDescription"
     * 3. "GetListfromNodes": iterates throw all nodes and gets the content
     * 4. "TranclateAndReplacePlaceholder"
     */

    class ShortDescription : SmartArtInterpreter
    {
        //attributes ---------------------------------------
        protected SmartArtInterpreter SInter;
        private bool existingSubPoints = false;

        //methods ------------------------------------------
        public ShortDescription(SmartArtInterpreter SInter)
        {
            this.SInter = SInter;
            
        }
        
        public string GetShortDescription(PowerPoint.Shape smartArtShape)
        {
            /*
             * this methode collegt all parts
             */ 

            //1. get start 
            string list = GetListfromNodes(smartArtShape);
            
            //2. get middle
            string shortDescription = GetStart();
            shortDescription = TranclateAndReplacePlaceholder(shortDescription);
            SInter.SetShortDescriptionfirstPart(shortDescription);

            //3. get end
            string shortDescriptonEnd = GetSpecialDescription();
            shortDescriptonEnd = TranclateAndReplacePlaceholder(shortDescriptonEnd);
            shortDescriptonEnd = System.Text.RegularExpressions.Regex.Replace(shortDescriptonEnd, "  +", "");

            return shortDescription + shortDescriptonEnd + "\n\n" + list;
        }

        private string GetStart()
        {
            /*
             * get the "descriptionStart" and the "shortDescription" from the xml
             * helper-methode is "GetSpecialDescription"
             */
            XMLHelper xmlHelper = XMLHelper.getInstance;
            string descriptionStart = xmlHelper.GetStartDescription();

            return descriptionStart;
        }

        private string GetSpecialDescription()
        {
            /*
             * 1. look for the CategoryNode
             * 2. if found: look for the SubCategoryNode in the Childnodes
             * 3. get the description
             */
            XMLHelper xmlHelper = XMLHelper.getInstance;
            return xmlHelper.GetSpecialShortDescription(SInter.GetCategory(), SInter.GetSubCategory());
        }
       
        private string GetListfromNodes(PowerPoint.Shape smartArtShape)
        {
            /*
             * creat a list of the content from the nodes with the MDFormConverter
             * set the "existingSubPoint"
             * set the "mainPoints"
             */
            string list = "";
            int mainPoints = 0;
            int subPoints = 0;
            MDFormConverter mdForm = new MDFormConverter();
            Office.SmartArtNodes allNodes = smartArtShape.SmartArt.AllNodes;

            foreach (Office.SmartArtNode node in allNodes)
            {
                if (node.Hidden != Office.MsoTriState.msoTrue)
                {
                    if (node.Level > 1)
                    {
                        SetExistingSubPoints(true);
                        subPoints++;
                        // get the space before each point
                        list += mdForm.GetSpaceBefore(node.Level);
                    }
                    else
                    {
                        mainPoints++;
                        subPoints = 0;
                    }
                    // get the bullet type
                    switch (SInter.GetCategory())
                    {
                        case "process":
                            if (subPoints != 0)
                            {
                                list += mdForm.GetOrderdList(subPoints);
                            }
                            else
                            {
                                list += mdForm.GetOrderdList(mainPoints);
                            }
                            break;
                        default:
                            list += mdForm.GetLineList();
                            break;
                    }
                    // get the content
                    SInter.SetCountMainPoints(mainPoints);
                    list += node.TextFrame2.TextRange.Text + "\n";
                }
            }
            return list;
        }

        private string TranclateAndReplacePlaceholder(string text)
        {
            /*
             * the method tranclate and replace the Placeholder:
             *      #Kategorie#, #Unterkategorie#, #AnzahlHauptpunkte#, #mit/ohneUP# 
             * "textCategory" ist importen to save the german word and to add it to the text 
             */
            string textCategory = XMLHelper.getInstance.TranslateCategory(SInter.GetCategory());
            text = text.Replace("#Kategorie#", textCategory);
            text = text.Replace("#Unterkategorie#", SInter.GetSubCategory());
            text = text.Replace("#AnzahlHauptpunkte#", SInter.GetCountMainPoints().ToString());
            if (existingSubPoints)
            {
                text = text.Replace("#mit/ohneUP#", "mit");
            }
            else
            {
                text = text.Replace("#mit/ohneUP#", "ohne");
            }
            return text;
        }

        //Getter and Setter ///////////////////////////////////////////////
        private void SetExistingSubPoints(bool exist)
        {
            this.existingSubPoints = exist;
        }
        private bool GetExistingSubPoints()
        {
            return this.existingSubPoints;
        }
    }
}
