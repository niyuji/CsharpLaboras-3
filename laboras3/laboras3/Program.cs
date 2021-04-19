using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace laboras3
{
    class Student
    {
        private string name, surName;
        private double finalPtsAvg, finalPtsMed;
        private List<double> homeWork;
        private double examRes;

        public string Name { get => name; set => name = value; }
        public string SurName { get => surName; set => surName = value; }
        public double FinalPtsAvg { get => finalPtsAvg; set => finalPtsAvg = value; }
        public double FinalPtsMed { get => finalPtsMed; set => finalPtsMed = value; }
        public List<double> HomeWork { get => homeWork; set => homeWork = value; }
        public double ExamRes { get => examRes; set => examRes = value; }

        public Student(){}

        public Student(string name, string surName, double finalPtsAvg, double finalPtsMed, List<double> homeWork, double examRes)
        {
            this.name = name;
            this.surName = surName;
            this.finalPtsAvg = finalPtsAvg;
            this.finalPtsMed = finalPtsMed;
            this.homeWork = homeWork;
            this.examRes = examRes;
        }

        public Student(string name, string surName, List<double> homeWork)
        {
            this.name = name;
            this.surName = surName;
            this.homeWork = homeWork;
        }


        public static void addStudent (List<Student> studentList)
        {
            string name, surName;
            List<double> homeWork = new List<double>();

            Console.WriteLine("Enter Student's Name: ");
            name = Console.ReadLine();
            Console.WriteLine("Enter Student's Surname: ");
            surName = Console.ReadLine();

            Student student = new Student(name, surName, homeWork);
            studentList.Add(student);
        }

        public static void addStudentFromFile(List<Student> studentList)
        {
            string line;
            int howMany=0;
            System.IO.StreamReader file = new System.IO.StreamReader(@"d:\students.txt");
            file.ReadLine(); //ignoruojam pirma eilute
            while ((line = file.ReadLine()) != null)
            {
                List<double> homeWork = new List<double>();
                string[] value = line.Split(' ');
                homeWork.Add(double.Parse(value[2]));
                homeWork.Add(double.Parse(value[3]));
                homeWork.Add(double.Parse(value[4]));
                homeWork.Add(double.Parse(value[5]));
                homeWork.Add(double.Parse(value[6]));
                //public Student(string name, string surName, double finalPtsAvg, double finalPtsMed, List<double> homeWork, double examRes)
                studentList.Add(new Student(value[1], value[0], 0, 0, homeWork, double.Parse(value[7])));
                calculateAvg(studentList, studentList.Count - 1);
                calculateMed(studentList, studentList.Count - 1);
                howMany++;
            }
            file.Close();
            Console.WriteLine("Successfully added " + howMany + " records");
        }

        public static void addStudentGradesLimited(List<Student> studentList)
        {
            int studentId=-1, hwNum;
            foreach (var student in studentList)
            {
                studentId++;
                Console.WriteLine(studentId + " " + student.name + " " + student.surName);
            }
            Console.WriteLine("\nEnter Student's Id: ");
            studentId = Int32.Parse(Console.ReadLine());
            studentList[studentId].homeWork.Clear(); //isvalom buvusius pazymius jei buv
            Console.WriteLine("Enter how many homeworks: ");
            hwNum = Int32.Parse(Console.ReadLine());

            for(int i=1; i<=hwNum; i++)
            {
                Console.WriteLine("Enter " + i + " HW Grade: ");
                studentList[studentId].homeWork.Add(double.Parse(Console.ReadLine()));
            }

            Console.WriteLine("Enter Student's Exam result: ");
            studentList[studentId].examRes = double.Parse(Console.ReadLine());

            calculateAvg(studentList, studentId);
            calculateMed(studentList, studentId);
            printStudentListAvg(studentList);
        }

        public static void addStudentGradesUnlimited (List<Student> studentList)
        {
            int studentId=-1, hwNum;
            foreach (var student in studentList)
            {
                studentId++;
                Console.WriteLine(studentId + " " + student.name + " " + student.surName);
            }
            Console.WriteLine("\nEnter Student's Id: ");
            studentId = Int32.Parse(Console.ReadLine());
            studentList[studentId].homeWork.Clear(); //isvalom buvusius pazymius jei buv
            string number_string;
            while (true)
            {
                Console.WriteLine("Enter HW Grade: ");
                number_string = Console.ReadLine();
                if (string.IsNullOrEmpty(number_string)) break;
                studentList[studentId].homeWork.Add(double.Parse(number_string));

            }
            Console.WriteLine("Enter Student's Exam result: ");
            studentList[studentId].examRes = double.Parse(Console.ReadLine());

            calculateAvg(studentList, studentId);
            calculateMed(studentList, studentId);
            printStudentListAvg(studentList);
        }

        public static void generateRandomPoints(List<Student> studentList)
        {
            int randomNumber, studentId=-1;
            Random random = new Random();
            foreach (var student in studentList)
            {
                studentId++;
                Console.WriteLine(studentId + " " + student.name + " " + student.surName);
            }
            Console.WriteLine("Enter Student's Id to automatically fill in the grades: ");
            studentId = Int32.Parse(Console.ReadLine());
            studentList[studentId].homeWork.Clear(); //isvalom buvusius pazymius jei buvo
            randomNumber = random.Next(1, 10); //random kiekis homeworku
            for(int i=0; i < randomNumber; i++)
            {
                randomNumber = random.Next(1, 10); // random pazymys
                studentList[studentId].homeWork.Add(randomNumber);
                Console.WriteLine("Random HW result: " + randomNumber);
            }
            studentList[studentId].examRes = random.Next(1, 10);
            Console.WriteLine("Random Exam result: " + studentList[studentId].examRes);
            calculateAvg(studentList, studentId);
            calculateMed(studentList, studentId);
            printStudentListAvg(studentList);
        }

        public static void calculateAvg (List<Student> studentList, int studentId)
        {
            double hwAvg = 0, hwSum = 0;
            int homeWorks = studentList[studentId].homeWork.Count();

            for (int i=0; i<homeWorks; i++) hwSum += studentList[studentId].homeWork[i];
            hwAvg = hwSum / homeWorks;
            //Final_points = 0.3 * average_of_hw + 0.7 * egzam
            studentList[studentId].finalPtsAvg = 
                Math.Round((0.3 * hwAvg + 0.7 * studentList[studentId].examRes), 2, MidpointRounding.AwayFromZero);
        }

        public static void calculateMed(List<Student> studentList, int studentId)
        {
            /**
             * Consider this set of numbers: 5, 7, 9, 9, 11. 
             * Since you have an odd number of scores, the median would be 9. 
             * You have five numbers, so you divide 5 by 2 to get 2.5, 
             * and round up to 3. The number in the third position is the median.
             * */

            List<double> sortedHomeWork = new List<double>();
            int homeWorks = studentList[studentId].homeWork.Count();
            double median = 0, middleArrayNumber;

            for (int i = 0; i < homeWorks; i++) sortedHomeWork.Add(studentList[studentId].homeWork[i]);
            sortedHomeWork.Sort();
            if(homeWorks % 2 == 1)
            {
                middleArrayNumber = (int) ((homeWorks / 2) + 0.5);
                median = sortedHomeWork[Convert.ToInt32(middleArrayNumber)];
            }

            /**
             * What happens when you have an even number of scores so there is no 
             * single middle score? Consider this set of numbers: 1, 2, 2, 4, 5, 7. 
             * Since there is an even number of scores, 
             * you need to take the average of the middle two scores, calculating their mean.
             * */
            if (homeWorks % 2 == 0)
            {
                middleArrayNumber = (int)((homeWorks / 2) + 0.5);
                median += sortedHomeWork[Convert.ToInt32(middleArrayNumber)];

                middleArrayNumber = (int)((homeWorks / 2) - 0.5);
                median += sortedHomeWork[Convert.ToInt32(middleArrayNumber)] / 2;
            }
            studentList[studentId].finalPtsMed = 
                Math.Round((0.3 * median + 0.7 * studentList[studentId].examRes), 2, MidpointRounding.AwayFromZero);
        }

        public static void printStudentListAvg (List<Student> studentList)
        {
            Console.WriteLine("\nSurname        Name            Final Points (Avg.)");
            Console.WriteLine("--------------------------------------------------");
            foreach (var student in studentList) Console.WriteLine(String.Format("{0,-14} {1,-14} {2,20}", student.surName, student.name, student.finalPtsAvg));
        }

        public static void printStudentListMed(List<Student> studentList)
        {
            Console.WriteLine("\nSurname        Name            Final Points (Med.)");
            Console.WriteLine("--------------------------------------------------");
            foreach (var student in studentList) Console.WriteLine(String.Format("{0,-14} {1,-14} {2,20}", student.surName, student.name, student.finalPtsMed));
        }

        public static void printStudentListAvgMed(List<Student> studentList)
        {
            Console.WriteLine("\nSurname        Name            Final Points (Avg.)  Final Points (Med.)");
            Console.WriteLine("-----------------------------------------------------------------------");
            foreach (var student in studentList)
                Console.WriteLine(String.Format("{0,-14} {1,-14} {2,20} {3,20}", student.surName , student.name , student.finalPtsAvg , student.finalPtsMed));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //asd
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
            } while (true);
        }
    }
}
