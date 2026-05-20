using System.Windows.Forms;
using PDV.Application.Interfaces;
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
            colCod = new DataGridViewTextBoxColumn();
            colDescricao = new DataGridViewTextBoxColumn();
            colUnit = new DataGridViewTextBoxColumn();
            colQtdEstoque = new DataGridViewTextBoxColumn();
            produtoVariacaoRepositoryBindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dgvListaProdutos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)produtoVariacaoRepositoryBindingSource).BeginInit();
            SuspendLayout();
            // 
            // txtPesquisa
            // 
            txtPesquisa.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPesquisa.Location = new Point(166, 22);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(266, 23);
            txtPesquisa.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 25);
            label1.Name = "label1";
            label1.Size = new Size(145, 15);
            label1.TabIndex = 1;
            label1.Text = "Digite o nome do produto";
            // 
            // dgvListaProdutos
            // 
            dgvListaProdutos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvListaProdutos.AutoGenerateColumns = false;
            dgvListaProdutos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvListaProdutos.BackgroundColor = SystemColors.ButtonFace;
            dgvListaProdutos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListaProdutos.Columns.AddRange(new DataGridViewColumn[] { colCod, colDescricao, colUnit, colQtdEstoque });
            dgvListaProdutos.DataSource = produtoVariacaoRepositoryBindingSource;
            dgvListaProdutos.Location = new Point(19, 76);
            dgvListaProdutos.Name = "dgvListaProdutos";
            dgvListaProdutos.Size = new Size(653, 261);
            dgvListaProdutos.TabIndex = 2;
            dgvListaProdutos.CellClick += dgvListaProdutos_CellClick;
            dgvListaProdutos.CellContentClick += dataGridView1_CellContentClick;
            // 
            // colCod
            // 
            colCod.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colCod.HeaderText = "Código de Barras";
            colCod.Name = "colCod";
            colCod.Width = 150;
            // 
            // colDescricao
            // 
            colDescricao.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDescricao.HeaderText = "Nome/Descrição do Produto";
            colDescricao.Name = "colDescricao";
            colDescricao.Width = 300;
            // 
            // colUnit
            // 
            colUnit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colUnit.HeaderText = "Preço Unitário";
            colUnit.Name = "colUnit";
            colUnit.Width = 80;
            // 
            // colQtdEstoque
            // 
            colQtdEstoque.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colQtdEstoque.HeaderText = "Qtde. Estoque";
            colQtdEstoque.Name = "colQtdEstoque";
            colQtdEstoque.Width = 80;
            // Vinculação de propriedades às colunas
            colCod.DataPropertyName = "CodigoBarras";
            colDescricao.DataPropertyName = "Descricao";
            colUnit.DataPropertyName = "PrecoVenda";
            colQtdEstoque.DataPropertyName = "EstoqueAtual";
            // 
        // produtoVariacaoRepositoryBindingSource
        // 
        // O DataSource deve apontar para o tipo de entidade exibida no grid, não para o repositório
        produtoVariacaoRepositoryBindingSource.DataSource = typeof(PDV.Domain.Entities.Produtos.ProdutoVariacao);
            // 
            // FrmBuscaProdutos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(686, 406);
            Controls.Add(dgvListaProdutos);
            Controls.Add(label1);
            Controls.Add(txtPesquisa);
            Name = "FrmBuscaProdutos";
            Text = "FrmBuscaProdutos";
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
        private DataGridViewTextBoxColumn colCod;
        private DataGridViewTextBoxColumn colDescricao;
        private DataGridViewTextBoxColumn colUnit;
        private DataGridViewTextBoxColumn colQtdEstoque;
    }
}

partial class FrmBuscaProdutos : Form
{
    public string? CodigoProdutoSelecionado { get; private set; }

    private readonly IProdutoVariacaoRepository _produtoVariacaoRepository;

    public FrmBuscaProdutos(IProdutoVariacaoRepository produtoVariacaoRepository, string termoInicial)
    {
        //InitializeComponent();

        _produtoVariacaoRepository = produtoVariacaoRepository;

        //if (!string.IsNullOrEmpty(termoInicial))
        //{
        //    //txtPesquisa.Text = termoInicial;
        //    ExecutarBusca();
        //}
    }

    //private void ExecutarBusca()
    //{
    //    //string termo = txtPesquisa.Text.Trim();

    //    if (string.IsNullOrWhiteSpace(termo))
    //    {
    //        //produtoVariacaoRepositoryBindingSource.DataSource = null;
    //        return;
    //    }

    //    try
    //    {
    //        //var produtosEncontrados = _produtoVariacaoRepository.BuscarPorNome(termo)?.ToList();

    //        if (produtosEncontrados == null || !produtosEncontrados.Any())
    //        {
    //            //produtoVariacaoRepositoryBindingSource.DataSource = null;
    //            return;
    //        }

    //        //produtoVariacaoRepositoryBindingSource.DataSource = produtosEncontrados;
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show($"Erro ao buscar produtos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //    }
    //}

    //private void dgvListaProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
    //{
    //    if (e.RowIndex >= 0)
    //    {
    //        CodigoProdutoSelecionado = dgvListaProdutos.Rows[e.RowIndex].Cells[0].Value?.ToString();

    //        this.DialogResult = DialogResult.OK;
    //        this.Close();
    //    }
    //}

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    //private void txtPesquisa_KeyDown(object sender, KeyEventArgs e)
    //{
    //    if (e.KeyCode == Keys.Enter)
    //    {
    //        ExecutarBusca();
    //        e.SuppressKeyPress = true;
    //    }
    //}
}