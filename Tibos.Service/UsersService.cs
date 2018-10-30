using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibos.Domain;
using Tibos.Common;
using Tibos.Repository.Contract;
using Tibos.Service.Contract;
using System.Linq.Expressions;

namespace Tibos.Service
{
	public partial class UsersService:UsersIService
    {

        private readonly IUsers dao;
        public UsersService(IUsers dao)
		{
            this.dao = dao;
        }
		//这个里面是通用方法,实现增删改查排序(动软代码生成器自动生成)
		#region  Method
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Users Get(int id) 
        {
            return dao.Get(id);
        }
        public RequestParams GetWhere(UsersRequest request)
        {
            RequestParams rp = new RequestParams();
            //追加查询参数
            if (!string.IsNullOrEmpty(request.email))
            {
                rp.Params.Add(new Params() { key = "email", value = request.email, searchType = EnumBase.SearchType.Eq });
            }
            //添加排序(多个排序条件,可以额外添加)
            if (!string.IsNullOrEmpty(request.sortKey))
            {
                rp.Sort.Add(new Sort() { key = request.sortKey, searchType = (EnumBase.OrderType)request.sortType });
            }
            else
            {
                rp.Sort = null;
            }

            //添加分页
            if (request.pageIndex > 0)
            {
                rp.Paging.pageIndex = request.pageIndex;
                rp.Paging.pageSize = request.pageSize;
            }
            else
            {
                rp.Paging = null;
            }
            return rp;
        }


        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public IList<Users> GetList(UsersRequest request) 
        {
            RequestParams rp = GetWhere(request);
            return dao.GetList(rp);
        }

        public IList<Users> GetList(Expression<Func<Users, bool>> expression, List<SortOrder<Users>> expressionOrder, Pagination pagination)
        {
            return dao.GetList(expression, expressionOrder, pagination);
        }

        /// <summary>
        /// 获取当前条件下的用户总记录
        /// </summary>
        /// <param name="user_name"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public int GetCount(UsersRequest request)
        {
            RequestParams rp = GetWhere(request);
            return dao.GetCount(rp);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="m_user"></param>
        /// <returns></returns>
        public int Save(Users m_user) 
        {
            return Convert.ToInt32(dao.Save(m_user));
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="m_user"></param>
        /// <returns></returns>
        public void Update(Users m_user) 
        {
            dao.Update(m_user);
        }

        public void Delete(int id)
        {
            dao.Delete(id);
        }

        public bool Exists(int id) 
        {
            return dao.Exists(id);
        }
		#endregion

        #region 自定义

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="un"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public Users Login(string un, string pwd)
        {
            List<SearchTemplate> st = new List<SearchTemplate>()
            {
                new SearchTemplate(){key="user_name",value=un,searchType=EnumBase.SearchType.Eq},
                new SearchTemplate(){key="password",value=pwd,searchType=EnumBase.SearchType.Eq}
            };
            IList<Users> list = GetList(m => m.user_name == un && m.password == pwd, null, null);
            if (list.Count > 0)
            {
                return list[0];
            }
            return null;
        }
        #endregion
    }
}