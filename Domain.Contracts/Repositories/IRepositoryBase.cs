using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories;
public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll(bool trackChanges = false);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
    void Create(T company);
    void Update(T company);
    void Delete(T company);

}
