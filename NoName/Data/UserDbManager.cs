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

/* PM Console commands
  Drop-Database -Context UserContext
  Add-Migration -Context UserContext -OutputDir Migrations/DbUser/ UserDbMigration
  Update-Database -Context UserContext

  Drop-Database -Context DataContext
  Add-Migration -Context DataContext -OutputDir Migrations/DbData/ DataDbMigration
  Update-Database -Context DataContext
*/


public class UserDbManager
{
    private static UserContext userContext;

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
     * 로그인한 유저 직업 인증 단계 호출 함수?
     */
    public void GetLoggedInUserJobAuthentication()
    {

    }

    /*
     * 로그인한 유저 인증된 직업 리스트 JobCodes?
     */
     public List<int> GetLoggedInUserJobCodes()
    {

        return null;
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
        if (userInfo.JobCodes.Count() > 0)
        {
            for (int i = 0; i < userInfo.JobCodes.Count(); ++i)
            {
                System.Diagnostics.Debug.WriteLine("JobCode user have : {0}", userInfo.JobCodes.ElementAt(i));
            }
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("No jobCodes.");
        }
    }
}
