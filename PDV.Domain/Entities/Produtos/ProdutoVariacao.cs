using System;
using System.Collections.Generic;
using System.Text;

namespace PDV.Domain.Entities.Produtos;

public class ProdutoVariacao
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ProdutoId { get; private set; }

    public string CodigoBarras { get; private set; }
    public decimal PrecoVenda { get; private set; }
    public decimal EstoqueAtual { get; private set; }

    public bool Ativo { get; private set; } = true;
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; private set; } = DateTimeOffset.UtcNow;

    public ProdutoVariacao(
        Guid produtoId,
        string codigoBarras,
        decimal precoVenda,
        decimal estoqueAtual = 0)
    {
        if (produtoId == Guid.Empty)
            throw new ArgumentException("ProdutoId invalido.");

        if (string.IsNullOrWhiteSpace(codigoBarras))
            throw new ArgumentException("Codigo de barras é obrigatório.");

        if (precoVenda <= 0)
            throw new ArgumentException("Preco de venda deve ser maior que zero.");

        if (estoqueAtual < 0)
            throw new ArgumentException("Estoque nao pode ser negativo.");

        ProdutoId = produtoId;
        CodigoBarras = codigoBarras.Trim();
        PrecoVenda = precoVenda;
        EstoqueAtual = estoqueAtual;
    }

    public void AlterarPreco(decimal novoPreco)
    {
        if (novoPreco <= 0)
            throw new ArgumentException("Novo preco deve ser maior que zero.");

        PrecoVenda = novoPreco;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void EntradaEstoque(decimal quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("Quantidade de entrada deve ser maior que zero.");

        EstoqueAtual += quantidade;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void BaixaEstoque(decimal quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("Quantidade de baixa deve ser maior que zero.");

        if (quantidade > EstoqueAtual)
            throw new InvalidOperationException("Estoque insuficiente.");

        EstoqueAtual -= quantidade;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void Desativar()
    {
        Ativo = false;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void Ativar()
    {
        Ativo = true;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
