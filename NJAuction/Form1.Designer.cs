namespace NJAuction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pos2 = new System.Windows.Forms.TextBox();
            this.pos1 = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.etc1 = new System.Windows.Forms.RadioButton();
            this.price1 = new System.Windows.Forms.TextBox();
            this.sobi1 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.price2 = new System.Windows.Forms.TextBox();
            this.etc2 = new System.Windows.Forms.RadioButton();
            this.sobi2 = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(8, 36);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 41);
            this.button1.TabIndex = 5;
            this.button1.Text = "소비/기타";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.Location = new System.Drawing.Point(102, 36);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 41);
            this.button2.TabIndex = 15;
            this.button2.Text = "정지";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button3.Location = new System.Drawing.Point(8, 85);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(88, 41);
            this.button3.TabIndex = 18;
            this.button3.Text = "장비/캐시";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "0";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button4.Location = new System.Drawing.Point(102, 85);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(85, 41);
            this.button4.TabIndex = 20;
            this.button4.Text = "종료";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(9, 273);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "F1 : 중지하기, F2 = 종료하기";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(9, 298);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(243, 15);
            this.label4.TabIndex = 22;
            this.label4.Text = "F11 : 소비/기타 구매, F12 = 장비/캐시 구매";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(9, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 15);
            this.label5.TabIndex = 23;
            this.label5.Text = "구매 시도 횟수";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(273, 45);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(219, 274);
            this.listBox1.TabIndex = 25;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.pos2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.pos1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(491, 325);
            this.panel1.TabIndex = 28;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(57, 224);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(205, 23);
            this.textBox1.TabIndex = 31;
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button6.Location = new System.Drawing.Point(193, 36);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(69, 90);
            this.button6.TabIndex = 42;
            this.button6.Text = "데이터   저장";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(127, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 25;
            this.label2.Text = "판매 시도 횟수";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label7.Location = new System.Drawing.Point(20, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 15);
            this.label7.TabIndex = 29;
            this.label7.Text = "ID";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(224, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 15);
            this.label6.TabIndex = 24;
            this.label6.Text = "0";
            // 
            // pos2
            // 
            this.pos2.Location = new System.Drawing.Point(141, 192);
            this.pos2.Name = "pos2";
            this.pos2.Size = new System.Drawing.Size(31, 23);
            this.pos2.TabIndex = 37;
            // 
            // pos1
            // 
            this.pos1.Location = new System.Drawing.Point(141, 152);
            this.pos1.Name = "pos1";
            this.pos1.Size = new System.Drawing.Size(31, 23);
            this.pos1.TabIndex = 36;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(21, 198);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 35;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(21, 156);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 34;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.etc1);
            this.groupBox1.Controls.Add(this.price1);
            this.groupBox1.Controls.Add(this.sobi1);
            this.groupBox1.Location = new System.Drawing.Point(12, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 37);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            // 
            // etc1
            // 
            this.etc1.AutoSize = true;
            this.etc1.Location = new System.Drawing.Point(81, 13);
            this.etc1.Name = "etc1";
            this.etc1.Size = new System.Drawing.Size(49, 19);
            this.etc1.TabIndex = 34;
            this.etc1.TabStop = true;
            this.etc1.Text = "기타";
            this.etc1.UseVisualStyleBackColor = true;
            // 
            // price1
            // 
            this.price1.Location = new System.Drawing.Point(166, 11);
            this.price1.Name = "price1";
            this.price1.Size = new System.Drawing.Size(74, 23);
            this.price1.TabIndex = 38;
            // 
            // sobi1
            // 
            this.sobi1.AutoSize = true;
            this.sobi1.Location = new System.Drawing.Point(28, 13);
            this.sobi1.Name = "sobi1";
            this.sobi1.Size = new System.Drawing.Size(49, 19);
            this.sobi1.TabIndex = 33;
            this.sobi1.TabStop = true;
            this.sobi1.Text = "소비";
            this.sobi1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.price2);
            this.groupBox2.Controls.Add(this.etc2);
            this.groupBox2.Controls.Add(this.sobi2);
            this.groupBox2.Location = new System.Drawing.Point(12, 182);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 37);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            // 
            // price2
            // 
            this.price2.Location = new System.Drawing.Point(166, 10);
            this.price2.Name = "price2";
            this.price2.Size = new System.Drawing.Size(74, 23);
            this.price2.TabIndex = 39;
            // 
            // etc2
            // 
            this.etc2.AutoSize = true;
            this.etc2.Location = new System.Drawing.Point(80, 13);
            this.etc2.Name = "etc2";
            this.etc2.Size = new System.Drawing.Size(49, 19);
            this.etc2.TabIndex = 34;
            this.etc2.TabStop = true;
            this.etc2.Text = "기타";
            this.etc2.UseVisualStyleBackColor = true;
            // 
            // sobi2
            // 
            this.sobi2.AutoSize = true;
            this.sobi2.Location = new System.Drawing.Point(27, 13);
            this.sobi2.Name = "sobi2";
            this.sobi2.Size = new System.Drawing.Size(49, 19);
            this.sobi2.TabIndex = 33;
            this.sobi2.TabStop = true;
            this.sobi2.Text = "소비";
            this.sobi2.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(292, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(192, 15);
            this.label8.TabIndex = 30;
            this.label8.Text = "NJ AUCTION V8.0[FAST VERSION]";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label9.Location = new System.Drawing.Point(4, 256);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 15);
            this.label9.TabIndex = 43;
            this.label9.Text = "원하는 구매수량";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(109, 249);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(153, 23);
            this.textBox2.TabIndex = 44;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(501, 333);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(1000, 0);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "NJ AUCTION PIXEL VERSION";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox price2;
        private System.Windows.Forms.TextBox price1;
        private System.Windows.Forms.TextBox pos2;
        private System.Windows.Forms.TextBox pos1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton etc1;
        private System.Windows.Forms.RadioButton sobi1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton etc2;
        private System.Windows.Forms.RadioButton sobi2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label9;
    }
}

