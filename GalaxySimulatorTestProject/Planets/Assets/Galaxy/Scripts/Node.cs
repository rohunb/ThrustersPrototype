using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public enum NodeShapes { SOLID_PLATE, ROD_CNTR, ROD_END, SOLID_SPHERE, HOLLOW_SPHERE, HOOP, CYLINDER}
    class Node
    {
        private Vector2 position;
        public Vector2 Position { get { return position; } set { position = value; } }

        private ulong mass;
        public ulong Mass { get { return mass; } set { mass = value; } }

        private float length;
        public float Length { get { return length; } set { length = value; } }

        private float width;
        public float Width { get { return width; } set { width = value; } }

        private float radius;
        public float Radius { get { return radius; } set { radius = value; } }

        private NodeShapes shape;
        public NodeShapes Shape { get { return shape; } set { shape = value; } }
        
        public Node()
        {
            position = new Vector2();
            mass = 0;
            shape = NodeShapes.SOLID_PLATE;
        }

        public Node(Vector2 _position)
        {
            position = _position;
            mass = 0;
            shape = NodeShapes.SOLID_PLATE;
        }

        public Node(Vector2 _position, ulong _mass)
        {
            position = _position;
            mass = _mass;
            shape = NodeShapes.SOLID_PLATE;
        }

        public Node(ulong _mass)
        {
            position = new Vector2();
            mass = _mass;
            shape = NodeShapes.SOLID_PLATE;
        }

        public Node(Vector2 _position, ulong _mass, NodeShapes _shape)
        {
            position = _position;
            mass = _mass;
            shape = _shape;
        }

        public float calcRotationalInertia(Vector2 centerOfMass)
        {

            Vector2 distanceV = new Vector2();
            distanceV.X = centerOfMass.X - position.X;
            distanceV.Y = centerOfMass.Y - position.Y;

            float distance = (float)Math.Sqrt((distanceV.X * distanceV.X) + (distanceV.Y * distanceV.Y));
            float rotationalInertia = 0.0f;

            switch (shape)
            {
                case NodeShapes.SOLID_PLATE:
                    rotationalInertia = (((length * length) + (width * width)) * (mass / 12.0f)) + (mass * distance * distance);
                    break;
                case NodeShapes.ROD_CNTR:
                    rotationalInertia = ((mass * length * length) / 12.0f) + (mass * distance * distance);
                    break;
                case NodeShapes.ROD_END:
                    rotationalInertia = ((mass * length * length) / 3.0f) + (mass * distance * distance);
                    break;
                case NodeShapes.SOLID_SPHERE:
                    rotationalInertia = (2.0f * (mass * radius * radius) / 5.0f) + (mass * distance * distance);
                    break;
                case NodeShapes.HOLLOW_SPHERE:
                    rotationalInertia = (2.0f * (mass * radius * radius) / 3.0f) + (mass * distance * distance);
                    break;
                case NodeShapes.HOOP:
                    rotationalInertia = ((mass * radius * radius) / 2.0f) + (mass * distance * distance);
                    break;
                case NodeShapes.CYLINDER:
                    rotationalInertia = (mass * radius * radius)  + (mass * distance * distance);
                    break;
                default:
                    break;
            }

            return rotationalInertia;
        }
    }
