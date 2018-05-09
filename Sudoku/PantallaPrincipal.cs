using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class PantallaPrincipal : Form
    {
        //Array que contiene todos los numeros para su mejor manejo
        public Label[,,] Casillas { get; set; } = new Label[9, 3, 3];
        //Array que contiene los paneles
        public Panel[] Panels { get; set; } = new Panel[9];
        //Casilla actual
        public Label LabelActual { get; set; }
        
        //Venta auxiliar para poner numeros chiquitines
        public PantallaNumeritos Numeritos { get; set; }
        //Declaración de la clase que controla el juego
        public Sudoku Game { get; set; }

        public PantallaPrincipal()
        {
            Game = new Sudoku();
            Numeritos = new PantallaNumeritos();
            InitializeComponent();
            CrearCasillas();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        //Metodo para crear el programa desde código
        private void CrearCasillas()
        {
            //CrearPannels
            int panelCont = 0;
            for (int column = 0; column < 3; column++)
            {
                for (int row = 0; row < 3; row++)
                {
                    Panel actualPanel = new Panel();
                    actualPanel.BackColor = Color.White;
                    actualPanel.Location = new Point(10 + (row * 5) + (row * 90), 34+ (column * 5) + (column * 90));
                    actualPanel.Name = "panel " +( panelCont);
                    actualPanel.Size = new Size(90, 90);
                    actualPanel.TabIndex = 2;
                    Controls.Add(actualPanel);
                    Panels[panelCont] = actualPanel;
                    panelCont++;
                }
            }

            //CrearLabels
            for (int panel = 0; panel < 9; panel++)
            {
                for (int column = 0; column < 3; column++)
                {
                    for (int row = 0; row < 3; row++)
                    {
                        Label actualLabel = new Label();
                        actualLabel.BorderStyle = BorderStyle.FixedSingle;
                        actualLabel.Location = new Point(row*30, column * 30);
                        actualLabel.Name = "label "+panel+" "+row+" "+column;
                        actualLabel.Size = new Size(30, 30);
                        actualLabel.TabIndex = 0;
                        actualLabel.Text = " ";
                        actualLabel.TextAlign = ContentAlignment.MiddleCenter;
                        actualLabel.MouseClick += new MouseEventHandler(Casilla_Click);
                        Panels[panel].Controls.Add(actualLabel);
                        Casillas[panel,column, row] = actualLabel;
                    }
                }
            }
        }

        //Evento de click
        private void Casilla_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Numeritos.ShowDialog();
            }
            //Si no es click derecho seleccionamos la casilla pulsada
            else
            {
                string[] parts;
                int panel, row, column;


                if (LabelActual != null)
                {
                    parts = LabelActual.Name.Split();
                    panel = Convert.ToInt32(parts[1]);
                    row = Convert.ToInt32(parts[2]);
                    column = Convert.ToInt32(parts[3]);
                    Casillas[panel, column, row].BackColor = Color.White;
                }

                LabelActual = ((Label)sender);

                parts = LabelActual.Name.Split();
                panel = Convert.ToInt32(parts[1]);
                row = Convert.ToInt32(parts[2]);
                column = Convert.ToInt32(parts[3]);
                parts = LabelActual.Name.Split();

                Casillas[panel, column, row].BackColor = Color.GreenYellow;
            }
        }

        //Evento para cambiar el numero de la casilla pulsada
        private void PantallaPrincipal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (LabelActual != null)
            {
                LabelActual.Text = Game.GetLabelText(e, LabelActual.Text);
            }
        }

        //Evento para iniciar el sudoku
        private void iniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reiniciarCasillas();
            do
            {
                Casillas = Game.GenerarNumeros(Casillas);
            } while (!(Game.PuedeResolverse(Casillas)));
        }

        //Metodo para reiniciar las labels
        private void reiniciarCasillas()
        {
            for (int panel = 0; panel < 9; panel++)
            {
                for (int row = 0; row < 3; row++)
                {
                    for (int column = 0; column < 3; column++)
                    {
                        Casillas[panel, column, row].Text = "";
                        Casillas[panel, column, row].Enabled = true;
                    }
                }
            }
        }
    }
}