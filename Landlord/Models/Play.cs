using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Landlord.Models
{
    public class Play
    {

        /// <summary>
        /// 出牌规则检测
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool PlayingCardsCheck(List<Card> list)
        {
            //对子,王炸
            if (list.Count == 2)
            {
                if (list[0].NickName == list[1].NickName || (list[0].Sort + list[1].Sort == 107))
                    return true;
                else
                    return false;
            }
            //三不带
            if (list.Count == 3)
            {
                if (list[0].NickName == list[1].NickName && list[0].NickName == list[2].NickName)
                    return true;
                else
                    return false;
            }
            //三带一,炸弹
            if (list.Count == 4)
            {

            }
            //三带二,顺子
            if (list.Count == 5)
            {

            }
            //四带二,顺子,连对,飞机(不带)
            if (list.Count == 6)
            {

            }
            //顺子
            if (list.Count == 7)
            {

            }
            //飞机(2张),连对,顺子,四带两对
            if (list.Count == 8)
            {

            }
            //顺子,飞机(不带)
            if (list.Count == 9)
            {

            }
            //飞机(2对),顺子,连对
            if (list.Count == 10)
            {

            }
            //顺子
            if (list.Count == 11)
            {

            }
            //飞机(不带),飞机(3张),连对,顺子
            if (list.Count == 12)
            {

            }
            //
            if (list.Count == 13)
            {

            }
            //连对
            if (list.Count == 14)
            {

            }
            //飞机(3对),飞机(不带)
            if (list.Count == 15)
            {

            }
            //连对,飞机(4张)
            if (list.Count == 16)
            {

            }
            //
            if (list.Count == 17)
            {

            }
            //连对
            if (list.Count == 18)
            {

            }
            //
            if (list.Count == 19)
            {

            }
            //飞机(4对)
            if (list.Count == 20)
            {

            }
            return false;
        }
    }
}
