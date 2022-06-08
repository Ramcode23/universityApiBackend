using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityApiBackend.Services
{
    public interface IBaseService<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T?> GetById(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
       bool Exists(int Id);
    }
   
    
 
}