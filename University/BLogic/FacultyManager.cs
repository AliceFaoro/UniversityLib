using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Linq.Expressions;
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

                    Faculty f = (new Faculty 
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        NameFaculty = dataReader["NameFaculty"].ToString().Trim()
                    });
                    facultyList.Add(f);

                }
            }
            catch (Exception)
            {
                Console.WriteLine("Errore");
                throw;
            }
            return facultyList;
        }
        
        public void GetFaculties2()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();

                foreach (Faculty facolta in FacultyManager.facultyList)
                {
                    int id = facolta.Id;
                    
                    //Recupero corsi facoltà
                    using SqlCommand sqlCmd = new("SELECT Id FROM Course WHERE FacultyId = @id", sqlCnn);
                    sqlCmd.Parameters.AddWithValue("@id", id);
                    using SqlDataReader dataReader = sqlCmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Course c = CourseManager.coursesList.Find(c => c.Id == int.Parse(dataReader["Id"].ToString()));
                        facolta.CoursesFaculty.Add(c);
                    }
                    dataReader.Close();

                    //Recupero Professori Facoltà
                    using SqlConnection sqlCnn1 = new(_connection.ConnectionString);
                    sqlCnn1.Open();
                    using SqlCommand sqlCmd1 = new("SELECT Id FROM Professor WHERE FacultyId = @id", sqlCnn1);
                    sqlCmd1.Parameters.AddWithValue("@id", id);
                    using SqlDataReader dataReader1 = sqlCmd1.ExecuteReader();
                    while (dataReader1.Read())
                    {
                        Professor p = ProfessorManager.professorList.Find(p => p.Id == int.Parse(dataReader1["Id"].ToString()));
                        facolta.ProfessorsFaculty.Add(p);
                    }
                    dataReader1.Close();

                    //Recupero Esami Facoltà
                    using SqlConnection sqlCnn2 = new(_connection.ConnectionString);
                    sqlCnn2.Open();
                    using SqlCommand sqlCmd2 = new("Select Id FROM Exam WHERE FacultyId = @id", sqlCnn2);
                    sqlCmd2.Parameters.AddWithValue("@id", id);
                    using SqlDataReader dataReader2 = sqlCmd2.ExecuteReader();
                    while (dataReader2.Read())
                    {
                        Exam e = ExamManager.examsList.Find(e => e.Id == int.Parse(dataReader2["Id"].ToString()));
                        facolta.ExamsFaculty.Add(e);
                    }

                }

            } 

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);  
            }
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
                Console.Write($"Nome facoltà: {f.NameFaculty}, ");
                foreach (Course c in f.CoursesFaculty)
                {
                    Console.Write($"Nome Corso: {c.CourseName}, ");
                }
                foreach (Exam e in f.ExamsFaculty)
                {
                    Console.Write($" Id Esame: {e.Course.CourseName}");
                }
                foreach (Professor p in f.ProfessorsFaculty)
                {
                    Console.Write($" Professore: {p.FullName}\n\n");
                }
            }
        }
    }
}
