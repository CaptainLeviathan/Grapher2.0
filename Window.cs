using System.Collections.Generic;

namespace Grapher
{
    class Window
    {
        public int with;
        public int hight; 
        public int centerX;
        public int centerY;
        public List<CharPoint> charPoints;

        public Window(int with, int hight, int centerX, int centerY)
        {
            charPoints = new List<CharPoint>();
            this.with = with;
            this.hight = hight;
            this.centerX = centerX;
            this.centerY = centerY;
        }
        public virtual void getPoints()
        {

        }
    }
}