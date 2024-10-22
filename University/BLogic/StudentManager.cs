using Microsoft.Data.SqlClient;
using System.Configuration;
using University.DataModel;

namespace University.BLogic
{
    public class StudentManager
    {
        ExceptionLogManager exLogManager = new();
        private readonly SqlConnection _connection = new();
        private SqlCommand _command = new();

        public static List<Student> studentList = new();
        //Importa i primi dati dal database
        public List<Student> GetStudents()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("SELECT * FROM STUDENT", sqlCnn);
                using SqlDataReader dataReader = sqlCmd.ExecuteReader();

                while (dataReader.Read())
                {
                    //per ogni studente nel database crea l'oggetto studente e lo aggiunge alla lista
                    studentList.Add(new Student
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        FullName = dataReader["FullName"].ToString().Trim(),
                        DateOfBirth = DateTime.Parse(dataReader["DateOfBirth"].ToString()),
                        Faculty = FacultyManager.facultyList.Find(f => f.Id == (int.Parse(dataReader["FacultyId"].ToString()))),
                        Tuition = decimal.Parse(dataReader["Tuition"].ToString()),
                        AvgGrades = decimal.Parse(dataReader["AvgGrades"].ToString())
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex);
            }

            return studentList;
        }
        //Importa i dati mancanti dal database
        public void GetStudents2()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();

                //Per ogni studente nella lista aggiungo i dati mancanti
                foreach (Student student in studentList)
                {
                    int id = student.Id;

                    //Recupero corsi studente e popolo la lista StudentsCourses
                    using SqlCommand sqlCmd = new("SELECT CourseId FROM Student_Course WHERE StudentId = @id", sqlCnn);
                    sqlCmd.Parameters.AddWithValue("@id", id);
                    using SqlDataReader dataReader = sqlCmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Course c = CourseManager.coursesList.Find(c => c.Id == int.Parse(dataReader["CourseId"].ToString()));
                        student.StudentsCourses.Add(c);
                    }
                    dataReader.Close();

                    //Recupero esami studente e popolo la lista StudentsExams
                    using SqlCommand sqlCmd1 = new("SELECT ExamId FROM Student_Exam WHERE StudentId = @id", sqlCnn);
                    sqlCmd1.Parameters.AddWithValue("@id", id);
                    using SqlDataReader dataReader1 = sqlCmd1.ExecuteReader();
                    while (dataReader1.Read())
                    {
                        Exam e = ExamManager.examsList.Find(e => e.Id == int.Parse(dataReader1["ExamId"].ToString()));
                        student.StudentsExams.Add(e);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex);
            }
        }
        //Aggiunge un nuovo studente al database e sulla lista
        public void AddStudent()
        {
            try
            {
                Console.Clear();
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

                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
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
                Console.WriteLine("\nStudente aggiunto con successo!\n");

                Student s = new Student
                {
                    Id = id,
                    FullName = name,
                    DateOfBirth = date,
                    Faculty = FacultyManager.facultyList.Find(f => f.Id == fId),
                    Tuition = tuition,
                    AvgGrades = avg
                };
                studentList.Add(s);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex);
            }

            
        }
        //Aggiorna studente sul database e nella lista
        public void UpdateStudent()
        {
            try
            {
                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();

                Console.Clear();
                Console.WriteLine("Inserire il nome dello studente da aggiornare: ");
                string nome = Console.ReadLine();
                Student s = studentList.Find(s => s.FullName.Equals(nome));
                Console.WriteLine("Cosa vuoi aggiornare? 1.Facoltà 2.Retta 3.Media");
                int scelta = int.Parse(Console.ReadLine());

                switch (scelta)
                {
                    //Aggiorna facoltà dello studente
                    case 1:

                        Console.WriteLine("Inserire il nome della nuova facoltà:");
                        string fName = Console.ReadLine();
                        Faculty f = FacultyManager.facultyList.Find(f => f.NameFaculty.Equals(fName));
                        s.Faculty = f;
                        int fId = f.Id;

                        using (SqlCommand sqlCmd = new("UPDATE Student " +
                                                       "SET FacultyId = @fId " +
                                                       "WHERE FullName = @nome", sqlCnn))
                        {
                            sqlCmd.Parameters.AddWithValue("@fId", fId);
                            sqlCmd.Parameters.AddWithValue("@nome", nome);
                            sqlCmd.ExecuteNonQuery();
                        }
                        break;

                    //Aggiorna la retta dello studente
                    case 2:

                        Console.WriteLine("Inserire la nuova retta: ");
                        decimal retta = decimal.Parse(Console.ReadLine());
                        s.Tuition = retta;

                        using (SqlCommand sqlCmd = new("UPDATE Student " +
                                                       "SET Tuition = @retta " +
                                                       "WHERE FullName = @nome", sqlCnn))
                        {
                            sqlCmd.Parameters.AddWithValue("@retta", retta);
                            sqlCmd.Parameters.AddWithValue("@nome", nome);
                            sqlCmd.ExecuteNonQuery();
                        }

                        break;

                    //Aggiorna la media dello studente
                    case 3:
                        Console.WriteLine("Inserire la nuova media: ");
                        decimal avg = decimal.Parse(Console.ReadLine());
                        s.AvgGrades = avg;

                        using (SqlCommand sqlCmd = new("UPDATE Student " +
                                                       "SET AvgGrades = @avg " +
                                                       "WHERE FullName = @nome", sqlCnn))
                        {
                            sqlCmd.Parameters.AddWithValue("@avg", avg);
                            sqlCmd.Parameters.AddWithValue("@nome", nome);
                            sqlCmd.ExecuteNonQuery();
                        }

                        break;
                }
                Console.WriteLine("\nDati aggiornati con successo!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex);
            }

        }
        //Stampa a console la lista di tutti gli studenti
        public void DeleteStudent()
        {
            
            try
            {
                Console.WriteLine("Inserisci il nome e cognome dello studente da eliminare: ");
                string nome = Console.ReadLine();

                _connection.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using SqlConnection sqlCnn = new(_connection.ConnectionString);
                sqlCnn.Open();
                using SqlCommand sqlCmd = new("DElETE FROM Student WHERE FullName = @nome", sqlCnn);
                sqlCmd.Parameters.AddWithValue("@nome", nome);
                sqlCmd.ExecuteNonQuery();
                Console.WriteLine("\nStudente eliminato con successo!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exLogManager.ExcLog(ex);
            }
        }
        //Visuaizza a console la lista degli studenti
        public void ViewStudents()
        {
            Console.Clear();
            Console.WriteLine("Elenco Studenti: \n");
            foreach (Student s in studentList)
            {
                Console.Write($"Nome: {s.FullName}\nData di nascita: {s.DateOfBirth}\nFacoltà: {s.Faculty.NameFaculty}\nRetta annuale: {s.Tuition}\nMedia Esami: {s.AvgGrades}\nCorsi: ");
                foreach (Course course in s.StudentsCourses)
                {
                    Console.WriteLine($"{course.CourseName}");
                }
                Console.Write("Esami: ");
                foreach (Exam exam in s.StudentsExams)
                {
                    Console.WriteLine($"Data: {exam.ExamDate}, Corso: {exam.Course.CourseName} ");
                }
                Console.WriteLine();
            }
        }
    }

}

