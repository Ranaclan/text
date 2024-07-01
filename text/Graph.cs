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

namespace text
{
    public partial class Graph : Form
    {
        string name;
        string supername;
        string path;
        string currentName;
        int layer;
        //interaction
        private int create = -1;
        private string[] newText = { "New document", "New subgraph" };
        private TextBox newName;
        private int dragged = 0;
        private int xOffset;
        private int yOffset;
        private string context;
        //graph explorer
        private List<Button> graphEntries;
        private List<string> explorerEntries;
        private int draggedExplorer = 0;
        private Point originalLocation;
        private List<Button> order;
        private List<Button> rearranged;
        private string supergraph;
        private DateTime explorerClick;
        //graphs
        private List<Button> graphs;
        private int graphCount = 0;
        private DateTime graphClick;
        //documents
        private List<Button> documents;
        private int docCount = 0;
        private DateTime docClick;
        //appearance
        private int explorerGap = 30;

        public Graph()
        {
            InitializeComponent();
            name = "graph";
            supername = name;
            path = @"C:\Users\kiero\OneDrive\Documents\text\";
            supergraphButton.Text = name;
            if (!File.Exists(path + name + ".txt"))
            {
                using (File.Create(path + name + ".txt")) { }
                File.WriteAllText(path + name + ".txt", "0\n\n0\n\n0\n\n");
            }

            currentName = name;
            layer = 0;
            LoadGraph(name);
            graphEntries = new List<Button>();
            explorerEntries = new List<string>();
            LoadGraphExplorer(path + name + ".txt", 0, 0);
        }

        private void Save()
        {
            string text = layer.ToString() + "\n\n" + docCount.ToString() + "\n\n" + graphCount.ToString() + "\n\n";

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

            currentName = name;
            LoadGraph(name);
        }

