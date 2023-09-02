using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of C#
            //using kullanılınca sadece bu kod bloğu çalışırken bellek tüketimi yapar. kod bloğu sonlanınca bellekten atılır.
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity); // referansı yakala
                addedEntity.State = EntityState.Added; // veritabanında işlem yapılacak nesne
                context.SaveChanges(); //işlemleri gerçekleştir
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity); // referansı yakala
                deletedEntity.State = EntityState.Deleted; // veritabanında işlem yapılacak nesne
                context.SaveChanges(); //işlemleri gerçekleştir
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
            using (TContext context = new TContext())
            {
                //eğer filtre null ise ilk kısım değilse ikinci kısım çalışır
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity); // referansı yakala
                updatedEntity.State = EntityState.Modified; // veritabanında işlem yapılacak nesne
                context.SaveChanges(); //işlemleri gerçekleştir
            }
        }
    }
}
