using Microsoft.Data.SqlClient;
using System.Configuration;
using University.DataModel;

namespace University.BLogic
{
    public class ProfessorManager
    {
        private readonly SqlConnection _connection = new();
        private SqlCommand _command = new();

        public static List<Professor> professorList = new();

        public List<Professor> GetProfessors()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("SELECT * FROM PROFESSOR", sqlCnn);
                using SqlDataReader dataReader = sqlCmd.ExecuteReader();

                while (dataReader.Read())
                {
                    professorList.Add(new Professor
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        FullName = dataReader["FullName"].ToString().Trim(),
                        DateOfBirth = DateTime.Parse(dataReader["DateOfBirth"].ToString()),
                        Faculty = FacultyManager.facultyList.Find(f => f.Id == (int.Parse(dataReader["FacultyId"].ToString()))),
                        Pay = decimal.Parse(dataReader["Pay"].ToString())
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            Console.WriteLine($"{professorList[0].FullName}, {professorList[0].DateOfBirth}, {professorList[0].Faculty.NameFaculty} ,{professorList[0].Pay}");
            return professorList;
        }
        public void AddProfessor()
        {
            Console.Clear();
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];

                Console.WriteLine("Inserire Id: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Nome completo: ");
                string name = Console.ReadLine();
                Console.WriteLine("Inserire Data di nascita: ");
                DateTime date = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Id Facoltà");
                int fId = int.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Stipendio: ");
                decimal pay = decimal.Parse(Console.ReadLine());

                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("INSERT INTO PROFESSOR" +
                                           "(Id,FullName, DateOfBirth, FacultyId, Pay) " +
                                           "VALUES " +
                                           "(@id, @name, @date, @fId, @pay)", sqlCnn);
                sqlCmd.Parameters.AddWithValue("@id", id);
                sqlCmd.Parameters.AddWithValue("@name", name);
                sqlCmd.Parameters.AddWithValue("@date", date);
                sqlCmd.Parameters.AddWithValue("@fId", fId);
                sqlCmd.Parameters.AddWithValue("@pay", pay);
                sqlCmd.ExecuteNonQuery();

                Professor p = new Professor
                {
                    Id = id,
                    FullName = name,
                    DateOfBirth = date,
                    Faculty = FacultyManager.facultyList.Find(f => f.Id == fId),
                    Pay = pay
                };
                professorList.Add(p);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ViewProfessor()
        {
            Console.Clear();
            Console.WriteLine("Elenco Professori: \n");
            foreach (Professor p in professorList)
            {
                Console.WriteLine($"Nome: {p.FullName}\nData di nascita: {p.DateOfBirth}\nFacoltà: {p.Faculty.NameFaculty}\nStipendio: {p.Pay}\n");
            }

        }
    }
}
