using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        public static Token token = new Token();
        public static Notes notes = new Notes();
        public static Note note = new Note();
        public static Message message = new Message();
        static void  loginScreen()
        {
            string username;
            string password;
            string input;
            
            while (token.access_token == null)
            {
                Console.WriteLine("\tWybierz opcję: \n" +
                    " 0.Zamknij\n 1.Rejestracja\n 2.Logowanie \n ");

                input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        Environment.Exit(0);
                        break;                        

                    case "1":
                        Console.WriteLine("Podaj login:");
                        username = Console.ReadLine();
                        Console.WriteLine("Podaj hasło:");
                        password = Console.ReadLine();
                        Task.WaitAll(Connection.register(username, password));
                        break;

                    case "2":
                        Console.WriteLine("Podaj login:");
                        username = Console.ReadLine();
                        Console.WriteLine("Podaj hasło:");
                        password = Console.ReadLine();
                        Task.WaitAll(Connection.login(username, password));
                        if(token.access_token == null)
                        {
                            Console.WriteLine("\n\tBłędny login lub hasło!");
                        }
                        break;                    

                    default:
                        break;
                }
            }
        }

        static  void Main(string[] args)
        {
            loginScreen();
            Console.WriteLine("\n\tPomyślnie zalogowano\n");

            while (true)
            {
                Console.WriteLine("\n\tWybierz opcję: \n" +
                        "0.Zamknij\n" +
                        "1.Wyświetl wszystkie notatki\n" +
                        "2.Wyświetl notatki po kategorii \n " +
                        "3.Wyświetl notatki po etykiecie \n " +
                        "4.Wyświetl notatke po id \n " +
                        "5.Usuń notatke po id \n " +
                        "6.Edytuj notatke po id \n " +
                        "7.Utwórz nową notatkę \n " );
                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        Environment.Exit(0);
                        break;

                    case "1":
                        Task.WaitAll(Connection.GetNotes());
                        Console.WriteLine("ID\t Title\t\t Tag\t\t Category");
                        if (notes.notes != null)
                        {
                            foreach (var item in notes.notes)
                            {
                                Console.WriteLine(item.id + "\t " + (item.title.Length < 9 ? item.title + "\t" : item.title) + "\t " + item.tag_id + "\t\t " + item.category_id);

                            }
                        }
                        else
                        {
                            Console.WriteLine("Nie ma notatek!");
                        }
                        break;

                    case "2":
                        Console.WriteLine("Podaj kategorie: ");
                        string category = Console.ReadLine();
                        Task.WaitAll(Connection.GetNotesByCategory(category));
                        if (notes.notes != null)
                        {
                            Console.WriteLine("ID\t Title\t\t Tag\t\t Category");
                            foreach (var item in notes.notes)
                            {
                                Console.WriteLine(item.id + "\t " + (item.title.Length < 9 ? item.title + "\t" : item.title) + "\t " + item.tag_id + "\t\t " + item.category_id);

                            }
                        }
                        else
                        {
                            Console.WriteLine("\tNie ma notatek!");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Podaj etykiete: ");
                        string tag = Console.ReadLine();
                        Task.WaitAll(Connection.GetNotesByTag(tag));
                        if (notes.notes != null)
                        {
                            Console.WriteLine("ID\t Title\t\t Tag\t\t Category");
                            foreach (var item in notes.notes)
                            {
                                Console.WriteLine(item.id + "\t " + (item.title.Length < 9 ? item.title + "\t" : item.title) + "\t " + item.tag_id + "\t\t " + item.category_id);

                            }
                        }
                        else
                        {
                            Console.WriteLine("\tNie ma notatek!");
                        }
                        break;

                    case "4":
                        Console.WriteLine("Podaj id notatki: ");
                        string id = Console.ReadLine();
                        Task.WaitAll(Connection.GetNoteById(id));
                        if (note != null)
                        {
                            Console.WriteLine("Title: \n\t" + note.title + "\n" +                                              
                                              "Category: \n\t" + note.category_id + "\n" +
                                              "Tag: \n\t" + note.tag_id + "\n" +
                                              "Body: \n\t" + note.body + "\n" );                            
                        }
                        else
                        {
                            Console.WriteLine("\tNie ma takiej notatki!");
                        }
                        break;

                    case "5":
                        Console.WriteLine("Podaj id notatki: ");
                        string id_delete = Console.ReadLine();
                        Task.WaitAll(Connection.DeleteNoteById(id_delete));                        
                        Console.WriteLine("\n\t" + message.ToString());
                        
                        break;

                    case "6":
                        Note newNote = new Note();
                        Console.WriteLine("Podaj id notatki: ");
                        string id_edit = Console.ReadLine();
                       
                            Console.WriteLine("Podaj tytuł notatki: ");
                            newNote.title = Console.ReadLine();
                            Console.WriteLine("Podaj treść notatki: ");
                            newNote.body = Console.ReadLine();
                            Console.WriteLine("Podaj kategorie notatki: ");
                            newNote.category_id = Console.ReadLine();
                            Console.WriteLine("Podaj etykiete notatki: ");
                            newNote.tag_id = Console.ReadLine();

                        Task.WaitAll(Connection.EditNoteById(id_edit, newNote));
                        Console.WriteLine("\n\t" + message.ToString());
                       
                        break;

                    case "7":
                        Note addNote = new Note();

                            Console.WriteLine("Podaj tytuł notatki: ");
                            addNote.title = Console.ReadLine();
                            Console.WriteLine("Podaj treść notatki: ");
                            addNote.body = Console.ReadLine();
                            Console.WriteLine("Podaj kategorie notatki: ");
                            addNote.category_id = Console.ReadLine();
                            Console.WriteLine("Podaj etykiete notatki: ");
                            addNote.tag_id = Console.ReadLine();

                        Task.WaitAll(Connection.AddNote(addNote));
                        Console.WriteLine("\n\t" + message.ToString());
                        
                        break;

                    default:
                        break;
                }

            }
        }
    }
}
