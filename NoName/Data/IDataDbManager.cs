using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoName.Data.DbData;

namespace NoName.Data
{
    public interface IDataDbManager
    {
        IQueryable<TablePost> GetPosts(int boardNumber);
    }
}
