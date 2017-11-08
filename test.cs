using System;

namespace Grapher
{
    class TestFunc : IMFunc
    {
        public double func(double x)
        {
            return Math.Acos(x);
        }
    }


class TransformTester
{
    // if the test works both of the points should come back (0,0)
    public TransformTest Runtest(TransformAG trans)
    {
        TransformTest tTest = new TransformTest();

        IntPoint AtoGtoA = new IntPoint(0, 0);
        Point GtoAtoG = new Point (0.0, 0.0);

        AtoGtoA = trans.GraphToAsciiTrans (trans.AsciiToGraphTrans (AtoGtoA));
        GtoAtoG = trans.AsciiToGraphTrans (trans.GraphToAsciiTrans (GtoAtoG));

        tTest.AtoGtoA = AtoGtoA;
        tTest.GtoAtoG = GtoAtoG;

        return tTest;
    }
}

    struct TransformTest
    {
        public IntPoint AtoGtoA;
        public Point GtoAtoG;
    }
}
