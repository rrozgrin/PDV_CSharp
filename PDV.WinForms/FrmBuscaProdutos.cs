using PDV.Application.Interfaces;
using PDV.Infrastructure.Repositories.Produtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PDV.Application.Interfaces;

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

            if (!string.IsNullOrEmpty(termoInicial))
            {
                txtPesquisa.Text = termoInicial;
                ExecutarBusca();
            }
        }

        private void ExecutarBusca()
        {
            string termo = txtPesquisa.Text.Trim();

            if (string.IsNullOrWhiteSpace(termo))
            {
                produtoVariacaoRepositoryBindingSource.DataSource = null;
                return;
            }

            //try
            //{
            //    //var produtosEncontrados = _produtoVariacaoRepository.BuscarPorNome(termo)?.ToList();

            //    if (produtosEncontrados == null || !produtosEncontrados.Any())
            //    {
            //        produtoVariacaoRepositoryBindingSource.DataSource = null;
            //        return;
            //    }

            //    produtoVariacaoRepositoryBindingSource.DataSource = produtosEncontrados;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Erro ao buscar produtos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void dgvListaProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CodigoProdutoSelecionado = dgvListaProdutos.Rows[e.RowIndex].Cells[0].Value?.ToString();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void txtPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ExecutarBusca();
                e.SuppressKeyPress = true;
            }
        }
    }
}
