using Sachmatai;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M.D_Sachmatu_projektas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Chess chess = new Chess();
        int x = 60, y = 60;
        Button[,] mygtukas = new Button[8, 8];
        string[,] poz = new string[8, 8];
        string[,] restart = new string[8, 8];
        private void Pridejimas()
        {

            //Pridedame mygtukus ir nustatome ju dydzius ir pozicijas
            for (int i = 0; i < chess.Ilgis; i++)
            {
                for (int j = 0; j < chess.Aukstis; j++)
                {
                    mygtukas[i, j] = new Button();
                    this.Controls.Add(mygtukas[i, j]);
                    mygtukas[i, j].Size = new Size(60, 60);
                    mygtukas[i, j].Location = new Point(x * i, y * j);
                    mygtukas[i, j].Click += Paspaudimas_Button_Click;
                    mygtukas[i, j].Tag = new Point(i, j);
                }
            }
        }
        private void Pavadinimai()
        {
            //Ant mygtuku uzrasomas tekstas
            for (int i = 0; i < chess.Ilgis; i++)
            {
                for (int j = 0; j < chess.Aukstis; j++)
                {
                    poz = chess.Begin();
                    restart[i, j] = poz[i, j];
                    mygtukas[i, j].Text = poz[i, j];
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Pridejimas();
            Pavadinimai();
            for (int i = 0; i < chess.Ilgis; i++)
            {
                for (int j = 0; j < chess.Aukstis; j++)
                {
                    if (!string.IsNullOrEmpty(poz[i, j]) && (poz[i, j].Substring(0, 1) == "w" || poz[i, j].Substring(0, 1) == "W"))
                    {
                        mygtukas[i, j].BackColor = Color.White;
                        mygtukas[i, j].ForeColor = Color.Black;
                    }
                    if (!string.IsNullOrEmpty(poz[i, j]) && (poz[i, j].Substring(0, 1) == "b" || poz[i, j].Substring(0, 1) == "B"))
                    {
                        mygtukas[i, j].BackColor = Color.Black;
                        mygtukas[i, j].ForeColor = Color.White;
                    }
                    if (string.IsNullOrEmpty(poz[i, j]))
                        mygtukas[i, j].BackColor = Color.Gray;
                    if (!string.IsNullOrWhiteSpace(poz[i, j]) && poz[i, j].Substring(0, 1) == "B")
                    {
                        mygtukas[i, j].Text = mygtukas[i, j].Text.Replace("B", "b");
                    }
                }
            }
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            label1.Text = "Ejimo skaicius = 1";
            label2.Text = "";
            label3.Text = "Baltuju Ejimas";
            chess.count = 0;
            chess.rok = 0;
            chess.rok2 = 0;
            for(int i = 0; i < chess.Ilgis; i++)
            {
                for(int j = 0; j < chess.Aukstis; j++)
                {
                    mygtukas[i, j].Text = restart[i, j];
                    poz[i, j] = restart[i, j];
                    if (string.IsNullOrEmpty(poz[i, j]))
                        mygtukas[i, j].BackColor = Color.Gray;
                    if (!string.IsNullOrEmpty(poz[i, j]) && (poz[i, j].Substring(0, 1) == "w" || poz[i, j].Substring(0, 1) == "W"))
                    {
                        mygtukas[i, j].BackColor = Color.White;
                        mygtukas[i, j].ForeColor = Color.Black;
                    }
                    if (!string.IsNullOrEmpty(poz[i, j]) && (poz[i, j].Substring(0, 1) == "b" || poz[i, j].Substring(0, 1) == "B"))
                    {
                        mygtukas[i, j].BackColor = Color.Black;
                        mygtukas[i, j].ForeColor = Color.White;
                    }
                    if (!string.IsNullOrWhiteSpace(poz[i, j]) && poz[i, j].Substring(0, 1) == "B")
                    {
                        mygtukas[i, j].Text = mygtukas[i, j].Text.Replace("B", "b");
                    }
                }
            }
        }

        private void Paspaudimas_Button_Click(object sender, EventArgs e)
        {
            label1.Text = "Ejimo skaicius";
            Button Paspaustas = (Button) sender;
            Point vieta = (Point)Paspaustas.Tag;
            int x = vieta.X;
            int y = vieta.Y;
            if (chess.count % 2 == 0 && Paspaustas.Text == "Laimeti")
            {
                label2.Text = "LAIMEJO BALTIEJI";
            }
            if (chess.count % 2 != 0 && Paspaustas.Text == "Laimeti")
            {
                label2.Text = "LAIMEJO JUODIEJI";
            }
            chess.Ejimas(x, y, Paspaustas.Text);
            /*if (label2.Text == "LAIMEJO BALTIEJI" || label2.Text == "LAIMEJO JUODIEJI")
            {
                for (int i = 0; i < chess.Ilgis; i++)
                {
                    for (int j = 0; j < chess.Aukstis; j++)
                    {
                        mygtukas[i, j].Text = mygtukas[i, j].Text.ToLower();
                    }
                }
            }*/
            for (int i=0; i<chess.Ilgis; i++)
            {
                for(int j=0; j<chess.Aukstis; j++)
                {
                    if (!string.IsNullOrEmpty(poz[i, j]) && (poz[i, j].Substring(0, 1) == "w" || poz[i, j].Substring(0, 1) == "W"))
                    {
                        mygtukas[i, j].BackColor = Color.White;
                        mygtukas[i, j].ForeColor = Color.Black;
                    }
                    if (!string.IsNullOrEmpty(poz[i, j]) && (poz[i, j].Substring(0, 1) == "b" || poz[i, j].Substring(0, 1) == "B"))
                    {
                        mygtukas[i, j].BackColor = Color.Black;
                        mygtukas[i, j].ForeColor = Color.White;
                    }
                    if (!string.IsNullOrEmpty(poz[i, j]) && poz[i, j] == "Eiti")
                    {
                        mygtukas[i, j].BackColor = Color.Green;
                        mygtukas[i, j].ForeColor = Color.Black;
                    }
                    if (!string.IsNullOrEmpty(poz[i, j]) && poz[i, j] == "Pulti")
                    {
                        mygtukas[i, j].BackColor = Color.Red;
                        mygtukas[i, j].ForeColor = Color.Black;
                    }
                    if (!string.IsNullOrEmpty(poz[i, j]) && poz[i, j] == "Laimeti")
                    {
                        mygtukas[i, j].BackColor = Color.Yellow;
                        mygtukas[i, j].ForeColor = Color.Red;
                    }
                    if (!string.IsNullOrEmpty(poz[i, j]) && poz[i, j] == "WT/ Rokiruote")
                    {
                        mygtukas[i, j].BackColor = Color.Blue;
                        mygtukas[i, j].ForeColor = Color.Red;
                    }
                    if (!string.IsNullOrEmpty(poz[i, j]) && poz[i, j] == "WI/ Rokiruote")
                    {
                        mygtukas[i, j].BackColor = Color.Blue;
                        mygtukas[i, j].ForeColor = Color.Red;
                    }
                    if (!string.IsNullOrEmpty(poz[i, j]) && poz[i, j] == "BT/ Rokiruote")
                    {
                        mygtukas[i, j].BackColor = Color.Blue;
                        mygtukas[i, j].ForeColor = Color.Red;
                    }
                    if (!string.IsNullOrEmpty(poz[i, j]) && poz[i, j] == "BI/ Rokiruote")
                    {
                        mygtukas[i, j].BackColor = Color.Blue;
                        mygtukas[i, j].ForeColor = Color.Red;
                    }
                    if (string.IsNullOrEmpty(poz[i, j]))
                        mygtukas[i, j].BackColor = Color.Gray;
                    mygtukas[i, j].Text = poz[i, j];
                    //Nustato ar yra juoduju ejimas:
                    if (!string.IsNullOrWhiteSpace(poz[i, j]) && poz[i, j].Substring(0, 1) == "b" && chess.count % 2 != 0)
                    {
                        mygtukas[i, j].Text = mygtukas[i, j].Text.Replace("b", "B");
                        label3.Text = "Juoduju Ejimas";
                    }
                    if (!string.IsNullOrWhiteSpace(poz[i, j]) && poz[i, j].Substring(0, 1) == "B" && chess.count % 2 == 0)
                    {
                        mygtukas[i, j].Text = mygtukas[i, j].Text.Replace("B", "b");
                        label3.Text = "Baltuju Ejimas";
                    }
                        //Nustato ar yra baltuju ejimas:
                    if (!string.IsNullOrWhiteSpace(poz[i, j]) && poz[i, j].Substring(0, 1) == "w" && chess.count % 2 == 0)
                    {
                        mygtukas[i, j].Text = mygtukas[i, j].Text.Replace("w", "W");
                        label3.Text = "Baltuju Ejimas";
                    }
                    if (!string.IsNullOrWhiteSpace(poz[i, j]) && poz[i, j].Substring(0, 1) == "W" && chess.count % 2 != 0)
                    {
                        mygtukas[i, j].Text = mygtukas[i, j].Text.Replace("W", "w");
                        label3.Text = "Juoduju Ejimas";
                    }
                }
            }
                
            int ej = chess.count + 1;
            label1.Text = label1.Text + " = " + ej.ToString();

        }
    }
}
