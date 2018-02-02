using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MapAPITest.Models
{
    public class Map
    {
        public Map()
        {
            this.Grid = new List<Point>();
        }
        public Point StartingPoint { get; set; }
        public Point EndPoint { get; set; }
        public Point CurrentPoint { get; set; }
        public List<Point> Grid { get; set; }
        public int MaximumY { get; set; }
        public int MaximumX { get; set; }
        public bool ReachedDestination { get; set; }

        public bool MoveUp(bool Reverse = false) { return Move(0, -1, Reverse); }
        public bool MoveDown(bool Reverse = false) { return Move(0, 1, Reverse); }
        public bool MoveRight(bool Reverse = false) { return Move(1, 0, Reverse); }
        public bool MoveLeft(bool Reverse = false) { return Move(-1, 0, Reverse); }

        public bool Move(int XMove, int YMove, bool Reverse)
        {
            int X = this.CurrentPoint.XValue + XMove;
            int Y = this.CurrentPoint.YValue + YMove;

            var point = Grid.Where(g => g.XValue == X && g.YValue == (Y)).SingleOrDefault();
            if(point == null)//for when it is beyond the text
            {
                return false;
            }
            if (point.Value == '.')
            {
                point.Value = '@';
                this.CurrentPoint.YValue = Y;
                this.CurrentPoint.XValue = X;
                return true;
            }
            else if(point.Value == 'B')
            {
                this.CurrentPoint.YValue = Y;
                this.CurrentPoint.XValue = X;
                this.ReachedDestination = true;
                return true;
            }
            else if(Reverse)
            {
                if (point.Value != 'A')
                {
                    point.Value = '@';
                }
                var oldPoint = Grid.Where(g => g.XValue == this.CurrentPoint.XValue 
                                    && g.YValue == this.CurrentPoint.YValue).SingleOrDefault();
                oldPoint.Value = ',';//To Block that path
                this.CurrentPoint.YValue = Y;
                this.CurrentPoint.XValue = X;
                return true;
            }
            else
            {
                return false;
            }
        }

        public string ConvertGridToString()
        {
            StringBuilder sbuild = new StringBuilder();
            for(int y = 0; y != this.MaximumY; y++)
            {
                var row = Grid.Where(g => g.YValue == y).ToList();
                foreach(var column in row)
                {
                    sbuild.Append(column.Value);
                }
                sbuild.AppendLine();
            }

            return sbuild.ToString().Replace(',','.');
        }
    }

    public class Point
    {
        public Point(int X, int Y)
        {
            this.XValue = X;
            this.YValue = Y;
        }

        public Point(int X, int Y, char Value)
        {
            this.XValue = X;
            this.YValue = Y;
            this.Value = Value;
        }
        public int XValue { get; set; }
        public int YValue { get; set; }
        public char Value { get; set; }
    }
}