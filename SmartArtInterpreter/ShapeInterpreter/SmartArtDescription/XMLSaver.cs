using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SmartArtInterpreter.ShapeInterpreter.SmartArtDescription
{
    /*
     * http://stackoverflow.com/questions/10019079/storing-xml-data-for-use-with-an-addin
     */
    class XMLSaver
    {
        // Singleton //////////////////////////
        private static XMLSaver _instance = null;
        private XMLSaver() 
        {
            CreateOrGetDirectory();
        }

        public static XMLSaver getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new XMLSaver();
                }
                return _instance;
            }
        }
        /////////////////////////////////////////
        //attribute --------------------------------------------
        private string descriptionXMLPath;
        private string placeholderXMLPath;

        //methoden --------------------------------------------

        private void CreateOrGetDirectory()
        {
            string userSettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string xmlFolder = Path.Combine(userSettingsPath, "SmartArtDescriptionXML");

            if (!Directory.Exists(xmlFolder))
            {
                Directory.CreateDirectory(xmlFolder);
            }
            
            SetDescriptionXMLPath(Path.Combine(xmlFolder, "SmartArtDescription.xml"));
            SetPlaceholderXMLPath(Path.Combine(xmlFolder, "PlaceholderAndInfo.xml"));
            CreateXML();
        }

        public void CreateXML()
        {
            if (!(File.Exists(GetDescriptionXMLPath())))
            {
                XmlDocument xmlDoc = new XmlDocument();
                //Create "SmartArtDescription.xml" ///////////////////
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = xmlDoc.CreateElement("SmartArt", "http://tempuri.org/SmartArtDescription.xsd");
                xmlDoc.AppendChild(root);
                xmlDoc.InsertBefore(xmlDeclaration, root);

                XmlElement descriptionStart = xmlDoc.CreateElement("descriptionStart", "http://tempuri.org/SmartArtDescription.xsd");
                descriptionStart.InnerText = "Kategorie: #Kategorie# \nUnterkategorie: #Unterkategorie# \nDiese Darstellung beinhaltet #AnzahlHauptpunkte# Hauptpunkte #mit/ohneUP# Unterpunkten.";
                root.AppendChild(descriptionStart);

                XmlElement categoryList = xmlDoc.CreateElement("category", "http://tempuri.org/SmartArtDescription.xsd");
                categoryList.SetAttribute("name", "list");
                categoryList.InnerText = "";
                root.AppendChild(categoryList);

                XmlElement categoryProcess = xmlDoc.CreateElement("category", "http://tempuri.org/SmartArtDescription.xsd");
                categoryProcess.SetAttribute("name", "process");
                categoryProcess.InnerText = "";
                root.AppendChild(categoryProcess);

                XmlElement categoryCycle = xmlDoc.CreateElement("category", "http://tempuri.org/SmartArtDescription.xsd");
                categoryCycle.SetAttribute("name", "cycle");
                categoryCycle.InnerText = "";
                root.AppendChild(categoryCycle);

                XmlElement categoryHierarchy = xmlDoc.CreateElement("category", "http://tempuri.org/SmartArtDescription.xsd");
                categoryHierarchy.SetAttribute("name", "hierarchy");
                categoryHierarchy.InnerText = "";
                root.AppendChild(categoryHierarchy);

                XmlElement categoryRelationship = xmlDoc.CreateElement("category", "http://tempuri.org/SmartArtDescription.xsd");
                categoryRelationship.SetAttribute("name", "relationship");
                categoryRelationship.InnerText = "";
                root.AppendChild(categoryRelationship);

                XmlElement categoryMatrix = xmlDoc.CreateElement("category", "http://tempuri.org/SmartArtDescription.xsd");
                categoryMatrix.SetAttribute("name", "matrix");
                categoryMatrix.InnerText = "";
                root.AppendChild(categoryMatrix);

                XmlElement categoryPyramid = xmlDoc.CreateElement("category", "http://tempuri.org/SmartArtDescription.xsd");
                categoryPyramid.SetAttribute("name", "pyramid");
                categoryPyramid.InnerText = "";
                root.AppendChild(categoryPyramid);

                xmlDoc.Save(GetDescriptionXMLPath());
             
            }
            if (!(File.Exists(GetPlaceholderXMLPath())))
            {
                XmlDocument xmlDoc = new XmlDocument();
                //Create "PlaceholderAndInfo.xml" ///////////////////
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = xmlDoc.CreateElement("AllInfos", "http://tempuri.org/PlaceholderAndInfoXML.xsd");
                xmlDoc.AppendChild(root);
                xmlDoc.InsertBefore(xmlDeclaration, root);

                XmlElement placeholder1 = xmlDoc.CreateElement("Placeholder");
                placeholder1.SetAttribute("name", "#Kategorie#");
                placeholder1.SetAttribute("descType", "startDescription");
                root.AppendChild(placeholder1);
                    XmlElement category1 = xmlDoc.CreateElement("inCategory");
                    category1.SetAttribute("category", "list");
                    placeholder1.AppendChild(category1);
                    XmlElement category2 = xmlDoc.CreateElement("inCategory");
                    category2.SetAttribute("category", "process");
                    placeholder1.AppendChild(category2);
                    XmlElement category3 = xmlDoc.CreateElement("inCategory");
                    category3.SetAttribute("category", "cycle");
                    placeholder1.AppendChild(category3);
                    XmlElement category4 = xmlDoc.CreateElement("inCategory");
                    category4.SetAttribute("category", "hierarchy");
                    placeholder1.AppendChild(category4);
                    XmlElement category5 = xmlDoc.CreateElement("inCategory");
                    category5.SetAttribute("category", "relationship");
                    placeholder1.AppendChild(category5);
                    XmlElement category6 = xmlDoc.CreateElement("inCategory");
                    category6.SetAttribute("category", "matrix");
                    placeholder1.AppendChild(category6);
                    XmlElement category7 = xmlDoc.CreateElement("inCategory");
                    category7.SetAttribute("category", "pyramid");
                    placeholder1.AppendChild(category7);
                    XmlElement info1 = xmlDoc.CreateElement("info");
                    info1.InnerText = "Platzhaltertyp: Wort \nDieser Platzhalter wird später durch den Kategorienamen ersetzt.";
                    placeholder1.AppendChild(info1);

                XmlElement placeholder2 = xmlDoc.CreateElement("Placeholder");
                placeholder2.SetAttribute("name", "#Unterkategorie#");
                placeholder2.SetAttribute("descType", "startDescription");
                root.AppendChild(placeholder2);
                    XmlElement category21 = xmlDoc.CreateElement("inCategory");
                    category21.SetAttribute("category", "list");
                    placeholder2.AppendChild(category21);
                    XmlElement category22 = xmlDoc.CreateElement("inCategory");
                    category22.SetAttribute("category", "process");
                    placeholder2.AppendChild(category22);
                    XmlElement category23 = xmlDoc.CreateElement("inCategory");
                    category23.SetAttribute("category", "cycle");
                    placeholder2.AppendChild(category23);
                    XmlElement category24 = xmlDoc.CreateElement("inCategory");
                    category24.SetAttribute("category", "hierarchy");
                    placeholder2.AppendChild(category24);
                    XmlElement category25 = xmlDoc.CreateElement("inCategory");
                    category25.SetAttribute("category", "relationship");
                    placeholder2.AppendChild(category25);
                    XmlElement category26 = xmlDoc.CreateElement("inCategory");
                    category26.SetAttribute("category", "matrix");
                    placeholder2.AppendChild(category26);
                    XmlElement category27 = xmlDoc.CreateElement("inCategory");
                    category27.SetAttribute("category", "pyramid");
                    placeholder2.AppendChild(category27);
                    XmlElement info21 = xmlDoc.CreateElement("info");
                    info21.InnerText = "Platzhaltertyp: Wort \nDieser Platzhalter wird später durch den Unterkategorienamen ersetzt.";
                    placeholder2.AppendChild(info21);

                XmlElement placeholder3 = xmlDoc.CreateElement("Placeholder");
                placeholder3.SetAttribute("name", "#AnzahlHauptpunkte#");
                placeholder3.SetAttribute("descType", "startDescription");
                root.AppendChild(placeholder3);
                    XmlElement category31 = xmlDoc.CreateElement("inCategory");
                    category31.SetAttribute("category", "list");
                    placeholder3.AppendChild(category31);
                    XmlElement category32 = xmlDoc.CreateElement("inCategory");
                    category32.SetAttribute("category", "process");
                    placeholder3.AppendChild(category32);
                    XmlElement category33 = xmlDoc.CreateElement("inCategory");
                    category33.SetAttribute("category", "cycle");
                    placeholder3.AppendChild(category33);
                    XmlElement category34 = xmlDoc.CreateElement("inCategory");
                    category34.SetAttribute("category", "hierarchy");
                    placeholder3.AppendChild(category34);
                    XmlElement category35 = xmlDoc.CreateElement("inCategory");
                    category35.SetAttribute("category", "relationship");
                    placeholder3.AppendChild(category35);
                    XmlElement category36 = xmlDoc.CreateElement("inCategory");
                    category36.SetAttribute("category", "matrix");
                    placeholder3.AppendChild(category36);
                    XmlElement category37 = xmlDoc.CreateElement("inCategory");
                    category37.SetAttribute("category", "pyramid");
                    placeholder3.AppendChild(category37);
                    XmlElement info31 = xmlDoc.CreateElement("info");
                    info31.InnerText = "Platzhaltertyp: ganze positive Zahl \nDieser Platzhalter wird durch die Anzahl der Level 1 Hauptpunkte ersetzt.";
                    placeholder3.AppendChild(info31);

                XmlElement placeholder4 = xmlDoc.CreateElement("Placeholder");
                placeholder4.SetAttribute("name", "#mit/ohneUP#");
                placeholder4.SetAttribute("descType", "startDescription");
                root.AppendChild(placeholder4);
                    XmlElement category41 = xmlDoc.CreateElement("inCategory");
                    category41.SetAttribute("category", "list");
                    placeholder4.AppendChild(category41);
                    XmlElement category42 = xmlDoc.CreateElement("inCategory");
                    category42.SetAttribute("category", "process");
                    placeholder4.AppendChild(category42);
                    XmlElement category43 = xmlDoc.CreateElement("inCategory");
                    category43.SetAttribute("category", "cycle");
                    placeholder4.AppendChild(category43);
                    XmlElement category44 = xmlDoc.CreateElement("inCategory");
                    category44.SetAttribute("category", "hierarchy");
                    placeholder4.AppendChild(category44);
                    XmlElement category45 = xmlDoc.CreateElement("inCategory");
                    category45.SetAttribute("category", "relationship");
                    placeholder4.AppendChild(category45);
                    XmlElement category46 = xmlDoc.CreateElement("inCategory");
                    category46.SetAttribute("category", "matrix");
                    placeholder4.AppendChild(category46);
                    XmlElement category47 = xmlDoc.CreateElement("inCategory");
                    category47.SetAttribute("category", "pyramid");
                    placeholder4.AppendChild(category47);
                    XmlElement info41 = xmlDoc.CreateElement("info");
                    info41.InnerText = "Platzhaltertyp: 'mit' oder 'ohne' \nDieser Platzhalter wird durch eines der obrigen Worte ersetzt um anzugeben ob Unterpunkte vorhanden sind.";
                    placeholder4.AppendChild(info41);

                XmlElement placeholder5 = xmlDoc.CreateElement("Placeholder");
                placeholder5.SetAttribute("name", "#AnzahlHauptpunkte#");
                placeholder5.SetAttribute("descType", "longDesciption");
                root.AppendChild(placeholder5);
                    XmlElement category51 = xmlDoc.CreateElement("inCategory");
                    category51.SetAttribute("category", "list");
                    placeholder5.AppendChild(category51);
                    XmlElement category52 = xmlDoc.CreateElement("inCategory");
                    category52.SetAttribute("category", "process");
                    placeholder5.AppendChild(category52);
                    XmlElement category53 = xmlDoc.CreateElement("inCategory");
                    category53.SetAttribute("category", "cycle");
                    placeholder5.AppendChild(category53);
                    XmlElement category54 = xmlDoc.CreateElement("inCategory");
                    category54.SetAttribute("category", "hierarchy");
                    placeholder5.AppendChild(category54);
                    XmlElement category57 = xmlDoc.CreateElement("inCategory");
                    category57.SetAttribute("category", "pyramid");
                    placeholder5.AppendChild(category57);
                    XmlElement info51 = xmlDoc.CreateElement("info");
                    info51.InnerText = "Platzhaltertyp: ganze positive Zahl \nDieser Platzhalter wird durch die Anzahl der Level 1 Hauptpunkte ersetzt.";
                    placeholder5.AppendChild(info51);

                XmlElement placeholder6 = xmlDoc.CreateElement("Placeholder");
                placeholder6.SetAttribute("name", "#wievielterHP#");
                placeholder6.SetAttribute("descType", "MainPointDescription");
                root.AppendChild(placeholder6);
                    XmlElement category61 = xmlDoc.CreateElement("inCategory");
                    category61.SetAttribute("category", "list");
                    placeholder6.AppendChild(category61);
                    XmlElement category62 = xmlDoc.CreateElement("inCategory");
                    category62.SetAttribute("category", "process");
                    placeholder6.AppendChild(category62);
                    XmlElement category63 = xmlDoc.CreateElement("inCategory");
                    category63.SetAttribute("category", "cycle");
                    placeholder6.AppendChild(category63);
                    XmlElement category64 = xmlDoc.CreateElement("inCategory");
                    category64.SetAttribute("category", "hierarchy");
                    placeholder6.AppendChild(category64);
                    XmlElement category65 = xmlDoc.CreateElement("inCategory");
                    category65.SetAttribute("category", "relationship");
                    placeholder6.AppendChild(category65);
                    XmlElement category67 = xmlDoc.CreateElement("inCategory");
                    category67.SetAttribute("category", "pyramid");
                    placeholder6.AppendChild(category67);
                    XmlElement info61 = xmlDoc.CreateElement("info");
                    info61.InnerText = "Platzhaltertyp: ganze posivite Zahl \nDieser Platzhalter nummeriert die Hauptpunkte durch, beginnend bei '1.' Hauptpunkt.";
                    placeholder6.AppendChild(info61);

                XmlElement placeholder7 = xmlDoc.CreateElement("Placeholder");
                placeholder7.SetAttribute("name", "#Farbe#");
                placeholder7.SetAttribute("descType", "MainPointDescription");
                root.AppendChild(placeholder7);
                    XmlElement category71 = xmlDoc.CreateElement("inCategory");
                    category71.SetAttribute("category", "list");
                    placeholder7.AppendChild(category71);
                    XmlElement category72 = xmlDoc.CreateElement("inCategory");
                    category72.SetAttribute("category", "process");
                    placeholder7.AppendChild(category72);
                    XmlElement category73 = xmlDoc.CreateElement("inCategory");
                    category73.SetAttribute("category", "cycle");
                    placeholder7.AppendChild(category73);
                    XmlElement category78 = xmlDoc.CreateElement("inCategory");
                    category78.SetAttribute("category", "relationship");
                    placeholder7.AppendChild(category78);
                    XmlElement category75 = xmlDoc.CreateElement("inCategory");
                    category75.SetAttribute("category", "relationship");
                    placeholder7.AppendChild(category75);
                    XmlElement category76 = xmlDoc.CreateElement("inCategory");
                    category76.SetAttribute("category", "matrix");
                    placeholder7.AppendChild(category76);
                    XmlElement category77 = xmlDoc.CreateElement("inCategory");
                    category77.SetAttribute("category", "pyramid");
                    placeholder7.AppendChild(category77);
                    XmlElement info71 = xmlDoc.CreateElement("info");
                    info71.InnerText = "Platzhaltertyp: Wort \nDieser Platzhalter wird durch das Farbwort der Hintergrundfarbe der umgebenden Form ersetzt.";
                    placeholder7.AppendChild(info71);

                XmlElement placeholder8 = xmlDoc.CreateElement("Placeholder");
                placeholder8.SetAttribute("name", "#InhaltHauptpunkt#");
                placeholder8.SetAttribute("descType", "MainPointDescription");
                root.AppendChild(placeholder8);
                    XmlElement category81 = xmlDoc.CreateElement("inCategory");
                    category81.SetAttribute("category", "list");
                    placeholder8.AppendChild(category81);
                    XmlElement category82 = xmlDoc.CreateElement("inCategory");
                    category82.SetAttribute("category", "process");
                    placeholder8.AppendChild(category82);
                    XmlElement category83 = xmlDoc.CreateElement("inCategory");
                    category83.SetAttribute("category", "cycle");
                    placeholder8.AppendChild(category83);
                    XmlElement category84 = xmlDoc.CreateElement("inCategory");
                    category84.SetAttribute("category", "hierarchy");
                    placeholder8.AppendChild(category84);
                    XmlElement category85 = xmlDoc.CreateElement("inCategory");
                    category85.SetAttribute("category", "relationship");
                    placeholder8.AppendChild(category85);
                    XmlElement category86 = xmlDoc.CreateElement("inCategory");
                    category86.SetAttribute("category", "matrix");
                    placeholder8.AppendChild(category86);
                    XmlElement category87 = xmlDoc.CreateElement("inCategory");
                    category87.SetAttribute("category", "pyramid");
                    placeholder8.AppendChild(category87);
                    XmlElement info81 = xmlDoc.CreateElement("info");
                    info81.InnerText = "Platzhaltertyp: Text \nHier wird der eigentliche Inhalt, welcher in der Grafik dargestellt ist, eingefügt.";
                    placeholder8.AppendChild(info81);

                XmlElement placeholder9 = xmlDoc.CreateElement("Placeholder");
                placeholder9.SetAttribute("name", "#links/rechts#");
                placeholder9.SetAttribute("descType", "MainPointDescription");
                root.AppendChild(placeholder9);
                    XmlElement category95 = xmlDoc.CreateElement("inCategory");
                    category95.SetAttribute("category", "relationship");
                    placeholder9.AppendChild(category95);
                    XmlElement info91 = xmlDoc.CreateElement("info");
                    info91.InnerText = "Platzhaltertyp: 'links' oder 'rechts' \nDieser Platzhalter wird später mit einem der obrigen Wörter ersetzt und bezeichnet die Position der Hauptpunkte.";
                    placeholder9.AppendChild(info91);

                XmlElement placeholder10 = xmlDoc.CreateElement("Placeholder");
                placeholder10.SetAttribute("name", "#XYposition#");
                placeholder10.SetAttribute("descType", "MainPointDescription");
                root.AppendChild(placeholder10);
                    XmlElement category106 = xmlDoc.CreateElement("inCategory");
                    category106.SetAttribute("category", "matrix");
                    placeholder10.AppendChild(category106);
                    XmlElement info101 = xmlDoc.CreateElement("info");
                    info101.InnerText = "Platzhaltertype: 'oben links', 'oben rechts', 'unten links' oder 'unten links' \nAchtung: Dieser Platzhalter macht nur Sinn, wenn 4 Hauptpunkte vorhanden sind.";
                    placeholder10.AppendChild(info101);

                XmlElement placeholder11 = xmlDoc.CreateElement("Placeholder");
                placeholder11.SetAttribute("name", "#InhaltUnterpunkt#");
                placeholder11.SetAttribute("descType", "SubPointDescription");
                root.AppendChild(placeholder11);
                    XmlElement category111 = xmlDoc.CreateElement("inCategory");
                    category111.SetAttribute("category", "list");
                    placeholder11.AppendChild(category111);
                    XmlElement category112 = xmlDoc.CreateElement("inCategory");
                    category112.SetAttribute("category", "process");
                    placeholder11.AppendChild(category112);
                    XmlElement category113 = xmlDoc.CreateElement("inCategory");
                    category113.SetAttribute("category", "cycle");
                    placeholder11.AppendChild(category113);
                    XmlElement category114 = xmlDoc.CreateElement("inCategory");
                    category114.SetAttribute("category", "hierarchy");
                    placeholder11.AppendChild(category114);
                    XmlElement category115 = xmlDoc.CreateElement("inCategory");
                    category115.SetAttribute("category", "relationship");
                    placeholder11.AppendChild(category115);
                    XmlElement category116 = xmlDoc.CreateElement("inCategory");
                    category116.SetAttribute("category", "matrix");
                    placeholder11.AppendChild(category116);
                    XmlElement category117 = xmlDoc.CreateElement("inCategory");
                    category117.SetAttribute("category", "pyramid");
                    placeholder11.AppendChild(category117);
                    XmlElement info111 = xmlDoc.CreateElement("info");
                    info111.InnerText = "Platzhaltertyp: Text \nHier wird der eigentliche Inhalt, welcher in der Grafik dargestellt ist, eingefügt.";
                    placeholder11.AppendChild(info111);

                XmlElement placeholder12 = xmlDoc.CreateElement("Placeholder");
                placeholder12.SetAttribute("name", "#Level#");
                placeholder12.SetAttribute("descType", "SubPointDescription");
                root.AppendChild(placeholder12);
                    XmlElement category121 = xmlDoc.CreateElement("inCategory");
                    category121.SetAttribute("category", "list");
                    placeholder12.AppendChild(category121);
                    XmlElement category122 = xmlDoc.CreateElement("inCategory");
                    category122.SetAttribute("category", "process");
                    placeholder12.AppendChild(category122);
                    XmlElement category123 = xmlDoc.CreateElement("inCategory");
                    category123.SetAttribute("category", "cycle");
                    placeholder12.AppendChild(category123);
                    XmlElement category124 = xmlDoc.CreateElement("inCategory");
                    category124.SetAttribute("category", "hierarchy");
                    placeholder12.AppendChild(category124);
                    XmlElement category126 = xmlDoc.CreateElement("inCategory");
                    category126.SetAttribute("category", "matrix");
                    placeholder12.AppendChild(category126);
                    XmlElement category127 = xmlDoc.CreateElement("inCategory");
                    category127.SetAttribute("category", "pyramid");
                    placeholder12.AppendChild(category127);
                    XmlElement info121 = xmlDoc.CreateElement("info");
                    info121.InnerText = "Platzhaltertyp: ganze positive Zahl \nDieser Platzhalter wird durch die Levelzahl des speziellen Punktes ersetzt. \nDer Hauptpunkt entspricht Level 1.";
                    placeholder12.AppendChild(info121);

                XmlElement placeholder13 = xmlDoc.CreateElement("Placeholder");
                placeholder13.SetAttribute("name", "#XYposition#");
                placeholder13.SetAttribute("descType", "SubPointDescription");
                root.AppendChild(placeholder13);
                    XmlElement category136 = xmlDoc.CreateElement("inCategory");
                    category136.SetAttribute("category", "matrix");
                    placeholder13.AppendChild(category136);
                    XmlElement info131 = xmlDoc.CreateElement("info");
                    info131.InnerText = "Platzhaltertype: 'oben links', 'oben rechts', 'unten links' oder 'unten links' \nAchtung: Dieser Platzhalter macht nur Sinn, wenn 4 Unterpunkte und nur 1 Hauptpunkt vorhanden sind.";
                    placeholder13.AppendChild(info131);

                XmlElement placeholder14 = xmlDoc.CreateElement("Placeholder");
                placeholder14.SetAttribute("name", "#Leveldarueber#");
                placeholder14.SetAttribute("descType", "SubPointDescription");
                root.AppendChild(placeholder14);
                    XmlElement category146 = xmlDoc.CreateElement("inCategory");
                    category146.SetAttribute("category", "hierarchy");
                    placeholder14.AppendChild(category146);
                    XmlElement info141 = xmlDoc.CreateElement("info");
                    info141.InnerText = "Platzhaltertyp: ganze positive Zahl \nDieser Platzhalter wird durch die Levelzahl des speziellen Elternpunktes ersetzt.";
                    placeholder14.AppendChild(info141);

                    xmlDoc.Save(GetPlaceholderXMLPath());
            }
            
        }

        ///////////////////////////////////////////

        public string GetDescriptionXMLPath()
        {
            return this.descriptionXMLPath;
        }
        private void SetDescriptionXMLPath(string path)
        {
            this.descriptionXMLPath = path;
        }

        public string GetPlaceholderXMLPath()
        {
            return this.placeholderXMLPath;
        }
        private void SetPlaceholderXMLPath(string path)
        {
            this.placeholderXMLPath = path;
        }
    }
}
