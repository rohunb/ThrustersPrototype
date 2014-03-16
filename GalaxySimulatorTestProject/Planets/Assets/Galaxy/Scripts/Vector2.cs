using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class Vector2
    {
        public static Vector2 ZERO = new Vector2(0, 0);

        private float x;
        public float X { get { return x; } set { x = value; } }

        private float y;
        public float Y { get { return y; } set { y = value; } }

        private float magnitude;
        public float Magnitude { get { magnitude = (float) Math.Sqrt((x * x) + (y * y));  return magnitude; } set { /* do nothing */ } }
        
        public Vector2()
        {
            x = 0.0f;
            y = 0.0f;
        }

        public Vector2(float _x, float _y)
        {
            x = _x;
            y = _y;
        }

        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return (lhs.x == rhs.x) && (lhs.y == rhs.y);
        }

        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return !(lhs == rhs);
        }

        public void Normalize()
        {
            x /= Magnitude;
            y /= Magnitude;
        }

        public Vector2 getTangentVector()
        {
            Vector2 tangentVector = new Vector2(-y, x);
            return tangentVector;
        }

        public static float DotProduct(Vector2 lhs, Vector2 rhs)
        {
            return (float) Math.Sqrt((lhs.x * rhs.x) + (lhs.y * rhs.y));
        }

        public String Text()
        {
            return "(" + x + "," + y + ")";
        }

    }
