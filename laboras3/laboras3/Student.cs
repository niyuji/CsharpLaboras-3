using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboras3
{
    class Student : IStudent
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

        public Student() { }

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


        public static void addStudent(List<Student> studentList)
        {
            string name, surName;
            List<double> homeWork = new List<double>();

            Console.WriteLine("Enter Student's Name: ");
            name = Console.ReadLine();
            Console.WriteLine("Enter Student's Surname: ");
            surName = Console.ReadLine();

            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(surName)) return;

            Student student = new Student(name, surName, homeWork);
            studentList.Add(student);
        }

        public static void addStudentFromFile(List<Student> studentList)
        {
            string line;
            int howMany = 0;
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@"d:\students.txt");

                try
                {
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
                        studentList.Add(new Student(value[1], value[0], 0, 0, homeWork, double.Parse(value[7])));
                        calculateAvg(studentList, studentList.Count - 1);
                        calculateMed(studentList, studentList.Count - 1);
                        howMany++;
                    }
                    file.Close();
                    Console.WriteLine("Successfully added " + howMany + " records");
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("error: file structure corrupted");
                }
                finally
                {
                    if (file != null) ((IDisposable)file).Dispose();
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("error reading file: file not found");
            }
            
        }

        public static void addStudentGradesLimited(List<Student> studentList)
        {
            int studentId = -1, hwNum;
            foreach (var student in studentList)
            {
                studentId++;
                Console.WriteLine(studentId + " " + student.name + " " + student.surName);
            }
            Console.WriteLine("\nEnter Student's Id: ");
            try
            {
                studentId = Int32.Parse(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                Console.WriteLine("error: Id must be Integer Number");
            }
            try {
            studentList[studentId].homeWork.Clear(); //isvalom buvusius pazymius jei buv
            Console.WriteLine("Enter how many homeworks: ");
            hwNum = Int32.Parse(Console.ReadLine());

            for (int i = 1; i <= hwNum; i++)
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
            catch (System.ArgumentOutOfRangeException)
            {
                Console.WriteLine("error: Student record with entered Id does not exist");
            }
        }

        public static void addStudentGradesUnlimited(List<Student> studentList)
        {
            int studentId = -1, hwNum;
            foreach (var student in studentList)
            {
                studentId++;
                Console.WriteLine(studentId + " " + student.name + " " + student.surName);
            }
            Console.WriteLine("\nEnter Student's Id: ");
            try
            {
                studentId = Int32.Parse(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                Console.WriteLine("error: Id must be Integer Number");
            }
            try { 
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
            catch (System.ArgumentOutOfRangeException)
            {
                Console.WriteLine("error: Student record with entered Id does not exist");
            }
        }

        public static void generateRandomPoints(List<Student> studentList)
        {
            int randomNumber, studentId = -1;
            Random random = new Random();
            foreach (var student in studentList)
            {
                studentId++;
                Console.WriteLine(studentId + " " + student.name + " " + student.surName);
            }
            Console.WriteLine("Enter Student's Id to automatically fill in the grades: ");
            try
            {
                studentId = Int32.Parse(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                Console.WriteLine("error: Id must be Integer Number, new grades were not generated");
            }
            
            try
            {
                studentList[studentId].homeWork.Clear(); //isvalom buvusius pazymius jei buvo
                randomNumber = random.Next(1, 11); //random kiekis homeworku
                for (int i = 0; i < randomNumber; i++)
                {
                    randomNumber = random.Next(1, 11); // random pazymys 1-10
                    studentList[studentId].homeWork.Add(randomNumber);
                    Console.WriteLine("Random HW result: " + randomNumber);
                }
                studentList[studentId].examRes = random.Next(1, 11);
                Console.WriteLine("Random Exam result: " + studentList[studentId].examRes);
                calculateAvg(studentList, studentId);
                calculateMed(studentList, studentId);
                printStudentListAvg(studentList);
            }catch (System.ArgumentOutOfRangeException)
            {
                Console.WriteLine("error: Student record with entered Id does not exist");
            }
        }

        public static void calculateAvg(List<Student> studentList, int studentId)
        {
            double hwAvg = 0, hwSum = 0;
            int homeWorks = studentList[studentId].homeWork.Count();

            for (int i = 0; i < homeWorks; i++) hwSum += studentList[studentId].homeWork[i];
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
            if (homeWorks % 2 == 1)
            {
                middleArrayNumber = (int)((homeWorks / 2) + 0.5);
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

        public static void printStudentListAvg(List<Student> studentList)
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
                Console.WriteLine(String.Format("{0,-14} {1,-14} {2,20} {3,20}", student.surName, student.name, student.finalPtsAvg, student.finalPtsMed));
        }
        public static void generateStudents(int numberOfStudents)
        {
            List<Student> generatedStudentList = new List<Student>();
            Random random = new Random();
            for (int i = 0; i < numberOfStudents; i++)
                {
                    string name = "Name" + i.ToString();
                    string surName = "Surname" + i.ToString();
                    List<double> hwGrades = new List<double>();
                    double examRes;
                    for (int j =0; j<5; j++)
                    {
                        hwGrades.Add(random.Next(1, 11));
                    }
                    examRes = random.Next(1, 11);

                    generatedStudentList.Add(new Student(name, surName, 0, 0, hwGrades, examRes));
                    calculateAvg(generatedStudentList, i);
                    calculateMed(generatedStudentList, i);
                    //Console.WriteLine("no: " + i);
                    Console.WriteLine(i + " " + name + " " + surName + 
                        " [" + hwGrades[0] + " " + hwGrades[1] + " " + hwGrades[2] + " " + hwGrades[3] + " " + hwGrades[4] + 
                        "] " + examRes);
                    //printStudentListAvgMed(generatedStudentList);
                }
            //printStudentListAvgMed(generatedStudentList);

            List<Student> generatedStudentListPassed = new List<Student>();
            List<Student> generatedStudentListFailed = new List<Student>();
            foreach (var student in generatedStudentList)
            {
                if (student.finalPtsAvg >= 5.0d)
                {
                    generatedStudentListPassed.Add(student);
                }
                else
                {
                    generatedStudentListFailed.Add(student);
                }
            }
            //Student.irasymas(generatedStudentListPassed,true);
            //Student.irasymas(generatedStudentListFailed,false);
            String passPath = @"d:\students_passed.txt";
            String failedPath = @"d:\students_failed.txt";

            using (var irasymas = new StreamWriter(passPath))
            {
                foreach(var item in generatedStudentListPassed)
                {
                    //irasymas.WriteLine(string.Join(",", properties.Select(p => p.GetValue(item, null))));
                    irasymas.WriteLine("name: " + item.name + " surname: " + item.surName + 
                        " homework: [" + item.homeWork[0] + " " + item.homeWork[1] + " " + item.homeWork[2] + " " + item.homeWork[3] + " " + item.homeWork[4] + 
                        "] exam: " + item.examRes + " [ avg: " + item.finalPtsAvg + ", med: " + item.finalPtsMed + "]");
                }
            }
            using (var irasymas = new StreamWriter(failedPath))
            {
                foreach(var item in generatedStudentListFailed)
                {
                    //irasymas.WriteLine(string.Join(",", properties.Select(p => p.GetValue(item, null))));
                    irasymas.WriteLine("name: " + item.name + " surname: " + item.surName + 
                        " homework: [" + item.homeWork[0] + " " + item.homeWork[1] + " " + item.homeWork[2] + " " + item.homeWork[3] + " " + item.homeWork[4] + 
                        "] exam: " + item.examRes + " [ avg: " + item.finalPtsAvg + ", med: " + item.finalPtsMed + "]");
                }
            }
        }

        /**public static void irasymas<T>(List<T> isfiltruoti, bool didPass) // i faila
        {
            String kelias = @"d:\students_passed.txt";
            if (didPass==true) kelias = @"d:\students_passed.txt";
            if (didPass==false) kelias = @"d:\students_failed.txt";
            
            if ((!File.Exists(kelias)))
            {
                FileStream fs = File.Create(kelias);
                fs.Close();
            }

            Type itemType = typeof(T);
            var properties = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            using (var irasymas = new StreamWriter(kelias))
            {
                //surasytu "Vardas,Pavarde,Pareigos,Profesine_patirtis,Pageidaujamas_atlygis" i pirma eilute
                //irasymas.WriteLine(string.Join(",", props.Select(p => p.Name)));
                foreach(var item in isfiltruoti)
                {
                    irasymas.WriteLine(string.Join(",", properties.Select(p => p.GetValue(item, null))));
                    //irasymas.WriteLine(string.Format("Item: {0} - Cost: {1}", , item.Cost.ToString()));
                }
            }
        }**/
    }
}
