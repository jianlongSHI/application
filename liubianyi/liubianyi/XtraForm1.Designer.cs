namespace liubianyi
{
    partial class Form1a
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1a));
            this.txtPwd = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.login = new DevExpress.XtraEditors.SimpleButton();
            this.sign = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPwd
            // 
            resources.ApplyResources(this.txtPwd, "txtPwd");
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.txtPwd.Properties.UseSystemPasswordChar = true;
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            this.txtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            // 
            // login
            // 
            this.login.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.login, "login");
            this.login.LookAndFeel.SkinName = "Office 2010 Blue";
            this.login.LookAndFeel.UseDefaultLookAndFeel = false;
            this.login.Name = "login";
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // sign
            // 
            this.sign.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            resources.ApplyResources(this.sign, "sign");
            this.sign.LookAndFeel.SkinName = "Office 2010 Blue";
            this.sign.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sign.Name = "sign";
            this.sign.UseWaitCursor = true;
            this.sign.Click += new System.EventHandler(this.sign_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.simpleButton1, "simpleButton1");
            this.simpleButton1.LookAndFeel.SkinName = "Office 2010 Blue";
            this.simpleButton1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // Form1
            // 
            this.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("Form1.Appearance.BackColor")));
            this.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile;
            this.BackgroundImageStore = global::liubianyi.Properties.Resources.login2;
            this.ControlBox = false;
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.login);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.sign);
            this.Controls.Add(this.txtPwd);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.LookAndFeel.SkinName = "Office 2010 Blue";
            this.LookAndFeel.TouchUIMode = DevExpress.Utils.DefaultBoolean.False;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.XtraForm1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.XtraForm1_MouseDown_1);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.XtraForm1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.XtraForm1_MouseUp_1);
            this.Resize += new System.EventHandler(this.XtraForm1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtPwd;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.SimpleButton login;
        private DevExpress.XtraEditors.SimpleButton sign;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}