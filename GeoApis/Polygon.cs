using System;
using System.Collections.Generic;
using System.Text;

namespace GeoApis.Custom
{

    class PolygonPoint
    {
        public System.Decimal X; // Lat
        public System.Decimal Y; // Long

        

        public PolygonPoint(decimal x, decimal y)
        {
            this.X = x;
            this.Y = y;
        }

        public PolygonPoint()
            :this(0,0)
        { }

    }


    class Polygon
    {

        public List<PolygonPoint> Points = new List<PolygonPoint>();

        public void PopulateV1()
        {
            Points.Add(new PolygonPoint(1, 1));
            Points.Add(new PolygonPoint(5, 1));
            Points.Add(new PolygonPoint(5, 2));
            // Points.Add(new PolygonPoint(1, 2));
            // Points.Add(new PolygonPoint(1, 1));
        }


        public bool IsClosed
        {
            get
            {
                decimal x0 = this.Points[0].X;
                decimal y0 = this.Points[0].Y;

                decimal xN = this.Points[this.Points.Count - 1].X;
                decimal yN = this.Points[this.Points.Count - 1].Y;

                if (x0 == xN && y0 == yN)
                    return true;

                return false;
            }
        }



        public void Close()
        {
            Points.Add(new PolygonPoint(this.Points[0].X, this.Points[0].Y));
        }

        public void UnClose()
        {
            Points.RemoveAt(Points.Count - 1);
        }


        public void EnsureClosed()
        {
            if (!this.IsClosed)
                this.Close();
        }


        public void EnsureUnclosed()
        {
            if (this.IsClosed)
                UnClose();
        }


        public decimal MathematicalArea
        {
            get
            {
                decimal nArea = 0m;

                for (int i = 0; i < this.Points.Count; ++i)
                {
                    if (this.Points[i] != null)
                    {
                        if (i != this.Points.Count - 1)
                        {
                            nArea += Points[i].X * Points[i + 1].Y - Points[i + 1].X * Points[i].Y;
                        }
                        else
                        {
                            nArea += Points[i].X * Points[0].Y - Points[0].X * Points[i].Y;
                        }

                    } // End if(this.Points[i] != null)

                } // Next point

                nArea *= 0.5m;
                return nArea;
            }
        }


        public decimal Area
        {
            get
            {
                return Math.Abs(this.MathematicalArea);
            }
        }


        // Geschlossen
        public PolygonPoint Centroid
        {
            get
            {
                decimal accumulatedArea = 0.0m;
                decimal centerX = 0.0m;
                decimal centerY = 0.0m;

                bool wasClosed = this.IsClosed;
                this.EnsureClosed();

                for (int i = 0, j = this.Points.Count - 1; i < this.Points.Count; j = i++)
                {
                    decimal temp = this.Points[i].X * this.Points[j].Y - this.Points[j].X * this.Points[i].Y;
                    accumulatedArea += temp;
                    centerX += (this.Points[i].X + this.Points[j].X) * temp;
                    centerY += (this.Points[i].Y + this.Points[j].Y) * temp;
                }

                if (!wasClosed)
                    this.EnsureUnclosed();


                if (Math.Abs(accumulatedArea) < 1E-7m)
                    return new PolygonPoint();  // Avoid division by zero

                accumulatedArea *= 3m;
                return new PolygonPoint(centerX / accumulatedArea, centerY / accumulatedArea);
            }
        }

        // Nicht geschlossen
        public PolygonPoint Midpoint
        {
            get
            {
                decimal x = 0m;
                decimal y = 0m;

                bool wasClosed = this.IsClosed;
                this.EnsureUnclosed();

                for (int i = 0; i < this.Points.Count; ++i)
                {
                    x += this.Points[i].X;
                    y += this.Points[i].Y;
                } // Next i 

                x /= (this.Points.Count);
                y /= (this.Points.Count);

                if (wasClosed)
                    this.EnsureClosed();

                return new PolygonPoint(x, y);
            }
        }

    }




}
