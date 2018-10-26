using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tibos.Test
{
    public class Growing
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public const string key = "b3adb7e5a168e167";
        /// <summary>
        /// 充币数量
        /// </summary>
        public const string depositAmount = "depositAmount";
        /// <summary>
        /// 充币成功次数
        /// </summary>
        public const string depositSuccess = "depositSuccess";
        /// <summary>
        /// 币币交易成功次数
        /// </summary>
        public const string trading = "Trading";
        /// <summary>
        /// 币币交易额
        /// </summary>
        public const string tradingAmount = "tradingAmount";


        /// <summary>
        /// 测试
        /// </summary>
        public const string testAA = "testAA";

        public static void Start()
        {
            M_Growing model = new M_Growing()
            {
                tm = ConvertToUnixTimeStampSeconds(DateTime.Now),
                n =  testAA,
                num = 5,
            };
            var data = JsonConvert.SerializeObject(model);
            var res = SendGrowing(data);
        }

        public static string  SendGrowing(string data)
        {
            var ai = key;
            var sendingTime = ConvertToUnixTimeStampSeconds(DateTime.Now);
            var url = $"https://api.growingio.com/v3/{ai}/s2s/cstm?stm={sendingTime}";
            var res = HttpCommon.HttpPost(url, data);
            return res;
        }
        

        /// <summary>
        /// 转换为格林时间戳，秒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ConvertToUnixTimeStampSeconds(DateTime dateTime)
        {
            //当地时区
            System.DateTime localStartTime = TimeZoneInfo.ConvertTimeFromUtc(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local);
            //相差秒数
            long timeStamp = (long)(dateTime - localStartTime).TotalSeconds;
            return timeStamp;
        }
    }

    [Serializable]
    public class M_Growing
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long tm { get; set; }

        /// <summary>
        /// 事件表示
        /// </summary>
        public string n { get; set; }

        /// <summary>
        /// 数值
        /// </summary>
        public int num { get; set; }

        /// <summary>
        /// 对象
        /// </summary>
        public object var { get; set; }
    }
}
