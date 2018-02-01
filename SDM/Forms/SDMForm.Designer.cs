using System.Windows.Forms;

namespace SDM.Forms
{
    partial class SdmForm
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
            this.TopBarMenu = new System.Windows.Forms.Panel();
            this.sdmHeaderLabel = new System.Windows.Forms.Label();
            this.MinimizeButton = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Label();
            this.ExportTypeComboBox = new System.Windows.Forms.ComboBox();
            this.ExportMenuPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ImportTypeComboBox = new System.Windows.Forms.ComboBox();
            this.ImportPanel = new System.Windows.Forms.Panel();
            this.ExcelContentPanel = new System.Windows.Forms.DataGridView();
            this.importLabel = new System.Windows.Forms.Label();
            this.exportLabel = new System.Windows.Forms.Label();
            this.TopBarMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExcelContentPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // TopBarMenu
            // 
            this.TopBarMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TopBarMenu.Controls.Add(this.sdmHeaderLabel);
            this.TopBarMenu.Controls.Add(this.MinimizeButton);
            this.TopBarMenu.Controls.Add(this.closeButton);
            this.TopBarMenu.Location = new System.Drawing.Point(0, 0);
            this.TopBarMenu.Name = "TopBarMenu";
            this.TopBarMenu.Size = new System.Drawing.Size(1001, 24);
            this.TopBarMenu.TabIndex = 0;
            this.TopBarMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopBarMenu_MouseDown);
            // 
            // sdmHeaderLabel
            // 
            this.sdmHeaderLabel.AutoSize = true;
            this.sdmHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdmHeaderLabel.ForeColor = System.Drawing.Color.White;
            this.sdmHeaderLabel.Location = new System.Drawing.Point(414, 2);
            this.sdmHeaderLabel.Name = "sdmHeaderLabel";
            this.sdmHeaderLabel.Size = new System.Drawing.Size(118, 20);
            this.sdmHeaderLabel.TabIndex = 1;
            this.sdmHeaderLabel.Text = "SDM Converter";
            this.sdmHeaderLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopBarMenu_MouseDown);
            // 
            // MinimizeButton
            // 
            this.MinimizeButton.AutoSize = true;
            this.MinimizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimizeButton.ForeColor = System.Drawing.Color.White;
            this.MinimizeButton.Location = new System.Drawing.Point(950, -6);
            this.MinimizeButton.Name = "MinimizeButton";
            this.MinimizeButton.Size = new System.Drawing.Size(23, 31);
            this.MinimizeButton.TabIndex = 1;
            this.MinimizeButton.Text = "-";
            this.MinimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click);
            this.MinimizeButton.MouseEnter += new System.EventHandler(this.MouseEnterMinimizeButton);
            this.MinimizeButton.MouseLeave += new System.EventHandler(this.MouseLeaveMinimizeButton);
            // 
            // closeButton
            // 
            this.closeButton.AutoSize = true;
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(976, 2);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(20, 20);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "X";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            this.closeButton.MouseEnter += new System.EventHandler(this.MouseEnterCloseButton);
            this.closeButton.MouseLeave += new System.EventHandler(this.MouseLeaveCloseButton);
            // 
            // ExportTypeComboBox
            // 
            this.ExportTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExportTypeComboBox.FormattingEnabled = true;
            this.ExportTypeComboBox.Location = new System.Drawing.Point(206, 59);
            this.ExportTypeComboBox.Name = "ExportTypeComboBox";
            this.ExportTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.ExportTypeComboBox.TabIndex = 19;
            this.ExportTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.ExportTypeComboBox_SelectedIndexChanged);
            // 
            // ExportMenuPanel
            // 
            this.ExportMenuPanel.Location = new System.Drawing.Point(333, 57);
            this.ExportMenuPanel.Name = "ExportMenuPanel";
            this.ExportMenuPanel.Size = new System.Drawing.Size(655, 30);
            this.ExportMenuPanel.TabIndex = 20;
            // 
            // ImportTypeComboBox
            // 
            this.ImportTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ImportTypeComboBox.FormattingEnabled = true;
            this.ImportTypeComboBox.Location = new System.Drawing.Point(12, 59);
            this.ImportTypeComboBox.Name = "ImportTypeComboBox";
            this.ImportTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.ImportTypeComboBox.TabIndex = 22;
            this.ImportTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.ImportTypeComboBox_SelectedIndexChanged);
            // 
            // ImportPanel
            // 
            this.ImportPanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ImportPanel.Location = new System.Drawing.Point(12, 87);
            this.ImportPanel.Name = "ImportPanel";
            this.ImportPanel.Size = new System.Drawing.Size(188, 451);
            this.ImportPanel.TabIndex = 23;
            // 
            // ExcelContentPanel
            // 
            this.ExcelContentPanel.AllowUserToAddRows = false;
            this.ExcelContentPanel.AllowUserToDeleteRows = false;
            this.ExcelContentPanel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ExcelContentPanel.Location = new System.Drawing.Point(206, 86);
            this.ExcelContentPanel.Name = "ExcelContentPanel";
            this.ExcelContentPanel.Size = new System.Drawing.Size(782, 452);
            this.ExcelContentPanel.TabIndex = 24;
            // 
            // importLabel
            // 
            this.importLabel.AutoSize = true;
            this.importLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importLabel.Location = new System.Drawing.Point(7, 31);
            this.importLabel.Name = "importLabel";
            this.importLabel.Size = new System.Drawing.Size(71, 25);
            this.importLabel.TabIndex = 0;
            this.importLabel.Text = "Import";
            // 
            // exportLabel
            // 
            this.exportLabel.AutoSize = true;
            this.exportLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportLabel.Location = new System.Drawing.Point(201, 31);
            this.exportLabel.Name = "exportLabel";
            this.exportLabel.Size = new System.Drawing.Size(74, 25);
            this.exportLabel.TabIndex = 25;
            this.exportLabel.Text = "Export";
            // 
            // SdmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 550);
            this.Controls.Add(this.exportLabel);
            this.Controls.Add(this.importLabel);
            this.Controls.Add(this.ExcelContentPanel);
            this.Controls.Add(this.ImportPanel);
            this.Controls.Add(this.ImportTypeComboBox);
            this.Controls.Add(this.ExportMenuPanel);
            this.Controls.Add(this.ExportTypeComboBox);
            this.Controls.Add(this.TopBarMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SdmForm";
            this.Text = "SDM";
            this.TopBarMenu.ResumeLayout(false);
            this.TopBarMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExcelContentPanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel TopBarMenu;
        private System.Windows.Forms.Label MinimizeButton;
        private System.Windows.Forms.Label closeButton;
        private System.Windows.Forms.Label sdmHeaderLabel;
        private System.Windows.Forms.ComboBox ExportTypeComboBox;
        private System.Windows.Forms.FlowLayoutPanel ExportMenuPanel;
        private System.Windows.Forms.ComboBox ImportTypeComboBox;
        private System.Windows.Forms.Panel ImportPanel;
        private System.Windows.Forms.DataGridView ExcelContentPanel;
        private Label importLabel;
        private Label exportLabel;
    }
}