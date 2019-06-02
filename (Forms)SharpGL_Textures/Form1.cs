using System;
using System.Windows.Input;
using System.Windows.Forms;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using SharpGL.Enumerations;

namespace _Forms_SharpGL_Textures
{
    public partial class Form1 : Form
    {
        float Rotate_X;
        float Rotate_Y;
        Texture firstTexture = new Texture();
        Texture secondTexture = new Texture();

        public Form1()
        {
            InitializeComponent();
            OpenGL gl = openGLControl1.OpenGL;

            gl.Enable(OpenGL.GL_TEXTURE_2D); //Режим 2-мерных текстур
            firstTexture.Create(gl, "../../../firstTexture.png");
            secondTexture.Create(gl, "../../../secondTexture.jpg");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openGLControl1_Load(object sender, EventArgs e)
        {

        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs args)
        {
            OpenGL gl = openGLControl1.OpenGL;
            gl.ClearColor(1f, 1f, 1f, 0f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.Translate(0f, 0f, -3f); //Точка отрисовки

            #region BaseProject
            /*gl.Rotate(Rotate_Figure, 0f, 1f, 1f);
            Rotate_Figure += 4f;

            texture.Bind(gl); //Связка

            gl.Begin(OpenGL.GL_QUADS); //Квадраты

            //Font
            gl.TexCoord(0f, 0f); gl.Vertex(-1f, -1f, 1f);
            gl.TexCoord(1f, 0f); gl.Vertex(1f, -1f, 1f);
            gl.TexCoord(1f, 1f); gl.Vertex(1f, 1f, 1f);
            gl.TexCoord(0f, 1f); gl.Vertex(-1f, 1f, 1f);

            //Back
            gl.TexCoord(1f, 0f); gl.Vertex(-1f, -1f, -1f);
            gl.TexCoord(1f, 1f); gl.Vertex(-1f, 1f, -1f);
            gl.TexCoord(0f, 1f); gl.Vertex(1f, 1f, -1f);
            gl.TexCoord(0f, 0f); gl.Vertex(1f, -1f, -1f);

            //Top
            gl.TexCoord(0f, 1f); gl.Vertex(-1f, 1f, -1f);
            gl.TexCoord(0f, 0f); gl.Vertex(-1f, 1f, 1f);
            gl.TexCoord(1f, 0f); gl.Vertex(1f, 1f, 1f);
            gl.TexCoord(1f, 1f); gl.Vertex(1f, 1f, -1f);

            //Bottom
            gl.TexCoord(1f, 1f); gl.Vertex(-1f, -1f, -1f);
            gl.TexCoord(0f, 1f); gl.Vertex(1f, -1f, -1f);
            gl.TexCoord(0f, 0f); gl.Vertex(1f, -1f, -1f);
            gl.TexCoord(1f, 0f); gl.Vertex(-1f, -1f, 1f);

            //Right
            gl.TexCoord(1f, 0f); gl.Vertex(1f, -1f, -1f);
            gl.TexCoord(1f, 1f); gl.Vertex(1f, 1f, -1f);
            gl.TexCoord(0f, 1f); gl.Vertex(1f, 1f, 1f);
            gl.TexCoord(0f, 0f); gl.Vertex(1f, -1f, 1f);

            //Left
            gl.TexCoord(0f, 0f); gl.Vertex(-1f, -1f, -1f);
            gl.TexCoord(1f, 0f); gl.Vertex(-1f, -1f, 1f);
            gl.TexCoord(1f, 1f); gl.Vertex(-1f, 1f, 1f);
            gl.TexCoord(0f, 1f); gl.Vertex(-1f, 1f, -1f);

            gl.End();*/
            #endregion

            DrawCompGraphLab(gl);

            gl.Flush();
        }

        private void DrawCompGraphLab(OpenGL gl)
        {
            Rotation();
            gl.Rotate(Rotate_X, 0, 1, 0);
            gl.Rotate(Rotate_Y, 1, 0, 0);

            firstTexture.Bind(gl);
            gl.Begin(BeginMode.Polygon);
            gl.TexCoord(1f, 1f); gl.Vertex(-1f, -1f, 0f);
            gl.TexCoord(0f, 1f); gl.Vertex(1f, -1f, 0f);
            gl.TexCoord(0f, 0f); gl.Vertex(1f, 1f, 0f);
            gl.TexCoord(1f, 0f); gl.Vertex(-1f, 1f, 0f);
            gl.End();

            secondTexture.Bind(gl);
            gl.Begin(BeginMode.Polygon);
            gl.TexCoord(0f, 1f); gl.Vertex(-1f, -1f, 0.01f);
            gl.TexCoord(1f, 1f); gl.Vertex(1f, -1f, 0.01f);
            gl.TexCoord(1f, 0f); gl.Vertex(1f, 1f, 0.01f);
            gl.TexCoord(0f, 0f); gl.Vertex(-1f, 1f, 0.01f);
            gl.End();
        }

        private void Rotation()
        {
            if (Keyboard.IsKeyDown(Key.Left))
                Rotate_X -= 4f;
            if (Keyboard.IsKeyDown(Key.Right))
                Rotate_X += 4f;
            if (Keyboard.IsKeyDown(Key.Up))
                Rotate_Y += 4f;
            if (Keyboard.IsKeyDown(Key.Down))
                Rotate_Y -= 4f;
        }
    }
}
