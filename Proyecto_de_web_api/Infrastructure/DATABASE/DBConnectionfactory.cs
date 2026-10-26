using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using Microsoft.Data.SqlClient;

namespace Infrastructure.DATABASE
{
    public class DBConnectionfactory
    {
        private readonly String _databaseName;

        public DBConnectionfactory(String databaseName)
        {
            _databaseName = databaseName;
        }

        public SqlConnection Createconection()
        {
            return new SqlConnection(_databaseName);
        }
    }
}
