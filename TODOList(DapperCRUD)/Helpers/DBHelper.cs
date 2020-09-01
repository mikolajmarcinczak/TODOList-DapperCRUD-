using System;
using System.Data.SqlClient;

namespace TODOList_DapperCRUD_.Helpers
{
    public class DBHelper
    {
        public static SqlConnection GetConnection() 
        {
            return new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TODOList;"); //DB Connection
        }
    }
}
