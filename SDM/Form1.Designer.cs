namespace SDM
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
            this.centurionImport = new System.Windows.Forms.Button();
            this.ImportClient = new System.Windows.Forms.Button();
            this.ExportFullDb = new System.Windows.Forms.Button();
            this.ExportSummedDb = new System.Windows.Forms.Button();
            this.DeleteCenturion = new System.Windows.Forms.Button();
            this.DeleteClientData = new System.Windows.Forms.Button();
            this.LatencyConversionTable = new System.Windows.Forms.Button();
            this.ClientIdBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // centurionImport
            // 
            this.centurionImport.Location = new System.Drawing.Point(12, 12);
            this.centurionImport.Name = "centurionImport";
            this.centurionImport.Size = new System.Drawing.Size(260, 23);
            this.centurionImport.TabIndex = 0;
            this.centurionImport.Text = "Import Centurion Report";
            this.centurionImport.UseVisualStyleBackColor = true;
            this.centurionImport.Click += new System.EventHandler(this.centurionImport_Click);
            // 
            // ImportClient
            // 
            this.ImportClient.Location = new System.Drawing.Point(12, 41);
            this.ImportClient.Name = "ImportClient";
            this.ImportClient.Size = new System.Drawing.Size(260, 23);
            this.ImportClient.TabIndex = 1;
            this.ImportClient.Text = "Import Client Data Report";
            this.ImportClient.UseVisualStyleBackColor = true;
            this.ImportClient.Click += new System.EventHandler(this.ImportClient_Click);
            // 
            // ExportFullDb
            // 
            this.ExportFullDb.Location = new System.Drawing.Point(12, 70);
            this.ExportFullDb.Name = "ExportFullDb";
            this.ExportFullDb.Size = new System.Drawing.Size(260, 23);
            this.ExportFullDb.TabIndex = 2;
            this.ExportFullDb.Text = "Export Full Database Report";
            this.ExportFullDb.UseVisualStyleBackColor = true;
            this.ExportFullDb.Click += new System.EventHandler(this.ExportFullDb_Click);
            // 
            // ExportSummedDb
            // 
            this.ExportSummedDb.Location = new System.Drawing.Point(12, 99);
            this.ExportSummedDb.Name = "ExportSummedDb";
            this.ExportSummedDb.Size = new System.Drawing.Size(260, 23);
            this.ExportSummedDb.TabIndex = 3;
            this.ExportSummedDb.Text = "Export Summed Database Report";
            this.ExportSummedDb.UseVisualStyleBackColor = true;
            this.ExportSummedDb.Click += new System.EventHandler(this.ExportSummedDb_Click);
            // 
            // DeleteCenturion
            // 
            this.DeleteCenturion.Location = new System.Drawing.Point(432, 12);
            this.DeleteCenturion.Name = "DeleteCenturion";
            this.DeleteCenturion.Size = new System.Drawing.Size(260, 23);
            this.DeleteCenturion.TabIndex = 4;
            this.DeleteCenturion.Text = "Delete Centurion Report";
            this.DeleteCenturion.UseVisualStyleBackColor = true;
            this.DeleteCenturion.Click += new System.EventHandler(this.DeleteCenturion_Click);
            // 
            // DeleteClientData
            // 
            this.DeleteClientData.Location = new System.Drawing.Point(432, 41);
            this.DeleteClientData.Name = "DeleteClientData";
            this.DeleteClientData.Size = new System.Drawing.Size(260, 23);
            this.DeleteClientData.TabIndex = 5;
            this.DeleteClientData.Text = "Delete Client Data Report";
            this.DeleteClientData.UseVisualStyleBackColor = true;
            this.DeleteClientData.Click += new System.EventHandler(this.DeleteClientData_Click);
            // 
            // LatencyConversionTable
            // 
            this.LatencyConversionTable.Location = new System.Drawing.Point(12, 234);
            this.LatencyConversionTable.Name = "LatencyConversionTable";
            this.LatencyConversionTable.Size = new System.Drawing.Size(260, 23);
            this.LatencyConversionTable.TabIndex = 6;
            this.LatencyConversionTable.Text = "Set Latency Conversion Table";
            this.LatencyConversionTable.UseVisualStyleBackColor = true;
            this.LatencyConversionTable.Click += new System.EventHandler(this.LatencyConversionTable_Click);
            // 
            // ClientIdBox
            // 
            this.ClientIdBox.Location = new System.Drawing.Point(278, 41);
            this.ClientIdBox.Name = "ClientIdBox";
            this.ClientIdBox.Size = new System.Drawing.Size(100, 20);
            this.ClientIdBox.TabIndex = 7;
            this.ClientIdBox.Text = "Client Id";
            this.ClientIdBox.TextChanged += new System.EventHandler(this.ClientIdBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 278);
            this.Controls.Add(this.ClientIdBox);
            this.Controls.Add(this.LatencyConversionTable);
            this.Controls.Add(this.DeleteClientData);
            this.Controls.Add(this.DeleteCenturion);
            this.Controls.Add(this.ExportSummedDb);
            this.Controls.Add(this.ExportFullDb);
            this.Controls.Add(this.ImportClient);
            this.Controls.Add(this.centurionImport);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button centurionImport;
        private System.Windows.Forms.Button ImportClient;
        private System.Windows.Forms.Button ExportFullDb;
        private System.Windows.Forms.Button ExportSummedDb;
        private System.Windows.Forms.Button DeleteCenturion;
        private System.Windows.Forms.Button DeleteClientData;
        private System.Windows.Forms.Button LatencyConversionTable;
        private System.Windows.Forms.TextBox ClientIdBox;
    }
}

