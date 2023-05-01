﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticlesViewer.Application.RepositoryContracts;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();    
    Task<T?> GetByIdAsync(object id);
    Task CreateAsync(T value);
    Task UpdateAsync(T value);
    Task DeleteAsync(object id);
    Task SaveAsync();
}


