using MinhasCompras.Models;
using SQLite;

public class SQLiteDatabaseHelper
{
    readonly SQLiteAsyncConnection _conn;

    public SQLiteDatabaseHelper(string path)
    {
        _conn = new SQLiteAsyncConnection(path);

        Task.Run(InitializeAsync);
    }
    public async Task InitializeAsync()
    {
        await _conn.CreateTableAsync<ProdutoDto>();
    }

    public async Task<int> Insert(ProdutoDto p)
    {
        return await _conn.InsertAsync(p);
    }

    public Task<List<ProdutoDto>> Update(ProdutoDto p)
    {
        string sql = "UPDATE ProdutoDto SET Descricao = ?, Quantidade = ?, Preco = ? WHERE Id = ?";
        return
            _conn.QueryAsync<ProdutoDto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
    }

    public Task<int> Delete(ProdutoDto p)
    {
        return _conn.Table<ProdutoDto>()
            .DeleteAsync(x => x.Id == p.Id);
    }

    public Task<List<ProdutoDto>> GetAll()
    {
        return _conn.Table<ProdutoDto>().OrderBy(p => p.Descricao).ToListAsync();
    }

    public Task<List<ProdutoDto>> Search(string search)
    {
       var sql = "SELECT * FROM ProdutoDto WHERE Descricao LIKE ?";
        return _conn.QueryAsync<ProdutoDto>(sql, "%" + search + "%");

}
