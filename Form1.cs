using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

           FileStream hhh =  File.Open("база.txt", FileMode.Open);
            if (hhh.Length<  0)
            {
                hhh.Close();
                return;
            }
            using (BinaryReader bw = new BinaryReader(hhh))
            {
                int n = bw.ReadInt32();
                int m = bw.ReadInt32();
                for (int i = 0; i < m; ++i)
                {
                    dataGridView1.Rows.Add();
                    for (int j = 0; j < n; ++j)
                    {
                        if (bw.ReadBoolean())
                        {
                            dataGridView1.Rows[i].Cells[j].Value = bw.ReadString();
                        }
                        else bw.ReadBoolean();
                    }
                }

            }
        }

        class Student
        {
            public string fio, subject, mark;
            public DateTime date;

            public Student(string fio, string subject, string mark) 
            {
                this.fio = fio;
                this.subject = subject;
                this.mark = mark; ;
            }
             
            public void Vivod() 
            {
                Console.WriteLine("ФИО : {0}", fio);
                Console.WriteLine("Оценка :{0}", mark); 
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a = dataGridView1.CurrentRow.Index;

            dataGridView1[2, a].Value = textBox1.Text;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            for(int a = 0; dataGridView1.Rows.Count -1 > a; a++)
            {
                if(dataGridView1[1, a].Value.ToString().Contains(textBox2.Text))
                {
                    dataGridView1.Rows[a].Selected = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Student a = new Student(textBox3.Text, "Математика", textBox1.Text);
            dataGridView1.Rows.Add(a.fio,"",a.subject, DateTime.Now);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            string file = "база.txt";
            using (BinaryWriter bw = new BinaryWriter(File.Open(file, FileMode.Create)))
            {
                bw.Write(dataGridView1.Columns.Count);
                bw.Write(dataGridView1.Rows.Count);
                foreach (DataGridViewRow dgvR in dataGridView1.Rows)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; ++j)
                    {
                        object val = dgvR.Cells[j].Value;
                        if (val == null)
                        {
                            bw.Write(false);
                            bw.Write(false);
                        }
                        else
                        {
                            bw.Write(true);
                            bw.Write(val.ToString());
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Student a = new Student(textBox3.Text, "Математика", textBox1.Text);
            dataGridView1.Rows.Add(a.fio, "", a.subject, DateTime.Now);
        }
    }
}
