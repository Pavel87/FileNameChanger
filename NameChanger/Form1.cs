using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NameChanger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label7.Text = ""; 
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (folderBrowserDialog.SelectedPath != "")
            {
                textBox1.Text = folderBrowserDialog.SelectedPath;
                string[] files = System.IO.Directory.GetFiles(folderBrowserDialog.SelectedPath);
                label4.Text = files.Length.ToString() + " Files";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //check if it is empty !! - TODO
            string dirPath = textBox1.Text;
            string[] files;
            if (dirPath.Length >= 3)
            {
                try
                {
                    files = System.IO.Directory.GetFiles(dirPath);


                    //check if amount of files is <= length
                    int minValue = Convert.ToInt32(textBox3.Text);
                    int length = Convert.ToInt32(textBox4.Text);


                    for (int i = 0; i < files.Length; i++)
                    {
                        renameFile(minValue, i, files);
                        minValue++;
                    }
                    label7.Text = "Convertion completed";
                }
                catch (System.IO.DirectoryNotFoundException ex)
                {
                    MessageBox.Show("!Directory doesn't exist. Try to use \"Browse\" button to find your folder", "!WARRNING!");
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Minimal value and Length field can contain only numbers", "!WARRNING!");
                }

            }

        }
        public void renameFile(int counter,int x, string[] files)
        {
            string prefix = textBox2.Text;
            int length = Convert.ToInt32(textBox4.Text);
            string dirPath = textBox1.Text;

            string extension = files[x].Substring(files[x].LastIndexOf("."));
            try
            {
                System.IO.File.Move(files[x], dirPath + "\\" + prefix + counter.ToString("D" + length) + extension);
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show("File \"" + files[x] + "\" cannot be accessed at this moment. File is in use by another application", "!WARRNING!");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Rename quickly thousands of your files within one folder.", "About");
        }

        private void exampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1) Select the folder containing several files \n2) Add prefix \"PICTURE_\" \n3) Enter minimal value for the first file\n4) Select length of digits \n\nPrefix=PICTURE_\nMin Value=5\nLength=3\nThe result file name will start with name:\nPICTURE_005", "Example");
        }
    }  
}
