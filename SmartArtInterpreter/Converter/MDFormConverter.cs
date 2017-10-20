using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartArtInterpreter.Converter
{
    class MDFormConverter
    {
        public string GetBoldMark()
        {
            return "**";
        }
        public string GetItalicMark()
        {
            return "_";
        }
        public string GetBoldItalicMark()
        {
            return "***";
        }
        public string GetFirstTitle(string title)
        {
            return "# " + title + "\n";
        }
        public string GetSecondTitle(string title)
        {
            return "## " + title + "\n";
        }
        public string GetNumberform(int nr, bool nrForm)
        {
            if (nrForm)
            {
                return "\n|| - Folie " + nr + " - \n\n";
            }
            else
            {
                return "\n|| - Seite " + nr + " - \n";
            }
        }
        public string GetLink(string linkName, string url)
        {
            return " [" + linkName + "](" + url + ")";
        }
        //SMARTART/////////////////////////////////////////
        public string GetSmartArtTitle(string subCategory, int slideNr, int nrOnSlide)
        {
            string title = "SmartArt " +nrOnSlide+ ": " + subCategory + " Folie: " + slideNr;
            return GetSecondTitle(title);
        }
        public string GetLinkToLongDesc(string imagePath, string title)
        {
            //[ ![Bildbeschreibung ist ausgelagert.](bilder/image_007_4.png)](bilder.html#bildbeschreibung-von-screenshot-aus-dem-e-learning-angbot)
            return "[![SmartArtbeschreibung ist ausgelagert.](" + imagePath + ")](SmartArtLangbeschreibungen.html#" + title + ")" + "\n";
        }
        public string GetLinkToShortDesc(string link)
        {
            return "[Kurzbeschreibung der nachfolenden SmartArt ](SmartArtKurzbeschreibungen.html#" + link + ")"+" \n";
        }
        //LISTE////////////////////////////////////////////
        public string GetLineList()
        {
            return "- ";
        }
        public string GetOrderdList(int number)
        {
            return number + ". ";
        }
        public string GetSpaceBefore(int nr)
        {
            string space = "";

            // is multi indent
            if (nr != 0)
            {
                space = new String('\t', nr);
            }
            return space;
        }
        //TABELE///////////////////////////////////////////
        public string GetTableCellLine()
        {
            return "|";
        }
        public string GetTableLine(int length)
        {
            string line = "";
            for (int c = 0; c < length; c++)
            {
                line += "-";
            }
            return line + "|";
        }
    }
}
