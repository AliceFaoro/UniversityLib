using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using University.DataModel;
using University.BLogic;
using Microsoft.Identity.Client;
using University.AppMenu;

namespace University.BLogic
{
    public class DbManager
    {
        List<Course> coursesList = new();
        List<Exam> examsList = new();
        List<Faculty> facultyList = new();
        List<Professor> professorList = new();
        List<Student> studentList = new();

        private readonly SqlConnection _connection = new();
        private SqlCommand _command = new();

        public readonly bool IsDbOnLine = false;
        public DbManager()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                _connection.Open();
                IsDbOnLine = true;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
            }
        }
        public List<Faculty> GetFaculties()
        {

            try
            {
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

                throw;
            }
            Console.WriteLine($"{facultyList[0].NameFaculty}");
            return facultyList;
        }
        public List<Student> GetStudents()
        {
            try
            {
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("SELECT * FROM STUDENT", sqlCnn);
                using SqlDataReader dataReader = sqlCmd.ExecuteReader();

                while (dataReader.Read())
                {
                    studentList.Add(new Student
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        FullName = dataReader["FullName"].ToString().Trim(),
                        DateOfBirth = DateTime.Parse(dataReader["DateOfBirth"].ToString()),
                        Faculty = facultyList.Find(f => f.Id == (int.Parse(dataReader["FacultyId"].ToString()))),
                        Tuition = decimal.Parse(dataReader["Tuition"].ToString()),
                        AvgGrades = decimal.Parse(dataReader["AvgGrades"].ToString())
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            Console.WriteLine($"{studentList[0].FullName}, {studentList[0].DateOfBirth}, {studentList[0].Faculty.NameFaculty} ,{studentList[0].Tuition}, {studentList[0].AvgGrades}");
            return studentList;
        }

        public List<Professor> GetProfessors()
        {
            try
            {
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
                        Faculty = facultyList.Find(f => f.Id == (int.Parse(dataReader["FacultyId"].ToString()))),
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
        public List<Course> GetCourses()
        {
            try
            {
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
                        Faculty = facultyList.Find(f => f.Id == (int.Parse(dataReader["FacultyId"].ToString()))),
                        CourseProfessor = professorList.Find(p => p.Id == (int.Parse(dataReader["ProfessorId"].ToString())))

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

        public List<Exam> GetExams()
        {
            try
            {
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("SELECT * FROM EXAM", sqlCnn);
                using SqlDataReader dataReader = sqlCmd.ExecuteReader();

                while (dataReader.Read())
                {
                    examsList.Add(new Exam
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        Faculty = facultyList.Find(f => f.Id == (int.Parse(dataReader["FacultyId"].ToString()))),
                        Course = coursesList.Find(c => c.Id == (int.Parse(dataReader["CourseId"].ToString()))),
                        ExamProfessor = professorList.Find(p => p.Id == (int.Parse(dataReader["ProfessorId"].ToString()))),
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
        public void AddFaculty()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("Inserire Id facoltà: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Nome facoltà: ");
                string name = Console.ReadLine();
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
                facultyList.Add(f);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void AddStudent()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("Inserire Id studente: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Nome completo studente: ");
                string name = Console.ReadLine();
                Console.WriteLine("Inserire Data di nascita: ");
                DateTime date = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Id Facoltà");
                int fId = int.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Retta: ");
                decimal tuition = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Media: ");
                decimal avg = decimal.Parse(Console.ReadLine());
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("INSERT INTO STUDENT" +
                                           "(Id,FullName, DateOfBirth, FacultyId, Tuition, AvgGrades) " +
                                           "VALUES " +
                                           "(@id, @name, @date, @fId, @tuition, @avg)", sqlCnn);
                sqlCmd.Parameters.AddWithValue("@id", id);
                sqlCmd.Parameters.AddWithValue("@name", name);
                sqlCmd.Parameters.AddWithValue("@date", date);
                sqlCmd.Parameters.AddWithValue("@fId", fId);
                sqlCmd.Parameters.AddWithValue("@tuition", tuition);
                sqlCmd.Parameters.AddWithValue("@avg", avg);
                sqlCmd.ExecuteNonQuery();

                Student s = new Student
                {
                    Id = id,
                    FullName = name,
                    DateOfBirth = date,
                    Faculty = facultyList.Find(f => f.Id == fId),
                    Tuition = tuition,
                    AvgGrades = avg
                };
                studentList.Add(s);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void AddProfessor()
        {
            Console.Clear();
            try
            {
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
                    Faculty = facultyList.Find(f => f.Id == fId),
                    Pay = pay
                };
                professorList.Add(p);
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
                    Faculty = facultyList.Find(f => f.Id == fId),
                    CourseProfessor = professorList.Find(p => p.Id == pId)
                };
                coursesList.Add(c);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AddExam()
        {
            try
            {
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
                    Faculty = facultyList.Find(f => f.Id == fId),
                    Course = coursesList.Find(f => f.Id == cId),
                    ExamProfessor = professorList.Find(p => p.Id == pId),
                    ExamDate = date
                };
                examsList.Add(e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
