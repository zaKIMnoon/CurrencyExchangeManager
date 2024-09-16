using CurrencyExchangeManagerLib.Connections;
using CurrencyExchangeManagerLib.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeManagerLib.Repositories
{
    public class CurrencyRepository : RepositoryBase<CurrencyRepository, Currency>, IRepository<Currency>
    {
        public CurrencyRepository(DatabaseConfig databaseConfig) : base(databaseConfig)
        {

        }
        public Currency Add(Currency item)
        {
            throw new NotImplementedException();
        }

        public async Task<Currency> AddAsync(Currency item)
        {
            var sql_params = new DynamicParameters();

            sql_params.Add("p_currency_code", item.currency_code, DbType.String);
            sql_params.Add("p_currency_name", item.currency_name, DbType.String);

            await ExecuteInsertAsync("CurrencyConversionDB.Ins_Currency", sql_params);
            var result  = await GetByNameAsync(item.currency_code);
            return result;
        }

        public Currency Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Currency> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            var sql_params = new DynamicParameters();

            sql_params.Add("p_Code", DBNull.Value, DbType.String);

            var result = await ExecuteQueryAsync("CurrencyConversionDB.Get_Currency", sql_params);
            return (IEnumerable<Currency>)result;
        }

        public Currency GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Currency GetByName(string val)
        {
            throw new NotImplementedException();
        }

        public async Task<Currency> GetByNameAsync(string val)
        {
            var sql_params = new DynamicParameters();
            sql_params.Add("p_Code", val, DbType.String);

            var result = await ExecuteQueryFirstAsync("CurrencyConversionDB.Get_Currency", sql_params);
            return result;
        }

        public Currency Update(Currency item)
        {
            throw new NotImplementedException();
        }
    }
}
