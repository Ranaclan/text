
namespace text
{
    partial class Graph
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.graphContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subgraphContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.supergraphButton = new System.Windows.Forms.Button();
            this.graphContextMenu.SuspendLayout();
            this.documentContextMenu.SuspendLayout();
            this.subgraphContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // graphContextMenu
            // 
            this.graphContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem});
            this.graphContextMenu.Name = "graphContextMenu";
            this.graphContextMenu.Size = new System.Drawing.Size(99, 26);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.documentToolStripMenuItem,
            this.graphToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // documentToolStripMenuItem
            // 
            this.documentToolStripMenuItem.Name = "documentToolStripMenuItem";
            this.documentToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.documentToolStripMenuItem.Text = "Document";
            this.documentToolStripMenuItem.Click += new System.EventHandler(this.AddDocument);
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.graphToolStripMenuItem.Text = "Graph";
            this.graphToolStripMenuItem.Click += new System.EventHandler(this.AddGraph);
            // 
            // documentContextMenu
            // 
            this.documentContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.duplicateToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.documentContextMenu.Name = "documentContextMenu";
            this.documentContextMenu.Size = new System.Drawing.Size(125, 136);
            this.documentContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuOpen);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.Document_ContextMenuOpen);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.duplicateToolStripMenuItem.Text = "Duplicate";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // subgraphContextMenu
            // 
            this.subgraphContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.renameToolStripMenuItem1,
            this.copyToolStripMenuItem1,
            this.duplicateToolStripMenuItem1,
            this.cutToolStripMenuItem1,
            this.deleteToolStripMenuItem1});
            this.subgraphContextMenu.Name = "subgraphContextMenu";
            this.subgraphContextMenu.Size = new System.Drawing.Size(125, 136);
            this.subgraphContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuOpen);
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.openToolStripMenuItem1.Text = "Open";
            // 
            // renameToolStripMenuItem1
            // 
            this.renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
            this.renameToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.renameToolStripMenuItem1.Text = "Rename";
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            this.copyToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.copyToolStripMenuItem1.Text = "Copy";
            // 
            // duplicateToolStripMenuItem1
            // 
            this.duplicateToolStripMenuItem1.Name = "duplicateToolStripMenuItem1";
            this.duplicateToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.duplicateToolStripMenuItem1.Text = "Duplicate";
            // 
            // cutToolStripMenuItem1
            // 
            this.cutToolStripMenuItem1.Name = "cutToolStripMenuItem1";
            this.cutToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.cutToolStripMenuItem1.Text = "Cut";
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            // 
            // supergraphButton
            // 
            this.supergraphButton.Location = new System.Drawing.Point(12, 12);
            this.supergraphButton.Name = "supergraphButton";
            this.supergraphButton.Size = new System.Drawing.Size(75, 23);
            this.supergraphButton.TabIndex = 3;
            this.supergraphButton.UseVisualStyleBackColor = true;
            this.supergraphButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Explorer_MouseDown);
            this.supergraphButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Explorer_MouseUp);
            // 
            // Graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ContextMenuStrip = this.graphContextMenu;
            this.Controls.Add(this.supergraphButton);
            this.KeyPreview = true;
            this.Name = "Graph";
            this.Text = "Graph";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Graph_KeyDown);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Graph_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Graph_MouseDown);
            this.graphContextMenu.ResumeLayout(false);
            this.documentContextMenu.ResumeLayout(false);
            this.subgraphContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip graphContextMenu;
        private System.Windows.Forms.ContextMenuStrip documentContextMenu;
        private System.Windows.Forms.ContextMenuStrip subgraphContextMenu;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.Button supergraphButton;
    }
}