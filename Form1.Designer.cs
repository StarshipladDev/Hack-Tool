namespace Bruteforce1
{
    ///<summary>
    ///Form1 is an object that deals with  both the 'physical' GUI of the hacktool.<para />
    ///Form1 also contains the internal hacking code (See <see cref="button1_Click(object, EventArgs)"/>)
    ///</summary>
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button b;
        private System.Windows.Forms.TextBox label1;
        private System.Windows.Forms.TextBox label2;
        private System.Windows.Forms.TextBox textBox;


        //The below code are examples of each major docmentation tag for c#
        /// <param name="disposing">true if managed resources should be disposed, otherwise, false.</param>
        /// <summary>
        /// 
        /// Clean up any resources being used.
        /// Only runs if <c><paramref name="disposing"/></c> is true
        /// <example>
        /// <para>
        /// The program recurivly calls dispose with :<para />
        /// <code>
        /// components.Dispose(); 
        /// </code>
        /// </para>
        /// </example>
        /// <remarks>
        /// Will only dispoase if components exsist, as created at the start of <see cref="Form1" />
        /// 
        /// </remarks>
        /// 
        /// </summary>

        /// 
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
        /// Creates componenets of the windows form
        /// </summary>
        /// 
        private void InitializeComponent()
        {
            this.b = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.TextBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // b
            // 
            this.b.BackColor = System.Drawing.SystemColors.GrayText;
            this.b.Location = new System.Drawing.Point(0, 325);
            this.b.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.b.Name = "b";
            this.b.Size = new System.Drawing.Size(112, 41);
            this.b.TabIndex = 0;
            this.b.Text = "Begin hack";
            this.b.UseVisualStyleBackColor = false;
            this.b.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AcceptsReturn = true;
            this.label1.Location = new System.Drawing.Point(0, 81);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.label1.Multiline = true;
            this.label1.Name = "label1";
            this.label1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.label1.Size = new System.Drawing.Size(526, 244);
            this.label1.TabIndex = 2;
            this.label1.Text = "Hack output will be dispalyed here:";
            this.label1.WordWrap = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(526, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sample Address: http://www.starshiplad.com/search.php?testsearch=REEE";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(0, 41);
            this.textBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(526, 20);
            this.textBox.TabIndex = 1;
            this.textBox.Text = "Type url with Get method here";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(600, 650);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.b);
            this.Controls.Add(this.textBox);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        
        #endregion

        
    }
}

