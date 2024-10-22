using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace University.BLogic
{
    public class ExceptionLogManager
    {
        private readonly SqlConnection _connection = new();
        private SqlCommand _command = new();

        public void ExcLog(Exception ex)
        {
            DateTime date = DateTime.Now;
            string message = ex.Message;

            _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            using SqlConnection sqlCnn = new(_connection.ConnectionString);
            sqlCnn.Open();
            using SqlCommand sqlCmd = new("INSERT INTO ExceptionLog" +
                                           "(Date, Message) " +
                                           "VALUES " +
                                           "(@date, @message)", sqlCnn);
            sqlCmd.Parameters.AddWithValue("@date", date);
            sqlCmd.Parameters.AddWithValue("@message", message);
            sqlCmd.ExecuteNonQuery();
        }

    }
}
