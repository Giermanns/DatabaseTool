using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace DBSInsert
{
    class Program
    {
        String server = "FELIX-PC";
        String database = "worters";
        String databaseTable = "wort";
        String databaseColum = "wort";

        static void Main(string[] args)
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("DBS Tool");
                Console.WriteLine("-----------------------");
                Console.WriteLine("1. Daten einlesen");
                Console.WriteLine("2. Daten ausgeben");
                Console.WriteLine("3. Daten löschen");
                Console.WriteLine("4. Exit");
                ConsoleKeyInfo key = Console.ReadKey();
                String keyEnter;
                keyEnter = key.Key.ToString();
                switch (keyEnter)
                {
                    case "D1":
                        Console.Clear();
                        Program Insert1 = new Program();
                        Insert1.Insert();
                        break;
                    case "D2":
                        Console.Clear();
                        Program Select1 = new Program();
                        Select1.Select();
                        break;
                    case "D3":
                        Console.Clear();
                        Program Delete1 = new Program();
                        Delete1.Delete();
                        break;
                    case "D4":
                        Console.Clear();
                        Console.WriteLine("Exit");
                        Console.ReadLine();
                        run = false;
                        break;
                    case "NumPad1":
                        Console.Clear();
                        Program Insert2 = new Program();
                        Insert2.Insert();
                        break;
                    case "NumPad2":
                        Console.Clear();
                        Program Select2 = new Program();
                        Select2.Select();
                        break;
                    case "NumPad3":
                        Console.Clear();
                        Program Delete2 = new Program();
                        Delete2.Delete();
                        break;
                    case "NumPad4":
                        Console.Clear();
                        Console.WriteLine("Exit");
                        Console.ReadLine();
                        run = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Falsche Eingabe!");
                        Console.WriteLine("Geben Sie Bitte nur 1, 2 oder 3 ein!");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public void Insert()
        {
            string eingabe;
            Console.Clear();
            MySqlConnection connection;
            connection = new MySqlConnection($"server={server}; user id=root;persistsecurityinfo=True;database={database};password=root");
            connection.Open();
            Console.WriteLine("Geben Sie ein Wort an das Sie Hinzufügen möchten: ");
            eingabe = Console.ReadLine();
            string[] words = eingabe.Split(',');
            foreach(String wort in words)
            {
                String query = $"Insert into {databaseTable} ({databaseColum}) values ('{wort}')";
                try
                {
                    MySqlCommand myCommand = new MySqlCommand(query, connection);
                    MySqlDataReader myReader;
                    myReader = myCommand.ExecuteReader();
                    myReader.Close();
                }
                catch (Exception)
                {
                    Console.WriteLine("Fehler beim der Eingabe");
                }
            }
            Console.WriteLine($"Das Wort {eingabe} wurde hinzugefügt!");
            Console.ReadKey();
        }

        public void Select()
        {
            String statement;
            Console.Clear();
            MySqlConnection connection;
            connection = new MySqlConnection($"server={server}; user id=root;persistsecurityinfo=True;database={database}; password=root");
            connection.Open();
            Console.WriteLine("Daten der Datenbank: ");
            String query = $"Select * from {databaseTable}";
            Console.WriteLine(query);
            Console.WriteLine("-----------------------");
            MySqlCommand mySqlCommand = new MySqlCommand(query, connection);
            MySqlDataReader myReader;
            myReader = mySqlCommand.ExecuteReader();
            myReader.Read();
            statement = myReader[0].ToString();
            statement += " | " + myReader[1].ToString();
            Console.WriteLine(statement);
            Console.WriteLine("-----------------------");
            while (myReader.Read())
            {
                statement = myReader[0].ToString();
                statement += " | "+myReader[1].ToString();
                Console.WriteLine(statement);
                Console.WriteLine("-----------------------");
            }
            myReader.Close();
            Console.ReadKey();
        }

        public void Delete()
        {
            Console.Clear();
            int id;
            MySqlConnection connection;
            connection = new MySqlConnection($"server={server}; user id=root;persistsecurityinfo=True;database={database}; password=root");
            connection.Open();
            Console.WriteLine("Löschen von Daten");
            Console.WriteLine("-----------------------");
            Console.WriteLine("Geben Sie die ID des Wortes an das Sie Löschen möchten: ");
            if (int.TryParse(Console.ReadLine(), out id))
            {
                String query = $"Delete {databaseTable} from wort where id = '{id}'";
                MySqlCommand mySqlCommand = new MySqlCommand(query, connection);
                MySqlDataReader myReader;
                myReader = mySqlCommand.ExecuteReader();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Fehler beider Eingabe!");
            }
        }
    }
}
