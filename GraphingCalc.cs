using System;
using CursesSharp;
using CursesSharp.Gui;

namespace Grapher
{
    public class GraphingCalc : Container
    {
        GraphWidget graphWidget;

        public GraphingCalc (int x,int y, int w, int h) : base(x, y, w, h)
        {
            graphWidget = new GraphWidget (x, y, w - 2, h - 2);
            DrawFrame(x, y, w, h);
            graphWidget.graphs.Add (new FuncGraph(new TestFunc(), graphWidget.trans));
            Add (graphWidget);
        }
    }
}

