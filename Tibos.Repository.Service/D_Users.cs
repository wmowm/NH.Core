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
namespace Tibos.Repository.Service
{
	/// <summary>
	/// 接口层D_Users
	/// </summary>
	public class D_Users:IUsers
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
        public virtual object Save(Users model)
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
        public virtual void Update(Users model)
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
        public virtual void Delete(int id)
		{
			using (var session = sessionFactory.OpenSession())
            {
                var customer = session.Load<Users>(id);
                session.Delete(customer);
                session.Flush();
            }
		}

		/// <summary>
		/// 删除数据
		/// </summary>
        public virtual void Delete(Users model)
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
        public virtual Users Get(object id)
		{
			using (ISession session = sessionFactory.OpenSession())
            {
                return session.Get<Users>(id);
            }
		}
		

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public virtual IList<Users> LoadAll()
		{
			using (ISession session = sessionFactory.OpenSession())
            {
                return session.QueryOver<Users>().List();
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
        public virtual IList<Users> GetList(List<SearchTemplate> st, List<SortOrder> order)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                ICriteria crit = session.CreateCriteria(typeof(Users));
                IList<Users> customers = ManagerPage.GetCrit<Users>(st, order, crit);
                return customers;
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
        public virtual int GetCount(List<SearchTemplate> st)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                ICriteria crit = session.CreateCriteria(typeof(Users));
                int count = ManagerPage.GetCrit<Users>(st, crit);
                return count;
            }
        }

		#endregion  成员方法

       
	} 
}