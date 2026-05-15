using Microsoft.Data.Sqlite;
using PDV.Application.Interfaces;
using PDV.Domain.Entities.Vendas;
using PDV.Infrastructure.Database;

namespace PDV.Infrastructure.Repositories;

public class VendaRepository : IVendaRepository
{
    private readonly SqliteConnectionFactory _connectionFactory;

    public VendaRepository(SqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void Criar(Venda venda)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var transaction = connection.BeginTransaction();

        const string sqlVenda = @"
            INSERT INTO venda
            (id, numero, data_hora, status, subtotal, desconto_total, total, observacao, created_at, updated_at)
            VALUES
            ($id, $numero, $data_hora, $status, $subtotal, $desconto_total, $total, $observacao, $created_at, $updated_at);";

        using (var cmdVenda = new SqliteCommand(sqlVenda, connection, transaction))
        {
            cmdVenda.Parameters.AddWithValue("$id", venda.Id.ToString());
            cmdVenda.Parameters.AddWithValue("$numero", venda.Numero);
            cmdVenda.Parameters.AddWithValue("$data_hora", venda.DataHora.ToString("O"));
            cmdVenda.Parameters.AddWithValue("$status", venda.Status);
            cmdVenda.Parameters.AddWithValue("$subtotal", venda.Subtotal);
            cmdVenda.Parameters.AddWithValue("$desconto_total", venda.DescontoTotal);
            cmdVenda.Parameters.AddWithValue("$total", venda.Total);
            cmdVenda.Parameters.AddWithValue("$observacao", (object?)venda.Observacao ?? DBNull.Value);
            cmdVenda.Parameters.AddWithValue("$created_at", venda.CreatedAt.ToString("O"));
            cmdVenda.Parameters.AddWithValue("$updated_at", venda.UpdatedAt.ToString("O"));

            cmdVenda.ExecuteNonQuery();
        }

        const string sqlItem = @"
            INSERT INTO venda_item
            (id, venda_id, produto_variacao_id, codigo_barras, descricao_produto, descricao_variacao, quantidade, valor_unitario, desconto, subtotal, created_at, updated_at)
            VALUES
            ($id, $venda_id, $produto_variacao_id, $codigo_barras, $descricao_produto, $descricao_variacao, $quantidade, $valor_unitario, $desconto, $subtotal, $created_at, $updated_at);";

        foreach (var item in venda.Itens)
        {
            using var cmdItem = new SqliteCommand(sqlItem, connection, transaction);
            cmdItem.Parameters.AddWithValue("$id", item.Id.ToString());
            cmdItem.Parameters.AddWithValue("$venda_id", item.VendaId.ToString());
            cmdItem.Parameters.AddWithValue("$produto_variacao_id", item.ProdutoVariacaoId.ToString());
            cmdItem.Parameters.AddWithValue("$codigo_barras", item.CodigoBarras);
            cmdItem.Parameters.AddWithValue("$descricao_produto", item.DescricaoProduto);
            cmdItem.Parameters.AddWithValue("$descricao_variacao", (object?)item.DescricaoVariacao ?? DBNull.Value);
            cmdItem.Parameters.AddWithValue("$quantidade", item.Quantidade);
            cmdItem.Parameters.AddWithValue("$valor_unitario", item.ValorUnitario);
            cmdItem.Parameters.AddWithValue("$desconto", item.Desconto);
            cmdItem.Parameters.AddWithValue("$subtotal", item.Subtotal);
            cmdItem.Parameters.AddWithValue("$created_at", item.CreatedAt.ToString("O"));
            cmdItem.Parameters.AddWithValue("$updated_at", item.UpdatedAt.ToString("O"));

            cmdItem.ExecuteNonQuery();
        }

        transaction.Commit();
    }

    public void Atualizar(Venda venda)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var transaction = connection.BeginTransaction();

        const string sqlUpdateVenda = @"
            UPDATE venda
            SET
                status = $status,
                subtotal = $subtotal,
                desconto_total = $desconto_total,
                total = $total,
                observacao = $observacao,
                updated_at = $updated_at
            WHERE id = $id;";

