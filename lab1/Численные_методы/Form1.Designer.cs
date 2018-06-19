namespace Численные_методы
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.задачаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.тестоваяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.основная1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.основная2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.задачаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(845, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // задачаToolStripMenuItem
            // 
            this.задачаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.тестоваяToolStripMenuItem,
            this.основная1ToolStripMenuItem,
            this.основная2ToolStripMenuItem});
            this.задачаToolStripMenuItem.Name = "задачаToolStripMenuItem";
            this.задачаToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.задачаToolStripMenuItem.Text = "Задача";
            // 
            // тестоваяToolStripMenuItem
            // 
            this.тестоваяToolStripMenuItem.Name = "тестоваяToolStripMenuItem";
            this.тестоваяToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.тестоваяToolStripMenuItem.Text = "тестовая";
            this.тестоваяToolStripMenuItem.Click += new System.EventHandler(this.тестоваяToolStripMenuItem_Click);
            // 
            // основная1ToolStripMenuItem
            // 
            this.основная1ToolStripMenuItem.Name = "основная1ToolStripMenuItem";
            this.основная1ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.основная1ToolStripMenuItem.Text = "основная_1";
            this.основная1ToolStripMenuItem.Click += new System.EventHandler(this.основная1ToolStripMenuItem_Click);
            // 
            // основная2ToolStripMenuItem
            // 
            this.основная2ToolStripMenuItem.Name = "основная2ToolStripMenuItem";
            this.основная2ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.основная2ToolStripMenuItem.Text = "основная_2";
            this.основная2ToolStripMenuItem.Click += new System.EventHandler(this.основная2ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 328);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem задачаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem тестоваяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem основная1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem основная2ToolStripMenuItem;
    }
}

