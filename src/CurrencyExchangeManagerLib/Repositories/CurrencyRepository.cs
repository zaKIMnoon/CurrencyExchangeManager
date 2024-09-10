using CurrencyExchangeManagerLib.Connections;
using CurrencyExchangeManagerLib.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeManagerLib.Repositories
{
    public class CurrencyRepository : RepositoryBase<CurrencyRepository>, IRepository<Currency>
    {
        public CurrencyRepository(DatabaseConfig databaseConfig) : base(databaseConfig)
        {

        }
        public Currency Add(Currency item)
        {
            using (var conn = Connection)
            {
                conn.Open();
                conn.Execute("Call CurrencyConversionDB.Ins_Currency(@p_currency_code, @p_currency_name)", new { p_currency_code = item.currency_code, p_currency_name = item.currency_name });
                return this.GetByName(item.currency_code);
            }
        }

        public Currency Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Currency> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                return conn.Query<Currency>("Call CurrencyConversionDB.Get_Currency(null)");
            }
        }

        public Currency GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Currency GetByName(string val)
        {
            using (var conn = Connection)
            {
                conn.Open();
                return conn.Query<Currency>("Call CurrencyConversionDB.Get_Currency(@p_Code)", new { p_Code = val }).FirstOrDefault();
            }
        }

        public Currency Update(Currency item)
        {
            throw new NotImplementedException();
        }
    }
}
