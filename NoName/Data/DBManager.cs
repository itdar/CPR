using System;
using NoName.Data;
using NoName.Data.DbData;

/// <summary>
/// Summary description for Class1
/// </summary>
public class DBManager
{
	private DBManager instance;

	private UserContext userDB;
	private DataContext dataDB;

	public UserContext UserDB => userDB;
	public DataContext DataDB => dataDB;

	private DBManager()
	{
		this.userDB = new UserContext();
		this.dataDB = new DataContext();
	}

	public DBManager GetInstance()
	{
		if (this.instance == null)
			this.instance = new DBManager();
		return this.instance;
	}
}
