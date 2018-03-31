using System;
using System.Collections.Generic;
using Tibos.Common;
using Tibos.Domain;

namespace Tibos.Service.Contract
{
    public interface UsersIService
    {

        #region 自定义



        Users Get(int id);

        IList<Users> GetList(List<SearchTemplate> st, List<SortOrder> order);

        int GetCount(List<SearchTemplate> st);

        int Save(Users m_user);

        void Update(Users m_user);


        void Delete(int id);

        bool Exists(int id);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="un"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        Users Login(string un, string pwd);
        #endregion
    }
}
