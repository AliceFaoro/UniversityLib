using Microsoft.Data.SqlClient;
using System.Data;
using University.DataModel;

namespace University.BLogic
{
    public class CourseManager
    {
        ExceptionLogManager exLogManager = new();
        private readonly SqlConnection _connection = new();
        private SqlCommand _command = new();

        public static List<Course> coursesList = new();
        
        //This method returns a string with all the Courses in the database
        public string GetCourses(string connectionString)
        {
            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
                    sqlCnn.Open();
                    using SqlCommand sqlCmd = new("GetCourses", sqlCnn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    using SqlDataReader dataReader = sqlCmd.ExecuteReader();
                    
                    while (dataReader.Read())
                    {
                        Course c = new Course
                        {
                            Id = int.Parse(dataReader["Id"].ToString()),
                            CourseName = dataReader["CourseName"].ToString()
                        };

                        c.Faculty = new Faculty
                        {
                            Id = int.Parse(dataReader["fId"].ToString()),
                            NameFaculty = dataReader["NameFaculty"].ToString()
                        };

                        c.CourseProfessor = new Professor
                        {
                            Id = int.Parse(dataReader["pId"].ToString()),
                            FullName = dataReader["FullName"].ToString(),
                            Faculty = c.Faculty
                        };
                        coursesList.Add(c);
                    }
                }
                }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex, connectionString);
            }

            string testo = "Elenco Corsi: \n\n";
            foreach (Course c in coursesList)
            {
                testo += $"Nome: {c.CourseName}\nFacoltà: {c.Faculty.NameFaculty}\nProfessore: {c.CourseProfessor.FullName}\n\n";
            }
            return testo;
        }
        
        //This methods Adds a new Course in the database
        public void AddCourse(string connectionString, int id,string name, int fId, int pId)
        {
            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
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
                    Console.WriteLine("\nCorso aggiunto con successo!\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex, connectionString);
            }
        }

        //This method Updates the Professor in a Course
        public void UpdateCourse(string connectionString, string nome, string professorId)
        {
            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
                    sqlCnn.Open();

                    using SqlCommand sqlCmd = new("UPDATE Course " +
                                                   "SET ProfessorId = @pId ", sqlCnn);
                    sqlCmd.Parameters.AddWithValue("@pId", professorId);
                    sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("\nCorso aggiornato con successo!\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex, connectionString);
            }

        }

        //This method Deletes a Course
        public void DeleteCourse(string connectionString, int id)
        {
            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
                    sqlCnn.Open();

                    using SqlCommand sqlCmd = new("DELETE Course " +
                                                   "WHERE Id = @id ", sqlCnn);
                    sqlCmd.Parameters.AddWithValue("@Id", id);
                    sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("\nCorso eliminato con successo!\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex, connectionString);
            }
        }
    }
}
