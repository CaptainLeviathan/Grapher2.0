namespace Grapher
{
    public static class CommandCenter
    {
        public static void Main()
        {
            TestFunc testFunc = new TestFunc();
            WindowManager.graphWindow.graphs.Add(new FuncGraph(testFunc));
            WindowManager.render();
        }
    }
}