using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Office = Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using MDConverter = SmartArtInterpreter.Converter.MDFormConverter;
using ShortDescriptionGen = SmartArtInterpreter.ShapeInterpreter.SmartArtDescType.ShortDescription;
using LongDescriptionLPCP = SmartArtInterpreter.ShapeInterpreter.SmartArtDescType.LongDescriptionLPCP;
using LongDescriptionM = SmartArtInterpreter.ShapeInterpreter.SmartArtDescType.LongDescriptionM;
using LongDescriptionR = SmartArtInterpreter.ShapeInterpreter.SmartArtDescType.LongDescriptionR;
using XMLHelper = SmartArtInterpreter.ShapeInterpreter.SmartArtDescription.XMLHelper;
using SmartArtInterpreter.ShapeInterpreter.SmartArtDescType;

namespace SmartArtInterpreter.ShapeInterpreter
{
    /*
     * that class gets the kategorie and 
     * and selects and controlls the next steps
     * 1. get the spacial short description
     * 2. get the spacial long description
     */
    class SmartArtInterpreter
    {
        //attributes ---------------------------------------
        string category;
        string subCategory;
        string shortDescriptionfirstPart;
        int countMainPoints;

        //methods ------------------------------------------

        public string GetSmartArtShortDescription(PowerPoint.Shape smartArt)
        {
            /*
             * if "shortDescription" was filled by "ShortDescription"-Class, it can be use by "LongDescription"-Class
             */
            SetCategory(smartArt.SmartArt.Layout.Category);
            SetSubCategory(smartArt.SmartArt.Layout.Name);
            // Get the short description and set the attributes
            ShortDescriptionGen shortDesc = new ShortDescriptionGen(this);
            string shortDescription = shortDesc.GetShortDescription(smartArt);

            return shortDescription;
        }

        public string GetSmartArtLongDescription(PowerPoint.Shape smartArt)
        {
            /*
             * if "shortDescription" was filled by "ShortDescription"-Class, it can be use by "LongDescription"-Class
             */
            SetCategory(smartArt.SmartArt.Layout.Category);
            SetSubCategory(smartArt.SmartArt.Layout.Name);
        
            // Get the long description
            string longDescription = "";
            switch (GetCategory())
            {
                case "matrix":
                    LongDescriptionM longDescM = new LongDescriptionM(this);
                    longDescription = longDescM.GetLongDescription(smartArt);
                    break;

                case "hierarchy":
                    LongDescriptionH longDescH = new LongDescriptionH(this);
                    longDescription = longDescH.GetLongDescription(smartArt);
                    break;

                case "relationship":
                    LongDescriptionR longDescR = new LongDescriptionR(this);
                    longDescription = longDescR.GetLongDescription(smartArt);
                    break;

                default: //list, process, cycle, pyramid, relationship
                    LongDescriptionLPCP longDescLPCP = new LongDescriptionLPCP(this);
                    longDescription = longDescLPCP.GetLongDescription(smartArt);
                    break;
            }
         
            /*
             * list, process, cycle, pyramid, Beziehung zusammen behandeln
             * Hierarchie (matrix) extra
             */

            return longDescription;
        }

        //Getter and Setter ///////////////////////////////////////////////
        public void SetCategory(string newCategory)
        {
            this.category = newCategory;
        }
        public string GetCategory()
        {
            return category;
        }

        public void SetSubCategory(string name)
        {
            this.subCategory = name;
        }
        public string GetSubCategory()
        {
            return this.subCategory;
        }

        public void SetShortDescriptionfirstPart(string description)
        {
            this.shortDescriptionfirstPart = description;
        }
        public string GetShortDescriptionfirstPart()
        {
            return shortDescriptionfirstPart;
        }

        public void SetCountMainPoints(int points)
        {
            this.countMainPoints = points;
        }
        public int GetCountMainPoints()
        {
            return this.countMainPoints;
        }
    }
}