        using (var cmdVenda = new SqliteCommand(sqlUpdateVenda, connection, transaction))
        {
            cmdVenda.Parameters.AddWithValue("$id", venda.Id.ToString());
            cmdVenda.Parameters.AddWithValue("$status", venda.Status);
            cmdVenda.Parameters.AddWithValue("$subtotal", venda.Subtotal);
            cmdVenda.Parameters.AddWithValue("$desconto_total", venda.DescontoTotal);
            cmdVenda.Parameters.AddWithValue("$total", venda.Total);
            cmdVenda.Parameters.AddWithValue("$observacao", (object?)venda.Observacao ?? DBNull.Value);
            cmdVenda.Parameters.AddWithValue("$updated_at", venda.UpdatedAt.ToString("O"));
            cmdVenda.ExecuteNonQuery();
        }

        const string sqlDeleteItens = "DELETE FROM venda_item WHERE venda_id = $venda_id;";
        using (var cmdDelete = new SqliteCommand(sqlDeleteItens, connection, transaction))
        {
            cmdDelete.Parameters.AddWithValue("$venda_id", venda.Id.ToString());
            cmdDelete.ExecuteNonQuery();
        }

        const string sqlInsertItem = @"
            INSERT INTO venda_item
            (id, venda_id, produto_variacao_id, codigo_barras, descricao_produto, descricao_variacao, quantidade, valor_unitario, desconto, subtotal, created_at, updated_at)
            VALUES
            ($id, $venda_id, $produto_variacao_id, $codigo_barras, $descricao_produto, $descricao_variacao, $quantidade, $valor_unitario, $desconto, $subtotal, $created_at, $updated_at);";

        foreach (var item in venda.Itens)
        {
            using var cmdItem = new SqliteCommand(sqlInsertItem, connection, transaction);
            cmdItem.Parameters.AddWithValue("$id", item.Id.ToString());
            cmdItem.Parameters.AddWithValue("$venda_id", item.VendaId.ToString());
            cmdItem.Parameters.AddWithValue("$produto_variacao_id", item.ProdutoVariacaoId.ToString());
            cmdItem.Parameters.AddWithValue("$codigo_barras", item.CodigoBarras);
            cmdItem.Parameters.AddWithValue("$descricao_produto", item.DescricaoProduto);
            cmdItem.Parameters.AddWithValue("$descricao_variacao", (object?)item.DescricaoVariacao ?? DBNull.Value);
            cmdItem.Parameters.AddWithValue("$quantidade", item.Quantidade);
            cmdItem.Parameters.AddWithValue("$valor_unitario", item.ValorUnitario);
            cmdItem.Parameters.AddWithValue("$desconto", item.Desconto);
            cmdItem.Parameters.AddWithValue("$subtotal", item.Subtotal);
            cmdItem.Parameters.AddWithValue("$created_at", item.CreatedAt.ToString("O"));
            cmdItem.Parameters.AddWithValue("$updated_at", item.UpdatedAt.ToString("O"));
            cmdItem.ExecuteNonQuery();
        }

        transaction.Commit();
    }


    public Venda? ObterPorId(Guid vendaId)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        const string sqlVenda = @"
            SELECT id, numero, data_hora, status, subtotal, desconto_total, total, observacao, created_at, updated_at
            FROM venda
            WHERE id = $id
            LIMIT 1;";

        using var cmdVenda = new SqliteCommand(sqlVenda, connection);
        cmdVenda.Parameters.AddWithValue("$id", vendaId.ToString());

        using var readerVenda = cmdVenda.ExecuteReader();
        if (!readerVenda.Read()) return null;

        var numero = readerVenda.GetInt32(1);
        var observacao = readerVenda.IsDBNull(7) ? null : readerVenda.GetString(7);

        var venda = new Venda(numero, observacao);

        const string sqlItens = @"
            SELECT id, venda_id, produto_variacao_id, codigo_barras, descricao_produto, descricao_variacao, quantidade, valor_unitario, desconto
            FROM venda_item
            WHERE venda_id = $venda_id;";

        using var cmdItens = new SqliteCommand(sqlItens, connection);
        cmdItens.Parameters.AddWithValue("$venda_id", vendaId.ToString());

        using var readerItens = cmdItens.ExecuteReader();
        while (readerItens.Read())
        {
            var item = new VendaItem(
                Guid.Parse(readerItens.GetString(1)),
                Guid.Parse(readerItens.GetString(2)),
                readerItens.GetString(3),
                readerItens.GetString(4),
                readerItens.IsDBNull(5) ? null : readerItens.GetString(5),
                readerItens.GetDecimal(6),
                readerItens.GetDecimal(7),
                readerItens.GetDecimal(8));

            venda.AdicionarItem(item);
        }

        return venda;
    }
}
