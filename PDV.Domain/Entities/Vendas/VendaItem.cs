using System;
using System.Collections.Generic;
using System.Text;

namespace PDV.Domain.Entities.Vendas;

public class VendaItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid VendaId { get; private set; }
    public Guid ProdutoVariacaoId { get; private set; }

    public string CodigoBarras { get; private set; }
    public string DescricaoProduto { get; private set; }
    public string? DescricaoVariacao { get; private set; }

    public decimal Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }
    public decimal Desconto { get; private set; }
    public decimal Subtotal => (Quantidade * ValorUnitario) - Desconto;

    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; private set; } = DateTimeOffset.UtcNow;

    public VendaItem(
        Guid vendaId,
        Guid produtoVariacaoId,
        string codigoBarras,
        string descricaoProduto,
        string? descricaoVariacao,
        decimal quantidade,
        decimal valorUnitario,
        decimal desconto = 0)
    {
        if (vendaId == Guid.Empty) throw new ArgumentException("VendaId invalido.");
        if (produtoVariacaoId == Guid.Empty) throw new ArgumentException("ProdutoVariacaoId invalido.");
        if (string.IsNullOrWhiteSpace(codigoBarras)) throw new ArgumentException("Codigo de barras obrigatorio.");
        if (string.IsNullOrWhiteSpace(descricaoProduto)) throw new ArgumentException("Descricao do produto obrigatoria.");
        if (quantidade <= 0) throw new ArgumentException("Quantidade deve ser maior que zero.");
        if (valorUnitario <= 0) throw new ArgumentException("Valor unitario deve ser maior que zero.");
        if (desconto < 0) throw new ArgumentException("Desconto nao pode ser negativo.");
        if (desconto > quantidade * valorUnitario) throw new ArgumentException("Desconto maior que valor bruto do item.");

        VendaId = vendaId;
        ProdutoVariacaoId = produtoVariacaoId;
        CodigoBarras = codigoBarras.Trim();
        DescricaoProduto = descricaoProduto.Trim();
        DescricaoVariacao = descricaoVariacao?.Trim();
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
        Desconto = desconto;
    }
}

