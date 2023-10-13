using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20230715_HW_OOP_SeaBattle_
{
    public struct Coordinate
    {
        public int X { get; }

        public int Y { get; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Coordinate left, Coordinate right)
        {
            return (left.X == right.X) && (left.Y == right.Y);
        }

        public static bool operator !=(Coordinate left, Coordinate right)
        {
            return !(left == right);
        }
    }
}