using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace text
{
    public partial class Graph : Form
    {
        //documents
        private Button[] documents;
        private int docCount = 1;
        private int dragged = -1;

        public Graph()
        {
            InitializeComponent();
            InitialiseDocuments();
        }

        private void InitialiseDocuments()
        {
            documents = new Button[docCount];

            for (int i = 0; i < docCount; i++)
            {
                documents[i] = new Button();
                Controls.Add(documents[i]);
                documents[i].Name = i.ToString();
                documents[i].Text = "a";
                documents[i].Size = new Size(50, 70);
            }
        }

        private void Document_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                dragged = 0;
                System.Diagnostics.Debug.WriteLine(dragged);
            }
        }

        private void Document_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragged = -1;
                System.Diagnostics.Debug.WriteLine(sender.ToString());
            }
        }

        private void Document_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragged == 0)
            {
                documents[dragged].Location = e.Location;
            }
        }
    }
}
