using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibos.Domain;
using System.Linq.Expressions;
using NHibernate.Criterion;
using Tibos.Repository.Contract;
using Tibos.Common;
using NHibernate.Criterion.Lambda;

//Nhibernate Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Repository.Service
{
	public class DictTypeDao:IDictType
	{
        private ISessionFactory sessionFactory = ManagerPage.SessionFactory;
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public virtual bool Exists(object id)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                return Get(id) != null;
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public virtual object Save(DictType model)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                var id = session.Save(model);
                session.Flush();
                return id;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public virtual void Update(DictType model)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.SaveOrUpdate(model);
                session.Flush();
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        public virtual void Delete(object id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var model = session.Load<DictType>(id);
                session.Delete(model);
                session.Flush();
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public virtual void Delete(DictType model)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.Delete(model);

                session.Flush();
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public virtual DictType Get(object id)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                return session.Get<DictType>(id);
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public virtual IList<DictType> LoadAll()
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                return session.QueryOver<DictType>().List();
            }
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="user_name"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public virtual IList<DictType> GetList(RequestParams request)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                ICriteria crit = session.CreateCriteria(typeof(DictType));
                IList<DictType> list = ManagerPage.GetCrit<DictType>(request,crit);
                return list;
            }
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expressionOrder"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public virtual IList<DictType> GetList(Expression<Func<DictType, bool>> expression, List<SortOrder<DictType>> expressionOrder, Pagination pagination)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                var query = session.QueryOver<DictType>().Where(expression);
                IList<DictType> list = ManagerPage.GetQueryOver<DictType>(query, expressionOrder, pagination);
                return list;
            }
        }

        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="user_name"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public virtual int GetCount(RequestParams request)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                ICriteria crit = session.CreateCriteria(typeof(DictType));
                int count = ManagerPage.GetCritCount<DictType>(request, crit);
                return count;
            }
        }

		#endregion  成员方法
	}
}