using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopCalendar
{
    public partial class LogginForm : Form
    {
        
        public LogginForm()
        {
            
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 0.8;//透明度
            
            this.ShowInTaskbar = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string id = textBox1.Text.Trim();
                string pwd = textBox2.Text;
                REST_api.RESTClient client = new REST_api.RESTClient(@"http://localhost:8000/Service/", REST_api.EnumHttpVerb.GET);
                string info = client.HttpRequest(@"Client/Login/id=" + id + "&pwd=" + pwd);
                if (info == "\"true\"")
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("登录成功", "成功", buttons);
                    this.Close();
                }
                else
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("登录失败", "失败", buttons);
                }
            }
            catch(Exception exc)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(exc.Message, null, buttons);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
               
                string id = textBox1.Text.Trim();
                string pwd = textBox2.Text;
                REST_api.RESTClient client = new REST_api.RESTClient(@"http://localhost:8000/Service/", REST_api.EnumHttpVerb.POST);
                User user = new User();
                user.ID = int.Parse(id);
                user.Pwd = pwd;
                client.PostData= JsonConvert.SerializeObject(user);
                var resultPost = client.HttpRequest(@"Client/Register");
                JObject o = (JObject)JToken.Parse(resultPost);
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("注册成功，您的id为" + o["ID"] + ",密码为" + o["Pwd"], "结果", buttons);
                //MessageBox.Show(resultPost, "结果", buttons);
            }
            
           catch (Exception exc)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(exc.Message, null, buttons);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
