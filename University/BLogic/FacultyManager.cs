using Microsoft.Data.SqlClient;
using System.Configuration;
using University.DataModel;


namespace University.BLogic
{
    public class FacultyManager
    {
        ExceptionLogManager exLogManager = new();

        private readonly SqlConnection _connection = new();
        private SqlCommand _command = new();
        
        public static List<Faculty> facultyList = new();
        //Importa dal database i dati iniziali delle facoltà
        public List<Faculty> GetFaculties()
        {

            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("SELECT * FROM FACULTY", sqlCnn);
                using SqlDataReader dataReader = sqlCmd.ExecuteReader();
                Console.Clear();
                //Per ogni facoltà selezionata dal database creo un oggetto Faculty e lo aggiungo alla lista di facoltà
                while (dataReader.Read())
                {

                    Faculty f = (new Faculty 
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        NameFaculty = dataReader["NameFaculty"].ToString().Trim()
                    });
                    facultyList.Add(f);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore");
                exLogManager.ExcLog(ex);
            }
            return facultyList;
        }
        //Importa dal database i dati mancanti delle facoltà
        public void GetFaculties2()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();

                foreach (Faculty facolta in FacultyManager.facultyList)
                {
                    int id = facolta.Id;
                    
                    //Recupero i corsi di ogni facoltà e li aggiungo alla lista CoursesFaculty
                    using SqlCommand sqlCmd = new("SELECT Id FROM Course WHERE FacultyId = @id", sqlCnn);
                    sqlCmd.Parameters.AddWithValue("@id", id);
                    using SqlDataReader dataReader = sqlCmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Course c = CourseManager.coursesList.Find(c => c.Id == int.Parse(dataReader["Id"].ToString()));
                        facolta.CoursesFaculty.Add(c);
                    }
                    dataReader.Close();

                    //Recupero i professori di ogni facoltà e li aggiungo alla lista ProfessorsFaculty
                    using SqlConnection sqlCnn1 = new(_connection.ConnectionString);
                    sqlCnn1.Open();
                    using SqlCommand sqlCmd1 = new("SELECT Id FROM Professor WHERE FacultyId = @id", sqlCnn1);
                    sqlCmd1.Parameters.AddWithValue("@id", id);
                    using SqlDataReader dataReader1 = sqlCmd1.ExecuteReader();
                    while (dataReader1.Read())
                    {
                        Professor p = ProfessorManager.professorList.Find(p => p.Id == int.Parse(dataReader1["Id"].ToString()));
                        facolta.ProfessorsFaculty.Add(p);
                    }
                    dataReader1.Close();

                    //Recupero gli esami di ogni facoltà e li aggiungo alla lista ExamsFaculty
                    using SqlConnection sqlCnn2 = new(_connection.ConnectionString);
                    sqlCnn2.Open();
                    using SqlCommand sqlCmd2 = new("Select Id FROM Exam WHERE FacultyId = @id", sqlCnn2);
                    sqlCmd2.Parameters.AddWithValue("@id", id);
                    using SqlDataReader dataReader2 = sqlCmd2.ExecuteReader();
                    while (dataReader2.Read())
                    {
                        Exam e = ExamManager.examsList.Find(e => e.Id == int.Parse(dataReader2["Id"].ToString()));
                        facolta.ExamsFaculty.Add(e);
                    }

                }

            } 

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex);
            }
        }
        //Aggiunge una nuova facoltà nel database e nella lista
        public void AddFaculty()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("Inserire Id facoltà: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Inserire Nome facoltà: ");
                string name = Console.ReadLine();

                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
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
                Console.WriteLine("\nFacoltà aggiunta con successo!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex);
            }

        }
        //Aggiorna i dati di una facoltà nel database e nella lista
        public void UpdateFaculty()
        {
            Console.Clear();
           
            try
            {
                Console.WriteLine("Inserire il nome della facoltà da aggiornare: ");
                string nome = Console.ReadLine();
                Faculty f = FacultyManager.facultyList.Find(f => f.NameFaculty.Equals(nome));
                Console.WriteLine("Inserire il nuovo nome:");
                string newName = Console.ReadLine();
                f.NameFaculty = newName;

                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("UPDATE Faculty " +
                                               "SET NameFaculty = @newName " +
                                               "WHERE NameFaculty = @nome", sqlCnn);
                sqlCmd.Parameters.AddWithValue("@newName", newName);
                sqlCmd.Parameters.AddWithValue("@nome", nome);
                sqlCmd.ExecuteNonQuery();
                Console.WriteLine("Facoltà aggiornata con successo!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex);
            }

        }
        //Visualizza a console la lista delle facoltà
        public void ViewFaculties()
        {
            Console.Clear();
            Console.WriteLine("Elenco Facoltà: \n");
            foreach (Faculty f in FacultyManager.facultyList)
            {
                Console.WriteLine($"Nome facoltà: {f.NameFaculty}");
                foreach (Course c in f.CoursesFaculty)
                {
                    Console.WriteLine($"Nome Corso: {c.CourseName}");
                }
                foreach (Exam e in f.ExamsFaculty)
                {
                    Console.WriteLine($"Id Esame: {e.Id}");
                }
                foreach (Professor p in f.ProfessorsFaculty)
                {
                    Console.WriteLine($"Professore: {p.FullName}\n");
                }
            }
        }
    }
}
