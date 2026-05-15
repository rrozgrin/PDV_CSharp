using System;
using System.Collections.Generic;

namespace PDV.Domain.Entities.Produtos;

public class Produto
{
    private readonly List<ProdutoVariacao> _variacoes = new();

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Descricao { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public bool Ativo { get; private set; } = true;

    public IReadOnlyCollection<ProdutoVariacao> Variacoes => _variacoes.AsReadOnly();

    public Produto(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descricao do produto é obrigatória.");

        Descricao = descricao.Trim();
    }

    public void AlterarDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descricao do produto é obrigatória.");

        Descricao = descricao.Trim();
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void AdicionarVariacao(ProdutoVariacao variacao)
    {
        if (variacao is null)
            throw new ArgumentNullException(nameof(variacao));

        _variacoes.Add(variacao);
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void ValidarParaVenda()
    {
        if (_variacoes.Count == 0)
            throw new InvalidOperationException("Produto deve possuir ao menos uma variacao.");
    }

    public void Desativar() => Ativo = false;
    public void Ativar() => Ativo = true;
}
