using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Landlord.Models
{
    public enum ColorType
    {
        /// <summary>
        /// 黑桃
        /// </summary>
        [Description("黑桃")]
        Spade = 1,
        /// <summary>
        /// 红桃
        /// </summary>
        [Description("红桃")]
        Heart = 2,
        /// <summary>
        /// 梅花
        /// </summary>
        [Description("梅花")]
        Club = 3,
        /// <summary>
        /// 方块
        /// </summary>
        [Description("方块")]
        Diamond = 4,
        /// <summary>
        /// 小王
        /// </summary>
        [Description("小王")]
        LittleJoker = 5,
        /// <summary>
        /// 大王
        /// </summary>
        [Description("大王")]
        BigJoker = 6,
    }
}
