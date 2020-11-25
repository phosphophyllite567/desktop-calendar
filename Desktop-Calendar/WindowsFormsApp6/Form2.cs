using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace DesktopCalendar
{
    public partial class Form2 : Form
    {
        private CalendarData calendarData;
        //储存日历数据
        private static List<Control> allCtrls = new List<Control>();
        //储存所有子控件
        Form1 parentForm;

        static ClientData CalendarUser = new ClientData();
        REST_api.RESTService REST = new REST_api.RESTService(CalendarUser);


        private int calendarIndex;
        //当前第一行显示的日历数据的索引
        private ClientData clientData;
        //储存文本框数据
        public Form2(Form1 f)
        {
            calendarData = new CalendarData();
            parentForm = f;
            clientData = new ClientData();
            InitializeComponent();
            FormInitialize();

        }

        private void FormInitialize()
        {
            FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 0.8;//透明度
            this.TransparencyKey = this.BackColor;
            this.ShowInTaskbar = false;
            CheckAllCtrls(this);//将控件加入队列
            SetAllCtrls();//设置所有控件
            panel12.BorderStyle = BorderStyle.Fixed3D;
            calendarIndex = 1;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private static void CheckAllCtrls(Control item)//将所有子控件加入队列
        {
            for (int i = 0; i < item.Controls.Count; i++)
            {
                if (item.Controls[i].HasChildren)
                {
                    CheckAllCtrls(item.Controls[i]);

                }
                //else{allCtrls.Enqueue (item.Controls[i]);}//如果只要子控件，那么这个语句在else里
                allCtrls.Add(item.Controls[i]);
            }
            allCtrls = allCtrls.OrderBy(n => n.Name).ToList();
            

        }

        private void SetAllCtrls()//对控件属性进行初始设置
        {
            foreach(Control control in allCtrls)
            {
                if (control is Label&& control.Name.Substring(0, 5) == "label")
                {
                    control.Font = label10.Font;
                    control.ForeColor = label10.ForeColor;
                    control.DoubleClick += new EventHandler(Label_DoubleClick);//标签的双击事件
                }
                else if (control is TextBox)
                {
                    control.KeyPress += new KeyPressEventHandler(textBox_PressEnter);//按下回车进行文本框的确定
                    control.LostFocus += new EventHandler(textBox_Leave);//文本框失去焦点
                }
                else if (control is Button && control.Name.Substring(0, 5) == "button")
                {
                    control.Click += new EventHandler(button_Click);//按钮的点击事件
                }
                else if(control is Panel)
                {
                    Panel panel = (Panel)control;
                    panel.BorderStyle = BorderStyle.FixedSingle;
                }
            }
        }

        
        private void Label_DoubleClick(object sender, EventArgs e)//双击日期标签后，出现文本框和色彩选择
        {
            Label l = (Label)sender;
            foreach(Control control in l.Parent.Controls)
            {
                if (control is TextBox)
                {
                    control.TabIndex = 0;
                    control.Enabled = true;
                    control.BackColor = Color.White;
                }
                else if (control is Button)
                    control.Visible = true;
               

            }
            /*
            textBox1.Enabled = true;
            textBox1.BackColor = Color.White ;
            button1.Visible = true;
            label2.Visible = false;
            */
        }

        private void button_Click(object sender, EventArgs e)//点击色彩选择按钮后，唤出对话框
        {
            Button b = (Button)sender;
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = b.Parent.BackColor;

            // Update the text box color if the user clicks OK 
            if (colorDialog.ShowDialog() == DialogResult.OK)
                b.Parent.BackColor = colorDialog.Color;
        }


        private void textBox_PressEnter(object sender, KeyPressEventArgs e)//日记输入完成后，按回车确定
        {
            if (e.KeyChar == 13)
            {
                TextBox t = (TextBox)sender;
                t.BackColor = t.Parent.BackColor;
                t.Enabled = false;
                
                foreach (Control control in t.Parent.Controls)
                {
                    if (control is Button)
                        control.Visible = false;
                }
            }
            /*
            button1.Visible = false;
            label2.Visible = true;
            */
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            
             TextBox t = (TextBox)sender;
             t.BackColor = t.Parent.BackColor;
             t.Enabled = false;

             foreach (Control control in t.Parent.Controls)
             {
                  if (control is Button)
                     control.Visible = false;
             }
           
            /*
            button1.Visible = false;
            label2.Visible = true;
            */
        }

        private void button8_Click(object sender, EventArgs e)//关闭窗口并储存相关用户数据
        {
            
            this.Close();
            parentForm.Close();
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void upBotton_Click(object sender, EventArgs e)//日历向上翻页
        {
            calendarIndex++;
          
            foreach(Control control in allCtrls)
            {
                
                if (control is Label && control.Name.Substring(0, 5) == "label")//将label的数据上移
                    {
                        int labelid = Int32.Parse(control.Name.Substring(5));
                    
                        if (labelid > 7)//整体上移一行
                        {
                            string newlabelid = "label" + (labelid - 7).ToString();
                            Label label = (Label)this.Controls.Find(newlabelid, true)[0];
                            label.Text = control.Text;
                            label.Parent.BackColor = control.Parent.BackColor;
                        }
                        if (labelid > 14)//最后一行补充数据
                        {
                            try
                            {
                                control.Text = calendarData.datas[calendarIndex * 7 + 14 + (labelid - 1) % 7].Date;
                                switch (labelid % 7)
                                {
                                    case 1:
                                    case 2:
                                    case 3:
                                    case 4:
                                    case 5:
                                        control.Parent.BackColor = Color.PaleTurquoise; break;
                                    case 6:
                                        control.Parent.BackColor = Color.Plum; break;
                                    case 0:
                                        control.Parent.BackColor = Color.Pink; break;
                                }
                            }
                            catch (Exception exc) { }
                         }
                        
                     
             }
                
                if(control is TextBox)//将textbox的数据上移
                {
                    int boxid=-1;
                    if (control.Name.Length>=7)
                        boxid = Int32.Parse(control.Name.Substring(7));
                    if(boxid > 7)//整体上移一行
                    {
                        string newboxid = "textBox" + (boxid - 7).ToString();
                        TextBox textbox =(TextBox)this.Controls.Find(newboxid, true)[0];
                        textbox.Text = control.Text;
                        textbox.BackColor = control.BackColor;
                    }
                    if (boxid > 14)//最后一行补充数据
                    {
                        control.ResetText();
                        switch (boxid % 7)
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                                control.Parent.BackColor = Color.PaleTurquoise; break;
                            case 6:
                                control.Parent.BackColor = Color.Plum; break;
                            case 0:
                                control.Parent.BackColor = Color.Pink; break;
                        }
                    }
                }
            }
        }

        private void downButton_Click(object sender, EventArgs e)//日历向下翻页
        {
            calendarIndex--;
            
            foreach (Control control in allCtrls)
            {
                if (control is Label && control.Name.Substring(0, 5) == "label")//将label的数据下移
                {
                    int labelid = Int32.Parse(control.Name.Substring(5));
                    {
                        if (labelid < 15)
                        {
                            string newlabelid = "label" + (labelid + 7).ToString();
                            Label label = (Label)this.Controls.Find(newlabelid, true)[0];
                            label.Text = control.Text;
                            label.Parent.BackColor = control.Parent.BackColor;
                        }
                        if (labelid < 8)
                        {
                            try
                            {
                                control.Text = calendarData.datas[calendarIndex * 7 + (labelid - 1) % 7].Date;
                                switch (labelid % 7)
                                {
                                    case 1:
                                    case 2:
                                    case 3:
                                    case 4:
                                    case 5:
                                        control.Parent.BackColor = Color.PaleTurquoise; break;
                                    case 6:
                                        control.Parent.BackColor = Color.Plum; break;
                                    case 0:
                                        control.Parent.BackColor = Color.Pink; break;
                                }
                            }
                            catch (Exception exc) { }
                        }
                        
                    }
                }
                if (control is TextBox)//将textbox的数据上移
                {
                    int boxid = -1;
                    if (control.Name.Length >= 7)
                        boxid = Int32.Parse(control.Name.Substring(7));
                    if (boxid <15)
                    {
                        string newboxid = "textBox" + (boxid + 7).ToString();
                        TextBox textbox = (TextBox)this.Controls.Find(newboxid, true)[0];
                        textbox.Text = control.Text;
                        textbox.BackColor = control.BackColor;
                    }
                    if (boxid <8)
                    {
                        control.ResetText();
                        switch (boxid % 7)
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                                control.BackColor = Color.PaleTurquoise;
                                control.Parent.BackColor = Color.PaleTurquoise; break;
                            case 6:
                                control.BackColor = Color.Plum;
                                control.Parent.BackColor = Color.Plum; break;
                            case 0:
                                control.BackColor = Color.Pink;
                                control.Parent.BackColor = Color.Pink; break;
                        }
                    }
                }
            }
        }

        public void SaveClientDate()//保存用户信息
        {

        }

        private void LogButton_Click(object sender, EventArgs e)
        {
            LogginForm form = new LogginForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        private void QueryButton_Click(object sender, EventArgs e)
        {
            QueryForm form = new QueryForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }
    }
}
