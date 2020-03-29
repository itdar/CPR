﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NoName.Data;
using NoName.Data.DbData;
using NoName.Data.DbUser;

/// <summary>
/// Summary description for Class1
/// </summary>
public class UserDbManager
{
	//private readonly ILogger<UserDbManager> _logger;
	private static  UserContext userContext;

	public UserDbManager(UserContext _userContext)
	{
		userContext = _userContext;
		//_logger = logger;
	}

    private static UserDbManager instance;

	public UserContext UserDB { get; }

    private UserDbManager()
	{
		this.UserDB = new UserContext();
	}

	public static UserDbManager GetInstance()
	{
		if (instance == null)
			instance = new UserDbManager();

		return instance;
	}

	public IQueryable<TableUserJob> GetUserJobCodes(string userId)
	{
		return UserDB.UserJob.Where(userJob => userJob.ApplicationUser.Id == userId);
		//return UserDB.UserJob.Include(userJob => userJob.JobCode).Where(userJob => userJob.ApplicationUser.Id == userId).
		//		OrderByDescending(userJob => userJob.Number);

		//// Service + Manager 둘다 연결 되는지 테스트하려고 만듦
		//return UserDB.UserJob.Select.Include(userJob => userJob.JobCode).OrderByDescending(userJob => userJob.JobCode);
	}

	public IQueryable<ApplicationUser> GetLoggedInUser()
	{

		return null;
	}
}
