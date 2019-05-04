using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using DevExpress.XtraGrid.Columns;

namespace 自动方式压力平衡条件设定
{
    public partial class Formsetting : Form
    {
        public Formsetting()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.bandedGridView3.OptionsView.AllowCellMerge = true;//合并列中相同的数字
            bandedGridView3.OptionsView.ShowColumnHeaders = false;//隐藏表头
            DataTable dt = new DataTable();
            for (int i = 0; i <= 6; i++)
            {
                string x = "column"+i;
                dt.Columns.Add(x, typeof(String));
            }
            dt.Rows.Add(">0","<=1","0.020"," ","45","2","20");
            dt.Rows.Add(">1","<=8", "0.030","0.05", "30", "6","20");
            dt.Rows.Add(">8", "<=300", " ", "1.000", "15", "6", "20");
            this.gridControl2.DataSource = dt;
        }
        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            DataTable dt2 = this.gridControl2.DataSource as DataTable;
        }
    }
}

