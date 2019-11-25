using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab01
{
    public partial class Lab01 : Form
    {
        OpenGL gl;
        List<Object> shapes = new List<Object>(); //Các hình đã vẽ
        int n_shapes = 0; //Số lượng hình đã vẽ       
        Object currentShape; //Hình đang được vẽ

        enum Mode { Line, Circle, Rectangle, Ellipse, Triangle, Pentagon, Hexagon }; //Các chế độ vẽ hình
        Mode currentMode = Mode.Line;   //Chế độ hiện tại
        Color currentLineColor = new Color();   //Màu viền hiện tại            

        bool isDrawing = false; //Đang nhấn chuột để vẽ hình mới   

        Point pStart = new Point(), //Điểm chăn khởi đầu lúc nhấn chuột
            pEnd = new Point(); //Điểm chạn đích khi di chuyển chuột và khi thả chuột       

        int timeDrawing = 0;    //Thời gian vẽ hình
        //***********************************
        OpenGLControl openGLControl;
        private void renderShapes()
        {            
            //Reset lại khung vẽ
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //Vẽ lại các hình
            foreach (Object s in shapes)
                s.Draw(gl);
            gl.Flush();

        }
        private void InitializeComponent()
        {
            this.openGLControl = new SharpGL.OpenGLControl();
            this.btnEllipse = new System.Windows.Forms.Button();
            this.btnCircle = new System.Windows.Forms.Button();
            this.BtnHexagon = new System.Windows.Forms.Button();
            this.btnPentagon = new System.Windows.Forms.Button();
            this.btnRectangle = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnTriangle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.DrawFPS = false;
            this.openGLControl.Location = new System.Drawing.Point(12, 64);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(694, 440);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            this.openGLControl.Load += new System.EventHandler(this.openGLControl_Load);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseDown);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseMove);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseUp);
            // 
            // btnEllipse
            // 
            this.btnEllipse.Image = global::Lab01.Properties.Resources.Ellipse;
            this.btnEllipse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEllipse.Location = new System.Drawing.Point(251, 27);
            this.btnEllipse.Name = "btnEllipse";
            this.btnEllipse.Size = new System.Drawing.Size(75, 31);
            this.btnEllipse.TabIndex = 14;
            this.btnEllipse.Text = "Ellipse";
            this.btnEllipse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEllipse.UseVisualStyleBackColor = true;
            this.btnEllipse.Click += new System.EventHandler(this.btnEllipse_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.Image = global::Lab01.Properties.Resources.Circle;
            this.btnCircle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCircle.Location = new System.Drawing.Point(77, 27);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(65, 31);
            this.btnCircle.TabIndex = 13;
            this.btnCircle.Text = "Circle";
            this.btnCircle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCircle.UseVisualStyleBackColor = true;
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // BtnHexagon
            // 
            this.BtnHexagon.Image = global::Lab01.Properties.Resources.Hexagon;
            this.BtnHexagon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnHexagon.Location = new System.Drawing.Point(499, 27);
            this.BtnHexagon.Name = "BtnHexagon";
            this.BtnHexagon.Size = new System.Drawing.Size(83, 31);
            this.BtnHexagon.TabIndex = 12;
            this.BtnHexagon.Text = "Hexagon";
            this.BtnHexagon.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnHexagon.UseVisualStyleBackColor = true;
            this.BtnHexagon.Click += new System.EventHandler(this.BtnHexagon_Click);
            // 
            // btnPentagon
            // 
            this.btnPentagon.Image = global::Lab01.Properties.Resources.Pentagon;
            this.btnPentagon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPentagon.Location = new System.Drawing.Point(409, 27);
            this.btnPentagon.Name = "btnPentagon";
            this.btnPentagon.Size = new System.Drawing.Size(84, 31);
            this.btnPentagon.TabIndex = 11;
            this.btnPentagon.Text = "Pentagon";
            this.btnPentagon.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPentagon.UseVisualStyleBackColor = true;
            this.btnPentagon.Click += new System.EventHandler(this.btnPentagon_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.Image = global::Lab01.Properties.Resources.Rectangle;
            this.btnRectangle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRectangle.Location = new System.Drawing.Point(154, 27);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(91, 31);
            this.btnRectangle.TabIndex = 10;
            this.btnRectangle.Text = "Rectangle";
            this.btnRectangle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRectangle.UseVisualStyleBackColor = true;
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnLine
            // 
            this.btnLine.Enabled = false;
            this.btnLine.Image = global::Lab01.Properties.Resources.Line1;
            this.btnLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLine.Location = new System.Drawing.Point(12, 27);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(59, 31);
            this.btnLine.TabIndex = 9;
            this.btnLine.Text = "Line";
            this.btnLine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnTriangle
            // 
            this.btnTriangle.Image = global::Lab01.Properties.Resources.Triangle1;
            this.btnTriangle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTriangle.Location = new System.Drawing.Point(332, 27);
            this.btnTriangle.Name = "btnTriangle";
            this.btnTriangle.Size = new System.Drawing.Size(71, 31);
            this.btnTriangle.TabIndex = 8;
            this.btnTriangle.Text = "Triangle";
            this.btnTriangle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTriangle.UseVisualStyleBackColor = true;
            this.btnTriangle.Click += new System.EventHandler(this.btnTriangle_Click);
            // 
            // Lab01
            // 
            this.ClientSize = new System.Drawing.Size(740, 516);
            this.Controls.Add(this.btnEllipse);
            this.Controls.Add(this.btnCircle);
            this.Controls.Add(this.BtnHexagon);
            this.Controls.Add(this.btnPentagon);
            this.Controls.Add(this.btnRectangle);
            this.Controls.Add(this.btnLine);
            this.Controls.Add(this.btnTriangle);
            this.Controls.Add(this.openGLControl);
            this.Name = "Lab01";
            this.Text = "Lab01";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.ResumeLayout(false);

        }
        public Lab01()
        {
            InitializeComponent();
        }


        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            pStart.setPoint(e.Location.X, openGLControl.Height - e.Location.Y);
            isDrawing = true;
            switch (currentMode)
            {
                case Mode.Line:
                    currentShape = new Line();
                    break;
                case Mode.Triangle:
                    //currentShape = new Triangle();
                    break;
                case Mode.Rectangle:
                    currentShape = new Rectangle();
                    break;
                case Mode.Circle:
                    //currentShape = new Circle();
                    break;
                case Mode.Ellipse:
                    currentShape = new Ellipse();
                    break;
                case Mode.Pentagon:
                    currentShape = new Pentagon();
                    break;
                case Mode.Hexagon:
                    currentShape = new Hexagon();
                    break;
            }
            //currentShape.LineColor = currentLineColor;
            //currentShape.LineWidth = (int)lst_Width.Value;
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;
            pEnd.setPoint(e.Location.X, openGLControl.Height - e.Location.Y); //Cập nhật điểm chặn cuối khi di chuyển chuột

            //Đang vẽ hình
            currentShape.set(pStart, pEnd); //Cập nhật kích thước hình đang vẽ      
            renderShapes();
            currentShape.Draw(gl);
            gl.Flush();
        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;

            //Hoàn tất vẽ hình
            //timer_Drawing.Stop();
            //Thêm hình mới vào danh sách các hình đã vẽ
            shapes.Add(currentShape);
            n_shapes++;
        }
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            gl = openGLControl.OpenGL;
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
        }
        private void openGLControl_Resized(object sender, EventArgs e)
        {
            //Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            //Load the identity.
            gl.LoadIdentity();
            //Create a perspective transformation.
            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);
            gl.Ortho2D(0, openGLControl.Width, 0, openGLControl.Height);
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            btnLine.Enabled = false;
            btnCircle.Enabled = true;
            btnRectangle.Enabled = true;
            btnEllipse.Enabled = true;
            btnTriangle.Enabled = true;
            btnPentagon.Enabled = true;
            BtnHexagon.Enabled = true;
            currentMode = Mode.Line;
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            btnLine.Enabled = true;
            btnCircle.Enabled = false;
            btnRectangle.Enabled = true;
            btnEllipse.Enabled = true;
            btnTriangle.Enabled = true;
            btnPentagon.Enabled = true;
            BtnHexagon.Enabled = true;
            currentMode = Mode.Circle;
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            btnLine.Enabled = true;
            btnCircle.Enabled = true;
            btnRectangle.Enabled = false;
            btnEllipse.Enabled = true;
            btnTriangle.Enabled = true;
            btnPentagon.Enabled = true;
            BtnHexagon.Enabled = true;
            currentMode = Mode.Rectangle;
        }

        private void btnEllipse_Click(object sender, EventArgs e)
        {
            btnLine.Enabled = true;
            btnCircle.Enabled = true;
            btnRectangle.Enabled = true;
            btnEllipse.Enabled = false;
            btnTriangle.Enabled = true;
            btnPentagon.Enabled = true;
            BtnHexagon.Enabled = true;
            currentMode = Mode.Ellipse;
        }

        private void btnTriangle_Click(object sender, EventArgs e)
        {
            btnLine.Enabled = true;
            btnCircle.Enabled = true;
            btnRectangle.Enabled = true;
            btnEllipse.Enabled = true;
            btnTriangle.Enabled = false;
            btnPentagon.Enabled = true;
            BtnHexagon.Enabled = true;
            currentMode = Mode.Triangle;
        }

        private void btnPentagon_Click(object sender, EventArgs e)
        {
            btnLine.Enabled = true;
            btnCircle.Enabled = true;
            btnRectangle.Enabled = true;
            btnEllipse.Enabled = true;
            btnTriangle.Enabled = true;
            btnPentagon.Enabled = false;
            BtnHexagon.Enabled = true;
            currentMode = Mode.Pentagon;
        }

        private void BtnHexagon_Click(object sender, EventArgs e)
        {
            btnLine.Enabled = true;
            btnCircle.Enabled = true;
            btnRectangle.Enabled = true;
            btnEllipse.Enabled = true;
            btnTriangle.Enabled = true;
            btnPentagon.Enabled = true;
            BtnHexagon.Enabled = false;
            currentMode = Mode.Hexagon;
        }

        private void openGLControl_Load(object sender, EventArgs e)
        {
            gl.ClearColor(1f, 1f, 1f, 1f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
        }


        
    }
}
