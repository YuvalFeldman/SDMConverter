namespace SDM.Forms.ContentForms.ExportMenuForms
{
    partial class SummedExportMenu
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
            this.exportSummedReportButton = new System.Windows.Forms.Button();
            this.exportErrorsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // exportSummedReportButton
            // 
            this.exportSummedReportButton.Location = new System.Drawing.Point(9, 1);
            this.exportSummedReportButton.Name = "exportSummedReportButton";
            this.exportSummedReportButton.Size = new System.Drawing.Size(80, 23);
            this.exportSummedReportButton.TabIndex = 27;
            this.exportSummedReportButton.Text = "Export report";
            this.exportSummedReportButton.UseVisualStyleBackColor = true;
            // 
            // exportErrorsButton
            // 
            this.exportErrorsButton.Location = new System.Drawing.Point(95, 1);
            this.exportErrorsButton.Name = "exportErrorsButton";
            this.exportErrorsButton.Size = new System.Drawing.Size(80, 23);
            this.exportErrorsButton.TabIndex = 28;
            this.exportErrorsButton.Text = "Export Errors";
            this.exportErrorsButton.UseVisualStyleBackColor = true;
            // 
            // SummedExportMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 25);
            this.Controls.Add(this.exportErrorsButton);
            this.Controls.Add(this.exportSummedReportButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SummedExportMenu";
            this.Text = "SummedExportMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button exportSummedReportButton;
        private System.Windows.Forms.Button exportErrorsButton;
    }
}