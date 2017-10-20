using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using System.Text.RegularExpressions;
using XMLSaver = SmartArtInterpreter.ShapeInterpreter.SmartArtDescription.XMLSaver;
using System.IO;
using Microsoft.Office.Interop.PowerPoint;

namespace SmartArtInterpreter.ShapeInterpreter.SmartArtDescription
{
    /*
     * is the helper for the ribbon ans messageBox
     * 
     */
    class XMLHelper
    {       
        // Singleton //////////////////////////
        private static XMLHelper _inst = null;
        private XMLHelper() 
        {
            XMLSaver xmlSaver = XMLSaver.getInstance;
            SetDescriptionXMLPath(xmlSaver.GetDescriptionXMLPath());
            SetPlaceholderAndInfoXMLPath(xmlSaver.GetPlaceholderXMLPath());
        }

        public static XMLHelper getInstance
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new XMLHelper();
                }
                return _inst;
            }
        }
        /////////////////////////////////////////
        //attribute --------------------------------------------
        //protected string descriptionXMLPath = "D:/Dokumente/UNI/7. Semester/Bachelor-Arbeit/SmartArtInterpreter/SmartArtInterpreter/ShapeInterpreter/SmartArtDescription/SmartArtDescription.xml";
        //protected string placeholderAndInfoXMLPath = "D:/Dokumente/UNI/7. Semester/Bachelor-Arbeit/SmartArtInterpreter/SmartArtInterpreter/ShapeInterpreter/SmartArtDescription/PlaceholderAndInfo.xml";
        private string descriptionXMLPath;
        private string placeholderAndInfoXMLPath;

        //methoden --------------------------------------------

        public bool LoadAllSmartArtImagesInHiddenFolder(PowerPoint.Presentation presentation)
        {
            /*
             * Create a hidden Folder to export all SmartArts as image
             * the DescForm use that images to display same at the picturebox
             */

            string hiddenFolderPath = presentation.Path.ToString();
            hiddenFolderPath = Path.Combine(hiddenFolderPath, "ImagesToDescrip");
            if (!Directory.Exists(hiddenFolderPath))
            {
                //Directory.CreateDirectory(hiddenFolderPath);
                DirectoryInfo directory = Directory.CreateDirectory(hiddenFolderPath);
                directory.Attributes = FileAttributes.Hidden;
            }
            else
            {
                Array.ForEach(Directory.GetFiles(hiddenFolderPath), File.Delete);
            }
            foreach (PowerPoint.Slide slide in presentation.Slides)
            {
                foreach (PowerPoint.Shape element in slide.Shapes)
                {
                    int countSmartArtsPerSlide = 0;
                    //-----------------------------
                    if (element.Type == Office.MsoShapeType.msoSmartArt)
                    {
                        countSmartArtsPerSlide++;

                        string subCategory = element.SmartArt.Layout.Name;

                        string saveImageNamePath = Path.Combine(hiddenFolderPath, "smartArt_" + slide.SlideNumber.ToString() + "_" + countSmartArtsPerSlide.ToString() + "_" + subCategory + ".png");
                        element.Export(saveImageNamePath, PpShapeFormat.ppShapeFormatPNG, (int)presentation.PageSetup.SlideWidth, (int)presentation.PageSetup.SlideHeight, PpExportMode.ppScaleToFit);

                    }
                    //-----------------------------
                }
            }
            return true;
        }

        public void DeleteHiddenFolder(PowerPoint.Presentation presentation)
        {
            string hiddenFolderPath = presentation.Path.ToString();
            hiddenFolderPath = Path.Combine(hiddenFolderPath, "ImagesToDescrip");
            if (Directory.Exists(hiddenFolderPath))
            {
                Array.ForEach(Directory.GetFiles(hiddenFolderPath), File.Delete);
                Directory.Delete(hiddenFolderPath);
            }
        }

        public string[] XMLContainsAllSmartArts(PowerPoint.Presentation presentation)
        {
            /*
             * look for all SmartArts in that presentation
             * Does the XML contains all this SmartArts?
             */

            PowerPoint.Slides slides = presentation.Slides;
            List<string> ListAllSubcategories = AllSubCategories();
            /*
             * the smartArtCollector collect all all smartArt that are not in the XML
             * return: a array with the elements: "SlideNr","subCategory","category"
             */
            string smartArtCollector = "";
            int arrayCount = 0;

            string saveImagePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            saveImagePath = Path.Combine(saveImagePath, "SmartArtDescriptionXML");
            saveImagePath = Path.Combine(saveImagePath, "ImagesToDescrip");
            if (!Directory.Exists(saveImagePath))
            {
                Directory.CreateDirectory(saveImagePath);
            }
            else
            {
                Array.ForEach(Directory.GetFiles(saveImagePath), File.Delete);
            }

            foreach (PowerPoint.Slide slide in slides)
            {
                int countSmartArtsPerSlide = 0;
                foreach (PowerPoint.Shape element in slide.Shapes)
                {
                    
                    //-----------------------------
                    if (element.Type == Office.MsoShapeType.msoSmartArt)
                    {
                        countSmartArtsPerSlide++;
                        string category = element.SmartArt.Layout.Category;
                        string subCategory = element.SmartArt.Layout.Name;
                        if (!(ListAllSubcategories.Contains(subCategory)))
                        {
                             //SmartArt is not contains in the XML
                             //The export is importent for the DescForm
                            string saveImageNamePath = Path.Combine(saveImagePath, "smartArt_" + slide.SlideNumber.ToString() + "_" + countSmartArtsPerSlide.ToString() + ".png");
                            element.Export(saveImageNamePath, PpShapeFormat.ppShapeFormatPNG, (int)presentation.PageSetup.SlideWidth, (int)presentation.PageSetup.SlideHeight, PpExportMode.ppScaleToFit);
                            if (!(smartArtCollector == ""))
                            {
                                smartArtCollector += ";";
                            }
                            smartArtCollector += slide.SlideNumber.ToString() + "," + countSmartArtsPerSlide.ToString() + "," + subCategory + "," + category;
                            arrayCount++;
                        }                        
                    }
                    //-----------------------------
                } 
            }
            string[] notExistingDesc = null;
            if (!(smartArtCollector == ""))
            {
                notExistingDesc = Regex.Split(smartArtCollector, ";");
            }
            
            return notExistingDesc;
        }

        public bool XMLContainsSubCategory(string subCategory)
        {
            XmlReader reader = XmlReader.Create(descriptionXMLPath);
            while (reader.Read())
            {
                if (reader.HasAttributes)
                {
                    if (reader.GetAttribute("name") == subCategory)
                    {
                        reader.Close();
                        return true;
                    }
                }
            }
            reader.Close();
            return false;
        }

        private List<string> AllSubCategories()
        {
            List<string> allSubCategories = new List<string>();
            XmlReader reader = XmlReader.Create(descriptionXMLPath);

            while (reader.Read())
            {
                if (reader.HasAttributes)
                {
                    if (!(reader.GetAttribute("subName") == null))
                    {
                        allSubCategories.Add(reader.GetAttribute("subName"));
                    }
                }
            }
            reader.Close();
            return allSubCategories;
        }

        public List<string> ListAllCategries()
        {
            List<string> allCategories = new List<string>();
            XmlReader reader = XmlReader.Create(descriptionXMLPath);

            while (reader.Read())
            {
                if (reader.HasAttributes)
                {
                    if (!(reader.GetAttribute("name") == null))
                    {
                        allCategories.Add(reader.GetAttribute("name"));
                    }
                }
            }
            reader.Close();
            return allCategories;
        }

        public List<string> ListAllSubCategries(string category)
        {
            List<string> allSubCategories = new List<string>();
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(descriptionXMLPath);
            XmlNodeList nodes = xmlDoc.GetElementsByTagName("category");

            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["name"].Value == category)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        allSubCategories.Add(childNode.Attributes["subName"].Value);
                    }
                }
            }
            return allSubCategories;
        }

        public string GetCategoryFromSubCategory(string subCategory) 
        {
            /*
             * get the subcategory and look for the category
             */
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(descriptionXMLPath);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("subcategory");
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["subName"].Value == subCategory)
                {
                    return node.ParentNode.Attributes["name"].Value;
                }
            }
            return "XMLHelper.cs findet die Kategory nicht.";
        }

        public string GetStartDescription() 
        {
            string descriptionStart = "";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(descriptionXMLPath);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("descriptionStart");
            foreach (XmlNode node in nodes)
            {
                descriptionStart = node.InnerText;
            }
            return descriptionStart = System.Text.RegularExpressions.Regex.Replace(descriptionStart, "  +", "");
        } 

        public string GetSpecialShortDescription(string category, string subCategory)
        {
            /*
             * 1. look for the CategoryNode
             * 2. if found: look for the SubCategoryNode in the Childnodes
             * 3. get the description
             */
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(descriptionXMLPath);
            
            foreach(XmlNode node in xmlDoc.GetElementsByTagName("category"))
            {
                //<category>
                if (node.Attributes["name"].Value == category)
                {
                    foreach(XmlNode childNode in node.ChildNodes)
                    {
                            //<subcategory>
                            if (childNode.Attributes["subName"].Value == subCategory)
                            {
                                foreach (XmlNode childChildNode in childNode.ChildNodes)
                                {
                                    //<shortDescription> & <longDescripton>
                                    if (childChildNode.Name == "shortDescription")
                                    {
                                        string desc = childChildNode.InnerText;
                                        desc = System.Text.RegularExpressions.Regex.Replace(desc, "  +", "");
                                        desc = System.Text.RegularExpressions.Regex.Replace(desc, "\r\n+", "");
                                        return desc;
                                    }
                                }
                            }                        
                    }
                    return "Unterkategorie noch nicht angelegt.\n";
                }                
            }
            return "Kategorie noch nicht angelegt.\n";
        }

        public string GetSpecialLongDescription(string category, string subCategory)
        {
            /*
                 * get the complet content
                 * 1. get the content of the "<description>"-Tag
                 * 2. get the content of the "<eachMainPoint>"-Tag
                 * 3. get the content of the "<eachSubPoint>"-Tag
                 * 
                 * return: "description";"eachMainPoint";"eachSubPoint" 
                 * Get each part with: String[] eachpart = Regex.Split(returnStatment, ";"); 
                 */
            string specialDescription = "";
            string error = "";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(descriptionXMLPath);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("category");
            foreach(XmlNode node in nodes)
            {
                //<category>
                if(node.Attributes["name"].Value == category)
                {
                    foreach(XmlNode child1Node in node.ChildNodes)
                    {
                        //<subcategory>
                        if (child1Node.Attributes["subName"].Value == subCategory)
                        {
                            foreach (XmlNode child2Node in child1Node.ChildNodes)
                            {
                                //<shortDescription> & <longDescription>
                                if (child2Node.Name == "longDescription")
                                {
                                    //Childs: <description> & <descriptionForEachPoint>
                                    specialDescription = child2Node.FirstChild.InnerText;
                                    specialDescription = System.Text.RegularExpressions.Regex.Replace(specialDescription, "\r\n+", "");
        
                                    foreach (XmlNode child3Node in child2Node.LastChild.ChildNodes)
                                    {
                                        //<eachMainPoint> & <eachSubPoint>
                                        if (child3Node.Name == "eachMainPoint")
                                        {
                                            string child = child3Node.InnerText;
                                            child = System.Text.RegularExpressions.Regex.Replace(child, "\r\n+", "");
                                            specialDescription += ";" + child;
                                            
                                        }
                                        else if (child3Node.Name == "eachSubPoint")
                                        {
                                            string child = child3Node.InnerText;
                                            child = System.Text.RegularExpressions.Regex.Replace(child, "\r\n+", "");
                                            specialDescription += ";" + child;
                                            
                                        }
                                    }
                                    return specialDescription = System.Text.RegularExpressions.Regex.Replace(specialDescription, "  +", "");
                                }
                                error = "<longDescription>-Tag nicht gefunden";
                            }
                        }
                        error = "Diese Unterkategorie wurde nicht gefunden";
                    }
                }
                error = "Diese Kategorie wurde nicht gefunden";
            }
            return error;
        }

        public string TranslateCategory(string category)
        {
            /*
             * translate the category name
             */
            string germanCategory = "";
            switch (category)
            {
                case "list":
                    germanCategory = "Liste";
                    break;
                case "process":
                    germanCategory = "Prozess";
                    break;
                case "cycle":
                    germanCategory = "Zyklus";
                    break;
                case "hierarchy":
                    germanCategory = "Hierarchie";
                    break;
                case "relationship":
                    germanCategory = "Beziehung";
                    break;
                case "matrix":
                    germanCategory = "Matrix";
                    break;
                case "pyramid":
                    germanCategory = "Pyramide";
                    break;
                default:
                    germanCategory = category + " nicht übersetzt";
                    break;
            }
            return germanCategory;
        }

        // Placeholder ////////////////////////////////////

        public List<string> GetListOfPlaceholder(string category, string descType)
        {
            /*
             * creat a list with all Placholders from the special category and description type
             * descType have to be one of that: "startDescription", "longDesciption", "MainPointDescription", "SubPointDescription"
             */
            List<string> allPlaceholder = new List<string>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(placeholderAndInfoXMLPath);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("Placeholder");

            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["descType"].Value == descType)
                {                 
                    foreach(XmlNode childNode in node.ChildNodes){
                        if (childNode.Name == "inCategory")
                        {
                            if (childNode.Attributes["category"].Value == category)
                            {
                                string placeholder = node.Attributes["name"].Value;
                                placeholder = System.Text.RegularExpressions.Regex.Replace(placeholder, "\r\n+", "");
                                allPlaceholder.Add(placeholder);
                            }
                        }
                    }
                }
            }
            return allPlaceholder;
        }

        public string[][] GetListOfPlaceholderWithInfo(string category, string descType) //TODO: wird evtl. nicht benötigt
        {
            List<string> placeholderList = GetListOfPlaceholder(category, descType);
            if (placeholderList.Count > 0)
            {
                string[][] placeholderArray = new string[placeholderList.Count][];

                int counter = 0;
                foreach (string placeholder in placeholderList)
                {
                    placeholderArray[counter] = new string[] {placeholder, GetDescOfPlaceholder(placeholder, descType)};
                    counter++;
                }
                return placeholderArray;
            }
            return null;
        }

        public string GetDescOfPlaceholder(string placeholder, string descType)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(placeholderAndInfoXMLPath);

            XmlNodeList allNodes = xmlDoc.GetElementsByTagName("Placeholder");

            foreach (XmlNode node in allNodes)
            {
                if (node.Attributes["name"].Value == placeholder)
                {
                    if (node.Attributes["descType"].Value == descType)
                    {
                        foreach (XmlNode childNode in node.ChildNodes)
                        {
                            if (childNode.Name == "info")
                            {
                                string info = childNode.InnerText;
                                info = System.Text.RegularExpressions.Regex.Replace(info, "  +", "");
                                //info = System.Text.RegularExpressions.Regex.Replace(info, "\r\n+", "");
                                return info;
                            }
                        }
                    }
                }
            }
            return "Beschreibung nicht gefunden.";
        }

        ///////////////////////////////////
        private void SetDescriptionXMLPath(string path)
        {
            this.descriptionXMLPath = path;
        }
        private void SetPlaceholderAndInfoXMLPath(string path)
        {
            this.placeholderAndInfoXMLPath = path;
        }
    }
}
