using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NoName.Data.DbData;

/*
  Drop-Database -Context UserContext
  Add-Migration -Context UserContext -OutputDir Migrations/DbUser/ UserDbMigration
  Update-Database -Context UserContext

  Drop-Database -Context DataContext
  Add-Migration -Context DataContext -OutputDir Migrations/DbData/ DataDbMigration
  Update-Database -Context DataContext
*/

namespace NoName.Data
{

	/// <summary>
	/// Summary description for Class1
	/// </summary>
	public class DataDbManager
	{
		private static DataDbManager instance;

		public DataContext dataContext;

		public DataDbManager()
		{
			if (dataContext == null)
				dataContext = new DataContext();
		}

		public DataDbManager(DataContext _dataContext)
		{
			dataContext = _dataContext;
		}

		public static DataDbManager GetInstance()
		{
			if (instance == null)
				instance = new DataDbManager();

			return instance;
		}

		//public Task WriteMessage(string message)
		//{
		//	_logger.LogInformation(
		//		"MyDependency.WriteMessage called. Message: {MESSAGE}",
		//		message);

		//	return Task.FromResult(0);
		//}

		public IQueryable<TablePost> GetPosts(int boardId)
		{
			return dataContext.Post.Include(post => post.Board).Where(post => post.Board.BoardId == boardId).OrderByDescending(post => post.PostNumber);
		}
		public async Task<EntityEntry<TablePost>> AddPostAsync(TablePost post)
		{
			var ret = dataContext.Post.Add(post);
			await dataContext.SaveChangesAsync();
			return ret;
		}
		public IQueryable<TablePost> SearchInTitle(string searchString)
		{
			// Use LINQ to get list of Job.
			IQueryable<string> genreQuery = from m in dataContext.Post
											orderby m.PostNumber
											select m.UserId;

			var posts = from all in dataContext.Post select all;
			if (!string.IsNullOrEmpty(searchString))
			{
				posts = posts.Where(post => post.Title.Contains(searchString)).OrderByDescending(post => post.PostNumber);
			}
			return posts;
		}
		public IQueryable<TablePost> SearchInContents(string searchString)
		{
			var posts = from all in dataContext.Post select all;
			if (!string.IsNullOrEmpty(searchString))
			{
				posts = posts.Where(post => post.Content.Contains(searchString)).OrderByDescending(post => post.PostNumber);
			}
			return posts;
		}
		public IQueryable<TablePost> SearchInBoth(string searchString)
		{
			var posts = from all in dataContext.Post select all;
			if (!string.IsNullOrEmpty(searchString))
			{
				posts = posts.Where(post => post.Title.Contains(searchString) || post.Content.Contains(searchString)).OrderByDescending(post => post.PostNumber);
			}
			return posts;
		}
	}
}