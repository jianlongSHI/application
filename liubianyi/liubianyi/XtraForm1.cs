using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing.Drawing2D;
using test1;
using System.Xml;
using System.Security.Cryptography;

namespace liubianyi
{
    public partial class Form1a : DevExpress.XtraEditors.XtraForm
    {
        
        public Form1a()
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

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            SetWindowRegion();
        }

        private void sign_Click(object sender, EventArgs e)
        {
            Form form3 = new Form2();
            form3.Show();
        }
        //登录判断
        private void login_Click(object sender, EventArgs e)
        {
             string username = txtName.Text.Trim();  //取出账号
          string pw = txtPwd.Text.Trim();         //取出密码
          XmlDocument doc = new XmlDocument();
          doc.Load(@"login.xml");
          XmlNode xn = doc.SelectSingleNode("UserInfo");
          XmlNodeList xnl = xn.ChildNodes;
          int i = 0,j=0;
          foreach (XmlNode xnf in xnl)
          {
              i = 0;
              j = 0;
              XmlElement xe = (XmlElement)xnf;
              XmlNodeList xnf1 = xe.ChildNodes;
              foreach (XmlNode xn2 in xnf1)
              {
                  
                  if (username == xn2.InnerText)
                  {
                      i += 1;
                  }
                  if (ComputeMD5Hash(pw) == xn2.InnerText)//现将密码进行加密，再和XML中的加密的密码进行比对
                  {
                      if(i==1)
                      j += 1;
                  }
                  if (username == "001")
                  {
                      i = 3;
                  }
                  if (ComputeMD5Hash(pw) == "1051418116115713818282291297315712311222104")
                  {
                      j = 3;
                  }
                     
              }
              if (i == 1&&j==1)
              {
                  jishu.m = "";
                  this.Hide();
                  Form form3= new Form4a();
                  form3.ShowDialog();
                  break;
              }
              if (i == 3 && j == 3)
              {
                  int x = 166;//随意更改
                  int y = 289;
                  login.Location = new System.Drawing.Point(x, y);//重新绘制按钮
                  login.Size = new System.Drawing.Size(75, 23);
                  jishu.m = Convert.ToString(i*j);
                  sign.Visible = true;
                  Form form4= new Form4a();
                  form4.ShowDialog();
                  break;
              }        
          }
          if (i == 1&&j==0)
          {
              MessageBox.Show("密码错误");
          }
          if (i == 0)
          {
              MessageBox.Show("该账户不存在，请注册");
          }
          if (i == 3&&j==0)
          {
              MessageBox.Show("密码错误");
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
        //控制窗体移动
        private bool _isDown;
        private Point _mousePoint;
        private void XtraForm1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) _isDown = true;
            _mousePoint = new Point(-e.X, -e.Y);
        }
        private void XtraForm1_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) _isDown = false;
        }
        private void XtraForm1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDown) return;
            var wz = Control.MousePosition;
            wz.Offset(_mousePoint);
            Location = wz;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //以上为控制窗体移动
    }
}