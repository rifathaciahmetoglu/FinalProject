using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        //içerisine örneğin getall(categoryId==2) yapabiliriz. bu sayede ayrı ayrı metod yazmamıza gerek kalmaz.
        List<T> GetAll(Expression<Func<T, bool>>? filter = null);
        T Get(Expression<Func<T, bool>> filter); //Tek Data getirmek için, parantez içerisine veri yazmak zorundadır.
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
