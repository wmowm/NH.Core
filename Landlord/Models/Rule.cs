using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Landlord.Models
{
    public class Rule
    {
        /// <summary>
        /// 纸牌集合
        /// </summary>
        public List<Card> card_list = new List<Card>();

        /// <summary>
        /// 创建牌
        /// </summary>
        /// <returns></returns>
        public List<Card> CreateCard()
        {
            card_list = new List<Card>();
            //1-52为基础牌,从3至2,53小鬼54大鬼
            for (int i = 1; i <= 54; i++)
            {
                Card card = new Card();
                card.Sort = i;

                //花色
                switch (i % 4)
                {
                    //方块
                    case 0:
                        card.ColorType = ColorType.Diamond;
                        break;
                    //黑桃
                    case 1:
                        card.ColorType = ColorType.Spade;
                        break;
                    //红桃
                    case 2:
                        card.ColorType = ColorType.Heart;
                        break;
                    //梅花
                    case 3:
                        card.ColorType = ColorType.Club;
                        break;
                }
                //昵称 3-10
                if (i <= 32)
                {
                    card.NickName = ((i - 1) / 4 + 3).ToString();
                }
                //昵称 J-K
                else
                {
                    if (i <= 36)
                    {
                        card.NickName = "J";
                    }
                    else if (i <= 40)
                    {
                        card.NickName = "Q";
                    }
                    else if (i <= 44)
                    {
                        card.NickName = "K";
                    }
                    else if (i <= 48)
                    {
                        card.NickName = "A";
                    }
                    else if (i <= 52)
                    {
                        card.NickName = "2";
                    }
                    else if (i == 53)
                    {
                        card.ColorType = ColorType.LittleJoker;
                        card.NickName = "LittleJoker";
                    }
                    else if (i == 54)
                    {
                        card.ColorType = ColorType.BigJoker;
                        card.NickName = "BigJoker";
                    }
                }
                card_list.Add(card);
            }
            return card_list;
        }

        /// <summary>
        /// 洗牌
        /// </summary>
        /// <returns></returns>
        public List<Card> Shuffle(List<Card> list)
        {
            Card model = new Card();
            for (int i = 0; i < list.Count; i++)
            {
                var m = new Random().Next(0, list.Count - 1);
                //调整位置
                model = list[i];
                list[i] = list[m];
                list[m] = model;
            }
            return list;
        }

        /// <summary>
        /// 发牌
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<List<Card>> Licensing(List<Card> list)
        {
            int num = 3;//斗地主人数
            List<List<Card>> arrayList = new List<List<Card>>();
            var pageIndex = (list.Count - num) / num;
            for (int i = 0; i < num+1; i++)
            {
                arrayList.Add(list.Skip(i * pageIndex).Take(pageIndex).ToList());
            }
            return arrayList;
        }

    }
}
