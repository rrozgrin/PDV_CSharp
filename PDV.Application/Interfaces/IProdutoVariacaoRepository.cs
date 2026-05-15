using PDV.Domain.Entities;

namespace PDV.Application.Interfaces;

public interface IProdutoVariacaoRepository
{
    ProdutoVariacao? BuscarPorCodigoBarras(string codigoBarras);
}
