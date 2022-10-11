using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
namespace Sekretariat2
{
   
    public partial class Form2 : Form
    {
        class Student
        {
            public string id;
            public string name;
            public string surname;
            public string curClass;

            public Student(string curId,string curName,string curSurname, string choosenClass) {
                id = curId;
                name = curName;
                surname = curSurname;
                curClass = choosenClass;
            }
        }

        string path = @".\uczniowie.txt";
        List<Student> students = new List<Student>();
        int id = 0;

        public Form2()
        {
            InitializeComponent();
         
      
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void initStudents()
        {
            string s = "";
            using (StreamReader sr = File.OpenText(path))
            {
                while ((s = sr.ReadLine()) != null)
            {
                string[] elements = s.Split(' ');

                if (elements.Length == 4)
                    students.Add(
                   new Student(elements[0].ToString(), elements[1].ToString(), elements[2].ToString(), elements[3].ToString()));

                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            students = new List<Student>();
            var i = 0;
            this.richTextBox1.Text = "";
          
               
                List<Student> filteredStudents = new List<Student>();

            this.initStudents();
                if (comboBoxRegex.Text != "" && comboBoxField.Text !="")
                {
                   
                    foreach (var student in students)
                    {
                        

                        if (comboBoxRegex.Text == "Zawiera") {
                            Regex rg = new Regex(textBoxWord.Text);
                            if (comboBoxField.Text == "Nazwisko" && rg.IsMatch(student.surname))
                            {
                                filteredStudents.Add(student);
                            }

                                if (comboBoxField.Text == "Imie" && rg.IsMatch(student.name)) {
                                filteredStudents.Add(student);
                            }

                                    if (comboBoxField.Text == "Klasa" && rg.IsMatch(student.curClass)) {
                                filteredStudents.Add(student);
                            }
                                        }
                        else
                        {
                            Regex rg = new Regex("^("+textBoxWord.Text+")");
                            if (comboBoxField.Text == "Nazwisko" && rg.IsMatch(student.surname)) {
                                filteredStudents.Add(student);
                            }

                                if (comboBoxField.Text == "Imie" && rg.IsMatch(student.name)) {
                                filteredStudents.Add(student);
                            }

                                    if (comboBoxField.Text == "Klasa" && rg.IsMatch(student.curClass)) {
                                filteredStudents.Add(student);
                            }
                        }

                        i++;
                    }
                foreach (var student in filteredStudents)
                {
                    richTextBox1.Text += student.id + " " + student.name + " " + student.surname + " " + student.curClass + "\n";
                }
            }
            else
            {
                foreach (var student in students)
                {
                    richTextBox1.Text += student.id + " " + student.name + " " + student.surname + " " + student.curClass + "\n";
                }
            }
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(generateId()+" "+textBoxName.Text + " "+textBoxSurname.Text+" "+textBoxClass.Text);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(generateId() + " " + textBoxName.Text + " " + textBoxSurname.Text + " " + textBoxClass.Text);
                }
            }
            id++;
        }

        private string generateId() {
            var generatedId = "";

            if(Math.Floor(Math.Log10(id) + 1) == 1)
            {
                generatedId = "00" + id.ToString();
                return generatedId;
            }
            if (Math.Floor(Math.Log10(id) + 1) == 2)
            {
                generatedId = "0" + id.ToString();
                return generatedId;
            }

            generatedId = id.ToString();
            return generatedId;

        }
    }
}
