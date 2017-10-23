using System;
using System.Collections.Generic;
namespace Grapher
{
    public interface IGraphable
    {
		List<CharPoint> getPoints(GraphWidget window);
    }
}