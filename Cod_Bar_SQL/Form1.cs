using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Cod_Bar_SQL
{
    ///Reformulação do software de leitura de codigos de barras com controlo automatico da informação inserida
    public partial class Form1 : Form
    {
        ///Reformulação do software de leitura de codigos de barras com controlo automatico da informação inserida
        public Form1()
        {
            ///Reformulação do software de leitura de codigos de barras com controlo automatico da informação inserida
            InitializeComponent();

            string conexaosql = "Data Source=pcdptinfo1;Initial Catalog=Pesagens_Lotes;Persist Security Info=True;User ID=sa;Password=clande";

            SqlConnection ligacao = new SqlConnection(conexaosql);


            ligacao.Open();
            SqlCommand eliminar = new SqlCommand("delete from pesos_lote", ligacao);
            eliminar.ExecuteNonQuery();
            ligacao.Close();
            ligacao.Dispose();
            tb_decim.Text = "2";

            /*Version v = Assembly.GetExecutingAssembly().GetName().Version;
            string About = string.Format(CultureInfo.InvariantCulture, @"YourApp Version {0}.{1}.}.{2}.(r{3})", v.Major, v.Minor, v.Build, v.Revision);*/
            //this.label3.Text = Application.ProductVersion.ToString();

            this.label3.Text = PublishVersion;
            ///Tentar definir a textbox para ler os carateres GS1, concretamente o 1D hex
            //this.tcodigo.ImeMode = Encoding.UTF8;
        }
        //Função para saber qual a versão que está nas propriedades ao efectuar o publish da Aplicação
        public string PublishVersion
        {
            get
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    Version ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    return "Versão " + string.Format("{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
                }
                else
                    return "Not Published";
            }
        }
        //Função para a interpretação do Lote em cada código de barras lido.
        public string BCLote(string texto_analisar, int poslote, int tamposlote, int tamlote)
        {
            string lote = "";
            if (poslote != 0)
            {
                for (int i = poslote + tamposlote; i < poslote + tamlote; i++)
                {
                    lote = lote + texto_analisar[i].ToString();
                }
            }
            else
            {
                lote = texto_analisar.ToString();
            }
            return lote;
        }
        //Função para calculo do peso em cada código de barras lido
        public double BCPeso(string texto_analisar, int decim, int pospeso, int tampeso, int tipoBC)
        {
            double pes = 0.0;
            int i;
            double j;
            if (tipoBC != 29)
            {
                switch (decim)
                {
                    case 0:
                        for (j = 0.0001, i = pospeso + tampeso; i < tcodigo.Text.Length; i++)
                        {
                            pes = pes + Convert.ToDouble(texto_analisar[i].ToString()) / j;
                            j = j * 10;
                        }
                        break;

                    case 1:
                        for (j = 0.001, i = pospeso + tampeso; i < tcodigo.Text.Length; i++)
                        {
                            pes = pes + Convert.ToDouble(texto_analisar[i].ToString()) / j;
                            j = j * 10;
                        }
                        break;

                    case 2:
                        for (j = 0.01, i = pospeso + tampeso; i < tcodigo.Text.Length; i++)
                        {
                            pes = pes + Convert.ToDouble(texto_analisar[i].ToString()) / j;
                            j = j * 10;
                        }
                        break;

                    case 3:
                        for (j = 0.1, i = pospeso + tampeso; i < tcodigo.Text.Length; i++)
                        {
                            pes = pes + Convert.ToDouble(texto_analisar[i].ToString()) / j;
                            j = j * 10;
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (decim)
                {
                    case 0:
                        for (j = 0.0001, i = pospeso + tampeso; i < tcodigo.Text.Length - 1; i++)
                        {
                            pes = pes + Convert.ToDouble(texto_analisar[i].ToString()) / j;
                            j = j * 10;
                        }
                        break;

                    case 1:
                        for (j = 0.001, i = pospeso + tampeso; i < tcodigo.Text.Length - 1; i++)
                        {
                            pes = pes + Convert.ToDouble(texto_analisar[i].ToString()) / j;
                            j = j * 10;
                        }
                        break;

                    case 2:
                        for (j = 0.01, i = pospeso + tampeso; i < tcodigo.Text.Length - 1; i++)
                        {
                            pes = pes + Convert.ToDouble(texto_analisar[i].ToString()) / j;
                            j = j * 10;
                        }
                        break;

                    case 3:
                        for (j = 0.1, i = pospeso + tampeso; i < tcodigo.Text.Length - 1; i++)
                        {
                            pes = pes + Convert.ToDouble(texto_analisar[i].ToString()) / j;
                            j = j * 10;
                        }
                        break;
                    default:
                        break;
                }
            }
            return pes;
        }

        private void tcodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                int poslote = 0, pospeso = 0;
                string tlote, tpeso;
                double pes = 0.0;

                //string lote = tcodigo.Text.ToString().Substring(5,2);
                int decim = 0;

                tlote = "";
                tpeso = "";

                //criar case para os vários tipos de Lotes e AI analisando o inicio da String

                switch (tcodigo.Text.Substring(tcodigo.Text.IndexOf("]C1") + 3, 2).Trim())
                {
                    case "10":
                        {
                            switch (tcodigo.Text.Substring(tcodigo.Text.IndexOf("]C1") + 5, 2).Trim())
                            {
                                case "DS":
                                    {
                                        //para códigos ucc DS
                                        poslote = tcodigo.Text.IndexOf("DS");
                                        tlote = BCLote(tcodigo.Text.ToString(), poslote + 3, 2, 7);
                                        pospeso = tcodigo.Text.IndexOf("{310");

                                        //verifica o nº de casas decimais definidas no ecran principal.
                                        decim = int.Parse(tcodigo.Text.Substring(tcodigo.Text.IndexOf("{310") + 4, 1));
                                        pes = BCPeso(tcodigo.Text.ToString(), decim, pospeso, 6, 0);
                                    }
                                    break;

                                case "C0":
                                    {
                                        //para códigos ucc C0
                                        poslote = tcodigo.Text.IndexOf("]C110C");
                                        tlote = BCLote(tcodigo.Text.ToString(), poslote + 3, 2, 10);
                                        pospeso = tcodigo.Text.IndexOf("{310");

                                        //verifica o nº de casas decimais definidas no ecran principal.
                                        decim = int.Parse(tcodigo.Text.Substring(tcodigo.Text.IndexOf("{310") + 4, 1));
                                        pes = BCPeso(tcodigo.Text.ToString(), decim, pospeso, 6, 0);
                                    }
                                    break;

                                case "C1":
                                    {
                                        //para códigos ucc C1
                                        poslote = tcodigo.Text.IndexOf("]C110C");
                                        tlote = BCLote(tcodigo.Text.ToString(), poslote + 3, 2, 10);
                                        pospeso = tcodigo.Text.IndexOf("{310");

                                        //verifica o nº de casas decimais definidas no ecran principal.
                                        decim = int.Parse(tcodigo.Text.Substring(tcodigo.Text.IndexOf("{310") + 4, 1));
                                        pes = BCPeso(tcodigo.Text.ToString(), decim, pospeso, 6, 0);
                                    }
                                    break;

                                case "CG":
                                    {
                                        //para códigos ucc CG
                                        poslote = tcodigo.Text.IndexOf("]C110C");
                                        tlote = BCLote(tcodigo.Text.ToString(), poslote + 3, 2, 10);
                                        pospeso = tcodigo.Text.IndexOf("{310");

                                        //verifica o nº de casas decimais definidas no ecran principal.
                                        decim = int.Parse(tcodigo.Text.Substring(tcodigo.Text.IndexOf("{310") + 4, 1));
                                        pes = BCPeso(tcodigo.Text.ToString(), decim, pospeso, 6, 0);
                                    }
                                    break;

                                case "CB":
                                    {
                                        //para códigos ucc CB
                                        poslote = tcodigo.Text.IndexOf("]C110C");
                                        tlote = BCLote(tcodigo.Text.ToString(), poslote + 3, 2, 10);
                                        pospeso = tcodigo.Text.IndexOf("{310");

                                        //verifica o nº de casas decimais definidas no ecran principal.
                                        decim = int.Parse(tcodigo.Text.Substring(tcodigo.Text.IndexOf("{310") + 4, 1));
                                        pes = BCPeso(tcodigo.Text.ToString(), decim, pospeso, 6, 0);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;

                    case "01":
                        {
                            switch (tcodigo.Text.Substring(tcodigo.Text.IndexOf("{10") + 3, 2).Trim())
                            {
                                case "C0":
                                    {
                                        //para códigos ucc C0
                                        poslote = tcodigo.Text.IndexOf("{10C");
                                        tlote = BCLote(tcodigo.Text.ToString(), poslote, 3, 10);
                                        pospeso = tcodigo.Text.IndexOf("{310");

                                        //verifica o nº de casas decimais definidas no ecran principal.
                                        decim = int.Parse(tcodigo.Text.Substring(tcodigo.Text.IndexOf("{310") + 4, 1));
                                        pes = BCPeso(tcodigo.Text.ToString(), decim, pospeso, 6, 0);
                                    }
                                    break;

                                case "C1":
                                    {
                                        //para códigos ucc C1
                                        poslote = tcodigo.Text.IndexOf("{10C");
                                        tlote = BCLote(tcodigo.Text.ToString(), poslote, 3, 10);
                                        pospeso = tcodigo.Text.IndexOf("{310");

                                        //verifica o nº de casas decimais definidas no ecran principal.
                                        decim = int.Parse(tcodigo.Text.Substring(tcodigo.Text.IndexOf("{310") + 4, 1));
                                        pes = BCPeso(tcodigo.Text.ToString(), decim, pospeso, 6, 0);
                                    }
                                    break;

                                case "CG":
                                    {
                                        //para códigos ucc CG
                                        poslote = tcodigo.Text.IndexOf("{10C");
                                        tlote = BCLote(tcodigo.Text.ToString(), poslote, 3, 10);
                                        pospeso = tcodigo.Text.IndexOf("{310");

                                        //verifica o nº de casas decimais definidas no ecran principal.
                                        decim = int.Parse(tcodigo.Text.Substring(tcodigo.Text.IndexOf("{310") + 4, 1));
                                        pes = BCPeso(tcodigo.Text.ToString(), decim, pospeso, 6, 0);
                                    }
                                    break;

                                case "CB":
                                    {
                                        //para códigos ucc CB
                                        poslote = tcodigo.Text.IndexOf("{10C");
                                        tlote = BCLote(tcodigo.Text.ToString(), poslote, 3, 10);
                                        pospeso = tcodigo.Text.IndexOf("{310");

                                        //verifica o nº de casas decimais definidas no ecran principal.
                                        decim = int.Parse(tcodigo.Text.Substring(tcodigo.Text.IndexOf("{310") + 4, 1));
                                        pes = BCPeso(tcodigo.Text.ToString(), decim, pospeso, 6, 0);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
                switch (tcodigo.Text.Substring(0, 2).Trim())
                {
                    case "29":
                        {
                            //tlote = tBlote.Text.ToString();
                            tlote = BCLote(tBlote.Text.ToString(), 0, 0, 0);

                            //pospeso = 7;
                            //verifica o nº de casas decimais definidas no ecran principal, agora é com o campo tb_decim
                            decim = int.Parse(tb_decim.Text.ToString());
                            pes = BCPeso(tcodigo.Text.ToString(), decim, 7, 0, 29);
                        }
                        break;
                    default:
                        break;
                }
                switch (tcodigo.Text.Substring(0, 1).Trim())
                {
                    case "{":

                        switch (tcodigo.Text.Substring(tcodigo.Text.IndexOf("{10") + 3, 2).Trim())
                        {
                            case "DS":
                                {
                                    //para códigos ucc DS
                                    poslote = tcodigo.Text.IndexOf("DS");
                                    tlote = BCLote(tcodigo.Text.ToString(), poslote, 0, 7);
                                    pospeso = tcodigo.Text.IndexOf("{310");

                                    //verifica o nº de casas decimais definidas no ecran principal.
                                    decim = int.Parse(tcodigo.Text.Substring(tcodigo.Text.IndexOf("{310") + 4, 1));
                                    pes = BCPeso(tcodigo.Text.ToString(), decim, pospeso, 6, 0);
                                }
                                break;

                            case "C0":
                                {
                                    //para códigos ucc C0
                                    poslote = tcodigo.Text.IndexOf("{10C");
                                    tlote = BCLote(tcodigo.Text.ToString(), poslote + 3, 0, 7);
                                    pospeso = tcodigo.Text.IndexOf("{310");

                                    //verifica o nº de casas decimais definidas no ecran principal.
                                    decim = int.Parse(tcodigo.Text.Substring(tcodigo.Text.IndexOf("{310") + 4, 1));
                                    pes = BCPeso(tcodigo.Text.ToString(), decim, pospeso, 6, 0);
                                }
                                break;

                            case "C1":
                                {
                                    //para códigos ucc C1
                                    poslote = tcodigo.Text.IndexOf("{10C");
                                    tlote = BCLote(tcodigo.Text.ToString(), poslote + 3, 0, 7);
                                    pospeso = tcodigo.Text.IndexOf("{310");

                                    //verifica o nº de casas decimais definidas no ecran principal.
                                    decim = int.Parse(tcodigo.Text.Substring(tcodigo.Text.IndexOf("{310") + 4, 1));
                                    pes = BCPeso(tcodigo.Text.ToString(), decim, pospeso, 6, 0);
                                }
                                break;

                            case "CG":
                                {
                                    //para códigos ucc CG
                                    poslote = tcodigo.Text.IndexOf("{10C");
                                    tlote = BCLote(tcodigo.Text.ToString(), poslote + 3, 0, 7);
                                    pospeso = tcodigo.Text.IndexOf("{310");

                                    //verifica o nº de casas decimais definidas no ecran principal.
                                    decim = int.Parse(tcodigo.Text.Substring(tcodigo.Text.IndexOf("{310") + 4, 1));
                                    pes = BCPeso(tcodigo.Text.ToString(), decim, pospeso, 6, 0);
                                }
                                break;

                            case "CB":
                                {
                                    //para códigos ucc CG
                                    poslote = tcodigo.Text.IndexOf("{10CB");
                                    tlote = BCLote(tcodigo.Text.ToString(), poslote + 3, 0, 8);
                                    pospeso = tcodigo.Text.IndexOf("{310");

                                    //verifica o nº de casas decimais definidas no ecran principal.
                                    decim = int.Parse(tcodigo.Text.Substring(tcodigo.Text.IndexOf("{310") + 4, 1));
                                    pes = BCPeso(tcodigo.Text.ToString(), decim, pospeso, 6, 0);
                                }
                                break;
                            case "DB":
                                {
                                    //para códigos ucc CG
                                    poslote = tcodigo.Text.IndexOf("{10DB");
                                    tlote = BCLote(tcodigo.Text.ToString(), poslote + 3, 0, 8);
                                    pospeso = tcodigo.Text.IndexOf("{310");

                                    //verifica o nº de casas decimais definidas no ecran principal.
                                    decim = int.Parse(tcodigo.Text.Substring(tcodigo.Text.IndexOf("{310") + 4, 1));
                                    pes = BCPeso(tcodigo.Text.ToString(), decim, pospeso, 6, 0);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                }

                tpeso = pes.ToString();

                string conexaosql = "Data Source=pcdptinfo1;Initial Catalog=Pesagens_Lotes;Persist Security Info=True;User ID=sa;Password=clande";

                SqlConnection ligacao = new SqlConnection(conexaosql);

                ligacao.Open();
                SqlCommand inserção = new SqlCommand("insert into Pesos_lote (id,lote,peso,data) values((Select isnull(Max(id),0)+1 as id from pesos_lote (nolock)),'" + 
                    tlote.ToString() + "','" + tpeso.ToString() + "',convert(char(10),getdate(),112))", ligacao);
                inserção.ExecuteNonQuery();
                ligacao.Close();
                ligacao.Dispose();

                //this.pesos_loteTableAdapter.Fill(this.pesagens_Lotes1.Pesos_lote);
                this.pesos_loteTableAdapter.Fill(this.pesagens_LotesDataSet.Pesos_lote);

                tpesagens.Text = (dataGridView1.RowCount - 1).ToString();
                tcodigo.Text = "";
                tcodigo.Focus();
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            double total = 0.0;
            tpesagens.Text = (dataGridView1.RowCount - 1).ToString();

            foreach (DataGridViewRow dataview in dataGridView1.Rows)
            {
                total += Convert.ToDouble(dataview.Cells[2].Value);
            }

            ttotal.Text = total.ToString();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            double total = 0.0;

            tpesagens.Text = (dataGridView1.RowCount - 1).ToString();

            foreach (DataGridViewRow dataview in dataGridView1.Rows)
            {
                total += Convert.ToDouble(dataview.Cells[2].Value);
            }

            ttotal.Text = total.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pesagens_LotesDataSet.Pesos_lote' table. You can move, or remove it, as needed.
            this.pesos_loteTableAdapter.Fill(this.pesagens_LotesDataSet.Pesos_lote);  
         
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Form imp = new Imprimir();
            imp.Show();
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string conexaosql = "Data Source=pcdptinfo1;Initial Catalog=Pesagens_Lotes;Persist Security Info=True;User ID=sa;Password=clande";

            SqlConnection ligacao = new SqlConnection(conexaosql);

            ligacao.Open();

            int selecciona = e.Row.Index;

            int elimina = int.Parse(e.Row.Cells[0].Value.ToString());

            SqlCommand eliminar = new SqlCommand("delete from pesos_lote where id=" + elimina.ToString(), ligacao);
            eliminar.ExecuteNonQuery();
            ligacao.Close();
            ligacao.Dispose();
        }

        private void pesosloteBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                Form detail = new Detalhe();
                detail.Show();
            }
        }
    }
}