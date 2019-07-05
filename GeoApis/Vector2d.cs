
using System;

namespace GeoApis.Vectors
{


    // import Vectors.cVector_2d;
    public class cVector_2d
    {
        public bool bCurrentlyValid = true;
        public decimal x = 0;
        public decimal y = 0;
        public decimal z = 0; // Yay, for 3D abuse in 2D


        //Constructor
        public cVector_2d(decimal nXparam = 0, decimal nYparam = 0, decimal nZparam = 0)
        {
            this.bCurrentlyValid = true;
            this.x = nXparam;
            this.y = nYparam;
            this.z = nZparam;
        } // End Constructor


        //    public function toString():String
        //		{
        //			var strReturnValue:String = "[" + this.x + ", " + this.y + "]"
        //			return strReturnValue
        //}


        // cVector_2d.GetRelativeLength();
        public decimal GetRelativeLength()
        {
            double nReturnValue = Math.Pow((double)this.x, 2) + Math.Pow((double)this.y, 2);
            return (decimal)nReturnValue;
        } // End function GetRelativeLength


        // cVector_2d.GetLength();
        public decimal GetLength()
        {
            double nReturnValue = Math.Pow((double)this.x, 2) + Math.Pow((double)this.y, 2) + Math.Pow((double)this.z, 2);
            nReturnValue = Math.Sqrt(nReturnValue);
            return (decimal)nReturnValue;
        } // End function VectorLength


        // cVector_2d.MakeVector(endx, endy [, startx, starty]);
        //public static function MakeVector(nX1param:Number, nY1param:Number, nX0param:Number= 0, nY0param:Number= 0):cVector_2d
        //{
        //	var vecReturnValue:cVector_2d = new cVector_2d(nX1param-nX0param, nY1param-nY0param);
        //	return vecReturnValue;
        //} // End function MakeVector


        // cVector_2d.cPoint2Vector(cptPoint);
        //public static function cPoint2Vector(cptPoint:cPoint):cVector_2d
        //{
        //	var vecReturnValue:cVector_2d= new cVector_2d(cptPoint.x, cptPoint.y);
        //	return vecReturnValue;
        //} // End function MakeVector


        // cVector_2d.VectorSubtract(vec2, vec1); // vec2-vec1
        public static cVector_2d VectorSubtract(cVector_2d b, cVector_2d a)
        {
            cVector_2d vecReturnValue = new cVector_2d(b.x - a.x, b.y - a.y);
            return vecReturnValue;
        } // End function VectorSubtract


        // cVector_2d.VectorAdd(vec1, vec2);
        public static cVector_2d VectorAdd(cVector_2d a, cVector_2d b)
        {
            cVector_2d vecReturnValue = new cVector_2d(a.x + b.x, a.y + b.y);
            return vecReturnValue;
        } // End function VectorAdd


        // cVector_2d.CrossP(vec1, vec2); // z= det([vecA,vecB])
        public static cVector_2d CrossP(cVector_2d a, cVector_2d b)
        {
            cVector_2d vecReturnValue = new cVector_2d(0, 0); // Faster than 3d-version
            vecReturnValue.z = a.x * b.y - a.y * b.x; //  Yay, z-Abuse in 2d
            return vecReturnValue;
        } // End function CrossP


        // cVector_2d.DotP(vec1, vec2);
        public static decimal DotP(cVector_2d a, cVector_2d b)
        {
            //A * B = ax*bx+ay*by+az*bz
            decimal nReturnValue = a.x * b.x + a.y * b.y + a.z * b.z; // Z is zero in 2D
            return nReturnValue;
        } // End function DotP


        // cVector_2d.VectorLength(vec);
        public static decimal VectorLength(cVector_2d vec)
        {
            double nReturnValue = Math.Pow((double)vec.x, 2) + Math.Pow((double)vec.y, 2) + Math.Pow((double)vec.z, 2);
            nReturnValue = Math.Sqrt(nReturnValue);
            return (decimal)nReturnValue;
        } // End function VectorLength


        // cVector_2d.VectorNorm(vec);
        public static decimal VectorNorm(cVector_2d vec)
        {
            return VectorLength(vec);
        } // End function VectorNorm


