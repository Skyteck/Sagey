// *****************************************************************************
// http://www.progware.org/blog/ - Collision Detection Algorithms
// *****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ExtendedTest
{

    public static class CollisionDetection2D
    {
        /// <summary>
        /// This should be initialize at the initialize of the main game loop as follows:
        /// <example>
        /// <code>
        /// CollisionDetection2D.AdditionalRenderTargetForCollision=new RenderTarget2D(_graphics.GraphicsDevice, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight,1,_graphics.GraphicsDevice.DisplayMode.Format);
        /// </code>
        /// </example>
        /// </summary>
        public static RenderTarget2D AdditionalRenderTargetForCollision { get; set; }

        /// <summary>
        /// Receives two lists of 4points representing a rectangle and checks whether the two
        /// shapes overlap
        /// </summary>
        /// <param name="Rect1">The 4 points of the first rectangle</param>
        /// <param name="Rect2">The 4 points of the second rectangle</param>
        /// <returns>True if they overlap/False otherwise</returns>
        public static bool BoundingRectangle (List<Vector2> Rect1,List<Vector2> Rect2)
        {
            List<Vector2> Triangle11;
            List<Vector2> Triangle12;
            _createTrianglesFromRectangle(Rect1, out Triangle11, out Triangle12);
            foreach (Vector2 point in Rect2)
            {
                if (IsPointInPolygon(point, Triangle11))
                {
                    return true;
                }
                if (IsPointInPolygon(point, Triangle12))
                {
                    return true;
                }
            }
            List<Vector2> Triangle21;
            List<Vector2> Triangle22;
            _createTrianglesFromRectangle(Rect2, out Triangle21, out Triangle22);
            foreach (Vector2 point in Rect1)
            {
                if (IsPointInPolygon(point, Triangle21))
                {
                    return true;
                }
                if (IsPointInPolygon(point, Triangle22))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks wheteher two circles overlap
        /// </summary>
        /// <param name="x1">The x coordinate of the center of the first circle</param>
        /// <param name="y1">The y coordinate of the center of the first circle</param>
        /// <param name="radius1">The radius of the first circle</param>
        /// <param name="x2">The x coordinate of the center of the second circle</param>
        /// <param name="y2">The y coordinate of the center of the second circle</param>
        /// <param name="radius2">The radius of the second circle</param>
        /// <returns>True if they overlap/False otherwise</returns>
        public static bool BoundingCircle(int x1, int y1, int radius1, int x2, int y2, int radius2)
        {
            Vector2 V1 = new Vector2(x1, y1);
            Vector2 V2 = new Vector2(x2, y2);

            Vector2 Distance = V1 - V2;

            if (Distance.Length() < radius1 + radius2)
                return true;

            return false;
        }
        
        /// <summary>
        /// Checks whether two triangles overlap
        /// </summary>
        /// <param name="p1">The list of the 3 points of the first triangle</param>
        /// <param name="p2">The list of the 3 points of the second triangle</param>
        /// <returns>True if they overlap/False otherwise</returns>
        public static bool BoundingTriangles(List<Vector2> p1, List<Vector2> p2)
        {
            for (int i = 0; i < 3; i++)
                if (_isPointInsideTriangle(p1, p2[i])) return true;

            for (int i = 0; i < 3; i++)
                if (_isPointInsideTriangle(p2, p1[i])) return true;
            return false;
        }
        
        /// <summary>
        /// Per pixel collision detection that supports rotation
        /// </summary>
        /// <param name="Texture1">The first sprite's texture</param>
        /// <param name="Texture2">The second sprite's texture</param>
        /// <param name="Pos1">The first sprite's position</param>
        /// <param name="Pos2">The second sprite's position</param>
        /// <param name="Orig1">The first sprite's origin of rotation (texture reference point)</param>
        /// <param name="Orig2">The second sprite's origin of rotation (texture reference point)</param>
        /// <param name="Rect1">The first sprite's bounding rectangle</param>
        /// <param name="Rect2">The second sprite's bounding rectangle</param>
        /// <param name="Theta1">The first sprite's rotation</param>
        /// <param name="Theta2">The second sprite's rotation</param>
        /// <param name="spriteBatch">The current Spriteatch</param>
        /// <returns>True if they overla
        #region Private memebers
        private static bool _isPointInsideTriangle(List<Vector2> TrianglePoints, Vector2 p)
        {
            // Translated to C# from: http://www.ddj.com/184404201
            Vector2 e0 = p - TrianglePoints[0];
            Vector2 e1 = TrianglePoints[1] - TrianglePoints[0];
            Vector2 e2 = TrianglePoints[2] - TrianglePoints[0];

            float u, v = 0;
            if (e1.X == 0)
            {
                if (e2.X == 0) return false;
                u = e0.X / e2.X;
                if (u < 0 || u > 1) return false;
                if (e1.Y == 0) return false;
                v = (e0.Y - e2.Y * u) / e1.Y;
                if (v < 0) return false;
            }
            else
            {
                float d = e2.Y * e1.X - e2.X * e1.Y;
                if (d == 0) return false;
                u = (e0.Y * e1.X - e0.X * e1.Y) / d;
                if (u < 0 || u > 1) return false;
                v = (e0.X - e2.X * u) / e1.X;
                if (v < 0) return false;
                if ((u + v) > 1) return false;
            }

            return true;
        }

        public static bool IsPointInPolygon(Vector2 p, List<Vector2> polygon)
        {
            double minX = polygon[0].X;
            double maxX = polygon[0].X;
            double minY = polygon[0].Y;
            double maxY = polygon[0].Y;
            for (int i = 1; i < polygon.Count; i++)
            {
                Vector2 q = polygon[i];
                minX = Math.Min(q.X, minX);
                maxX = Math.Max(q.X, maxX);
                minY = Math.Min(q.Y, minY);
                maxY = Math.Max(q.Y, maxY);
            }

            if (p.X < minX || p.X > maxX || p.Y < minY || p.Y > maxY)
            {
                return false;
            }

            // http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
            bool inside = false;
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                if ((polygon[i].Y > p.Y) != (polygon[j].Y > p.Y) &&
                     p.X < (polygon[j].X - polygon[i].X) * (p.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        private static Rectangle _getBoundingRectangleOfRotatedRectangle(List<Vector2> RectanglePoints)
        {
            Vector2 BoundingRectangleStart = new Vector2()
            {
                X = _getMinimumOf(RectanglePoints[0].X, RectanglePoints[1].X,RectanglePoints[2].X,RectanglePoints[3].X),
                Y = _getMinimumOf(RectanglePoints[0].Y, RectanglePoints[1].Y, RectanglePoints[2].Y, RectanglePoints[3].Y)
            };

            int BoundingRectangleWidth = -(int)BoundingRectangleStart.X + _getMaximumOf(RectanglePoints[0].X, RectanglePoints[1].X, RectanglePoints[2].X, RectanglePoints[3].X);
            int BoundingRectangleHeight = -(int)BoundingRectangleStart.Y + _getMaximumOf(RectanglePoints[0].Y, RectanglePoints[1].Y, RectanglePoints[2].Y, RectanglePoints[3].Y);

            return new Rectangle((int)BoundingRectangleStart.X, (int)BoundingRectangleStart.Y, BoundingRectangleWidth, BoundingRectangleHeight);

        }
        private static int _getMinimumOf(float a1, float a2, float a3, float a4)
        {
            return (int)MathHelper.Min(MathHelper.Min(MathHelper.Min(a1, a2), a3), a4);
        }
        private static int _getMaximumOf(float a1, float a2, float a3, float a4)
        {
            return (int)MathHelper.Max(MathHelper.Max(MathHelper.Max(a1, a2), a3), a4);
        }
        private static void _createTrianglesFromRectangle(List<Vector2> RectPoints, out List<Vector2> Triangle1, out List<Vector2> Triangle2)
        {
            Triangle1 = new List<Vector2>()
            {
                new Vector2(RectPoints[0].X,RectPoints[0].Y),
                new Vector2(RectPoints[1].X,RectPoints[1].Y),
                new Vector2(RectPoints[2].X,RectPoints[2].Y),
            };
            Triangle2 = new List<Vector2>()
            {
                new Vector2(RectPoints[1].X,RectPoints[1].Y),
                new Vector2(RectPoints[2].X,RectPoints[2].Y),
                new Vector2(RectPoints[3].X,RectPoints[3].Y),
            };
        }
        #endregion
    }
}
