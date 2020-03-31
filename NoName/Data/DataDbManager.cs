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

/* PM Console commands
  Drop-Database -Context UserContext
  Add-Migration -Context UserContext -OutputDir Migrations/DbUser/ UserDbMigration
  Update-Database -Context UserContext

  Drop-Database -Context DataContext
  Add-Migration -Context DataContext -OutputDir Migrations/DbData/ DataDbMigration
  Update-Database -Context DataContext
*/

namespace NoName.Data
{
	public class DataDbManager
	{
		private static DataDbManager instance;
		public DataContext dataContext;
		private DataDbManager()
		{
			if (dataContext == null)
				dataContext = new DataContext();
		}
		//public DataDbManager(DataContext _dataContext)
		//{
		//	dataContext = _dataContext;
		//}
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


		/************************************BOARD************************************/
		public TableBoard GetBoard(int? boardId)
		{
			return dataContext.Board.FirstOrDefault(board => board.BoardId == boardId);
		}
		public IQueryable<TableBoard> GetBoards(int jobCode)
		{
			return dataContext.Board.Include(board => board.Job).Where(board => board.JobCode == jobCode).
				OrderByDescending(board => board.BoardId);
		}
		public int GetBoardCount(int jobCode)
		{
			return dataContext.Board.Include(board => board.Job).Where(board => board.JobCode == jobCode).Count();
		}
		public string GetBoardName(int boardId)
		{
			if (dataContext.Board.Count() > 0)
				return dataContext.Board.FirstOrDefault(board => board.BoardId == boardId).BoardName;
			else
				return "There is no Board.";
		}

		/************************************POST************************************/
		public IQueryable<TablePost> GetPosts(int boardId)
		{
			return dataContext.Post.Include(post => post.Board).Where(post => post.Board.BoardId == boardId).
				OrderByDescending(post => post.PostNumber);
		}
		public IQueryable<TablePost> GetPosts(int? boardId,int listNumber)
		{
			var index = dataContext.Post.Where(post => post.BoardId == boardId).Count() - listNumber + 1;
			return  dataContext.Post.Where(post => post.BoardId == boardId).
				OrderByDescending(post => post.PostNumber).Take(listNumber);

		}
		public TablePost GetPostDetail(int postNumber)
		{
			return dataContext.Post.FirstOrDefault(post => post.PostNumber == postNumber);
		}
		public int GetPostsCount(int? boardId)
		{
			return dataContext.Post.Where(post => post.BoardId == boardId).Count();
		}
		public async Task<EntityEntry<TablePost>> AddPostAsync(TablePost post)
		{
			var ret = dataContext.Post.Add(post);
			await dataContext.SaveChangesAsync();
	
			return ret;
		}
		//public void EditPost(TablePost ModifiedPost)
		//{
		//	dataContext.Attach(ModifiedPost).State = EntityState.Modified;
		//}

		/************************************POST_SEARCH************************************/
		public IQueryable<TablePost> SearchInTitle(string searchString)
		{
			// Use LINQ to get list of Job.
			IQueryable<string> genreQuery = from m in dataContext.Post
											orderby m.PostNumber
											select m.Id;

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

		/************************************COMMENT************************************/
		public List<TableComment> GetParentComments(int postNumber)
		{
			var parentComments = dataContext.Comment.Where(comment => comment.PostNumber == postNumber && comment.ParentCommentNumber == 0).
				OrderBy(comment => comment.CreatedTime).ToList();

			return parentComments;
		}
		public List<TableComment> GetChildComments(int postNumber)
		{
			var childComments = dataContext.Comment.Where(comment => comment.PostNumber == postNumber && comment.ParentCommentNumber != 0).
				OrderBy(comment => comment.ParentCommentNumber).ThenBy(comment => comment.CreatedTime).ToList();

			return childComments;
		}
		public async Task<EntityEntry<TableComment>> AddCommnetAsync(TableComment comment)
		{
			var ret = dataContext.Comment.Add(comment);
			await dataContext.SaveChangesAsync();
			return ret;
		}
	}
}