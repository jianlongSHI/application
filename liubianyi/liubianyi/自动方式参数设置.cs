using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Ports;
using System.Xml;
using System.Xml.Xsl;
using System.Configuration;

namespace liubianyi
{
    public partial class 自动方式参数设置 : Form
    {
        public 自动方式参数设置()
        {
            InitializeComponent();
            
        }
        DataTable dt = new DataTable();  
         List<Data > da = new List<Data>();


         
           
         //static DataTable dt = new DataTable();
        private void Form5_Load(object sender, EventArgs e)
         {
             for (int i = 0; i < dataGridView2.RowCount; i++)
             {//列宽自适应表格
                 dataGridView2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
             }
             string newLine = dataGridView1.Text + Environment.NewLine;
             // 将textBox1的内容插入到第一行
             // 索引0是 richText1 第一行位置
             richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
              
             dataGridView2.Columns.Add("", "设置");
             dataGridView2.Columns.Add("", "速率下限");
             dataGridView2.Columns.Add("", "速率上限");
             dataGridView2.Columns.Add("", "段数");
             dataGridView2.Rows.Add(2);
             dataGridView2.Rows[0].Cells[0].Value = "参数";
            dataGridView2.Rows[0].Cells[1].Value=50;
         dataGridView2.Rows[0].Cells[2].Value = 10000;
         dataGridView2.Rows[0].Cells[3].Value = 10;
         dataGridView2.Rows[1].Cells[0].Value = "单位";
         dataGridView2.Rows[1].Cells[1].Value = "m/s";
         dataGridView2.Rows[1].Cells[2].Value = "m/s";
            double  a = Convert.ToDouble(dataGridView2.Rows[0].Cells[1].Value);

              double b= Convert.ToDouble(dataGridView2.Rows[0].Cells[2].Value);
              int c = Convert.ToInt16(dataGridView2.Rows[0].Cells[3].Value);
            
            //毛细管1
            comboBox1.Text = "1*1*180";//名称

            comboBox4.Text = "1";//直径
            comboBox6.Text = "1";//长度
            comboBox8.Text = "180";//入角口
            //毛细2
            comboBox2.Text = "1*1*180";//名称
            comboBox5.Text = "1";//直径
            comboBox7.Text = "20";//长度
            comboBox9.Text = "180";//入角口
            //料筒直径
           comboBox3.Text = "20";
            //dataGridView1
            dataGridView1.Columns.Add("", "段数");
            dataGridView1.Columns.Add("", "速度");
            dataGridView1.Rows.Add(10);
            //添加段数
            for (int i = 0; i < dataGridView1.RowCount; i++)
            { dataGridView1.Rows[i].Cells[0].Value = i + 1; }
            double[] r = new double[5];
          
            //剪切速率分段参数
            //速率下限
            double v1 = a;

            //速率上限
            double v2 = b;
            //段数
            int   num = c;
            //剪切速率对数值ln
            double[] lgx = new double[20];
            //剪切速率分段值
            double[] jqslfdz = new double[20];
            //速度分段值
            double[] jqslfdsd = new double[20];

            //料筒直径
            double comboBox3Text = double.Parse(comboBox3.SelectedItem.ToString());
            //毛细管1直径
           
               double comboBox4Text = double.Parse(comboBox4.Text);
      
            //对数值
            lgx[1] = Math.Log(v1, Math.E);
            lgx[num] = Math.Log(v2, Math.E);
            double[] nmb = new double[5];
            nmb[0] = (lgx[num] - lgx[1]) / (num - 1);
            //第一段速度 jqslfdsd [1]
            jqslfdz[1] = Math.Exp(lgx[1]);
            jqslfdsd[1] = (15 * ((comboBox4Text / 2) * (comboBox4Text / 2) * (comboBox4Text / 2)) * jqslfdz[1]) / ((comboBox3Text / 2) * (comboBox3Text / 2));
            //第2-10速度jqslfdsd [2-n]
            for (int i = 2; i <= num; i++)
            {
                lgx[i] = lgx[i - 1] + nmb[0];
                jqslfdz[i] = Math.Exp(lgx[i]);
                jqslfdsd[i] = (15 * ((comboBox4Text / 2) * (comboBox4Text / 2) * (comboBox4Text / 2)) * jqslfdz[i]) / ((comboBox3Text / 2) * (comboBox3Text / 2));
            }
            //添加每段的数值

            for (int i = 0; i < dataGridView1.RowCount; i++)
            { dataGridView1.Rows[i].Cells[1].Value = Math.Round(jqslfdsd[i + 1], 2); }

          


        }
          
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            
            int c = Convert.ToInt16(dataGridView2.Rows[0].Cells[3].Value);
      
