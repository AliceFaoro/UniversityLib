using Microsoft.Data.SqlClient;
using System.Configuration;
using University.DataModel;

namespace University.BLogic
{
    public class ExamManager
    {
        private readonly SqlConnection _connection = new();
        private SqlCommand _command = new();

        public static List<Exam> examsList = new();
        public List<Exam> GetExams()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("SELECT * FROM EXAM", sqlCnn);
                using SqlDataReader dataReader = sqlCmd.ExecuteReader();

                while (dataReader.Read())
                {
                    examsList.Add(new Exam
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        Faculty = FacultyManager.facultyList.Find(f => f.Id == (int.Parse(dataReader["FacultyId"].ToString()))),
                        Course = CourseManager.coursesList.Find(c => c.Id == (int.Parse(dataReader["CourseId"].ToString()))),
                        ExamProfessor = ProfessorManager.professorList.Find(p => p.Id == (int.Parse(dataReader["ProfessorId"].ToString()))),
                        ExamDate = DateTime.Parse(dataReader["ExamDate"].ToString()),

                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            Console.WriteLine($"{examsList[0].Faculty.NameFaculty}, {examsList[0].Course.CourseName}, {examsList[0].ExamProfessor.FullName} ,{examsList[0].ExamDate}");

            return examsList;
        }
        public void AddExam()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];

                Console.WriteLine("Inserire Id: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Id Facoltà");
                int fId = int.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Id corso");
                int cId = int.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Id professore");
                int pId = int.Parse(Console.ReadLine());
                Console.WriteLine("Inserire data esame: ");
                DateTime date = DateTime.Parse(Console.ReadLine());

                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("INSERT INTO EXAM" +
                                           "(Id, FacultyId, CourseId, ProfessorId, ExamDate ) " +
                                           "VALUES " +
                                           "(@id, @fId, @cId, @pId, @date)", sqlCnn);
                sqlCmd.Parameters.AddWithValue("@id", id);
                sqlCmd.Parameters.AddWithValue("@fId", fId);
                sqlCmd.Parameters.AddWithValue("@cId", cId);
                sqlCmd.Parameters.AddWithValue("@pId", pId);
                sqlCmd.Parameters.AddWithValue("@date", date);
                sqlCmd.ExecuteNonQuery();

                Exam e = new Exam
                {
                    Id = id,
                    Faculty = FacultyManager.facultyList.Find(f => f.Id == fId),
                    Course = CourseManager.coursesList.Find(f => f.Id == cId),
                    ExamProfessor = ProfessorManager.professorList.Find(p => p.Id == pId),
                    ExamDate = date
                };
                examsList.Add(e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ViewExam()
        {
            Console.Clear();
            Console.WriteLine("Elenco Esami: \n");
            foreach (Exam e in examsList)
            {
                Console.WriteLine($"Facoltà: {e.Faculty.NameFaculty}\nCorso: {e.Course.CourseName}\nProfessore: {e.ExamProfessor.FullName}\nData: {e.ExamDate}\n");
            }
        }
    }
}