        private void LoadGraph(string name)
        {
            string file = path + name + ".txt";
            this.name = name;

            string text = File.ReadAllText(Path.GetFullPath(file));
            string[] graphData = text.Split("\n\n");
            layer = int.Parse(graphData[0]);
            docCount = int.Parse(graphData[1]);
            documents = new List<Button>();
            graphCount = int.Parse(graphData[2]);
            graphs = new List<Button>();
            
            for (int i = 0; i < docCount; i++)
            {
                string[] docData = graphData[i + 3].Split("\n");
                string[] docPosition = docData[1].Split(",");
                string[] docSize = docData[2].Split(",");

                documents.Add(new Button());
                Controls.Add(documents[i]);
                documents[i].Name = (documents.Count()).ToString();
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
                string[] subGraphData = graphData[i + 3 + docCount].Split("\n");
                string[] subGraphPosition = subGraphData[1].Split(",");
                string[] subGraphSize = subGraphData[2].Split(",");

                graphs.Add(new Button());
                Controls.Add(graphs[i]);
                graphs[i].Name = (-graphs.Count()).ToString();
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

        private int LoadGraphExplorer(string file, int index, int layer)
        {
            string text = File.ReadAllText(Path.GetFullPath(file));
            string[] graphData = text.Split("\n\n");
            int docs = int.Parse(graphData[1]);
            int graphs = int.Parse(graphData[2]);

            for (int i = 0; i < graphs; i++)
            {
                string[] subGraphData = graphData[i + 3 + docs].Split("\n");

                graphEntries.Add(new Button());
                Controls.Add(graphEntries[index]);
                graphEntries[index].Name = (graphEntries.Count() - 1).ToString();
                graphEntries[index].MouseDown += new MouseEventHandler(Explorer_MouseDown);
                graphEntries[index].MouseUp += new MouseEventHandler(Explorer_MouseUp);
                graphEntries[index].MouseMove += new MouseEventHandler(Explorer_MouseMove);
                graphEntries[index].ContextMenuStrip = null;
                graphEntries[index].Text = subGraphData[0];
                graphEntries[index].Location = new Point(supergraphButton.Location.X + 10 + 10 * layer, supergraphButton.Location.Y + explorerGap * (index + 1));
                graphEntries[index].Size = new Size(supergraphButton.Size.Width, supergraphButton.Size.Height);

                index++;
                if (layer < 3)
                {
                    index = LoadGraphExplorer(path + subGraphData[0] + ".txt", index, layer + 1);
                }
            }

            return index;
        }

        private void ReloadGraphExplorer()
        {
            foreach(Button button in graphEntries)
            {
                Controls.Remove(button);
            }

            graphEntries = new List<Button>();
            explorerEntries = new List<string>();
            LoadGraphExplorer(path + supername + ".txt", 0, 0);
        }

        private void CollapseExpandGraphExplorer(string name)
        {
            int start = int.Parse(name);

            if (start < graphEntries.Count - 1)
            {
                if (graphEntries[start + 1].Name.Contains(":"))
                {
                    ExpandGraphExplorer(start);
                }
                else
                {
                    CollapseGraphExplorer(start);
                }
            }
        }

        private void CollapseGraphExplorer(int start)
        {
            bool removal = true;
            for (int i = start + 1, remove = 0; i < graphEntries.Count; i++)
            {
                if (removal && graphEntries[i].Location.X > graphEntries[start].Location.X)
                {
                    if (!graphEntries[i].Name.Contains(":"))
                    {
                        Controls.Remove(graphEntries[i]);
                        remove++;
                    }

                    graphEntries[i].Name += ":" + graphEntries[start].Name;

                }
                else
                {
                    removal = false;
                    graphEntries[i].Location = new Point(graphEntries[i].Location.X, graphEntries[i].Location.Y - explorerGap * remove);
                }
            }
        }

        private void ExpandGraphExplorer(int start)
        {
            for (int i = start + 1, add = 0; i < graphEntries.Count; i++)
            {
                if (graphEntries[i].Name.Contains(":" + graphEntries[start].Name))
                {
                    graphEntries[i].Name = graphEntries[i].Name.Replace(":" + graphEntries[start].Name, "");

                    if (!graphEntries[i].Name.Contains(":"))
                    {
                        Controls.Add(graphEntries[i]);
                        add++;
                    }
                }
                else
                {
                    graphEntries[i].Location = new Point(graphEntries[i].Location.X, graphEntries[i].Location.Y + explorerGap * add);
                }
            }
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
            Save();
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

            string file = path + graphs[^1].Text + ".txt";

            if (!File.Exists(file))
            {
                using (File.Create(file)) { }
                File.WriteAllText(file, (layer + 1).ToString() + "\n\n0\n\n0\n\n");
            }

            New_Remove();
            Save();

            ReloadGraphExplorer();
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
                string file = path + name + ".txt";

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
                documents[dragged - 1].Location = new Point(documents[dragged-1].Location.X + e.X - xOffset, documents[dragged-1].Location.Y + e.Y - yOffset);
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

        private void Explorer_MouseDown(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;

            if (e.Button == MouseButtons.Left)
            {
                if (button != supergraphButton)
                {
                    draggedExplorer = int.Parse(button.Name) + 1;
                    originalLocation = button.Location;
                    order = EntryOrder(button);
                    xOffset = e.X;
                    yOffset = e.Y;
                }
                explorerClick = DateTime.Now;
            }
        }

        private void Explorer_MouseUp(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            if (e.Button == MouseButtons.Left)
            {
                draggedExplorer = 0;
                if ((DateTime.Now - explorerClick).TotalMilliseconds < 200)
                {
                    OpenSubgraph(button.Text);
                }
                else
                {
                    RearrangedOrder();

                    if (!Enumerable.SequenceEqual(order, rearranged))
                    {
                        ExplorerSwap(button);
                    }
                }
                ReloadGraphExplorer();
            }

            if (e.Button == MouseButtons.Middle && button.Name != "supergraph")
            {
                CollapseExpandGraphExplorer(button.Name);
            }
        }

        private void ExplorerSwap(Button moved)
        {
            for (int i = 0; i < rearranged.Count; i++)
            {
                if (rearranged[i] == moved)
                {
                    Button previous = null;
                    if(i > 0)
                    {
                        previous = rearranged[i - 1];
                    }

                    string file;
                    if (supergraph == null)
                    {
                        file = path + supername + ".txt";
                    }
                    else
                    {
                        file = path + supergraph + ".txt";
                    }

                    string text = File.ReadAllText(Path.GetFullPath(file));
                    string[] graphData = text.Split("\n\n");
                    int docCount = int.Parse(graphData[1]);
                    int graphCount = int.Parse(graphData[2]);
                    string movedData = "";
                    string previousData = "";

                    if (previous == null)
                    {
                        //top
                        for (int j = 0; j < graphCount; j++)
                        {
                            string[] subGraphData = graphData[j + 3 + docCount].Split("\n");
                            if (subGraphData[0] == moved.Text)
                            {
                                movedData = graphData[j + 3 + docCount];
                                break;
                            }
                        }

                        previousData = graphData[3 + docCount];

                        File.WriteAllText(file, Regex.Replace(File.ReadAllText(file), "\n\n" + movedData, ""));
                        File.WriteAllText(file, Regex.Replace(File.ReadAllText(file), previousData, movedData + "\n\n" + previousData));
                    }
                    else
                    {
                        for (int j = 0; j < graphCount; j++)
                        {
                            string[] subGraphData = graphData[j + 3 + docCount].Split("\n");
                            if (subGraphData[0] == moved.Text)
                            {
                                movedData = "\n\n" + graphData[j + 3 + docCount];
                            }
                            else if (subGraphData[0] == previous.Text)
                            {
                                previousData = graphData[j + 3 + docCount];
                            }
                            else if (movedData != "" && previousData != "")
                            {
                                break;
                            }
                        }

                        File.WriteAllText(file, Regex.Replace(File.ReadAllText(file), movedData, ""));
                        File.WriteAllText(file, Regex.Replace(File.ReadAllText(file), previousData, previousData + movedData));

                        break;
                    }
                }
            }
        }

        private void Explorer_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggedExplorer > 0)
            {
                graphEntries[draggedExplorer - 1].Location = new Point(graphEntries[draggedExplorer - 1].Location.X + e.X - xOffset, graphEntries[draggedExplorer - 1].Location.Y + e.Y - yOffset);
            }
            else if (draggedExplorer < 0)
            {
                graphs[-draggedExplorer - 1].Location = new Point(graphs[-draggedExplorer - 1].Location.X + e.X - xOffset, graphs[-draggedExplorer - 1].Location.Y + e.Y - yOffset);
            }
        }

        private int NextExplorerEntry(Button current)
        {
            for (int i = int.Parse(current.Name) + 1; i < graphEntries.Count; i++)
            {
                int x = graphEntries[i].Location.X;
                if (x < current.Location.X)
                {
                    return -1;
                }

                if(x == current.Location.X)
                {
                    return i;
                }
            }

            return -1;
        }

        private List<Button> EntryOrder(Button current)
        {
            supergraph = null;
            int start = int.Parse(current.Name);
            List<Button> above = new List<Button>();
            List<Button> below = new List<Button>();

            for (int i = start; i >= 0; i--)
            {
                if (graphEntries[i].Location.X < current.Location.X)
                {
                    supergraph = graphEntries[i].Text;
                    break;
                }

                if (graphEntries[i].Location.X == current.Location.X)
                {
                    above.Insert(0, graphEntries[i]);
                }
            }

            for (int i = start + 1; i < graphEntries.Count; i++)
            {
                if (graphEntries[i].Location.X < current.Location.X)
                {
                    break;
                }

                if (graphEntries[i].Location.X == current.Location.X)
                {
                    below.Add(graphEntries[i]);
                }
            }

            above.AddRange(below);
            return above;
        }

        private List<Button> RearrangedOrder()
        {
            rearranged = order.Select(i => i).ToList();
            for (int i = 0; i < rearranged.Count; i++)
            {
                Button current = rearranged[i];
                int j = i - 1;
                while(j >= 0 && j < rearranged.Count - 1 && current.Location.Y < rearranged[j].Location.Y)
                {
                    rearranged[j + 1] = rearranged[j];
                    j--;
                }
                rearranged[j + 1] = current;
            }

            return rearranged;
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
