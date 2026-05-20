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

            txtCodigoBarras.TextChanged += txtCodigoBarras_TextChanged;
            string diretorioBase = AppDomain.CurrentDomain.BaseDirectory;
            string caminhoBanco = System.IO.Path.Combine(diretorioBase, "pdv.db");
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
            lblTotalGeral.Width = 500;

            dgvItens.Columns["colUnit"]!.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItens.Columns["colTotal"]!.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItens.Columns["colDescricao"]!.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvItens.Columns["colCodigo"]!.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

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
                variacao.PrecoVenda.ToString("C"),
                totalItem.ToString("C"));

            _quantidadePendente = 1m;
            txtQuantidade.Text = "1";

            txtCodigoBarras.Clear();
            txtCodigoBarras.Focus();
        }


        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            ConsultarCodigoBarras(_quantidadePendente);
        }

        private void txtCodigoBarras_TextChanged(object? sender, EventArgs e)
        {
            var texto = txtCodigoBarras.Text.Trim();

            if (texto.EndsWith("x", StringComparison.OrdinalIgnoreCase) || texto.EndsWith("*"))
            {
                string apenasONumero = texto[..^1];

                if (decimal.TryParse(apenasONumero, out var qtd) && qtd > 0)
                {
                    _quantidadePendente = qtd;
                    txtQuantidade.Text = _quantidadePendente.ToString("N3");
                    txtPrecoUnitario.Clear();
                    txtTotalItem.Clear();
                    txtCodigoBarras.Clear();
                }
            }
        }

        private void txtCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var texto = txtCodigoBarras.Text.Trim();
                if (string.IsNullOrWhiteSpace(texto)) return;

                e.SuppressKeyPress = true;

                if (long.TryParse(texto, out _))
                {
                    ConsultarCodigoBarras(_quantidadePendente);

                    //_quantidadePendente = 1m;
                    //txtQuantidade.Text = "1";
                }
                else
                {
                    // Se tiver letras, abre a tela de pesquisar produto pelo nome
                    // AbrirModalConsulta(texto); 
                }
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
