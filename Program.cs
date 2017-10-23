using System;
using CursesSharp.Gui;
using CursesSharp;

namespace Grapher
{
	class MainClass
	{
		static GraphingCalc grapher;

		public static void Main (string[] args)
		{
			Terminal.Init (false);
			TermResize.Start ();
			grapher = new GraphingCalc(1, 1, Terminal.Cols - 2, Terminal.Lines - 2);
			Terminal.Run (grapher);
			TermResize.End ();
		}
	}
}
