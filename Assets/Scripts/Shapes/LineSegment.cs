using System;
using UnityEngine;

namespace Shapes
{
    [Serializable]
    public class LineSegment
    {
        public Vector2 A;
        public Vector2 B;

        public LineSegment(Vector2 a, Vector2 b)
        {
            A = a;
            B = b;
        }

        public static bool ArePointsOnSegment(Vector2 p, Vector2 q, Vector2 r)
        {
            return q.x <= Mathf.Max(p.x, r.x) && q.x >= Mathf.Min(p.x, r.x) &&
                   q.y <= Mathf.Max(p.y, r.y) && q.y >= Mathf.Min(p.y, r.y);
        }

        public static int Orientation(Vector2 p, Vector2 q, Vector2 r)
        {
            float val = (q.y - p.y) * (r.x - q.x) -
                      (q.x - p.x) * (r.y - q.y);
 
            // collinear
            if (val == 0) return 0; 
 
            // clock or counterclock wise
            return val > 0? 1: 2; 
        }
        
        public static bool IsIntersecting(LineSegment lhs, LineSegment rhs)
        {
            Vector2 p1 = lhs.A;
            Vector2 q1 = lhs.B;
            Vector2 p2 = rhs.A;
            Vector2 q2 = rhs.B;
            
            // Find the four orientations needed for general and
            // special cases
            int o1 = Orientation(p1, q1, p2);
            int o2 = Orientation(p1, q1, q2);
            int o3 = Orientation(p2, q2, p1);
            int o4 = Orientation(p2, q2, q1);
 
            // General case
            if (o1 != o2 && o3 != o4)
                return true;
 
            // Special Cases
            // p1, q1 and p2 are collinear and p2 lies on segment p1q1
            if (o1 == 0 && ArePointsOnSegment(p1, p2, q1)) return true;
 
            // p1, q1 and q2 are collinear and q2 lies on segment p1q1
            if (o2 == 0 && ArePointsOnSegment(p1, q2, q1)) return true;
 
            // p2, q2 and p1 are collinear and p1 lies on segment p2q2
            if (o3 == 0 && ArePointsOnSegment(p2, p1, q2)) return true;
 
            // p2, q2 and q1 are collinear and q1 lies on segment p2q2
            if (o4 == 0 && ArePointsOnSegment(p2, q1, q2)) return true;
 
            return false; // Doesn't fall in any of the above cases
        }
    }
}