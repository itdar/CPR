using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NoName.Data.DbData;

namespace NoName.Data
{
    public interface IDataDbManager
    {
        IQueryable<TablePost> GetPosts(int boardNumber);
        Task<EntityEntry<TablePost>> AddPostAsync(TablePost post);
        IQueryable<TablePost> SearchInTitle(string searchString);
        IQueryable<TablePost> SearchInContents(string searchString);
    }
}
