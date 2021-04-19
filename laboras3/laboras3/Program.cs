using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace laboras3
{

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> studentList = new List<Student>();

            do
            {
                Console.WriteLine(
                    "\n[Input option from this list]\n" +
                    "0. Terminate program\n" +
                    "1. Add student by hand\n" +
                    "2. Read students from file\n" +
                    "3. Add student's grades by hand\n" +
                    "4. Add student's automatically generated grades\n" +
                    "5. Print student list with AVG points\n" +
                    "6. Print student list with MED points\n" +
                    "7. Print student list with AVG and MED points");
                try { 
                    switch (Int32.Parse(Console.ReadLine()))
                    {
                        case 0:
                            Environment.Exit(0);
                            break;

                        case 1:
                            Student.addStudent(studentList);
                            break;

                        case 2:
                            Student.addStudentFromFile(studentList);
                            break;

                        case 3:
                            Console.WriteLine("[Input (1 or 2) option]\n" +
                                "0. Go back\n" +
                                "1. Student's number of HW is known\n" +
                                "2. Student's number of HW is UNknown");
                            switch (Int32.Parse(Console.ReadLine()))
                            {
                                case 0:
                                    break;
                                case 1:
                                    Student.addStudentGradesLimited(studentList);
                                    break;
                                case 2:
                                    Student.addStudentGradesUnlimited(studentList);
                                    break;
                            }
                            break;

                        case 4:
                            Student.generateRandomPoints(studentList);
                            break;

                        case 5:
                            Student.printStudentListAvg(studentList);
                            break;

                        case 6:
                            Student.printStudentListMed(studentList);
                            break;

                        case 7:
                            Student.printStudentListAvgMed(studentList);
                            break;
                    }
                }catch (System.FormatException)
                {
                    Console.WriteLine("error (selection): please input only numbers");
                }
            } while (true);
        }
    }
}
