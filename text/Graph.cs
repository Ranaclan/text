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

namespace text
{
    public partial class Graph : Form
    {
        //documents
        private Button[] documents;
        private int docCount = 2;
        private int dragged = -1;
        private int xOffset;
        private int yOffset;

        public Graph()
        {
            InitializeComponent();
        }

        private void Save()
        {
            SaveFileDialog dialogue = new SaveFileDialog();
            dialogue.Filter = "Plain Text File (*.txt)|*.txt";
            dialogue.DefaultExt = "*.txt";
            dialogue.FilterIndex = 1;
            dialogue.Title = "Save graph as";

            string text = "";

            for (int i = 0; i < docCount; i++)
            {
                text += documents[i].Name;
                text += "\n";
                text += documents[i].Location.X + "," + documents[i].Location.Y;
                text += "\n";
                text += documents[i].Size.Width + "," + documents[i].Size.Width;

                if (i < docCount - 1)
                {
                    text += "\n\n";
                }
            }

            if (dialogue.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(Path.GetFullPath(dialogue.FileName), text);
            }
        }

        private void LoadFile()
        {
            OpenFileDialog dialogue = new OpenFileDialog();
            dialogue.Filter = "Plain Text File (*.txt)|*.txt";
            dialogue.FilterIndex = 1;
            dialogue.Title = "Load";
            
            if (dialogue.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(dialogue.FileName))
            {
                string text = File.ReadAllText(Path.GetFullPath(dialogue.FileName));
                string[] graphData = text.Split("\n\n");
                documents = new Button[2];

                for (int i = 0; i < docCount; i++)
                {
                    string[] docData = graphData[i].Split("\n");
                    string[] docPosition = docData[1].Split(",");
                    string[] docSize = docData[2].Split(",");

                    documents[i] = new Button();
                    Controls.Add(documents[i]);
                    documents[i].Name = docData[0].ToString();
                    documents[i].MouseDown += new MouseEventHandler(this.Document_MouseDown);
                    documents[i].MouseUp += new MouseEventHandler(this.Document_MouseUp);
                    documents[i].MouseMove += new MouseEventHandler(this.Document_MouseMove);
                    documents[i].Text = docData[0].ToString();
                    documents[i].Location = new Point(int.Parse(docPosition[0]), int.Parse(docPosition[1]));
                    documents[i].Size = new Size(int.Parse(docSize[0]), int.Parse(docSize[1]));
                }
            }
        }


        private void CreateDocument()
        {

        }

        private void Graph_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                e.SuppressKeyPress = true;

                if (e.KeyCode == (Keys.S))
                {
                    Save();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys key)
        {
            if (key == (Keys.O | Keys.Control))
            {
                LoadFile();
            }

            return base.ProcessCmdKey(ref msg, key);
        }

        private void Document_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Button doc = (Button)sender;
                dragged = int.Parse(doc.Name);
                xOffset = e.X;
                yOffset = e.Y;
            }
        }

        private void Document_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragged = -1;
            }
        }

        private void Document_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragged != -1)
            {
                documents[dragged].Location = new Point(documents[dragged].Location.X + e.X - xOffset, documents[dragged].Location.Y + e.Y - yOffset);
            }
        }

        private void Graph_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                CreateDocument();
            }
        }
    }
}
