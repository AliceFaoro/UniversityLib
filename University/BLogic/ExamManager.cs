using Microsoft.Data.SqlClient;
using System.Data;
using University.DataModel;

namespace University.BLogic
{
    public class ExamManager
    {
        ExceptionLogManager exLogManager = new();

        private readonly SqlConnection _connection = new();
        private SqlCommand _command = new();

        public static List<Exam> examsList = new();

        ///This method returns a string with all the Exams in the database
        public string GetExams(string connectionString)
        {
            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {

                    sqlCnn.Open();
                    using SqlCommand sqlCmd = new("GetExams", sqlCnn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    using SqlDataReader dataReader = sqlCmd.ExecuteReader();
                    //Per ogni esame nel database creo un oggetto Exam e lo aggiungo alla lista
                    while (dataReader.Read())
                    {
                        Exam e = new Exam
                        {
                            Id = int.Parse(dataReader["Id"].ToString()),
                            ExamDate = DateTime.Parse(dataReader["ExamDate"].ToString()),

                        };

                        e.Faculty = new Faculty
                        {
                            Id = int.Parse(dataReader["fId"].ToString()),
                            NameFaculty = dataReader["NameFaculty"].ToString()
                        };

                        e.Course = new Course
                        {
                            Id = int.Parse(dataReader["cId"].ToString()),
                            CourseName = dataReader["CourseName"].ToString()
                        };

                        e.Professor = new Professor
                        {
                            Id = int.Parse(dataReader["fId"].ToString()),
                            FullName = dataReader["FullName"].ToString(),
                            Faculty = e.Faculty
                        };

                        examsList.Add(e);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex, connectionString);
            }

            string testo = "Elenco Esami: \n\n";
            foreach (Exam e in examsList)
            {
                testo += $"Facoltà: {e.Faculty.NameFaculty}\nCorso: {e.Course.CourseName}\nData: {e.ExamDate}\n\n";
            }

            return testo;

        }

        //This methods Adds a new Exam in the database
        public void AddExam(string connectionString, int id, int fId, int cId, int pId, DateTime date)
        {
            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
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

                    Console.WriteLine("\nEsame aggiunto con successo!\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex, connectionString);
            }
        }

        //This method Updates the Professor and Date of an Exam
        public void UpdateExam(string connectionString, int eId, int pId, DateTime data)
        {
            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
                    sqlCnn.Open();
                    
                    using (SqlCommand sqlCmd = new("UPDATE Exam SET ExamDate = @data, ProfessorId = @pId WHERE Id = @eId", sqlCnn))
                    {
                        sqlCmd.Parameters.AddWithValue("@data", data);
                        sqlCmd.Parameters.AddWithValue("@pId", pId);
                        sqlCmd.Parameters.AddWithValue("@eId", eId);
                        sqlCmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("\nEsame aggiornato con successo!\n");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex, connectionString);
            }
        }

        //This method Deletes an Exam
        public void DeleteExam(string connectionString, int eId)
        {
            try
            {
                using (var sqlCnn = new SqlConnection(connectionString))
                {
                    sqlCnn.Open();

                    using (SqlCommand sqlCmd = new("DELETE Exam WHERE Id = @eId", sqlCnn))
                    {
                        sqlCmd.Parameters.AddWithValue("@eId", eId);
                        sqlCmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("\nEsame eliminato con successo!\n");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex, connectionString);
            }
        }

    }
}
