using CurrencyExchangeManagerLib.Connections;
using CurrencyExchangeManagerLib.Models;
using Dapper;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeManagerLib.Repositories
{
    public class CurrencyRateRepository : RepositoryBase<CurrencyRateRepository, CurrencyRate>, IRepository<CurrencyRate>
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
        public async Task<CurrencyRate> AddAsync(CurrencyRate item)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var result = await conn.ExecuteAsync("Call CurrencyConversionDB.Ins_Currency_Exchange_Rate(@p_source_currency_id, @p_target_currency_id, @p_source_system_id, @p_rate)", new { p_source_currency_id = item.source_currency_id, p_target_currency_id = item.target_currency_id, p_source_system_id = item.source_system_id, p_rate = item.rate }).ConfigureAwait(true);
                return item;
            }
        }

        public List<CurrencyRate> Add(List<CurrencyRate> itemlist)
        {
            itemlist.ForEach(item => {
                var result = AddAsync(item);
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

        public async Task<CurrencyRateReadOnly> GetCurrencySourceTargetRate(string source_currency, string target_currency) {
            
            using (var conn = Connection)
            {
                var sql_Params = new DynamicParameters();
                sql_Params.Add("p_source_currency_code", source_currency);
                sql_Params.Add("p_target_currency_code", target_currency);

                conn.Open();
                var result = await conn.QueryFirstAsync<CurrencyRateReadOnly>("CurrencyConversionDB.Get_ReadOnly_Currency_Exchange_Rate",sql_Params,null,15, System.Data.CommandType.StoredProcedure).ConfigureAwait(true);
                return result;
            }
        }

        public Task<CurrencyRate> GetByNameAsync(string val)
        {
            throw new NotImplementedException();
        }
    }
}
