using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatedProject.Business_Layer
{
    internal class Students
    {
        public string StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public string Course { get; set; }

        public override string ToString()
        {
            return $"{StudentID},{StudentName},{Age},{Course}";
        }

        public Students(string studentID, string studentName, int age, string course)
        {
            this.StudentID = studentID;
            this.StudentName = studentName;
            this.Age = age;
            this.Course = course;
        }

    }
}
