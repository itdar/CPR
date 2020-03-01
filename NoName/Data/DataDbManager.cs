using System;
using NoName.Data.DbData;

namespace NoName.Data
{

	/// <summary>
	/// Summary description for Class1
	/// </summary>
	public class DataDbManager
	{
		private static DataDbManager instance;

		public DataContext DataDB { get; }

		private DataDbManager()
		{
			DataDB = new DataContext();
		}

		public static DataDbManager GetInstance()
		{
			if (instance == null)
				instance = new DataDbManager();
			return instance;
		}
	}
}