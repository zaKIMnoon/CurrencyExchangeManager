using CurrencyExchangeManagerLib.Connections;
using CurrencyExchangeManagerLib.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeManagerLib.Repositories
{
    public class RepositoryBase<T, U>
    {
        protected readonly DatabaseConfig _databaseConfig;
        
        protected IDbConnection Connection => new MySqlConnection(_databaseConfig.ConnectionString);

        public RepositoryBase(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<IEnumerable<U>> ExecuteQueryAsync(string storedProcedureName, DynamicParameters parameters) 
        {
            try 
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var result = await conn.QueryAsync<U>(storedProcedureName, parameters, null, 10, CommandType.StoredProcedure).ConfigureAwait(true);
                    return (IEnumerable<U>)result;
                }
            } 
            catch (Exception ex) 
            {
                var e = ex; 
            }
            return default(IEnumerable<U>);
        }
        public async Task<U> ExecuteQueryFirstAsync(string storedProcedureName, DynamicParameters parameters)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var result = await conn.QueryFirstAsync<U>(storedProcedureName, parameters, null, 10, CommandType.StoredProcedure).ConfigureAwait(true);
                    return result;
                }
            }
            catch (Exception ex)
            {
                var e = ex;
            }
            return default(U);
        }
        public async Task<U> ExecuteInsertAsync(string storedProcedureName, DynamicParameters parameters)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    var result = await conn.ExecuteAsync(storedProcedureName,parameters,null,15,CommandType.StoredProcedure).ConfigureAwait(true);
                }
            }
            catch (Exception ex)
            {
                var e = ex;
            }
            return default(U);
        }
    }
}
