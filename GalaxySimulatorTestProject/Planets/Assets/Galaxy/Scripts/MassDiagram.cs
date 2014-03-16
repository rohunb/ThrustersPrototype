using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class MassDiagram
    {
        List<Node> nodes;

        public MassDiagram()
        {
            nodes = new List<Node>();
        }

        public void AddNode(Node newNode)
        {
            nodes.Add(newNode);
        }

        public ulong calcTotalMass()
        {
            ulong sumOfMasses = 0;

            foreach (Node node in nodes)
            {
                sumOfMasses += node.Mass;
            }

            return sumOfMasses;
        }

        public Vector2 calcCenterOfMass()
        {
            float totalMass = calcTotalMass();

            float xPos = 0;
            float yPos = 0;

            foreach (Node node in nodes)
            {
                xPos += (node.Mass * node.Position.X);
                yPos += (node.Mass * node.Position.Y);
            }

            xPos /= totalMass;
            yPos /= totalMass;

            return new Vector2(xPos, yPos);
        }

        public float calcRotationalInertia()
        {
            Vector2 centerOfMass = calcCenterOfMass();
            float sumOfInertias = 0.0f;

            foreach (Node node in nodes)
            {
                sumOfInertias += node.calcRotationalInertia(centerOfMass);
            }

            return sumOfInertias;
        }
    }