        // cVector_2d.CrossP(vec1, vec2);
        public static cVector_2d UnitVector(cVector_2d vec)
        {
            decimal len = VectorLength(vec);
            cVector_2d vecReturnValue = new cVector_2d(vec.x / len, vec.y / len);
            return vecReturnValue;
        } // End function UnitVector


        // cVector_2d.Angle_Rad(vec1, vec2);
        public static decimal Angle_Rad(cVector_2d a, cVector_2d b)
        {
            double nReturnValue =
            Math.Acos(
                        (
                            (double)(a.x * b.x + a.y * b.y + a.z * b.z) /
                                (Math.Sqrt(Math.Pow((double)b.x, 2) + Math.Pow((double)b.y, 2) + Math.Pow((double)b.z, 2))

                                * Math.Sqrt(Math.Pow((double)a.x, 2) + Math.Pow((double)a.y, 2) + Math.Pow((double)a.z, 2))
                            )
                        )

                    );
            return (decimal)nReturnValue;
        }  // End function Angle_Rad


        // cVector_2d.Angle_Degrees(vec1, vec2);
        public static decimal Angle_Degrees(cVector_2d a, cVector_2d b)
        {
            double nReturnValue =
            Math.Acos(
                        (
                            (double)(a.x * b.x + a.y * b.y + a.z * b.z) /
                                (Math.Sqrt(Math.Pow((double)b.x, 2) + Math.Pow((double)b.y, 2) + Math.Pow((double)b.z, 2))

                                * Math.Sqrt(Math.Pow((double)a.x, 2) + Math.Pow((double)a.y, 2) + Math.Pow((double)a.z, 2))
                            )
                        )

                    );
            return (decimal)(nReturnValue * 180.0 / Math.PI);
        }  // End function Angle_Degrees


        // http://mathworld.wolfram.com/Point-LineDistance3-Dimensional.html
        // cVector_2d.DistanceOfPointToLine(vecP, vecA, vecB)
        public static decimal DistanceOfPointToLine(cVector_2d x0, cVector_2d x1, cVector_2d x2)
        {
            cVector_2d x2minusx1 = VectorSubtract(x2, x1); // vec2-vec1
                                                           //trace("x2minusx1: " + x2minusx1.toString());
            cVector_2d x1minusx0 = VectorSubtract(x1, x0); // vec2-vec1
                                                           //trace("x1minusx0: " + x1minusx0.toString());
            cVector_2d DeltaVector = CrossP(x2minusx1, x1minusx0); // Yay, 2D crossp = determinant = scalar result = saved in z ;-)
                                                                   //trace("DeltaVector.z: " + DeltaVector.z);

            decimal DeltaNorm = VectorNorm(DeltaVector); // Get the scalar value from crossp z, z=sqrt(z^2) ;-)
                                                         //trace("DeltaNorm: " + DeltaNorm);
            decimal x2minusx1Norm = VectorNorm(x2minusx1);
            //trace("x2minusx1Norm: " + x2minusx1Norm);
            decimal nReturnValue = DeltaNorm / x2minusx1Norm;
            return nReturnValue;
        }


        // var vec:cVector_2d=cVector_2d.GetNormalVector(vec2_VecLine);
        public static cVector_2d GetNormalVector(cVector_2d vec)
        {
            cVector_2d vecReturnValue = new cVector_2d(-vec.y, vec.x, 0);
            return vecReturnValue;
        }


        public class cPoint
        {
            public bool bHasInterSection;
            public decimal x;
            public decimal y;
        }

