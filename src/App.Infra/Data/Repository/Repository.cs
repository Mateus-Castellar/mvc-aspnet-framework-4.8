using App.Business.Core.Data;
using App.Business.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Infra.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MvcContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository()
        {
            Db = new MvcContext();
            DbSet = Db.Set<TEntity>();
        }

        public async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public async Task Atualizar(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
        }

        public async Task Remover(Guid id)
        {
            Db.Entry(new TEntity { Id = id }).State = EntityState.Deleted;
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
