using System;
using CursesSharp.Gui;
using CursesSharp;


namespace Grapher
{
    public class GraphContainer : Container
    {
        public GraphWidget graphWid;

        public GraphContainer (int x,int y, int w, int h) : base(x, y, w, h)
        {
            DrawFrame (x,y,w,h);
            graphWid = new GraphWidget (x + 1, y + 1, w - 2, h - 2);
            Add (graphWid);
        }
    }
}

