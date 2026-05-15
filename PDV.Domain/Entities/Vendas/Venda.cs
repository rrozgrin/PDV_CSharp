using System;
using System.Collections.Generic;
using System.Text;

namespace PDV.Domain.Entities.Vendas;

public class Venda
{
    private readonly List<VendaItem> _itens = new();

    public Guid Id { get; private set; } = Guid.NewGuid();
    public int Numero { get; private set; }
    public DateTimeOffset DataHora { get; private set; } = DateTimeOffset.UtcNow;
    public string Status { get; private set; } = "ABERTA"; // ABERTA | FINALIZADA | CANCELADA
    public decimal Subtotal { get; private set; }
    public decimal DescontoTotal { get; private set; }
    public decimal Total { get; private set; }
    public string? Observacao { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; private set; } = DateTimeOffset.UtcNow;

    public IReadOnlyCollection<VendaItem> Itens => _itens.AsReadOnly();

    public Venda(int numero, string? observacao = null)
    {
        if (numero <= 0) throw new ArgumentException("Numero da venda invalido.");
        Numero = numero;
        Observacao = observacao?.Trim();
    }

    public void AdicionarItem(VendaItem item)
    {
        if (Status != "ABERTA") throw new InvalidOperationException("Venda nao está aberta.");
        _itens.Add(item);
        RecalcularTotais();
    }

    public void RemoverItem(Guid itemId)
    {
        if (Status != "ABERTA") throw new InvalidOperationException("Venda nao está aberta.");
        var item = _itens.FirstOrDefault(x => x.Id == itemId) ?? throw new ArgumentException("Item nao encontrado.");
        _itens.Remove(item);
        RecalcularTotais();
    }

    public void Finalizar()
    {
        if (_itens.Count == 0) throw new InvalidOperationException("Nao e permitido finalizar venda sem itens.");
        Status = "FINALIZADA";
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void Cancelar(string? motivo = null)
    {
        if (Status == "FINALIZADA") throw new InvalidOperationException("Nao e permitido cancelar venda finalizada.");
        Status = "CANCELADA";
        Observacao = string.IsNullOrWhiteSpace(motivo) ? Observacao : motivo.Trim();
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    private void RecalcularTotais()
    {
        Subtotal = _itens.Sum(i => i.Quantidade * i.ValorUnitario);
        DescontoTotal = _itens.Sum(i => i.Desconto);
        Total = _itens.Sum(i => i.Subtotal);
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}

