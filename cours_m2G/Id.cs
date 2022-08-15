﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cours_m2G
{
    class Id
    {
        private string name;
        private string description;

        public string Name { get { return name; } }
        public string Description { get { return description; } }

        public Id(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public static bool operator ==(Id id1, Id id2)
        {
            if (id1.description == id2.description && id1.name == id2.name)
                return true;
            return false;
        }
        public static bool operator !=(Id id1, Id id2)
        {
            if (id1 == id2)
                return false;
            return true;
        }
    }

    enum MatrixType { Rotate, Scale, Translate, Perspective, Orto }
    enum TypeVisitor { Drawer, Reader, Transform }
    public enum CameraDirection
    {
        FORWARD, BACKWARD, LEFT, RIGHT, UP, DOWN, YAW, PICH, ROTATIONY
    }

}