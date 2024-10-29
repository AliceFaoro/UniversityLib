using Microsoft.Data.SqlClient;
using System.Data;
using University.DataModel;

namespace University.BLogic
{
    public class StudentManager
    {
        ExceptionLogManager exLogManager = new();
        private SqlCommand _command = new();
        public static List<Student> studentList = new();

        //This method returns a string with all the Students in the database
        public string GetStudents(string connectionString)
        {
            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
                    sqlCnn.Open();

                    using SqlCommand sqlCmd = new("GetStudents", sqlCnn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    using SqlDataReader dataReader = sqlCmd.ExecuteReader();


                    while (dataReader.Read())
                    {
                        int id = int.Parse(dataReader["StudentId"].ToString());
                        if (studentList.Find(s => s.Id == id) == null)
                        {
                            Student s = new Student
                            {
                                Id = int.Parse(dataReader["StudentId"].ToString()),
                                FullName = dataReader["FullName"].ToString().Trim(),
                                DateOfBirth = DateTime.Parse(dataReader["DateOfBirth"].ToString()),
                                Tuition = decimal.Parse(dataReader["Tuition"].ToString()),
                                AvgGrades = decimal.Parse(dataReader["AvgGrades"].ToString())

                            };

                            s.Faculty = new Faculty
                            {
                                Id = int.Parse(dataReader["FacultyId"].ToString()),
                                NameFaculty = dataReader["NameFaculty"].ToString()
                            };


                            s.StudentsExams.Add(new Exam
                            {
                                Id = int.Parse(dataReader["ExamId"].ToString()),
                                Faculty = s.Faculty
                            });

                            s.StudentsCourses.Add(new Course
                            {
                                Id = int.Parse(dataReader["CourseId"].ToString()),
                                CourseName = dataReader["CourseName"].ToString(),
                                Faculty = s.Faculty
                            });

                            studentList.Add(s);
                        } else
                        {
                            Student s1 = studentList.Find(s => s.Id == id);

                            
                            int courseId = int.Parse(dataReader["CourseId"].ToString());
                            int examId = int.Parse(dataReader["ExamId"].ToString());

                            if (s1.StudentsCourses.Find(c => c.Id == courseId) == null)
                            {
                                s1.StudentsCourses.Add(new Course
                                {
                                    Id = int.Parse(dataReader["CourseId"].ToString()),
                                    CourseName = dataReader["CourseName"].ToString(),
                                    Faculty = new Faculty
                                    {
                                        Id = int.Parse(dataReader["FacultyId"].ToString()),
                                        NameFaculty = dataReader["NameFaculty"].ToString()
                                    }
                                });
                            }
                            if (s1.StudentsExams.Find(e => e.Id == examId) == null)
                            {
                                s1.StudentsExams.Add(new Exam
                                {
                                    Id = int.Parse(dataReader["ExamId"].ToString()),
                                    Faculty = new Faculty
                                    {
                                        Id = int.Parse(dataReader["FacultyId"].ToString()),
                                        NameFaculty = dataReader["NameFaculty"].ToString()
                                    }
                                });
                            }
                        }
                    }  

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex, connectionString);
            }

            string testo = "Elenco Studenti:\n\n";

            foreach (Student s in studentList)
            {
                testo += $"Nome: {s.FullName}\nData di nascita: {s.DateOfBirth}\nFacoltà: {s.Faculty.NameFaculty}\nRetta annuale: {s.Tuition}\nMedia Esami: {s.AvgGrades}\nCorsi: ";

                foreach (Course course in s.StudentsCourses)
                {
                    testo += $"{course.CourseName} ";
                }
                testo += "\nEsami: ";
                foreach (Exam exam in s.StudentsExams)
                {
                    testo += $" Data: {exam.ExamDate} ";
                }
                testo += "\n\n";
            }
            return testo;

        }

        //This methods Adds a new Student in the database
        public void AddStudent(string connectionString, int Id, string FullName, DateTime DateOfBirth, int FacultyId, decimal Tuiton, decimal AvgGrades)
        {
            try
            {

                using (var sqlCnn = new SqlConnection(connectionString))
                {
                    sqlCnn.Open();
                    using SqlCommand sqlCmd = new("INSERT INTO STUDENT" +
                                               "(Id,FullName, DateOfBirth, FacultyId, Tuition, AvgGrades) " +
                                               "VALUES " +
                                               "(@id, @name, @date, @facultyId, @tuition, @avg)", sqlCnn);
                    sqlCmd.Parameters.AddWithValue("@id", Id);
                    sqlCmd.Parameters.AddWithValue("@name", FullName);
                    sqlCmd.Parameters.AddWithValue("@date", DateOfBirth);
                    sqlCmd.Parameters.AddWithValue("@facultyId", FacultyId);
                    sqlCmd.Parameters.AddWithValue("@tuition", Tuiton);
                    sqlCmd.Parameters.AddWithValue("@avg", AvgGrades);
                    sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("\nStudente aggiunto con successo!\n");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex, connectionString);
            }


        }

        //This method Updates the Tuition and the Avg of a Student
        public void UpdateStudent(string connectionString, string name, decimal tuition, decimal avg)
        {
            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
                    sqlCnn.Open();

                    using (SqlCommand sqlCmd = new("UPDATE Student " +
                                                   "SET Tuition = @tuition, AvgGrades = @avg " +
                                                   "WHERE FullName = @name", sqlCnn))
                    {
                        sqlCmd.Parameters.AddWithValue("@tuition", tuition);
                        sqlCmd.Parameters.AddWithValue("@avg", avg);
                        sqlCmd.Parameters.AddWithValue("@name", name);
                        sqlCmd.ExecuteNonQuery();
                    }

                    Console.WriteLine("\nStudente aggiornato con successo!\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex, connectionString);
            }

        }

        //This method Deletes a Studnt
        public void DeleteStudent(string connectionString, int Id)
        {

            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
                    sqlCnn.Open();
                    using SqlCommand sqlCmd = new("DELETE Student WHERE Id = @Id", sqlCnn);
                    sqlCmd.Parameters.AddWithValue("@Id", Id);
                    sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("\nStudente eliminato con successo!\n");
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

