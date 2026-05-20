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
        private decimal _quantidadePendente = 1m;

        public FrmFrenteCaixaPDV()
        {
            InitializeComponent();

            // 1. Descobre a pasta onde o programa está sendo executado (geralmente a bin/Debug)
            string diretorioBase = AppDomain.CurrentDomain.BaseDirectory;

            // 2. Junta o caminho da pasta com o nome do arquivo do banco de dados
            string caminhoBanco = System.IO.Path.Combine(diretorioBase, "pdv.db");

            // 3. Monta a string de conexão corretamente
            string stringConexao = $"Data Source={caminhoBanco}";

            var connectionFactory = new SqliteConnectionFactory(stringConexao);
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

        private void ConsultarCodigoBarras(decimal quantidade)
        {
            var codigoBarras = txtCodigoBarras.Text.Trim();
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
            txtQuantidade.Text = quantidade.ToString("N3");

            var totalItem = variacao.PrecoVenda * quantidade;
            _totalVenda += totalItem;
            lblTotalGeral.Text = $"Total: R$ {_totalVenda:N2}";
            txtTotalItem.Text = totalItem.ToString("N2");
            nomeProduto = variacao.Descricao;

            var numeroItem = dgvItens.Rows.Count + 1;
            dgvItens.Rows.Add(
                numeroItem,
                variacao.CodigoBarras,
                nomeProduto,
                quantidade.ToString("N3"),
                variacao.PrecoVenda.ToString("N2"),
                totalItem.ToString("N2"));

            txtCodigoBarras.Clear();
            txtCodigoBarras.Focus();
        }


        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            ConsultarCodigoBarras(_quantidadePendente);
            _quantidadePendente = 1m;
            txtQuantidade.Text = "1";
        }

        private void txtCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            var texto = txtCodigoBarras.Text.Trim();

            // Ex.: "2x" ou "2*"
            if ((texto.EndsWith("x", StringComparison.OrdinalIgnoreCase) || texto.EndsWith("*")) &&
                decimal.TryParse(texto[..^1], out var qtd) && qtd > 0)
            {
                _quantidadePendente = qtd;
                txtQuantidade.Text = _quantidadePendente.ToString("N3");
                txtCodigoBarras.Clear();
                e.SuppressKeyPress = true;
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                ConsultarCodigoBarras(_quantidadePendente);
                _quantidadePendente = 1m;
                txtQuantidade.Text = "1";
                e.SuppressKeyPress = true;
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
