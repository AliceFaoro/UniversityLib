using Microsoft.Data.SqlClient;
using System.Data;
using University.DataModel;

namespace University.BLogic
{
    public class ProfessorManager
    {
        ExceptionLogManager exLogManager = new();

        private readonly SqlConnection _connection = new();
        private SqlCommand _command = new();

        public static List<Professor> professorList = new();

        //This method returns a string with all the Professors in the database
        public string GetProfessors(string connectionString)
        {
            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
                    sqlCnn.Open();
                    using SqlCommand sqlCmd = new("GetProfessors", sqlCnn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    using SqlDataReader dataReader = sqlCmd.ExecuteReader();

                    //Per ogni professore presente nel database creo un oggetto professore e lo aggiungo alla lista
                    while (dataReader.Read())
                    {
                        int id = int.Parse(dataReader["Id"].ToString());
                        if (professorList.Find(p => p.Id == id) == null)
                        {

                            Professor p = new Professor
                            {
                                Id = int.Parse(dataReader["Id"].ToString()),
                                FullName = dataReader["FullName"].ToString().Trim(),
                                DateOfBirth = DateTime.Parse(dataReader["DateOfBirth"].ToString()),
                                Pay = decimal.Parse(dataReader["Pay"].ToString())
                            };

                            p.Faculty = new Faculty
                            {
                                Id = int.Parse(dataReader["fId"].ToString()),
                                NameFaculty = dataReader["NameFaculty"].ToString()
                            };

                            p.ProfessorsCourses.Add(new Course
                            {
                                Id = int.Parse(dataReader["cId"].ToString()),
                                CourseName = dataReader["CourseName"].ToString(),
                                Faculty = p.Faculty
                            });

                            professorList.Add(p);
                        }
                        else
                        {
                            Professor p1 = professorList.Find(p => p.Id == id);
                            int courseId = int.Parse(dataReader["cId"].ToString());
                            if (p1.ProfessorsCourses.Find(c => c.Id == courseId) == null)
                            {
                                p1.ProfessorsCourses.Add(new Course
                                {
                                    Id = int.Parse(dataReader["Id"].ToString()),
                                    CourseName = dataReader["CourseName"].ToString(),
                                    Faculty = new Faculty
                                    {
                                        Id = int.Parse(dataReader["fId"].ToString()),
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

            string testo = "Elenco Professori: \n";

            foreach (Professor p in professorList)
            {
                testo += $"Nome: {p.FullName}\nData di nascita: {p.DateOfBirth}\nFacoltà: {p.Faculty.NameFaculty}\nStipendio: {p.Pay}\n";
                foreach (Course c in p.ProfessorsCourses)
                {
                    testo += $"Corso: {c.CourseName}\n\n";
                }
            }
            return testo;

        }

        //This methods Adds a new Professor in the database
        public void AddProfessor(string connectionString, int id, string name, DateTime date, int fId, decimal pay)
        {
            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
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

                    Console.WriteLine("\nProfessore aggiunto con successo!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex, connectionString);
            }
        }

        //This method Updates the Faculty and the Pay of a Professor
        public void UpdateProfessor(string connectionString, string nome, int fId, decimal pay)
        {
            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
                    sqlCnn.Open();

                    using SqlCommand sqlCmd = new("UPDATE Professor " +
                                                  "SET FacultyId = @fId, Pay = @pay " +
                                                  "WHERE FullName = @nome", sqlCnn);
                    sqlCmd.Parameters.AddWithValue("@fId", fId);
                    sqlCmd.Parameters.AddWithValue("@pay", pay);
                    sqlCmd.Parameters.AddWithValue("@nome", nome);
                    sqlCmd.ExecuteNonQuery();
                }

                Console.WriteLine("\nProfessore aggiornato con successo!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex, connectionString);
            }
        }

        //This method Deletes a Professor
        public void DeleteProfessor(string connectionString, int pId)
        {
            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
                    sqlCnn.Open();

                    using SqlCommand sqlCmd = new("DELETE Professor " +
                                                  "WHERE Id = @pId", sqlCnn);
                    sqlCmd.Parameters.AddWithValue("@pId", pId);
                    sqlCmd.ExecuteNonQuery();
                }

                Console.WriteLine("\nProfessore eliminato con successo!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex, connectionString);
            }
        }

    }
}
