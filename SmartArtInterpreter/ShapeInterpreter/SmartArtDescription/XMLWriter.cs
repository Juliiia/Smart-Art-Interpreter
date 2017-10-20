using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace SmartArtInterpreter.ShapeInterpreter.SmartArtDescription
{
    /*
     * modify the content of the XML
     */
    class XMLWriter
    {
        // Singelton /////////////////////
        private static XMLWriter _inst = null;
        private XMLWriter() 
        {
            XMLSaver xmlSaver = XMLSaver.getInstance;
            SetDescriptionXMLPath(xmlSaver.GetDescriptionXMLPath());
        }

        public static XMLWriter GetInstance{
            get
            {
                if (_inst == null)
                {
                    return _inst = new XMLWriter();
                }
                else
                {
                    return _inst;
                }
            }
        }
        /////////////////////////////////

        //attributes ---------------------------------------
        //string path = "D:/Dokumente/UNI/7. Semester/Bachelor-Arbeit/SmartArtInterpreter/SmartArtInterpreter/ShapeInterpreter/SmartArtDescription/SmartArtDescription.xml";
        private string descriptionXMLPath;

        //methods ------------------------------------------

        public bool ChangeShortDescInnerText(string category, string subCategory, string newContent)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(descriptionXMLPath);

            foreach (XmlNode node in xmlDoc.GetElementsByTagName("category"))
            {
                //<category>
                if (node.Attributes["name"].Value == category)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        //<subcategory>
                        if (childNode.Attributes["subName"].Value == subCategory)
                        {
                            foreach (XmlNode childChildNode in childNode.ChildNodes)
                            {
                                //<shortDescription> & <longDescripton>
                                if (childChildNode.Name == "shortDescription")
                                {
                                    childChildNode.InnerText = newContent;
                                    xmlDoc.Save(descriptionXMLPath);
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool ChangeLongDescInnerText(string category, string subCategory, string tagName, string newContent)
        {
            /*
             * Change the InnerText from the <description>-, <eachMainPoint>- or <eachSubPoint>-Tag
             */
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(descriptionXMLPath);

            foreach (XmlNode node in xmlDoc.GetElementsByTagName("category"))
            {
                //<category>
                if (node.Attributes["name"].Value == category)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        //<subcategory>
                        if (childNode.Attributes["subName"].Value == subCategory)
                        {
                            foreach (XmlNode childChildNode in childNode.ChildNodes)
                            {
                                //<shortDescription> & <longDescripton>
                                if (childChildNode.Name == "longDescription")
                                {
                                    if (childChildNode.FirstChild.Name == tagName)
                                    {
                                        //<description>
                                        childChildNode.FirstChild.InnerText = newContent;
                                        xmlDoc.Save(descriptionXMLPath);
                                        return true;
                                    }
                                    if (childChildNode.LastChild.FirstChild.Name == tagName)
                                    {
                                        //<eachMainPoint>
                                        childChildNode.LastChild.FirstChild.InnerText = newContent;
                                        xmlDoc.Save(descriptionXMLPath);
                                        return true;
                                    }
                                    if (childChildNode.LastChild.LastChild.Name == tagName)
                                    {
                                        //<eachSubPoint>
                                        childChildNode.LastChild.LastChild.InnerText = newContent;
                                        xmlDoc.Save(descriptionXMLPath);
                                        return true;

                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool InsertNewSubCategory(string category, string subCategory, string shortDescription, string longDescription, string descMainPoint, string descSubPoint)
        {
            /*
             * Attention!!! befor you start this methode check if the new subcategory does not exist
             */
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(descriptionXMLPath);

            foreach (XmlNode node in xmlDoc.GetElementsByTagName("category"))
            {
                //<category>
                if (node.Attributes["name"].Value == category)
                {
                    //check if SmartArt already exists
                    XmlNodeList existingSubCategories = node.ChildNodes;
                    foreach (XmlNode exitingSubCategory in existingSubCategories)
                    {
                        if (exitingSubCategory.Attributes["subName"].Value == subCategory)
                        {
                            return true;
                        }
                    }

                    //<subcategory subName="subCategory">
                    XmlElement newSubCat = xmlDoc.CreateElement("subcategory", node.NamespaceURI);
                    newSubCat.SetAttribute("subName", subCategory);
                    //    <shortDescription>
                    XmlElement shortDesc = xmlDoc.CreateElement("shortDescription");                   
                    //        shortDescription
                    shortDesc.InnerText = shortDescription;
                    //    Add </shortDescription>
                    newSubCat.AppendChild(shortDesc);
                    //    <longDescription>
                    XmlElement longDesc = xmlDoc.CreateElement("longDescription");
                    //        <description>
                    XmlElement longDescSpcial = xmlDoc.CreateElement("description");
                    //            longDescription
                    longDescSpcial.InnerText = longDescription;
                    //        Add </description>
                    longDesc.AppendChild(longDescSpcial);
                    //        <descriptionForEachPoint>
                    XmlElement descForEach = xmlDoc.CreateElement("descriptionForEachPoint");
                    //            <eachMainPoint>
                    XmlElement descForMain = xmlDoc.CreateElement("eachMainPoint");
                    //              descMainPoint
                    descForMain.InnerText = descMainPoint;
                    //            Add </eachMainPoint>
                    descForEach.AppendChild(descForMain);
                    //            <eachSubPoint>
                    XmlElement descForSub = xmlDoc.CreateElement("eachSubPoint");
                    //              descMainPoint
                    descForSub.InnerText = descSubPoint;
                    //            Add </eachSubPoint>
                    descForEach.AppendChild(descForSub);
                    //        Add </descriptionForEachPoint>
                    longDesc.AppendChild(descForEach);
                    //    Add </longDescription>
                    newSubCat.AppendChild(longDesc);
                    //    Add </subcategory>
                    node.AppendChild(newSubCat);
                    xmlDoc.Save(descriptionXMLPath);
                    return true;
                     
                }
            }
            return false;
        }

        public void DeleteSmartArtDescription(string category, string subCategory)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(descriptionXMLPath);

            foreach (XmlNode node in xmlDoc.GetElementsByTagName("category"))
            {
                if (node.Attributes["name"].Value == category)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.Attributes["subName"].Value == subCategory)
                        {
                            childNode.RemoveAll();
                            node.RemoveChild(childNode);
                            xmlDoc.Save(descriptionXMLPath);
                        }
                    }
                }
            }
        }

        /////////////////////////////
        private void SetDescriptionXMLPath(string path)
        {
            this.descriptionXMLPath = path;
        }
    }
}
