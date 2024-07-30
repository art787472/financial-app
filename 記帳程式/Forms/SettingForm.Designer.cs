namespace 記帳程式.Forms
{
    partial class SettingForm
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
            this.menuBar1 = new 記帳程式.Components.MenuBar();
            this.SuspendLayout();
            // 
            // menuBar1
            // 
            this.menuBar1.Location = new System.Drawing.Point(359, 343);
            this.menuBar1.Name = "menuBar1";
            this.menuBar1.Size = new System.Drawing.Size(419, 82);
            this.menuBar1.TabIndex = 0;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuBar1);
            this.Name = "SettingForm";
            this.Text = "SettinhForm";
            this.ResumeLayout(false);

        }

        #endregion

        public Components.MenuBar menuBar1;
    }
}