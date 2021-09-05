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

        private Player[] players;
        private Player curr;
        private int i = 0;
        private Button[,] slots = new Button[3, 3];
        private Button[,] Winners;
        private int counter = 0;

        public Form1()
        {
            InitializeComponent();
            aboutToolStripMenuItem.Text = $"About {Text}...";
            players = new Player[2] {
                new("Player 1", Marker.O, 1),
                new("Player 2", Marker.X, 2)
            };
            curr = players.First();
            Setup();
        }

        private void Setup()
        {
            counter = 0;
            slots = new Button[3, 3] {
                { button1, button2, button3 },
                { button4, button5, button6 },
                { button7, button8, button9 }
            };
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    slots[x, y].Enabled = true;
                    slots[x, y].Tag = 0;
                    slots[x, y].BackColor = Color.FromKnownColor(KnownColor.Control);
                    slots[x, y].Text = "";
                }
            }

            UpdateWinners();
        }
        private void UpdateWinners()
        {
            Winners = new Button[8, 3] {
                { slots[0, 0], slots[0, 1], slots[0, 2]},
                { slots[1, 0], slots[1, 1], slots[1, 2]},
                { slots[2, 0], slots[2, 1], slots[2, 2]},
                { slots[0, 0], slots[1, 0], slots[2, 0]},
                { slots[0, 1], slots[1, 1], slots[2, 1]},
                { slots[0, 2], slots[1, 2], slots[2, 2]},
                { slots[0, 0], slots[1, 1], slots[2, 2]},
                { slots[0, 2], slots[1, 1], slots[2, 0]}
            };
        }

        private void Turn()
        {
            i++;
            i %= 2;
            curr = players[i];
        }

        private bool CheckGame()
        {
            for (int i = 0; i < 8; i++)
            {
                if (((int)Winners[i, 0].Tag == curr.PlayerId) && ((int)Winners[i, 1].Tag == curr.PlayerId) && ((int)Winners[i, 2].Tag == curr.PlayerId))
                {
                    Winners[i, 0].BackColor = Color.Green;
                    Winners[i, 1].BackColor = Color.Green;
                    Winners[i, 2].BackColor = Color.Green;
                    MessageBox.Show($"{curr.PlayerName} Wins!");
                    Disable();
                    return true;
                }
                else
                {
                    if (counter >= 8)
                    {
                        MessageBox.Show("Stalemate!");
                        Disable();
                        return true;
                    }
                }
            }
            counter++;
            return false;
        }

        private void Disable()
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    slots[x, y].Enabled = false;
                }
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setup();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showHighscoresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button b = (sender as Button);
            if ((int)b.Tag > 0) return;
            b.Text = curr.PlayerMarker.ToString();
            b.Tag = curr.PlayerId;
            if (CheckGame()) return;
            Turn();
            
        }
    }
}
