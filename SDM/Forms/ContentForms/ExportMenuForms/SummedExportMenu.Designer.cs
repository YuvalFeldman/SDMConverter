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
            this.summedTablesComboBox = new System.Windows.Forms.ComboBox();
            this.exportAllReports = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // exportSummedReportButton
            // 
            this.exportSummedReportButton.Location = new System.Drawing.Point(136, 1);
            this.exportSummedReportButton.Name = "exportSummedReportButton";
            this.exportSummedReportButton.Size = new System.Drawing.Size(98, 23);
            this.exportSummedReportButton.TabIndex = 27;
            this.exportSummedReportButton.Text = "Export report";
            this.exportSummedReportButton.UseVisualStyleBackColor = true;
            // 
            // summedTablesComboBox
            // 
            this.summedTablesComboBox.FormattingEnabled = true;
            this.summedTablesComboBox.Location = new System.Drawing.Point(9, 2);
            this.summedTablesComboBox.Name = "summedTablesComboBox";
            this.summedTablesComboBox.Size = new System.Drawing.Size(121, 21);
            this.summedTablesComboBox.TabIndex = 29;
            // 
            // exportAllReports
            // 
            this.exportAllReports.Location = new System.Drawing.Point(240, 1);
            this.exportAllReports.Name = "exportAllReports";
            this.exportAllReports.Size = new System.Drawing.Size(98, 23);
            this.exportAllReports.TabIndex = 30;
            this.exportAllReports.Text = "Export all reports";
            this.exportAllReports.UseVisualStyleBackColor = true;
            // 
            // SummedExportMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 25);
            this.Controls.Add(this.exportAllReports);
            this.Controls.Add(this.summedTablesComboBox);
            this.Controls.Add(this.exportSummedReportButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SummedExportMenu";
            this.Text = "SummedExportMenu";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button exportSummedReportButton;
        public System.Windows.Forms.ComboBox summedTablesComboBox;
        public System.Windows.Forms.Button exportAllReports;
    }
}