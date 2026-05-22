using PDV.Domain.Entities.Produtos;

namespace PDV.Application.Interfaces;

public interface IProdutoVariacaoRepository
{
    ProdutoVariacao? BuscarPorCodigoBarras(string codigoBarras);
    List<ProdutoVariacao> BuscarPorNome(string nome);
}
