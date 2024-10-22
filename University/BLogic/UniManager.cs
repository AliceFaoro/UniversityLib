namespace University.BLogic
{
    public class UniManager
    {
         

        //public void ImportFiles()
        //{

        //    try
        //    {
        //        //Importazione file esami:
        //        string esamiFile = File.ReadAllText(Config.PathEsami);
        //        string[] esamiLines = esamiFile.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        //        foreach (string line in esamiLines)
        //        {
        //            string[] elements = line.Split(';');
        //            Esame e = listaEsami.Find(e => e.Id == int.Parse(elements[0]));
        //            if (e != null)
        //            {
        //                Console.WriteLine($"Esame {e.Id} già presente!");
        //            }
        //            else
        //            {
        //                Esame esame = new()
        //                {
        //                    Id = int.Parse(elements[0]),
        //                    NomeEsame = elements[1],
        //                };
        //                listaEsami.Add(esame);
        //            }


        //        }

        //        //Importazione file lezioni:
        //        string lezioniFile = File.ReadAllText(Config.PathLezioni);
        //        string[] lezioniLines = lezioniFile.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        //        foreach (string line in lezioniLines)
        //        {
        //            string[] elements = line.Split(';');

        //            Lezione l = listaLezioni.Find(l => l.Id == int.Parse(elements[0]));
        //            if (l != null)
        //            {
        //                Console.WriteLine($"Lezione {l.Id} già presente!");
        //            }
        //            else
        //            {
        //                Lezione lezione = new Lezione
        //                {
        //                    Id = int.Parse(elements[0]),
        //                    Data = DateTime.Parse(elements[2]),
        //                    Aula = elements[3],
        //                    Smart = bool.Parse(elements[4])
        //                };
        //                listaLezioni.Add(lezione);
        //            }

        //        }

        //        //Importazione file professori
        //        string profFile = File.ReadAllText(Config.PathProf);
        //        string[] profLines = profFile.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        //        foreach (string line in profLines)
        //        {
        //            string[] elements = line.Split(';');

        //            Professore p = listaProfessori.Find(p => p.Id == int.Parse(elements[0]));

        //            if (p != null)
        //            {
        //                Console.WriteLine($"Professore {p.Id} già inserito!");
        //            }
        //            else
        //            {
        //                Professore professore = new Professore
        //                {
        //                    Id = int.Parse(elements[0]),
        //                    Nome = elements[1],
        //                    Cognome = elements[2],
        //                    DataNascita = DateTime.Parse(elements[3]),
        //                    Stipendio = int.Parse(elements[5]),
        //                    oreSettimanali = int.Parse(elements[6])
        //                };
        //                listaProfessori.Add(professore);
        //            }
        //        }

        //        //Importazione file segretarie
        //        string segrFile = File.ReadAllText(Config.PathSegretarie);

        //        string[] segrLines = segrFile.Split("\r\n");

        //        foreach (string line in segrLines)
        //        {
        //            string[] elements = line.Split(';');

        //            Segretaria s = listaSegretarie.Find(s => s.Id == int.Parse(elements[0]));

        //            if (s != null)
        //            {
        //                Console.WriteLine($"Segretaria {s.Id} già presente!");
        //            }
        //            else
        //            {
        //                Segretaria segretaria = new Segretaria
        //                {
        //                    Id = int.Parse(elements[0]),
        //                    Nome = elements[1],
        //                    Cognome = elements[2],
        //                    DataNascita = DateTime.Parse(elements[3]),
        //                    Stipendio = decimal.Parse(elements[5]),
        //                    oreSettimanali = int.Parse(elements[6]),
        //                    TelefonoAziendale = bool.Parse(elements[7])
        //                };
        //                listaSegretarie.Add(segretaria);
        //            }
        //        }

        //        //Importazione file studenti
        //        string studFile = File.ReadAllText(Config.PathStudenti);
        //        string[] studLines = studFile.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        //        foreach (string line in studLines)
        //        {
        //            string[] elements = line.Split(";");
        //            Studente s = listaStudenti.Find(s => s.Id == int.Parse(elements[0]));
        //            if (s != null)
        //            {
        //                Console.WriteLine($"Studente {s.Id} già presente!");
        //            }
        //            else
        //            {
        //                Studente studente = new Studente
        //                {
        //                    Id = int.Parse(elements[0]),
        //                    Nome = elements[1],
        //                    Cognome = elements[2],
        //                    DataNascita = DateTime.Parse(elements[3]),
        //                    Retta = int.Parse(elements[6]),
        //                    Media = decimal.Parse(elements[7])
        //                };
        //                listaStudenti.Add(studente);
        //            }
        //        }

        //        //Importazione file facoltà
        //        string facFile = File.ReadAllText(Config.PathFacolta);
        //        string[] facLines = facFile.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        //        foreach (string line in facLines)
        //        {
        //            string[] elements = line.Split(';');
        //            Facolta f = listaFacolta.Find(f => f.Id == int.Parse(elements[0]));
        //            if (f != null)
        //            {
        //                Console.WriteLine($"Facoltà {f.Id} già presente");
        //            }
        //            else
        //            {
        //                Facolta facolta = new Facolta
        //                {
        //                    Id = int.Parse(elements[0]),
        //                    NomeFacolta = elements[1],
        //                };
        //                listaFacolta.Add(facolta);
        //            }
        //        }

        //        //Importazione file università
        //        string uniFile = File.ReadAllText(Config.PathUni);
        //        string[] uniLines = uniFile.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        //        foreach (string uniLine in uniLines)
        //        {
        //            string[] elements = uniLine.Split(";");
        //            Universita u = listaUniversita.Find(u => u.Id == int.Parse(elements[0]));
        //            if (u != null)
        //            {
        //                Console.WriteLine($"Università {u.Id} già presente!");
        //            }
        //            else
        //            {
        //                Universita uni = new Universita
        //                {
        //                    Id = int.Parse(elements[0]),
        //                    Nome = elements[1],
        //                };
        //                listaUniversita.Add(uni);
        //            }

        //        }


        //        //Importazione dati mancanti perchè collegati ad altre classi.

        //        //Dati mancanti esami
        //        foreach (Esame esame in listaEsami)
        //        {
        //            string[] elements = esamiLines[esame.Id - 1].Split(';');
        //            esame.Facolta = listaFacolta.Find(f => f.Id == int.Parse(elements[2]));
        //            esame.Professore = listaProfessori.Find(p => p.Id == int.Parse(elements[3]));

        //        }

        //        //Dati mancanti lezioni
        //        foreach (Lezione lezione in listaLezioni)
        //        {
        //            string[] elements = lezioniLines[lezione.Id - 1].Split(";");
        //            lezione.Esame = listaEsami.Find(e => e.Id == int.Parse(elements[1]));
        //            lezione.Professore = listaProfessori.Find(p => p.Id == int.Parse(elements[5]));
        //        }

        //        //Dati mancanti professori
        //        foreach (Professore prof in listaProfessori)
        //        {
        //            string[] elements = profLines[prof.Id - 1].Split(";");
        //            prof.Universita = listaUniversita.Find(u => u.Id == int.Parse(elements[4]));
        //            string[] exams = elements[7].Split(',');
        //            for (int i = 0; i < exams.Length - 1; i++)
        //            {
        //                prof.Esami.Add(listaEsami.Find(e => e.Id == int.Parse(exams[i])));
        //            }
        //        }

        //        //Dati mancanti segretarie
        //        foreach (Segretaria se in listaSegretarie)
        //        {
        //            string[] elements = segrLines[se.Id - 1].Split(";");
        //            se.Universita = listaUniversita.Find(u => u.Id == int.Parse(elements[4]));

        //        }

        //        //Dati mancanti studenti
        //        foreach (Studente s in listaStudenti)
        //        {
        //            string[] elements = studLines[s.Id - 1].Split(";");
        //            s.Universita = listaUniversita.Find(u => u.Id == int.Parse(elements[4]));
        //            string[] exams = elements[5].Split(",");
        //            for (int i = 0; i < exams.Length - 1; i++)
        //            {
        //                s.Esami.Add(listaEsami.Find(e => e.Id == int.Parse(exams[i])));
        //            }


        //        }

        //        //Dati mancanti facoltà
        //        foreach (Facolta f in listaFacolta)
        //        {
        //            string[] elements = facLines[f.Id - 1].Split(";");
        //            f.Universita = listaUniversita.Find(u => u.Id == int.Parse(elements[2]));

        //            string[] exams = elements[3].Split(',');
        //            for (int i = 0; i < exams.Length - 1; i++)
        //            {
        //                f.Esami.Add(listaEsami.Find(e => e.Id == int.Parse(exams[i])));
        //            }
        //            string[] profs = elements[4].Split(",");
        //            for (int i = 0; i < profs.Length - 1; i++)
        //            {
        //                f.Professori.Add(listaProfessori.Find(p => p.Id == int.Parse(exams[i])));
        //            }


        //        }

        //        //Dati mancanti università
        //        foreach (Universita u in listaUniversita)
        //        {
        //            string[] elements = uniLines[u.Id - 1].Split(";");

        //            string[] profs = elements[2].Split(',');
        //            for (int i = 0; i < profs.Length - 1; i++)
        //            {
        //                u.Professori.Add(listaProfessori.Find(p => p.Id == int.Parse(profs[i])));
        //            }

        //            string[] segr = elements[3].Split(',');
        //            for (int i = 0; i < segr.Length - 1; i++)
        //            {
        //                u.Segretarie.Add(listaSegretarie.Find(s => s.Id == int.Parse(segr[i])));
        //            }

        //            string[] fac = elements[4].Split(',');
        //            for (int i = 0; i < fac.Length - 1; i++)
        //            {
        //                u.Facolta.Add(listaFacolta.Find(s => s.Id == int.Parse(fac[i])));
        //            }

        //            string[] stud = elements[3].Split(',');
        //            for (int i = 0; i < stud.Length - 1; i++)
        //            {
        //                u.Studenti.Add(listaStudenti.Find(s => s.Id == int.Parse(stud[i])));
        //            }


        //        }

        //        Console.WriteLine("Dati importati correttamente!");

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Errore nell'importazione dei file: {ex.Message}");
        //        logManager.LogFile(ex);
        //    }
        //}

        //public void AddNewStudent()
        //{
        //    Console.Clear();
        //    Studente s = new();

        //    bool isValid = false;
        //    do
        //    {
        //        Console.WriteLine("Inserire ID: ");
        //        string idStringa = Console.ReadLine();
        //        int id;

        //        if (int.TryParse(idStringa, out id))
        //        {
        //            Studente stud = listaStudenti.Find(s => s.Id == id);
        //            if (stud == null)
        //            {
        //                s.Id = id;
        //                isValid = true;
        //            }
        //            else
        //            {
        //                Console.WriteLine("Studente già esistente, vuoi aggiornarlo? S/N");
        //                string scelta = Console.ReadLine();
        //                if (scelta.Equals("S"))
        //                {
        //                    Update();
        //                }
        //                else
        //                {
        //                    isValid = false;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Inserire un numero!\n");
        //        }
        //    } while (!isValid);

        //    Console.WriteLine("Inserire nome: ");
        //    s.Nome = Console.ReadLine();
        //    Console.WriteLine("Inserire cognome: ");
        //    s.Cognome = Console.ReadLine();

        //    isValid = false;
        //    do
        //    {
        //        Console.WriteLine("Inserire data di nascita: ");
        //        string dateS = Console.ReadLine();
        //        DateTime date;
        //        if (DateTime.TryParse(dateS, out date))
        //        {
        //            s.DataNascita = date;
        //            isValid = true;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Inserire una data di nascita valida!");
        //        }
        //    } while (!isValid);

        //    try
        //    {
        //        Console.WriteLine("Inserire Università: ");
        //        string uni = Console.ReadLine();
        //        s.Universita = listaUniversita.Find(u => u.Nome.Equals(uni));
        //        Console.WriteLine("Inserire nome dell'esame: ");
        //        string esame = Console.ReadLine();
        //        s.Esami.Add(listaEsami.Find(e => e.NomeEsame.Equals(esame)));

        //        Console.WriteLine("Desideri aggiungere altri esami? S/N");
        //        string scelta = Console.ReadLine();
        //        while (scelta.Equals("S"))
        //        {
        //            Console.WriteLine("Inserire nome dell'esame: ");
        //            esame = Console.ReadLine();
        //            s.Esami.Add(listaEsami.Find(e => e.NomeEsame.Equals(esame)));
        //            Console.WriteLine("Desideri aggiungere altri esami? S/N");
        //            scelta = Console.ReadLine();
        //        }

        //        Console.WriteLine("Inserire retta: ");
        //        s.Retta = decimal.Parse(Console.ReadLine());
        //        Console.WriteLine("Inserire media: ");
        //        s.Media = decimal.Parse(Console.ReadLine());

        //        listaStudenti.Add(s);

        //        Console.WriteLine("\nStudente inserito correttamente!");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Errore nell'inserimento: {ex.Message}");
        //        logManager.LogFile(ex);
        //    }
        //}

        //public void Update()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Inserisci il tipo di dato da aggiornare: 1.Esame, 2.Facoltà, 3.Lezione, 4.Professore, 5.Segretaria, 6.Studente, 7.Università: \n");
        //    int scelta = int.Parse(Console.ReadLine());
        //    switch (scelta)
        //    {
        //        //Update esame:
        //        case 1:
        //            Console.WriteLine("Inserire ID esame: ");
        //            Esame e = listaEsami.Find(e => e.Id == int.Parse(Console.ReadLine()));

        //            Console.WriteLine("Cosa desideri aggiornare? 1.Nome Esame, 2.ID Facoltà, 3.ID Professore");
        //            int s = int.Parse(Console.ReadLine());
        //            switch (s)
        //            {
        //                case 1:
        //                    Console.WriteLine("Inserire nuovo nome: ");
        //                    e.NomeEsame = Console.ReadLine();
        //                    break;
        //                case 2:
        //                    Console.WriteLine("Inserire nuovo ID facoltà: ");
        //                    e.Facolta.Id = int.Parse(Console.ReadLine());
        //                    break;
        //                case 3:
        //                    Console.WriteLine("Inserire nuovo ID professore: ");
        //                    break;
        //                default:
        //                    Console.WriteLine("Inserire numero valido!");
        //                    break;
        //            }
        //            break;

        //        //Update Facoltà
        //        case 2:
        //            Console.WriteLine("Inserire ID Facoltà: ");
        //            Facolta f = listaFacolta.Find(f => f.Id == int.Parse(Console.ReadLine()));

        //            Console.WriteLine("Cosa desideri aggiornare? 1.Nome Facoltà, 2.ID Università, 3.Lista Esami, 4.Lista Professori");
        //            int sFac = int.Parse(Console.ReadLine());
        //            switch (sFac)
        //            {
        //                case 1:
        //                    Console.WriteLine("Inserire nuovo nome: ");
        //                    f.NomeFacolta = Console.ReadLine();
        //                    break;
        //                case 2:
        //                    Console.WriteLine("Inserire nuovo ID università: ");
        //                    f.Universita.Id = int.Parse(Console.ReadLine());
        //                    break;
        //                case 3:
        //                    string s1 = " ";
        //                    do
        //                    {
        //                        Console.WriteLine("Aggiungere ID esame da aggiungere: ");
        //                        f.Esami.Add(listaEsami.Find(e => e.Id == int.Parse(Console.ReadLine())));
        //                        Console.WriteLine("Desideri aggiungere un altro esame? S/N");
        //                        s1 = Console.ReadLine();
        //                    } while (s1.Equals("S"));

        //                    break;

        //                case 4:
        //                    string s2 = " ";
        //                    do
        //                    {
        //                        Console.WriteLine("Aggiungere ID Professore da aggiungere: ");
        //                        f.Professori.Add(listaProfessori.Find(p => p.Id == int.Parse(Console.ReadLine())));
        //                        Console.WriteLine("Desideri aggiungere un altro Professore? S/N");
        //                        s1 = Console.ReadLine();
        //                    } while (s2.Equals("S"));

        //                    break;
        //                default:
        //                    Console.WriteLine("Inserire numero valido!");
        //                    break;
        //            }
        //            break;

        //        //Update Lezione
        //        case 3:
        //            Console.WriteLine("Inserire ID Lezione: ");
        //            Lezione l = listaLezioni.Find(l => l.Id == int.Parse(Console.ReadLine()));

        //            Console.WriteLine("Cosa desideri aggiornare? 1.ID Esame, 2.Data, 3.Aula, 4.Smart Working, 5.ID Professore");
        //            int sLez = int.Parse(Console.ReadLine());

        //            switch (sLez)
        //            {
        //                case 1:
        //                    Console.WriteLine("Inserire nuovo ID Esame:");
        //                    l.Esame.Id = int.Parse(Console.ReadLine());
        //                    break;
        //                case 2:
        //                    Console.WriteLine("Inserire nuova data: ");
        //                    l.Data = DateTime.Parse(Console.ReadLine());
        //                    break;
        //                case 3:
        //                    Console.WriteLine("Inserire nuova aula: ");
        //                    l.Aula = Console.ReadLine();
        //                    break;
        //                case 4:
        //                    Console.WriteLine("Smart Working? true/false");
        //                    l.Smart = bool.Parse(Console.ReadLine());
        //                    break;
        //                case 5:
        //                    Console.WriteLine("Inserire ID professore: ");
        //                    l.Professore.Id = int.Parse(Console.ReadLine());
        //                    break;
        //                default:
        //                    Console.WriteLine("Inserire numero valido!");
        //                    break;
        //            }
        //            break;

        //        //Update Professore
        //        case 4:
        //            Console.WriteLine("Inserire ID Professore: ");
        //            Professore p = listaProfessori.Find(p => p.Id == int.Parse(Console.ReadLine()));

        //            Console.WriteLine("Cosa vuoi modificare: 1. ID Università, 2.Stipendio, 3.Ore Settimanali, 4.Lista Esami");
        //            int sProf = int.Parse(Console.ReadLine());
        //            switch (sProf)
        //            {
        //                case 1:
        //                    Console.WriteLine("Inserire il nuovo ID Università: ");
        //                    p.Universita.Id = int.Parse(Console.ReadLine());
        //                    break;
        //                case 2:
        //                    Console.WriteLine("Inserire il nuovo stipendio: ");
        //                    p.Stipendio = int.Parse(Console.ReadLine());
        //                    break;
        //                case 3:
        //                    Console.WriteLine("Inserire le ore settimanali: ");
        //                    p.oreSettimanali = int.Parse(Console.ReadLine());
        //                    break;
        //                case 4:
        //                    string s1 = " ";
        //                    do
        //                    {
        //                        Console.WriteLine("Aggiungere ID esame da aggiungere: ");
        //                        p.Esami.Add(listaEsami.Find(e => e.Id == int.Parse(Console.ReadLine())));
        //                        Console.WriteLine("Desideri aggiungere un altro esame? S/N");
        //                        s1 = Console.ReadLine();
        //                    } while (s1.Equals("S"));
        //                    break;
        //            }
        //            break;

        //        //Update Segretaria
        //        case 5:
        //            Console.WriteLine("Inserire ID Segretaria: ");
        //            Segretaria se = listaSegretarie.Find(s => s.Id == int.Parse(Console.ReadLine()));

        //            Console.WriteLine("Cosa vuoi modificare: 1. ID Università, 2.Stipendio, 3.Ore Settimanali, 4.Telefono Aziendale");
        //            int sSegr = int.Parse(Console.ReadLine());
        //            switch (sSegr)
        //            {
        //                case 1:
        //                    Console.WriteLine("Inserire il nuovo ID Università: ");
        //                    se.Universita.Id = int.Parse(Console.ReadLine());
        //                    break;
        //                case 2:
        //                    Console.WriteLine("Inserire il nuovo stipendio: ");
        //                    se.Stipendio = int.Parse(Console.ReadLine());
        //                    break;
        //                case 3:
        //                    Console.WriteLine("Inserire le ore settimanali: ");
        //                    se.oreSettimanali = int.Parse(Console.ReadLine());
        //                    break;
        //                case 4:
        //                    Console.WriteLine("Telefono Aziendale? true/false");
        //                    se.TelefonoAziendale = bool.Parse(Console.ReadLine());
        //                    break;

        //            }

        //            break;

        //        //Update Studente
        //        case 6:
        //            Console.WriteLine("Inserire ID Studente: ");
        //            Studente st = listaStudenti.Find(s => s.Id == int.Parse(Console.ReadLine()));

        //            Console.WriteLine("Cosa vuoi modificare: 1. ID Università, 2.Esami, 3.Retta, 4.Media");
        //            int sStud = int.Parse(Console.ReadLine());
        //            switch (sStud)
        //            {
        //                case 1:
        //                    Console.WriteLine("Inserire il nuovo ID Università: ");
        //                    st.Universita.Id = int.Parse(Console.ReadLine());
        //                    break;
        //                case 2:
        //                    string s1 = " ";
        //                    do
        //                    {
        //                        Console.WriteLine("Aggiungere ID esame da aggiungere: ");
        //                        st.Esami.Add(listaEsami.Find(e => e.Id == int.Parse(Console.ReadLine())));
        //                        Console.WriteLine("Desideri aggiungere un altro esame? S/N");
        //                        s1 = Console.ReadLine();
        //                    } while (s1.Equals("S"));
        //                    break;
        //                case 3:
        //                    Console.WriteLine("Inserire Retta: ");
        //                    st.Retta = decimal.Parse(Console.ReadLine());
        //                    break;
        //                case 4:
        //                    Console.WriteLine("Inserire Media: ");
        //                    st.Media = decimal.Parse(Console.ReadLine());
        //                    break;

        //            }
        //            break;

        //        //Update Università
        //        case 7:
        //            Console.WriteLine("Inserire ID Università");
        //            Universita u = listaUniversita.Find(u => u.Id == int.Parse(Console.ReadLine()));
        //            Console.WriteLine("Cosa Vuoi Modificare? 1.Lista Professori 2.Lista Segretarie 3.Lista Facoltà 4.Lista Studenti");
        //            int sUni = int.Parse(Console.ReadLine());
        //            switch (sUni)
        //            {
        //                case 1:
        //                    Console.WriteLine("");
        //                    break;
        //            }
        //            break;

        //    }


        //}
        //public void Add()
        //{
        //    Console.WriteLine("Chi desideri aggiungere? 1.Esame 2.Facoltà, 3.Lezione, 4.Professore, 5.Segretaria, 6.Studente, 7.Università");
        //    int scelta = int.Parse(Console.ReadLine());
        //    switch (scelta)
        //    {
        //        case 1:
        //            AddNewExam();
        //            break;

        //        case 2:
        //            AddNewFacolta();
        //            break;

        //        case 3:
        //            AddNewLesson();
        //            break;

        //        case 4:
        //            AddNewTeacher();
        //            break;

        //        case 5:
        //            AddNewSecretary();
        //            break;

        //        case 6:
        //            AddNewStudent();
        //            break;

        //        case 7:
        //            AddNewUniversity();
        //            break;


        //    }
        //}
        //public void AddNewTeacher()
        //{
        //    Console.Clear();
        //    Professore p = new();

        //    bool isValid = false;
        //    do
        //    {
        //        Console.WriteLine("Inserire ID: ");
        //        string idStringa = Console.ReadLine();
        //        int id;
        //        if (int.TryParse(idStringa, out id))
        //        {
        //            p.Id = id;
        //            isValid = true;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Inserire un numero!\n");
        //        }
        //    } while (!isValid);

        //    Console.WriteLine("Inserisci Nome: ");
        //    p.Nome = Console.ReadLine();
        //    Console.WriteLine("Inserisci Cognome: ");
        //    p.Cognome = Console.ReadLine();

        //    isValid = false;
        //    do
        //    {
        //        Console.WriteLine("Inserire data di nascita: ");
        //        string dateP = Console.ReadLine();
        //        DateTime date;
        //        if (DateTime.TryParse(dateP, out date))
        //        {
        //            p.DataNascita = date;
        //            isValid = true;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Inserire una data di nascita valida!");
        //        }
        //    } while (!isValid);

        //    try
        //    {
        //        Console.WriteLine("Inserisci Nome Università: ");
        //        string uni = Console.ReadLine();
        //        p.Universita = listaUniversita.Find(u => u.Nome.Equals(uni));
        //        Console.WriteLine("Inserisci Stipendio: ");
        //        p.Stipendio = decimal.Parse(Console.ReadLine());
        //        Console.WriteLine("Inserisci Ore Settimanali: ");
        //        p.oreSettimanali = int.Parse(Console.ReadLine());
        //        Console.WriteLine("Inserisci nome Esame: ");
        //        string esame = Console.ReadLine();
        //        p.Esami.Add(listaEsami.Find(e => e.NomeEsame.Equals(esame)));
        //        Console.WriteLine("Vuoi inserire un altro esame? S/N");
        //        string s = Console.ReadLine();
        //        while (s.Equals("S"))
        //        {
        //            Console.WriteLine("Inserisci nome Esame: ");
        //            esame = Console.ReadLine();
        //            p.Esami.Add(listaEsami.Find(e => e.NomeEsame.Equals(esame)));
        //        }
        //        Console.WriteLine("Insegnante aggiunto correttamente!");

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Errore durante l'inserimento: {ex.Message}");
        //        logManager.LogFile(ex);
        //    }
        //}
        //public void AddNewSecretary()
        //{
        //    Console.Clear();
        //    Segretaria s = new();

        //    bool isValid = false;
        //    do
        //    {
        //        Console.WriteLine("Inserire ID: ");
        //        string idStringa = Console.ReadLine();
        //        int id;
        //        if (int.TryParse(idStringa, out id))
        //        {
        //            s.Id = id;
        //            isValid = true;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Inserire un numero!\n");
        //        }
        //    } while (!isValid);

        //    Console.WriteLine("Inserisci Nome: ");
        //    s.Nome = Console.ReadLine();
        //    Console.WriteLine("Inserisci Cognome: ");
        //    s.Cognome = Console.ReadLine();

        //    isValid = false;
        //    do
        //    {
        //        Console.WriteLine("Inserire data di nascita: ");
        //        string dateS = Console.ReadLine();
        //        DateTime date;
        //        if (DateTime.TryParse(dateS, out date))
        //        {
        //            s.DataNascita = date;
        //            isValid = true;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Inserire una data di nascita valida!");
        //        }
        //    } while (!isValid);

        //    try
        //    {
        //        Console.WriteLine("Inserisci Nome Università: ");
        //        string uni = Console.ReadLine();
        //        s.Universita = listaUniversita.Find(u => u.Nome.Equals(uni));
        //        Console.WriteLine("Inserisci Stipendio: ");
        //        s.Stipendio = decimal.Parse(Console.ReadLine());
        //        Console.WriteLine("Inserisci Ore Settimanali: ");
        //        s.oreSettimanali = int.Parse(Console.ReadLine());
        //        Console.WriteLine("Telefono aziendale? True/False");
        //        s.TelefonoAziendale = bool.Parse(Console.ReadLine());

        //        Console.WriteLine("Segretaria aggiunta correttamente!");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Errore durante l'inserimento: {ex.Message}");
        //        logManager.LogFile(ex);
        //    }
        //}

        //public void AddNewExam()
        //{
        //    try
        //    {
        //        Console.Clear();
        //        Esame e = new();

        //        bool isValid = false;
        //        do
        //        {
        //            Console.WriteLine("Inserire ID: ");
        //            string idStringa = Console.ReadLine();
        //            int id;
        //            if (int.TryParse(idStringa, out id))
        //            {
        //                e.Id = id;
        //                isValid = true;
        //            }
        //            else
        //            {
        //                Console.WriteLine("Inserire un numero!\n");
        //            }
        //        } while (!isValid);


        //        Console.WriteLine("Inserisci Il nome: ");
        //        e.NomeEsame = Console.ReadLine();
        //        Console.WriteLine("Inserisci l'id del professore");
        //        int idProf = int.Parse(Console.ReadLine());
        //        e.Professore = listaProfessori.Find(p => p.Id == idProf);

        //        listaEsami.Add(e);

        //        Console.WriteLine("Esame aggiunto correttamente!");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Errore durante l'inserimento: {ex.Message}");
        //        logManager.LogFile(ex);
        //    }
        //}
        //public void AddNewFacolta()
        //{
        //    try
        //    {
        //        Console.Clear();
        //        Facolta f = new();

        //        bool isValid = false;
        //        do
        //        {
        //            Console.WriteLine("Inserire ID Facoltà: ");
        //            string idStringa = Console.ReadLine();
        //            int id;
        //            if (int.TryParse(idStringa, out id))
        //            {
        //                f.Id = id;
        //                isValid = true;
        //            }
        //            else
        //            {
        //                Console.WriteLine("Inserire un numero!\n");
        //            }
        //        } while (!isValid);


        //        Console.WriteLine("Inserisci Il nome: ");
        //        f.NomeFacolta = Console.ReadLine();

        //        Console.WriteLine("Inserisci l'id dell'università: ");
        //        int idUni = int.Parse(Console.ReadLine());
        //        f.Universita = listaUniversita.Find(u => u.Id == idUni);

        //        Console.WriteLine("Inserisci l'id dell'esame da aggiungere: ");
        //        int idEsami = int.Parse(Console.ReadLine());
        //        f.Esami.Add(listaEsami.Find(e => e.Id == idEsami));

        //        Console.WriteLine("Vuoi inserire un altro esame? S/N");
        //        string s = Console.ReadLine();
        //        while (s.Equals("S"))
        //        {
        //            Console.WriteLine("Inserisci id Esame: ");
        //            int idEsame = int.Parse(Console.ReadLine());
        //            f.Esami.Add(listaEsami.Find(e => e.Id == idEsame));
        //        }

        //        Console.WriteLine("Inserisci l'id del professore da aggiungere: ");
        //        int idProf = int.Parse(Console.ReadLine());
        //        f.Professori.Add(listaProfessori.Find(p => p.Id == idProf));

        //        Console.WriteLine("Vuoi inserire un altro Professore? S/N");
        //        s = Console.ReadLine();
        //        while (s.Equals("S"))
        //        {
        //            Console.WriteLine("Inserisci id Professore: ");
        //            idProf = int.Parse(Console.ReadLine());
        //            f.Professori.Add(listaProfessori.Find(p => p.Id == idProf));
        //        }

        //        listaFacolta.Add(f);

        //        Console.WriteLine("Facoltà aggiunta correttamente!");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Errore durante l'inserimento: {ex.Message}");
        //        logManager.LogFile(ex);
        //    }
        //}
        //public void AddNewLesson()
        //{
        //    try
        //    {
        //        Console.Clear();
        //        Lezione l = new();

        //        bool isValid = false;
        //        do
        //        {
        //            Console.WriteLine("Inserire ID Lezione: ");
        //            string idStringa = Console.ReadLine();
        //            int id;
        //            if (int.TryParse(idStringa, out id))
        //            {
        //                l.Id = id;
        //                isValid = true;
        //            }
        //            else
        //            {
        //                Console.WriteLine("Inserire un numero!\n");
        //            }
        //        } while (!isValid);

        //        Console.WriteLine("Inserisci l'id dell'esame: ");
        //        int idEsami = int.Parse(Console.ReadLine());
        //        l.Esame = listaEsami.Find(e => e.Id == idEsami);

        //        isValid = false;
        //        do
        //        {
        //            Console.WriteLine("Inserire la data della lezione: ");
        //            string dateP = Console.ReadLine();
        //            DateTime date;
        //            if (DateTime.TryParse(dateP, out date))
        //            {
        //                l.Data = date;
        //                isValid = true;
        //            }
        //            else
        //            {
        //                Console.WriteLine("Inserire una data valida!");
        //            }
        //        } while (!isValid);

        //        Console.WriteLine("Inserire l'aula: ");
        //        l.Aula = Console.ReadLine();

        //        Console.WriteLine("E' possibile frequentare da remoto? True/False.");
        //        l.Smart = bool.Parse(Console.ReadLine());

        //        Console.WriteLine("Inserisci l'id del professore: ");
        //        int idProf = int.Parse(Console.ReadLine());
        //        l.Professore = listaProfessori.Find(p => p.Id == idProf);

        //        listaLezioni.Add(l);

        //        Console.WriteLine("Lezione aggiunta correttamente!");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Errore durante l'inserimento: {ex.Message}");
        //        logManager.LogFile(ex);
        //    }
        //}
        //public void AddNewUniversity()
        //{
        //    try
        //    {
        //        Console.Clear();
        //        Universita u = new();

        //        bool isValid = false;
        //        do
        //        {
        //            Console.WriteLine("Inserire ID Università: ");
        //            string idStringa = Console.ReadLine();
        //            int id;
        //            if (int.TryParse(idStringa, out id))
        //            {
        //                u.Id = id;
        //                isValid = true;
        //            }
        //            else
        //            {
        //                Console.WriteLine("Inserire un numero!\n");
        //            }
        //        } while (!isValid);


        //        Console.WriteLine("Inserisci Il nome: ");
        //        u.Nome = Console.ReadLine();

        //        Console.WriteLine("Inserisci l'id del professore da aggiungere: ");
        //        int idProf = int.Parse(Console.ReadLine());
        //        u.Professori.Add(listaProfessori.Find(p => p.Id == idProf));

        //        Console.WriteLine("Vuoi inserire un altro Professore? S/N");
        //        string s = Console.ReadLine();
        //        while (s.Equals("S"))
        //        {
        //            Console.WriteLine("Inserisci id Professore: ");
        //            idProf = int.Parse(Console.ReadLine());
        //            u.Professori.Add(listaProfessori.Find(p => p.Id == idProf));
        //        }

        //        Console.WriteLine("Inserisci l'id della segretaria da aggiungere: ");
        //        int idSegr = int.Parse(Console.ReadLine());
        //        u.Segretarie.Add(listaSegretarie.Find(s => s.Id == idSegr));

        //        Console.WriteLine("Vuoi inserire un'altra segretaria? S/N");
        //        s = Console.ReadLine();
        //        while (s.Equals("S"))
        //        {
        //            Console.WriteLine("Inserisci id Segretaria: ");
        //            idSegr = int.Parse(Console.ReadLine());
        //            u.Segretarie.Add(listaSegretarie.Find(s => s.Id == idSegr));
        //        }

        //        Console.WriteLine("Inserisci l'id della facoltà da aggiungere: ");
        //        int idFaco = int.Parse(Console.ReadLine());
        //        u.Facolta.Add(listaFacolta.Find(f => f.Id == idFaco));

        //        Console.WriteLine("Vuoi inserire un'altra facoltà? S/N");
        //        s = Console.ReadLine();
        //        while (s.Equals("S"))
        //        {
        //            Console.WriteLine("Inserisci id Facoltà: ");
        //            idFaco = int.Parse(Console.ReadLine());
        //            u.Facolta.Add(listaFacolta.Find(f => f.Id == idFaco));
        //        }

        //        Console.WriteLine("Inserisci l'id dello studente da aggiungere: ");
        //        int idStud = int.Parse(Console.ReadLine());
        //        u.Studenti.Add(listaStudenti.Find(s => s.Id == idStud));

        //        Console.WriteLine("Vuoi inserire un altro studente? S/N");
        //        s = Console.ReadLine();
        //        while (s.Equals("S"))
        //        {
        //            Console.WriteLine("Inserisci id Studente: ");
        //            idStud = int.Parse(Console.ReadLine());
        //            u.Studenti.Add(listaStudenti.Find(s => s.Id == idStud));
        //        }

        //        listaUniversita.Add(u);

        //        Console.WriteLine("Università aggiunta correttamente!");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Errore durante l'inserimento: {ex.Message}");
        //        logManager.LogFile(ex);
        //    }
        //}
        //public void Remove()
        //{
        //    Console.WriteLine("Cosa desideri eliminare? 1.Esame 2.Facoltà 3.Lezione 4.Professore 5.Segretaria 6.Studente 7.Università");
        //    int scelta = int.Parse(Console.ReadLine());
        //    switch (scelta)
        //    {
        //        case 1:
        //            break;
        //        case 2:
        //            break;
        //        case 4:
        //            break;
        //        case 5:
        //            break;
        //        case 6:
        //            break;
        //        case 7:
        //            break;

        //    }
        //}
        //public void RemoveStudent()
        //{
        //    Console.Clear();
        //    try
        //    {
        //        Console.WriteLine("Inserisci l'ID dello studente da eliminare");
        //        string stud = Console.ReadLine();
        //        listaStudenti.Remove(listaStudenti.Find(s => s.Id == int.Parse(stud)));
        //        Console.WriteLine("Studente rimosso correttamente.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Errore durante l'inserimento: {ex.Message}");
        //        logManager.LogFile(ex);
        //    }
        //}

        //public void RemoveExam()
        //{
        //    Console.Clear();
        //    try
        //    {
        //        Console.WriteLine("Inserisci l'ID dell'esame da eliminare: ");
        //        string exam = Console.ReadLine();
        //        listaEsami.Remove(listaEsami.Find(e => e.Id == int.Parse(exam)));
        //        Console.WriteLine("Esame rimosso correttamente.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Errore durante l'inserimento: {ex.Message}");
        //        logManager.LogFile(ex);
        //    }
        //}
        //public void RemoveTeacher()
        //{
        //    Console.Clear();
        //    try
        //    {
        //        Console.WriteLine("Inserisci il cognome dell'insegnante da eliminare");
        //        string prof = Console.ReadLine();
        //        listaProfessori.Remove(listaProfessori.Find(p => p.Cognome.Equals(prof)));
        //        Console.WriteLine("Professore rimosso correttamente.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Errore durante l'inserimento: {ex.Message}");
        //        logManager.LogFile(ex);
        //    }
        //}

        //public void RemoveSecretary()
        //{
        //    Console.Clear();
        //    try
        //    {
        //        Console.WriteLine("Inserisci il cognome della segretaria da eliminare");
        //        string segr = Console.ReadLine();
        //        listaSegretarie.Remove(listaSegretarie.Find(s => s.Cognome.Equals(segr)));
        //        Console.WriteLine("Segretaria rimossa correttamente.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Errore durante l'inserimento: {ex.Message}");
        //        logManager.LogFile(ex);
        //    }
        //}
        //public void SaveToJson()
        //{

        //    try
        //    {

        //        //var options = new JsonSerializerOptions
        //        //{
        //        //    WriteIndented = true,
        //        //    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
        //        //};

        //        JsonSerializerSettings settings = new JsonSerializerSettings
        //        {
        //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore, // Ignora i loop circolari
        //            Formatting = Formatting.Indented // Per formattare il JSON in modo leggibile
        //        };

        //        var json = JsonConvert.SerializeObject(listaEsami, settings);

        //        File.WriteAllText(Config.EsamiPath, json);

        //        var json2 = JsonConvert.SerializeObject(listaLezioni, settings);
        //        File.WriteAllText(Config.LezioniPath, json2);

        //        var json3 = JsonConvert.SerializeObject(listaProfessori, settings);
        //        File.WriteAllText(Config.ProfessoriPath, json3);

        //        var json4 = JsonConvert.SerializeObject(listaSegretarie, settings);
        //        File.WriteAllText(Config.SegretariePath, json4);

        //        var json5 = JsonConvert.SerializeObject(listaStudenti, settings);
        //        File.WriteAllText(Config.StudentiPath, json5);

        //        var json6 = JsonConvert.SerializeObject(listaFacolta, settings);
        //        File.WriteAllText(Config.FacoltaPath, json6);

        //        var json7 = JsonConvert.SerializeObject(listaUniversita, settings);
        //        File.WriteAllText(Config.UniversitaPath, json7);

        //        Console.WriteLine("Dati salvati su file json!");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Errore nel salvataggio: {ex.Message}");
        //        logManager.LogFile(ex);
        //    }


        //}


    }
}
