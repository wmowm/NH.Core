using Tibos.Domain;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Dialect;
using NHibernate.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using Tibos.ConfingModel.model;
using System.Xml;
using MySql.Data;
//using System.Data.SQLite;
using Tibos.Common;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Tibos.ConfingModel;
using System.Linq.Expressions;
using NHibernate.Criterion.Lambda;

namespace Tibos.Repository.Service
{
    public class ManagerPage
    {
        private static ISessionFactory _sessionFactory;
        /// <summary>
        /// 链接信息,初始化NH
        /// </summary>
        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    ManageConfig config =  JsonConfigurationHelper.GetAppSettings<ManageConfig>("ManageConfig.json", "ManageConfig");
                    var path = AppContext.BaseDirectory + config.DBType;
                    var cfg = new NHibernate.Cfg.Configuration().Configure(path);
                    _sessionFactory = cfg.BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        public static IList<T> GetCrit<T>(RequestParams request, ICriteria crit)
        {
            if(request.Sort != null)
            {
                foreach (var item in request.Sort)
                {
                    if (item.searchType.ToString() == EnumBase.OrderType.Asc.ToString())
                    {
                        crit.AddOrder(Order.Asc(item.key));
                    }
                    else
                    {
                        crit.AddOrder(Order.Desc(item.key));
                    }
                }
            }
            if (request.Params != null)
            {
                crit = GetCrit(request.Params, crit);
            }
            if (request.Paging != null)
            {
                crit.SetFirstResult((request.Paging.pageIndex - 1) * request.Paging.pageSize);
                crit.SetMaxResults(request.Paging.pageSize);
            }
            return crit.List<T>();
        }

        public static IList<T> GetQueryOver<T>(IQueryOver<T, T> query, List<SortOrder<T>> expressionOrder, Pagination pagination)
        {
            if (expressionOrder != null)
            {
                for (int i = 0; i < expressionOrder.Count; i++)
                {
                    var model = expressionOrder[i];
                    IQueryOverOrderBuilder<T, T> sort;
                    if (i > 0)
                    {
                        sort = query.ThenBy(model.value);
                    }
                    else
                    {
                        sort = query.OrderBy(model.value);
                    }
                    if (model.searchType == EnumBase.OrderType.Asc)
                    {
                        query = sort.Asc;
                    }
                    else
                    {
                        query = sort.Desc;
                    }
                }
            }
            if (pagination != null)
            {
                query.Skip((pagination.pageIndex - 1) * pagination.pageSize);
                query.Take(pagination.pageSize);
            }
            var list = query.List<T>();
            return list;
        }
    
        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="crit"></param>
        /// <returns></returns>
        public static int GetCritCount<T>(RequestParams request, ICriteria crit)
        {
            if (request.Params != null)
            {
                crit = GetCrit(request.Params, crit,IsGetCount:true);
            }
            return Convert.ToInt32(crit.SetProjection(Projections.RowCount()).UniqueResult());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="crit"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static ICriteria GetCrit(List<Params> para, ICriteria crit,bool IsGetCount= false)
        {
            foreach (var item in para)
            {
                if (item.value == null) continue;
                if (item.value.GetType() == typeof(String))
                {
                    if (item.value.ToString() == "") continue;
                }
                if (item.searchType.ToString() == EnumBase.SearchType.Eq.ToString())
                {
                    crit.Add(Restrictions.Eq(item.key, item.value));
                    continue;
                }
                if (item.searchType.ToString() == EnumBase.SearchType.Gt.ToString())
                {
                    crit.Add(Restrictions.Gt(item.key, item.value));
                    continue;
                }
                if (item.searchType.ToString() == EnumBase.SearchType.Ge.ToString())
                {
                    crit.Add(Restrictions.Ge(item.key, item.value));
                    continue;
                }
                if (item.searchType.ToString() == EnumBase.SearchType.Lt.ToString())
                {
                    crit.Add(Restrictions.Lt(item.key, item.value));
                    continue;
                }
                if (item.searchType.ToString() == EnumBase.SearchType.Le.ToString())
                {
                    crit.Add(Restrictions.Le(item.key, item.value));
                    continue;
                }
                if (item.searchType.ToString() == EnumBase.SearchType.IsNull.ToString())
                {
                    crit.Add(Restrictions.IsNull(item.key));
                    continue;
                }
                if (item.searchType.ToString() == EnumBase.SearchType.IsNotNull.ToString())
                {
                    crit.Add(Restrictions.IsNotNull(item.key));
                    continue;
                }
                if (item.searchType.ToString() == EnumBase.SearchType.Like.ToString())
                {
                    crit.Add(Restrictions.Like(item.key, item.value + "%"));
                    continue;
                }
                if (item.searchType.ToString() == EnumBase.SearchType.StartLike.ToString())
                {
                    crit.Add(Restrictions.Like(item.key, "%" + item.value));
                    continue;
                }
                if (item.searchType.ToString() == EnumBase.SearchType.In.ToString())
                {
                    crit.Add(Restrictions.In(item.key, (List<object>)item.value));
                    continue;
                }
                if (item.searchType.ToString() == EnumBase.SearchType.NotIn.ToString())
                {
                    crit.Add(Restrictions.Not(Restrictions.In(item.key, (List<object>)item.value)));
                    continue;
                }
                if (item.searchType.ToString() == EnumBase.SearchType.Group.ToString())
                {
                    crit.SetProjection(Projections.ProjectionList()
                        .Add(Projections.GroupProperty(item.value.ToString()))
                        .Add(Projections.RowCount()));
                }

            }
            return crit;
        }

    }
}
