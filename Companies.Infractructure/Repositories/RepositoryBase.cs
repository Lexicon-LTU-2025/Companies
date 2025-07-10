﻿using Companies.Infractructure.Data;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Infractructure.Repositories;
public class RepositoryBase<T> : IRepositoryBase<T> where T : class //Do Entitybase
{
    protected ApplicationDbContext Context { get; }
    protected DbSet<T> DbSet { get; }

    public RepositoryBase(ApplicationDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public IQueryable<T> FindAll(bool trackChanges = false) =>
        !trackChanges ? DbSet.AsNoTracking() :
                        DbSet;

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
        !trackChanges ? DbSet.Where(expression).AsNoTracking() :
                        DbSet.Where(expression);

    public void Create(T entity) => DbSet.Add(entity);
 
    public void Update(T entity) => DbSet.Update(entity);

    public void Delete(T entity) => DbSet.Remove(entity);
}
