namespace 自动方式压力平衡条件设定
{
    partial class Formsetting
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.bandedGridView2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand9 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.bandedGridView3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand11 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand16 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand17 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn9 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand12 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand18 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn10 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand19 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn11 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand13 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand21 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn12 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand14 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand20 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn13 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand15 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn14 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // bandedGridView2
            // 
            this.bandedGridView2.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand9});
            this.bandedGridView2.GridControl = this.gridControl2;
            this.bandedGridView2.Name = "bandedGridView2";
            // 
            // gridBand9
            // 
            this.gridBand9.Caption = "gridBand9";
            this.gridBand9.Name = "gridBand9";
            this.gridBand9.VisibleIndex = 0;
            // 
            // gridControl2
            // 
            this.gridControl2.Location = new System.Drawing.Point(12, 12);
            this.gridControl2.MainView = this.bandedGridView3;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(765, 220);
            this.gridControl2.TabIndex = 1;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView3,
            this.bandedGridView2});
            this.gridControl2.Click += new System.EventHandler(this.gridControl2_Click);
            // 
            // bandedGridView3
            // 
            this.bandedGridView3.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand11,
            this.gridBand12,
            this.gridBand13,
            this.gridBand14,
            this.gridBand15});
            this.bandedGridView3.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumn8,
            this.bandedGridColumn9,
            this.bandedGridColumn10,
            this.bandedGridColumn11,
            this.bandedGridColumn12,
            this.bandedGridColumn13,
            this.bandedGridColumn14});
            this.bandedGridView3.GridControl = this.gridControl2;
            this.bandedGridView3.Name = "bandedGridView3";
            this.bandedGridView3.OptionsMenu.EnableColumnMenu = false;
            // 
            // gridBand11
            // 
            this.gridBand11.Caption = "速度";
            this.gridBand11.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand16,
            this.gridBand17});
            this.gridBand11.Name = "gridBand11";
            this.gridBand11.VisibleIndex = 0;
            this.gridBand11.Width = 243;
            // 
            // gridBand16
            // 
            this.gridBand16.Caption = "下限";
            this.gridBand16.Columns.Add(this.bandedGridColumn8);
            this.gridBand16.Name = "gridBand16";
            this.gridBand16.VisibleIndex = 0;
            this.gridBand16.Width = 114;
            // 
            // bandedGridColumn8
            // 
            this.bandedGridColumn8.Caption = "下限";
            this.bandedGridColumn8.FieldName = "column0";
            this.bandedGridColumn8.Name = "bandedGridColumn8";
            this.bandedGridColumn8.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn8.Visible = true;
            this.bandedGridColumn8.Width = 114;
            // 
            // gridBand17
            // 
            this.gridBand17.Caption = "上限";
            this.gridBand17.Columns.Add(this.bandedGridColumn9);
            this.gridBand17.Name = "gridBand17";
            this.gridBand17.VisibleIndex = 1;
            this.gridBand17.Width = 129;
            // 
            // bandedGridColumn9
            // 
            this.bandedGridColumn9.Caption = "上限";
            this.bandedGridColumn9.FieldName = "column1";
            this.bandedGridColumn9.Name = "bandedGridColumn9";
            this.bandedGridColumn9.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn9.Visible = true;
            this.bandedGridColumn9.Width = 129;
            // 
            // gridBand12
            // 
            this.gridBand12.Caption = "平衡变量条件";
            this.gridBand12.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand18,
            this.gridBand19});
            this.gridBand12.Name = "gridBand12";
            this.gridBand12.VisibleIndex = 1;
            this.gridBand12.Width = 229;
            // 
            // gridBand18
            // 
            this.gridBand18.Caption = "ΔP";
            this.gridBand18.Columns.Add(this.bandedGridColumn10);
            this.gridBand18.Name = "gridBand18";
            this.gridBand18.VisibleIndex = 0;
            this.gridBand18.Width = 154;
            // 
            // bandedGridColumn10
            // 
            this.bandedGridColumn10.Caption = "ΔP";
            this.bandedGridColumn10.FieldName = "column2";
            this.bandedGridColumn10.Name = "bandedGridColumn10";
            this.bandedGridColumn10.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn10.Visible = true;
            this.bandedGridColumn10.Width = 154;
            // 
            // gridBand19
            // 
            this.gridBand19.Caption = "N%";
            this.gridBand19.Columns.Add(this.bandedGridColumn11);
            this.gridBand19.Name = "gridBand19";
            this.gridBand19.VisibleIndex = 1;
            this.gridBand19.Width = 75;
            // 
            // bandedGridColumn11
            // 
            this.bandedGridColumn11.Caption = "N%";
            this.bandedGridColumn11.FieldName = "column3";
            this.bandedGridColumn11.Name = "bandedGridColumn11";
            this.bandedGridColumn11.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn11.Visible = true;
            // 
            // gridBand13
            // 
            this.gridBand13.Caption = "平衡持续时间";
            this.gridBand13.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand21});
            this.gridBand13.Name = "gridBand13";
            this.gridBand13.VisibleIndex = 2;
            this.gridBand13.Width = 98;
            // 
            // gridBand21
            // 
            this.gridBand21.Caption = "秒";
            this.gridBand21.Columns.Add(this.bandedGridColumn12);
            this.gridBand21.Name = "gridBand21";
            this.gridBand21.VisibleIndex = 0;
            this.gridBand21.Width = 98;
            // 
            // bandedGridColumn12
            // 
            this.bandedGridColumn12.Caption = "秒";
            this.bandedGridColumn12.FieldName = "column4";
            this.bandedGridColumn12.Name = "bandedGridColumn12";
            this.bandedGridColumn12.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn12.Visible = true;
            this.bandedGridColumn12.Width = 98;
            // 
            // gridBand14
            // 
            this.gridBand14.Caption = "平衡交变次数";
            this.gridBand14.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand20});
            this.gridBand14.Name = "gridBand14";
            this.gridBand14.VisibleIndex = 3;
            this.gridBand14.Width = 135;
            // 
            // gridBand20
            // 
            this.gridBand20.Caption = "变交累计";
            this.gridBand20.Columns.Add(this.bandedGridColumn13);
            this.gridBand20.Name = "gridBand20";
            this.gridBand20.VisibleIndex = 0;
            this.gridBand20.Width = 135;
            // 
            // bandedGridColumn13
            // 
            this.bandedGridColumn13.Caption = "变量计数";
            this.bandedGridColumn13.FieldName = "column5";
            this.bandedGridColumn13.Name = "bandedGridColumn13";
            this.bandedGridColumn13.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridColumn13.Visible = true;
            this.bandedGridColumn13.Width = 135;
            // 
            // gridBand15
            // 
            this.gridBand15.Caption = "连续采集个数";
            this.gridBand15.Columns.Add(this.bandedGridColumn14);
            this.gridBand15.Name = "gridBand15";
            this.gridBand15.VisibleIndex = 4;
            this.gridBand15.Width = 165;
            // 
            // bandedGridColumn14
            // 
            this.bandedGridColumn14.Caption = "连续采集个数";
            this.bandedGridColumn14.FieldName = "column6";
            this.bandedGridColumn14.Name = "bandedGridColumn14";
            this.bandedGridColumn14.Visible = true;
            this.bandedGridColumn14.Width = 165;
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = -1;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "gridBand2";
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = -1;
            // 
            // gridBand3
            // 
            this.gridBand3.Caption = "gridBand3";
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = -1;
            // 
            // gridBand4
            // 
            this.gridBand4.Caption = "gridBand4";
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.VisibleIndex = -1;
            // 
            // Formsetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 235);
            this.ControlBox = false;
            this.Controls.Add(this.gridControl2);
            this.Name = "Formsetting";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn10;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn11;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn12;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn13;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn14;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand11;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand16;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand17;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand12;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand18;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand19;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand13;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand21;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand14;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand20;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand15;
    }
}

