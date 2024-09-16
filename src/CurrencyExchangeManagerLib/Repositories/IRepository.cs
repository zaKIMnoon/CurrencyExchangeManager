using CurrencyExchangeManagerLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeManagerLib.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetByName(string val);
        Task<T> GetByNameAsync(string val);
        T GetById(int id);
        T Add(T item);
        Task<T> AddAsync(T item);
        T Update(T item);
        T Delete(int id);
        
    }
}
