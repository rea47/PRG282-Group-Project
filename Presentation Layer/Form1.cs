using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UpdatedProject.Data_Layer;

namespace UpdatedProject
{
    public partial class Form1 : Form
    {
        private Filehandler fileHandler = new Filehandler();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id = txtStudentID.Text;
            string name = txtStudentName.Text;
            int age = int.Parse(txtStudentAge.Text);
            string course = txtStudentCourse.Text;

            fileHandler.AddStudent(id, name, age, course);
            DisplayAllStudents();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtStudentID.Text;
            string newName = txtStudentName.Text;
            int newAge = int.Parse(txtStudentAge.Text);
            string newCourse = txtStudentCourse.Text;

            fileHandler.UpdateStudent(id, newName, newAge, newCourse);
            DisplayAllStudents();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id = dataGridView1.SelectedRows[0].Cells["StudentID"].Value.ToString();
                fileHandler.DeleteStudent(id);
                DisplayAllStudents();
            }
        }

        private void dvgsDisplay_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "StudentID";
            dataGridView1.Columns[1].Name = "StudentName";
            dataGridView1.Columns[2].Name = "StudentAge";
            dataGridView1.Columns[3].Name = "StudentCourse";
            DisplayAllStudents();
        }

        private void SaveSummary(int totalStudents, double averageAge)
        {
            string summaryReport = $"Summary Report\n" +
                                   $"Total Students: {totalStudents}\n" +
                                   $"Average Age: {averageAge:F2}\n";

            // Save to summary.txt
            File.WriteAllText("summary.txt", summaryReport);
        }

        private void btnViewSummary_Click(object sender, EventArgs e)
        {
            // Get all students from the file
            var studentsList = fileHandler.GetAllStudents();

            // Calculate total number of students and average age
            int totalStudents = studentsList.Count;
            double averageAge = studentsList.Any() ? studentsList.Average(s => s.Age) : 0;

            // Display the summary in the label
            LblSummary.Text = $"Total Students: {totalStudents}\nAverage Age: {averageAge:F2}";

            // Save the summary to a file
            SaveSummary(totalStudents, averageAge);
        }

        private void DisplayAllStudents()
        {
            var studentsList = fileHandler.GetAllStudents();
            dataGridView1.DataSource = studentsList;
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtStudentID.Text = row.Cells["StudentID"].Value.ToString();
                txtStudentName.Text = row.Cells["StudentName"].Value.ToString();
                txtStudentAge.Text = row.Cells["StudentAge"].Value.ToString();
                txtStudentCourse.Text = row.Cells["StudentCourse"].Value.ToString();
            }
        }

        private void LblSummary_Click(object sender, EventArgs e)
        {

        }
    }
}

