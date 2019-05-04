using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using System.Security.Cryptography;
namespace liubianyi
{
   
    public partial class Serial : Form
    {
        public  Serial()
        {
            InitializeComponent();
        }

        string xml_FilePath = "F:\\C#\\liubianyi\\liubianyi\\bin\\Debug\\login.xml";//用来记录当前打开文件的路径的
        private SerialPort comm = new SerialPort();//串口通信类
        private StringBuilder builder = new StringBuilder();
        private long received_count = 0;//接收计数
        private long send_count = 0;//发送计数
        public List<Materials> cls = new List<Materials>();
        public List<Data> da = new List<Data>();
        public int i = 0;//记录图表中数据的条数
        int q = 0;

        //private string resultFile;
        private void Form1_Load(object sender, EventArgs e)
        {

            if (jishu.m == "9")
            {
                Administrator.Visible = true;
            }
            //初始化下拉串口名称列表框
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);//排序
            //comboPortName.it
           // comboPortName5.
            comboPortName1.Items.AddRange(ports);
            comboPortName1.SelectedIndex = comboPortName1.Items.Count > 0 ? 0 : -1;
            comboBaudrate.SelectedIndex = comboBaudrate.Items.IndexOf("9600");
            comboBoxstopb.SelectedIndex = comboBoxstopb.Items.IndexOf("1");
            comboBoxdateb.SelectedIndex = comboBoxdateb.Items.IndexOf("8");
            comboBoxjiou.SelectedIndex = comboBoxjiou.Items.IndexOf("无");
            comm.DataReceived += comm_DataReceived;
            ribbonControl1.ShowToolbarCustomizeItem = false; //清除菜单栏
        }
        //接收数据
        void comm_DataReceived(object sender, EventArgs e)
        {
           int n = comm.BytesToRead;//获取缓存区字节数
            byte[] buf = new byte[n];//存储串口数据用
            received_count += n;//接收计数
            comm.Read(buf, 0, n);//读取缓冲数据
            builder.Clear();//清除字符串构造器的内容

            //因为要访问ui资源，所以需要使用invoke方式同步ui。
            this.Invoke((EventHandler)(delegate
            {
                //直接按ASCII规则转换成字符串
                  builder.Append(Encoding.Default.GetString(buf));
                //追加的形式添加到文本框末端，并滚动到最后。
                //builder.Tostring()为接收过来的字符串
                this.txGet.AppendText(builder.ToString());
                //修改接收计数
                toolStripStatusLabel4.Text = "Recv:" + received_count.ToString();
                //将数据发送到表格中
                string receive = builder.ToString() + ',' + DateTime.Now.ToShortDateString().ToString()+"/"+DateTime.Now.TimeOfDay.ToString(); 
                String[] row1 = new String[5]; 
                String[]  row2 =builder.ToString().Split(',');
                    int j = 0,k=0;
                    row1[j] = row2[k];
                    j++;
                    k++;
                    row1[j] = row2[k];
                    j++;
                    k++;
                    row1[j] = row2[k];
                    j++;
                    row1[j] = Convert.ToString( Convert.ToDouble(row1[1]) * Convert.ToDouble(row1[2]));
                    j++;
                    row1[j] = DateTime.Now.ToShortDateString().ToString() + "/" + DateTime.Now.TimeOfDay.ToString();
                  //创建对象
                    cls.Add(new Materials(row1));
                    q++;
                    cht1.Text = Convert.ToString(q);
                this.dataGridView1.Rows.Add(row1);
            }));
        }
        //发送数据
        private void buttonSend_Click(object sender, EventArgs e)
        {
            int n = 0;
            comm.Write(Encoding.Default.GetBytes(txSend.Text), 0, Encoding.Default.GetBytes(txSend.Text).Length);
            n = txSend.Text.Length;
            send_count += n;//累加发送字节数
            toolStripStatusLabel3.Text = "Send:" + send_count.ToString();
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }
        private void txGet_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txSend_TextChanged(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {
            // 设置显示范围
            ChartArea chartArea = cht1.ChartAreas[0];
            chartArea.CursorX.IsUserEnabled = true;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.CursorX.Interval = 0;
            chartArea.CursorX.IntervalOffset = 0;
            chartArea.CursorX.IntervalType = DateTimeIntervalType.Minutes;
            chartArea.AxisX.ScaleView.Zoomable = true;
            chartArea.AxisX.ScrollBar.IsPositionedInside = false;
            //设置Y轴允许拖动放大
            chartArea.CursorY.IsUserEnabled = true;
            chartArea.CursorY.IsUserSelectionEnabled = true;
            chartArea.CursorY.Interval = 0;
            chartArea.CursorY.IntervalOffset = 0;
            chartArea.CursorY.IntervalType = DateTimeIntervalType.Minutes;
            chartArea.AxisY.ScaleView.Zoomable = true;
            chartArea.AxisY.ScrollBar.IsPositionedInside = false;
        }
        private void buttonReset_Click_1(object sender, EventArgs e)
        {
            txSend.Clear();
            send_count = 0;
            toolStripStatusLabel3.Text = "Send:0";
        }

        private void btncleanrec_Click(object sender, EventArgs e)
        {
            txGet.Clear();
            received_count = 0;
            toolStripStatusLabel4.Text = "Recv:0";
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        //从excel中将数据存入datatable中
        DataTable GetDataFromExcelByCom(bool hasTitle = true)
        {
            string strFileNamePath = string.Empty;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.FileName = "";
            openFile.Title = "请选择要导入的Excel文件";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFile.Filter = "Excel文件(*.xls)|*.xls";
            openFile.CheckFileExists = true;
            openFile.CheckPathExists = true;
            if (openFile.ShowDialog() == DialogResult.OK)
                strFileNamePath = openFile.FileName;
            var excelFilePath = strFileNamePath;
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Sheets sheets;
            object oMissiong = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            DataTable dt = new DataTable();

            try
            {
                if (app == null) return null;
                workbook = app.Workbooks.Open(excelFilePath, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong,
                    oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong);
                sheets = workbook.Worksheets;

                //将数据读入到DataTable中
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(1);//读取第一张表  
                if (worksheet == null) return null;

                int iRowCount = worksheet.UsedRange.Rows.Count;
                int iColCount = worksheet.UsedRange.Columns.Count;
                //生成列头
                for (int i = 0; i < iColCount; i++)
                {
                    var name = "column" + i;
                    if (hasTitle)
                    {
                        var txt = ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1]).Text.ToString();
                        if (!string.IsNullOrWhiteSpace(txt)) name = txt;
                    }
                    while (dt.Columns.Contains(name)) name = name + "_1";//重复行名称会报错。
                    dt.Columns.Add(new DataColumn(name, typeof(string)));
                }
                //生成行数据
                Microsoft.Office.Interop.Excel.Range range;
                int rowIdx = hasTitle ? 2 : 1;
                for (int iRow = rowIdx; iRow <= iRowCount; iRow++)
                {
                    DataRow dr = dt.NewRow();
                    for (int iCol = 1; iCol <= iColCount; iCol++)
                    {
                        range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[iRow, iCol];
                        dr[iCol - 1] = (range.Value2 == null) ? "" : range.Text.ToString();
                    }
                    dt.Rows.Add(dr);
                }
                MessageBox.Show("导入成功");
                return dt;
            }
            catch { return null; }
        }
        //
        //将excal导入到DataGridView并且将数据赋值给对象  
        private void btImport_Click(object sender, EventArgs e)
        {
            //da.Clear();//清空List
            //DataTable dt = GetDataFromExcelByCom(true);
            //dataGridView2.DataSource = dt;
            //for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            //{
            //    da.Add(new Data());
            //    for (int j = 0; j < dataGridView2.Columns.Count; j++)
            //    {
            //        da[i].message[j] = dataGridView2.Rows[i].Cells[j].Value.ToString();
            //    }
            //}
        }
        //绘制机器发送的数据图像
        private void cht1_TextChanged(object sender, EventArgs e)
        {

       
            cht2.Visible = false;
            ////清除默认的series
            cht1.Series.Clear();
            Series a = new Series("动量-时间");
           // Series b = new Series("动量-时间");
            //设置chart的类型，这里为折线图
            a.ChartType = SeriesChartType.Line;
            a.IsValueShownAsLabel = true;
            a.BorderWidth = 6;
            a.Color = System.Drawing.Color.Cyan;
            //b.ChartType = SeriesChartType.Line;
            //b.IsValueShownAsLabel = true;
            //b.BorderWidth = 6;
            //cht1.ChartAreas[0].AxisX.MajorGrid.Interval = 0.5;
            //cht1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            //cht1.ChartAreas[0].Area3DStyle.Enable3D = false;
            //cht1.ChartAreas[0].AxisX.IsMarginVisible = true;
            //cht1.ChartAreas[0].AxisX.Title = "质量";
            //cht1.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Crimson;
            //cht2.ChartAreas[0].AxisX.MajorGrid.Interval = 0.5;
            cht1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            //chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            cht1.ChartAreas[0].AxisX.IsMarginVisible = true;
            cht1.ChartAreas[0].AxisX.Title = "时间";
            cht1.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Crimson;

            cht1.ChartAreas[0].AxisY.Title = "动量";
            cht1.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Crimson;
            cht1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
            a.ChartType = SeriesChartType.Line;
           // b.ChartType = SeriesChartType.Line;
            //给系列上的点进行赋值，分别对应横坐标和纵坐标的值
            for (int i = 0; i < cls.Count; i++)
            {
                a.Points.AddXY(cls[i].date, cls[i].momentum);
               // b.Points.AddXY(Xdate[i], Convert.ToDouble(da[i].message[3]));
                //把series添加到chart上
            }
            cht1.Series.Add(a);
            //cht1.Series.Add(b);
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void cht2_Click(object sender, EventArgs e)
        {
            // 设置显示范围
            ChartArea chartArea = cht2.ChartAreas[0];
            //chartArea.AxisX.Minimum = 0d;
           // chartArea.AxisX.Maximum = 30d;
           // chartArea.AxisY.Minimum = 0d;
            //chartArea.AxisY.Maximum = 100d;
            //设置X轴允许拖动放大
            chartArea.CursorX.IsUserEnabled = true;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.CursorX.Interval = 0;
            chartArea.CursorX.IntervalOffset = 0;
            chartArea.CursorX.IntervalType = DateTimeIntervalType.Minutes;
            chartArea.AxisX.ScaleView.Zoomable = true;
            chartArea.AxisX.ScrollBar.IsPositionedInside = false;
            //设置Y轴允许拖动放大
            chartArea.CursorY.IsUserEnabled = true;
            chartArea.CursorY.IsUserSelectionEnabled = true;
            chartArea.CursorY.Interval = 0;
            chartArea.CursorY.IntervalOffset = 0;
            chartArea.CursorY.IntervalType = DateTimeIntervalType.Minutes;
            chartArea.AxisY.ScaleView.Zoomable = true;
            chartArea.AxisY.ScrollBar.IsPositionedInside = false;
        }
        //串口开关按钮
        private void buttonopen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //根据当前串口对象，来判断操作
            if (comm.IsOpen)
            {
                //打开时点击，则关闭串口
                comm.Close();
            }
            else
            {
                if (comboPortName1.Text == "")
                {

                    MessageBox.Show("请插入串口设备！");
                    return;

                }
                //关闭时点击，则设置好端口，波特率后打开
                comm.PortName = comboPortName1.Text;
                comm.BaudRate = int.Parse(comboBaudrate.Text);
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
            buttonopen.Caption = comm.IsOpen ? "关闭串口" : "打开串口";
            // buttonSend.Enabled = comm.IsOpen ? true:false;
            labelopenflag.ForeColor = comm.IsOpen ? Color.Red : Color.Black;
        }
        //从外部导入数据
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            da.Clear();//清空List
            DataTable dt = GetDataFromExcelByCom(true);
            dataGridView2.DataSource = dt;
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                da.Add(new Data());
                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                {
                    da[i].message[j] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                }
            }
        }
        //绘制导入的数据的图像
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Form2 form = new Form2();
            // dataGridView2.Visible = true;
            cht2.Visible = true;
            ////清除默认的series
            //cht2.Series.Clear();
            Series[] a = new Series[10000];
            //Series b = new Series("质量-动量");
            //设置chart的类型，这里为折线图
            a[i] = new Series("动量-时间" + i);
            a[i].ChartType = SeriesChartType.Line;
            a[i].IsValueShownAsLabel = true;
            a[i].BorderWidth = 6;
            a[i].Color = System.Drawing.Color.Cyan;
            // b.ChartType = SeriesChartType.Line;
            //b.IsValueShownAsLabel = true;
            //b.BorderWidth = 6;
            //cht2.ChartAreas[0].AxisX.MajorGrid.Interval = 0.5;
            //cht2.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            //cht2.ChartAreas[0].Area3DStyle.Enable3D = false;
            //cht2.ChartAreas[0].AxisX.IsMarginVisible = true;
            //cht2.ChartAreas[0].AxisX.Title = "质量";
            //cht2.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Crimson;
                      
            cht2.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
     
            cht2.ChartAreas[0].AxisX.IsMarginVisible = true;
            cht2.ChartAreas[0].AxisX.Title = "时间";
            cht2.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Crimson;

            
            cht2.ChartAreas[0].AxisY.Title = "动量";
            cht2.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Crimson;
            cht2.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;

            a[i].ChartType = SeriesChartType.Line;
            // b.ChartType = SeriesChartType.Line;
            //给系列上的点进行赋值，分别对应横坐标和纵坐标的值
            for (int j = 0; j < da.Count; j++)
            {
                a[i].Points.AddXY(da[j].message[4], Convert.ToDouble(da[j].message[3]));
                //把series添加到chart上
            }
            cht2.Series.Add(a[i]);
            i++;
            //form.Show();
        }
        //将数据保存到excel中
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Export Excel File";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == "")
                return;
            Stream myStream;
            myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));

            string str = "";
            try
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (i > 0)
                    {
                        str += "\t";
                    }
                    str += dataGridView1.Columns[i].HeaderText;
                }
                sw.WriteLine(str);
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    string tempStr = "";
                    for (int k = 0; k < dataGridView1.Columns.Count; k++)
                    {
                        if (k > 0)
                        {
                            tempStr += "\t";
                        }
                        tempStr += dataGridView1.Rows[j].Cells[k].Value.ToString();
                    }
                    sw.WriteLine(tempStr);
                }
                sw.Close();
                myStream.Close();
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
            #endregion
        }
        //将数据保存到txt中
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "TXT files (*.txt)|*.txt";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Export TXT File";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == "")
                return;
            string FullFileName = @saveFileDialog.FileName;
            //FileStream fs=new FileStream(FullFileName,FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(FullFileName, true, Encoding.Default);
            string str = "";
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                if (i > 0)
                {
                    str += "\t";
                }
                str += dataGridView1.Columns[i].HeaderText;
            }
            sw.WriteLine(str);
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    str = dataGridView1.Rows[i].Cells[j].Value.ToString().Trim();
                    if (str.Length < 10)
                        str = str.PadRight(10, ' '); //不够长度的，补齐空格！
                    sw.Write(str);
                }
                sw.WriteLine("");
            }
            sw.Close();
        }
        //隐藏chart
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            cht2.Visible = false;
        }
        //清除图表信息
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            cht2.Series.Clear();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
                
                dataGridView3.Visible = true;
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
                    dataGridView3.Rows.Clear();//清空dataGridView1，防止和上次处理的数据混乱
                    foreach (XmlNode xmlNode in xmlNodeList)//遍历class的所有节点
                    {
                        XmlElement xmlElement = (XmlElement)xmlNode;//对于任何一个元素，其实就是每一个<student>
                        //旗下的子节点<name>和<number>分别放入dataGridView3
                        int index = dataGridView3.Rows.Add();//在dataGridView1新加一行，并拿到改行的行标
                        dataGridView3.Rows[index].Cells[0].Value = xmlElement.ChildNodes.Item(0).InnerText;//各个单元格分别添加
                        dataGridView3.Rows[index].Cells[1].Value = xmlElement.ChildNodes.Item(1).InnerText;
                    }
                }
                catch
                {
                    MessageBox.Show("XML格式不对！");
                }
        }
      //修改保存了的密码
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataGridView3.Visible = true;
            XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”
            if (xml_FilePath != "")//如果用户已读入xml文件，我们的任务就是修改这个xml文件了
            {
              
                //更改密码
                xmlDocument.Load(xml_FilePath);
                XmlNode xmlElement_class = xmlDocument.SelectSingleNode("UserInfo");//找到<class>作为根节点
                xmlElement_class.RemoveAll();//删除旗下所有节点
                int row = dataGridView3.Rows.Count;//得到总行数    
                int cell = dataGridView3.Rows[1].Cells.Count;//得到总列数    
                for (int i = 0; i < row - 1; i++)//遍历这个dataGridView
                {
                    XmlElement xmlElement_student = xmlDocument.CreateElement("user");//创建一个<student>节点
                    XmlElement xmlElement_name = xmlDocument.CreateElement("username");//创建<name>节点
                    if (dataGridView3.Rows[i].Cells[0].Value == null)
                    {
                        dataGridView3.Rows[i].Cells[0].Value = "  ";
                        xmlElement_name.InnerText = dataGridView3.Rows[i].Cells[0].Value.ToString();
                    }
                    else
                    {
                        xmlElement_name.InnerText = dataGridView3.Rows[i].Cells[0].Value.ToString();
                    }
                    //其文本就是第0个单元格的内容
                    xmlElement_student.AppendChild(xmlElement_name);//在<student>下面添加一个新的节点<name>
                    //同理添加<number>
                    XmlElement xmlElement_number = xmlDocument.CreateElement("password");
                    if (dataGridView3.Rows[i].Cells[1].Value == null)
                    {
                        dataGridView3.Rows[i].Cells[1].Value = "  ";
                        xmlElement_name.InnerText = dataGridView3.Rows[i].Cells[1].Value.ToString();
                    }
                    xmlElement_number.InnerText = ComputeMD5Hash(dataGridView3.Rows[i].Cells[1].Value.ToString());
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

        private void ribbonControl1_Click_1(object sender, EventArgs e)
        {

        }
  }
    public class Materials
    {
        public Materials(string[] row1){
          this.name=row1[0];
          this.speed= Convert.ToDouble(row1[1]);
          this.quality=Convert.ToDouble(row1[2]);
          this.momentum=this.speed*this.quality;
          this.date=row1[4];
        }
        public string name;//名称
        public double speed;//速度
        public double quality;//质量
        public double momentum;//动量
        public string date;//录入时间
    }
    public class Data
    {
        public string[] message=new string[5];
        public Data()
        {
            message[0] = null;//名字
            message[1] = null;//质量
            message[2] = null;//速度
            message[3] = null;//动量
            message[4] = null;//录入时间
        }
       
    }
}
