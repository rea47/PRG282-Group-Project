using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdatedProject.Business_Layer;

namespace UpdatedProject.Data_Layer
{
    internal class Filehandler
    {
        private const string FilePath = "students.txt";

        // Add a new student to the file
        public void AddStudent(string id, string name, int age, string course)
        {
            using (StreamWriter writer = new StreamWriter(FilePath, true))
            {
                writer.WriteLine($"{id},{name},{age},{course}");
            }
        }

        // Retrieve all students from the file
        public List<Students> GetAllStudents()
        {
            var students = new List<Students>();
            if (File.Exists(FilePath))
            {
                foreach (var line in File.ReadLines(FilePath))
                {
                    var data = line.Split(',');
                    students.Add(new Students(data[0], data[1], int.Parse(data[2]), data[3]));
                }
            }
            return students;
        }

    }
}



