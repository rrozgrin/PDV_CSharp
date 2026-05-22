using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PDV.Application.Interfaces;
using PDV.Domain.Entities.Produtos;

namespace PDV.WinForms
{
    public partial class FrmBuscaProdutos : Form
    {
        public string? CodigoProdutoSelecionado { get; private set; }

        private readonly IProdutoVariacaoRepository _produtoVariacaoRepository;

        public FrmBuscaProdutos(IProdutoVariacaoRepository produtoVariacaoRepository, string termoInicial)
        {
            InitializeComponent();

            _produtoVariacaoRepository = produtoVariacaoRepository;

            dgvListaProdutos.AutoGenerateColumns = false;

            if (!string.IsNullOrWhiteSpace(termoInicial))
            {
                txtPesquisa.Text = termoInicial;
                ExecutarBusca();
            }

            txtPesquisa.KeyDown += txtPesquisa_KeyDown;
        }

        private void ExecutarBusca()
        {
            string termo = txtPesquisa.Text.Trim();

            if (string.IsNullOrWhiteSpace(termo))
            {
                dgvListaProdutos.DataSource = null;
                return;
            }

            try
            {
                var produtosEncontrados = _produtoVariacaoRepository.BuscarPorNome(termo)?.ToList();

                if (produtosEncontrados == null || !produtosEncontrados.Any())
                {
                    dgvListaProdutos.DataSource = null;
                    return;
                }

                dgvListaProdutos.DataSource = produtosEncontrados;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar produtos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvListaProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            CodigoProdutoSelecionado = dgvListaProdutos.Rows[e.RowIndex].Cells["colCod"].Value?.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void txtPesquisa_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ExecutarBusca();
                e.SuppressKeyPress = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Mantido apenas para não quebrar o evento gerado no Designer
        }
    }
}