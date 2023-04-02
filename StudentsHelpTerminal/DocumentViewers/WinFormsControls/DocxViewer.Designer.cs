namespace DocumentViewers
{
    partial class DocxViewer
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.docViewer1 = new Spire.DocViewer.Forms.DocViewer();
            this.SuspendLayout();
            // 
            // docViewer1
            // 
            this.docViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.docViewer1.IsToolBarVisible = false;
            this.docViewer1.Location = new System.Drawing.Point(0, 0);
            this.docViewer1.Name = "docViewer1";
            this.docViewer1.Size = new System.Drawing.Size(150, 150);
            this.docViewer1.TabIndex = 0;
            this.docViewer1.Text = "docViewer1";
            this.docViewer1.UseNewEngine = true;
            // 
            // DocxViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.docViewer1);
            this.Name = "DocxViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private Spire.DocViewer.Forms.DocViewer docViewer1;
    }
}
