using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Landlord.Models
{
    public class Card
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }

        /// <summary>
        /// 序号(1-54)
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 花色
        /// </summary>
        public ColorType ColorType { get; set; }


    }
}
