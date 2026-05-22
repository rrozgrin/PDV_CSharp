using System.Windows.Forms;

namespace PDV.WinForms
{
    partial class FrmBuscaProdutos
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
            components = new System.ComponentModel.Container();
            txtPesquisa = new TextBox();
            label1 = new Label();
            dgvListaProdutos = new DataGridView();

            colId = new DataGridViewTextBoxColumn();
            colProdutoId = new DataGridViewTextBoxColumn();
            colCod = new DataGridViewTextBoxColumn();
            colDescricao = new DataGridViewTextBoxColumn();
            colUnit = new DataGridViewTextBoxColumn();
            colQtdEstoque = new DataGridViewTextBoxColumn();

            produtoVariacaoRepositoryBindingSource = new BindingSource(components);

            ((System.ComponentModel.ISupportInitialize)dgvListaProdutos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)produtoVariacaoRepositoryBindingSource).BeginInit();
            SuspendLayout();

            // txtPesquisa
            txtPesquisa.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPesquisa.Location = new System.Drawing.Point(166, 22);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new System.Drawing.Size(266, 23);
            txtPesquisa.TabIndex = 0;

            // label1
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(15, 25);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(145, 15);
            label1.TabIndex = 1;
            label1.Text = "Digite o nome do produto";

            // dgvListaProdutos
            dgvListaProdutos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvListaProdutos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvListaProdutos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dgvListaProdutos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListaProdutos.Location = new System.Drawing.Point(19, 76);
            dgvListaProdutos.Name = "dgvListaProdutos";
            dgvListaProdutos.Size = new System.Drawing.Size(653, 261);
            dgvListaProdutos.TabIndex = 2;
            dgvListaProdutos.CellClick += dgvListaProdutos_CellClick;
            dgvListaProdutos.CellContentClick += dataGridView1_CellContentClick;

            // colId
            colId.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colId.DataPropertyName = "Id";
            colId.HeaderText = "Id";
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Visible = false;

            // colProdutoId
            colProdutoId.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colProdutoId.DataPropertyName = "ProdutoId";
            colProdutoId.HeaderText = "ProdutoId";
            colProdutoId.Name = "colProdutoId";
            colProdutoId.ReadOnly = true;
            colProdutoId.Visible = false;

            // colCod
            colCod.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colCod.DataPropertyName = "CodigoBarras";
            colCod.HeaderText = "Código de Barras";
            colCod.Name = "colCod";
            colCod.ReadOnly = true;
            colCod.Width = 150;

            // colDescricao
            colDescricao.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDescricao.DataPropertyName = "Descricao";
            colDescricao.HeaderText = "Nome/Descrição do Produto";
            colDescricao.Name = "colDescricao";
            colDescricao.ReadOnly = true;
            colDescricao.Width = 300;

            // colUnit
            colUnit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colUnit.DataPropertyName = "PrecoVenda";
            colUnit.HeaderText = "Preço Unitário";
            colUnit.Name = "colUnit";
            colUnit.ReadOnly = true;
            colUnit.Width = 80;

            // colQtdEstoque
            colQtdEstoque.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colQtdEstoque.DataPropertyName = "EstoqueAtual";
            colQtdEstoque.HeaderText = "Qtde. Estoque";
            colQtdEstoque.Name = "colQtdEstoque";
            colQtdEstoque.ReadOnly = true;
            colQtdEstoque.Width = 80;

            dgvListaProdutos.Columns.AddRange(new DataGridViewColumn[]
            {
                colId, colProdutoId, colCod, colDescricao, colUnit, colQtdEstoque
            });

            // produtoVariacaoRepositoryBindingSource
            produtoVariacaoRepositoryBindingSource.DataSource = typeof(PDV.Domain.Entities.Produtos.ProdutoVariacao);

            // FrmBuscaProdutos
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Info;
            ClientSize = new System.Drawing.Size(686, 406);
            Controls.Add(dgvListaProdutos);
            Controls.Add(label1);
            Controls.Add(txtPesquisa);
            Name = "FrmBuscaProdutos";
            Text = "Busca de Produtos";

            ((System.ComponentModel.ISupportInitialize)dgvListaProdutos).EndInit();
            ((System.ComponentModel.ISupportInitialize)produtoVariacaoRepositoryBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPesquisa;
        private Label label1;
        private DataGridView dgvListaProdutos;
        private BindingSource produtoVariacaoRepositoryBindingSource;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colProdutoId;
        private DataGridViewTextBoxColumn colCod;
        private DataGridViewTextBoxColumn colDescricao;
        private DataGridViewTextBoxColumn colUnit;
        private DataGridViewTextBoxColumn colQtdEstoque;
    }
}