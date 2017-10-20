using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartArtInterpreter.Converter
{
    class ColorFinder
    {
        /*
         * Find the lowest distance from a unknown color to a known named color 
         * Delta E
         * 1.   RGB -> sRGB (absolute color space) "ForeColor.RGB" already use sRGB
         * 2.   sRGB -> L*a*b* (CIE 1931 color space)
         * 3.   firstResult = (L2 - L1)^2 + (a2 - a1)^2 + (b2 - b1)^2
         * 4.   Delta E = Sqrt(firstResult)
         * 5.   find the lowest distance
         * 
         * Test: http://davengrace.com/cgi-bin/cspace.pl
         * Test with steps: http://www.easyrgb.com/index.php?X=CALC#Result
         * Source: http://www.mycsharp.de/wbb2/thread.php?threadid=78941
         * D65 value: http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html
         * 
         * aussagen: rötliche, bläulich, ...
         */

        private static object[,] knownColors = {
                                                   { 255, 0, 0, "rötlichen"},       
                                                   { 0, 255, 0, "grünlichen"},
                                                   { 204, 255, 153, "grünlichen"},
                                                   { 0, 0, 255, "bläulichen"},  
                                                   { 100, 149, 237, "bläulichen"},
                                                   { 153, 221, 255, "hell bläulichen"},
                                                   { 255, 162, 0, "orangenen"},    
                                                   { 255, 228, 0, "gelblichen"},
                                                   { 255, 255, 153, "hell gelblichen"},
                                                   { 255, 0, 234, "pinkenen"},     
                                                   { 180, 0, 255, "lilanen"},
                                                   { 255, 255, 255, "weißlichen"},
                                                   { 0, 0, 0, "dunklen fast schwarzen"}
                                               };
        //TODO: Farbarray ergänzen

        public static string GetColorName(int rgb)
        {
            string name = "";
            double shortestDeltaEDistance = -1;
            // int rgb -> int[] rgb
            int[] rgbArray = RGBintToArray(rgb);
            // 2. sRGB -> L*a*b*
            double[] lab = RGBtoLab(rgbArray[0], rgbArray[1], rgbArray[2]);
            
            // for each
            for (int i = 0; i < knownColors.GetLongLength(0); i++)
            {
                double[] knownColorLab = RGBtoLab((int)knownColors[i, 0], (int)knownColors[i, 1], (int)knownColors[i, 2]);
                // Delta E = Sqrt((L2 - L1)^2 + (a2 - a1)^2 + (b2 - b1)^2)
                double deltaEDistance = Math.Sqrt(Math.Pow((lab[0] - knownColorLab[0]), 2) + Math.Pow((lab[1] - knownColorLab[1]), 2) + Math.Pow((lab[2] - knownColorLab[2]), 2));
                deltaEDistance = Math.Abs(deltaEDistance);
                if (shortestDeltaEDistance == -1.0)
                {
                    shortestDeltaEDistance = deltaEDistance;
                }
                if (deltaEDistance < shortestDeltaEDistance)
                {
                    shortestDeltaEDistance = deltaEDistance;
                    name = (string)knownColors[i, 3];
                }
            }


                return name;
        }

        //private static double[] RGBtoLab2(int r, int g, int b)
        //{
        //    // RGB [0 - 255] -> RGB [0 - 1]
        //    double[] doubleRGB = new double[3];
        //    doubleRGB[0] = r / 255d;
        //    doubleRGB[1] = g / 255d;
        //    doubleRGB[2] = b / 255d;

        //    // RGB -> XYZ (mit Referenzweiß D65 http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html)
        //    double[] xyz = new double[3];
        //    double[,] referenceMatrix = 
        //    {     { 0.412453, 0.357580, 0.180423 },
        //          { 0.212671, 0.715160, 0.072169 },
        //          { 0.019334, 0.119193, 0.950227 }  };


        //    xyz[0] = referenceMatrix[0, 0] * doubleRGB[0] + referenceMatrix[0, 1] * doubleRGB[1] + referenceMatrix[0, 2] * doubleRGB[2];
        //    xyz[1] = referenceMatrix[1, 0] * doubleRGB[0] + referenceMatrix[1, 1] * doubleRGB[1] + referenceMatrix[1, 2] * doubleRGB[2];
        //    xyz[2] = referenceMatrix[2, 0] * doubleRGB[0] + referenceMatrix[2, 1] * doubleRGB[1] + referenceMatrix[2, 2] * doubleRGB[2];

        //    // xyzUnnormiert normieren
        //    double[] xyzNormiert = new double[3];
        //    double[] writeD65 = { 0.950456, 1, 1.088754 };
        //    for (int i = 0; i < xyzNormiert.Length; i++)
        //    {
        //        xyzNormiert[i] = xyz[i] / writeD65[i];
        //    }

        //    //limit: 
        //    const double limit = 0.008856;

        //    bool Xlimit = xyzNormiert[0] > limit;
        //    bool Ylimit = xyzNormiert[1] > limit;
        //    bool Zlimit = xyzNormiert[2] > limit;
        //    double Y3 = Math.Pow(xyzNormiert[1], 1d / 3d);

        //    // nolinear projection XYZ -> Lab
        //    double X = Xlimit ? Math.Pow(xyzNormiert[0], 1d / 3d) : 7.787 * xyzNormiert[0] + 16d / 116d;
        //    double Y = Ylimit ? Y3 : 7.787 * xyzNormiert[1] + 16d / 116d;
        //    double Z = Zlimit ? Math.Pow(xyzNormiert[2], 1d / 3d) : 7.787 * xyzNormiert[1] + 16d / 116d;

        //    double[] lab = new double[3];
        //    lab[0] = Ylimit ? 116d * Y3 - 16d : 903.3 * xyzNormiert[1];
        //    lab[1] = 500d * (X - Y);
        //    lab[2] = 200d * (Y - Z);

        //    return lab;
        //}

        private static double[] RGBtoLab(int r, int g, int b)
        {
            // RGB [0 - 255] -> RGB [0 - 1]
            double[] doubleRGB = new double[3];
            doubleRGB[0] = r / 255d;
            doubleRGB[1] = g / 255d;
            doubleRGB[2] = b / 255d;

            // RGB -> XYZ (mit Referenzweiß D65 http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html)
            // https://github.com/THEjoezack/ColorMine/blob/master/ColorMine/ColorSpaces/Conversions/XyzConverter.cs
            if (doubleRGB[0] > 0.04045)
            {
                doubleRGB[0] = Math.Pow(((doubleRGB[0] + 0.055) / 1.055), 2.4);
            }
            else
            {
                doubleRGB[0] = doubleRGB[0] / 12.92;
            }
            if (doubleRGB[1] > 0.04045 )
            {
                doubleRGB[1] = Math.Pow((( doubleRGB[1] + 0.055 ) / 1.055 ), 2.4);
            }
            else
            {
                doubleRGB[1] = doubleRGB[1] / 12.92;
            }
            if (doubleRGB[2] > 0.04045)
            {
                doubleRGB[2] = Math.Pow(((doubleRGB[2] + 0.055) / 1.055), 2.4);
            }
            else
            {
                doubleRGB[2] = doubleRGB[2] / 12.92;
            }

            doubleRGB[0] = doubleRGB[0] * 100;
            doubleRGB[1] = doubleRGB[1] * 100;
            doubleRGB[2] = doubleRGB[2] * 100;

            double[] XYZ = new double[3];
            double[,] referenceWrite = 
            {     { 0.412453, 0.357580, 0.180423 },
                  { 0.212671, 0.715160, 0.072169 },
                  { 0.019334, 0.119193, 0.950227 }  };


            XYZ[0] = referenceWrite[0, 0] * doubleRGB[0] + referenceWrite[0, 1] * doubleRGB[1] + referenceWrite[0, 2] * doubleRGB[2];
            XYZ[1] = referenceWrite[1, 0] * doubleRGB[0] + referenceWrite[1, 1] * doubleRGB[1] + referenceWrite[1, 2] * doubleRGB[2];
            XYZ[2] = referenceWrite[2, 0] * doubleRGB[0] + referenceWrite[2, 1] * doubleRGB[1] + referenceWrite[2, 2] * doubleRGB[2];

            // nolinear projection XYZ -> Lab
            double X = XYZ[0] / 95.047;   //Illuminant= D65
            double Y = XYZ[1] / 100.000;
            double Z = XYZ[2] / 108.883;

            if ( X > 0.008856 )
            {
                X = Math.Pow(X, 1D/3 );
            }else{
                X = ( 7.787 * X ) + ( 16 / 116 );
            }
            if ( Y > 0.008856 )
            {
                Y = Math.Pow(Y, 1D/3 );
            }else{
                Y = ( 7.787 * Y ) + ( 16 / 116 );
            }
            if ( Z > 0.008856 )
            {
                Z = Math.Pow(Z, 1D/3 );
            }else{
                Z = ( 7.787 * Z ) + ( 16 / 116 );
            }

            double[] cieLab = new double[3];
            cieLab[0] = ( 116 * Y ) - 16;
            cieLab[1] = 500 * ( X - Y );
            cieLab[2] = 200 * (Y - Z);

            return cieLab;
        }

        private static int[] RGBintToArray(int rgb)
        {
            int temp1 = rgb / 256;
            int temp2 = temp1 / 256;

            int[] rgbArray = new int[3];

            rgbArray[0] = rgb % 256;
            rgbArray[1] = temp1 % 256;
            rgbArray[2] = temp2 % 256;

            return rgbArray;
        }
    }
}
