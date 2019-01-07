using System;
using System.Collections.Generic;
using System.Text;

namespace EF_Test
{
    public class BaseRepository<T>
    {
        public BaseRepository(BaseDbContext dbContext)
        {
            this.DbContent = dbContext;
            this.Table = this.DbContent.Set<T>();

        }
    }
}
