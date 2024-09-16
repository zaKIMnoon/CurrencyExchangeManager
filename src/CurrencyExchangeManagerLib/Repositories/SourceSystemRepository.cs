using CurrencyExchangeManagerLib.Connections;
using CurrencyExchangeManagerLib.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeManagerLib.Repositories
{
    public class SourceSystemRepository : RepositoryBase<SourceSystemRepository, SourceSystem>, IRepository<SourceSystem>
    {
        public SourceSystemRepository(DatabaseConfig databaseConfig) : base(databaseConfig) 
        {
            
        }

        public SourceSystem Add(SourceSystem item)
        {
            throw new NotImplementedException();
        }

        public async Task<SourceSystem> AddAsync(SourceSystem item)
        {
            var sql_params = new DynamicParameters();

            sql_params.Add("p_source_system_name", item.Source_System_Name, DbType.String);
            sql_params.Add("p_source_system_url", item.Source_System_Url, DbType.String);

            await ExecuteInsertAsync("CurrencyConversionDB.Ins_CurrencySourceSystem", sql_params);
            var result =  await this.GetByNameAsync(item.Source_System_Name);
            return result;
        }

        public SourceSystem Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SourceSystem> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SourceSystem>> GetAllAsync()
        {
            var sql_params = new DynamicParameters();
            
            sql_params.Add("p_source_system_name",DBNull.Value,DbType.String);
            
            var result =  await ExecuteQueryAsync("CurrencyConversionDB.Get_CurrencySourceSystem", sql_params);
            return (IEnumerable<SourceSystem>)result;
        }

        public SourceSystem GetById(int id)
        {
            throw new NotImplementedException();
        }

        public SourceSystem GetByName(string val)
        {
            throw new NotImplementedException();
        }

        public async Task<SourceSystem> GetByNameAsync(string val)
        {
            var sql_params = new DynamicParameters();
            sql_params.Add("p_source_system_name", val, DbType.String);
            
            var result = await ExecuteQueryFirstAsync("CurrencyConversionDB.Get_CurrencySourceSystem", sql_params);
            return result;
        }

        public SourceSystem Update(SourceSystem item)
        {
            throw new NotImplementedException();
        }
    }
}
