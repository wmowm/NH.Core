using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Tibos.Test;

namespace Test
{

    class Model
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            List<Model> list = new List<Model>();
            List<List<Model>> res = new List<List<Model>>();
            //初始化数据
            for (int i = 0; i < 10000; i++)
            {
                var heigth = new Random().Next(1100, 1250);
                var width = new Random().Next(1200, 1300);
                //对数据进行整理 width>= height
                if(heigth > width)
                {
                    list.Add(new Model() { Height = width, Width = heigth });
                }
                else
                {
                    list.Add(new Model() { Height = heigth, Width = width });
                }
            }
            //以最小的width为基准
            while (list.Count>0)
            {
                var min_width_model = list.OrderBy(m => m.Width).First();
                var min_list = list.Where(m => m.Width <= min_width_model.Width + 3).ToList();
                //对height拆分
                while(min_list.Count > 0)
                {
                    var min_height_model = min_list.OrderBy(m => m.Height).First();
                    var height_min_list = min_list.Where(m => m.Height <= min_height_model.Height + 3).ToList();
                    res.Add(height_min_list);
                    min_list.RemoveAll(m => height_min_list.Contains(m));
                    list.RemoveAll(m => height_min_list.Contains(m));
                }
            }

            for (int i = 0; i < res.Count; i++)
            {
                Console.WriteLine($"第{i}组数据,共{res[i].Count}条");
                foreach (var item in res[i])
                {
                    Console.WriteLine($"-->Height:{item.Height},Width:{item.Width}");
                }
            }
            Console.Read();
        }
    }
}
