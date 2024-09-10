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
    public class CurrencyRateRepository : RepositoryBase<CurrencyRateRepository>, IRepository<CurrencyRate>
    {

        public CurrencyRateRepository(DatabaseConfig databaseConfig) : base(databaseConfig)
        {

        }

        public CurrencyRate Add(CurrencyRate item)
        {
            using (var conn = Connection)
            {
                conn.Open();
                conn.Execute("Call CurrencyConversionDB.Ins_Currency_Exchange_Rate(@p_source_currency_id, @p_target_currency_id, @p_source_system_id, @p_rate)", new { p_source_currency_id = item.source_currency_id, p_target_currency_id = item.target_currency_id, p_source_system_id = item.source_system_id , p_rate = item.rate});
                return item;
            }
        }
        public List<CurrencyRate> Add(List<CurrencyRate> itemlist)
        {
            itemlist.ForEach(item => {
                Add(item);
            });
            return itemlist;
        }

        public CurrencyRate Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CurrencyRate> GetAll()
        {
            throw new NotImplementedException();
        }

        public CurrencyRate GetById(int id)
        {
            throw new NotImplementedException();
        }

        public CurrencyRate GetByName(string val)
        {
            throw new NotImplementedException();
        }

        public CurrencyRate Update(CurrencyRate item)
        {
            throw new NotImplementedException();
        }
    }
}
