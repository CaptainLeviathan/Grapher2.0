using System;
using CursesSharp;
using CursesSharp.Gui;

namespace Grapher
{
	public class GraphingCalc : Container
	{
		GraphContainer graphCon;

		public GraphingCalc (int x,int y, int w, int h) : base(x, y, w, h)
		{
			graphCon = new GraphContainer (x, y, w, h);
			graphCon.graphWid.graphs.Add (new FuncGraph(new TestFunc(), graphCon.graphWid.trans));
			Add (graphCon);
		}
	}
}

