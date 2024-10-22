using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using University.DataModel;

namespace University.BLogic
{
    public class CourseManager
    {

        private readonly SqlConnection _connection = new();
        private SqlCommand _command = new();

        public static List<Course> coursesList = new();

        public List<Course> GetCourses()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("SELECT * FROM COURSE", sqlCnn);
                using SqlDataReader dataReader = sqlCmd.ExecuteReader();

                while (dataReader.Read())
                {
                    coursesList.Add(new Course
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        CourseName = dataReader["CourseName"].ToString().Trim(),
                        Faculty = FacultyManager.facultyList.Find(f => f.Id == (int.Parse(dataReader["FacultyId"].ToString()))),
                        CourseProfessor = ProfessorManager.professorList.Find(p => p.Id == (int.Parse(dataReader["ProfessorId"].ToString())))

                    });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return coursesList;
        }
        public void GetCourses2()
        {
            try
            {


                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();

                //Recupero lista Corsi
                foreach (Course course in coursesList)
                {
                    int id = course.Id;
                    using SqlCommand sqlCmd = new("SELECT Id FROM Exam WHERE CourseId = @id", sqlCnn);
                    sqlCmd.Parameters.AddWithValue("@id", id);
                    using SqlDataReader dataReader = sqlCmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Exam e = ExamManager.examsList.Find(e => e.Id == int.Parse(dataReader["Id"].ToString()));
                        course.CourseExams.Add(e);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AddCourse()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();

                Console.WriteLine("Inserire Id: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Nome del corso: ");
                string name = Console.ReadLine();
                Console.WriteLine("Inserire Id Facoltà");
                int fId = int.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Id professore");
                int pId = int.Parse(Console.ReadLine());

                using SqlCommand sqlCmd = new("INSERT INTO COURSE" +
                                           "(Id,CourseName, FacultyId, ProfessorId) " +
                                           "VALUES " +
                                           "(@id, @name, @fId, @pId)", sqlCnn);
                sqlCmd.Parameters.AddWithValue("@id", id);
                sqlCmd.Parameters.AddWithValue("@name", name);
                sqlCmd.Parameters.AddWithValue("@fId", fId);
                sqlCmd.Parameters.AddWithValue("@pId", pId);
                sqlCmd.ExecuteNonQuery();

                Course c = new Course
                {
                    Id = id,
                    CourseName = name,
                    Faculty = FacultyManager.facultyList.Find(f => f.Id == fId),
                    CourseProfessor = ProfessorManager.professorList.Find(p => p.Id == pId)
                };
                coursesList.Add(c);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void UpdateCourse()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();

                Console.Clear();
                Console.WriteLine("Inserire il nome del corso da aggiornare: ");
                string nome = Console.ReadLine();
                Course c = coursesList.Find(c => c.CourseName.Equals(nome));
                Console.WriteLine("Inserire il nome del nuovo professore: ");
                string pName = Console.ReadLine();
                Professor p = ProfessorManager.professorList.Find(p => p.FullName.Equals(pName));
                int pId = p.Id;


                using SqlCommand sqlCmd = new("UPDATE Course " +
                                               "SET ProfessorId = @pId " +
                                               "WHERE FullName = @nome", sqlCnn);
                sqlCmd.Parameters.AddWithValue("@pId", pId);
                sqlCmd.Parameters.AddWithValue("@nome", nome);
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void ViewCourse()
        {
            Console.Clear();
            Console.WriteLine("Elenco Corsi: \n");
            foreach (Course c in coursesList)
            {
                Console.WriteLine($"Nome: {c.CourseName}\nFacoltà: {c.Faculty.NameFaculty}\nProfessore: {c.CourseProfessor.FullName}\nEsami: ");
                foreach (Exam e in c.CourseExams)
                {
                    Console.WriteLine($"-{e.Course.CourseName} {e.ExamDate}");
                }

                Console.WriteLine();
            }
        }

    }
}
