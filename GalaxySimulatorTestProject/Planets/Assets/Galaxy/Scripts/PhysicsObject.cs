using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public enum PhysicsObjectType { SMBH, STAR, ROCK_PLANET, ROCK_MOON, GAS_GIANT, G_G_MOON, GALAXY }
    public class PhysicsObject
    {
        public rotate rotateScript;
        private List<PhysicsObject> components = new List<PhysicsObject>();

        public List<PhysicsObject> getComponents()
        {
            return components;
        }

        private PhysicsObject parent;
        public PhysicsObject Parent { get { return parent; } set { parent = value; } }

        private static int totalObjects = 0;
        private int ID;

        private PhysicsObjectType physicsObjectType;
        public PhysicsObjectType ObjectType { get { return physicsObjectType; } set { physicsObjectType = value; } }

        //private const double KeplerKonstant = 0.000000000000000000297; // this is the actual constant
        //private const double KeplerKonstant = 0.000000297; // this scaled version works just right

        private Vector2 position; 
        public Vector2 Position { get { return position; } set { position = value; } }
        
        private Vector2 velocity;
	    public Vector2 Velocity { get { return velocity;} set { velocity = value;} }
	    
        private Vector2 acceleration;
	    public Vector2 Acceleration {  get { return acceleration;} set { acceleration = value;} }
	    
        private Vector2 netForces;
	    public Vector2 NetForces { get { return netForces;} set { netForces = value;} }

        private double angle;
        public double Angle { get { return angle; } set { angle = value; } }
        
        private float angularVelocity;
        public float AngularVelocity { get { return angularVelocity;} set { angularVelocity = value;} }
	
        private float angularAcceleration;
	    public float AngularAcceleration { get { return angularAcceleration;} set { angularAcceleration = value;} }
	
        private float torque;
	    public float Torque { get { return torque;} set { torque = value;} }

        private ulong mass;
        public ulong Mass { get { return mass; } set { mass = value; } }

        private ulong radius = 0;
        public ulong Radius { get { return radius; } set { radius = value; } }

        private double orbitalCircumference = 0;
        public double OrbitalCircumference { get { return orbitalCircumference; } set { orbitalCircumference = value; } }

        private Vector2 gravityWell;
        public Vector2 GravityWell { get { return gravityWell; } set { gravityWell = value; } }

        private double periodInSeconds = 0;
        public double PeriodInSeconds { get { return periodInSeconds; } set { periodInSeconds = value; } }

        double linearVelocityScalar;

        private int numComponents = 0;

        private float timeFactor = 0.01f;

        public int getNumComponents()
        {
            return numComponents;
        }

        public void addComponent(PhysicsObject component)
        {
            components.Add(component);
            numComponents++;
        }

        public void removeComponent(PhysicsObject component)
        {
            components.Remove(component);
            numComponents--;
        }

        public PhysicsObject pop()
        {
            if (components.Count > 0)
            {
                PhysicsObject tmp = components.First<PhysicsObject>();
                components.Remove(tmp);
                numComponents--;
                return tmp; 
            }
            else
            {
                return null;
            }
        }

        //constructor with values
        public PhysicsObject(Vector2 _position, ulong _mass, PhysicsObjectType _type)
        {
            position = _position;
            mass = _mass;
            physicsObjectType = _type;
            finishSetup();
        }

        public PhysicsObject()
        {
            position = Vector2.zero;
            mass = 0;
            physicsObjectType = PhysicsObjectType.SMBH;
            finishSetup();
        }

        public ulong calcRadius(PhysicsObject _gravityWell)
        {
            parent = _gravityWell;
            gravityWell = _gravityWell.Position;
            radius = (ulong)new Vector2(position.x - gravityWell.x, position.y - gravityWell.y).magnitude;
            orbitalCircumference = 2 * Math.PI * radius;
            calcPeriodInSeconds();
            return radius;
        }

        public double calcPeriodInSeconds()
        {

            periodInSeconds = Math.Sqrt(radius * radius * radius) * timeFactor ;

                calcTheRest();
            return periodInSeconds;
        }

        public double calcTheRest()
        {
            linearVelocityScalar = orbitalCircumference / periodInSeconds;

            Vector2 direction = new Vector2(Position.x - gravityWell.x, Position.y - gravityWell.y);
            Vector2 tanToDir = new Vector2(-direction.y, direction.x);
            tanToDir.Normalize();

            tanToDir.x *= (float)linearVelocityScalar;
            tanToDir.y *= (float)linearVelocityScalar;

            velocity = tanToDir;

            return 0;
        }

        //calls any remaining setup functions that both constructors need to complete
        private void finishSetup()
        {
            velocity = Vector2.zero;
            acceleration = Vector2.zero;
            netForces = Vector2.zero;
            angle = 0.0f;
            angularVelocity = 0.0f;
            angularAcceleration = 0.0f;
            torque = 0;
            ID = totalObjects++;
        }

        //print values every frame
        public void Update()
        {
            Physics();
        }

        private void Physics()
        {
            float DELTATIME = Time.deltaTime;
            //physics
            angularVelocity = (float)(linearVelocityScalar / periodInSeconds);

            switch (physicsObjectType)
            {
                case PhysicsObjectType.SMBH:
                    break;
                case PhysicsObjectType.STAR:
                    angle += ((angularVelocity * DELTATIME) + ((0.5f * angularAcceleration) * (DELTATIME * DELTATIME)));
                    
                    break;
                case PhysicsObjectType.ROCK_PLANET:
                    angle += ((angularVelocity * DELTATIME) + ((0.5f * angularAcceleration) * (DELTATIME * DELTATIME)));

                    break;
                case PhysicsObjectType.ROCK_MOON:
                    angle += ((angularVelocity * DELTATIME) + ((0.5f * angularAcceleration) * (DELTATIME * DELTATIME)));
                    break;
                case PhysicsObjectType.GAS_GIANT:
                    angle += ((angularVelocity * DELTATIME) + ((0.5f * angularAcceleration) * (DELTATIME * DELTATIME)));
                    
                    break;
                case PhysicsObjectType.G_G_MOON:
                    angle += ((angularVelocity * DELTATIME) + ((0.5f * angularAcceleration) * (DELTATIME * DELTATIME)));
                    break;
                case PhysicsObjectType.GALAXY:
                    break;
                default:
                    break;
            }

            if (angle >= 360)
            {
                angle = 0;
            }

            position.x = (float)(gravityWell.x + radius * Math.Cos(angle));
            position.y = (float)(gravityWell.y + radius * Math.Sin(angle));
        }

        public string Text()
        {
            string typeText;

            switch (physicsObjectType)
            {
                case PhysicsObjectType.SMBH:
                    typeText = "[0 - Supermassive Black Hole]";
                    break;
                case PhysicsObjectType.STAR:
                    typeText = "[1 - Star]";
                    break;
                case PhysicsObjectType.ROCK_PLANET:
                    typeText = "[2 - Rock Planet]";
                    break;
                case PhysicsObjectType.ROCK_MOON:
                    typeText = "[3 - Moon Orbitting Rock Planet]";
                    break;
                case PhysicsObjectType.GAS_GIANT:
                    typeText = "[4 - Gas Giant]";
                    break;
                case PhysicsObjectType.G_G_MOON:
                    typeText = "[5 - Moon Orbitting Gas Giant]";
                    break;
                default:
                    typeText = "[? - ????]";
                    break;
            }
            return "";// typeText + ID + " pos: " + position.Text() + " mass: " + mass + " radius: " + radius + " orbital period: " + periodInSeconds + " vel: " + velocity.Text() + " angle: " + angle + " angular Velocity: " + angularVelocity + "\n";
        }
    }
