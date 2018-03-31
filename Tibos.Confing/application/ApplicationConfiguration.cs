using System;
using System.Collections.Generic;
using System.Text;

namespace Tibos.Confing.application
{
    public class ApplicationConfiguration
    {
        #region 属性成员

        /// <summary>
        /// 文件上传路径
        /// </summary>
        public string FileUpPath { get; set; }
        /// <summary>
        /// 是否启用单用户登录
        /// </summary>
        public bool IsSingleLogin { get; set; }
        /// <summary>
        /// 允许上传的文件格式
        /// </summary>
        public string AttachExtension { get; set; }
        /// <summary>
        /// 图片上传最大值KB
        /// </summary>
        public int AttachImagesize { get; set; }
        #endregion
    }
}
