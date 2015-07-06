namespace Cod_Bar_SQL
{
    partial class Detalhe
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.Pesos_loteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Pesagens_LotesDataSet = new Cod_Bar_SQL.Pesagens_LotesDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Pesos_loteTableAdapter = new Cod_Bar_SQL.Pesagens_LotesDataSetTableAdapters.Pesos_loteTableAdapter();
            this.Pesagens_LotesDataSet1 = new Cod_Bar_SQL.Pesagens_LotesDataSet1();
            ((System.ComponentModel.ISupportInitialize)(this.Pesos_loteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pesagens_LotesDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pesagens_LotesDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // Pesos_loteBindingSource
            // 
            this.Pesos_loteBindingSource.DataMember = "Pesos_lote";
            this.Pesos_loteBindingSource.DataSource = this.Pesagens_LotesDataSet;
            this.Pesos_loteBindingSource.CurrentChanged += new System.EventHandler(this.Pesos_loteBindingSource_CurrentChanged);
            // 
            // Pesagens_LotesDataSet
            // 
            this.Pesagens_LotesDataSet.DataSetName = "Pesagens_LotesDataSet";
            this.Pesagens_LotesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.AutoSize = true;
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.DocumentMapWidth = 34;
            reportDataSource1.Name = "Lotes_Detalhe";
            reportDataSource1.Value = this.Pesos_loteBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Cod_Bar_SQL.Lotes_pesos_Detalhe.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(617, 420);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load_1);
            // 
            // Pesos_loteTableAdapter
            // 
            this.Pesos_loteTableAdapter.ClearBeforeFill = true;
            // 
            // Pesagens_LotesDataSet1
            // 
            this.Pesagens_LotesDataSet1.DataSetName = "Pesagens_LotesDataSet1";
            this.Pesagens_LotesDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Detalhe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 420);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Detalhe";
            this.Text = "Imprimir Detalhe";
            this.Load += new System.EventHandler(this.Detalhe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Pesos_loteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pesagens_LotesDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pesagens_LotesDataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource Pesos_loteBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Pesagens_LotesDataSet Pesagens_LotesDataSet;
        private Pesagens_LotesDataSetTableAdapters.Pesos_loteTableAdapter Pesos_loteTableAdapter;
        private Pesagens_LotesDataSet1 Pesagens_LotesDataSet1;

    }
}