            double b = Convert.ToDouble(dataGridView2.Rows[0].Cells[2].Value);
            double a = Convert.ToDouble(dataGridView2.Rows[0].Cells[1].Value);
           
            //添加段数
            for (int i = 0; i < dataGridView1.RowCount; i++)
            { dataGridView1.Rows[i].Cells[0].Value = i + 1; }
            double[] r = new double[5];

            //剪切速率分段参数
            //速率下限
            double v1 = a;
           
            //速率上限
            double v2 = b;
            //段数
            int num = c;
            //剪切速率对数值ln
            double[] lgx = new double[20];
            //剪切速率分段值
            double[] jqslfdz = new double[20];
            //速度分段值
            double[] jqslfdsd = new double[20];
            comboBox3.Text = "20";
            //料筒直径
            double comboBox3Text = double.Parse(comboBox3.SelectedItem.ToString());
            //毛细管1直径

            double comboBox4Text = double.Parse(comboBox4.Text);
           
            //对数值
            lgx[1] = Math.Log(v1, Math.E);
            lgx[num] = Math.Log(v2, Math.E);
            double[] nmb = new double[5];
            nmb[0] = (lgx[num] - lgx[1]) / (num - 1);
            //第一段速度 jqslfdsd [1]
            jqslfdz[1] = Math.Exp(lgx[1]);
            jqslfdsd[1] = (15 * ((comboBox4Text / 2) * (comboBox4Text / 2) * (comboBox4Text / 2)) * jqslfdz[1]) / ((comboBox3Text / 2) * (comboBox3Text / 2));
            //第2-10速度jqslfdsd [2-n]
            for (int i = 2; i <= num; i++)
            {
                lgx[i] = lgx[i - 1] + nmb[0];
                jqslfdz[i] = Math.Exp(lgx[i]);
                jqslfdsd[i] = (15 * ((comboBox4Text / 2) * (comboBox4Text / 2) * (comboBox4Text / 2)) * jqslfdz[i]) / ((comboBox3Text / 2) * (comboBox3Text / 2));
            }
            //添加每段的数值

            for (int i = 0; i < dataGridView1.RowCount; i++)
            { dataGridView1.Rows[i].Cells[1].Value = Math.Round(jqslfdsd[i + 1], 2); }
           
          
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //毛细管1
            comboBox1.Text = "1*1*180";//名称

            comboBox4.Text = "1";//直径
            comboBox6.Text = "1";//长度
            comboBox8.Text = "180";//入角口
            //毛细2
            comboBox2.Text = "1*1*180";//名称
            comboBox5.Text = "1";//直径
            comboBox7.Text = "20";//长度
            comboBox9.Text = "180";//入角口
            //料筒直径
            comboBox3.Text = "20";
            double a = Convert.ToDouble(dataGridView2.Rows[0].Cells[1].Value);

            double b = Convert.ToDouble(dataGridView2.Rows[0].Cells[2].Value);
            int c = Convert.ToInt16(dataGridView2.Rows[0].Cells[3].Value);
            //添加段数
            for (int i = 0; i < dataGridView1.RowCount; i++)
            { dataGridView1.Rows[i].Cells[0].Value = i + 1; }
            double[] r = new double[5];

            //剪切速率分段参数
            //速率下限
            double v1 = a;

            //速率上限
            double v2 = b;
            //段数
            int num = c;
            //剪切速率对数值ln
            double[] lgx = new double[20];
            //剪切速率分段值
            double[] jqslfdz = new double[20];
            //速度分段值
            double[] jqslfdsd = new double[20];
            comboBox3.Text = "20";
            //料筒直径
            double comboBox3Text = double.Parse(comboBox3.SelectedItem.ToString());
            //毛细管1直径

