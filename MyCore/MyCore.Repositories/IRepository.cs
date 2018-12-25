using System;
using System.Linq;
using System.Linq.Expressions;

namespace MyCore.Repositories
{
    public interface IRepository<T> where T:class
    {
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find(object id);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Any(object id);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Any(Func<T, bool> predicate);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Get(Func<T, bool> predicate);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entities"></param>
        void Add(T[] entities);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entities"></param>
        void Update(T[] entities);

        /// <summary>
        /// 硬删除，无法恢复
        /// </summary>
        /// <param name="id"></param>
        void Remove(object id);

        /// <summary>
        /// 硬删除，无法恢复
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);

        /// <summary>
        /// 硬删除，无法恢复
        /// </summary>
        /// <param name="entities"></param>
        void Remove(T[] entities);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// 还原
        /// </summary>
        /// <param name="id"></param>
        void Restore(object id);

        /// <summary>
        /// 还原
        /// </summary>
        /// <param name="entity"></param>
        void Restore(T entity);
    }
}
