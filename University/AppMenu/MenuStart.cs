using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.BLogic;

namespace University.AppMenu
{

    public static class MenuStart
    {
        public static void Show()
        {
            UniManager uniManager = new();
            DbManager dbManager = new();
            CreateMenu(dbManager); Console.WriteLine("\n\nPremere 'S' per continuare, qualsiasi altro tasto per uscire.");
            var s = Console.ReadKey();
            while (s.Key == ConsoleKey.S)
            {
                Console.Clear();
                CreateMenu(dbManager);
                Console.WriteLine("\n\nPremere 'S' per continuare, qualsiasi altro tasto per uscire.");
                s = Console.ReadKey();
            }
            Console.Clear();
            Console.WriteLine("Hai premuto un tasto diverso da 'S'. Chiusura del programma...");
            Environment.Exit(0);
        }

        private static void CreateMenu(DbManager d)
        {
            Console.WriteLine("BENVENUTO!\n");
            Console.WriteLine("Menù:\n1. Importare Dati dal Database\n2. Aggiungi\n3. Modifica\n4. Cancella");
            Console.WriteLine("\nEffettua una scelta da 1 a 5: ");
            int scelta = int.Parse(Console.ReadLine());
            switch (scelta)
            {
                case 1:
                    try
                    {
                        d.GetFaculties();
                        d.GetStudents();
                        d.GetProfessors();
                        d.GetCourses();
                        d.GetExams();
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
                            d.AddFaculty();
                            break;
                        case 2:
                            d.AddStudent();
                            break;
                        case 3:
                            d.AddProfessor();
                            break;
                        case 4:
                            d.AddCourse();
                            break;
                        case 5:
                            d.AddExam();
                            break;
                    }

                    break;
                case 3:
                    Console.WriteLine("Chi desideri modificare? 1.Facoltà 2.Studente 3.Professore 4.Corso 5.Esame");
                    s = int.Parse(Console.ReadLine());
                    switch (s)
                    {
                        case 1:
                            break;
                    }
                    break;

            }

        }
    }
}


