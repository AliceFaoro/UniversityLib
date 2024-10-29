using Microsoft.Data.SqlClient;

namespace University.BLogic
{
    //This class saves in a database's table all the exceptions
    public class ExceptionLogManager
    {
        private readonly SqlConnection _connection = new();
        private SqlCommand _command = new();

        public void ExcLog(Exception ex, string connectionString)
        {
            DateTime date = DateTime.Now;
            string message = ex.Message;

            using (var sqlCnn = new SqlConnection(connectionString))
            {
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
}
