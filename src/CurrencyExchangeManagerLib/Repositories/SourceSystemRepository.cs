using CurrencyExchangeManagerLib.Connections;
using CurrencyExchangeManagerLib.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeManagerLib.Repositories
{
    public class SourceSystemRepository : RepositoryBase<SourceSystemRepository>, IRepository<SourceSystem>
    {
        public SourceSystemRepository(DatabaseConfig databaseConfig) : base(databaseConfig) 
        {
            
        }

        public SourceSystem Add(SourceSystem item)
        {
            using (var conn = Connection)
            {
                conn.Open();
                conn.Execute("Call CurrencyConversionDB.Ins_CurrencySourceSystem(@p_source_system_name, @p_source_system_url)", new { p_source_system_name = item.Source_System_Name, p_source_system_url = item.Source_System_Url });
                return this.GetByName(item.Source_System_Name);
            }
        }

        public SourceSystem Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SourceSystem> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                return conn.Query<SourceSystem>("Call CurrencyConversionDB.Get_CurrencySourceSystem(null)");
            }
        }

        public SourceSystem GetById(int id)
        {
            throw new NotImplementedException();
        }

        public SourceSystem GetByName(string val)
        {
            using (var conn = Connection)
            {
                conn.Open();
                return conn.Query<SourceSystem>("Call CurrencyConversionDB.Get_CurrencySourceSystem(@p_source_system_name)", new { p_source_system_name = val }).FirstOrDefault();
            }
        }

        public SourceSystem Update(SourceSystem item)
        {
            throw new NotImplementedException();
        }
    }
}
