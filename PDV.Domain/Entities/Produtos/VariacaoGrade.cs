using System;
using System.Collections.Generic;
using System.Text;

namespace PDV.Domain.Entities;

public class VariacaoGrade
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ProdutoVariacaoId { get; private set; }

    // Ex.: "Peso", "Cor", "Tamanho"
    public string Atributo { get; private set; }

    // Ex.: "1Kg", "Azul", "G"
    public string Valor { get; private set; }

    public short OrdemExibicao { get; private set; } = 0;
    public bool Ativo { get; private set; } = true;
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; private set; } = DateTimeOffset.UtcNow;

    public VariacaoGrade(
        Guid produtoVariacaoId,
        string atributo,
        string valor,
        short ordemExibicao = 0)
    {
        if (produtoVariacaoId == Guid.Empty)
            throw new ArgumentException("ProdutoVariacaoId invalido.");

        if (string.IsNullOrWhiteSpace(atributo))
            throw new ArgumentException("Atributo é obrigatório.");

        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("Valor é obrigatório.");

        if (ordemExibicao < 0)
            throw new ArgumentException("Ordem de exibicao invalida.");

        ProdutoVariacaoId = produtoVariacaoId;
        Atributo = atributo.Trim();
        Valor = valor.Trim();
        OrdemExibicao = ordemExibicao;
    }

    public void AlterarValor(string novoValor)
    {
        if (string.IsNullOrWhiteSpace(novoValor))
            throw new ArgumentException("Valor é obrigatório.");

        Valor = novoValor.Trim();
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void AlterarOrdem(short novaOrdem)
    {
        if (novaOrdem < 0)
            throw new ArgumentException("Ordem de exibicao invalida.");

        OrdemExibicao = novaOrdem;
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
