using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Catechize.Services
{
    public interface IDataAccess
    {
        // Internal helper for Sql server
        void ExecuteNonQuery(string query);
        void ExecuteNonQuery(DbCommand command);
        object ExecuteScalar(string query);
        object ExecuteScalar(DbCommand command);

        // Factory?
        DbConnection CreateConnect(string connectionString = null);
        DbCommand CreateCommand(string commandText = null);
        DbTransaction CreateTransaction(); 
    }
}
