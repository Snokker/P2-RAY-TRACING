using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK;

namespace template
{
    //http://neokabuto.blogspot.nl/2014/01/opentk-tutorial-5-basic-camera.html
    class camera
    {
        //2 basic things we need, the direction its looking from and the direction its looking in
        public Vector3 Position = Vector3.Zero;
        public Vector3 Direction = new Vector3((float)Math.PI, 0f, 0f);
        public float MoveSpeed = 0.2f;
        public float MouseSensitivity = 0.01f;

        //het houd alleen niet de hoeken van de screen niet bij nu volgens mij

        //make a view matrix from position and direction
        public Matrix4 GetViewMatrix()
        {
            Vector3 lookat = new Vector3();

            lookat.X = (float)(Math.Sin((float)Direction.X) * Math.Cos((float)Direction.Y));
            lookat.Y = (float)Math.Sin((float)Direction.Y);
            lookat.Z = (float)(Math.Cos((float)Direction.X) * Math.Cos((float)Direction.Y));

            return Matrix4.LookAt(Position, Position + lookat, Vector3.UnitY);
        }
        // dus de positie en directie worden gebruikt om een "view matrix te maken"

        //move ding
        public void Move(float x, float y, float z)
        {
            Vector3 offset = new Vector3();

            Vector3 forward = new Vector3((float)Math.Sin((float)Direction.X), 0, (float)Math.Cos((float)Direction.X));
            Vector3 right = new Vector3(-forward.Z, 0, forward.X);

            offset += x * right;
            offset += y * forward;
            offset.Y += z;

            offset.NormalizeFast();
            offset = Vector3.Multiply(offset, MoveSpeed);

            Position += offset;
        }

        //Rotation
        public void AddRotation(float x, float y)
        {
            x = x * MouseSensitivity;
            y = y * MouseSensitivity;

            Direction.X = (Direction.X + x) % ((float)Math.PI * 2.0f);
            Direction.Y = Math.Max(Math.Min(Direction.Y + y, (float)Math.PI / 2.0f - 0.1f), (float)-Math.PI / 2.0f + 0.1f);
        }
    }
}
