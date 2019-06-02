using System;
using System.Drawing;
using _Forms_CompGraph_1_11_.Labs.SixthLab.Utils;
using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.SixthLab
{
    class SixthLab : LabBase
    {
        private const double Inf = double.MaxValue;
        private readonly ColorRGB BackGroundColor = new ColorRGB(0, 0, 0);
        private GraphicalObject[] GraphicalObjects { get; set; }
        private LightSource[] LightSources { get; set; }

        public SixthLab(Bitmap source) : base(source)
        {
        }

        #region Presets
        //private void Preset1()
        //{
        //    BackGroundColor = new ColorRGB(0, 0, 0);
        //    //Light Sources
        //    LightSources = new LightSource[3];
        //    DoublePoint3D point;
        //    float intensity;
        //    string type;

        //    intensity = 0.2f;
        //    LightSources[0] = new LightSource(intensity);

        //    point = new DoublePoint3D(2, 1, 0);
        //    intensity = 0.6f;
        //    type = "Point";
        //    LightSources[1] = new LightSource(point, intensity, type);

        //    point = new DoublePoint3D(1, 4, 4);
        //    intensity = 0.2f;
        //    type = "Directional";
        //    LightSources[2] = new LightSource(point, intensity, type);

        //    //Spheres
        //    Spheres = new Sphere[4];
        //    DoublePoint3D center;
        //    double radius;
        //    ColorRGB color;
        //    int specular;
        //    float reflection;
        //    float transparency;

        //    center = new DoublePoint3D(0, -1, 3);
        //    radius = 1;
        //    color = new ColorRGB(255, 0, 0); //Red
        //    specular = 500;
        //    reflection = 0.2f;
        //    transparency = 0f;
        //    Spheres[0] = new Sphere(center, radius, color, specular, reflection, transparency);

        //    center = new DoublePoint3D(2, 0, 4);
        //    radius = 1;
        //    color = new ColorRGB(0, 0, 255); //Blue
        //    specular = 500;
        //    reflection = 0.3f;
        //    transparency = 0f;
        //    Spheres[1] = new Sphere(center, radius, color, specular, reflection, transparency);

        //    center = new DoublePoint3D(-2, 0, 4);
        //    radius = 1;
        //    color = new ColorRGB(0, 255, 0); //Green
        //    specular = 10;
        //    reflection = 0.4f;
        //    transparency = 0f;
        //    Spheres[2] = new Sphere(center, radius, color, specular, reflection, transparency);

        //    center = new DoublePoint3D(0, -5001, 0);
        //    radius = 5000;
        //    color = new ColorRGB(255, 255, 0); //Yellow
        //    specular = 1000;
        //    reflection = 0.5f;
        //    transparency = 0f;
        //    Spheres[3] = new Sphere(center, radius, color, specular, reflection, transparency);
        //}
        #endregion

        public override void Draw(LabParameters labParameters)
        {
            if (!(labParameters is SixthLabParameters parameters))
                throw new ArgumentException($"{nameof(labParameters)} has wrong type: {labParameters.GetType().Name}");

            //Preset1();

            GraphicalObjects = parameters.GraphicalObjects;
            LightSources = parameters.LightSources;

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

            if (closest.Object == -1)
                return BackGroundColor;

            //Compute local color
            var intersectPoint = cameraPosition + closest.Point * viewPortPoint; //Compute intersection point
            var normalVector = (GraphicalObjects[closest.Object].ToString().Contains("Sphere")) ? intersectPoint - (GraphicalObjects[closest.Object] as Sphere).Center : closest.Normal;
            normalVector /= normalVector.Length();
            ColorRGB localColor = ComputeLightning(intersectPoint, normalVector, -viewPortPoint, GraphicalObjects[closest.Object].Specular) * GraphicalObjects[closest.Object].Color;

            //Transparency
            var transparency = GraphicalObjects[closest.Object].Transparency;
            ColorRGB transparentColor = BackGroundColor;
            if (transparency > 0)
                transparentColor = TraceRay(intersectPoint, viewPortPoint, interval, recursionDerpth);

            //Reflection
            var reflection = GraphicalObjects[closest.Object].Reflective;
            if ((recursionDerpth <= 0) || (reflection <= 0))
                return (1 - transparency) * localColor + transparency * transparentColor;
            var reflectedVector = ReflectRay(-viewPortPoint, normalVector);
            var reflectedColor = TraceRay(intersectPoint, reflectedVector, interval, recursionDerpth - 1);

            return (1 - reflection) * (1 - transparency) * localColor + reflection * (1 - transparency) * reflectedColor + transparency * transparentColor;
        }

        private Closest ClosestIntersection(DoublePoint3D cameraPosition, DoublePoint3D viewPortPoint, double[] interval)
        {
            var closest = new Closest(-1, Inf);

            for (var i = 0; i < GraphicalObjects.Length; i++)
            {
                if (GraphicalObjects[i].ToString().Contains("Sphere"))
                {
                    var intersections = IntersectRaySphere(cameraPosition, viewPortPoint, GraphicalObjects[i] as Sphere);
                    if ((intersections[0] < closest.Point) && (interval[0] < intersections[0]) && (intersections[0] < interval[1]))
                    {
                        closest.Point = intersections[0];
                        closest.Object = i;
                    }
                    if ((intersections[1] < closest.Point) && (interval[0] < intersections[1]) && (intersections[1] < interval[1]))
                    {
                        closest.Point = intersections[1];
                        closest.Object = i;
                    }
                }
                else
                {
                    //var count = 0;
                    var hex = GraphicalObjects[i] as Hexahedron;

                    //First edge
                    var mass = new DoublePoint3D[] { hex.Points[0].RotateX(hex.Rotation.X).RotateY(hex.Rotation.Y), hex.Points[1].RotateX(hex.Rotation.X).RotateY(hex.Rotation.Y), hex.Points[2].RotateX(hex.Rotation.X).RotateY(hex.Rotation.Y), hex.Points[3].RotateX(hex.Rotation.X).RotateY(hex.Rotation.Y) };
                    var intersection = Intercept(viewPortPoint, cameraPosition, mass);//IntersectRayEdge(mass, cameraPosition, viewPortPoint);
                    if ((intersection > 1) && (intersection < closest.Point))
                    {
                        closest.Point = intersection;
                        closest.Object = i;
                        closest.Normal = (mass[1] - mass[0]).VecMultiply(mass[2] - mass[0]);
                        //count++;
                    }

                    //Second edge
                    /*mass[0] = hex.Points[4];
                    mass[1] = hex.Points[5];
                    mass[2] = hex.Points[6];
                    mass[3] = hex.Points[7];
                    intersection = IntersectRayEdge(mass, cameraPosition, viewPortPoint);
                    if ((intersection > 1) && (intersection < closest.Point))
                    {
                        closest.Point = intersection;
                        closest.Object = i;
                        closest.Normal = (mass[1] - mass[0]).VecMultiply(mass[2] - mass[0]);
                        count++;
                        if (count == 2)
                            continue;
                    }

                    //Third edge
                    mass[0] = hex.Points[1];
                    mass[1] = hex.Points[5];
                    mass[2] = hex.Points[4];
                    mass[3] = hex.Points[0];
                    intersection = IntersectRayEdge(mass, cameraPosition, viewPortPoint);
                    if ((intersection > 1) && (intersection < closest.Point))
                    {
                        closest.Point = intersection;
                        closest.Object = i;
                        closest.Normal = (mass[1] - mass[0]).VecMultiply(mass[2] - mass[0]);
                        count++;
                        if (count == 2)
                            continue;
                    }

                    //Fourth edge
                    mass[0] = hex.Points[3];
                    mass[1] = hex.Points[7];
                    mass[2] = hex.Points[6];
                    mass[3] = hex.Points[2];
                    intersection = IntersectRayEdge(mass, cameraPosition, viewPortPoint);
                    if ((intersection > 1) && (intersection < closest.Point))
                    {
                        closest.Point = intersection;
                        closest.Object = i;
                        closest.Normal = (mass[1] - mass[0]).VecMultiply(mass[2] - mass[0]);
                        count++;
                        if (count == 2)
                            continue;
                    }

                    //Fifth edge
                    mass[0] = hex.Points[0];
                    mass[1] = hex.Points[4];
                    mass[2] = hex.Points[7];
                    mass[3] = hex.Points[3];
                    intersection = IntersectRayEdge(mass, cameraPosition, viewPortPoint);
                    if ((intersection > 1) && (intersection < closest.Point))
                    {
                        closest.Point = intersection;
                        closest.Object = i;
                        closest.Normal = (mass[1] - mass[0]).VecMultiply(mass[2] - mass[0]);
                        count++;
                        if (count == 2)
                            continue;
                    }

                    //Sixth edge
                    mass[0] = hex.Points[2];
                    mass[1] = hex.Points[6];
                    mass[2] = hex.Points[5];
                    mass[3] = hex.Points[1];
                    intersection = IntersectRayEdge(mass, cameraPosition, viewPortPoint);
                    if ((intersection > 1) && (intersection < closest.Point))
                    {
                        closest.Point = intersection;
                        closest.Object = i;
                        closest.Normal = (mass[1] - mass[0]).VecMultiply(mass[2] - mass[0]);
                        count++;
                        if (count == 2)
                            continue;
                    }*/
                }
            }

            return closest;
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

        //private Closest 

        private double IntersectRayEdge(DoublePoint3D[] edgePoints, DoublePoint3D rayPoint, DoublePoint3D rayVector)
        {
            var ab = edgePoints[1] - edgePoints[0];
            var ac = edgePoints[3] - edgePoints[0];

            /*try
            {
                var a = (edgePoints[0].X - rayPoint.X) / rayVector.X;
                var b = ab.X / rayVector.X;
                var c = ac.X / rayVector.X;
                var d = (edgePoints[0].Y - rayPoint.Y - a * rayVector.Y) / (b * rayVector.Y - ab.Y);
                var e = (ac.Y - c * rayVector.Y) / (b * rayVector.Y - ab.Y);

                var h = (edgePoints[0].Z + d * ab.Z - rayPoint.Z - a * rayVector.Z - d * rayVector.Z) / (e * rayVector.Z + c * rayVector.Z - e * ab.Z - ac.Z);
                var w = d + e * h;
                var t = a + b * w + c * h;


                if ((0 <= h) && (h <= 1) && (0 <= w) && (w <= 1))
                    return t;
            }
            catch
            {
                return -1;
            }*/

            var n = ab.VecMultiply(ac);
            var v = edgePoints[0] - rayPoint;
            var d = n.ScalMultiply(v);
            var e = n.ScalMultiply(rayVector);
            if (e != 0)
            {
                var a = (rayPoint.X + d / e * rayVector.X - edgePoints[0].X) / ab.X;
                var b = ac.X / ab.X;
                var h = (rayPoint.Y + d / e * rayVector.Y - edgePoints[0].Y - a * ab.Y) / (b * ab.Y + ac.Y);
                var w = a + b * h;

                if ((0 <= w) && (w <= 1) && (0 <= h) && (h <= 1))
                    return d / e;
            }

            return -1;
        }
        #endregion

        #region Lightning
        private float ComputeLightning(DoublePoint3D intersectPoint, DoublePoint3D normalVector, DoublePoint3D vector, int specular)
        {
            var result = 0f;
            var interval = new double[] { 0.001, Inf };

            foreach (LightSource light in LightSources)
            {
                if (light.ToString().Contains("Ambient"))
                    result += light.Intense;
                else
                {
                    DoublePoint3D lightVector;
                    if (light.ToString().Contains("Point"))
                        lightVector = (light as PointLight).Coord - intersectPoint;
                    else
                        lightVector = (light as DirectLight).Vector;

                    //Shadows
                    Closest shadow = ClosestIntersection(intersectPoint, lightVector, interval);
                    float transparency = 1;
                    while (shadow.Object != -1)
                    {
                        if ((GraphicalObjects[shadow.Object].Transparency == 0) || (transparency == 0))
                        {
                            transparency = 0;
                            break;
                        }
                        transparency *= GraphicalObjects[shadow.Object].Transparency;
                        shadow = ClosestIntersection(intersectPoint + shadow.Point * lightVector, lightVector, interval);
                    }
                    if (transparency == 0)
                        continue;

                    //Diffusal Illumination
                    var scal = normalVector.ScalMultiply(lightVector);
                    if (scal > 0)
                        result += (float)(light.Intense * scal / (normalVector.Length() * lightVector.Length()));

                    //Specular Illumination
                    if (specular != -1)
                    {
                        var refleclectedVector = ReflectRay(lightVector, normalVector);
                        scal = refleclectedVector.ScalMultiply(vector);
                        if (scal > 0)
                            result += (float)(transparency * (light.Intense * Math.Pow(scal / (refleclectedVector.Length() * vector.Length()), specular)));
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

        #region Poligonism
        public double Intercept(DoublePoint3D vector,DoublePoint3D normal, DoublePoint3D[] vertex)
        {
            double d = Dot(normal, vector);
            if (d > 0)
            {
                double k = Dot(normal, vertex[0]) / d;
                DoublePoint3D point = k*vector;//new DoublePoint3D( vector.X * k, vector.Y * k, vector.Z * k );

                DoublePoint3D u = Sub(vertex[0], vertex[1]);
                DoublePoint3D v = Sub(vertex[0], vertex[2]);
                DoublePoint3D w = Sub(vertex[0], point);

                double uu = Dot(u, u);
                double uv = Dot(u, v);
                double vv = Dot(v, v);
                double wu = Dot(w, u);
                double wv = Dot(w, v);
                double D = uv * uv - uu * vv;
                double s, t;
                s = (uv * wv - vv * wu) / D;
                if (s < 0.0 || s > 1.0)
                {
                    return -1;
                }
                else
                {
                    t = (uv * wu - uu * wv) / D;
                    if (t < 0.0 || (s + t) > 1.0)
                    {
                        return -1;
                    }
                    else
                    {
                        return point.X / vector.X;
                    }
                }
            }
            return -1;
        }

        public static double Dot(DoublePoint3D firstVec, DoublePoint3D secondVec)
        {
            return firstVec.X * secondVec.X + firstVec.Y * secondVec.Y + firstVec.Z * secondVec.Z;
        }

        public static DoublePoint3D Sub(DoublePoint3D firstPoint, DoublePoint3D secondPoint)
        {
            return secondPoint - firstPoint;
        }
        #endregion

        #region structs
        /*struct Sphere
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
        }*/

        /*struct LightSource
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
        }*/

        private struct Closest
        {
            public int Object { get; set; }
            public double Point { get; set; }
            public DoublePoint3D Normal { get; set; }

            public Closest(int obj, double point)
            {
                Object = obj;
                Point = point;
                Normal = new DoublePoint3D();
            }

            public Closest(int obj, double point, DoublePoint3D normal)
            {
                Object = obj;
                Point = point;
                Normal = normal;
            }
        }
        #endregion
    }
}
