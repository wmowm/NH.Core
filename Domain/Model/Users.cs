using System;
//Nhibernate Code Generation Template 1.0
//author:tibos
//blog:www.cnblogs.com/tibos
//Entity Code Generation Template
namespace Domain
{
    //会员主表
    public class Users
    {

        /// <summary>
        /// 自增ID
        /// </summary>
        public virtual int id
        {
            get;
            set;
        }
        /// <summary>
        /// 用户组ID
        /// </summary>
        public virtual int? group_id
        {
            get;
            set;
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string user_name
        {
            get;
            set;
        }
        /// <summary>
        /// 6位随机字符串,加密用到
        /// </summary>
        public virtual string salt
        {
            get;
            set;
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        public virtual string password
        {
            get;
            set;
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string mobile
        {
            get;
            set;
        }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public virtual string email
        {
            get;
            set;
        }
        /// <summary>
        /// 用户头像
        /// </summary>
        public virtual string avatar
        {
            get;
            set;
        }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public virtual string nick_name
        {
            get;
            set;
        }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual string sex
        {
            get;
            set;
        }
        /// <summary>
        /// 生日
        /// </summary>
        public virtual DateTime? birthday
        {
            get;
            set;
        }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string telphone
        {
            get;
            set;
        }
        /// <summary>
        /// 所属地区逗号分隔
        /// </summary>
        public virtual string area
        {
            get;
            set;
        }
        /// <summary>
        /// 详情地址
        /// </summary>
        public virtual string address
        {
            get;
            set;
        }
        /// <summary>
        /// QQ号码
        /// </summary>
        public virtual string qq
        {
            get;
            set;
        }
        /// <summary>
        /// MSN号码
        /// </summary>
        public virtual string msn
        {
            get;
            set;
        }
        /// <summary>
        /// 账户余额
        /// </summary>
        public virtual decimal? amount
        {
            get;
            set;
        }
        /// <summary>
        /// 账户积分
        /// </summary>
        public virtual int? point
        {
            get;
            set;
        }
        /// <summary>
        /// 升级经验值
        /// </summary>
        public virtual int? exp
        {
            get;
            set;
        }
        /// <summary>
        /// 账户状态,0正常,1待验证,2待审核,3锁定
        /// </summary>
        public virtual int? status
        {
            get;
            set;
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        public virtual DateTime? reg_time
        {
            get;
            set;
        }
        /// <summary>
        /// 注册IP
        /// </summary>
        public virtual string reg_ip
        {
            get;
            set;
        }
        /// <summary>
        /// 是否为渠道商
        /// </summary>
        public virtual bool? IsDitch
        {
            get;
            set;
        }
        /// <summary>
        /// realName
        /// </summary>
        public virtual string realName
        {
            get;
            set;
        }
        /// <summary>
        /// 代理商标识
        /// </summary>
        public virtual int? Invite
        {
            get;
            set;
        }
        /// <summary>
        /// LoginTime
        /// </summary>
        public virtual DateTime? LoginTime
        {
            get;
            set;
        }
        /// <summary>
        /// YB
        /// </summary>
        public virtual int? YB
        {
            get;
            set;
        }

    }
}