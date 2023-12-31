﻿using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicroServer.Repository.Base
{
    public interface IRepositoryBase<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// 获得Dto
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TDto> GetAsync<TDto>(TKey id);

        /// <summary>
        /// 根据条件获取Dto
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<TDto> GetAsync<TDto>(Expression<Func<TEntity, bool>> exp);

        /// <summary>
        /// 根据条件获取实体
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> SoftDeleteAsync(TKey id);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> SoftDeleteAsync(Expression<Func<TEntity, bool>> exp, params string[] name);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> SoftDeleteAsync(TKey[] id);
    }

    public interface IRepositoryBase<TEntity> : IRepositoryBase<TEntity, long> where TEntity : class
    {
    }
}
