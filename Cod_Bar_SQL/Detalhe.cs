using System;
using System.Windows.Forms;

namespace Cod_Bar_SQL
{
    public partial class Detalhe : Form
    {
        public Detalhe()
        {
            InitializeComponent();
        }

        private void Detalhe_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'Pesagens_LotesDataSet.Pesos_lote' table. You can move, or remove it, as needed.
            this.Pesos_loteTableAdapter.Fill(this.Pesagens_LotesDataSet.Pesos_lote);
           //reportViewer1.
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
        }

        private void Pesos_loteBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load_1(object sender, EventArgs e)
        {

        }
    }
}