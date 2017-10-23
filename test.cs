using System;

namespace Grapher
{
    class TestFunc : IMFunc
    {
        public double func(double x)
        {
			return Math.Atan(x);
        }
    }


	class TransformTester
	{
		// if the test works both of the points should come back (0,0)
		public TransformTest Runtest(GraphWidget gW)
		{
			TransformTest tTest = new TransformTest();

			IntPoint AtoGtoA = new IntPoint(0, 0);
			DoublePoint GtoAtoG = new DoublePoint (0.0, 0.0);

			AtoGtoA = gW.GraphToAsciiTrans (gW.AsciiToGraphTrans (AtoGtoA));
			GtoAtoG = gW.AsciiToGraphTrans (gW.GraphToAsciiTrans (GtoAtoG));

			tTest.AtoGtoA = AtoGtoA;
			tTest.GtoAtoG = GtoAtoG;

			return tTest;
		}
	}

	struct TransformTest
	{
		public IntPoint AtoGtoA;
		public DoublePoint GtoAtoG;
	}
}
