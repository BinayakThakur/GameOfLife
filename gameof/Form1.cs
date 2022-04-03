using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gameof
{
    public partial class Form1 : Form
    {
        int num = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            num = Convert.ToInt32(textBox1.Text);
            int ax = 60;
            int y = 60;
            int[,] state = new int[num+1,num+1];
            
            int locx = 0;
            int locy = 0;
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    Button x = new Button();
                    x.Height = 50;
                    x.Width = 50;
                    x.BackColor = Color.FloralWhite;
                    x.Location = new Point(ax, y);
                    x.Name = i + " " + j;
                    ax = ax + 50;
                    x.ForeColor = Color.FloralWhite;
                    x.Text = "0";
                    
                    x.Click += (s, e) =>
                    {
                        int a = locx;
                        int b = locy;
                        String te = x.Text;
                        if (te.Equals("0"))
                        {
                            
                            x.BackColor = Color.Black;
                            x.ForeColor = Color.Black;
                            x.Text = "1";
                            state[locx, locy] = 1;
                           
                        }
                        else
                        {
                            x.BackColor = Color.FloralWhite;
                            x.ForeColor = Color.FloralWhite;
                            x.Text = "0";
                            
                            
                        }
                    };


                    Controls.Add(x);
                    locx++;
                }
                locy++;locx = 0;
                y = y + 50; ax = 60;
            }
           



            Button sim = new Button();
            sim.Height = 30;
            sim.Width = 100;
            sim.Text = "Simulate";
            sim.Location = new Point(550, 12);
            Controls.Add(sim);

            sim.Click += (s, e) => {
                
                    int n = 0;
                    int col = 0;
                    int row = 0;
                    foreach (Control ctrl in this.Controls)
                    {


                        if (ctrl is Button)
                            if (ctrl.Text == "1" || ctrl.Text == "0")
                            {

                                state[col, row] = Convert.ToInt32(ctrl.Text);
                                row++;
                                if (row == num) { col++; row = 0; }
                                if (col == num) { col = 0; }
                            }

                    }



                timer2.Enabled = true;
                timer2.Interval = 1100;
                timer2.Tick += (s, e) =>
                {
                    state = GameOfLife(state);
                    foreach (Control ctrl in this.Controls)
                    {


                        if (ctrl is Button)
                            if (ctrl.Text == "1" || ctrl.Text == "0")
                            {
                                if (state[col, row] == 0) { ctrl.BackColor = Color.FloralWhite;
                                    ctrl.ForeColor = Color.FloralWhite;
                                }
                                if (state[col, row] == 1) { ctrl.BackColor = Color.Black; ctrl.ForeColor = Color.Black ; }
                                row++;
                                if (row == num) { col++; row = 0; }
                                if (col == num) { col = 0; }
                            }
                    }





                };
            };

        }

        public int[,] GameOfLife(int[,] board)
        {
            int m = board.GetLength(0), n = board.GetLength(1);
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    int lives = 0;
                    for (int k = Math.Max(0, i - 1); k < Math.Min(i + 2, m); k++)
                        for (int l = Math.Max(0, j - 1); l < Math.Min(j + 2, n); l++)
                            if (k != i || l != j) lives += board[k, l] % 2;
                    if (board[i, j] == 0 && lives == 3) board[i, j] = 2;
                    else if (board[i, j] == 1 && (lives < 2 || lives > 3)) board[i, j] = 3;
                }
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    if (board[i, j] > 1) board[i, j] = 3 - board[i, j];
            return board;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
      
}
