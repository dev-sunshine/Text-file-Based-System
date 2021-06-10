using System;
using System.Collections.Generic;  
using System.IO;

namespace Text_file_Based_System
{
    class Teacher
     {
        private List <Class> ClassList;

        public Teacher(int id, string name) {
             ID = id;
             Name = name;
             ClassList = new List <Class>();
         }
        public int ID { get; set; }
        public string Name { get; set; }

        public void AddClass (Class className) {
            ClassList.Add(className);
        }
        public void UpdateClass (Class oldClass, string newClassName) {
            Class c = ClassList.Find (x => x.Name.Equals(oldClass.Name));
            ClassList.Add(c);
        }

        public List <Class> GetClassList () {
            return ClassList;
        }
        public int GetNumberOfClasses () {
            return ClassList.Count;
        }

        public override string ToString()
        {
            string tostring = $"Teacher ID: {ID}\nTeacher Name: {Name}\n";
            foreach (var element in ClassList)
            tostring += element.ToString();
            return tostring;
        }

     } //end Teacher class
     class Class 
    {
        private List <string> SectionList;
        public Class (string name ) {
            Name = name.ToLower();
            SectionList = new List <string> ();

        }
        public string Name { get; set; }
        public List <string> GetSectionList () {
            return SectionList;
        }
        public bool AddSection (string section) {
            string section1 = SectionList.Find (x => x.Equals(section));
            SectionList.Add(section);
            return true;
        }
        public int GetNumberOfSections () {
            return SectionList.Count;
        }
        public override string ToString()
        {
            string tostring = $"Class: {Name}\n";
            foreach (var element in SectionList)
            tostring += $"Section: {element.ToString()}\n";
            return tostring;
        }

    }//end Class class



    class Program
    {
        static List< Teacher> teacherList = new List <Teacher>();
        const string fileName = "project.txt";

        public static void CreateFile(){

            if(!File.Exists(fileName)){
                File.CreateText(fileName);
                Console.WriteLine("the file is empty! a new file created");
            }
            Console.WriteLine($"{fileName} File found!");

        }

