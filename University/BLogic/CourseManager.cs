using Microsoft.Data.SqlClient;
using System.Configuration;
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
            catch (Exception)
            {

                throw;
            }
            Console.WriteLine($"{coursesList[0].CourseName}, {coursesList[0].Faculty.NameFaculty}, {coursesList[0].CourseProfessor.FullName}");

            return coursesList;
        }
        public void AddCourse()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];

                Console.WriteLine("Inserire Id: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Nome del corso: ");
                string name = Console.ReadLine();
                Console.WriteLine("Inserire Id Facoltà");
                int fId = int.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Id professore");
                int pId = int.Parse(Console.ReadLine());

                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
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
        public void ViewCourse()
        {
            Console.Clear();
            Console.WriteLine("Elenco Corsi: \n");
            foreach (Course c in coursesList)
            {
                Console.WriteLine($"Nome: {c.CourseName}\nFacoltà: {c.Faculty.NameFaculty}\nProfessore: {c.CourseProfessor.FullName}\n");
            }
        }

    }
}
