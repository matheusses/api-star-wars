using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matheusses.StarWars.Domain.Interfaces.Repository
{
    public interface IRepository<TEntidade> where TEntidade : class
    {
        Task<IEnumerable<TEntidade>> GetAllAsync();
        Task AddAsync(TEntidade entity);    
    }
}