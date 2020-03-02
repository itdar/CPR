using System;
using NoName.Data;
using NoName.Data.DbData;

/// <summary>
/// Summary description for Class1
/// </summary>
public class UserDbManager
{
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
}
