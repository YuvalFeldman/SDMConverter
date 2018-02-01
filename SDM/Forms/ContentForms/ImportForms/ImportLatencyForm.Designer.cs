namespace SDM.Forms.ContentForms.ImportForms
{
    partial class ImportLatencyForm
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
            this.deleteSelectedButton = new System.Windows.Forms.Button();
            this.updateUsagesButton = new System.Windows.Forms.Button();
            this.FilesContentPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ImportLatencyConversionTableButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // deleteSelectedButton
            // 
            this.deleteSelectedButton.BackColor = System.Drawing.Color.Red;
            this.deleteSelectedButton.ForeColor = System.Drawing.Color.White;
            this.deleteSelectedButton.Location = new System.Drawing.Point(8, 416);
            this.deleteSelectedButton.Name = "deleteSelectedButton";
            this.deleteSelectedButton.Size = new System.Drawing.Size(172, 23);
            this.deleteSelectedButton.TabIndex = 16;
            this.deleteSelectedButton.Text = "Delete selected";
            this.deleteSelectedButton.UseVisualStyleBackColor = false;
            this.deleteSelectedButton.Click += new System.EventHandler(this.deleteSelectedButton_Click);
            // 
            // updateUsagesButton
            // 
            this.updateUsagesButton.Location = new System.Drawing.Point(8, 318);
            this.updateUsagesButton.Name = "updateUsagesButton";
            this.updateUsagesButton.Size = new System.Drawing.Size(172, 23);
            this.updateUsagesButton.TabIndex = 15;
            this.updateUsagesButton.Text = "Update usages";
            this.updateUsagesButton.UseVisualStyleBackColor = true;
            this.updateUsagesButton.Click += new System.EventHandler(this.updateUsagesButton_Click);
            // 
            // FilesContentPanel
            // 
            this.FilesContentPanel.AutoScroll = true;
            this.FilesContentPanel.BackColor = System.Drawing.SystemColors.Control;
            this.FilesContentPanel.Location = new System.Drawing.Point(11, 51);
            this.FilesContentPanel.Name = "FilesContentPanel";
            this.FilesContentPanel.Size = new System.Drawing.Size(164, 261);
            this.FilesContentPanel.TabIndex = 14;
            // 
            // ImportLatencyConversionTableButton
            // 
            this.ImportLatencyConversionTableButton.Location = new System.Drawing.Point(8, 12);
            this.ImportLatencyConversionTableButton.Name = "ImportLatencyConversionTableButton";
            this.ImportLatencyConversionTableButton.Size = new System.Drawing.Size(172, 23);
            this.ImportLatencyConversionTableButton.TabIndex = 13;
            this.ImportLatencyConversionTableButton.Text = "Import latency conversion table";
            this.ImportLatencyConversionTableButton.UseVisualStyleBackColor = true;
            this.ImportLatencyConversionTableButton.Click += new System.EventHandler(this.ImportLatencyConversionTableButton_Click);
            // 
            // ImportLatencyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(188, 451);
            this.Controls.Add(this.deleteSelectedButton);
            this.Controls.Add(this.updateUsagesButton);
            this.Controls.Add(this.FilesContentPanel);
            this.Controls.Add(this.ImportLatencyConversionTableButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ImportLatencyForm";
            this.Text = "ImportLatencyForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button deleteSelectedButton;
        public System.Windows.Forms.Button updateUsagesButton;
        private System.Windows.Forms.FlowLayoutPanel FilesContentPanel;
        private System.Windows.Forms.Button ImportLatencyConversionTableButton;
    }
}