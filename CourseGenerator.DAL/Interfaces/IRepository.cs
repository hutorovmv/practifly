﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseGenerator.DAL.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task<T> GetAsync(params object[] key);
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T item);
        void Update(T item);
        void Delete(T item);
    }
}
