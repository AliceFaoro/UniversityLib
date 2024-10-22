using Microsoft.Data.SqlClient;
using System.Configuration;
using University.DataModel;

namespace University.BLogic
{
    public class ProfessorManager
    {
        ExceptionLogManager exLogManager = new();

        private readonly SqlConnection _connection = new();
        private SqlCommand _command = new();

        public static List<Professor> professorList = new();
        //Importa dati iniziali dal database
        public List<Professor> GetProfessors()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("SELECT * FROM PROFESSOR", sqlCnn);
                using SqlDataReader dataReader = sqlCmd.ExecuteReader();

                //Per ogni professore presente nel database creo un oggetto professore e lo aggiungo alla lista
                while (dataReader.Read())
                {
                    professorList.Add(new Professor
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        FullName = dataReader["FullName"].ToString().Trim(),
                        DateOfBirth = DateTime.Parse(dataReader["DateOfBirth"].ToString()),
                        Faculty = FacultyManager.facultyList.Find(f => f.Id == (int.Parse(dataReader["FacultyId"].ToString()))),
                        Pay = decimal.Parse(dataReader["Pay"].ToString())
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex);
            }
            return professorList;
        }
        //Importa dati mancanti dal database
        public void GetProfessors2()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();

                //Aggiungo la lista dei corsi tenuti da ogni professore
                //Dalla tabella nel database Course selezioni tutti i corsi che hanno il ProfessorId uguale all'id del professore nella lista 
                foreach (Professor prof in professorList)
                {
                    int id = prof.Id;
                    using SqlCommand sqlCmd = new("SELECT Id FROM Course WHERE ProfessorId = @id", sqlCnn);
                    sqlCmd.Parameters.AddWithValue("@id", id);
                    using SqlDataReader dataReader = sqlCmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Course c = CourseManager.coursesList.Find(c => c.Id == int.Parse(dataReader["Id"].ToString()));
                        prof.ProfessorsCourses.Add(c);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex);
            }
        }
        //Aggiungo professore nel database e nella lista
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

                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
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
                    Faculty = FacultyManager.facultyList.Find(f => f.Id == fId),
                    Pay = pay
                };
                professorList.Add(p);
                Console.WriteLine("\nProfessore aggiunto con successo!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex);
            }
        }
        //Modifico un professore sia nel database che nella lista
        public void UpdateProfessor()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();

                Console.Clear();
                Console.WriteLine("Inserire il nome del professore da aggiornare: ");
                string nome = Console.ReadLine();
                Professor p = professorList.Find(p => p.FullName.Equals(nome));
                Console.WriteLine("Cosa vuoi aggiornare? 1.Facoltà 2.Stipendio");
                int scelta = int.Parse(Console.ReadLine());

                switch (scelta)
                {
                    case 1:

                        Console.WriteLine("Inserire il nome della nuova facoltà:");
                        string fName = Console.ReadLine();
                        Faculty f = FacultyManager.facultyList.Find(f => f.NameFaculty.Equals(fName));
                        p.Faculty = f;
                        int fId = f.Id;

                        try
                        {
                            using SqlCommand sqlCmd = new("UPDATE Professor " +
                                                           "SET FacultyId = @fId " +
                                                           "WHERE FullName = @nome", sqlCnn);
                            sqlCmd.Parameters.AddWithValue("@fId", fId);
                            sqlCmd.Parameters.AddWithValue("@nome", nome);
                            sqlCmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            exLogManager.ExcLog(ex);
                        }

                        break;
                    case 2:

                        Console.WriteLine("Inserire il nuovo stipendio ");
                        decimal pay = decimal.Parse(Console.ReadLine());
                        p.Pay = pay;

                        try
                        {
                            using SqlCommand sqlCmd = new("UPDATE Professor " +
                                                           "SET Pay = @pay " +
                                                           "WHERE FullName = @nome", sqlCnn);
                            sqlCmd.Parameters.AddWithValue("@pay", pay);
                            sqlCmd.Parameters.AddWithValue("@nome", nome);
                            sqlCmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            exLogManager.ExcLog(ex);
                        }

                        break;

                }
                Console.WriteLine("\nProfessore aggiornato con successo!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex);
            }
        }
        //Visualizza a console la lista di tutti i professori presenti nella lista
        public void ViewProfessor()
        {
            Console.Clear();
            Console.WriteLine("Elenco Professori: \n");
            foreach (Professor p in professorList)
            {
                Console.WriteLine($"Nome: {p.FullName}\nData di nascita: {p.DateOfBirth}\nFacoltà: {p.Faculty.NameFaculty}\nStipendio: {p.Pay}");
                foreach (Course c in p.ProfessorsCourses)
                {
                    Console.Write($"Corso: {c.CourseName}\n\n");
                }
            }

        }
    }
}
