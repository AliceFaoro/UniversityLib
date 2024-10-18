using University.BLogic;

namespace University.AppMenu
{

    public static class MenuStart
    {
        public static void Show()
        {
            UniManager uniManager = new();
            FacultyManager fManager = new();
            StudentManager sManager = new();
            ProfessorManager pManager = new();
            CourseManager cManager = new();
            ExamManager eManager = new();

            CreateMenu(fManager, sManager, pManager, cManager, eManager); Console.WriteLine("\n\nPremere 'S' per continuare, qualsiasi altro tasto per uscire.");
            var s = Console.ReadKey();
            while (s.Key == ConsoleKey.S)
            {
                Console.Clear();
                CreateMenu(fManager, sManager, pManager, cManager, eManager);
                Console.WriteLine("\n\nPremere 'S' per continuare, qualsiasi altro tasto per uscire.");
                s = Console.ReadKey();
            }
            Console.Clear();
            Console.WriteLine("Hai premuto un tasto diverso da 'S'. Chiusura del programma...");
            Environment.Exit(0);
        }

        private static void CreateMenu(FacultyManager f, StudentManager sM, ProfessorManager pM, CourseManager cM, ExamManager eM)
        {
            Console.WriteLine("BENVENUTO!\n");
            Console.WriteLine("Menù:\n1. Importare Dati dal Database\n2. Aggiungi\n3. Modifica\n4. Cancella\n5. Visualizza elenco");
            Console.WriteLine("\nEffettua una scelta da 1 a 5: ");
            int scelta = int.Parse(Console.ReadLine());
            switch (scelta)
            {
                case 1:
                    try
                    {
                        f.GetFaculties();
                        sM.GetStudents();
                        pM.GetProfessors();
                        cM.GetCourses();
                        eM.GetExams();
                        Console.WriteLine("\n\nDati importati con successo!\n");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"{e.Message}");
                    }

                    break;

                case 2:
                    Console.WriteLine("Chi desideri aggiungere? 1.Facoltà 2.Studente 3.Professore 4.Corso 5.Esame");
                    int s = int.Parse(Console.ReadLine());
                    switch (s)
                    {
                        case 1:
                            f.AddFaculty();
                            break;
                        case 2:
                            sM.AddStudent();
                            break;
                        case 3:
                            pM.AddProfessor();
                            break;
                        case 4:
                            cM.AddCourse();
                            break;
                        case 5:
                            eM.AddExam();
                            break;
                    }

                    break;
                case 3:
                    Console.WriteLine("Chi desideri modificare? 1.Facoltà 2.Studente 3.Professore 4.Corso 5.Esame");
                    s = int.Parse(Console.ReadLine());
                    switch (s)
                    {
                        case 1:
                            f.UpdateFaculty();
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                    }
                    break;
                case 4:
                    Console.WriteLine("Chi desideri cancellare? 1.Facoltà 2.Studente 3.Professore 4.Corso 5.Esame");
                    s = int.Parse(Console.ReadLine());
                    switch (s)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                    }
                    break;
                case 5:
                    Console.WriteLine("\nChi desideri visualizzare? 1.Facoltà 2.Studente 3.Professore 4.Corso 5.Esame");
                    s = int.Parse(Console.ReadLine());

                    switch(s)
                    {
                        case 1:
                            f.ViewFaculties(); 
                            break;
                        case 2:
                            sM.ViewStudents();
                            break;
                        case 3:
                            pM.ViewProfessor();
                            break;
                        case 4:
                            cM.ViewCourse();
                            break;
                        case 5:
                            eM.ViewExam();
                            break;
                    }

                    break;

            }

        }
    }
}


