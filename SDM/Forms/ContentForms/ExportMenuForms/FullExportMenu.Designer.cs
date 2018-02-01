namespace SDM.Forms.ContentForms.ExportMenuForms
{
    partial class FullExportMenu
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
            this.ExportFullReportButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ExportFullReportButton
            // 
            this.ExportFullReportButton.Location = new System.Drawing.Point(9, 1);
            this.ExportFullReportButton.Name = "ExportFullReportButton";
            this.ExportFullReportButton.Size = new System.Drawing.Size(80, 23);
            this.ExportFullReportButton.TabIndex = 23;
            this.ExportFullReportButton.Text = "Export report";
            this.ExportFullReportButton.UseVisualStyleBackColor = true;
            // 
            // FullExportMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 25);
            this.Controls.Add(this.ExportFullReportButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FullExportMenu";
            this.Text = "ExportForm";
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button ExportFullReportButton;
    }
}