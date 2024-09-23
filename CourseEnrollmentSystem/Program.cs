using System.Text.RegularExpressions;

namespace CourseEnrollmentSystem
{
    internal class Program
    {

        static List<(int AID, string Aname, string email, string password)> Admin = new List<(int AID, string Aname, string email, string password)>();

        static string filePathAdmin = "C:\\Users\\Lenovo\\source\\repos\\testCourse\\AdminsFile.txt";
        static void Main(string[] args)
        {
            bool ExitFlag = false;


            try
            {
                //LoadBooksFromFile();
               // LoadCategoriesFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data from file: " + ex.Message);
                return;
            }

            do
            {

                Console.Clear();
                Console.WriteLine("======================================================= ");
                Console.WriteLine("      Welcome to the Course Enrollment System    ");
               Console.WriteLine("========================================================\n");

                Console.WriteLine("Please choose an option from the menu below:");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine(" A - Admin");
                Console.WriteLine(" B - Student");
                Console.WriteLine(" C - Save and Exit");
                Console.WriteLine("----------------------------------------");
                Console.Write("\nYour choice: ");

                string choice = Console.ReadLine().ToUpper();

                try
                {
                    switch (choice)
                    {
                        case "A":
                            Console.Clear();
                            Console.WriteLine("Admin Menu");
                            AdminFunction();
                            break;

                        case "B":
                            Console.Clear();
                            Console.WriteLine("Students Menu");
                            //UserFunction();
                            break;

                        case "C":
                            Console.WriteLine("\nSaving data and exiting the system...");
                           // SaveBooksToFile();
                            ExitFlag = true;
                            break;

                        default:
                            Console.WriteLine("\nInvalid choice. Please select a valid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while processing your choice: " + ex.Message);
                }

                if (!ExitFlag)
                {
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }

            } while (!ExitFlag);


            Console.Clear();
            Console.WriteLine("Thank you for using the Library System. Goodbye!");
        }



        //***************** Admin function *********************************
        static void AdminFunction()
        {
            bool ExitFlag = false;


            while (!ExitFlag)
            {
                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine("          Welcome, Admin!            ");
                Console.WriteLine("=====================================\n");

                Console.WriteLine("Please select an operation from the menu below:");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine(" A - Admin Registration");
                Console.WriteLine(" B - Admin Login");
                Console.WriteLine(" C - Exit to Main Menu");
                Console.WriteLine("-------------------------------------");
                Console.Write("\nYour choice: ");

                string choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "A":
                        Console.Clear();
                        Console.WriteLine("Admin Registration\n");
                        RegisterAdmin();
                        break;

                    case "B":
                        Console.Clear();
                        Console.WriteLine("Admin Login\n");
                        LoadAdminFromFile();
                        LoginAdmin();
                        break;

                    case "C":
                        ExitFlag = true;
                        Console.WriteLine("\nExiting Admin Menu...");
                        break;

                    default:
                        Console.WriteLine("\nInvalid option. Please choose a valid action.");
                        break;
                }


                if (!ExitFlag)
                {
                    Console.WriteLine("\nPress any key to return to the Admin Menu...");
                    Console.ReadKey();
                }
            }

            Console.Clear();
            Console.WriteLine("Returning to the main menu...");
        }
        static void RegisterAdmin()
        {
            int AID = 1;


            if (Admin.Count > 0)
            {

                AID = Admin[0].AID;


                for (int i = 1; i < Admin.Count; i++)
                {
                    if (Admin[i].AID > AID)
                    {
                        AID = Admin[i].AID;
                    }
                }

                AID++;
            }

            Console.WriteLine("Enter admin name:");
            string adminName = Console.ReadLine();


            bool nameExists = false;
            for (int i = 0; i < Admin.Count; i++)
            {
                if (Admin[i].Aname == adminName)
                {
                    nameExists = true;
                    break;
                }
            }

            if (nameExists)
            {
                Console.WriteLine("Admin name already exists.");
                return;
            }


            string email = GetValidEmail();
            string password = GetValidPassword();


            Admin.Add((AID, adminName, email, password));
            Console.WriteLine("Admin registered successfully!");
            SaveAdminToFile();
            AdminFunction();

        }


    }
}
