using System;
using NoName.Data;
using NoName.Data.DbData;

/// <summary>
/// Summary description for Class1
/// </summary>
public class UserDbManager
{
	private UserDbManager instance;

	public UserContext UserDB { get; }

    private UserDbManager()
	{
		this.UserDB = new UserContext();
	}

	public UserDbManager GetInstance()
	{
		if (this.instance == null)
			this.instance = new UserDbManager();
		return this.instance;
	}
}
