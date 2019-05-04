using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.IO.Ports;
using System.Xml;
using System.Security.Cryptography;

namespace liubianyi
{
    public partial class Form4a : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form4a()
        {
            InitializeComponent();
        }
        BarCheckItem[] FirstBarCheckItem = new BarCheckItem[2];
        BarCheckItem[] SecondBarCheckItem = new BarCheckItem[2];
        string Com = "a";
        string BaudRate = "1";
        string xml_FilePath = "C:\\Users\\汪洋\\Desktop\\liubianyi\\liubianyi\\bin\\Debug\\login.xml";//用来记录当前打开文件的路径的
        private SerialPort comm = new SerialPort();//串口通信类
        private StringBuilder builder = new StringBuilder();
        string[] temperature = new string[9] { "02", "61", "30", "30", "30", "30", "03", "32", "39" };//温度发送协议
        string[] pressure = new string[9] { "02", "64", "30", "30", "30", "30", "03", "32", "39" };//压力发送协议
        Int32[] row5 = new Int32[9];//发送协议
        string[] row3 = new string[9];//接收协议
        bool p = true;//是否开始采集数据
        string[] ports = SerialPort.GetPortNames();
       
        private void Form4_Load(object sender, EventArgs e)
        {
            

            //ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False; //隐藏右上角箭头图标
            //ribbonPageGroup1.ShowCaptionButton = false;//隐藏组标题
            if (jishu.m == "9")
            {
                Administrator.Visible = true;
            }
            userreport.Visible = false;
            timer1.Interval = 1000;//设置timer1的timer1_Tick实践执行周期为1000毫秒
            //初始化下拉串口名称列表框
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);//排序
            //comboPortName.it
            // comboPortName5.
            repositoryItemComboBox3.Items.AddRange(ports);
            repositoryItemComboBox4.Items.Add("9600");
            repositoryItemComboBox4.Items.Add("19200");

