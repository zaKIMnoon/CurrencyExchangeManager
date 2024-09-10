using CurrencyExchangeManagerLib.Connections;
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
    public class RepositoryBase<T>
    {
        protected readonly DatabaseConfig _databaseConfig;
        
        protected IDbConnection Connection => new MySqlConnection(_databaseConfig.ConnectionString);

        public RepositoryBase(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }
    }
}
