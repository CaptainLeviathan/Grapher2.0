using System;

namespace Grapher
{
    public interface ITransformAG
    {
        IntPoint GraphToAsciiTrans(DoublePoint gPoint);

        DoublePoint AsciiToGraphTrans(IntPoint aPoint);

        DoublePoint Get_GARatio();

        DoublePoint Get_GraphCenter();


        void Set_GraphCenter(DoublePoint c);

        DoublePoint Get_GraphSize();

        void Set_GraphSize(DoublePoint s);


        void Set_MinMax(DoublePoint Min, DoublePoint Max);

        DoublePoint Get_Min();

        void Set_Min(DoublePoint m);

        DoublePoint Get_Max();

        void Set_Max(DoublePoint m);


        IntPoint Get_AsciiSize();

        void Set_AsciiSize(IntPoint s);
    }
}

