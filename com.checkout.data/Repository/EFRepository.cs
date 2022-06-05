﻿using com.checkout.data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace com.checkout.data.Repository
{
    public class EFRepository
    {
        public readonly CKODBContext _context;

        public EFRepository(CKODBContext context)
        {
            this._context = context;
        }

        public void Add<EntityType>(EntityType entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }
        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }
        public IQueryable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return _context.Set<TEntity>().Where(predicate);
        }
        public bool Update<TEntity>(TEntity item) where TEntity : class
        {
            _context.Attach(item);
            _context.Update(item);
            _context.SaveChanges();
            return true;
        }
        // add generic update method here

        //public bool UpdateTransaction(Transaction transaction)
        //{
        //    _context.Update<Transaction>(transaction);//.Update(transaction);
        //    int status = _context.SaveChanges();

        //    return status == 1 ? true : false;
        //}

    }
}
