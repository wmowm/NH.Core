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
	public class NavigationRoleDao:INavigationRole
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
        public virtual object Save(NavigationRole model)
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
        public virtual void Update(NavigationRole model)
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
                var model = session.Load<NavigationRole>(id);
                session.Delete(model);
                session.Flush();
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public virtual void Delete(NavigationRole model)
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
        public virtual NavigationRole Get(object id)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                return session.Get<NavigationRole>(id);
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public virtual IList<NavigationRole> LoadAll()
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                return session.QueryOver<NavigationRole>().List();
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
        public virtual IList<NavigationRole> GetList(RequestParams request)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                ICriteria crit = session.CreateCriteria(typeof(NavigationRole));
                IList<NavigationRole> list = ManagerPage.GetCrit<NavigationRole>(request,crit);
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
        public virtual IList<NavigationRole> GetList(Expression<Func<NavigationRole, bool>> expression, List<SortOrder<NavigationRole>> expressionOrder, Pagination pagination)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                var query = session.QueryOver<NavigationRole>().Where(expression);
                IList<NavigationRole> list = ManagerPage.GetQueryOver<NavigationRole>(query, expressionOrder, pagination);
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
                ICriteria crit = session.CreateCriteria(typeof(NavigationRole));
                int count = ManagerPage.GetCritCount<NavigationRole>(request, crit);
                return count;
            }
        }

		#endregion  成员方法
	}
}