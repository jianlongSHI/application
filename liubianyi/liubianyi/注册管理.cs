using System;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using test1;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace 流变仪
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
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
        private void Form4_Load(object sender, EventArgs e)
        {
            SetWindowRegion();
        }
        //public class Udata
        //{
        //    public Udata()
        //    { }
        //    private string username;

        //    public string Username
        //    {
        //        get { return username; }
        //        set { username = value; }
        //    }

        //    private string password;

        //    public string Password
        //    {
        //        get { return password; }
        //        set { password = value; }
        //    }

        //}
        //List<users> cls = new List<users>();
        //class users
        //{
        //    public string username;
        //    public string passward;
        //    public users(string[] row1)
        //    {
        //        this.username = row1[0];
        //        this.passward = row1[1];
        //    }

        //}
        //private void button1_Click_1(object sender, EventArgs e)
        //{
        //    //打开某文件(假设web.config在根目录中) 
        //    XmlDocument doc = new XmlDocument();
        //    XmlReaderSettings settings = new XmlReaderSettings();
        //    settings.IgnoreComments = true;//忽略文档里面的注释
        //    doc.Load(@"login.xml");//加载xml文档
        //    //得到顶层节点列表 
        //    XmlNode xn = doc.SelectSingleNode("UserInfo");
        //    XmlNodeList xnl = xn.ChildNodes;
        //    foreach (XmlNode xn1 in xnl)
        //    {
        //        Udata udata = new Udata();
        //        // 将节点转换为元素，便于得到节点的属性值
        //        XmlElement xe = (XmlElement)xn1;
        //        // 得到Type和ISBN两个属性的属性值 <book Type="必修课" ISBN="7-111-19149-5">
        //        //udata.Username = xe.GetAttribute("ISBN").ToString();
        //        //udata.Password = xe.GetAttribute("Type").ToString();
        //        // 得到Book节点的所有子节点
        //        XmlNodeList xnl0 = xe.ChildNodes;
        //        udata.Username = xnl0.Item(0).InnerText;
        //        udata.Password = xnl0.Item(1).InnerText;
        //        string[] row = new string[2];
        //        row[0] = udata.Username;
        //        row[1] = udata.Password;
        //        cls.Add(new users(row));
        //        this.dataGridView1.Rows.Add(row);
        //    }
        //}

        string xml_FilePath = @"login.xml";

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
        private void button2_Click_1(object sender, EventArgs e)
        {
            //OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();//一个打开文件的对话框
            //openFileDialog1.Filter = "xml文件(*.xml)|*.xml";//设置允许打开的扩展名
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了文件  
            //{
            //    xml_FilePath = openFileDialog1.FileName;//记录用户选择的文件路径
            XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”
            xmlDocument.Load(@"login.xml");//载入路径这个xml
            try
            {
                XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("UserInfo").ChildNodes;//选择class为根结点并得到旗下所有子节点
                dataGridView1.Rows.Clear();//清空dataGridView1，防止和上次处理的数据混乱
                foreach (XmlNode xmlNode in xmlNodeList)//遍历class的所有节点
                {
                    XmlElement xmlElement = (XmlElement)xmlNode;//对于任何一个元素，其实就是每一个<student>
                    //旗下的子节点<name>和<number>分别放入dataGridView1
                    int index = dataGridView1.Rows.Add();//在dataGridView1新加一行，并拿到改行的行标
                    dataGridView1.Rows[index].Cells[0].Value = xmlElement.ChildNodes.Item(0).InnerText;//各个单元格分别添加
                    dataGridView1.Rows[index].Cells[1].Value = xmlElement.ChildNodes.Item(1).InnerText;
                }
            }
            catch
            {
                MessageBox.Show("XML格式不对！");
            }
            //}
            //else
            //{
            //    MessageBox.Show("请打开XML文件");
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”
            if (xml_FilePath != "../bin/debug/login.xml")//如果用户已读入xml文件，我们的任务就是修改这个xml文件了
            {
                xmlDocument.Load(xml_FilePath);
                XmlNode xmlElement_class = xmlDocument.SelectSingleNode("UserInfo");//找到<class>作为根节点
                xmlElement_class.RemoveAll();//删除旗下所有节点
                int row = dataGridView1.Rows.Count;//得到总行数    
                int cell = dataGridView1.Rows[1].Cells.Count;//得到总列数    
                for (int i = 0; i < row; i++)//遍历这个dataGridView
                {
                    XmlElement xmlElement_student = xmlDocument.CreateElement("user");//创建一个<student>节点
                    XmlElement xmlElement_name = xmlDocument.CreateElement("username");//创建<name>节点
                    xmlElement_name.InnerText = dataGridView1.Rows[i].Cells[0].Value.ToString();//其文本就是第0个单元格的内容
                    xmlElement_student.AppendChild(xmlElement_name);//在<student>下面添加一个新的节点<name>
                    //同理添加<number>
                    XmlElement xmlElement_number = xmlDocument.CreateElement("password");
                    xmlElement_number.InnerText = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    xmlElement_student.AppendChild(xmlElement_number);
                    xmlElement_class.AppendChild(xmlElement_student);//将这个<student>节点放到<class>下方
                }
                xmlDocument.Save("../debug/login.xml");//保存这个xml
                MessageBox.Show("保存成功");
            }

            else
            {
                MessageBox.Show("请保存为XML文件");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.SelectedRows.Count;
            if (row == 0)
            {
                MessageBox.Show("没有选中任何行", "Error");
                return;
            }
            else if (MessageBox.Show("确认删除选中的" + row.ToString() + "条记录吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int selectcount = dataGridView1.SelectedRows.Count;
                while (selectcount > 0)
                {
                    if (!dataGridView1.SelectedRows[0].IsNewRow)
                        dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                    selectcount--;
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form form = new Form2();
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}


