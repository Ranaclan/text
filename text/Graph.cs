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
        string path;
        string name;
        //documents
        private List<Button> documents;
        private DateTime clickTime;
        private int docCount = 0;
        private int dragged = -1;
        private int xOffset;
        private int yOffset;
        private TextBox docName;
        private bool create = false;

        public Graph()
        {
            InitializeComponent();
            documents = new List<Button>();
            path = @"C:\Users\kiero\OneDrive\Documents\text\";
            name = "graph";
        }

        private void Save()
        {
            /*
            SaveFileDialog dialogue = new SaveFileDialog();
            dialogue.Filter = "Plain Text File (*.txt)|*.txt";
            dialogue.DefaultExt = "*.txt";
            dialogue.FilterIndex = 1;
            dialogue.Title = "Save graph as";
            */

            string text = docCount.ToString() + "\n\n";

            for (int i = 0; i < docCount; i++)
            {
                text += documents[i].Text;
                text += "\n";
                text += documents[i].Location.X + "," + documents[i].Location.Y;
                text += "\n";
                text += documents[i].Size.Width + "," + documents[i].Size.Height;

                if (i < docCount - 1)
                {
                    text += "\n\n";
                }
            }

            /*
            if (dialogue.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(Path.GetFullPath(dialogue.FileName), text);
            }
            */

            File.WriteAllText(Path.GetFullPath(path + name + ".txt"), text);
        }

        private void LoadFile()
        {
            OpenFileDialog dialogue = new OpenFileDialog();
            dialogue.Filter = "Plain Text File (*.txt)|*.txt";
            dialogue.FilterIndex = 1;
            dialogue.Title = "Load";
            
            if (dialogue.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(dialogue.FileName))
            {
                foreach(Button doc in documents)
                {
                    Controls.Remove(doc);
                }

                string text = File.ReadAllText(Path.GetFullPath(dialogue.FileName));
                string[] graphData = text.Split("\n\n");
                docCount = int.Parse(graphData[0]);
                documents = new List<Button>();

                for (int i = 1; i < docCount + 1; i++)
                {
                    string[] docData = graphData[i].Split("\n");
                    string[] docPosition = docData[1].Split(",");
                    string[] docSize = docData[2].Split(",");

                    documents.Add(new Button());
                    Controls.Add(documents[i-1]);
                    documents[i - 1].Name = (documents.Count() - 1).ToString();
                    documents[i - 1].MouseDown += new MouseEventHandler(Document_MouseDown);
                    documents[i - 1].MouseUp += new MouseEventHandler(Document_MouseUp);
                    documents[i - 1].MouseMove += new MouseEventHandler(Document_MouseMove);
                    documents[i - 1].Text = docData[0].ToString();
                    documents[i - 1].Location = new Point(int.Parse(docPosition[0]), int.Parse(docPosition[1]));
                    documents[i - 1].Size = new Size(int.Parse(docSize[0]), int.Parse(docSize[1]));
                }
            }
        }

        private void NewDocument(Point position)
        {
            docName = new TextBox();
            create = true;
            Controls.Add(docName);
            docName.Location = position;
            docName.Focus();
            docName.KeyDown += new KeyEventHandler(New_KeyDown);
        }

        private void New_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                CreateDocument(docName.Text, docName.Location);
                New_Remove();
            }

            if(e.KeyCode == Keys.Escape)
            {
                New_Remove();
            }
        }

        private void New_Remove()
        {
            if (create)
            {
                Controls.Remove(docName);
                create = false;
            }
        }

        private void CreateDocument(string name, Point position)
        {
            documents.Add(new Button());
            Controls.Add(documents[^1]);
            documents[^1].Name = (documents.Count() - 1).ToString();
            documents[^1].MouseDown += new MouseEventHandler(Document_MouseDown);
            documents[^1].MouseUp += new MouseEventHandler(Document_MouseUp);
            documents[^1].MouseMove += new MouseEventHandler(Document_MouseMove);
            documents[^1].Text = name;
            documents[^1].Location = position;
            documents[^1].Size = new Size(50, 70);
            docCount++;
        }

        private void OpenDocument(string name)
        {
            try
            {
                string file = path + name + ".rtf";
                Document document = new Document();
                Hide();

                if (!File.Exists(file))
                {
                    using (File.Create(file)) { }
                    document.LoadNewText(file);
                    document.ShowDialog();
                    Show();
                    return;
                }

                FileInfo info = new FileInfo(file);

                if(info.Length == 0)
                {
                    document.LoadNewText(file);
                }
                else
                {
                    document.LoadText(file);
                }

                document.ShowDialog();
                Show();
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
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

                if ((DateTime.Now - clickTime).TotalMilliseconds < 500)
                {
                    Button doc = (Button)sender;
                    OpenDocument(doc.Text);
                }
                else
                {
                    clickTime = DateTime.Now;
                }
            }
        }

        private void Document_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragged != -1)
            {
                documents[dragged].Location = new Point(documents[dragged].Location.X + e.X - xOffset, documents[dragged].Location.Y + e.Y - yOffset);
            }
        }

        private void Document_DoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Button button = (Button)sender;
                OpenDocument(button.Text);
            }
        }

        private void Graph_MouseDown(object sender, MouseEventArgs e)
        {
            New_Remove();

            if (e.Button == MouseButtons.Right)
            {
            }
        }

        private void Graph_DoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NewDocument(e.Location);
            }
        }
    }
}
