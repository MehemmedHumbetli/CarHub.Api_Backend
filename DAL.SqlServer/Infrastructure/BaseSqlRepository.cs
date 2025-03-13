using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SqlServer.Infrastructure;

public class BaseSqlRepository(string connectionString)
{
    private readonly string _connectionString = connectionString;

    protected SqlConnection OpenConnection()
    {
        var conn = new SqlConnection(_connectionString);
        conn.Open();
        return conn;
    }
}
