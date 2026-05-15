using Microsoft.Data.Sqlite;
using PDV.Application.Interfaces;
using PDV.Domain.Entities.Produtos;
using PDV.Infrastructure.Database;

namespace PDV.Infrastructure.Repositories.Produtos;

public class ProdutoVariacaoRepository : IProdutoVariacaoRepository
{
    private readonly SqliteConnectionFactory _connectionFactory;

    public ProdutoVariacaoRepository(SqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public ProdutoVariacao? BuscarPorCodigoBarras(string codigoBarras)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        const string sql = @"
            SELECT id, produto_id, codigo_barras, preco_venda, estoque_atual
            FROM produto_variacao
            WHERE codigo_barras = $codigoBarras
              AND ativo = 1
            LIMIT 1;";

        using var cmd = new SqliteCommand(sql, connection);
        cmd.Parameters.AddWithValue("$codigoBarras", codigoBarras);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return null;

        var id = Guid.Parse(reader.GetString(0));
        var produtoId = Guid.Parse(reader.GetString(1));
        var cb = reader.GetString(2);
        var preco = reader.GetDecimal(3);
        var estoque = reader.GetDecimal(4);

        return new ProdutoVariacao(produtoId, cb, preco, estoque);
    }
}
