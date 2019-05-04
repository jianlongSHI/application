using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography;
using System.Drawing.Drawing2D;

namespace test2
{
    public partial class Form2a : Form
    {
        public Form2a()
        {
            InitializeComponent();
        }
        #region 窗体圆角的实现

        public void SetWindowRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, 50);
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
        #endregion

        private void XtraForm1_Resize(object sender, EventArgs e)
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
        private void Form2_Load(object sender, EventArgs e)
        {
            SetWindowRegion();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            string username = txtName.Text.Trim();  //取出账号
            string pw = txtPwd.Text.Trim();         //取出密码
            XmlDocument doc = new XmlDocument();
            doc.Load(@"login.xml");//加载xml文档
            XmlNode xn = doc.SelectSingleNode("UserInfo");
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode xnf in xnl)
            {
                XmlElement xe = (XmlElement)xnf;
                XmlNodeList xnf1 = xe.ChildNodes;
                foreach (XmlNode xn2 in xnf1)
                {
                    //Console.WriteLine(xn2.InnerText);//显示子节点点文本
                    if (username == xn2.InnerText)
                    {
                        if (username == "")
                        {
                            MessageBox.Show("用户名不能为空,请重新注册");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("改用户名已经使用,请重新注册");
                            return;
                        }
                    }
                    if (username == "")
                    {
                         MessageBox.Show("用户名不能为空,请重新注册");
                            return;
                    }
                }
            }
            XmlNode root = doc.SelectSingleNode("UserInfo");//获取xml文档里面的第一个节点
            XmlElement xelKey = doc.CreateElement("user");//创建节点
            //创建子节点
            XmlElement xelAuthor = doc.CreateElement("username");//创建节点
            xelAuthor.InnerText = username;//将用户名赋值给username节点
            xelKey.AppendChild(xelAuthor);//将username节点挂载到user节点上
            XmlElement xelAuthor2 = doc.CreateElement("password");
            xelAuthor2.InnerText = ComputeMD5Hash(pw);//将密码加密后赋值给password节点
            xelKey.AppendChild(xelAuthor2);//将password节点挂载到user节点上
            root.AppendChild(xelKey);
            doc.Save(@"login.xml");//保存xml文件
            if (xelAuthor.InnerText != null)
            {
                MessageBox.Show("注册成功");
                this.Hide();
            }
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
    }
}
