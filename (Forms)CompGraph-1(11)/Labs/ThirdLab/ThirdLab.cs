﻿using System;
using System.Drawing;
using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.ThirdLab
{
    struct DoublePoint3D
    {
        public double X;
        public double Y;
        public double Z;

        public DoublePoint3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public DoublePoint3D(DoublePoint2D xy, double z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }

        public DoublePoint3D RotateX(double radians)
        {
            var result = new DoublePoint3D();

            result.X = X;
            result.Y = Math.Cos(radians) * Y - Math.Sin(radians) * Z;
            result.Z = Math.Sin(radians) * Y + Math.Cos(radians) * Z;

            return new DoublePoint3D(result.X, result.Y, result.Z);
        }

        public DoublePoint3D RotateY(double radians)
        {
            var result = new DoublePoint3D();

            result.X = Math.Cos(radians) * X + Math.Sin(radians) * Z;
            result.Y = Y;
            result.Z = -Math.Sin(radians) * X + Math.Cos(radians) * Z;

            return new DoublePoint3D(result.X, result.Y, result.Z);
        }
    }

    class ThirdLab : LabBase
    {
        public ThirdLab(Bitmap source) : base(source)
        {

        }

        public override void Draw(LabParameters labParameters)
        {
            if (!(labParameters is ThirdLabParameters parameters))
                throw new ArgumentException($"{nameof(labParameters)} has wrong type: {labParameters.GetType().Name}");

            var areaPoints = new DoublePoint3D[parameters.AreaPointsXY.Length];
            for (var i = 0; i < parameters.AreaPointsXY.Length; i++)
                areaPoints[i] = new DoublePoint3D(parameters.AreaPointsXY[i], parameters.AreaPointZ[i]);

            DrawLab(areaPoints, parameters.XRotateDegree, parameters.YRotateDegree);
        }

        private void DrawLab(DoublePoint3D[] areaPoints, double xRotateDegree, double yRotateDegree)
        {
            Drawer.DrawLine(Source, new DoublePoint2D(0, 300), new DoublePoint2D(0, 0),Color.Gray);
            Drawer.DrawLine(Source, new DoublePoint2D(0, 0), new DoublePoint2D(Math.Cos(Math.PI/6)*300, -Math.Sin(Math.PI/6)*300), Color.Gray);
            Drawer.DrawLine(Source, new DoublePoint2D(0, 0), new DoublePoint2D(-Math.Cos(Math.PI/6)*300, -Math.Sin(Math.PI/6)*300), Color.Gray);

            var realPoints = new DoublePoint2D[areaPoints.Length];
            xRotateDegree = xRotateDegree / 360 * 2 * Math.PI;
            yRotateDegree = yRotateDegree / 360 * 2 * Math.PI;

            for (var i = 0; i < areaPoints.Length; i++)
                realPoints[i] = To2D(areaPoints[i].RotateX(xRotateDegree).RotateY(yRotateDegree));

            for (var i = 0; i < areaPoints.Length; i++)
                Drawer.DrawLine(Source, realPoints[i], realPoints[(i + 1) % areaPoints.Length], Color.Black);
        }

        private DoublePoint2D To2D(DoublePoint3D point)
        {
            var result = new DoublePoint2D();
            var xAngle = Math.PI / 6;
            var zAngle = Math.PI / 6;

            result.X = Math.Cos(xAngle) * point.X - Math.Cos(zAngle) * point.Z;
            result.Y = point.Y - Math.Sin(xAngle) * point.X - Math.Sin(zAngle) * point.Z;

            return result;
        }
    }
}
