namespace Bruteforce1
{
    partial class Form1
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
            this.b = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.TextBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // b
            // 
            this.b.BackColor = System.Drawing.SystemColors.GrayText;
            this.b.Location = new System.Drawing.Point(290, 177);
            this.b.Name = "b";
            this.b.Size = new System.Drawing.Size(163, 23);
            this.b.TabIndex = 0;
            this.b.Text = "Begin hack";
            this.b.UseVisualStyleBackColor = false;
            this.b.Click += new System.EventHandler(this.button1_Click);
            //
            //Text box
            //
            
            this.textBox.Location = new System.Drawing.Point(150,0);
            this.textBox.Name = "b";
            this.textBox.Size = new System.Drawing.Size(250,250);
            this.textBox.TabIndex = 1;
            this.textBox.Text = "Type url with Get method here";


            //
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(290,200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(500,300);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            this.label1.ReadOnly = false;
            this.label1.Multiline = true;
            this.label1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.label1.WordWrap = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.b);
            this.Controls.Add(this.textBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        
        #endregion

        private System.Windows.Forms.Button b;
        private System.Windows.Forms.TextBox label1;
        private System.Windows.Forms.TextBox textBox;
    }
}

