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
using System.Text.RegularExpressions;

namespace UpdatedProject
{
    public partial class Form1 : Form
    {
        private Filehandler fileHandler = new Filehandler();

        public Form1()
        {
            InitializeComponent();
        }

        // Button click event to add a new student
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                try
                {
                    string id = txtStudentID.Text;
                    string name = txtStudentName.Text;
                    int age = int.Parse(txtStudentAge.Text);
                    string course = txtStudentCourse.Text;

                    // Add student to file
                    fileHandler.AddStudent(id, name, age, course);
                    DisplayAllStudents();
                    MessageBox.Show("Student added successfully!", "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding student: {ex.Message}", "Error");
                }
            }
        }

        // Button click event to update existing student information
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                try
                {
                    string id = txtStudentID.Text;
                    string newName = txtStudentName.Text;
                    int newAge = int.Parse(txtStudentAge.Text);
                    string newCourse = txtStudentCourse.Text;

                    // Update student information in file
                    fileHandler.UpdateStudent(id, newName, newAge, newCourse);
                    DisplayAllStudents();
                    MessageBox.Show("Student updated successfully!", "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating student: {ex.Message}", "Error");
                }
            }
        }

        // Validates input fields for correctness
        // Validates input fields for correctness
        private bool ValidateInputs()
        {
            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(txtStudentID.Text) ||
                string.IsNullOrWhiteSpace(txtStudentName.Text) ||
                string.IsNullOrWhiteSpace(txtStudentAge.Text) ||
                string.IsNullOrWhiteSpace(txtStudentCourse.Text))
            {
                MessageBox.Show("All fields are required.", "Validation Error");
                return false;
            }

            // Check if the age is a positive integer
            if (!int.TryParse(txtStudentAge.Text, out int age) || age <= 0)
            {
                MessageBox.Show("Please enter a valid positive integer for age.", "Validation Error");
                return false;
            }

            // Validate that the student name contains only letters (no digits or special characters)
            if (!Regex.IsMatch(txtStudentName.Text, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Student name should only contain letters.", "Validation Error");
                return false;
            }

            // Check for duplicate Student ID
            if (IsDuplicateStudentID(txtStudentID.Text))
            {
                MessageBox.Show("Student ID already exists. Please enter a unique ID.", "Validation Error");
                return false;
            }

            return true;
        }


        // Button click event to delete a student
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    string id = dataGridView1.SelectedRows[0].Cells["StudentID"].Value.ToString();
                    fileHandler.DeleteStudent(id);
                    DisplayAllStudents();
                    MessageBox.Show("Student deleted successfully!", "Success");
                }
                else
                {
                    MessageBox.Show("Please select a student to delete.", "Warning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting student: {ex.Message}", "Error");
            }
        }

        // Displays all students in DataGridView
        private void dvgsDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.ColumnCount = 4;
                dataGridView1.Columns[0].Name = "StudentID";
                dataGridView1.Columns[1].Name = "StudentName";
                dataGridView1.Columns[2].Name = "StudentAge";
                dataGridView1.Columns[3].Name = "StudentCourse";
                DisplayAllStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying students: {ex.Message}", "Error");
            }
        }

        // Displays all students from the file
        private void DisplayAllStudents()
        {
            try
            {
                var studentsList = fileHandler.GetAllStudents();
                dataGridView1.DataSource = studentsList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading students: {ex.Message}", "Error");
            }
        }

        // Handles cell click to populate fields with selected student's data
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


        // Checks if the entered Student ID already exists
        private bool IsDuplicateStudentID(string studentID)
        {
            try
            {
                // Retrieve the current list of students
                var studentsList = fileHandler.GetAllStudents();

                // Check if any student has the same ID
                return studentsList.Any(student => student.StudentID.Equals(studentID, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking for duplicates: {ex.Message}", "Error");
                return false;
            }
        }



        private void LblSummary_Click(object sender, EventArgs e)
            {

            }
        }

    } 

    


