using System;
using System.Collections.Generic;

namespace Grapher
{
    public class Axes : IGraphable
    {
        DoublePoint Min;
        DoublePoint Max;

        DoublePoint Ratio;

        public List<CharPoint> getPoints(ITransformAG trans)
        {
            List<CharPoint> axes = new List<CharPoint> ();

            Min = trans.Get_Min ();
            Max = trans.Get_Max ();

            Ratio = trans.Get_GARatio ();

            X_Axis (trans, ref axes);
            Y_Axis (trans, ref axes);

            return axes;
        }

        void X_Axis(ITransformAG trans, ref List<CharPoint> axes)
        {

            DoublePoint x_Point = new DoublePoint (Min.x, 0);

            while(x_Point.x <= Max.x)
            {
                CharPoint outPoint = new CharPoint (trans.GraphToAsciiTrans (x_Point));

                outPoint.symbol = '_';

                axes.Add (outPoint);

                x_Point.x += Ratio.x;
            }
        }

        void Y_Axis(ITransformAG trans, ref List<CharPoint> axes)
        {
            DoublePoint y_Point = new DoublePoint (0, Min.y);

            while(y_Point.y <= Max.y)
            {
                CharPoint outPoint = new CharPoint (trans.GraphToAsciiTrans (y_Point));

                outPoint.symbol = '|';

                axes.Add (outPoint);

                y_Point.y += Ratio.y;
            }
        }
    }
}

