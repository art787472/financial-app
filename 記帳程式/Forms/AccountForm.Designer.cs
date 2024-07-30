namespace 記帳程式.Forms
{
    partial class AccountForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.menuBar1 = new 記帳程式.Components.MenuBar();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(331, 196);
            this.button1.Name = "button1";
            this.button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button1.Size = new System.Drawing.Size(138, 58);
            this.button1.TabIndex = 1;
            this.button1.Text = "新增記帳";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuBar1
            // 
            this.menuBar1.Location = new System.Drawing.Point(388, 358);
            this.menuBar1.Name = "menuBar1";
            this.menuBar1.Size = new System.Drawing.Size(400, 80);
            this.menuBar1.TabIndex = 2;
            // 
            // AccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuBar1);
            this.Controls.Add(this.button1);
            this.Name = "AccountForm";
            this.Text = "帳戶設定";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        public Components.MenuBar menuBar1;
    }
}