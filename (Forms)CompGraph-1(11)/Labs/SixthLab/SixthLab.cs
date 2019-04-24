﻿using System;
using System.Drawing;
using _Forms_CompGraph_1_11_.Labs.SixthLab.Utils;
using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.SixthLab
{
    class SixthLab : LabBase
    {
        private const double Inf = double.MaxValue;
        private ColorRGB BackGroundColor = new ColorRGB(0, 0, 0);
        private Sphere[] Spheres { get; set; }
        private LightSource[] LightSources { get; set; }

        public SixthLab(Bitmap source) : base(source)
        {
        }

        #region Presets
        private void Preset1()
        {
            BackGroundColor = new ColorRGB(0, 0, 0);
            //Light Sources
            LightSources = new LightSource[3];
            DoublePoint3D point;
            float intensity;
            string type;

            intensity = 0.2f;
            LightSources[0] = new LightSource(intensity);

            point = new DoublePoint3D(2, 1, 0);
            intensity = 0.6f;
            type = "Point";
            LightSources[1] = new LightSource(point, intensity, type);

            point = new DoublePoint3D(1, 4, 4);
            intensity = 0.2f;
            type = "Directional";
            LightSources[2] = new LightSource(point, intensity, type);

            //Spheres
            Spheres = new Sphere[4];
            DoublePoint3D center;
            double radius;
            ColorRGB color;
            int specular;
            float reflection;
            float transparency;

            center = new DoublePoint3D(0, -1, 3);
            radius = 1;
            color = new ColorRGB(255, 0, 0); //Red
            specular = 500;
            reflection = 0.2f;
            transparency = 0f;
            Spheres[0] = new Sphere(center, radius, color, specular, reflection, transparency);

            center = new DoublePoint3D(2, 0, 4);
            radius = 1;
            color = new ColorRGB(0, 0, 255); //Blue
            specular = 500;
            reflection = 0.3f;
            transparency = 0f;
            Spheres[1] = new Sphere(center, radius, color, specular, reflection, transparency);

            center = new DoublePoint3D(-2, 0, 4);
            radius = 1;
            color = new ColorRGB(0, 255, 0); //Green
            specular = 10;
            reflection = 0.4f;
            transparency = 0f;
            Spheres[2] = new Sphere(center, radius, color, specular, reflection, transparency);

            center = new DoublePoint3D(0, -5001, 0);
            radius = 5000;
            color = new ColorRGB(255, 255, 0); //Yellow
            specular = 1000;
            reflection = 0.5f;
            transparency = 0f;
            Spheres[3] = new Sphere(center, radius, color, specular, reflection, transparency);
        }
        #endregion

        public override void Draw(LabParameters labParameters)
        {
            if (!(labParameters is SixthLabParameters parameters))
                throw new ArgumentException($"{nameof(labParameters)} has wrong type: {labParameters.GetType().Name}");

            Preset1();

            RenderScene(parameters.CameraPosition, parameters.CameraRotation);
        }

        private void RenderScene(DoublePoint3D position, DoublePoint2D rotation)
        {
            const double distance = 1;
            const byte recursionDepth = 3;

            var interval = new double[] { 0.0001, Inf };
            var cameraPosition = position; //new DoublePoint3D(0, 0, 0);// (0, 5, 2);
            var cameraAngle = rotation; //new DoublePoint2D(0, 0);// (Math.PI / 2, Math.PI);

            for (var x = 0; x < Source.Width; x++)
                for (var y = 0; y < Source.Height; y++)
                {
                    DoublePoint3D viewPortPoint = CanvasToViewport(new DoublePoint2D(x, y), distance).RotateX(cameraAngle.X).RotateY(cameraAngle.Y);
                    ColorRGB color = TraceRay(cameraPosition, viewPortPoint, interval, recursionDepth);
                    Source.SetPixel(x, y, Color.FromArgb(color.R, color.G, color.B));
                }
        }

        #region Basic
        private DoublePoint3D CanvasToViewport(DoublePoint2D canvasPoint, double distance)
        {
            var viewPortSize = new DoublePoint2D(distance, distance);
            canvasPoint.X -= Source.Width / 2;
            canvasPoint.Y -= Source.Height / 2;
            canvasPoint.Y = -canvasPoint.Y;
            return new DoublePoint3D(canvasPoint.X * viewPortSize.X / Source.Width, canvasPoint.Y * viewPortSize.Y / Source.Height, distance); //What'a hell?
        }
        #endregion

        #region Tracing
        private ColorRGB TraceRay(DoublePoint3D cameraPosition, DoublePoint3D viewPortPoint, double[] interval, int recursionDerpth)
        {
            var closest = ClosestIntersection(cameraPosition, viewPortPoint, interval); //Sphere=0, Point=1

            if (closest.Sphere == -1)
                return BackGroundColor;

            //Compute local color
            var intersectPoint = cameraPosition + closest.Point * viewPortPoint; //Compute intersection point
            var normalVector = intersectPoint - Spheres[closest.Sphere].Center; //Compute normal vector
            normalVector /= normalVector.Length();
            ColorRGB localColor = ComputeLightning(intersectPoint, normalVector, -viewPortPoint, Spheres[closest.Sphere].Specular) * Spheres[closest.Sphere].Color;

            //Transparency
            var transparency = Spheres[closest.Sphere].Transparency;
            ColorRGB transparentColor = BackGroundColor;
            if (transparency > 0)
                transparentColor = TraceRay(intersectPoint, viewPortPoint, interval, recursionDerpth);

            //Reflection
            var reflection = Spheres[closest.Sphere].Reflection;
            if ((recursionDerpth <= 0) || (reflection <= 0))
                return (1 - transparency) * localColor + transparency * transparentColor;
            var reflectedVector = ReflectRay(-viewPortPoint, normalVector);
            var reflectedColor = TraceRay(intersectPoint, reflectedVector, interval, recursionDerpth - 1);

            return (1 - reflection) * (1 - transparency) * localColor + reflection * (1 - transparency) * reflectedColor + transparency * transparentColor;
        }

        private Closest ClosestIntersection(DoublePoint3D cameraPosition, DoublePoint3D viewPortPoint, double[] interval)
        {
            var closestPoint = Inf;
            var closestSphere = -1;

            for (var i = 0; i < Spheres.Length; i++)
            {
                var intersections = IntersectRaySphere(cameraPosition, viewPortPoint, Spheres[i]);
                if ((intersections[0] < closestPoint) && (interval[0] < intersections[0]) && (intersections[0] < interval[1]))
                {
                    closestPoint = intersections[0];
                    closestSphere = i;
                }
                if ((intersections[1] < closestPoint) && (interval[0] < intersections[1]) && (intersections[1] < interval[1]))
                {
                    closestPoint = intersections[1];
                    closestSphere = i;
                }
            }

            return new Closest(closestSphere, closestPoint);
        }

        private double[] IntersectRaySphere(DoublePoint3D cameraPosition, DoublePoint3D viewVector, Sphere sphere)
        {
            var oc = cameraPosition - sphere.Center; //vector from camera to sphere center

            var a = viewVector.ScalMultiply(viewVector); //viewVector length
            var b = 2 * oc.ScalMultiply(viewVector);
            var c = oc.ScalMultiply(oc) - sphere.Radius * sphere.Radius;

            var discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
                return new double[] { Inf, Inf };

            var root1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            var root2 = (-b - Math.Sqrt(discriminant)) / (2 * a);

            return new double[] { root1, root2 };
        }
        #endregion

        #region Lightning
        private float ComputeLightning(DoublePoint3D intersectPoint, DoublePoint3D normalVector, DoublePoint3D vector, int specular)
        {
            var result = 0f;
            var interval = new double[] { 0.001, Inf };

            foreach (LightSource light in LightSources)
            {
                if (light.Type == "Ambient")
                    result += light.Intensity;
                else
                {
                    DoublePoint3D lightVector;
                    if (light.Type == "Point")
                        lightVector = light.Point - intersectPoint;
                    else
                        lightVector = light.Point;

                    //Shadows
                    Closest shadow = ClosestIntersection(intersectPoint, lightVector, interval);
                    float transparency = 1;
                    while (shadow.Sphere != -1)
                    {
                        if ((Spheres[shadow.Sphere].Transparency == 0) || (transparency == 0))
                        {
                            transparency = 0;
                            break;
                        }
                        transparency *= Spheres[shadow.Sphere].Transparency;
                        shadow = ClosestIntersection(intersectPoint + shadow.Point * lightVector, lightVector, interval);
                    }
                    if (transparency == 0)
                        continue;

                    //Diffusal Illumination
                    var scal = normalVector.ScalMultiply(lightVector);
                    if (scal > 0)
                        result += (float)(light.Intensity * scal / (normalVector.Length() * lightVector.Length()));

                    //Specular Illumination
                    if (specular != -1)
                    {
                        var refleclectedVector = ReflectRay(lightVector, normalVector);
                        scal = refleclectedVector.ScalMultiply(vector);
                        if (scal > 0)
                            result += (float)(transparency * (light.Intensity * Math.Pow(scal / (refleclectedVector.Length() * vector.Length()), specular)));
                    }
                }
            }

            return result;
        }
        #endregion

        #region Reflection
        private DoublePoint3D ReflectRay(DoublePoint3D fallingVector, DoublePoint3D normalVector)
        {
            return 2 * normalVector * normalVector.ScalMultiply(fallingVector) - fallingVector;
        }
        #endregion

        #region structs
        struct Sphere
        {
            public DoublePoint3D Center { get; set; }
            public double Radius { get; set; }
            public ColorRGB Color { get; set; }
            public int Specular { get; set; }
            public float Reflection { get; set; }
            public float Transparency { get; set; }

            public Sphere(DoublePoint3D center, double radius, ColorRGB color, int specular, float reflection, float transparency)
            {
                Center = center;
                Radius = radius;
                Color = color;
                Specular = specular;
                Reflection = reflection;
                Transparency = transparency;
            }
        }

        struct LightSource
        {
            public DoublePoint3D Point { get; set; }
            public float Intensity { get; set; }
            public string Type { get; set; }

            public LightSource(DoublePoint3D vector, float intensity, string type)
            {
                Point = vector;
                Intensity = intensity;
                Type = type;
            }

            public LightSource(float intensity)
            {
                Point = new DoublePoint3D();
                Intensity = intensity;
                Type = "Ambient";
            }
        }

        private struct Closest
        {
            public int Sphere { get; set; }
            public double Point { get; set; }

            public Closest(int sphere, double point)
            {
                Sphere = sphere;
                Point = point;
            }
        }
        #endregion
    }
}
