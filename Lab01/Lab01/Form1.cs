﻿using SharpGL;
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
        int Selected = -1;  // có được chọn hay k
        int objectId = -1;  //Hình đang được chọn
        int chosenControlPointId = -1;  // điểm điều khiển được chọn

        enum Mode { Line, Circle, Rectangle, Ellipse, Triangle, Pentagon, Hexagon, Polygon, Select }; //Các chế độ vẽ hình
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
            this.components = new System.ComponentModel.Container();
            this.openGLControl = new SharpGL.OpenGLControl();
            this.btnEllipse = new System.Windows.Forms.Button();
            this.btnCircle = new System.Windows.Forms.Button();
            this.BtnHexagon = new System.Windows.Forms.Button();
            this.btnPentagon = new System.Windows.Forms.Button();
            this.btnRectangle = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnTriangle = new System.Windows.Forms.Button();
            this.btnPolygon = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_LineColor = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lst_Width = new System.Windows.Forms.NumericUpDown();
            this.lb_Time = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.timer_Drawing = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lst_Width)).BeginInit();
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
            this.openGLControl.Size = new System.Drawing.Size(659, 440);
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
            // btnPolygon
            // 
            this.btnPolygon.Image = global::Lab01.Properties.Resources.Hexagon;
            this.btnPolygon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPolygon.Location = new System.Drawing.Point(588, 27);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(83, 31);
            this.btnPolygon.TabIndex = 15;
            this.btnPolygon.Text = "Polygon";
            this.btnPolygon.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPolygon.UseVisualStyleBackColor = true;
            this.btnPolygon.Click += new System.EventHandler(this.btnPolygon_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Image = global::Lab01.Properties.Resources.Line1;
            this.btnSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelect.Location = new System.Drawing.Point(677, 27);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(76, 31);
            this.btnSelect.TabIndex = 16;
            this.btnSelect.Text = "Select";
            this.btnSelect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(699, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Line Color";
            // 
            // bt_LineColor
            // 
            this.bt_LineColor.BackColor = System.Drawing.SystemColors.ControlText;
            this.bt_LineColor.Location = new System.Drawing.Point(702, 111);
            this.bt_LineColor.Name = "bt_LineColor";
            this.bt_LineColor.Size = new System.Drawing.Size(65, 24);
            this.bt_LineColor.TabIndex = 18;
            this.bt_LineColor.UseVisualStyleBackColor = false;
            this.bt_LineColor.Click += new System.EventHandler(this.bt_LineColor_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(699, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Line Width";
            // 
            // lst_Width
            // 
            this.lst_Width.Location = new System.Drawing.Point(702, 174);
            this.lst_Width.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.lst_Width.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lst_Width.Name = "lst_Width";
            this.lst_Width.Size = new System.Drawing.Size(65, 20);
            this.lst_Width.TabIndex = 20;
            this.lst_Width.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lst_Width.ValueChanged += new System.EventHandler(this.lst_Width_ValueChanged);
            // 
            // lb_Time
            // 
            this.lb_Time.AutoSize = true;
            this.lb_Time.Location = new System.Drawing.Point(699, 491);
            this.lb_Time.Name = "lb_Time";
            this.lb_Time.Size = new System.Drawing.Size(37, 13);
            this.lb_Time.TabIndex = 21;
            this.lb_Time.Text = "0:00.0";
            // 
            // Lab01
            // 
            this.ClientSize = new System.Drawing.Size(780, 516);
            this.Controls.Add(this.lb_Time);
            this.Controls.Add(this.lst_Width);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bt_LineColor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnEllipse);
            this.Controls.Add(this.btnCircle);
            this.Controls.Add(this.BtnHexagon);
            this.Controls.Add(this.btnPentagon);
            this.Controls.Add(this.btnRectangle);
            this.Controls.Add(this.btnLine);
            this.Controls.Add(this.btnTriangle);
            this.Controls.Add(this.btnPolygon);
            this.Controls.Add(this.openGLControl);
            this.Name = "Lab01";
            this.Text = "Lab01";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lst_Width)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public Lab01()
        {
            InitializeComponent();

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
            btnPolygon.Enabled = true;
            btnSelect.Enabled = true;
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
            btnPolygon.Enabled = true;
            btnSelect.Enabled = true;
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
            btnPolygon.Enabled = true;
            btnSelect.Enabled = true;
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
            btnPolygon.Enabled = true;
            btnSelect.Enabled = true;
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
            btnPolygon.Enabled = true;
            btnSelect.Enabled = true;
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
            btnPolygon.Enabled = true;
            btnSelect.Enabled = true;
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
            btnPolygon.Enabled = true;
            btnSelect.Enabled = true;
            currentMode = Mode.Hexagon;
        }

        private void btnPolygon_Click(object sender, EventArgs e)
        {
            btnLine.Enabled = true;
            btnCircle.Enabled = true;
            btnRectangle.Enabled = true;
            btnEllipse.Enabled = true;
            btnTriangle.Enabled = true;
            btnPentagon.Enabled = true;
            BtnHexagon.Enabled = true;
            btnPolygon.Enabled = false;
            btnSelect.Enabled = true;
            currentMode = Mode.Polygon;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            btnLine.Enabled = true;
            btnCircle.Enabled = true;
            btnRectangle.Enabled = true;
            btnEllipse.Enabled = true;
            btnTriangle.Enabled = true;
            btnPentagon.Enabled = true;
            BtnHexagon.Enabled = true;
            btnPolygon.Enabled = true;
            btnSelect.Enabled = false;
            currentMode = Mode.Select;
        }

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {

            pStart.setPoint(e.Location.X, openGLControl.Height - e.Location.Y);
            // vẽ đa giác
            if (currentMode == Mode.Polygon)
            {
                if (!isDrawing)
                {
                    Selected = -1;
                    renderShapes();
                    currentShape = new MultiP_Poly();
                    currentShape.LineColor = currentLineColor;
                    currentShape.LineWidth = (int)lst_Width.Value;
                    isDrawing = true;
                    timer_Drawing.Start();
                    timeDrawing = 0;
                }
                return;
            }

            isDrawing = true;

            //Chế độ chọn hình
            if (currentMode == Mode.Select)
            {
                int x = pStart.X, y = pStart.Y;

                if (objectId >= 0) //Xác định có kéo điểm điều khiển của hình được chọn hay không
                {
                    chosenControlPointId = shapes[objectId].getControlPointId(x, y);
                    if (chosenControlPointId >= 0)
                    {
                        //lb_Forward.Enabled = true;
                        //lb_Backward.Enabled = true;
                        //lb_Delete.Enabled = true;
                        return;
                    }
                }

                //Xác định hình được chọn
                objectId = -1;
                for (int i = n_shapes - 1; i >= 0; i--)
                    if (shapes[i].isInsideBox(x, y))
                    {
                        objectId = i;
                        break;
                    }

                renderShapes();

                if (objectId >= 0) //Có hình được chọn thì vẽ điểm điều khiển cho hình đó
                {
                    shapes[objectId].drawControlBox(gl);
                    //lst_Width.Value = shapes[objectId].LineWidth;
                    //lb_Forward.Enabled = true;
                    //lb_Backward.Enabled = true;
                    //lb_Delete.Enabled = true;
                }

                return;
            }

            switch (currentMode)
            {
                case Mode.Line:
                    currentShape = new Line();
                    break;
                case Mode.Triangle:
                    currentShape = new Triangle();
                    break;
                case Mode.Rectangle:
                    currentShape = new Rectangle();
                    break;
                case Mode.Circle:
                    currentShape = new Circle();
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
            currentShape.LineColor = currentLineColor;
            currentShape.LineWidth = (int)lst_Width.Value;
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;
            pEnd.setPoint(e.Location.X, openGLControl.Height - e.Location.Y); //Cập nhật điểm chặn cuối khi di chuyển chuột
            if (currentMode == Mode.Polygon)
            {
                ((MultiP_Poly)currentShape).moveVertex(pEnd);
                renderShapes();
                currentShape.Draw(gl);
            }

            else if (currentMode == Mode.Select)
            {
                if (objectId >= 0)
                {
                    if (chosenControlPointId >= 0)
                        shapes[objectId].dragControlPoint(chosenControlPointId, pStart, pEnd); //Co giãn hình qua điểm điều khiển
                    else shapes[objectId].translate(pStart, pEnd); //Di chuyển tịnh tiến hình được chọn

                    if (chosenControlPointId != 8)
                        pStart.setPoint(pEnd);
                    renderShapes();
                    shapes[objectId].drawControlBox(gl);
                }
            }
            else
            {
                //Đang vẽ hình
                currentShape.set(pStart, pEnd); //Cập nhật kích thước hình đang vẽ      
                renderShapes();
                currentShape.Draw(gl);
                gl.Flush();
            }
        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (currentMode == Mode.Polygon)
            {
                if (isDrawing)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)    //Kết thúc vẽ một đa giác
                    {
                        shapes.Add(((MultiP_Poly)currentShape).getPolygon());
                        n_shapes++;
                        isDrawing = false;
                        timer_Drawing.Stop();
                        renderShapes();
                    }
                    else
                    {
                        if (((MultiP_Poly)currentShape).nPoly == 0)
                        {
                            ((MultiP_Poly)currentShape).addVertex(new Point(e.Location.X, openGLControl.Height - e.Location.Y));
                        }
                        ((MultiP_Poly)currentShape).addVertex(new Point(e.Location.X, openGLControl.Height - e.Location.Y));

                        currentShape.Draw(gl);
                    }
                }
                return;
            }

            isDrawing = false;

            if (currentMode == Mode.Select)
            {
                if (chosenControlPointId >= 0)  //Vừa mới kéo thả điểm điều khiển
                {
                    renderShapes();
                    shapes[objectId].drawControlBox(gl);
                    chosenControlPointId = -1;
                }
            }
            else
            {
                //Hoàn tất vẽ hình
                timer_Drawing.Stop();
                //Thêm hình mới vào danh sách các hình đã vẽ
                shapes.Add(currentShape);
                n_shapes++;
            }
        }

        private void timer_Drawing_Tick(object sender, EventArgs e)
        {
            timeDrawing++;
            int min, sec, mil;
            mil = timeDrawing % 10;
            sec = (timeDrawing / 10) % 60;
            min = timeDrawing / 600;
            lb_Time.Text = min.ToString() + ":" + (sec < 10 ? "0" : "") + sec.ToString() + "." + mil.ToString();
        }

        private void bt_LineColor_Click(object sender, EventArgs e)
        {
            //Chọn màu viền
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentLineColor = new Color(colorDialog.Color.R / 255.0f, colorDialog.Color.G / 255.0f, colorDialog.Color.B / 255.0f);
                bt_LineColor.BackColor = colorDialog.Color;
            }
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

        private void lst_Width_ValueChanged(object sender, EventArgs e)
        {

        }

        private void openGLControl_Load(object sender, EventArgs e)
        {
            gl.ClearColor(1f, 1f, 1f, 1f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
        }



    }
}
