using System;
using System.Timers;
using CursesSharp.Gui;
using CursesSharp;

namespace Grapher
{
    public static class TermResize
    {

        public static event EventHandler Resized;
        static Timer checkColsLines;
        static int lastCols;
        static int lastLines;

        public static void Start()
        {
            lastCols = Terminal.Cols;
            lastLines = Terminal.Lines;

            checkColsLines = new Timer (10);
            checkColsLines.AutoReset = true;
            checkColsLines.Elapsed += new ElapsedEventHandler (CheckIfResisized);

            checkColsLines.Enabled = true;
        }

        public static void End()
        {
            checkColsLines.Enabled = false;
        }

        static void CheckIfResisized(object sender, ElapsedEventArgs e)
        {
            if (Terminal.Cols != lastCols || Terminal.Lines != lastLines)
            {
                if (Resized != null) 
                {
                    Resized (null, EventArgs.Empty);
                }
            }

            lastCols = Terminal.Cols;
            lastLines = Terminal.Lines;
        }
    }
}

