using System.Collections.Generic;
using System.Windows.Forms;

namespace SDM.Forms.ContentForms.ExportMenuForms
{
    public partial class SummedExportMenu : Form
    {
        private readonly BindingSource _summedTableOptions = new BindingSource();

        public SummedExportMenu()
        {
            this.TopLevel = false;
            InitializeComponent();
        }

        public void UpdateSummedComboBoxOptions(List<string> summedTables)
        {
            summedTablesComboBox.Controls.Clear();
            _summedTableOptions.DataSource = summedTables;
            summedTablesComboBox.DataSource = _summedTableOptions;
        }
    }
}
