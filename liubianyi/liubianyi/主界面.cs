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
using 自动方式压力平衡条件设定;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Configuration;     
namespace liubianyi
{
    public partial class Form4a : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public const int N = 1;
        public Form4a()
        {
            InitializeComponent();
        }
        BarCheckItem[] FirstBarCheckItem = new BarCheckItem[2];
        BarCheckItem[] SecondBarCheckItem = new BarCheckItem[2];
        string xml_FilePath = "F:\\C#\\liubianyi2\\liubianyi\\liubianyi\\bin\\Debug\\login.xml";//用来记录当前打开文件的路径的
        private SerialPort comm = new SerialPort();//串口通信类
        private StringBuilder builder = new StringBuilder();
        string[] temperature = new string[9] { "02", "61", "30", "30", "30", "30", "03", "32", "39" };//温度发送协议
        string[] pressure = new string[9] { "02", "64", "30", "30", "30", "30", "03", "32", "39" };//压力发送协议
        Int32[] row5 = new Int32[9];//发送协议
        string[] row3 = new string[9];//接收协议
        bool p = true;//是否开始采集数据
        //创建Socket 实例
        Socket[] socket = new Socket[4];
        string[] ports = SerialPort.GetPortNames();
        private void Form4_Load(object sender, EventArgs e)
        {
            label18.Text = "当前用户：" + Username.User;
            //ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False; //隐藏右上角箭头图标
            //ribbonPageGroup1.ShowCaptionButton = false;//隐藏组标题
            if (jishu.m == "9")
            {
                Administrator.Visible = true;
            }
            else
            {
                Administrator.Visible = false;
            }
            jishu.m = "0";//清除管理员密码的记录
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
            //comm.DataReceived += comm_DataReceived;
        }
        #region TCP通信
        private void timer1_Tick(object sender, EventArgs e)//每1000毫秒执行一次
        {
            #region 控制LED的颜色
            if (ribbon.SelectedPage == ribbonPage1)
            {
                led1.Visible = true;
                led2.Visible = true;
                led3.Visible = true;
                led4.Visible = true;
            }
            else
            {
                led1.Visible = false;
                led2.Visible = false;
                led3.Visible = false;
                led4.Visible = false;
            }
            #endregion
            this.txGet1.Clear();
            this.txGet2.Clear();
            this.txGet3.Clear();
            this.txGet4.Clear();
            this.txGet5.Clear();
            //得到ip
            //192.168.235.1
            //127.0.0.1
            string serverIP = ConfigurationManager.AppSettings["ServerIP"];
            IPAddress ip = IPAddress.Parse(serverIP);
            //端口
            int[] port = new int[4];
            port[0] = Int32.Parse(ConfigurationManager.AppSettings["port0"]);
            port[1] = Int32.Parse(ConfigurationManager.AppSettings["port1"]);
            port[2] = Int32.Parse(ConfigurationManager.AppSettings["port2"]);
            port[3] = Int32.Parse(ConfigurationManager.AppSettings["port3"]);
            IPEndPoint[] hostEP = new IPEndPoint[4];
            for (int i = 0; i <= N; i++)
            {
                //组合出远程终结点
                hostEP[i] = new IPEndPoint(ip, port[i]);
                socket[i] = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //尝试连接
                try
                {
                    socket[i].Connect(hostEP[i]);
                    switch (i)
                    {
                        case 0: led1.BackColor = Color.Green; break;
                        case 1: led2.BackColor = Color.Green; break;
                        case 2: led3.BackColor = Color.Green; break;
                        case 3: led4.BackColor = Color.Green; break;
                    }
                    ClientSend(i);
                    ClientReceive(i);
                    socket[i].Close();
                }
                catch
                {
                    switch (i)
                    {
                        case 0: led1.BackColor = Color.Red; break;
                        case 1: led2.BackColor = Color.Red; break;
                        case 2: led3.BackColor = Color.Red; break;
                        case 3: led4.BackColor = Color.Red; break;
                    }
                    timer1.Enabled = false;
                    MessageBox.Show("无法连接到服务器端口" + hostEP[i].ToString() + ",请打开服务器，并重启改进程");
                    break;
                }
            }
        }
#endregion
#region 客户端发送数据
        public void ClientSend(int i)
        {
            //发送给远程主机的请求内容串
            string[] sendStr = new string[4];
            sendStr[0] = "02 64 30 30 30 30 03 32 39";
            sendStr[1] = "02 64 30 30 30 30 03 32 39";
            sendStr[2] = "02 64 30 30 30 30 03 32 39";
            sendStr[3] = "02 64 30 30 30 30 03 32 39";
            //创建bytes字节数组以转换发送串
            byte[] bytesSendStr;
                bytesSendStr = new byte[1024];
                //将发送内容字符串转换成字节byte数组
                bytesSendStr = Encoding.ASCII.GetBytes(sendStr[i]);
                socket[i].Send(bytesSendStr, bytesSendStr.Length, 0);
                bytesSendStr = null;
        }
#endregion
#region 客户端接收数据
        public void ClientReceive(int j)
        {
            byte[] dataBuffer0;//设置接收数据的长度
            int[] count = new int[4];
            string[] msgReceive = new string[4];
            dataBuffer0 = new byte[1024];
            count[j] = socket[j].Receive(dataBuffer0);//获取到dataBuffer的长度
            msgReceive[j] = System.Text.Encoding.UTF8.GetString(dataBuffer0, 0, count[j]);
            dataBuffer0 = null;
            if (msgReceive[j] != "")
            {
                row3 = msgReceive[j].ToString().Split();
                string x = "";
                for (int i = 0; i <= 9 - 1; i++)
                {
                    row5[i] = Convert.ToInt32(temperature[i], 16);
                }
                string[] row6 = new string[4];//计算发送和接收的数据之差
                string[] row7 = new string[4];//将计算的差值转为16进制保存
                for (int i = 2; i <= 5; i++)
                {
                    if (Convert.ToInt32(row3[1]) >= 61 && Convert.ToInt32(row3[1]) <= 63)
                        row6[i - 2] = (Convert.ToInt32(row3[i], 16) - Convert.ToInt32(temperature[i], 16)).ToString("X1");
                    else if (Convert.ToInt32(row3[1]) >= 64 && Convert.ToInt32(row3[1]) <= 65)
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
                switch (row3[1])
                {
                    case "61":
                        this.txGet1.AppendText(m);
                        break;
                    case "62":
                        this.txGet2.AppendText(m);
                        break;
                    case "63":
                        this.txGet3.AppendText(m);
                        break;
                    case "64":
                        this.txGet4.AppendText(m);
                        break;
                    case "65":
                        this.txGet5.AppendText(m);
                        break;
                    default:
                        timer1.Enabled = false;
                        MessageBox.Show("返回的功能码有误，请检查设备，重新开始采集数据");
                        break;
                }
            }
            else
            {
                //MessageBox.Show("接收的数据为空");
            }
        }
#endregion
#region 求某个十进制数的十六进制补码
        public void chage(int x)
        {

            char[] a = new char[8]; ;                                  //程序功能：将十进制整数转换为补码输出
            if (x < 0)
            {
                x = 256 + x;                                             //x为输入的整数
                for (int i = 0; i < 8; i++)
                {
                    a[i] = 'f';
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    a[i] = '0';
                }
            }
            int n = 0;                                                //n为每次x%2取得的余数 
            int j = 7;                                                 //i为整型数组长度减一                           
            char[] b = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            while (x > 0.5)
            {
                n = x % 16;
                x = (x - n) / 16;
                a[j] = b[n];
                j = j - 1;
            }
            string a16="";
            for (int i = 0; i < a.Length; i++)
            {
                a16 = a16 + "" + a[i];
            }
            MessageBox.Show(a16);
        }
#endregion
        //#region 串口接收数据
        //void comm_DataReceived(object sender, EventArgs e)
        //{
        //    int n = comm.BytesToRead;//获取缓存区字节数
        //    byte[] buf = new byte[n];//存储串口数据用
        //    comm.Read(buf, 0, n);//读取缓冲数据
        //    builder.Clear();//清除字符串构造器的内容

        //    //因为要访问ui资源，所以需要使用invoke方式同步ui。
        //    this.Invoke((EventHandler)(delegate
        //    {

        //        //直接按ASCII规则转换成字符串
        //        builder.Append(Encoding.Default.GetString(buf));
        //        string[] row1 = builder.ToString().Trim().Split('H');
        //        Int32[] row2 = new Int32[row1.Length - 1];
        //        for (int i = 0; i < row1.Length - 1; i++)
        //        {
        //            row2[i] = Convert.ToInt32(row1[i]);
        //            row3[i] = Convert.ToString(row2[i]);
        //        }
        //    }));
        //}
        //#endregion
        //结束采集
#region CRC校验
         public byte[] CRC16_C(byte[] data)
        {
            byte CRC16Lo;
            byte CRC16Hi; //CRC寄存器
            byte CL; byte CH; //多项式码&HA001
            byte SaveHi; byte SaveLo;
            byte[] tmpData;
            int Flag;
            CRC16Lo = 0xFF;
            CRC16Hi = 0xFF;
            CL = 0x01;
            CH = 0xA0;
            tmpData = data;
            for (int i = 0; i < tmpData.Length; i++)
            {
                CRC16Lo = (byte)(CRC16Lo ^ tmpData[i]); //每一个数据与CRC寄存器进行异或
                for (Flag = 0; Flag <= 7; Flag++)
                {
                    SaveHi = CRC16Hi;
                    SaveLo = CRC16Lo;
                    CRC16Hi = (byte)(CRC16Hi >> 1); //高位右移一位
                    CRC16Lo = (byte)(CRC16Lo >> 1); //低位右移一位
                    if ((SaveHi & 0x01) == 0x01) //如果高位字节最后一位为1
                    {
                        CRC16Lo = (byte)(CRC16Lo | 0x80); //则低位字节右移后前面补1
                    } //否则自动补0
                    if ((SaveLo & 0x01) == 0x01) //如果LSB为1，则与多项式码进行异或
                    {
                        CRC16Hi = (byte)(CRC16Hi ^ CH);
                        CRC16Lo = (byte)(CRC16Lo ^ CL);
                    }
                }
            }
            byte[] ReturnData = new byte[2];
            ReturnData[0] = CRC16Hi; //CRC高位
            ReturnData[1] = CRC16Lo; //CRC低位
            return ReturnData;
        }
        #endregion
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            userreport.Visible = false;
            timer1.Enabled = false; //设置为truetimer1_Tick实践就会执行，开始计时
        }
        //采集数据
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            userreport.Visible = false;
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
 #region MD5加密
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
            //串口开关按钮{
            //根据当前串口对象，来判断操作
            if (comm.IsOpen)
            {
                //打开时点击，则关闭串口
                comm.Close();
            }
            else
            {
                if (Convert.ToString(barEditItem3.EditValue) == "")
                {

                    MessageBox.Show("请插入串口设备！");
                    return;
                }
                ////关闭时点击，则设置好端口，波特率后打开
                comm.PortName = barEditItem3.EditValue.ToString();
                comm.BaudRate = int.Parse(barEditItem4.EditValue.ToString());
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

        private void barToggleSwitchItem8_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (barToggleSwitchItem8.Checked == false)
            {
                jishu.rowsum = 0;
                barToggleSwitchItem8.Caption = "手动方式";
                timer2.Enabled = false;
                textBox1.Text = "手动";
                textBox3.Text = "手动";
            }
            else
            {
                barToggleSwitchItem8.Caption = "自动方式";
                timer2.Enabled = true;
                #region 刷新工作方式的数据
                textBox1.Text = jishu.rowsum.ToString();
                #endregion
            }
        }

        private void barToggleSwitchItem9_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (barToggleSwitchItem9.Checked == false)
            {
                barToggleSwitchItem9.Caption = "暂停";
            }
            else
            {
                barToggleSwitchItem9.Caption = "继续";
            }
        }


        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (barToggleSwitchItem8.Checked == true)
            {
                Form formauto = new 自动方式参数设置();
                formauto.ShowDialog();
            }
        }

        private void barToggleSwitchItem10_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if(barToggleSwitchItem10.Checked==false)
            {
                barToggleSwitchItem10.Caption="保存数据";
            }
            else
            {
                barToggleSwitchItem10.Caption="不存数据";
            }
        }

        private void barToggleSwitchItem11_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (barToggleSwitchItem11.Checked == false)
            {
                barToggleSwitchItem11.Caption = "检测柱塞位";
            }
            else
            {
                barToggleSwitchItem11.Caption = "不检柱塞位";

            }
        }
        #endregion

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form Formsetting = new Formsetting();
            Formsetting.ShowDialog();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            #region 刷新工作方式的数据
            textBox1.Text = jishu.rowsum.ToString();
            #endregion
        }
    } 
}