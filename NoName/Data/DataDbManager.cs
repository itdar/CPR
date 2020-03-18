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

namespace NoName.Data
{

	/// <summary>
	/// Summary description for Class1
	/// </summary>
	public class DataDbManager : IDataDbManager
	{
		private readonly ILogger<DataDbManager> _logger;
		private readonly DataContext _dataContext;
		public DataDbManager(DataContext dataContext, ILogger<DataDbManager> logger)
		{
			_dataContext = dataContext;
			_logger = logger;
		}

		public Task WriteMessage(string message)
		{
			_logger.LogInformation(
				"MyDependency.WriteMessage called. Message: {MESSAGE}",
				message);

			return Task.FromResult(0);
		}
		public IQueryable<TablePost> GetPosts(int boardNumber)
		{
			return _dataContext.Post.Include(post => post.Board).Where(post => post.Board.BoardNumber == boardNumber).OrderByDescending(post => post.PostNumber);
		}
		public async Task<EntityEntry<TablePost>> AddPostAsync(TablePost post)
		{
			var ret = _dataContext.Post.Add(post);
			await _dataContext.SaveChangesAsync();
			return ret;
		}
		public IQueryable<TablePost> SearchInTitle(string searchString)
		{
			// Use LINQ to get list of Job.
			IQueryable<string> genreQuery = from m in _dataContext.Post
											orderby m.PostNumber
											select m.UserId;

			var posts = from all in _dataContext.Post select all;
			if (!string.IsNullOrEmpty(searchString))
			{
				posts = posts.Where(post => post.Title.Contains(searchString)).OrderByDescending(post => post.PostNumber);
			}
			return posts;
		}
		public IQueryable<TablePost> SearchInContents(string searchString)
		{
			var posts = from all in _dataContext.Post select all;
			if (!string.IsNullOrEmpty(searchString))
			{
				posts = posts.Where(post => post.Content.Contains(searchString)).OrderByDescending(post => post.PostNumber);
			}
			return posts;
		}
		public IQueryable<TablePost> SearchInBoth(string searchString)
		{
			var posts = from all in _dataContext.Post select all;
			if (!string.IsNullOrEmpty(searchString))
			{
				posts = posts.Where(post => post.Title.Contains(searchString) || post.Content.Contains(searchString)).OrderByDescending(post => post.PostNumber);
			}
			return posts;
		}

		/*
		private static DataDbManager instance;
		
		public DataContext DataDB { get; }
		public object ServiceProviderFactory { get; }

		private DataDbManager()
		{
			DataDB = new DataContext();
			//var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
			

			//DataDB = serviceCollection..GetServices(typeof(DataContext));
			//DataDB = serviceProvider.GetService<DataContext>();
		}
		private DataDbManager(DataContext dataContext)
		{
			DataDB = dataContext;
		}
		public static DataDbManager GetInstance()
		{
			if (instance == null)
				instance = new DataDbManager();
			return instance;
		}
		public static void InitInstance(DataContext dataContext)
		{
			instance = new DataDbManager(dataContext);
		}
		*/
	}
}