            double comboBox4Text = double.Parse(comboBox4.Text);
            //     double comboBox4Text = double.Parse(comboBox4.SelectedItem.ToString());
            //!!!!
            //需要从txtbox取值
            //对数值
            lgx[1] = Math.Log(v1, Math.E);
            lgx[num] = Math.Log(v2, Math.E);
            double[] nmb = new double[5];
            nmb[0] = (lgx[num] - lgx[1]) / (num - 1);
            //第一段速度 jqslfdsd [1]
            jqslfdz[1] = Math.Exp(lgx[1]);
            jqslfdsd[1] = (15 * ((comboBox4Text / 2) * (comboBox4Text / 2) * (comboBox4Text / 2)) * jqslfdz[1]) / ((comboBox3Text / 2) * (comboBox3Text / 2));
            //第2-10速度jqslfdsd [2-n]
            for (int i = 2; i <= num; i++)
            {
                lgx[i] = lgx[i - 1] + nmb[0];
                jqslfdz[i] = Math.Exp(lgx[i]);
                jqslfdsd[i] = (15 * ((comboBox4Text / 2) * (comboBox4Text / 2) * (comboBox4Text / 2)) * jqslfdz[i]) / ((comboBox3Text / 2) * (comboBox3Text / 2));
            }
            //添加每段的数值

            for (int i = 0; i < dataGridView1.RowCount; i++)
            { dataGridView1.Rows[i].Cells[1].Value = Math.Round(jqslfdsd[i + 1], 2); }

        }
        //导入参数
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "段 数        速 度" + System.Environment.NewLine + "1            " + dataGridView1.Rows[0].Cells[1].Value + System.Environment.NewLine + "2            " + dataGridView1.Rows[1].Cells[1].Value + System.Environment.NewLine + "3            " + dataGridView1.Rows[2].Cells[1].Value + System.Environment.NewLine + "4            " + dataGridView1.Rows[3].Cells[1].Value + System.Environment.NewLine + "5            " + dataGridView1.Rows[4].Cells[1].Value + System.Environment.NewLine + "6            " + dataGridView1.Rows[5].Cells[1].Value + System.Environment.NewLine + "7            " + dataGridView1.Rows[6].Cells[1].Value + System.Environment.NewLine + "8            " + dataGridView1.Rows[7].Cells[1].Value + System.Environment.NewLine + "9            " + dataGridView1.Rows[8].Cells[1].Value + System.Environment.NewLine + "10           " + dataGridView1.Rows[9].Cells[1].Value + System.Environment.NewLine;
           
            int c = Convert.ToInt16(dataGridView2.Rows[0].Cells[3].Value);

            double b = Convert.ToDouble(dataGridView2.Rows[0].Cells[2].Value);
            double a = Convert.ToDouble(dataGridView2.Rows[0].Cells[1].Value);

            //添加段数
            for (int i = 0; i < dataGridView1.RowCount; i++)
            { dataGridView1.Rows[i].Cells[0].Value = i + 1; }
            double[] r = new double[5];

            //剪切速率分段参数
            //速率下限
            double v1 = a;

            //速率上限
            double v2 = b;
            //段数
            int num = c;
            //剪切速率对数值ln
            double[] lgx = new double[20];
            //剪切速率分段值
            double[] jqslfdz = new double[20];
            //速度分段值
            double[] jqslfdsd = new double[20];
            comboBox3.Text = "20";
            //料筒直径
            double comboBox3Text = double.Parse(comboBox3.SelectedItem.ToString());
            //毛细管1直径

            double comboBox4Text = double.Parse(comboBox4.Text);

