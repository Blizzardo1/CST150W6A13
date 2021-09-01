using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CST150W6A13
{
    public partial class Form1 : Form
    {
        private Game game;

        public Form1()
        {
            InitializeComponent();
            aboutToolStripMenuItem.Text = $"About {Text}...";
            game = new Game(this);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showHighscoresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
