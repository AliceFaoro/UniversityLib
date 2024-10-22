using Microsoft.Data.SqlClient;
using System.Configuration;
using University.DataModel;

namespace University.BLogic
{
    public class ExamManager
    {
        ExceptionLogManager exLogManager = new();

        private readonly SqlConnection _connection = new();
        private SqlCommand _command = new();

        public static List<Exam> examsList = new();
        //Importa esami dal database
        public List<Exam> GetExams()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("SELECT * FROM EXAM", sqlCnn);
                using SqlDataReader dataReader = sqlCmd.ExecuteReader();
                //Per ogni esame nel database creo un oggetto Exam e lo aggiungo alla lista
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
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex);
            }

            return examsList;
        }
        //Aggiunge un esame nel database e nella lista
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

                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
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
                Console.WriteLine("\nEsami aggiungi con successo!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex);
            }
        }
        //Aggiorna i dati di un esame sia nel database che nella lista
        public void UpdateExam()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();

                Console.Clear();
                Console.WriteLine("Inserire l'id dell'esame da aggiornare: ");
                int eId = int.Parse(Console.ReadLine());
                Exam e = examsList.Find(e => e.Id == eId);
                Console.WriteLine("Cosa vuoi aggiornare? 1.Professore 2.Data");
                int scelta = int.Parse(Console.ReadLine());

                switch (scelta)
                {
                    //Aggiorna Professore dell'esame
                    case 1:

                        Console.WriteLine("Inserire il nome del nuovo professore:");
                        string pName = Console.ReadLine();
                        Professor p = ProfessorManager.professorList.Find(p => p.FullName.Equals(pName));
                        int pId = p.Id;
                        e.ExamProfessor = p;

                        using (SqlCommand sqlCmd = new("UPDATE Exam SET ProfessorId = @pId WHERE Id = @eId", sqlCnn))
                        {
                            sqlCmd.Parameters.AddWithValue("@pId", pId);
                            sqlCmd.Parameters.AddWithValue("@eId", eId);
                            sqlCmd.ExecuteNonQuery();
                        }

                        break;

                    //Aggiorna la data dell'esame
                    case 2:

                        Console.WriteLine("Inserire la nuova data ");
                        DateTime data = DateTime.Parse(Console.ReadLine());
                        e.ExamDate = data;

                        using (SqlCommand sqlCmd = new("UPDATE Exam SET ExamDate = @data WHERE Id = @eId", sqlCnn))
                        {
                            sqlCmd.Parameters.AddWithValue("@data", data);
                            sqlCmd.Parameters.AddWithValue("@eId", eId);
                            sqlCmd.ExecuteNonQuery();
                        }

                        break;
                }
                Console.WriteLine("\nEsame aggiornato con successo!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex);
            }
        }
        //Visualizza a console la lista degli esami
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
