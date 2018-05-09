using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public class Sudoku
    {
        public Label[,] CasillasBi { get; set; } = new Label[9,9];

        //Devuelve el número de la casilla
        public string GetLabelText(KeyPressEventArgs e, string TextoActual)
        {
            switch (e.KeyChar)
            {
                case '1': return "1";
                case '2': return "2";
                case '3': return "3";
                case '4': return "4";
                case '5': return "5";
                case '6': return "6";
                case '7': return "7";
                case '8': return "8";
                case '9': return "9";
                case '0':
                case '\b': return " ";
                default: return TextoActual;
            }
        }

        //Metdo para generar los numeros aleatorios
        public Label[,,] GenerarNumeros( Label[,,] casillas)
        {
            Random r = new Random();

            int cont = 0;
            for (; cont < 34; cont++)
            {
                int panel = r.Next(0,9);
                int row = r.Next(0,3); 
                int column = r.Next(0, 3);
                int number = r.Next(1,9);


                if (ComprobarNumero(casillas,number,panel, row, column))
                {
                    casillas[panel, column, row].Text = number.ToString();
                    casillas[panel, column, row].Enabled = false;

                    Label aux = casillas[panel, column, row];

                    GetCasillasBi(panel, ref row, ref column);
                    CasillasBi[column, row] = aux;
                }
                else
                {
                    cont--;
                }
            }
            return casillas;
        }

        //Método para comprobar que no se repiten los numeros en la fila, columna y panel
        private bool ComprobarNumero(Label[,,] casillas,int number, int panel, int row, int column)
        {
            int auxRow = row;
            int auxColumn = column;

            //Comprobar que no se repite en el mismo panel
            if (casillas[panel, 0, 0].Text == number.ToString() || casillas[panel, 0, 1].Text == number.ToString() || casillas[panel, 0, 2].Text == number.ToString() ||
                casillas[panel, 1, 0].Text == number.ToString() || casillas[panel, 1, 1].Text == number.ToString() || casillas[panel, 1, 2].Text == number.ToString() ||
                casillas[panel, 2, 0].Text == number.ToString() || casillas[panel, 2, 1].Text == number.ToString() || casillas[panel, 2, 2].Text == number.ToString())
            {
                return false;
            }
            GetCasillasBi(panel, ref auxRow, ref auxColumn);

            //Comprobar que no se repite en la misma fila
            for (int i = 0; i < 9; i++)
            {
                if (CasillasBi[auxColumn, i] != null && CasillasBi[auxColumn, i].Text == number.ToString())
                {
                    return false;
                }
            }

            //Comprobar que no se repite en la misma columna
            for (int i = 0; i < 9; i++)
            {
                if (CasillasBi[i, auxRow] != null && CasillasBi[i, auxRow].Text == number.ToString())
                {
                    return false;
                }
            }

            return true;
        }

        //Metodo para transformar las posiciones del array tridimensional en bidimensional
        public void GetCasillasBi(int panel, ref int row, ref int column)
        {
            int newRow = 0, newColumn = 0;
            switch (panel)
            {
                case 0:
                    newRow = 0;
                    newColumn = 0;
                    break;
                case 1:
                    newRow = 3;
                    newColumn = 0;
                    break;
                case 2:
                    newRow = 6;
                    newColumn = 0;
                    break;
                case 3:
                    newRow = 0;
                    newColumn = 3;
                    break;
                case 4:
                    newRow = 3;
                    newColumn = 3;
                    break;
                case 5:
                    newRow = 6;
                    newColumn = 3;
                    break;
                case 6:
                    newRow = 0;
                    newColumn = 6;
                    break;
                case 7:
                    newRow = 3;
                    newColumn = 6;
                    break;
                case 8:
                    newRow = 6;
                    newColumn = 6;
                    break;
            }

            for (int i = column; i > 0; i--)
            {
                newColumn++;
            }

            for (int j = row; j > 0; j--)
            {
                newRow++;
            }

            row = newRow;
            column = newColumn;
        }

        //Método para comprobar que se puede resolver
        public bool PuedeResolverse(Label[,,] casillas)
        {

            return true;
        }
    }
}
