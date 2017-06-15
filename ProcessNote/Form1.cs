using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;

namespace ProcessNote
{
    public partial class Form1 : Form
    {
        Process[] processlist = Process.GetProcesses();
        public static Dictionary<int, string> comments = new Dictionary<int, string>();

        public Form1()
        {
            InitializeComponent();

            int i = 0;
            foreach (Process process in processlist)
            {
                processTable.Rows.Add("", "", "");
                processTable.Rows[i].Cells[0].Value = process.ProcessName;
                processTable.Rows[i].Cells[1].Value = process.Id;
                i++;
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void processTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            listBox1.Items.Clear();
            int id = processTable.CurrentCell.RowIndex;
            int processId = Int32.Parse(processTable.Rows[id].Cells[1].Value.ToString());
            foreach (Process process in processlist) {
                if (processId == process.Id) {
                    listBox1.Items.Add("Details: \n");
                    var content = processTable.CurrentCell.Value.ToString();
                    listBox1.Items.Add(content);
                    try {
                        listBox1.Items.Add("Process Id: " + process.Id);
                        listBox1.Items.Add("Process StartTime: " + process.StartTime); //Shows the time the process started
                        listBox1.Items.Add("Process StartInfo: " + process.StartInfo);
                        listBox1.Items.Add("Process Threads: " + process.Threads); //Gives access to the collection of threads in the process
                        listBox1.Items.Add("Process Threads Count: " + process.Threads.Count);
                        listBox1.Items.Add("Physical Memory Usage: " + process.WorkingSet.ToString());
                        
                    } catch (Win32Exception)
                    {
                        MessageBox.Show("You don't have access!");
                    }
                    try {
                        listBox1.Items.Add("Process UserProcessorTime: " + process.UserProcessorTime);
                        listBox1.Items.Add("Process TotalProcessorTime: " + process.TotalProcessorTime); //Shows the amount of CPU time the process has taken
                    } catch (Win32Exception)
                    {
                        MessageBox.Show("You don't have access!");
                    }
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            int id = processTable.CurrentCell.RowIndex;
            comments.Add(id, commentBox.Text);
        }
    }
}
