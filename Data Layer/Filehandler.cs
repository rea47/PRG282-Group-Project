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

        // Update a student's details in the file
        public void UpdateStudent(string id, string newName, int newAge, string newCourse)
        {
            var lines = File.ReadAllLines(FilePath).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                var data = lines[i].Split(',');
                if (data[0] == id)
                {
                    lines[i] = $"{id},{newName},{newAge},{newCourse}";
                    break;
                }
            }
            File.WriteAllLines(FilePath, lines);
        }

        // Delete a student from the file
        public void DeleteStudent(string id)
        {
            var lines = File.ReadAllLines(FilePath).Where(line => !line.StartsWith(id)).ToList();
            File.WriteAllLines(FilePath, lines);
        }
    }
}



