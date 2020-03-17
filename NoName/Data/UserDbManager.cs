using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NoName.Data;
using NoName.Data.DbData;

/// <summary>
/// Summary description for Class1
/// </summary>
public class UserDbManager
{
	private readonly ILogger<UserDbManager> _logger;
	private readonly UserContext _userContext;

	public UserDbManager(UserContext userContext, ILogger<UserDbManager> logger)
	{
		_userContext = userContext;
		_logger = logger;
	}
	private static UserDbManager instance;

	public UserContext UserDB { get; }

    private UserDbManager()
	{
		this.UserDB = new UserContext();
		//_userContext = userContext;
	}

	public static UserDbManager GetInstance()
	{
		if (instance == null)
			instance = new UserDbManager();
		return instance;
	}
}
