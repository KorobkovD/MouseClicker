namespace MouseClicker
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.upDownClicks = new System.Windows.Forms.NumericUpDown();
            this.upDownSec = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.upDownClicks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownSec)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "кликов за";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "мс";
            // 
            // upDownClicks
            // 
            this.upDownClicks.Location = new System.Drawing.Point(5, 12);
            this.upDownClicks.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.upDownClicks.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upDownClicks.Name = "upDownClicks";
            this.upDownClicks.Size = new System.Drawing.Size(47, 20);
            this.upDownClicks.TabIndex = 4;
            this.upDownClicks.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // upDownSec
            // 
            this.upDownSec.Location = new System.Drawing.Point(110, 12);
            this.upDownSec.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.upDownSec.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upDownSec.Name = "upDownSec";
            this.upDownSec.Size = new System.Drawing.Size(57, 20);
            this.upDownSec.TabIndex = 4;
            this.upDownSec.Value = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(191, 69);
            this.Controls.Add(this.upDownSec);
            this.Controls.Add(this.upDownClicks);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(207, 108);
            this.MinimumSize = new System.Drawing.Size(207, 108);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.upDownClicks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownSec)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown upDownClicks;
        private System.Windows.Forms.NumericUpDown upDownSec;
    }
}

