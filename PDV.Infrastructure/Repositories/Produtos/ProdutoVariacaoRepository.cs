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
            SELECT pv.id, produto_id, codigo_barras, preco_venda, estoque_atual, p.descricao as descricao
            FROM produto_variacao pv
            INNER JOIN produto p ON p.id = pv.produto_id
            WHERE codigo_barras = $codigoBarras
              AND pv.ativo = 1
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
        var descricao = reader.GetString(5);

        return new ProdutoVariacao(id, produtoId, cb, preco, estoque, descricao);
    }

    public List<ProdutoVariacao> BuscarPorNome(string nome)
    {
        var lista = new List<ProdutoVariacao>();
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();


        const string sql = @"
            SELECT
                pv.id,
                pv.produto_id,
                pv.codigo_barras,
                pv.preco_venda,
                pv.estoque_atual,
                p.descricao
                  || CASE
                       WHEN vg.descricao_grade IS NOT NULL AND vg.descricao_grade <> ''
                       THEN ' - ' || vg.descricao_grade
                       ELSE ''
                     END AS descricao
            FROM produto_variacao pv
            INNER JOIN produto p ON p.id = pv.produto_id
            LEFT JOIN (
                SELECT
                    produto_variacao_id,
                    GROUP_CONCAT(atributo || ': ' || valor, ', ') AS descricao_grade
                FROM variacao_grade
                WHERE ativo = 1
                GROUP BY produto_variacao_id
            ) vg ON vg.produto_variacao_id = pv.id
            WHERE p.descricao LIKE $nome
              AND pv.ativo = 1;";
        using var cmd = new SqliteCommand(sql, connection);
        cmd.Parameters.AddWithValue("$nome", $"%{nome}%");
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var p = new ProdutoVariacao(
            Guid.Parse(reader.GetString(0)), // ID do banco
            Guid.Parse(reader.GetString(1)), // produtoId
            reader.GetString(2),             // codigo
            reader.GetDecimal(3),            // preco
            reader.GetDecimal(4),            // estoque
            reader.GetString(5)              // descricao
        );
            lista.Add(p);
        }
        return lista;
    }
}
