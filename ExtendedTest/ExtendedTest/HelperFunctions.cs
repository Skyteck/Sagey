using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey
{
    public static class HelperFunctions
    {

        public static List<Vector2> RectToList(Rectangle rect)
        {
            List<Vector2> points = new List<Vector2>();
            Vector2 topLeft = new Vector2(rect.Left, rect.Top);
            Vector2 topRight = new Vector2(rect.Right, rect.Top);
            Vector2 bottomLeft = new Vector2(rect.Left, rect.Bottom);
            Vector2 bottomRight = new Vector2(rect.Right, rect.Bottom);
            points.Add(topLeft);
            points.Add(topRight);
            points.Add(bottomLeft);
            points.Add(bottomRight);
            return points;
        }

        public static List<Vector2> RotatedRectList(Rectangle rect, float Rotation, int offset = 0)
        {
            List<Vector2> points = new List<Vector2>();
            Vector2 rectCenter = new Vector2(rect.Center.X, rect.Center.Y);

            Vector2 topleft = new Vector2(rect.Left + offset, rect.Top + offset);
            topleft = Vector2.Transform(topleft - rectCenter, Matrix.CreateRotationZ(Rotation)) + rectCenter;
            points.Add(topleft);

            Vector2 topRight = new Vector2(rect.Right - offset, rect.Top + offset);
            topRight = Vector2.Transform(topRight - rectCenter, Matrix.CreateRotationZ(Rotation)) + rectCenter;
            points.Add(topRight);

            Vector2 bottomLeft = new Vector2(rect.Left + offset, rect.Bottom - offset);
            bottomLeft = Vector2.Transform(bottomLeft - rectCenter, Matrix.CreateRotationZ(Rotation)) + rectCenter;
            points.Add(bottomLeft);

            Vector2 bottomRight = new Vector2(rect.Right - offset, rect.Bottom - offset);
            bottomRight = Vector2.Transform(bottomRight - rectCenter, Matrix.CreateRotationZ(Rotation)) + rectCenter;
            points.Add(bottomRight);
            return points;
        }

        public static Point VectorToPoint(Vector2 vec)
        {
            return new Point((int)vec.X, (int)vec.Y);
        }

        public static Vector2 PointToVector(Point p)
        {
            return new Vector2(p.X, p.Y);
        }

        public  static T[,] Make2DArray<T>(T[] input, int height, int width)
        {
            T[,] output = new T[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output[i, j] = input[i * width + j];
                }
            }
            return output;
        }
    }
}
