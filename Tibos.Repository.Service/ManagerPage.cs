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
                   autofac config =  JsonConfigurationHelper.GetAppSettings<autofac>("autofac.json","autofac");
                    //string dbtype = "/bin/" + System.Configuration.ConfigurationManager.AppSettings["dbtype"];
                    //var path = HttpContext.Current.Server.MapPath(dbtype);
                    var path1 = HttpUtility.UrlPathEncode("");
                    var path = @"F:\GitProduct\NH.Core\Tibos.Confing\sqlconfig\mysql.cfg.xml";
                    var cfg = new NHibernate.Cfg.Configuration().Configure(path);
                    _sessionFactory = cfg.BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        public static IList<T> GetCrit<T>(List<SearchTemplate> list, List<SortOrder> order, ICriteria crit)
        {
            if (order == null)
            {
                crit.AddOrder(Order.Desc("id"));
            }
            else
            {
                foreach (var item in order)
                {
                    if (item.searchType.ToString() == EnumBase.OrderType.Asc.ToString())
                    {
                        crit.AddOrder(Order.Asc(item.value));
                    }
                    else
                    {
                        crit.AddOrder(Order.Desc(item.value));
                    }
                }
            }
            if (list != null)
            {
                crit = GetCrit(list, crit, 1);
            }
            return crit.List<T>();
        }

        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="crit"></param>
        /// <returns></returns>
        public static int GetCrit<T>(List<SearchTemplate> list, ICriteria crit)
        {
            if (list != null)
            {
                crit = GetCrit(list, crit, 2);
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
        private static ICriteria GetCrit(List<SearchTemplate> list, ICriteria crit, int type = 1)
        {
            foreach (var item in list)
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
                if (item.searchType.ToString() == EnumBase.SearchType.Paging.ToString() && type == 1)
                {
                    int[] paging = (int[])item.value;
                    crit.SetFirstResult((paging[0] - 1) * paging[1]);
                    crit.SetMaxResults(paging[1]);
                    continue;
                }
            }
            return crit;
        }

    }
}
