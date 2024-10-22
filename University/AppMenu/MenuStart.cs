using University.BLogic;

namespace University.AppMenu
{

    public static class MenuStart
    {
        public static void Show()
        {
            FacultyManager fManager = new();
            StudentManager sManager = new();
            ProfessorManager pManager = new();
            CourseManager cManager = new();
            ExamManager eManager = new();

            CreateMenu(fManager, sManager, pManager, cManager, eManager); 

            Console.WriteLine("\n\nPremere 'S' per continuare, qualsiasi altro tasto per uscire.");

            //Legge il tasto schiacciato dall'utente
            var s = Console.ReadKey();

            //Se il tasto corrisponde ad 'S' allora ricrea il menù
            while (s.Key == ConsoleKey.S)
            {
                Console.Clear();
                CreateMenu(fManager, sManager, pManager, cManager, eManager);
                Console.WriteLine("\n\nPremere 'S' per continuare, qualsiasi altro tasto per uscire.");
                s = Console.ReadKey();
            }

            //Se l'utente preme un qualsisi tasto diverso da 'S' si chiude l'applicazione
            Console.Clear();
            Console.WriteLine("\nHai premuto un tasto diverso da 'S'. Chiusura del programma...");
            Environment.Exit(0);
        }

        //Metodo per creare il menù
        private static void CreateMenu(FacultyManager f, StudentManager sM, ProfessorManager pM, CourseManager cM, ExamManager eM)
        {
            ExceptionLogManager exLogManager = new();

            Console.WriteLine("BENVENUTO!\n");
            Console.WriteLine("Effettua una scelta da 1 a 5:\n\n1. Importa Dati \n2. Aggiungi Dati\n3. Modifica Dati\n4. Cancella Dati\n5. Visualizza Elenco");
            Console.Write("\nScelta: ");
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
                        f.GetFaculties2();
                        sM.GetStudents2();
                        pM.GetProfessors2();
                        cM.GetCourses2();

                        Console.WriteLine("\nDati importati con successo!\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                        exLogManager.ExcLog(ex);
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
                            sM.UpdateStudent();
                            break;
                        case 3:
                            pM.UpdateProfessor();
                            break;
                        case 4:
                            cM.UpdateCourse();
                            break;
                        case 5:
                            eM.UpdateExam();
                            break;
                    }
                    break;

                case 4:
                    sM.DeleteStudent();
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


