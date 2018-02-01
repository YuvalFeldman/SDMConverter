using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SDM.Forms.ContentForms.ExportMenuForms
{
    public partial class SummedExportMenu : Form
    {
        private readonly BindingSource _summedTableOptions = new BindingSource();
        public List<string> SummedTables = new List<string>();
        public SummedExportMenu()
        {
            this.TopLevel = false;
            InitializeComponent();
        }

        public void UpdateSummedComboBoxOptions(List<string> summedTables)
        {
            try
            {
                SummedTables = summedTables;
                summedTablesComboBox.Controls.Clear();
                _summedTableOptions.DataSource = summedTables;
                summedTablesComboBox.DataSource = _summedTableOptions;
            }
            catch (Exception)
            {
                //wat
            }
        }
    }
}
