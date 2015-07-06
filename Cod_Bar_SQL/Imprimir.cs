using System;
using System.Windows.Forms;

namespace Cod_Bar_SQL
{
    public partial class Imprimir : Form
    {
        public Imprimir()
        {
            InitializeComponent();
        }

        private void Imprimir_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'Pesagens_LotesDataSet.Pesos_lote' table. You can move, or remove it, as needed.
            this.Pesos_loteTableAdapter.Fill(this.Pesagens_LotesDataSet.Pesos_lote);
            // TODO: This line of code loads data into the 'pesagens_Lotes.Pesos_lote' table. You can move, or remove it, as needed.

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
        }

        private void Pesos_loteBindingSource_CurrentChanged(object sender, EventArgs e)
        {
        }
    }
}