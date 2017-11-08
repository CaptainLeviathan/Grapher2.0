using System;

namespace Grapher
{
    public interface ITransformAG
    {
        IntPoint GraphToAsciiTrans(Point gPoint);

        Point AsciiToGraphTrans(IntPoint aPoint);

        Point Get_GARatio();

        Point Get_GraphCenter();


        void Set_GraphCenter(Point c);

        Point Get_GraphSize();

        void Set_GraphSize(Point s);


        void Set_MinMax(Point Min, Point Max);

        Point Get_Min();

        void Set_Min(Point m);

        Point Get_Max();

        void Set_Max(Point m);


        IntPoint Get_AsciiSize();

        void Set_AsciiSize(IntPoint s);
    }
}