        // cVector_2d.GetPointVerticalIntersection(aptDefinitionPoints[i], vec2_VecLine, cptPointToAdd);
        public static cPoint GetPointVerticalIntersection(cPoint cptPointQ, cVector_2d vecInputLine, cPoint cpPointP)
        {
            // Q: Line start/endpoint
            // P + a * vecNormalVector = Intersection = Q + b * vecInputLine
            // a = (-(Px*vy-Py*vx-Qx*vy+Qy*vx))/(nx*vy-ny*vx)
            // b = (-(Px*ny-Py*nx-Qx*ny+Qy*nx))/(nx*vy-ny*vx)

            cVector_2d vecNormalVector = GetNormalVector(vecInputLine);
            decimal nDenominator = vecNormalVector.x * vecInputLine.y - vecNormalVector.y * vecInputLine.x;


            cPoint cptIntersectionPoint = new cPoint();
            if (nDenominator == 0)
            {
                // no intersection
                cptIntersectionPoint.bHasInterSection = false;
                return cptIntersectionPoint;
            }

            decimal a = (-(cpPointP.x * vecInputLine.y - cpPointP.y * vecInputLine.x - cptPointQ.x * vecInputLine.y + cptPointQ.y * vecInputLine.x)) / nDenominator;
            decimal b = (-(cpPointP.x * vecNormalVector.y - cpPointP.y * vecNormalVector.x - cptPointQ.x * vecNormalVector.y + cptPointQ.y * vecNormalVector.x)) / nDenominator;
            cptIntersectionPoint.bHasInterSection = true;

            cptIntersectionPoint.x = cpPointP.x + a * vecNormalVector.x;
            cptIntersectionPoint.y = cpPointP.y + a * vecNormalVector.y;

            return cptIntersectionPoint;
        }


        // cVector_2d.isPointOnLine(cpLinePoint1, cpLinePoint2, cpPointInQuestion);
        public static bool isPointOnLine(cPoint cptLinePoint1, cPoint cptLinePoint2, cPoint cptPoint)
        {
            cPoint cptPointA = new cPoint();
            cPoint cptPointB = new cPoint();

            if (cptLinePoint1.x < cptLinePoint2.x)
            {
                cptPointA.x = cptLinePoint1.x;
                cptPointB.x = cptLinePoint2.x;
            }
            else
            {
                cptPointA.x = cptLinePoint2.x;
                cptPointB.x = cptLinePoint1.x;
            }

            if (cptLinePoint1.y < cptLinePoint2.y)
            {
                cptPointA.y = cptLinePoint1.y;
                cptPointB.y = cptLinePoint2.y;
            }
            else
            {
                cptPointA.y = cptLinePoint2.y;
                cptPointB.y = cptLinePoint1.y;
            }


            if (cptPoint.x >= cptPointA.x && cptPoint.y >= cptPointA.y && cptPoint.x <= cptPointB.x && cptPoint.y <= cptPointB.y)
            {
                return true;
            }

            return false;
        } // End function isPointOnLine


        /*
		// var xy:Point=cVector_2d.GetPolygonCenter(pt1, pt2,..., ptn);
		public static function GetPolygonCenter(... args):Point
		{
			// http://www.flex888.com/574/can-i-have-a-variable-number-of-arguments-in-as3.html
			var sum:Point = new Point(0, 0);
			for(var i:int = 0; i<args.length;i++)
			{
				sum.x += args[i].x;
				sum.y += args[i].y;
			}
			
			sum.x/=args.length;
			sum.y/=args.length;
			return sum;
		} // End function GetPolygonCenter
		
		*/

        /*
		// var xy:Point=cVector_2d.GetIntersection(vec1, vec2);
		public static function GetIntersection ( v1:cVector_2d , v2:cVector_2d ):Point
		{
			trace( "cVector_2d.GetIntersection" );
			
			var v3bx:Number = v2.x - v1.x;
			var v3by:Number = v2.y - v1.y;
			var perP1:Number = v3bx*v2.by - v3by*v2.bx;
			var perP2:Number = v1.bx*v2.by - v1.by*v2.bx;
			var t:Number = perP1/perP2;
			
			var cx:Number = v1.x + v1.bx*t;
			var cy:Number = v1.y + v1.by*t;
			return new Point( cx , cy );
		}
		*/


    } // End cPoint 

} // End Package

// http://mathworld.wolfram.com/NormalVector.html
// http://www.softsurfer.com/Archive/algorithm_0104/algorithm_0104B.htm
// http://www.cs.mtu.edu/~shene/COURSES/cs3621/NOTES/curves/normal.html
// http://en.wikibooks.org/wiki/Linear_Algebra/Orthogonal_Projection_Into_a_Line