            comm.DataReceived += comm_DataReceived;
            //
            FirstBarCheckItem[0] = barCheckItem6;
            FirstBarCheckItem[1] = barCheckItem7;
            SecondBarCheckItem[0] = barCheckItem8;
            SecondBarCheckItem[1] = barCheckItem9;
            for (int i = 0; i < FirstBarCheckItem.Length; i++)
            {
                FirstBarCheckItem[i].Caption = ports[i];
            }
        }
        //打开串口
        #region
        void comm_DataReceived(object sender, EventArgs e)
        {
            int n = comm.BytesToRead;//获取缓存区字节数
            byte[] buf = new byte[n];//存储串口数据用
            comm.Read(buf, 0, n);//读取缓冲数据
            builder.Clear();//清除字符串构造器的内容

            //因为要访问ui资源，所以需要使用invoke方式同步ui。
            this.Invoke((EventHandler)(delegate
            {
              
                      //直接按ASCII规则转换成字符串
                      builder.Append(Encoding.Default.GetString(buf));
                      string[] row1 = builder.ToString().Trim().Split('H');
                    Int32[] row2 = new Int32[row1.Length-1];
                    //string[] row3 = new string[row1.Length - 1];
                   
                    for (int i = 0; i < row1.Length-1; i++)
                    {
                        row2[i]= Convert.ToInt32(row1[i]);
                        row3[i] = Convert.ToString(row2[i]);
                    }
            }));
        }
        //结束采集
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            userreport.Visible = false;
            timer1.Enabled = false; //设置为truetimer1_Tick实践就会执行，开始计时
        }
        //采集数据
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            userreport.Visible = false;
            timer1.Enabled = true;//设置为truetimer1_Tick实践就会执行，开始计时
             
            
        }
        private void timer1_Tick(object sender, EventArgs e)//每1000毫秒执行一次
        {
            if (row3[0] == null)
            {
                err.Text = "采集超时请打开串口";
                ribbon.SelectedPage = ribbonPage1;
            }
            else
                err.Text = "";
            this.txGet1.Clear();
            this.txGet2.Clear();
            this.txGet3.Clear();
            this.txGet4.Clear();
            this.txGet5.Clear();
            string x = "";
            for (int i = 0; i <= 9 - 1; i++)
            {
                row5[i] = Convert.ToInt32(temperature[i], 16);
            }
            string[] row6 = new string[4];//计算发送和接收的数据之差
            string[] row7 = new string[4];//将计算的差值转为16进制保存
            for (int i = 2; i <= 5; i++)
            {
                if(Convert.ToInt32(row3[1])>=61&&Convert.ToInt32(row3[1])<=63)
                   row6[i-2] = (Convert.ToInt32(row3[i], 16) - Convert.ToInt32(temperature[i], 16)).ToString("X1");
                else if(Convert.ToInt32(row3[1])>=64&&Convert.ToInt32(row3[1])<=65)
                   row6[i - 2] = (Convert.ToInt32(row3[i], 16) - Convert.ToInt32(pressure[i], 16)).ToString("X1");
            }
            for (int i = 0; i < row6.Length; i++)
            {
                if (Convert.ToInt32(row6[i]) >= 10)
                    row7[i] = (Convert.ToInt32(row6[i]) - 1).ToString("X1");
                else
                    row7[i] = Convert.ToInt32(row6[i]).ToString("X1");
            }
            for (int i = 0; i < row7.Length; i++)
            {

                x += row7[i];
            }
            int wendu1 = Convert.ToInt32(x, 16);
            string wendu2 = Convert.ToString(wendu1);
            int l = wendu2.Length;
            string m = "";
            for (int i = 0; i < l; i++)
            {
                if (l - i == 2)
                    m += ".";
                m += wendu2[i];
            }
            if (row3[1] == "62")
                this.txGet2.AppendText(m);
            else if (row3[1] == "63")
                this.txGet3.AppendText(m);
            else if (row3[1] == "61")
                this.txGet1.AppendText(m);
            else if(row3[1] == "64")
                this.txGet4.AppendText(m);
            else if (row3[1] == "65")
                this.txGet5.AppendText(m);
        }
        //查看用户
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            userreport.Visible = true;
            #region 删除空白节点
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xml_FilePath);//载入路径这个xml
            //获取节点的所有子节点
            XmlNode root = xmlDoc.SelectSingleNode("UserInfo");
            XmlNodeList xnList = root.ChildNodes;
            foreach (XmlNode xn in xnList)
            {
                if (xn.SelectSingleNode("username").InnerText == "")
                {
                    root.RemoveChild(xn);// 移除指定的子节点
                }
            }
            xmlDoc.Save(@"login.xml");
            #endregion
            try
            {
                XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”
                xmlDocument.Load(xml_FilePath);//载入路径这个xml
                XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("UserInfo").ChildNodes;//选择class为根结点并得到旗下所有子节点
                userreport.Rows.Clear();//清空dataGridView1，防止和上次处理的数据混乱
                foreach (XmlNode xmlNode in xmlNodeList)//遍历class的所有节点
                {
                    XmlElement xmlElement = (XmlElement)xmlNode;//对于任何一个元素，其实就是每一个<student>
                    //旗下的子节点<name>和<number>分别放入dataGridView3
                    int index = userreport.Rows.Add();//在dataGridView1新加一行，并拿到改行的行标
                    userreport.Rows[index].Cells[0].Value = xmlElement.ChildNodes.Item(0).InnerText;//各个单元格分别添加
                    userreport.Rows[index].Cells[1].Value = xmlElement.ChildNodes.Item(1).InnerText;
                }
            }
            catch
            {
                MessageBox.Show("XML格式不对！");
            }
            userreport.Visible = true;
        }
        //修改密码
        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            userreport.Visible = true;
            XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”
            if (xml_FilePath != "")//如果用户已读入xml文件，我们的任务就是修改这个xml文件了
            {

                //更改密码
                xmlDocument.Load(xml_FilePath);
                XmlNode xmlElement_class = xmlDocument.SelectSingleNode("UserInfo");//找到<class>作为根节点
                xmlElement_class.RemoveAll();//删除旗下所有节点
                int row = userreport.Rows.Count;//得到总行数    
                int cell = userreport.Rows[1].Cells.Count;//得到总列数    
                for (int i = 0; i < row - 1; i++)//遍历这个dataGridView
                {
                    XmlElement xmlElement_student = xmlDocument.CreateElement("user");//创建一个<student>节点
                    XmlElement xmlElement_name = xmlDocument.CreateElement("username");//创建<name>节点
                    if (userreport.Rows[i].Cells[0].Value == null)
                    {
                        userreport.Rows[i].Cells[0].Value = "  ";
                        xmlElement_name.InnerText = userreport.Rows[i].Cells[0].Value.ToString();
                    }
                    else
                    {
                        xmlElement_name.InnerText = userreport.Rows[i].Cells[0].Value.ToString();
                    }
                    //其文本就是第0个单元格的内容
                    xmlElement_student.AppendChild(xmlElement_name);//在<student>下面添加一个新的节点<name>
                    //同理添加<number>
                    XmlElement xmlElement_number = xmlDocument.CreateElement("password");
                    if (userreport.Rows[i].Cells[1].Value == null)
                    {
                        userreport.Rows[i].Cells[1].Value = "  ";
                        xmlElement_name.InnerText = userreport.Rows[i].Cells[1].Value.ToString();
                    }
                    xmlElement_number.InnerText = ComputeMD5Hash(userreport.Rows[i].Cells[1].Value.ToString());
                    xmlElement_student.AppendChild(xmlElement_number);
                    xmlElement_class.AppendChild(xmlElement_student);//将这个<student>节点放到<class>下方
                }
                // 移除指定的子节点
                xmlDocument.Save(xml_FilePath);//保存这个xml
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


#endregion
        private void buttonopen2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Com = barEditItem3.EditValue.ToString();
            BaudRate = barEditItem4.EditValue.ToString();


            //串口开关按钮
            //根据当前串口对象，来判断操作
            if (comm.IsOpen)
            {
                //打开时点击，则关闭串口
                comm.Close();
            }
            else
            {
                if (repositoryItemComboBox3.Items.Count == 0)
                {

                    MessageBox.Show("请插入串口设备！");
                    return;
                    
                }
                //关闭时点击，则设置好端口，波特率后打开
                comm.PortName = Com;
                comm.BaudRate =  Convert.ToInt32 (BaudRate);
                try
                {
                    comm.Open();

                }
                catch (Exception ex)
                {
                    //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
                    // comm = new SerialPort();
                    //现实异常信息给客户。
                    MessageBox.Show(ex.Message);
                }
            }
            //设置按钮的状态
            buttonopen2.Caption = comm.IsOpen ? "关闭串口" : "打开串口";
            // buttonSend.Enabled = comm.IsOpen ? true:false;
            //labelopenflag.ForeColor = comm.IsOpen ? Color.Red : Color.Black;
        }
        #region
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }//退出

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)//隐藏用户
        {
            userreport.Visible = false;
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barToggleSwitchItem1_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            buttonopen2.Caption = comm.IsOpen ? "关闭串口" : "打开串口";
        }//切换用户
        #endregion
    }
}