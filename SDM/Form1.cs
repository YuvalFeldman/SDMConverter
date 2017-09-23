using System.Windows.Forms;
using SDM.SDM;

namespace SDM
{
    public partial class Form1 : Form
    {
        private readonly ISDM _sdm;

        public Form1(ISDM sdm)
        {
            _sdm = sdm;
            InitializeComponent();
        }

        private void centurionImport_Click(object sender, System.EventArgs e)
        {
            _sdm.ImportcenturionReport();
        }

        private void ImportClient_Click(object sender, System.EventArgs e)
        {
            _sdm.ImportClientReport(ClientIdBox.Text);
        }

        private void ExportFullDb_Click(object sender, System.EventArgs e)
        {
            _sdm.ExportFullDebtReport();
        }

        private void ExportSummedDb_Click(object sender, System.EventArgs e)
        {
            _sdm.ExportSummedDebtReport();
        }

        private void DeleteCenturion_Click(object sender, System.EventArgs e)
        {
            _sdm.DeleteCenturionReport();
        }

        private void DeleteClientData_Click(object sender, System.EventArgs e)
        {
            _sdm.DeleteClientReport();
        }

        private void LatencyConversionTable_Click(object sender, System.EventArgs e)
        {
            _sdm.SetLatencyConversionTable();
        }

        private void ClientIdBox_TextChanged(object sender, System.EventArgs e)
        {

        }
    }
}
