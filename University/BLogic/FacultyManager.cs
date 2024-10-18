using Microsoft.Data.SqlClient;
using System.Configuration;
using University.DataModel;


namespace University.BLogic
{
    public class FacultyManager
    {
        private readonly SqlConnection _connection = new();
        private SqlCommand _command = new();
        
        public static List<Faculty> facultyList = new();

        public List<Faculty> GetFaculties()
        {

            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("SELECT * FROM FACULTY", sqlCnn);
                using SqlDataReader dataReader = sqlCmd.ExecuteReader();
                Console.Clear();
                while (dataReader.Read())
                {

                    facultyList.Add(new Faculty
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        NameFaculty = dataReader["NameFaculty"].ToString().Trim()
                    });
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Errore");
                throw;
            }
            Console.WriteLine($"{facultyList[0].NameFaculty}");
            return facultyList;
        }
        public void AddFaculty()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("Inserire Id facoltà: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Nome facoltà: ");
                string name = Console.ReadLine();

                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("INSERT INTO FACULTY" +
                                           "([Id],[NameFaculty]) " +
                                           "VALUES " +
                                           "(@id, @name)", sqlCnn);
                sqlCmd.Parameters.AddWithValue("@id", id);
                sqlCmd.Parameters.AddWithValue("@name", name);
                sqlCmd.ExecuteNonQuery();

                Faculty f = new Faculty
                {
                    Id = id,
                    NameFaculty = name
                };
                FacultyManager.facultyList.Add(f);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void UpdateFaculty()
        {
            Console.Clear();
            Console.WriteLine("Inserire il nome della facoltà da aggiornare: ");
            string nome = Console.ReadLine();
            Faculty f = FacultyManager.facultyList.Find(f => f.NameFaculty.Equals(nome));
            Console.WriteLine("Inserire il nuovo nome:");
            string newName = Console.ReadLine();
            f.NameFaculty = newName;
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("UPDATE Faculty " +
                                               "SET NameFaculty = @newName " +
                                               "WHERE NameFaculty = @nome", sqlCnn);
                sqlCmd.Parameters.AddWithValue("@newName", newName);
                sqlCmd.Parameters.AddWithValue("@nome", nome);
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void ViewFaculties()
        {
            Console.Clear();
            Console.WriteLine("Elenco Facoltà: \n");
            foreach (Faculty f in FacultyManager.facultyList)
            {
                Console.WriteLine($"Nome: {f.NameFaculty}\n");
            }
        }
    }
}