            //对数值
            lgx[1] = Math.Log(v1, Math.E);
            lgx[num] = Math.Log(v2, Math.E);
            double[] nmb = new double[5];
            nmb[0] = (lgx[num] - lgx[1]) / (num - 1);
            //第一段速度 jqslfdsd [1]
            jqslfdz[1] = Math.Exp(lgx[1]);
            jqslfdsd[1] = (15 * ((comboBox4Text / 2) * (comboBox4Text / 2) * (comboBox4Text / 2)) * jqslfdz[1]) / ((comboBox3Text / 2) * (comboBox3Text / 2));
            //第2-10速度jqslfdsd [2-n]
            for (int i = 2; i <= num; i++)
            {
                lgx[i] = lgx[i - 1] + nmb[0];
                jqslfdz[i] = Math.Exp(lgx[i]);
                jqslfdsd[i] = (15 * ((comboBox4Text / 2) * (comboBox4Text / 2) * (comboBox4Text / 2)) * jqslfdz[i]) / ((comboBox3Text / 2) * (comboBox3Text / 2));
            }
            //添加每段的数值
            for (int i = 0; i < dataGridView1.RowCount; i++)
            { dataGridView1.Rows[i].Cells[1].Value = Math.Round(jqslfdsd[i + 1], 2); }
            jishu.rowsum = dataGridView1.RowCount;//记录设定段数
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {  
        }
        //保存文件
        private void button2_Click(object sender, EventArgs e)
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
            // FileStream fs=new FileStream(FullFileName,FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(FullFileName, true, Encoding.Default);
            string str = "";
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                if (i > 0)
                {
                    str += "!";
                }
                str += dataGridView1.Columns[i].HeaderText;
            }
            sw.WriteLine(str);

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    str = dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() + "!";
                    if (str.Length < 10)
                        str = str.PadRight(10, ' '); //不够长度的，补齐空格！
                    sw.Write(str);
                }
                sw.WriteLine("");
            }
            sw.Close();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
    }


    //打开文件
        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult dr;
            dr = openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.LoadFile(openFileDialog1.FileName, System.Windows.Forms.RichTextBoxStreamType.PlainText);
            }

        }
        //导入到表格
        private void button4_Click(object sender, EventArgs e)
        {
            
            //DataRow row = dt.NewRow();
            //dt.Rows.Add(row1);
            //this.datagridView1.DataSource = dt;
          //da.Clear();
          //     //清空List
          //     DataTable dt = GetDataFromExcelByCom(true);//自动添加列与表头
          //     dataGridView2.DataSource = dt;
          //     MessageBox.Show("导入成功");
          //     for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
          //     {
          //         da.Add(new Data());
          //         for (int j = 0; j < dataGridView2.Columns.Count; j++)
          //         {
          //             da[i].message[j] = dataGridView2.Rows[i].Cells[j].Value.ToString();
          //         }
          //     }
          
            using (OpenFileDialog dlgText = new OpenFileDialog())      
            {                dlgText.Filter = "文本文件|*.txt";     
                if (dlgText.ShowDialog() == DialogResult.OK)   
                {                  
                    //新建一个datatable用于保存读入的数据     
                    DataTable dt = new DataTable();        
                    //给datatable添加6个列标题  
                  
                  dt.Columns.Add("段数", typeof(String));    
                   dt.Columns.Add("速度", typeof(String));
                   
                    //读入文件                
                    using (StreamReader reader = new StreamReader(dlgText.FileName, Encoding.Default))                  
                    {
                        
                        //循环读取所有行       
                        while (!reader.EndOfStream)       
                        {
                            dataGridView1.Columns.Clear();
                            //将每行数据，用“Tab”分割成6段     
                            char[] separator = {'!'};           
                           // string[] data = reader.ReadLine().Split(separator);  
                           string[] data = reader.ReadLine().Replace("段数", "速度").Split(separator);                           
                            //新建一行，并将读出的数据分段，分别存入6个对应的列中                           
                            DataRow dr = dt.NewRow();                   
                            dr[0] = data[0];
                            dr[1] = data[1];
                            //将这行数据加入到datatable中             
                            dt.Rows.Add(dr);
                           // da.Clear();
                           // //清空List
                           //// DataTable dt = GetDataFromExcelByCom(true);//自动添加列与表头
                           // dataGridView1.DataSource = dt;
                           //MessageBox.Show("导入成功");
                           // for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                           // {
                           //     da.Add(new Data());
                           //     for (int j = 0; j < dataGridView1.Columns.Count; j++)
                           //     {
                           //         da[i].message[j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                           //     }
                           // }
          
                        }           
                    }                  
                    //将datatable绑定到datagridview上显示结果    
                    this.dataGridView1.DataSource = dt;          
                    //删除第一行                   
                    this.dataGridView1.Rows.RemoveAt(0);    
                    //行头隐藏                  
                    //this.dataGridView1.RowHeadersVisible = false;   

                }         
            }     
        

        }
        }
}
public class Data
{
    public string[] message = new string[5];
    public Data()
    {
        message[0] = null;//段数
        message[1] = null;//速度
       
    }

}