        static void appendTeacherListToFile () {
            foreach( Teacher teacher in teacherList){
            using(StreamWriter writer = new StreamWriter(fileName)){
                writer.WriteLine(teacher);
            }
            }

            Console.WriteLine("File updated");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("\nHello !");
            CreateFile();
            while(true) {
            Console.WriteLine("\nWhich service do you want? (Enter Number Only)\n1. Add a new teacher.\n2. retrieve all teachers data.\n3. retrieve teacher data by ID.\n4. retrieve teacher data by name.\n5. Update teacher data.\n6. Exit.\n");
            int serviceNum;
            bool isNemuricValue= int.TryParse(Console.ReadLine(), out serviceNum); //if it is true value is the parsed number or 0
            if (!isNemuricValue) {
                Console.WriteLine("You have to enter numeric value only");
                continue;
            }
            if (serviceNum == 6)  {
                appendTeacherListToFile();
                break;
            }
            int teacherID = 0;
            Teacher teacher = null;
            string teacherName = "";
            string teacherClass = "";
            string teacherSection = "";
            
            switch(serviceNum)
            {
                case 1:
                Console.WriteLine("enter Teacher name:");
                teacherName = Console.ReadLine();
                if(teacherName.Equals("")) {
                    Console.WriteLine("should not be empty! try again");
                    continue;
                }
                Console.WriteLine("enter Teacher ID:");
                isNemuricValue = int.TryParse(Console.ReadLine(), out teacherID);
                if (!isNemuricValue) {
                    Console.WriteLine("enter numeric value only");
                    continue;
                }
                if(teacherList.Exists(x => x.ID== teacherID)) {
                    Console.WriteLine("this ID already exists");
                    continue;
                }
                teacher = new Teacher (teacherID, teacherName);

                while(true) {
                Console.WriteLine("enter Teacher class: if you finish enter 1");
                teacherClass = Console.ReadLine();
                if(teacherClass.Equals("")) {
                    Console.WriteLine("should not be empty! try again");
                    continue;
                }
                if(teacherClass.Equals("1")&&teacher.GetNumberOfClasses()==0) {
                    Console.WriteLine("you should add at least one class");
                    continue;
                } 
                else if (teacherClass.Equals("1")&&teacher.GetNumberOfClasses()!=0) {
                    break;
                }
                Class c = new Class(teacherClass);
                teacher.AddClass(c);


                while(true) {
                    Console.WriteLine($"enter Teacher section for this class {teacherClass}: if you finish enter 1");
                    teacherSection = Console.ReadLine();
                    
                    if(teacherSection.Equals("")) {
                        Console.WriteLine("should not be empty! try again");
                        continue;
                    }
                    if(teacherSection.Equals("1")&&c.GetNumberOfSections()==0) {
                    Console.WriteLine("you should add at least one section");
                    continue;
                } 
                else if (teacherSection.Equals("1")&&c.GetNumberOfSections()!=0) {
                    break;
                }
                    c.AddSection(teacherSection);
                }
                }
                addTeacher(teacher);
                break;

                case 2:
                printAllTeachersData();
                break;

                case 3:
                Console.WriteLine("enter Teacher ID:");
                isNemuricValue= int.TryParse(Console.ReadLine(), out teacherID); //if it is true value is the parsed number or 0
                if (!isNemuricValue) {
                Console.WriteLine("You have to enter numeric value only");
                continue;
                 }
                teacher = findTeacherById(teacherID);
                if (teacher!=null)
                Console.WriteLine(teacher);
                else
                Console.WriteLine("teacher not exist");
                break;

                case 4:
                Console.WriteLine("enter Teacher name:");
                teacherName = Console.ReadLine();
                teacher = findTeacherByName(teacherName);
                if (teacher!=null)
                Console.WriteLine(teacher);
                else
                Console.WriteLine($"teacher {teacherName} not exist");
                break;

                case 5:
                /* 
                Console.WriteLine("Enter the teacher ID in order to update his/her data: ");
                Console.WriteLine("enter Teacher ID:");
                isNemuricValue = int.TryParse(Console.ReadLine(), out teacherID);
                if (!isNemuricValue) {
                    Console.WriteLine("enter numeric value only");
                continue;
                }
                teacher = findTeacherById(teacherID);
                if(teacher==null) {
                    Console.WriteLine("Teacher with this ID is not exist!");
                continue;
                }
                Console.WriteLine(teacher);
                Console.WriteLine("Enter the teacher class");
                updateTeacherClass(teacher, Console.ReadLine());
                Console.WriteLine("Enter the teacher section");
                updateTeacherSection(teacher, Console.ReadLine());
                Console.WriteLine("teacher data updated successfully: \n" + teacher);
                */
                break;

                default:
                Console.WriteLine("Enter only 1 to 6.");
                break;
            }
            
            }//end while
        }// end Main method


        public static void addTeacher(Teacher teacher) {
             teacherList.Add(teacher);
             Console.WriteLine("Teacher added successfully! \n"+teacher);
        } // end addTeacher method
        
        public static void printAllTeachersData (){
            if(teacherList.Count==0)
            Console.WriteLine("There is no teachers yet");
            else {
                foreach(var teacher in teacherList){
                    Console.WriteLine(teacher);
                    }
                    }
        }// end printAllTeachersData method

        //Find a Teacher methods
        public static Teacher findTeacherById(int ID){
            return  teacherList.Find (x => x.ID== ID);
        }
        public static Teacher findTeacherByName(string name){
            return  teacherList.Find (x => x.Name.Equals(name));
        }
        /*
        //Update Teacher data methods
        public static bool updateTeacherClass (Teacher teacher, string Class){
            if (teacher==null)
            return false;
            teacher.Class = Class;
            return true;
        }
        public static bool updateTeacherSection (Teacher teacher, string Section){
            if (teacher==null)
            return false;
            teacher.Section = Section;
            return true;
        }
         */
        
        
    } //end Program class
}
