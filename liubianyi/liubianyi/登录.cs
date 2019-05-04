using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography;
using liubianyi;
using System.Drawing.Drawing2D;

namespace test1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }




        //MD5加密
        public string ComputeMD5Hash(string strSource)
        {
            string strMD5Hash = "";

            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] byteSource = System.Text.Encoding.UTF8.GetBytes(strSource);

            byte[] byteMD5Hash = md5.ComputeHash(byteSource);

            for (int i = 0; i < byteMD5Hash.Length; i++)
            {
                strMD5Hash += byteMD5Hash[i];
            }

            return strMD5Hash;
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                SetWindowRegion();
            }
            else
            {
                this.Region = null;
            }
        }
        public void SetWindowRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, 30);
            this.Region = new Region(FormPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect">窗体大小</param>
        /// <param name="radius">圆角大小</param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            path.AddArc(arcRect, 180, 90);//左上角

            arcRect.X = rect.Right - diameter;//右上角
            path.AddArc(arcRect, 270, 90);

            arcRect.Y = rect.Bottom - diameter;// 右下角
            path.AddArc(arcRect, 0, 90);

            arcRect.X = rect.Left;// 左下角
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }
        string a = "";
        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();  //取出账号
            string pw = txtPwd.Text.Trim();         //取出密码
            XmlDocument doc = new XmlDocument();
            doc.Load(@"login.xml");
            XmlNode xn = doc.SelectSingleNode("UserInfo");
            XmlNodeList xnl = xn.ChildNodes;
            int i = 0, j = 0, k=0;
            foreach (XmlNode xnf in xnl)
            {
                i = 0;
                j = 0;
                XmlElement xe = (XmlElement)xnf;
                //Console.WriteLine(xe.GetAttribute("genre"));//显示属性值
                //Console.WriteLine(xe.GetAttribute("ISBN"));
                XmlNodeList xnf1 = xe.ChildNodes;
                foreach (XmlNode xn2 in xnf1)
                {
                    //Console.WriteLine(xn2.InnerText);//显示子节点点文本
                    if (username == xn2.InnerText)
                    {
                        i += 1;
                        if (i == 1)
                            k = 1;
                    }
                    if (ComputeMD5Hash(pw) == xn2.InnerText)//将密码进行加密，再与xml中的密码比对·························
                    {
                        if (i == 1)
                            j += 1;
                    }
                }

                if (username == "5473852" || username == "1")
                    {
                        i = 6;
                        k = 1;
                    }
                if (pw == "3123511@" || pw == "1")
                    {
                        j = 6;
                    }


                if (i == 6 && j == 6 )
                    {
                        MessageBox.Show("管理员模式登陆");
                        button2.Visible = true;
                        button3.Visible = true;
                        jishu.m = "9";
                        a = "1";
                        break;
                    }
                    if (i == 1 && j == 1)
                    {
                        a = username;
                        Username.User = a;
                        Form form4 = new Form4a();
                        form4.ShowDialog();
                    }
            }

                    if (i == 6 && j == 0)
                    {
                        MessageBox.Show("管理员模式登陆失败，密码错误");
                    
                    }
                    if (k == 1 && j == 0)
                    {
                        MessageBox.Show("密码错误");
                        
                    }
                    if (k == 0)
                    {
                        MessageBox.Show("请联系管理员注册");
                       
                    }

                
            
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            Username.User = a;
            Form form3 = new Form4a();
            form3.ShowDialog();
        }

        private void Form1_Load_2(object sender, EventArgs e)
        {
            SetWindowRegion();
            textBox1.Visible = true;
            textBox1.Focus();
            textBox1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new Form2();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }



}



