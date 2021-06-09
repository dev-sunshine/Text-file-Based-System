using System;

namespace Text_file_Based_System
{
    class Teacher
     {
         public Teacher(int id, string name, string teacherClass, string section) {
             ID = id;
             Name = name;
             Class = teacherClass;
             Section = section;
         }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }

     }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
