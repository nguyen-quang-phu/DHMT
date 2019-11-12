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

        enum Mode { Line, Circle, Rectangle, Eclipse, Triangle, Pentagon, Hexagon }; //Các chế độ vẽ hình
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
            this.rd_Line = new System.Windows.Forms.RadioButton();
            this.rd_Tri = new System.Windows.Forms.RadioButton();
            this.rd_Rec = new System.Windows.Forms.RadioButton();
            this.rd_pen = new System.Windows.Forms.RadioButton();
            this.rd_hex = new System.Windows.Forms.RadioButton();
            this.rd_cir = new System.Windows.Forms.RadioButton();
            this.rd_ecl = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.DrawFPS = false;
            this.openGLControl.Location = new System.Drawing.Point(12, 71);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(561, 407);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            this.openGLControl.Load += new System.EventHandler(this.openGLControl_Load);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseDown);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseMove);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseUp);
            // 
            // rd_Line
            // 
            this.rd_Line.AutoSize = true;
            this.rd_Line.Location = new System.Drawing.Point(48, 33);
            this.rd_Line.Name = "rd_Line";
            this.rd_Line.Size = new System.Drawing.Size(45, 17);
            this.rd_Line.TabIndex = 1;
            this.rd_Line.TabStop = true;
            this.rd_Line.Text = "Line";
            this.rd_Line.UseVisualStyleBackColor = true;
            this.rd_Line.CheckedChanged += new System.EventHandler(this.rd_Line_CheckedChanged);
            // 
            // rd_Tri
            // 
            this.rd_Tri.AutoSize = true;
            this.rd_Tri.Location = new System.Drawing.Point(100, 33);
            this.rd_Tri.Name = "rd_Tri";
            this.rd_Tri.Size = new System.Drawing.Size(63, 17);
            this.rd_Tri.TabIndex = 2;
            this.rd_Tri.TabStop = true;
            this.rd_Tri.Text = "Triangle";
            this.rd_Tri.UseVisualStyleBackColor = true;
            this.rd_Tri.CheckedChanged += new System.EventHandler(this.rd_Tri_CheckedChanged);
            // 
            // rd_Rec
            // 
            this.rd_Rec.AutoSize = true;
            this.rd_Rec.Location = new System.Drawing.Point(170, 33);
            this.rd_Rec.Name = "rd_Rec";
            this.rd_Rec.Size = new System.Drawing.Size(74, 17);
            this.rd_Rec.TabIndex = 3;
            this.rd_Rec.TabStop = true;
            this.rd_Rec.Text = "Rectangle";
            this.rd_Rec.UseVisualStyleBackColor = true;
            this.rd_Rec.CheckedChanged += new System.EventHandler(this.rd_Rec_CheckedChanged);
            // 
            // rd_pen
            // 
            this.rd_pen.AutoSize = true;
            this.rd_pen.Location = new System.Drawing.Point(251, 33);
            this.rd_pen.Name = "rd_pen";
            this.rd_pen.Size = new System.Drawing.Size(71, 17);
            this.rd_pen.TabIndex = 4;
            this.rd_pen.TabStop = true;
            this.rd_pen.Text = "Pentagon";
            this.rd_pen.UseVisualStyleBackColor = true;
            this.rd_pen.CheckedChanged += new System.EventHandler(this.rd_pen_CheckedChanged);
            // 
            // rd_hex
            // 
            this.rd_hex.AutoSize = true;
            this.rd_hex.Location = new System.Drawing.Point(329, 33);
            this.rd_hex.Name = "rd_hex";
            this.rd_hex.Size = new System.Drawing.Size(68, 17);
            this.rd_hex.TabIndex = 5;
            this.rd_hex.TabStop = true;
            this.rd_hex.Text = "Hexagon";
            this.rd_hex.UseVisualStyleBackColor = true;
            this.rd_hex.CheckedChanged += new System.EventHandler(this.rd_hex_CheckedChanged);
            // 
            // rd_cir
            // 
            this.rd_cir.AutoSize = true;
            this.rd_cir.Location = new System.Drawing.Point(404, 33);
            this.rd_cir.Name = "rd_cir";
            this.rd_cir.Size = new System.Drawing.Size(51, 17);
            this.rd_cir.TabIndex = 6;
            this.rd_cir.TabStop = true;
            this.rd_cir.Text = "Circle";
            this.rd_cir.UseVisualStyleBackColor = true;
            this.rd_cir.CheckedChanged += new System.EventHandler(this.rd_cir_CheckedChanged);
            // 
            // rd_ecl
            // 
            this.rd_ecl.AutoSize = true;
            this.rd_ecl.Location = new System.Drawing.Point(462, 33);
            this.rd_ecl.Name = "rd_ecl";
            this.rd_ecl.Size = new System.Drawing.Size(59, 17);
            this.rd_ecl.TabIndex = 7;
            this.rd_ecl.TabStop = true;
            this.rd_ecl.Text = "Eclipse";
            this.rd_ecl.UseVisualStyleBackColor = true;
            this.rd_ecl.CheckedChanged += new System.EventHandler(this.rd_ecl_CheckedChanged);
            // 
            // Lab01
            // 
            this.ClientSize = new System.Drawing.Size(629, 482);
            this.Controls.Add(this.rd_ecl);
            this.Controls.Add(this.rd_cir);
            this.Controls.Add(this.rd_hex);
            this.Controls.Add(this.rd_pen);
            this.Controls.Add(this.rd_Rec);
            this.Controls.Add(this.rd_Tri);
            this.Controls.Add(this.rd_Line);
            this.Controls.Add(this.openGLControl);
            this.Name = "Lab01";
            this.Text = "Lab01";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public Lab01()
        {
            InitializeComponent();
        }

       


        private void rd_Line_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rd_Tri_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rd_Rec_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rd_pen_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rd_hex_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rd_cir_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rd_ecl_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            pStart.setPoint(e.Location.X, openGLControl.Height - e.Location.Y);
            isDrawing = true;
            currentShape = new Line();
            currentShape.LineColor = currentLineColor;
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

        private void openGLControl_Load(object sender, EventArgs e)
        {
            gl.ClearColor(1f, 1f, 1f, 1f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
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
    }
}
