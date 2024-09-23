using System.Text.RegularExpressions;

namespace CourseEnrollmentSystem
{
    internal class Program
    {
        static Dictionary<string, HashSet<string>> Courses = new Dictionary<string, HashSet<string>>();
        static Dictionary<string, int> courseCapacities = new Dictionary<string, int>();

        static List<(int AID, string Aname, string email, string password)> Admin = new List<(int AID, string Aname, string email, string password)>();




        static string filePathAdmin = "C:\\Users\\Lenovo\\source\\repos\\testCourse\\AdminsFile.txt";
        static string filePathcourses = "C:\\Users\\Lenovo\\source\\repos\\testCourse\\courses.txt";

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
        static string GetValidPassword()
        {
            string password;
            while (true)
            {
                Console.WriteLine("Enter your password (at least 8 characters, include uppercase, lowercase, and a digit):");
                password = Console.ReadLine();
                if (password.Length >= 8 &&
                    Regex.IsMatch(password, @"[A-Z]") &&
                    Regex.IsMatch(password, @"[a-z]") &&
                    Regex.IsMatch(password, @"[0-9]"))
                {
                    break;
                }
                Console.WriteLine("Password does not meet criteria. Try again.");
            }
            return password;
        }
        static string GetValidEmail()
        {
            string email;
            while (true)
            {
                Console.WriteLine("Enter your email (must contain '@' and end with .com or .edu):");
                email = Console.ReadLine();

                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.(com|edu)$"))
                {
                    Console.WriteLine("Invalid email format. Try again.");
                    continue;
                }

                bool emailExists = Admin.Any(a => a.email == email);

                if (!emailExists)
                {
                    break;
                }

                Console.WriteLine("Duplicate email. Try again.");
            }
            return email;
        }
        static void LoginAdmin()
        {

            Console.WriteLine("Enter your email:");
            string email = Console.ReadLine();

            // Check if the email exists 
            bool adminFound = false;
            int foundAdmin = -1;

            for (int i = 0; i < Admin.Count; i++)
            {
                if (Admin[i].email.Equals(email, StringComparison.OrdinalIgnoreCase))
                {
                    adminFound = true;
                    foundAdmin = i;
                    Console.WriteLine("\nEnter Admin's Password:");
                    string password = Console.ReadLine();
                    if (Admin[i].password == password)
                    {
                        AdminMenu(Admin[i].AID);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect Admin's Password.");
                        Console.WriteLine("\nPress Enter key to continue...");
                        Console.ReadLine();
                        return;
                    }
                }
            }

            if (!adminFound)
            {
                Console.WriteLine("Admin is not registered. Do you want to register? (yes / no)");
                if (Console.ReadLine().ToLower() == "yes")
                {
                    RegisterAdmin();
                }
                else
                {
                    AdminFunction();
                }
            }
        }
        static void AdminMenu(int adminID)
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("===================================");
                Console.WriteLine("          Library Admin Menu       ");
                Console.WriteLine("===================================");
                Console.WriteLine("1. Add a new course");
                Console.WriteLine("2. Add a new Students");
                Console.WriteLine("3. Remove a student from a course:");
                Console.WriteLine("4. Remove Course ");
                Console.WriteLine("5. Display all students in a course:");
                Console.WriteLine("6. Display all courses and their students:");
                Console.WriteLine("7. Log Out");
                Console.WriteLine("===================================");
                Console.Write("Choose an option (1-7): ");
                var option = Console.ReadLine();


                Console.WriteLine("\n-----------------------------------\n");

