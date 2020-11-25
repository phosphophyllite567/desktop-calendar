using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopCalendar
{

    public partial class Form1 : Form
    {
        private Form2 childForm;
        public const int WM_SYSCOMMAND = 0x112;
        public const int SC_MOVE = 0xF012;
        private Color tr_color = Color.Transparent;
        

        public Form1()
        {

            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormInitialize();
           
        }

        private void FormInitialize()
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            this.Opacity = 0.2;
            this.Paint += FocusToday;
            this.Location = new Point(System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Width, 0);
            this.childForm = new Form2(this);
            this.childForm.Owner = this;    // 这支所属窗体              
            this.childForm.Dock = DockStyle.Fill;
            this.childForm.Show();
            this.childForm.BringToFront();
            childForm.Location = new Point(this.Location.X, this.Location.Y);
            this.Size = new Size(1000, 500);
            this.childForm.Size = new Size(this.Size.Width, this.Height);
            //mouseControl();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            UpdateStyles();
        }

       

        private void FocusToday(object sender, PaintEventArgs e)
        {
            /*
            panel9.BorderStyle = BorderStyle.None;
            Rectangle rectangle = panel9.ClientRectangle;
            rectangle.Inflate(10, 10);
            ControlPaint.DrawBorder3D(e.Graphics, rectangle,Border3DStyle.Etched);
            */

            Rectangle rectangle = this.ClientRectangle;
            rectangle.Inflate(10, 10);
            ControlPaint.DrawBorder3D(e.Graphics, rectangle, Border3DStyle.Bump);
        }


        protected override void WndProc(ref Message m)//对父类方法的重写，使得窗体无法拖动
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                if ((int)m.WParam == SC_MOVE)
                    return;
            }
            base.WndProc(ref m);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//关闭并保存用户信息
        {
            this.Close();
            childForm.SaveClientDate();
        }

        

        private void BottomMost()//将窗体固定在最底层
        {
            
            IntPtr hprog = User32.FindWindowEx(
                User32.FindWindowEx(
                    User32.FindWindow("Progman", "Program Manager"),
                    IntPtr.Zero, "SHELLDLL_DefView", ""
                ),
                IntPtr.Zero, "SysListView32", "FolderView"
            );
            User32.SetWindowLong(this.Handle, -8, hprog);
        }
        
    }
 }

