using System;
using CursesSharp.Gui;

namespace Grapher
{
    public class TransformAG : ITransformAG
    {
        public static double charAspect = 1.0;

        private IntPoint asciiSize;

        private DoublePoint graphCenter;

        private DoublePoint graphSize;


        public DoublePoint Get_GARatio()
        {
            return new DoublePoint (
                (graphSize.x * charAspect) / asciiSize.x,
                graphSize.y/asciiSize.y
            );
        }

        public DoublePoint Get_GraphCenter()
        {
            return graphCenter;
        }

        public void Set_GraphCenter(DoublePoint c)
        {
            graphCenter = c;
        }

        public DoublePoint Get_GraphSize()
        {
            return graphSize;
        }

        public void Set_GraphSize(DoublePoint s)
        {
        	graphSize = s;
        }

        //min and max are computed when requested
        //most things should be done with size and center

        public void Set_MinMax(DoublePoint Min, DoublePoint Max)
        {
            if(Min.x <= Max.x && Min.y <= Max.y)
            {
                graphCenter.x = (Max.x + Min.x) / 2;
                graphCenter.y = (Max.y + Min.y) / 2;

                graphSize.x = Max.x - Min.x;
                graphSize.y = Max.y - Min.y;
            }
            else
            {
                Terminal.Msg (true, "NOPE", "Bad Min_Max Set");
            }
        }

        public DoublePoint Get_Min()
        {
            return new DoublePoint (graphCenter.x - graphSize.x/2, graphCenter.y - graphSize.y/2);
        }

        public void Set_Min(DoublePoint m)
        {
            Set_MinMax(m, Get_Max());
        }

        public DoublePoint Get_Max()
        {
            return new DoublePoint (graphCenter.x + graphSize.x/2, graphCenter.y + graphSize.y/2);
        }

        public void Set_Max(DoublePoint m)
        {
            Set_MinMax(Get_Min(), m);
        }

        public IntPoint Get_AsciiSize()
        {
            return asciiSize;
        }

        public void Set_AsciiSize(IntPoint s)
        {
            asciiSize = s;
        }

        public TransformAG (int asciiW,int asciiH, double graphX, double graphY, double graphW, double graphH)
        {
            asciiSize = new IntPoint (asciiW, asciiH);
            graphCenter = new DoublePoint (graphX, graphY);
            graphSize = new DoublePoint (graphW, graphH);
        }

        public IntPoint GraphToAsciiTrans(DoublePoint gPoint)
        {
            IntPoint output = new IntPoint(
                //Transform Equetions for converting somthing a function out puts to something that can be more esily, drawn latter and worked with.
                gPoint.isXNaN ? 0 : Convert.ToInt32( (gPoint.x - graphCenter.x) / Get_GARatio().x + asciiSize.x/2), 
                gPoint.isYNaN ? 0 : Convert.ToInt32( (gPoint.y - graphCenter.y) / Get_GARatio().y + asciiSize.y/2) 
            );

            output.isXNaN = gPoint.isXNaN;
            output.isYNaN = gPoint.isYNaN;

            return output;
        }

        public DoublePoint AsciiToGraphTrans(IntPoint aPoint)
        {
            DoublePoint output = new DoublePoint(
                //Transform Equations for converting a point on the Ascii graph to somthing that a function can take
                aPoint.isXNaN ? Double.NaN : (Convert.ToDouble((aPoint.x - asciiSize.x/2) * Get_GARatio().x + graphCenter.x)),
                aPoint.isYNaN ? Double.NaN : (Convert.ToDouble((aPoint.y - asciiSize.y/2) * Get_GARatio().y + graphCenter.y))
            );

            output.isXNaN = aPoint.isXNaN;
            output.isYNaN = aPoint.isYNaN;

            return output;
        }
    }
}

