using System;
using System.Collections.Generic;
using System.Linq;

namespace EF_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (BaseDbContext db = new BaseDbContext())
            {
                var res = from dict in db.Dict
                          join dt in db.DictType
                          on dict.Tid equals dt.Id
                          where dict.Name == "男"
                          select new Dict
                          {
                              Name = dict.Name
                          };
               var list = res.ToList();
            }


            Console.Read();
        }



        public  List<Dict> GetList()
        {
            using (BaseDbContext db = new BaseDbContext())
            {
                var query = db.Dict.AsQueryable();
                //query = query.OrderBy(p => p.Sort).ThenByDescending(p => p.CreateTime);
                var list = query.ToList();
                return list;
            }
        }

        public List<Dict> GetList(int pageIndex,int pageSize)
        {
            using (BaseDbContext db = new BaseDbContext())
            {
                var query = db.Dict.Skip(pageIndex * pageSize).Take(pageSize);
                //query = query.OrderBy(p => p.Sort).ThenByDescending(p => p.CreateTime);
                var list = query.ToList();
                return list;
            }
        }

    }
}