                switch (option)
                {
                    case "1":
                        Console.WriteLine("Add a new course");
                        Console.WriteLine("-----------------------------------");
                        AddNewcourse();
                        break;
                    case "2":
                        Console.WriteLine("Add a new Students");
                        Console.WriteLine("-----------------------------------");
                        // AddNewStudents(); 
                        break;
                    case "3":
                        Console.WriteLine(" Remove a student from a course");
                        Console.WriteLine("-----------------------------------");
                        //RemoveStudentFromCourse();
                        break;
                    case "4":
                        Console.WriteLine("Remove Course ");
                        Console.WriteLine("-----------------------------------");
                        // RemoveCourse();
                        break;
                    case "5":
                        Console.WriteLine(" Display all students in a course:");
                        Console.WriteLine("-----------------------------------");
                        // DisplayAllStudentsCourse();
                        break;
                    case "6":
                        Console.WriteLine("Display all courses and their students:");
                        Console.WriteLine("-----------------------------------");
                        // DisplayAllCoursesAndTheirStudents();
                        break;
                    case "7":
                        Console.WriteLine("Saving changes and logging out...");
                        // SaveToFile();
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose a valid option (1-7).");
                        break;
                }

                if (running)
                {

                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }
            }
        }
        static void LoadAdminFromFile()
        {
            try
            {
                if (File.Exists(filePathAdmin))
                {
                    using (StreamReader reader = new StreamReader(filePathAdmin))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 4)
                            {
                                Admin.Add((int.Parse(parts[0].Trim()), parts[1].Trim(), parts[2].Trim(), parts[3].Trim()));
                            }
                        }
                    }
                    Console.WriteLine("Admins loaded from file successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }
        static void SaveAdminToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePathAdmin))
                {
                    foreach (var admins in Admin)
                    {
                        writer.WriteLine($"{admins.AID}|{admins.Aname}|{admins.email}|{admins.password}");
                    }
                }
                Console.WriteLine("Admin saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }


        //************** Function ******************************************

        static void AddNewcourse()

        {

            Console.WriteLine("Enter course code :");
            string courseCode = Console.ReadLine();


            Console.WriteLine("Enter course capacity :");
            int CourseCapacity = int.Parse(Console.ReadLine());



            if (Courses.ContainsKey(courseCode))
            {
                Console.WriteLine($"Course {courseCode} already exists");
            }
            else
            {
                Courses[courseCode] = new HashSet<string>();
                courseCapacities[courseCode] = CourseCapacity;
                Console.WriteLine($"Course {courseCode} added with a capacity of {CourseCapacity}.");

            }
            SaveCoursesToFile();

        }

        static void SaveCoursesToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePathcourses))
            {
                foreach (var course in Courses)
                {
                    string courseCode = course.Key;
                    int capacity = courseCapacities[courseCode];
                    writer.WriteLine($"{courseCode},{capacity}");
                }
            }
            Console.WriteLine("Courses saved to file.");
        }

        static void LoadCoursesFromFile()
        {
            if (File.Exists(filePathcourses))
            {
                using (StreamReader reader = new StreamReader(filePathcourses))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');
                        if (data.Length == 2)
                        {
                            string courseCode = data[0];
                            int capacity = int.Parse(data[1]);

                            Courses[courseCode] = new HashSet<string>();
                            courseCapacities[courseCode] = capacity;
                        }
                    }
                }
                Console.WriteLine("Courses loaded from file.");
            }
            else
            {
                Console.WriteLine("No course data file found.");
            }
        }


        static void EnrollStudent()
        {

            Console.WriteLine("Enter the student's Name:");
            string studentName = Console.ReadLine();


            Console.WriteLine("Enter the course code:");
            string courseCode = Console.ReadLine();

            //  course exists
            if (Courses.ContainsKey(courseCode))
            {
                var enrollStudents = Courses[courseCode];

                // student is already enrolled
                if (enrollStudents.Contains(studentName))
                {

                    Console.WriteLine($"{studentName} is already enrolled in {courseCode}.");

                }
                // course is full
                else if (enrollStudents.Count >= courseCapacities[courseCode])
                {

                    waitList.Add((studentName, courseCode));
                    Console.WriteLine($"{studentName} has been added to the waitlist for {courseCode}.");
                }
                // Enroll the student in the course
                else
                {
                    enrollStudents.Add(studentName);

                    Console.WriteLine($"{studentName} has been enrolled in {courseCode}.");
                }
            }
            else
            {
                Console.WriteLine($"Course {courseCode} does not exist.");
            }
        }

    }
}
