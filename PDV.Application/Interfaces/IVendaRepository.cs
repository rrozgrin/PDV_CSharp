using PDV.Domain.Entities.Vendas;

namespace PDV.Application.Interfaces;

public interface IVendaRepository
{
    void Criar(Venda venda);
    void Atualizar(Venda venda);
    Venda? ObterPorId(Guid vendaId);
}
