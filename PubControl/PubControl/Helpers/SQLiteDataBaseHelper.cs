using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using PubControl.Models;
using System.Threading.Tasks;
using PubControl.Views;

namespace PubControl.Helpers
{
    public class SQLiteDataBaseHelper
    {
        readonly SQLiteAsyncConnection _db;

        public SQLiteDataBaseHelper(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Publicacao>().Wait();
        }

        public async Task<List<Publicacao>> GetAllRows()
        {
            return await _db.Table<Publicacao>().OrderByDescending(i => i.Id).ToListAsync();
        }

        public Task<Publicacao> GetById(int id)
        {
            return _db.Table<Publicacao>().FirstAsync(i => i.Id == id);
        }

        public Task<int> Insert(Publicacao model)
        {
            return _db.InsertAsync(model);
        }

        public Task<List<Publicacao>> Update(Publicacao model)
        {
            string sql = "UPDATE PUBLICACAO SET DESCRICAO=?, CODIGOREFERENCIA=?, IDTIPOPUBLICACAO=?, DATA=?, OBSERVACAO=? WHERE ID=?";
            return _db.QueryAsync<Publicacao>(
                sql,
                model.Descricao,
                model.CodigoReferencia,
                model.Id,
                model.Data,
                model.Observacao);
        }

        public Task<int> Delete(int id)
        {
            return _db.Table<Publicacao>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Publicacao>> Search(string q)
        {
            string sql = "SELECT * FROM PUBLICACAO WHERE DESCRICAO LIKE '%" + q + "%'";
            return _db.QueryAsync<Publicacao>(sql);
        }
            //GetAllRows()
            //GetById(Ind Id)
            //Insert(publicacao model)
            //Update(publicacao model)
            //Delete(int id)
            //Search (string query)
    }
}
