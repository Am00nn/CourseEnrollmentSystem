using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace CourseEnrollmentSystem
{
    internal class Program
    {
        static Dictionary<string, HashSet<string>> Courses = new Dictionary<string, HashSet<string>>();
        static Dictionary<string, int> courseCapacities = new Dictionary<string, int>();
        static List<(string studentName, string courseCode)> waitList = new List<(string, string)>();


        static void Main(string[] args)
        {
            InitializeStartupData();
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("===================================");
                Console.WriteLine("    Course Enrollment System       ");
                Console.WriteLine("===================================");
                Console.WriteLine("1. Add a new course");
                Console.WriteLine("2. Enroll a student in a course:");
                Console.WriteLine("3. Remove a student from a course:");
                Console.WriteLine("4. Remove Course ");
                Console.WriteLine("5. Display all students in a course:");
                Console.WriteLine("6. Display all courses and their students:");
                Console.WriteLine("7. Find courses with common students");
                Console.WriteLine("8. Withdraw a Student from All Courses:");
                Console.WriteLine("9. Viwe waitList:");
                Console.WriteLine("10. Log Out");
                Console.WriteLine("===================================");
                Console.Write("Choose an option (1-8): ");
                var option = Console.ReadLine();


                Console.WriteLine("\n-----------------------------------\n");

                switch (option)
                {
                    case "1":
                        Console.WriteLine("Add a new course");
                        Console.WriteLine("-----------------------------------");
                        InitializeStartupData();
                        DisplayAllCoursesAndTheirStudents();
                        AddNewcourse();
                        break;
                    case "2":
                        Console.WriteLine("Enroll a student in a course");
                        Console.WriteLine("-----------------------------------");
                        DisplayAllCoursesAndTheirStudents();
                        EnrollStudent();
                        break;
                    case "3":
                        Console.WriteLine(" Remove a student from a course");
                        Console.WriteLine("-----------------------------------");
                        DisplayAllCoursesAndTheirStudents();
                        RemoveStudentFromCourse();

                        break;
                    case "4":
                        Console.WriteLine("Remove Course ");
                        Console.WriteLine("-----------------------------------");
                        DisplayAllCoursesAndTheirStudents();
                        RemoveCourse();
                        break;
                    case "5":
                        Console.WriteLine(" Display all students in a course:");
                        Console.WriteLine("-----------------------------------");
                        DisplayAllStudentsCourse();
                        break;
                    case "6":
                        Console.WriteLine("Display all courses and their students:");
                        Console.WriteLine("-----------------------------------");
                        DisplayAllCoursesAndTheirStudents();
                        break;

                    case "7":
                        Console.WriteLine("Find courses with common students");
                        Console.WriteLine("-----------------------------------");
                        DisplayAllCoursesAndTheirStudents();
                        FindCoursesWithCommonStudents();
                        break;
                    case "8":
                        Console.WriteLine("Withdraw a Student from All Courses");
                        Console.WriteLine("-----------------------------------");
                        WithdrawStudent();
                        break;



                    case "9":
                        Console.WriteLine("viwe WaitList ");
                        Console.WriteLine("-----------------------------------");
                        ViweWaitList();
                        break;
                    case "10":
                        Console.WriteLine("logging out...");

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

        static void InitializeStartupData()
        {
            // Example data: Courses and their enrolled students (cross-over students)
            Courses["CS101"] = new HashSet<string> { "Alice", "Bob", "Charlie" };   // CS101 has Alice, Bob, Charlie
            Courses["MATH202"] = new HashSet<string> { "David", "Eva", "Bob" };     // MATH202 has David, Eva, and Bob (cross-over with CS101)
            Courses["ENG303"] = new HashSet<string> { "Frank", "Grace", "Charlie" };// ENG303 has Frank, Grace, and Charlie (cross-over with CS101)
            Courses["BIO404"] = new HashSet<string> { "Ivy", "Jack", "David" };     // BIO404 has Ivy, Jack, and David (cross-over with MATH202)
                                                                                    // Set course capacities (varying)
            courseCapacities["CS101"] = 3;  // CS101 capacity of 3 (currently full)
            courseCapacities["MATH202"] = 5; // MATH202 capacity of 5 (can accept more students)
            courseCapacities["ENG303"] = 3;  // ENG303 capacity of 3 (currently full)
            courseCapacities["BIO404"] = 4;  // BIO404 capacity of 4 (can accept more students)
                                             // Waitlist for courses (students waiting to enroll in full courses)
            waitList.Add(("Helen", "CS101"));   // Helen waiting for CS101
            waitList.Add(("Jack", "ENG303"));   // Jack waiting for ENG303
            waitList.Add(("Alice", "BIO404"));  // Alice waiting for BIO404
            waitList.Add(("Eva", "ENG303"));    // Eva waiting for ENG303
            waitList.Add(("karim", "MATH202"));    // Eva waiting for ENG303
            Console.WriteLine("Startup data initialized.");
        }
        static void AddNewcourse()

        {
            if (Courses.Count == 0)
            {
                InitializeStartupData();
            }
            Console.WriteLine("Enter course code :");
            string courseCode = Console.ReadLine().ToUpper();


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


        }

        static void EnrollStudent()
        {

            Console.WriteLine("Enter the student's Name:");
            string studentName = Console.ReadLine().ToUpper();


            Console.WriteLine("Enter the course code:");
            string courseCode = Console.ReadLine().ToUpper();


            if (Courses.ContainsKey(courseCode))
            {
                var StudentInCourse = Courses[courseCode];


                if (StudentInCourse.Contains(studentName))
                {

                    Console.WriteLine($"{studentName} is already enrolled in {courseCode}.");

                }

                else if (StudentInCourse.Count >= courseCapacities[courseCode])
                {

                    waitList.Add((studentName, courseCode));
                    Console.WriteLine($"{studentName} has been added to the waitlist for {courseCode}.");
                }

                else
                {
                    StudentInCourse.Add(studentName);

                    Console.WriteLine($"{studentName} has been enrolled in {courseCode}.");
                }
            }
            else
            {
                Console.WriteLine($"Course {courseCode} does not exist.");
            }
        }

        static void RemoveStudentFromCourse()
        {
            Console.WriteLine("Enter the student's Name:");
            string studentName = Console.ReadLine();

            Console.WriteLine("Enter the course code:");
            string courseCode = Console.ReadLine().ToUpper();


            if (Courses.ContainsKey(courseCode))
            {
                var Students = Courses[courseCode];


                if (Students.Contains(studentName))
                {
                    Students.Remove(studentName);
                    Console.WriteLine($"{studentName} has been removed from {courseCode}.");


                    for (int i = 0; i < waitList.Count; i++)
                    {
                        if (waitList[i].courseCode == courseCode)
                        {

                            string waitStudentName = waitList[i].studentName;
                            Students.Add(waitStudentName);


                            waitList.RemoveAt(i);

                            Console.WriteLine($"{waitStudentName} has been enrolled from the waitlist into {courseCode}.");
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"{studentName} is not enrolled in {courseCode}.");
                }
            }
            else
            {
                Console.WriteLine($"Course {courseCode} does not exist.");
            }
        }

        static void DisplayAllStudentsCourse()
        {
            Console.WriteLine("Enter the course code:");
            string courseCode = Console.ReadLine().ToUpper();


            if (Courses.ContainsKey(courseCode))
            {
                var Students = Courses[courseCode];


                if (Students.Count > 0)
                {
                    Console.WriteLine($"Students enrolled in {courseCode}:");
                    foreach (var student in Students)
                    {
                        Console.WriteLine(student);
                    }
                }
                else
                {
                    Console.WriteLine($"No students are enrolled in {courseCode}.");
                }
            }
            else
            {
                Console.WriteLine($"Course {courseCode} does not exist.");
            }
        }

        static void RemoveCourse()
        {
            Console.WriteLine("Enter the course code to you want remove:");
            string courseCode = Console.ReadLine().ToUpper();

            if (Courses.ContainsKey(courseCode))
            {

                if (Courses[courseCode].Count == 0)
                {

                    Courses.Remove(courseCode);
                    courseCapacities.Remove(courseCode);
                    Console.WriteLine($"Course {courseCode} has been removed.");
                }
                else
                {
                    Console.WriteLine($"Course {courseCode} cannot be removed because students are enrolled.");
                }
            }
            else
            {
                Console.WriteLine($"Course {courseCode} does not exist.");
            }
        }

        static void DisplayAllCoursesAndTheirStudents()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("      Courses and their Enrolled Students ");
            Console.WriteLine("========================================\n");

            foreach (var course in Courses)
            {
                string courseCode = course.Key;
                var studentsInCourse = course.Value;

                Console.WriteLine($"Course Code: {courseCode}");
                Console.WriteLine("----------------------------------------");

                if (studentsInCourse.Count > 0)
                {
                    Console.WriteLine(" Enrolled Students:");
                    foreach (var student in studentsInCourse)
                    {
                        Console.WriteLine($"* {student}");
                    }
                }
                else
                {
                    Console.WriteLine(" No Students Enrolled.");
                }


                Console.WriteLine();
            }

            Console.WriteLine("========================================");
        }
        static void ViweWaitList()

        {
         
            foreach (var n in waitList) 
            {
                Console.WriteLine(n);

            }



        }

        static void WithdrawStudent()
        {
            Console.WriteLine("Enter the name of the student to withdraw::");
            string studentName = Console.ReadLine();


            bool flage = false;

            List<string> coursesToWithdraw = new List<string>();
            foreach (var course in Courses)
            {
                if (course.Value.Contains(studentName)) // Check if the student is enrolled in the course
                {
                    coursesToWithdraw.Add(course.Key); // Add course to the list
                    flage = true;
                }
            }
            foreach (var course in coursesToWithdraw)
            {
                Console.WriteLine(course);
            }
            foreach (var courseCode in coursesToWithdraw)
            {
                Courses[courseCode].Remove(studentName);
                Console.WriteLine($"Student '{studentName}' has been withdrawn from course '{courseCode}'.");
                // Check if the course now has space for students on the waitlist
                var studentOnWaitlist = waitList.FirstOrDefault(w => w.courseCode == courseCode);
                if (studentOnWaitlist != default)
                {
                    // Enroll the first student from the waitlist
                    Courses[courseCode].Add(studentOnWaitlist.studentName);
                    waitList.Remove(studentOnWaitlist);
                    Console.WriteLine($"Student '{studentOnWaitlist.studentName}' from the waitlist has been enrolled in course '{courseCode}'.");
                }
            }


            if (!flage)
            {
                Console.WriteLine($"{studentName} is not enrolled in any courses.");
            }
        }

        static void FindCoursesWithCommonStudents()
        {
            Console.WriteLine("enter frist course : ");
            string courseCode1 = Console.ReadLine();
            Console.WriteLine("Enter second Course :");
            string courseCode2 = Console.ReadLine();

            if (Courses.ContainsKey(courseCode1) && Courses.ContainsKey(courseCode2))
            {
                List<string> CommenStudents = new List<string>();


                foreach (var student in Courses[courseCode1])
                {

                    if (Courses[courseCode2].Contains(student))
                    {
                        CommenStudents.Add(student);
                    }
                }

                if (CommenStudents.Count > 0)
                {
                    Console.WriteLine($"Common students in {courseCode1} and {courseCode2}");
                    foreach (var e in CommenStudents)
                    {
                        Console.WriteLine(e);
                    }
                }
                else
                {
                    Console.WriteLine($"There are no common students enrolled in {courseCode1} and {courseCode2}.");
                }
            }
            else
            {
                Console.WriteLine(" course codes do not exist.");
            }

        }



    }
    
}
