using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;
using University.DataModel;


namespace University.BLogic
{
    public class FacultyManager
    {
        ExceptionLogManager exLogManager = new();

        private readonly SqlConnection _connection = new();
        private SqlCommand _command = new();

        public static List<Faculty> facultyList = new();

        //This method returns a string with all the Faculties in the database
        public string GetFaculties(string connectionString)
        {

            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
                    sqlCnn.Open();
                    using SqlCommand sqlCmd = new("GetFaculties", sqlCnn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using SqlDataReader dataReader = sqlCmd.ExecuteReader();
                    Console.Clear();
                    
                    while (dataReader.Read())
                    {
                        int id = int.Parse(dataReader["fId"].ToString());
                        if (facultyList.Find(f => f.Id == id) == null)
                        {
                            Faculty f = new Faculty
                            {
                                Id = int.Parse(dataReader["fId"].ToString()),
                                NameFaculty = dataReader["NameFaculty"].ToString().Trim()
                            };

                            f.CoursesFaculty.Add(new Course
                            {
                                Id = int.Parse(dataReader["cId"].ToString()),
                                CourseName = dataReader["CourseName"].ToString(),
                                Faculty = f
                            });

                            f.ExamsFaculty.Add(new Exam
                            {
                                Id = int.Parse(dataReader["eId"].ToString()),
                                ExamDate = DateTime.Parse(dataReader["ExamDate"].ToString()),
                                Faculty = f
                            });

                            f.ProfessorsFaculty.Add(new Professor
                            {
                                Id = int.Parse(dataReader["pId"].ToString()),
                                FullName = dataReader["FullName"].ToString(),
                                Faculty = f
                            });

                            facultyList.Add(f);

                        }
                        else
                        {
                            Faculty f1 = facultyList.Find(f => f.Id == id);

                            int cId = int.Parse(dataReader["cId"].ToString());
                            int eId = int.Parse(dataReader["fId"].ToString());
                            int pId = int.Parse(dataReader["pId"].ToString());

                            if (f1.CoursesFaculty.Find(c => c.Id == cId) == null)
                            {
                                f1.CoursesFaculty.Add(new Course
                                {
                                    Id = int.Parse(dataReader["cId"].ToString()),
                                    CourseName = dataReader["CourseName"].ToString(),
                                    Faculty = f1
                                });
                            }

                            if (f1.ExamsFaculty.Find(e => e.Id == eId) == null)
                            {
                                f1.ExamsFaculty.Add(new Exam
                                {
                                    Id = int.Parse(dataReader["eId"].ToString()),
                                    ExamDate = DateTime.Parse(dataReader["ExamDate"].ToString()),
                                    Faculty = f1
                                });
                            }

                            if (f1.ProfessorsFaculty.Find(p => p.Id == pId) == null)
                            {
                                f1.ProfessorsFaculty.Add(new Professor
                                {
                                    Id = int.Parse(dataReader["pId"].ToString()),
                                    FullName = dataReader["FullName"].ToString(),
                                    Faculty = f1
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore");
                exLogManager.ExcLog(ex, connectionString);
            }
            string testo = ($"Elenco Facoltà: \n\n");

            foreach (Faculty f in facultyList)
            {
                testo += $"Nome facoltà: {f.NameFaculty}\n";

                foreach (Course c in f.CoursesFaculty)
                {
                    testo += $"Nome Corso: {c.CourseName}\n";
                }
                testo += $"Id Esame: ";
                foreach (Exam e in f.ExamsFaculty)
                {
                    testo += $" {e.Id}, ";
                }
                testo += $"\nProfessore: ";
                foreach (Professor p in f.ProfessorsFaculty)
                {

                    testo += $" {p.FullName}, ";
                }
                testo += "\n\n";
            }
            return testo;

        }

        //This methods Adds a new Faculty in the database
        public void AddFaculty(string connectionString, int id, string name)
        {
            try
            {

                using (var sqlCnn = new SqlConnection(connectionString))
                {
                    sqlCnn.Open();
                    using SqlCommand sqlCmd = new("INSERT INTO FACULTY" +
                                               "(Id,NameFaculty) " +
                                               "VALUES " +
                                               "(@id, @name)", sqlCnn);
                    sqlCmd.Parameters.AddWithValue("@id", id);
                    sqlCmd.Parameters.AddWithValue("@name", name);
                    sqlCmd.ExecuteNonQuery();

                    Console.WriteLine("\nFacoltà aggiunta con successo!\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex, connectionString);
            }

        }
        
        //This method Updates the Name of a Faculty
        public void UpdateFaculty(string connectionString, string name, string newName)
        {
            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
                    sqlCnn.Open();
                    using SqlCommand sqlCmd = new("UPDATE Faculty " +
                                                   "SET NameFaculty = @newName " +
                                                   "WHERE NameFaculty = @nome", sqlCnn);
                    sqlCmd.Parameters.AddWithValue("@newName", newName);
                    sqlCmd.Parameters.AddWithValue("@nome", name);
                    sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("Facoltà aggiornata con successo!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex, connectionString);
            }

        }

        //This method Deletes a Faculty
        public void DeleteFaculty(string connectionString, int Id)
        {
            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
                    sqlCnn.Open();
                    using SqlCommand sqlCmd = new("DELETE Faculty WHERE Id = @id", sqlCnn);
                    sqlCmd.Parameters.AddWithValue("@id", Id);
                    sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("Facoltà eliminata con successo!");
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
