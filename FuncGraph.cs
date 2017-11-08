using System;
using System.Collections.Generic;

namespace Grapher
{
    class FuncGraph : IGraphable
    {
        IMFunc mFunc;

        ITransformAG trans;

        public FuncGraph(IMFunc mFunc, ITransformAG trans)
        {
            this.mFunc = mFunc;
            this.trans = trans;
        }

        //returns completed charpoints to go and get placed on the graphwindow
        public List<CharPoint> getPoints (ITransformAG trans)
        {
            this.trans = trans;

            List<CharPoint> ploted = new List<CharPoint>();

            IntPoint lastPoint = new IntPoint(-2,0);
            IntPoint point = new IntPoint(-1,0);
            IntPoint nextPoint = new IntPoint(0,0);

            evalPoint(ref lastPoint);
            evalPoint(ref point);
            evalPoint(ref nextPoint);

            int deltaY;

            for (int x = 0; x < trans.Get_AsciiSize().x; x++)
            {
                //points for next iteration
                lastPoint = point;
                point = nextPoint;
                nextPoint.x = x + 2;
                evalPoint(ref nextPoint);

                //null checkes
                if(point.isNaN)
                {
                  continue;
                }

                if (lastPoint.isNaN) 
                {
                  ploted.Add(new CharPoint(point.x, point.y, '('));
                   continue;
                }

                if (nextPoint.isNaN) 
                {
                  ploted.Add(new CharPoint(point.x, point.y, ')'));
                  continue;
                }

                //getting the change in y vaule the last point
                deltaY = nextPoint.y - lastPoint.y;

                //checks if the point is strateled between the last point and the next point
                if(point.y <= nextPoint.y && point.y >= lastPoint.y || point.y >= nextPoint.y && point.y <= lastPoint.y)
                {
                    if(deltaY >= -2 && deltaY <= 2)
                    {
                        //this is for the strate line case
                        //      ---
                        if(deltaY == 0)
                        {
                            ploted.Add(new CharPoint(point.x, point.y, '_'));
                           continue;
                        }

                        // this covers
                        //    _    _
                        //  _/  ,   \_
                        else if(deltaY <= 1 && deltaY >= -1)
                        {
                            if(point.y == lastPoint.y && deltaY == 1)
                            {
                                ploted.Add(new CharPoint(point.x, point.y, '/'));
                                continue;
                            }
                            else if(point.y == nextPoint.y && deltaY == -1)
                            {
                                ploted.Add(new CharPoint(point.x, point.y, (char)92));
                                continue;
                            }
                            else
                            {
                                ploted.Add(new CharPoint(point.x, point.y, '_'));
                                continue;
                            }
                        }
                        //this covers theses cases
                        //     /
                        //    /
                        //   /
                        else if(deltaY > 0)
                        {
                            ploted.Add(new CharPoint(point.x, point.y, '/'));
                            continue;
                        }
                        //this covers theses cases
                        //     \
                        //      \
                        //       \
                        else if(deltaY < 0)
                        {
                            ploted.Add(new CharPoint(point.x, point.y, (char)92));
                            continue;
                        }
                    }
                    else
                    {
                        //this is the intended behavior
                        //     _
                        //    /
                        //    |
                        //    |
                        // __/
                        //     
                        //    ._
                        //    |
                        //    |
                        // __/
                        if(deltaY > 0)
                        {
                            for (int y = lastPoint.y + 1; y < nextPoint.y; y++)
                            {
                                ploted.Add(new CharPoint(point.x, y, '|'));
                            }

                            if(point.y == nextPoint.y)
                            {
                                ploted.Add(new CharPoint(point.x, point.y, '.'));
                            }
                            else
                            {
                                ploted.Add(new CharPoint(point.x, point.y, '/'));                         
                            }

                            continue;
                        }
                        else if (deltaY < 0)
                        {
                            for (int y = lastPoint.y - 1; y > nextPoint.y; y--)
                            {
                                ploted.Add(new CharPoint(point.x, y, '|'));
                            }
                                if(point.y == nextPoint.y)
                            {
                                ploted.Add(new CharPoint(point.x, point.y, ' '));
                            }
                            else
                            {
                                ploted.Add(new CharPoint(point.x, point.y, '/'));                         
                            }
                            continue;
                        }
                    }
                }
                else
                {
                    ploted.Add(new CharPoint(point.x, point.y, '*'));
                    continue;
                }
            }

            //return all of the newly minteted points
            return ploted;
        }

        void evalPoint(ref IntPoint Ip)
        {
            Point gp = new Point(0, 0);
            gp = trans.AsciiToGraphTrans(Ip);
            gp.y = mFunc.func(gp.x);
            Ip = trans.GraphToAsciiTrans(gp);
        }
    }
}