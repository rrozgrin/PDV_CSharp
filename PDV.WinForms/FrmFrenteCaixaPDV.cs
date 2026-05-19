using PDV.Application.Interfaces;
using PDV.Infrastructure.Database;
using PDV.Infrastructure.Repositories.Produtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace PDV.WinForms
{
    public partial class FrmFrenteCaixaPDV : Form
    {
        private readonly ProdutoVariacaoRepository _produtoVariacaoRepository;
        private decimal _totalVenda = 0m;

        public FrmFrenteCaixaPDV()
        {
            InitializeComponent();

            var connectionFactory = new SqliteConnectionFactory(@"Data Source=C:\Users\dev_php\source\repos\PDV_CSharp\pdv.db");
            _produtoVariacaoRepository = new ProdutoVariacaoRepository(connectionFactory);
        }

        private void FrmFrenteCaixaPDV_Load(object sender, EventArgs e)
        {
            // Garantir comportamento estável do label de total
            lblTotalGeral.AutoSize = false;
            lblTotalGeral.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblTotalGeral.TextAlign = ContentAlignment.MiddleRight;
            lblTotalGeral.Width = 300; // ajuste conforme necessário

            // Fixar posição X relativa ao form para evitar deslocamento quando outros controles mudam
            lblTotalGeral.Left = this.ClientSize.Width - lblTotalGeral.Width - 20; // 20px de margem

            // Ouvir redimensionamento do form para manter posição fixa
            this.ClientSizeChanged += (s, ev) =>
            {
                lblTotalGeral.Left = this.ClientSize.Width - lblTotalGeral.Width - 20;
            };

            txtCodigoBarras.Focus();
        }

        private void FrmFrenteCaixaPDV_Leave(object sender, EventArgs e)
        {

        }

        private void ConsultarCodigoBarras()
        {
            var codigoBarras = txtCodigoBarras.Text.Trim();
            var quantidade = 1; // Para simplificar, sempre consideramos quantidade 1. Você pode expandir isso para permitir que o usuário informe a quantidade.
            var nomeProduto = string.Empty;

            if (string.IsNullOrWhiteSpace(codigoBarras))
            {
                MessageBox.Show("Informe o codigo de barras.");
                txtCodigoBarras.Focus();
                return;
            }

            var variacao = _produtoVariacaoRepository.BuscarPorCodigoBarras(codigoBarras);

            if (variacao is null)
            {
                MessageBox.Show("Produto nao encontrado.");
                txtCodigoBarras.SelectAll();
                txtCodigoBarras.Focus();
                return;
            }

            txtPrecoUnitario.Text = variacao.PrecoVenda.ToString("N2");
            txtQuantidade.Text = "1";

            var totalItem = variacao.PrecoVenda;
            _totalVenda += totalItem;
            lblTotalGeral.Text = $"Total: R$ {_totalVenda:N2}";
            txtTotalItem.Text = totalItem.ToString("N2");
            nomeProduto = variacao.Descricao;

            dgvItens.Rows.Add(
                variacao.CodigoBarras,
                nomeProduto,
                quantidade,
                variacao.PrecoVenda.ToString("N2"),
                totalItem.ToString("N2"));

            txtCodigoBarras.Clear();
            txtCodigoBarras.Focus();
        }


        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            ConsultarCodigoBarras();
        }

        private void txtCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConsultarCodigoBarras();
            }
        }

        private void lblTotalItem_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalGeral_Click(object sender, EventArgs e)
        {

        }
    }
}
