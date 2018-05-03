using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class Coordinates
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public Coordinates()
        {
        }

        public Coordinates(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.X = z;
        }
    }

