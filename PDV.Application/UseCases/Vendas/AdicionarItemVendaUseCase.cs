using PDV.Application.Interfaces;
using PDV.Domain.Entities.Vendas;

namespace PDV.Application.UseCases.Vendas;

public class AdicionarItemVendaUseCase
{
    private readonly IVendaRepository _vendaRepository;
    private readonly IProdutoVariacaoRepository _produtoVariacaoRepository;

    public AdicionarItemVendaUseCase(
        IVendaRepository vendaRepository,
        IProdutoVariacaoRepository produtoVariacaoRepository)
    {
        _vendaRepository = vendaRepository;
        _produtoVariacaoRepository = produtoVariacaoRepository;
    }

    public void Executar(Guid vendaId, string codigoBarras, decimal quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("Quantidade deve ser maior que zero.");

        if (string.IsNullOrWhiteSpace(codigoBarras))
            throw new ArgumentException("Codigo de barras obrigatorio.");

        var venda = _vendaRepository.ObterPorId(vendaId)
            ?? throw new InvalidOperationException("Venda nao encontrada.");

        var variacao = _produtoVariacaoRepository.BuscarPorCodigoBarras(codigoBarras.Trim())
            ?? throw new InvalidOperationException("Produto nao encontrado para o codigo informado.");

        if (variacao.EstoqueAtual < quantidade)
            throw new InvalidOperationException("Estoque insuficiente.");

        var item = new VendaItem(
            venda.Id,
            variacao.Id,
            variacao.CodigoBarras,
            "Produto", // placeholder ate trazer descricao real
            null,      // placeholder ate trazer descricao da variacao
            quantidade,
            variacao.PrecoVenda,
            0);

        venda.AdicionarItem(item);
        _vendaRepository.Atualizar(venda);
    }
}
