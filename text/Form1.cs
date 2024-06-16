﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace text
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Save()
        {
            SaveFileDialog dialogue = new SaveFileDialog();
            dialogue.Filter = "Rich Text File (*.rtf)|*.rtf|Plain Text File (*.txt)|*.txt"; //Don't include space when when typing *.ext. Because space is treated as extension
            dialogue.DefaultExt = "*.rtf";
            dialogue.FilterIndex = 1;
            dialogue.Title = "Save As";

            if (dialogue.ShowDialog() == DialogResult.OK)
            {
                if (dialogue.FilterIndex == 1)
                {
                    text.SaveFile(dialogue.FileName, RichTextBoxStreamType.RichText);
                }
                else
                {
                    text.SaveFile(dialogue.FileName, RichTextBoxStreamType.PlainText);
                }
            }
        }

        private void LoadFile()
        {

            OpenFileDialog dialogue = new OpenFileDialog();
            dialogue.Filter = "Rich Text File (*.rtf)|*.rtf| Plain Text File (*.txt)|*.txt";
            dialogue.FilterIndex = 1;
            dialogue.Title = "Load";

            RichTextBoxStreamType type;
            if (dialogue.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(dialogue.FileName))
            {
                if (dialogue.FilterIndex == 1)
                {
                    type = RichTextBoxStreamType.RichText;
                }
                else
                {
                    type = RichTextBoxStreamType.PlainText;
                }

                try
                {
                    text.LoadFile(dialogue.FileName, type);
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("In use");
                }
            }
        }

        private void text_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
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

            if (key == (Keys.B | Keys.Control))
            {
                Bold();
            }

            if (key == (Keys.I | Keys.Control))
            {
                Italic();
            }

            if (key == (Keys.U | Keys.Control))
            {
                Underline();
            }

            if (key == (Keys.OemMinus | Keys.Control))
            {
                Strike();
            }

            if (key == (Keys.A | Keys.Control))
            {
                text.SelectAll();
            }

            if (key == (Keys.Z | Keys.Control))
            {
            }

            return base.ProcessCmdKey(ref msg, key);
        }

        private void Bold()
        {
            Font selected = text.SelectionFont;
            if (selected != null)
            {
                text.SelectionFont = new Font(selected, selected.Style ^ FontStyle.Bold);
            }
        }

        private void Italic()
        {
            Font selected = text.SelectionFont;
            if (selected != null)
            {
                text.SelectionFont = new Font(selected, selected.Style ^ FontStyle.Italic);
            }
        }

        private void Underline()
        {
            Font selected = text.SelectionFont;
            if (selected != null)
            {
                text.SelectionFont = new Font(selected, selected.Style ^ FontStyle.Underline);
            }
        }

        private void Strike()
        {
            Font selected = text.SelectionFont;
            if (selected != null)
            {
                text.SelectionFont = new Font(selected, selected.Style ^ FontStyle.Strikeout);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
