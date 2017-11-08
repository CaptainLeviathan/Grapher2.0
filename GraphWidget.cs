using System;
using System.Collections.Generic;
using CursesSharp.Gui;
using CursesSharp;

namespace Grapher
{
    public class GraphWidget : Widget
    {
        public bool HasAxes = true;
        public ITransformAG trans;
        public Point moveSpeed;
        public List<IGraphable> graphs;
        public Axes axes;

        public GraphWidget(int X, int Y, int with, int hight) : base(X, Y, with, hight)
        {
            CanFocus = true;
            trans = new TransformAG (with, hight, 0.0, 0.0, 5.0, 2.0);
            moveSpeed = new Point (1.0, 1.0);
            graphs = new List<IGraphable>();
            axes = new Axes ();
        }

        public GraphWidget(int X, int Y, int with, int hight, double graphX, double graphY, double graphW, double graphH) : base(X, Y, with, hight)
        {
            CanFocus = true;
            trans = new TransformAG (with, hight, graphX, graphY, graphW, graphH);
            moveSpeed = new Point (1.0, 1.0);
            graphs = new List<IGraphable>();
            axes = new Axes ();
        }


        //final transormation to flips the y-axis to be printed on the widget
        CharPoint AsciiToCollineTrans(CharPoint aPoint)
        {
            return new CharPoint(
                aPoint.x,
                h - aPoint.y
            );
        }

        //this is were the controls are prosses for zooming and 
        public override bool ProcessKey (int key)
        {

            Point graphCenter = trans.Get_GraphCenter();
            Point graphSize = trans.Get_GraphSize();
            Point StepSize = calcStepSize ();

            switch (key) 
            {
            case 'w':
                graphCenter.y += StepSize.y;
                break;
            case 'a':
                graphCenter.x -= StepSize.x;
                break;
            case 's':
                graphCenter.y -= StepSize.y;
                break;
            case 'd':
                graphCenter.x += StepSize.x;
                break;
            case 'r':
                graphSize.x -= 2 * StepSize.x;
                graphSize.y -= 2 * StepSize.y;
                break;
            case 'f':
                graphSize.x += 2 * StepSize.x;
                graphSize.y += 2 * StepSize.y;
                break;
            default:
                return false;
            }

            trans.Set_GraphCenter (graphCenter);
            trans.Set_GraphSize (graphSize);

            Redraw ();

            return true;
        }

        Point calcStepSize()
        {
            //computing the amount to move when you hit a key
            Point graphStep = new Point (0,0);

            graphStep.x = trans.Get_GARatio().x * moveSpeed.x;
            graphStep.y = trans.Get_GARatio().y * moveSpeed.y;

            return graphStep;
        }

        //gets called if the wigets size ever changes
        public override void DoSizeChanged ()
        {
            Redraw ();
        }

        //redraws the grapg window
        public override void Redraw()
        {
            List<CharPoint> charPoints = new List<CharPoint>();

            Stdscr.Attr =  (ColorNormal);

            //clearing screen bilt in function dose not work
            for(int col = 0; col < w; ++col)
            {
                for(int line = 0; line < h; ++line)
                {
                    this.Move (line + y, col + x);
                Stdscr.Add (' ');
                }
            }

            // geting all of the points
            if (HasAxes) 
            {
                charPoints.AddRange(axes.getPoints(trans));
            }

            for (int i = 0; i < graphs.Count; i++)
            {
                charPoints.AddRange(graphs[i].getPoints(trans));
            }

            //finlaly printing every thing to the screen
            CharPoint CollineP = new CharPoint(0,0);

            for (int i = 0; i < charPoints.Count; i++) 
            {
                CollineP = AsciiToCollineTrans (charPoints[i]);

                if (CollineP.x >= 0 && CollineP.y >= 0 && CollineP.x < w && CollineP.y < h)
                {
                    this.Move (CollineP.y + y, CollineP.x + x);
                    Stdscr.Add (charPoints [i].symbol);
                }
            }
        }
    }
}