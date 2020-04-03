using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NoName.BackendClass.Login;
using NoName.Data;
using NoName.Data.DbData;
using NoName.Data.DbUser;

/// <summary>
/// Summary description for Class1
/// </summary>
public class UserDbManager
{
	private static  UserContext userContext;

	public UserDbManager(UserContext _userContext)
	{
		userContext = _userContext;
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

	/*
	 * email로 해당 유저를 찾아서 UserInformation 에 넣어줌
	 * 반환값으로 유저정보를 받아서 외부에서 UserInformation 에 넣어줘야 깨끗함
	 * 바꿔야함
	 */
	public void SetLoggedInUserInfoUsingEmail(string _email)
	{
		var loggedInUser = UserDB.User.Where(user => user.Email == _email);
		var userId = "";
		var userName = "";
		var userEmail = "";
		var userJobCodes = new List<int>();
		var jobCodes = UserDB.UserJob.Where(userJob => userJob.ApplicationUser.Email == _email);
		foreach (var user in loggedInUser)
		{
			userId = user.Id;
			userName = user.UserName;
			userEmail = user.Email;
		}
		foreach (var jobCode in jobCodes)
		{
			userJobCodes.Add(jobCode.JobCode);
		}

		UserInformation.GetInstance().SetInformation(userId, userName, userEmail, userJobCodes);
	}

	public IQueryable<ApplicationUser> GetLoggedInUser()
	{

		return null;
	}

	/*
	 * 단순 로그인된 유저 정보 확인 디버깅용 함수
	 */
	public void CheckLoggedInUserInformation()
	{
		var userInfo = UserInformation.GetInstance();

		System.Diagnostics.Debug.WriteLine("UserId : " + userInfo.UserId);
		System.Diagnostics.Debug.WriteLine("UserName : " + userInfo.UserName);
		System.Diagnostics.Debug.WriteLine("Email : " + userInfo.Email);
		for (int i = 0; i < userInfo.JobCodes.Count(); ++i)
		{
			System.Diagnostics.Debug.WriteLine("JobCode user have : {0}", userInfo.JobCodes.ElementAt(i));
		}
	}
}
