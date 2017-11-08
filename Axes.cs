using System;
using System.Collections.Generic;

namespace Grapher
{
    public class Axes : IGraphable
    {
        IntPoint AsciiSize;

        IntPoint AsciiCenter;

        Point Zero;

        public Axes()
        {
            Zero = new Point(0.0, 0.0);
        }

        public List<CharPoint> getPoints(ITransformAG trans)
        {
            List<CharPoint> axes = new List<CharPoint> ();

            AsciiSize = trans.Get_AsciiSize();

            AsciiCenter = trans.GraphToAsciiTrans(Zero);

            X_Axis (trans, ref axes);
            Y_Axis (trans, ref axes);

            return axes;
        }

        void X_Axis(ITransformAG trans, ref List<CharPoint> axes)
        {

            if(AsciiCenter.y >= 0 && AsciiCenter.y <= AsciiSize.y)
            {
                for(int x = 0; x <= AsciiSize.x; ++x)
                {
                    axes.Add(new CharPoint(x, AsciiCenter.y, '_'));
                }
            }
           
        }

        void Y_Axis(ITransformAG trans, ref List<CharPoint> axes)
        {
            if(AsciiCenter.x >= 0 && AsciiCenter.x <= AsciiSize.x)
            {
                for(int y = 0; y <= AsciiSize.y; ++y)
                {
                    axes.Add(new CharPoint(AsciiCenter.x, y, '|'));
                }
            }
        }
    }
}

