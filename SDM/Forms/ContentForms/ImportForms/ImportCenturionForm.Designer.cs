namespace SDM.Forms.ContentForms.ImportForms
{
    partial class ImportCenturionForm
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
            this.ImportCenturionButton = new System.Windows.Forms.Button();
            this.FilesContentPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.updateUsagesButton = new System.Windows.Forms.Button();
            this.deleteSelectedButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ImportCenturionButton
            // 
            this.ImportCenturionButton.Location = new System.Drawing.Point(9, 12);
            this.ImportCenturionButton.Name = "ImportCenturionButton";
            this.ImportCenturionButton.Size = new System.Drawing.Size(172, 23);
            this.ImportCenturionButton.TabIndex = 9;
            this.ImportCenturionButton.Text = "Import Centurion report";
            this.ImportCenturionButton.UseVisualStyleBackColor = true;
            this.ImportCenturionButton.Click += new System.EventHandler(this.ImportCenturionButton_Click);
            // 
            // FilesContentPanel
            // 
            this.FilesContentPanel.AutoScroll = true;
            this.FilesContentPanel.BackColor = System.Drawing.SystemColors.Control;
            this.FilesContentPanel.Location = new System.Drawing.Point(12, 51);
            this.FilesContentPanel.Name = "FilesContentPanel";
            this.FilesContentPanel.Size = new System.Drawing.Size(164, 261);
            this.FilesContentPanel.TabIndex = 10;
            // 
            // updateUsagesButton
            // 
            this.updateUsagesButton.Location = new System.Drawing.Point(9, 318);
            this.updateUsagesButton.Name = "updateUsagesButton";
            this.updateUsagesButton.Size = new System.Drawing.Size(172, 23);
            this.updateUsagesButton.TabIndex = 11;
            this.updateUsagesButton.Text = "Update usages";
            this.updateUsagesButton.UseVisualStyleBackColor = true;
            // 
            // deleteSelectedButton
            // 
            this.deleteSelectedButton.BackColor = System.Drawing.Color.DarkRed;
            this.deleteSelectedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteSelectedButton.ForeColor = System.Drawing.Color.White;
            this.deleteSelectedButton.Location = new System.Drawing.Point(9, 416);
            this.deleteSelectedButton.Name = "deleteSelectedButton";
            this.deleteSelectedButton.Size = new System.Drawing.Size(172, 23);
            this.deleteSelectedButton.TabIndex = 12;
            this.deleteSelectedButton.Text = "Delete selected";
            this.deleteSelectedButton.UseVisualStyleBackColor = false;
            this.deleteSelectedButton.Click += new System.EventHandler(this.deleteSelectedButton_Click);
            // 
            // ImportCenturionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(188, 451);
            this.Controls.Add(this.deleteSelectedButton);
            this.Controls.Add(this.updateUsagesButton);
            this.Controls.Add(this.FilesContentPanel);
            this.Controls.Add(this.ImportCenturionButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ImportCenturionForm";
            this.Text = "ImportCenturionForm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ImportCenturionButton;
        private System.Windows.Forms.FlowLayoutPanel FilesContentPanel;
        public System.Windows.Forms.Button updateUsagesButton;
        private System.Windows.Forms.Button deleteSelectedButton;
    }
}