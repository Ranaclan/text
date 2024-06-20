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
        string name;
        string path;
        //interaction
        private int create = -1;
        private string[] newText = { "New document", "New subgraph" };
        private TextBox newName;
        private int dragged = 0;
        private int xOffset;
        private int yOffset;
        private string context;
        //graphs
        private List<Button> graphs;
        private int graphCount = 0;
        private DateTime graphClick;
        //documents
        private List<Button> documents;
        private int docCount = 0;
        private DateTime docClick;

        public Graph()
        {
            InitializeComponent();
            name = "graph";
            path = @"C:\Users\kiero\OneDrive\Documents\text\";
            LoadGraph(name);
        }

        private void Save()
        {
            string text = docCount.ToString() + "\n\n" + graphCount.ToString() + "\n\n";

            for (int i = 0; i < docCount; i++)
            {
                text += documents[i].Text;
                text += "\n";
                text += documents[i].Location.X + "," + documents[i].Location.Y;
                text += "\n";
                text += documents[i].Size.Width + "," + documents[i].Size.Height;
                text += "\n\n";
            }

            for (int i = 0; i < graphCount; i++)
            {
                text += graphs[i].Text;
                text += "\n";
                text += graphs[i].Location.X + "," + graphs[i].Location.Y;
                text += "\n";
                text += graphs[i].Size.Width + "," + graphs[i].Size.Height;

                if (i < graphCount - 1)
                {
                    text += "\n\n";
                }
            }

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
                LoadGraph(dialogue.FileName);
            }
        }

        private void ChangeGraph(string name)
        {
            foreach (Button doc in documents)
            {
                Controls.Remove(doc);
            }
            foreach (Button graph in graphs)
            {
                Controls.Remove(graph);
            }

            LoadGraph(name);
        }

        private void LoadGraph(string name)
        {
            string file = path + name + ".txt";
            this.name = name;

            string text = File.ReadAllText(Path.GetFullPath(file));
            string[] graphData = text.Split("\n\n");
            graphCount = int.Parse(graphData[0]);
            graphs = new List<Button>();
            docCount = int.Parse(graphData[1]);
            documents = new List<Button>();

            for (int i = 0; i < docCount; i++)
            {
                string[] docData = graphData[i + 2].Split("\n");
                string[] docPosition = docData[1].Split(",");
                string[] docSize = docData[2].Split(",");

                documents.Add(new Button());
                Controls.Add(documents[i]);
                documents[i].Name = (documents.Count() - 1).ToString();
                documents[i].MouseDown += new MouseEventHandler(Button_MouseDown);
                documents[i].MouseUp += new MouseEventHandler(Button_MouseUp);
                documents[i].MouseUp += new MouseEventHandler(Document_MouseUp);
                documents[i].MouseMove += new MouseEventHandler(Button_MouseMove);
                documents[i].ContextMenuStrip = documentContextMenu;
                documents[i].Text = docData[0].ToString();
                documents[i].Location = new Point(int.Parse(docPosition[0]), int.Parse(docPosition[1]));
                documents[i].Size = new Size(int.Parse(docSize[0]), int.Parse(docSize[1]));
            }

            for (int i = 0; i < graphCount; i++)
            {
                string[] subGraphData = graphData[i + 2 + docCount].Split("\n");
                string[] subGraphPosition = subGraphData[1].Split(",");
                string[] subGraphSize = subGraphData[2].Split(",");

                graphs.Add(new Button());
                Controls.Add(graphs[i]);
                graphs[i].Name = (graphs.Count() - 1).ToString();
                graphs[i].MouseDown += new MouseEventHandler(Button_MouseDown);
                graphs[i].MouseUp += new MouseEventHandler(Button_MouseUp);
                graphs[i].MouseUp += new MouseEventHandler(Subgraph_MouseUp);
                graphs[i].MouseMove += new MouseEventHandler(Button_MouseMove);
                graphs[i].ContextMenuStrip = subgraphContextMenu;
                graphs[i].Text = subGraphData[0].ToString();
                graphs[i].Location = new Point(int.Parse(subGraphPosition[0]), int.Parse(subGraphPosition[1]));
                graphs[i].Size = new Size(int.Parse(subGraphSize[0]), int.Parse(subGraphSize[1]));
            }

            dragged = 0;
        }

        private void NewFeature(int type, Point position)
        {
            newName = new TextBox();
            create = type;
            Controls.Add(newName);
            newName.Location = position;
            newName.Focus();
            newName.KeyDown += new KeyEventHandler(New_KeyDown);
            newName.Text = newText[create];
            newName.SelectAll();
        }

        private void AddFeature()
        {
            switch (create)
            {
                case 0:
                    CreateDocument();
                    break;
                case 1:
                    CreateSubgraph();
                    break;
            }

            Save();
        }

        private void New_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddFeature();
            }

            if(e.KeyCode == Keys.Escape)
            {
                New_Remove();
            }
        }

        private void New_Remove()
        {
            if (create != -1)
            {
                Controls.Remove(newName);
                create = -1;
            }
        }

        private void CreateDocument()
        {
            documents.Add(new Button());
            Controls.Add(documents[^1]);
            documents[^1].Name = (documents.Count()).ToString();
            documents[^1].MouseDown += new MouseEventHandler(Button_MouseDown);
            documents[^1].MouseUp += new MouseEventHandler(Button_MouseUp);
            documents[^1].MouseUp += new MouseEventHandler(Document_MouseUp);
            documents[^1].MouseMove += new MouseEventHandler(Button_MouseMove);
            documents[^1].ContextMenuStrip = documentContextMenu;
            documents[^1].Text = newName.Text;
            documents[^1].Location = newName.Location;
            documents[^1].Size = new Size(50, 70);
            docCount++;

            New_Remove();
        }

        private void CreateSubgraph()
        {
            graphs.Add(new Button());
            Controls.Add(graphs[^1]);
            graphs[^1].Name = (-graphs.Count()).ToString();
            graphs[^1].MouseDown += new MouseEventHandler(Button_MouseDown);
            graphs[^1].MouseUp += new MouseEventHandler(Button_MouseUp);
            graphs[^1].MouseUp += new MouseEventHandler(Subgraph_MouseUp);
            graphs[^1].MouseMove += new MouseEventHandler(Button_MouseMove);
            graphs[^1].Text = newName.Text;
            graphs[^1].Location = newName.Location;
            graphs[^1].Size = new Size(80, 80);
            graphs[^1].BackColor = Color.White;
            graphCount++;

            New_Remove();
        }

        private void OpenDocument(string name)
        {
            try
            {
                string file = path + name + ".rtf";
                Document document = new Document(name, file);

                if(File.Exists(file))
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

        private void OpenSubgraph(string name)
        {
            try
            {
                string file = path + graphs[^1].Text + ".txt";

                if (!File.Exists(file))
                {
                    using (File.Create(file)) { }
                    File.WriteAllText(file, "0\n\n0\n\n");
                }

                ChangeGraph(name);
            }
            catch (Exception e)
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

        private void Graph_MouseDown(object sender, MouseEventArgs e)
        {
            if (create != -1)
            {
                if (newName.Text == "" || (create == 0 && newName.Text == "New document") || (create == 1 && newName.Text == "New subgraph"))
                {
                    New_Remove();
                }
                else
                {
                    AddFeature();
                }
            }

            if (e.Button == MouseButtons.Right)
            {
            }
        }

        private void Graph_DoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NewFeature(0, e.Location);
            }

            if(e.Button == MouseButtons.Middle)
            {
                NewFeature(1, e.Location);
            }
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Button button = (Button)sender;
                dragged = int.Parse(button.Name);
                xOffset = e.X;
                yOffset = e.Y;
            }
        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragged = 0;
                Save();
            }
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragged > 0)
            {
                documents[dragged-1].Location = new Point(documents[dragged-1].Location.X + e.X - xOffset, documents[dragged-1].Location.Y + e.Y - yOffset);
            }
            else if(dragged < 0)
            {
                graphs[-dragged - 1].Location = new Point(graphs[-dragged - 1].Location.X + e.X - xOffset, graphs[-dragged - 1].Location.Y + e.Y - yOffset);
            }
        }

        private void Document_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((DateTime.Now - docClick).TotalMilliseconds < 500)
                {
                    Button button = (Button)sender;
                    OpenDocument(button.Text);
                }
                else
                {
                    docClick = DateTime.Now;
                }
            }
        }

        private void Subgraph_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((DateTime.Now - graphClick).TotalMilliseconds < 500)
                {
                    Button button = (Button)sender;
                    OpenSubgraph(button.Text);
                }
                else
                {
                    graphClick = DateTime.Now;
                }
            }
        }

        private void AddDocument(object sender, EventArgs e)
        {
            NewFeature(0, MousePosition);
        }

        private void AddGraph(object sender, EventArgs e)
        {
            NewFeature(1, MousePosition);
        }

        private void ContextMenuOpen(object sender, CancelEventArgs e)
        {
            ContextMenuStrip menu = sender as ContextMenuStrip;
            Button button = (Button)menu.SourceControl;
            context = button.Text;
        }

        private void Document_ContextMenuOpen(object sender, EventArgs e)
        {
            OpenDocument(context);
        }

        private void Subraph_ContextMenuOpen(object sender, EventArgs e)
        {
            OpenSubgraph(context);
        }
    }
}
