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

        public bool AddClass (Class className) {
            if(ClassList.Exists(x => x.Name.Equals(className.Name.ToLower())))
            return false;
            ClassList.Add(className);
            return true;
        }

        public bool UpdateClass (string oldClass, string newClassName) {
            Class c = ClassList.Find (x => x.Name.Equals(oldClass.ToLower()));
            if(c == null)
            return false;
            c.Name = newClassName;
            return true;
        }

        public Class GetClass (string className) {
            return ClassList.Find (x => x.Name.Equals(className.ToLower()));
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
        private List <Section> SectionList;

        public Class (string name ) {
            Name = name.ToLower();
            SectionList = new List <Section> ();

        }
        public string Name { get; set; }
        
        public bool AddSection (string sectionName) {
            if(SectionList.Exists(x => x.Name.Equals(sectionName)))
            return false;
            Section section = new Section (sectionName);
            SectionList.Add(section);
            return true;
        }

        public bool SectionExists (string sectionName) {
            return SectionList.Exists(x => x.Name.Equals(sectionName.ToLower()));
        }

        public bool UpdateSection (string oldSection, string newSection) {
            Section section = SectionList.Find (x => x.Name.Equals(oldSection.ToLower()));
            if(section == null)
            return false;
            section.Name = newSection;
            return true;
        }

        public int GetNumberOfSections () {
            return SectionList.Count;
        }
        public override string ToString()
        {
            string tostring = $"Class: {Name}\n";
            foreach (var element in SectionList)
                tostring += element.ToString();
            return tostring;
        }

    }//end Class class

         class Section 
    {
        public Section (string name ) {
            Name = name.ToLower();
        }
        public string Name { get; set; }
        
        public override string ToString()
        {
            return $"Section: {Name}\n";
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
            else
            Console.WriteLine($"{fileName} File found!");

        }

        public static bool retrieveAllDataFromFile(){

            StreamReader sr = new StreamReader(fileName);
            var line = "";
            string teacherName = "";
            int teacherID = 0;
            Teacher teacher = null;
            string[] subs = null;
            Class c = null;
        

            while ((line = sr.ReadLine()) != null)
                {
                    
                    subs = line.Split(": ");
                        if(subs[0].Equals("Teacher ID")) {
                            int.TryParse(subs[1], out teacherID);
                        }
                        else if(subs[0].Equals("Teacher Name")) {
                            teacherName = subs[1];
                            teacher = new Teacher(teacherID,teacherName);
                            teacherList.Add(teacher);
                        }
                        else if(subs[0].Equals("Class")) {
                            c = new Class(subs[1]);
                            teacher.AddClass(c);
                        }
                        
                        else if(subs[0].Equals("Section")) {
                            c.AddSection(subs[1]);

                        }
                }
            return true;
        }

        static void appendTeacherListToFile () {
            //File.AppendAllText(fileName, teacher.ToString());
            using(StreamWriter writer = new StreamWriter(fileName)){
             foreach( Teacher teacher in teacherList){
               writer.WriteLine(teacher);
            //    Console.WriteLine(teacher);
            }
            }

            Console.WriteLine("File updated");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("\nHello !");
            CreateFile();
            retrieveAllDataFromFile();
            while(true) {
            Console.WriteLine("\nWhich service do you want? (Enter Number Only)\n1. Add a new teacher.\n2. retrieve all teachers data.\n3. retrieve teacher data by ID.\n4. retrieve teacher data by name.\n5. Update teacher data.\n6. Add a new class for a teacher.\n7. Add a new section to one of teacher classes\n8. Exit.\n");
            int serviceNum;
            bool isNemuricValue= int.TryParse(Console.ReadLine(), out serviceNum); //if it is true value is the parsed number or 0
            if (!isNemuricValue) {
                Console.WriteLine("You have to enter numeric value only");
                continue;
            }
            if (serviceNum == 8)  {
                appendTeacherListToFile();
                break;
            }
            int teacherID = 0;
            Teacher teacher = null;
            string teacherName = "";
            string teacherClass = "";
            string teacherSection = "";
            Class c = null;
            
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
                Console.WriteLine("enter Teacher class: if you finish enter 0");
                teacherClass = Console.ReadLine();
                if(teacherClass.Equals("")) {
                    Console.WriteLine("should not be empty! try again");
                    continue;
                }
                if(teacherClass.Equals("0")&&teacher.GetNumberOfClasses()==0) {
                    Console.WriteLine("you should add at least one class");
                    continue;
                } 
                else if (teacherClass.Equals("0")&&teacher.GetNumberOfClasses()!=0) {
                    break;
                }
                c = new Class(teacherClass);
                if(!teacher.AddClass(c))
                {
                    Console.WriteLine("Class already exist");
                    continue;
                }

                while(true) {
                    Console.WriteLine($"enter Teacher section for this class {teacherClass}: if you finish enter 0");
                    teacherSection = Console.ReadLine();
                    
                    if(teacherSection.Equals("")) {
                        Console.WriteLine("should not be empty! try again");
                        continue;
                    }
                    if(teacherSection.Equals("0")&&c.GetNumberOfSections()==0) {
                    Console.WriteLine("you should add at least one section");
                    continue;
                } 
                else if (teacherSection.Equals("0")&&c.GetNumberOfSections()!=0) {
                    break;
                }
                    if(!c.AddSection(teacherSection))
                    {
                    Console.WriteLine("Section already exist");
                    continue;
                    }
                }
                }
                addTeacherToList(teacher);
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
                int userEntered = 0;
                while(true) {
                Console.WriteLine("1. Change teacher name.\n2. Change one of the teacher classes.\n3. Change one of the teacher sections.\n4. Exit.\n");
                isNemuricValue= int.TryParse(Console.ReadLine(), out userEntered);
                if (!isNemuricValue) {
                Console.WriteLine("You have to enter numeric value only");
                continue;
                 }
                if(userEntered == 4)
                break;
                switch(userEntered) {
                    case 1:
                    Console.WriteLine("Enter the new name: ");
                    teacherName = Console.ReadLine();
                    if(teacherName.Equals("")) {
                        Console.WriteLine("should not be empty! try again");
                        continue;
                    }
                    teacher.Name = teacherName;
                    break;
                    case 2:
                    string oldClassName = "";
                    Console.WriteLine("Enter the old class name: ");
                    oldClassName = Console.ReadLine();
                    c = teacher.GetClass(oldClassName);
                    if(c==null) {
                        Console.WriteLine("this class not exist!");
                        continue;
                    }
                    Console.WriteLine("Enter the new class name: ");
                    teacherClass = Console.ReadLine();
                    if(oldClassName.Equals("")||teacherClass.Equals("")) {
                        Console.WriteLine("should not be empty! try again");
                        continue;
                    }
                    if(teacher.UpdateClass(oldClassName,teacherClass))
                        Console.WriteLine("class updated successfully");
                    else 
                    Console.WriteLine("something wrong! class not updated");

                    break;
                    case 3:
                    Console.WriteLine("Enter the class of the section: ");
                    teacherClass = Console.ReadLine();
                    c = teacher.GetClass(teacherClass);
                    if (c == null) {
                        Console.WriteLine("this course not exist!");
                        continue;
                    }
                    string newSectionName = "";
                    string oldSectionName = "";
                    Console.WriteLine("Enter the old section name: ");
                    oldSectionName = Console.ReadLine();
                    if (!c.SectionExists(oldSectionName)) {
                        Console.WriteLine("this section not exist!");
                        continue;
                    }
                    Console.WriteLine("Enter the new section name: ");
                    newSectionName = Console.ReadLine();
                    if(oldSectionName.Equals("")||newSectionName.Equals("")) {
                        Console.WriteLine("should not be empty! try again");
                        continue;
                    }

                    if (c.UpdateSection(oldSectionName,newSectionName))
                    Console.WriteLine("section updated successfully");
                    else 
                    Console.WriteLine("something wrong! section not updated");
                    break;
                    default:
                    Console.WriteLine("Enter only 1 to 4.");
                    break;

                }
                }
                break;
                case 6:
                Console.WriteLine("Enter the teacher ID in order to add a new class: ");
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
                Console.WriteLine("Enter the class name: ");
                teacherClass = Console.ReadLine();
                if(teacher.GetClass(teacherClass)!=null) {
                    Console.WriteLine("this class already exist!");
                continue;
                }
                Console.WriteLine($"enter the section for this class {teacherClass}:");
                    teacherSection = Console.ReadLine();
                    
                    if(teacherSection.Equals("")) {
                        Console.WriteLine("should not be empty! try again");
                        continue;
                    }
                    c = new Class(teacherClass);
                    c.AddSection(teacherSection);
                    if(teacher.AddClass(c))
                        Console.WriteLine(teacherClass+ " class added successfully");
                    else Console.WriteLine("something wrong. the class doesn't added successfully");

                break;

                case 7:
                Console.WriteLine("Enter the teacher ID in order to add a new section: ");
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
                Console.WriteLine("Enter the class name for the new section: ");
                teacherClass = Console.ReadLine();
                c = teacher.GetClass(teacherClass);
                if(c==null) {
                    Console.WriteLine("this class is not exist!");
                continue;
                }
                Console.WriteLine($"enter the new section for this class {teacherClass}:");
                    teacherSection = Console.ReadLine();
                    
                    if(teacherSection.Equals("")) {
                        Console.WriteLine("should not be empty! try again");
                        continue;
                    }
                    if(c.AddSection(teacherSection))
                        Console.WriteLine(teacherSection+ " section added successfully");
                    else Console.WriteLine("section already exist!");

                break;

                default:
                Console.WriteLine("Enter only 1 to 6.");
                break;
            }
            
            }//end while
        }// end Main method


        public static void addTeacherToList(Teacher teacher) {
             teacherList.Add(teacher);
             Console.WriteLine("Teacher added successfully! \n"+teacher);
        } // end addTeacherToList method
        
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
        
        
    } //end Program class
}
