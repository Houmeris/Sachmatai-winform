using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sachmatai
{
    public class Chess
    {
        public int Ilgis { get; set; }
        public int Aukstis { get; set; }
        public string[,] poz = new string[8, 8];
        public string[,] tarpinis = new string[8, 8];
        public string dabartinis;
        public int pozx = 0;
        public int pozy = 0;
        public int count = 0;
        public int rok = 0;
        public int rok2 = 0;
        public Chess()
        {
            count = 0;
            Ilgis = 8;
            Aukstis = 8;
        }

        public string[,] Begin()
        {
            for (int i = 0; i < Ilgis; i++)
            {
                poz[i, 6] = "W: Pawn";
                poz[i, 1] = "B: Pawn";
            }
            poz[7, 7] = "W: Rook";
            poz[0, 7] = "W: Rook";
            poz[1, 7] = "W: Knight";
            poz[6, 7] = "W: Knight";
            poz[2, 7] = "W: Bishop";
            poz[5, 7] = "W: Bishop";
            poz[3, 7] = "W: Queen";
            poz[4, 7] = "W: King";
            poz[0, 0] = "B: Rook";
            poz[7, 0] = "B: Rook";
            poz[1, 0] = "B: Knight";
            poz[6, 0] = "B: Knight";
            poz[2, 0] = "B: Bishop";
            poz[5, 0] = "B: Bishop";
            poz[3, 0] = "B: Queen";
            poz[4, 0] = "B: King";
            return poz;
        }
        private void Load()
        {
            for (int i = 0; i < Ilgis; i++)
            {
                for (int j = 0; j < Aukstis; j++)
                {
                    poz[i, j] = tarpinis[i, j];
                }
            }
        }
        private void Save()
        {
            for (int i = 0; i < Ilgis; i++)
            {
                for (int j = 0; j < Aukstis; j++)
                {
                    tarpinis[i, j] = poz[i, j];
                    if (!string.IsNullOrWhiteSpace(poz[i, j]))
                    {
                        //poz[i, j] = "taviskis";
                        poz[i, j] = poz[i, j].ToLower();
                    }
                }
            }
        }
        public void Ejimas (int x, int y, string piece)
        {
            switch(piece)
            {
                //BALTI
                case "W: Pawn":
                    try
                    {
                        dabartinis = piece;
                        Save();
                        pozx = x;
                        pozy = y;
                        poz[x, y] = "Cancel";
                        if (y == 6)
                        {
                            if (string.IsNullOrWhiteSpace(poz[x, y-2]) && string.IsNullOrWhiteSpace(poz[x, y - 1]))
                            {
                                poz[x, y - 2] = "Eiti";
                            }
                            
                        }
                        //poz[x, y - 1] != poz[x, y - 2].ToLower()
                        if (y > 0 && string.IsNullOrWhiteSpace(poz[x, y - 1]))
                        {
                            poz[x, y - 1] = "Eiti";
                        }
                        if (x < 7 && y > 0 && !string.IsNullOrWhiteSpace(poz[x + 1, y - 1]) && poz[x + 1, y - 1] == "b: king")
                        {
                            poz[x + 1, y - 1] = "Laimeti";
                        }
                        if (x > 0 && y > 1 && !string.IsNullOrWhiteSpace(poz[x - 1, y - 1]) && poz[x - 1, y - 1] == "b: king")
                        {
                            poz[x - 1, y - 1] = "Laimeti";
                        }
                        if (x < 7 && y > 0 && !string.IsNullOrWhiteSpace(poz[x+1, y - 1]) && poz[x+1, y-1].Substring(0,1) == "b")
                        {
                            poz[x + 1, y - 1] = "Pulti";
                        }
                        if (x > 0 && y > 1 && !string.IsNullOrWhiteSpace(poz[x - 1, y - 1]) && poz[x - 1, y - 1].Substring(0, 1) == "b")
                        {
                            poz[x - 1, y - 1] = "Pulti";
                        }
                    }
                    catch
                    {}
                    break;
                case "W: Rook":
                    dabartinis = piece;
                    Save();
                    pozx = x;
                    pozy = y;
                    poz[x, y] = "Cancel";
                    //Zemyn
                    if(rok < 1 && (poz[7, 7] == string.Empty || poz[0, 7] == string.Empty))
                    {
                        rok++;
                    }
                    if (rok < 1 && poz[4, 7] == string.Empty)
                    {
                        rok++;
                    }
                    for (int i = y; i < Ilgis;)
                    {
                        if (string.IsNullOrWhiteSpace(poz[x, i]))
                        {
                            poz[x, i] = "Eiti";
                        }
                        if (i < Aukstis-1)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x, i]))
                        {
                            if (poz[x, i] == "b: king")
                            {
                                poz[x, i] = "Laimeti";
                                break;
                            }
                            if (poz[x, i]. Substring(0, 1) == "b")
                            {
                                poz[x, i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    //I Virsu
                    for (int i = y; i < Ilgis;)
                    {
                        if (string.IsNullOrWhiteSpace(poz[x, i]))
                        {
                            poz[x, i] = "Eiti";
                        }
                        if(i > 0)
                            i--;
                        if (!string.IsNullOrWhiteSpace(poz[x, i]))
                        {
                            if (poz[x, i] == "b: king")
                            {
                                poz[x, i] = "Laimeti";
                                break;
                            }
                            if (poz[x, i].Substring(0, 1) == "b")
                            {
                                poz[x, i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    //Kaire
                    for (int i = x; i < Aukstis;)
                    {
                        if (string.IsNullOrWhiteSpace(poz[i, y]))
                        {
                            poz[i, y] = "Eiti";
                        }
                        if(i > 0)
                            i--;
                        if (!string.IsNullOrWhiteSpace(poz[i, y]))
                        {
                            if (poz[i, y] == "b: king")
                            {
                                poz[i, y] = "Laimeti";
                                break;
                            }
                            if (rok < 1 && y == 7 && poz[i, y] == "w: king")
                            {
                                poz[i, y] = "WT/ Rokiruote";
                                break;
                            }
                            if (poz[i, y].Substring(0, 1) == "b")
                            {
                                poz[i, y] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    //Desine
                    for (int i = x; i < Aukstis;)
                    {
                        if (string.IsNullOrWhiteSpace(poz[i, y]))
                        {
                            poz[i, y] = "Eiti";
                        }
                        if (i < Aukstis-1)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[i, y]))
                        {
                            if (poz[i, y] == "b: king")
                            {
                                poz[i, y] = "Laimeti";
                                break;
                            }
                            if (rok < 1 && y == 7 && poz[i, y] == "w: king")
                            {
                                poz[i, y] = "WI/ Rokiruote";
                                break;
                            }
                            if (poz[i, y].Substring(0, 1) == "b")
                            {
                                poz[i, y] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }

                    poz[x, y] = "Cancel";
                    break;
                case "W: Bishop":
                    dabartinis = piece;
                    Save();
                    pozx = x;
                    pozy = y;
                    poz[x, y] = "Cancel";
                    for (int i = 0; i < Ilgis;)
                    {
                        if (x + i < Ilgis && y + i < Aukstis && string.IsNullOrWhiteSpace(poz[x + i, y + i]))
                        {
                            poz[x + i, y + i] = "Eiti";
                        }
                        if(x + i < Ilgis - 1 && y + i < Ilgis - 1)
                            i++;
                        if(!string.IsNullOrWhiteSpace(poz[x + i, y + i]))
                        {
                            if (poz[x + i, y + i] == "b: king")
                            {
                                poz[x + i, y + i] = "Laimeti";
                                break;
                            }
                            if (poz[x + i, y + i].Substring(0,1) == "b")
                            {
                                poz[x + i, y + i] = "Pulti";
                                break;
                            }
                            break;
                        }

                    }
                    for (int i = 0; i < Ilgis;)
                    {
                        if (x + i < Ilgis && y - i > -1 && string.IsNullOrWhiteSpace(poz[x + i, y - i]))
                        {
                            poz[x + i, y - i] = "Eiti";
                        }
                        if (x + i < Ilgis - 1 && y - i > 0)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x + i, y - i]))
                        {
                            if (poz[x + i, y - i] == "b: king")
                            {
                                poz[x + i, y - i] = "Laimeti";
                                break;
                            }
                            if (poz[x + i, y - i].Substring(0, 1) == "b")
                            {
                                poz[x + i, y - i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = 0; i < Ilgis;)
                    {
                        if (x - i > -1 && y + i < Aukstis && string.IsNullOrWhiteSpace(poz[x - i, y + i]))
                        {
                            poz[x - i, y + i] = "Eiti";
                        }
                        if (x - i > 0 && y + i < Ilgis - 1)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x - i, y + i]))
                        {
                            if (poz[x - i, y + i] == "b: king")
                            {
                                poz[x - i, y + i] = "Laimeti";
                                break;
                            }
                            if (poz[x - i, y + i].Substring(0, 1) == "b")
                            {
                                poz[x - i, y + i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = 0; i < Ilgis;)
                    {
                        if (x - i > -1 && y - i > -1 && string.IsNullOrWhiteSpace(poz[x - i, y - i]))
                        {
                            poz[x - i, y - i] = "Eiti";
                        }
                        if (x - i > 0 && y - i > 0)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x - i, y - i]))
                        {
                            if (poz[x - i, y - i] == "b: king")
                            {
                                poz[x - i, y - i] = "Laimeti";
                                break;
                            }
                            if (poz[x - i, y - i].Substring(0, 1) == "b")
                            {
                                poz[x - i, y - i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    poz[x, y] = "Cancel";
                    break;
                case "W: Knight":
                    dabartinis = piece;
                    Save();
                    pozx = x;
                    pozy = y;
                    poz[x, y] = "Cancel";
                    if (x < 7 && y < 6 && string.IsNullOrWhiteSpace(poz[x + 1, y + 2]))
                        poz[x + 1, y + 2] = "Eiti";
                    if (x < 7 && y < 6 && poz[x + 1, y + 2] == "b: king")
                        poz[x + 1, y + 2] = "Laimeti";
                    if (x < 7 && y < 6 && poz[x + 1, y + 2].Substring(0, 1) == "b")
                        poz[x + 1, y + 2] = "Pulti";
                    if (x < 7 && y > 1 && string.IsNullOrWhiteSpace(poz[x + 1, y - 2]))
                        poz[x + 1, y - 2] = "Eiti";
                    if (x < 7 && y > 1 && poz[x + 1, y - 2] == "b: king")
                        poz[x + 1, y - 2] = "Laimeti";
                    if (x < 7 && y > 1 && poz[x + 1, y - 2].Substring(0, 1) == "b")
                        poz[x + 1, y - 2] = "Pulti";
                    if (x < 6 && y < 7 && string.IsNullOrWhiteSpace(poz[x + 2, y + 1]))
                        poz[x + 2, y + 1] = "Eiti";
                    if (x < 6 && y < 7 && poz[x + 2, y + 1] == "b: king")
                        poz[x + 2, y + 1] = "Laimeti";
                    if (x < 6 && y < 7 && poz[x + 2, y + 1].Substring(0, 1) == "b")
                        poz[x + 2, y + 1] = "Pulti";
                    if (x < 6 && y > 0 && string.IsNullOrWhiteSpace(poz[x + 2, y - 1]))
                        poz[x + 2, y - 1] = "Eiti";
                    if (x < 6 && y > 0 && poz[x + 2, y - 1] == "b: king")
                        poz[x + 2, y - 1] = "Laimeti";
                    if (x < 6 && y > 0 && poz[x + 2, y - 1].Substring(0, 1) == "b")
                        poz[x + 2, y - 1] = "Pulti";
                    if (x > 0 && y < 6 && string.IsNullOrWhiteSpace(poz[x - 1, y + 2]))
                        poz[x - 1, y + 2] = "Eiti";
                    if (x > 0 && y < 6 && poz[x - 1, y + 2] == "b: king")
                        poz[x - 1, y + 2] = "Laimeti";
                    if (x > 0 && y < 6 && poz[x - 1, y + 2].Substring(0, 1) == "b")
                        poz[x - 1, y + 2] = "Pulti";
                    if (x > 0 && y > 1 && string.IsNullOrWhiteSpace(poz[x - 1, y - 2]))
                        poz[x - 1, y - 2] = "Eiti";
                    if (x > 0 && y > 1 && poz[x - 1, y - 2] == "b: king")
                        poz[x - 1, y - 2] = "Laimeti";
                    if (x > 0 && y > 1 && poz[x - 1, y - 2].Substring(0, 1) == "b")
                        poz[x - 1, y - 2] = "Pulti";
                    if (x > 1 && y < 7 && string.IsNullOrWhiteSpace(poz[x - 2, y + 1]))
                        poz[x - 2, y + 1] = "Eiti";
                    if (x > 1 && y < 7 && poz[x - 2, y + 1] == "b: king")
                        poz[x - 2, y + 1] = "Laimeti";
                    if (x > 1 && y < 7 && poz[x - 2, y + 1].Substring(0, 1) == "b")
                        poz[x - 2, y + 1] = "Pulti";
                    if (x > 1 && y > 0 && string.IsNullOrWhiteSpace(poz[x - 2, y - 1])) 
                         poz[x - 2, y - 1] = "Eiti";
                    if (x > 1 && y > 0 && poz[x - 2, y - 1] == "b: king")
                        poz[x - 2, y - 1] = "Laimeti";
                    if (x > 1 && y > 0 && poz[x - 2, y - 1].Substring(0, 1) == "b")
                        poz[x - 2, y - 1] = "Pulti";
                    break;
                case "W: Queen":
                    dabartinis = piece;
                    Save();
                    pozx = x;
                    pozy = y;
                    poz[x, y] = "Cancel";
                    for (int i = y; i < Ilgis;)
                    {
                        if (string.IsNullOrWhiteSpace(poz[x, i]))
                        {
                            poz[x, i] = "Eiti";
                        }
                        if (i < Aukstis - 1)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x, i]))
                        {
                            if (poz[x, i] == "b: king")
                            {
                                poz[x, i] = "Laimeti";
                                break;
                            }
                            if (poz[x, i].Substring(0, 1) == "b")
                            {
                                poz[x, i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = y; i < Ilgis;)
                    {
                        if (string.IsNullOrWhiteSpace(poz[x, i]))
                        {
                            poz[x, i] = "Eiti";
                        }
                        if (i > 0)
                            i--;
                        if (!string.IsNullOrWhiteSpace(poz[x, i]))
                        {
                            if (poz[x, i] == "b: king")
                            {
                                poz[x, i] = "Laimeti";
                                break;
                            }
                            if (poz[x, i].Substring(0, 1) == "b")
                            {
                                poz[x, i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = x; i < Aukstis;)
                    {
                        if (string.IsNullOrWhiteSpace(poz[i, y]))
                        {
                            poz[i, y] = "Eiti";
                        }
                        if (i > 0)
                            i--;
                        if (!string.IsNullOrWhiteSpace(poz[i, y]))
                        {
                            if (poz[i, y] == "b: king")
                            {
                                poz[i, y] = "Laimeti";
                                break;
                            }
                            if (poz[i, y].Substring(0, 1) == "b")
                            {
                                poz[i, y] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = x; i < Aukstis;)
                    {
                        if (string.IsNullOrWhiteSpace(poz[i, y]))
                        {
                            poz[i, y] = "Eiti";
                        }
                        if (i < Aukstis - 1)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[i, y]))
                        {
                            if (poz[i, y] == "b: king")
                            {
                                poz[i, y] = "Laimeti";
                                break;
                            }
                            if (poz[i, y].Substring(0, 1) == "b")
                            {
                                poz[i, y] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    poz[x, y] = "Cancel";
                    for (int i = 0; i < Ilgis;)
                    {
                        if (x + i < Ilgis && y + i < Aukstis && string.IsNullOrWhiteSpace(poz[x + i, y + i]))
                        {
                            poz[x + i, y + i] = "Eiti";
                        }
                        if (x + i < Ilgis - 1 && y + i < Ilgis - 1)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x + i, y + i]))
                        {
                            if (poz[x + i, y + i] == "b: king")
                            {
                                poz[x + i, y + i] = "Laimeti";
                                break;
                            }
                            if (poz[x + i, y + i].Substring(0, 1) == "b")
                            {
                                poz[x + i, y + i] = "Pulti";
                                break;
                            }
                            break;
                        }

                    }
                    for (int i = 0; i < Ilgis;)
                    {
                        if (x + i < Ilgis && y - i > -1 && string.IsNullOrWhiteSpace(poz[x + i, y - i]))
                        {
                            poz[x + i, y - i] = "Eiti";
                        }
                        if (x + i < Ilgis - 1 && y - i > 0)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x + i, y - i]))
                        {
                            if (poz[x + i, y - i] == "b: king")
                            {
                                poz[x + i, y - i] = "Laimeti";
                                break;
                            }
                            if (poz[x + i, y - i].Substring(0, 1) == "b")
                            {
                                poz[x + i, y - i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = 0; i < Ilgis;)
                    {
                        if (x - i > -1 && y + i < Aukstis && string.IsNullOrWhiteSpace(poz[x - i, y + i]))
                        {
                            poz[x - i, y + i] = "Eiti";
                        }
                        if (x - i > 0 && y + i < Ilgis - 1)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x - i, y + i]))
                        {
                            if (poz[x - i, y + i] == "b: king")
                            {
                                poz[x - i, y + i] = "Laimeti";
                                break;
                            }
                            if (poz[x - i, y + i].Substring(0, 1) == "b")
                            {
                                poz[x - i, y + i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = 0; i < Ilgis;)
                    {
                        if (x - i > -1 && y - i > -1 && string.IsNullOrWhiteSpace(poz[x - i, y - i]))
                        {
                            poz[x - i, y - i] = "Eiti";
                        }
                        if (x - i > 0 && y - i > 0)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x - i, y - i]))
                        {
                            if (poz[x - i, y - i] == "b: king")
                            {
                                poz[x - i, y - i] = "Laimeti";
                                break;
                            }
                            if (poz[x - i, y - i].Substring(0, 1) == "b")
                            {
                                poz[x - i, y - i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    poz[x, y] = "Cancel";
                    break;
                case "W: King":
                    dabartinis = piece;
                    Save();
                    pozx = x;
                    pozy = y;
                    poz[x, y] = "Cancel";
                    if (x < 7 && y < 7 && string.IsNullOrWhiteSpace(poz[x + 1, y + 1]))
                        poz[x + 1, y + 1] = "Eiti";
                    if (x < 7 && y < 7 && poz[x + 1, y + 1] == "b: king")
                        poz[x + 1, y + 1] = "Laimeti";
                    if (x < 7 && y < 7 && poz[x + 1, y + 1].Substring(0, 1) == "b")
                        poz[x + 1, y + 1] = "Pulti";
                    if (x < 7 && y > 0 && string.IsNullOrWhiteSpace(poz[x + 1, y - 1]))
                        poz[x + 1, y - 1] = "Eiti";
                    if (x < 7 && y > 0 && poz[x + 1, y - 1] == "b: king")
                        poz[x + 1, y - 1] = "Laimeti";
                    if (x < 7 && y > 0 && poz[x + 1, y - 1].Substring(0, 1) == "b")
                        poz[x + 1, y - 1] = "Pulti";
                    if (x > 0 && y < 7 && string.IsNullOrWhiteSpace(poz[x - 1, y + 1]))
                        poz[x - 1, y + 1] = "Eiti";
                    if (x > 0 && y < 7 && poz[x - 1, y + 1] == "b: king")
                        poz[x - 1, y + 1] = "Laimeti";
                    if (x > 0 && y < 7 && poz[x - 1, y + 1].Substring(0, 1) == "b")
                        poz[x - 1, y + 1] = "Pulti";
                    if (x > 0 && y >0 && string.IsNullOrWhiteSpace(poz[x - 1, y - 1]))
                        poz[x - 1, y - 1] = "Eiti";
                    if (x > 0 && y > 0 && poz[x - 1, y - 1] == "b: king")
                        poz[x - 1, y - 1] = "Laimeti";
                    if (x > 0 && y > 0 && poz[x - 1, y - 1].Substring(0, 1) == "b")
                        poz[x - 1, y - 1] = "Pulti";
                    if (y > 0 && string.IsNullOrWhiteSpace(poz[x, y - 1]))
                        poz[x, y - 1] = "Eiti";
                    if (y > 0 && poz[x, y - 1] == "b: king")
                        poz[x, y - 1] = "Laimeti";
                    if (y > 0 && poz[x, y - 1].Substring(0, 1) == "b")
                        poz[x, y - 1] = "Pulti";
                    if (y < 7 && string.IsNullOrWhiteSpace(poz[x, y + 1]))
                        poz[x, y + 1] = "Eiti";
                    if (y < 7 && poz[x, y + 1] == "b: king")
                        poz[x, y + 1] = "Laimeti";
                    if (y < 7 && poz[x, y + 1].Substring(0, 1) == "b")
                        poz[x, y + 1] = "Pulti";
                    if (x < 7 && string.IsNullOrWhiteSpace(poz[x + 1, y]))
                        poz[x + 1, y] = "Eiti";
                    if (x < 7 && poz[x + 1, y] == "b: king")
                        poz[x + 1, y] = "Laimeti";
                    if (x < 7 && poz[x + 1, y].Substring(0, 1) == "b")
                        poz[x + 1, y] = "Pulti";
                    if (x > 0 && string.IsNullOrWhiteSpace(poz[x - 1, y]))
                        poz[x - 1, y] = "Eiti";
                    if (x > 0 && poz[x - 1, y] == "b: king")
                        poz[x - 1, y] = "Laimeti";
                    if (x > 0 && poz[x - 1, y].Substring(0, 1) == "b")
                        poz[x - 1, y] = "Pulti";
                    break;
                // JUODI
                case "B: Pawn":
                    try
                    {
                        dabartinis = piece;
                        Save();
                        pozx = x;
                        pozy = y;
                        poz[x, y] = "Cancel";
                        if (y == 1)
                        {
                            if (string.IsNullOrWhiteSpace(poz[x, y + 2]) && string.IsNullOrWhiteSpace(poz[x, y + 1]))
                            {
                                poz[x, y + 2] = "Eiti";
                            }

                        }
                        //poz[x, y - 1] != poz[x, y - 2].ToLower()
                        if (y > 0 && string.IsNullOrWhiteSpace(poz[x, y + 1]))
                        {
                            poz[x, y + 1] = "Eiti";
                        }
                        if (x < 7 && y < 7 && !string.IsNullOrWhiteSpace(poz[x + 1, y + 1]) && poz[x + 1, y + 1] == "w: king")
                        {
                            poz[x + 1, y + 1] = "Laimeti";
                        }
                        if (x > 0 && y < 7 && !string.IsNullOrWhiteSpace(poz[x - 1, y + 1]) && poz[x - 1, y + 1] == "w: king")
                        {
                            poz[x - 1, y + 1] = "Laimeti";
                        }
                        if (x < 7 && y < 7 && !string.IsNullOrWhiteSpace(poz[x + 1, y + 1]) && poz[x + 1, y + 1].Substring(0, 1) == "w")
                        {
                            poz[x + 1, y + 1] = "Pulti";
                        }
                        if (x > 0 && y < 7 && !string.IsNullOrWhiteSpace(poz[x - 1, y + 1]) && poz[x - 1, y + 1].Substring(0, 1) == "w")
                        {
                            poz[x - 1, y + 1] = "Pulti";
                        }
                    }
                    catch
                    { }
                    break;
                case "B: Rook":
                    dabartinis = piece;
                    Save();
                    pozx = x;
                    pozy = y;
                    poz[x, y] = "Cancel";
                    if (rok2 < 1 && (poz[7, 0] == string.Empty || poz[0, 0] == string.Empty))
                    {
                        rok2++;
                    }
                    if (rok2 < 1 && poz[4, 0] == string.Empty)
                    {
                        rok2++;
                    }
                    for (int i = y; i < Ilgis;)
                    {
                        if (string.IsNullOrWhiteSpace(poz[x, i]))
                        {
                            poz[x, i] = "Eiti";
                        }
                        if (i < Aukstis - 1)
                            i++;
                        if (poz[x, i] == "w: king")
                        {
                            poz[x, i] = "Laimeti";
                            break;
                        }
                        if (!string.IsNullOrWhiteSpace(poz[x, i]))
                        {
                            if (poz[x, i].Substring(0, 1) == "w")
                            {
                                poz[x, i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = y; i < Ilgis;)
                    {
                        if (string.IsNullOrWhiteSpace(poz[x, i]))
                        {
                            poz[x, i] = "Eiti";
                        }
                        if (i > 0)
                            i--;
                        if (poz[x, i] == "w: king")
                        {
                            poz[x, i] = "Laimeti";
                            break;
                        }
                        if (!string.IsNullOrWhiteSpace(poz[x, i]))
                        {
                            if (poz[x, i].Substring(0, 1) == "w")
                            {
                                poz[x, i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = x; i < Aukstis;)
                    {
                        if (string.IsNullOrWhiteSpace(poz[i, y]))
                        {
                            poz[i, y] = "Eiti";
                        }
                        if (i > 0)
                            i--;
                        if (poz[i, y] == "w: king")
                        {
                            poz[i, y] = "Laimeti";
                            break;
                        }
                        if (rok2 < 1 && y == 0 && poz[i, y] == "b: king")
                        {
                            poz[i, y] = "BT/ Rokiruote";
                            break;
                        }
                        if (!string.IsNullOrWhiteSpace(poz[i, y]))
                        {
                            if (poz[i, y].Substring(0, 1) == "w")
                            {
                                poz[i, y] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = x; i < Aukstis;)
                    {
                        if (string.IsNullOrWhiteSpace(poz[i, y]))
                        {
                            poz[i, y] = "Eiti";
                        }
                        if (i < Aukstis - 1)
                            i++;
                        if (poz[i, y] == "w: king")
                        {
                            poz[i, y] = "Laimeti";
                            break;
                        }
                        if (rok2 < 1 && y == 0 && poz[i, y] == "b: king")
                        {
                            poz[i, y] = "BI/ Rokiruote";
                            break;
                        }
                        if (!string.IsNullOrWhiteSpace(poz[i, y]))
                        {
                            if (poz[i, y].Substring(0, 1) == "w")
                            {
                                poz[i, y] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }

                    poz[x, y] = "Cancel";
                    break;
                case "B: Bishop":
                    dabartinis = piece;
                    Save();
                    pozx = x;
                    pozy = y;
                    poz[x, y] = "Cancel";
                    for (int i = 0; i < Ilgis;)
                    {
                        if (x + i < Ilgis && y + i < Aukstis && string.IsNullOrWhiteSpace(poz[x + i, y + i]))
                        {
                            poz[x + i, y + i] = "Eiti";
                        }
                        if (x + i < Ilgis - 1 && y + i < Ilgis - 1)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x + i, y + i]))
                        {
                            if (poz[x + i, y + i] == "w: king")
                            {
                                poz[x + i, y + i] = "Laimeti";
                                break;
                            }
                            if (poz[x + i, y + i].Substring(0, 1) == "w")
                            {
                                poz[x + i, y + i] = "Pulti";
                                break;
                            }
                            break;
                        }

                    }
                    for (int i = 0; i < Ilgis;)
                    {
                        if (x + i < Ilgis && y - i > -1 && string.IsNullOrWhiteSpace(poz[x + i, y - i]))
                        {
                            poz[x + i, y - i] = "Eiti";
                        }
                        if (x + i < Ilgis - 1 && y - i > 0)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x + i, y - i]))
                        {
                            if (poz[x + i, y - i] == "w: king")
                            {
                                poz[x + i, y - i] = "Laimeti";
                                break;
                            }
                            if (poz[x + i, y - i].Substring(0, 1) == "w")
                            {
                                poz[x + i, y - i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = 0; i < Ilgis;)
                    {
                        if (x - i > -1 && y + i < Aukstis && string.IsNullOrWhiteSpace(poz[x - i, y + i]))
                        {
                            poz[x - i, y + i] = "Eiti";
                        }
                        if (x - i > 0 && y + i < Ilgis - 1)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x - i, y + i]))
                        {
                            if (poz[x - i, y + i] == "w: king")
                            {
                                poz[x - i, y + i] = "Laimeti";
                                break;
                            }
                            if (poz[x - i, y + i].Substring(0, 1) == "w")
                            {
                                poz[x - i, y + i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = 0; i < Ilgis;)
                    {
                        if (x - i > -1 && y - i > -1 && string.IsNullOrWhiteSpace(poz[x - i, y - i]))
                        {
                            poz[x - i, y - i] = "Eiti";
                        }
                        if (x - i > 0 && y - i > 0)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x - i, y - i]))
                        {
                            if (poz[x - i, y - i] == "w: king")
                            {
                                poz[x - i, y - i] = "Laimeti";
                                break;
                            }
                            if (poz[x - i, y - i].Substring(0, 1) == "w")
                            {
                                poz[x - i, y - i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    poz[x, y] = "Cancel";
                    break;
                case "B: Knight":
                    dabartinis = piece;
                    Save();
                    pozx = x;
                    pozy = y;
                    poz[x, y] = "Cancel";
                    if (x < 7 && y < 6 && string.IsNullOrWhiteSpace(poz[x + 1, y + 2]))
                        poz[x + 1, y + 2] = "Eiti";
                    if (x < 7 && y < 6 && poz[x + 1, y + 2] == "w: king")
                        poz[x + 1, y + 2] = "Laimeti";
                    if (x < 7 && y < 6 && poz[x + 1, y + 2].Substring(0, 1) == "w")
                        poz[x + 1, y + 2] = "Pulti";
                    if (x < 7 && y > 1 && string.IsNullOrWhiteSpace(poz[x + 1, y - 2]))
                        poz[x + 1, y - 2] = "Eiti";
                    if (x < 7 && y > 1 && poz[x + 1, y - 2] == "w: king")
                        poz[x + 1, y - 2] = "Laimeti";
                    if (x < 7 && y > 1 && poz[x + 1, y - 2].Substring(0, 1) == "w")
                        poz[x + 1, y - 2] = "Pulti";
                    if (x < 6 && y < 7 && string.IsNullOrWhiteSpace(poz[x + 2, y + 1]))
                        poz[x + 2, y + 1] = "Eiti";
                    if (x < 6 && y < 7 && poz[x + 2, y + 1] == "w: king")
                        poz[x + 2, y + 1] = "Laimeti";
                    if (x < 6 && y < 7 && poz[x + 2, y + 1].Substring(0, 1) == "w")
                        poz[x + 2, y + 1] = "Pulti";
                    if (x < 6 && y > 0 && string.IsNullOrWhiteSpace(poz[x + 2, y - 1]))
                        poz[x + 2, y - 1] = "Eiti";
                    if (x < 6 && y > 0 && poz[x + 2, y - 1] == "w: king")
                        poz[x + 2, y - 1] = "Laimeti";
                    if (x < 6 && y > 0 && poz[x + 2, y - 1].Substring(0, 1) == "w")
                        poz[x + 2, y - 1] = "Pulti";
                    if (x > 0 && y < 6 && string.IsNullOrWhiteSpace(poz[x - 1, y + 2]))
                        poz[x - 1, y + 2] = "Eiti";
                    if (x > 0 && y < 6 && poz[x - 1, y + 2] == "w: king")
                        poz[x - 1, y + 2] = "Laimeti";
                    if (x > 0 && y < 6 && poz[x - 1, y + 2].Substring(0, 1) == "w")
                        poz[x - 1, y + 2] = "Pulti";
                    if (x > 0 && y > 1 && string.IsNullOrWhiteSpace(poz[x - 1, y - 2]))
                        poz[x - 1, y - 2] = "Eiti";
                    if (x > 0 && y > 1 && poz[x - 1, y - 2] == "w: king")
                        poz[x - 1, y - 2] = "Laimeti";
                    if (x > 0 && y > 1 && poz[x - 1, y - 2].Substring(0, 1) == "w")
                        poz[x - 1, y - 2] = "Pulti";
                    if (x > 1 && y < 7 && string.IsNullOrWhiteSpace(poz[x - 2, y + 1]))
                        poz[x - 2, y + 1] = "Eiti";
                    if (x > 1 && y < 7 && poz[x - 2, y + 1] == "w: king")
                        poz[x - 2, y + 1] = "Laimeti";
                    if (x > 1 && y < 7 && poz[x - 2, y + 1].Substring(0, 1) == "w")
                        poz[x - 2, y + 1] = "Pulti";
                    if (x > 1 && y > 0 && string.IsNullOrWhiteSpace(poz[x - 2, y - 1]))
                        poz[x - 2, y - 1] = "Eiti";
                    if (x > 1 && y > 0 && poz[x - 2, y - 1] == "w: king")
                        poz[x - 2, y - 1] = "Laimeti";
                    if (x > 1 && y > 0 && poz[x - 2, y - 1].Substring(0, 1) == "w")
                        poz[x - 2, y - 1] = "Pulti";
                    break;
                case "B: Queen":
                    dabartinis = piece;
                    Save();
                    pozx = x;
                    pozy = y;
                    poz[x, y] = "Cancel";
                   for (int i = y; i < Ilgis;)
                    {
                        if (string.IsNullOrWhiteSpace(poz[x, i]))
                        {
                            poz[x, i] = "Eiti";
                        }
                        if (i < Aukstis - 1)
                            i++;
                        if (poz[x, i] == "w: king")
                        {
                            poz[x, i] = "Laimeti";
                            break;
                        }
                        if (!string.IsNullOrWhiteSpace(poz[x, i]))
                        {
                            if (poz[x, i].Substring(0, 1) == "w")
                            {
                                poz[x, i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = y; i < Ilgis;)
                    {
                        if (string.IsNullOrWhiteSpace(poz[x, i]))
                        {
                            poz[x, i] = "Eiti";
                        }
                        if (i > 0)
                            i--;
                        if (poz[x, i] == "w: king")
                        {
                            poz[x, i] = "Laimeti";
                            break;
                        }
                        if (!string.IsNullOrWhiteSpace(poz[x, i]))
                        {
                            if (poz[x, i].Substring(0, 1) == "w")
                            {
                                poz[x, i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = x; i < Aukstis;)
                    {
                        if (string.IsNullOrWhiteSpace(poz[i, y]))
                        {
                            poz[i, y] = "Eiti";
                        }
                        if (i > 0)
                            i--;
                        if (poz[i, y] == "w: king")
                        {
                            poz[i, y] = "Laimeti";
                            break;
                        }
                        if (!string.IsNullOrWhiteSpace(poz[i, y]))
                        {
                            if (poz[i, y].Substring(0, 1) == "w")
                            {
                                poz[i, y] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = x; i < Aukstis;)
                    {
                        if (string.IsNullOrWhiteSpace(poz[i, y]))
                        {
                            poz[i, y] = "Eiti";
                        }
                        if (i < Aukstis - 1)
                            i++;
                        if (poz[i, y] == "w: king")
                        {
                            poz[i, y] = "Laimeti";
                            break;
                        }
                        if (!string.IsNullOrWhiteSpace(poz[i, y]))
                        {
                            if (poz[i, y].Substring(0, 1) == "w")
                            {
                                poz[i, y] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    poz[x, y] = "Cancel";
                    for (int i = 0; i < Ilgis;)
                    {
                        if (x + i < Ilgis && y + i < Aukstis && string.IsNullOrWhiteSpace(poz[x + i, y + i]))
                        {
                            poz[x + i, y + i] = "Eiti";
                        }
                        if (x + i < Ilgis - 1 && y + i < Ilgis - 1)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x + i, y + i]))
                        {
                            if (poz[x + i, y + i] == "w: king")
                            {
                                poz[x + i, y + i] = "Laimeti";
                                break;
                            }
                            if (poz[x + i, y + i].Substring(0, 1) == "w")
                            {
                                poz[x + i, y + i] = "Pulti";
                                break;
                            }
                            break;
                        }

                    }
                    for (int i = 0; i < Ilgis;)
                    {
                        if (x + i < Ilgis && y - i > -1 && string.IsNullOrWhiteSpace(poz[x + i, y - i]))
                        {
                            poz[x + i, y - i] = "Eiti";
                        }
                        if (x + i < Ilgis - 1 && y - i > 0)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x + i, y - i]))
                        {
                            if (poz[x + i, y - i] == "w: king")
                            {
                                poz[x + i, y - i] = "Laimeti";
                                break;
                            }
                            if (poz[x + i, y - i].Substring(0, 1) == "w")
                            {
                                poz[x + i, y - i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = 0; i < Ilgis;)
                    {
                        if (x - i > -1 && y + i < Aukstis && string.IsNullOrWhiteSpace(poz[x - i, y + i]))
                        {
                            poz[x - i, y + i] = "Eiti";
                        }
                        if (x - i > 0 && y + i < Ilgis - 1)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x - i, y + i]))
                        {
                            if (poz[x - i, y + i] == "w: king")
                            {
                                poz[x - i, y + i] = "Laimeti";
                                break;
                            }
                            if (poz[x - i, y + i].Substring(0, 1) == "w")
                            {
                                poz[x - i, y + i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    for (int i = 0; i < Ilgis;)
                    {
                        if (x - i > -1 && y - i > -1 && string.IsNullOrWhiteSpace(poz[x - i, y - i]))
                        {
                            poz[x - i, y - i] = "Eiti";
                        }
                        if (x - i > 0 && y - i > 0)
                            i++;
                        if (!string.IsNullOrWhiteSpace(poz[x - i, y - i]))
                        {
                            if (poz[x - i, y - i] == "w: king")
                            {
                                poz[x - i, y - i] = "Laimeti";
                                break;
                            }
                            if (poz[x - i, y - i].Substring(0, 1) == "w")
                            {
                                poz[x - i, y - i] = "Pulti";
                                break;
                            }
                            break;
                        }
                    }
                    poz[x, y] = "Cancel";
                    break;
                case "B: King":
                    dabartinis = piece;
                    Save();
                    pozx = x;
                    pozy = y;
                    poz[x, y] = "Cancel";
                    if (x < 7 && y < 7 && string.IsNullOrWhiteSpace(poz[x + 1, y + 1]))
                        poz[x + 1, y + 1] = "Eiti";
                    if (x < 7 && y < 7 && poz[x + 1, y + 1] == "w: king")
                        poz[x + 1, y + 1] = "Laimeti";
                    if (x < 7 && y < 7 && poz[x + 1, y + 1].Substring(0, 1) == "w")
                        poz[x + 1, y + 1] = "Pulti";
                    if (x < 7 && y > 0 && string.IsNullOrWhiteSpace(poz[x + 1, y - 1]))
                        poz[x + 1, y - 1] = "Eiti";
                    if (x < 7 && y > 0 && poz[x + 1, y - 1] == "w: king")
                        poz[x + 1, y - 1] = "Laimeti";
                    if (x < 7 && y > 0 && poz[x + 1, y - 1].Substring(0, 1) == "w")
                        poz[x + 1, y - 1] = "Pulti";
                    if (x > 0 && y < 7 && string.IsNullOrWhiteSpace(poz[x - 1, y + 1]))
                        poz[x - 1, y + 1] = "Eiti";
                    if (x > 0 && y < 7 && poz[x - 1, y + 1] == "w: king")
                        poz[x - 1, y + 1] = "Laimeti";
                    if (x > 0 && y < 7 && poz[x - 1, y + 1].Substring(0, 1) == "w")
                        poz[x - 1, y + 1] = "Pulti";
                    if (x > 0 && y > 0 && string.IsNullOrWhiteSpace(poz[x - 1, y - 1]))
                        poz[x - 1, y - 1] = "Eiti";
                    if (x > 0 && y > 0 && poz[x - 1, y - 1] == "w: king")
                        poz[x - 1, y - 1] = "Laimeti";
                    if (x > 0 && y > 0 && poz[x - 1, y - 1].Substring(0, 1) == "w")
                        poz[x - 1, y - 1] = "Pulti";
                    if (y > 0 && string.IsNullOrWhiteSpace(poz[x, y - 1]))
                        poz[x, y - 1] = "Eiti";
                    if (y > 0 && poz[x, y - 1] == "w: king")
                        poz[x, y - 1] = "Laimeti";
                    if (y > 0 && poz[x, y - 1].Substring(0, 1) == "w")
                        poz[x, y - 1] = "Pulti";
                    if (y < 7 && string.IsNullOrWhiteSpace(poz[x, y + 1]))
                        poz[x, y + 1] = "Eiti";
                    if (y < 7 && poz[x, y + 1] == "w: king")
                        poz[x, y + 1] = "Laimeti";
                    if (y < 7 && poz[x, y + 1].Substring(0, 1) == "w")
                        poz[x, y + 1] = "Pulti";
                    if (x < 7 && string.IsNullOrWhiteSpace(poz[x + 1, y]))
                        poz[x + 1, y] = "Eiti";
                    if (x < 7 && poz[x + 1, y] == "w: king")
                        poz[x + 1, y] = "Laimeti";
                    if (x < 7 && poz[x + 1, y].Substring(0, 1) == "w")
                        poz[x + 1, y] = "Pulti";
                    if (x > 0 && string.IsNullOrWhiteSpace(poz[x - 1, y]))
                        poz[x - 1, y] = "Eiti";
                    if (x > 0 && poz[x - 1, y] == "w: king")
                        poz[x - 1, y] = "Laimeti";
                    if (x > 0 && poz[x - 1, y].Substring(0, 1) == "w")
                        poz[x - 1, y] = "Pulti";
                    break;
                case "Cancel":
                    Load();
                    break;
                case "Eiti":
                    Load();
                    count++;
                    poz[x, y] = dabartinis;
                    poz[pozx, pozy] = string.Empty;
                    break;
                case "Pulti":
                    Load();
                    count++;
                    poz[x, y] = dabartinis;
                    poz[pozx, pozy] = string.Empty;
                    break;
                case "WT/ Rokiruote":
                    Load();
                    count++;
                    rok++;
                    //rook 7, 7
                    //king 4, 7
                    poz[5, 7] = poz[7, 7];
                    poz[7, 7] = string.Empty;
                    poz[6, 7] = poz[4, 7];
                    poz[4, 7] = string.Empty;
                    break;
                case "WI/ Rokiruote":
                    Load();
                    count++;
                    rok++;
                    //rook 0, 7
                    //king 4, 7
                    poz[3, 7] = poz[0, 7];
                    poz[2, 7] = poz[4, 7];
                    poz[0, 7] = string.Empty;
                    poz[4, 7] = string.Empty;
                    break;
                    //ghuftr
                case "BT/ Rokiruote":
                    Load();
                    count++;
                    rok2++;
                    //rook 7, 0
                    //king 4, 0
                    poz[5, 0] = poz[7, 0];
                    poz[7, 0] = string.Empty;
                    poz[6, 0] = poz[4, 0];
                    poz[4, 0] = string.Empty;
                    break;
                case "BI/ Rokiruote":
                    Load();
                    count++;
                    rok2++;
                    //rook 0, 0
                    //king 4, 0
                    poz[3, 0] = poz[0, 0];
                    poz[2, 0] = poz[4, 0];
                    poz[0, 0] = string.Empty;
                    poz[4, 0] = string.Empty;
                    break;
                case "Laimeti":
                    Load();
                    count++;
                    poz[x, y] = dabartinis;
                    poz[pozx, pozy] = string.Empty;
                    for(int i= 0; i< Ilgis; i++)
                    {
                        for(int j = 0; j<Aukstis; j++)
                        {
                            if(!string.IsNullOrEmpty(poz[i, j]))
                            poz[i, j] = poz[i, j].ToLower();
                        }
                    }
                    break;
            }
        }
    }
}
