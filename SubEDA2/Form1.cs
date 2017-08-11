using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubEDA2
{
    public partial class Form1 : Form
    {
        bool objeto = false;
        bool inicio = false;
        bool jogar = false;             
        int x = -1, y = -1;
        KnightsTourGraph kt;
        int tam = -1;
        int i = 0, j = 0;
        int[] obsX = new int[100];
        int[] obsY = new int[100];
        int countObs = 0;
        
        




    public Form1()
        {
            InitializeComponent();            
        }

        public void Quadro_Inicio()
        {
            quadro.Columns.Clear();
            quadro.BackgroundColor = Color.LightGray;
            quadro.DefaultCellStyle.BackColor = Color.LightGray;

            for (int i = 0; i < tam; i++)
            {
                quadro.Columns.Add("a","a");
                quadro.Rows.Add();
                
            }



            foreach (DataGridViewColumn c in quadro.Columns)
            {
                c.Width = quadro.Width / quadro.Columns.Count;
            }

            foreach (DataGridViewRow c in quadro.Rows)
            {
                c.Height = quadro.Height / quadro.Rows.Count;
            }

            for (int row = 0; row < quadro.Rows.Count; row++)
            {
                for (int col = 0; col < quadro.Columns.Count; col++)
                {
                    if (row % 2 == 0 && col % 2 != 0)
                    {
                        quadro[col, row].Style.BackColor = Color.DarkGray;
                    }

                    if (row % 2 != 0 && col % 2 == 0)
                    {
                        quadro[col, row].Style.BackColor = Color.DarkGray;
                    }
                    quadro[col, row].ReadOnly = true;
                }
            }
            
            quadro.CurrentCell.Selected = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" )
                MessageBox.Show("Digite um valor", "KnightsTour", MessageBoxButtons.OK);
            else if(Convert.ToInt32(textBox2.Text) == 0 || Convert.ToInt32(textBox2.Text) <=4)
                MessageBox.Show("Digite um valor diferente", "KnightsTour", MessageBoxButtons.OK);
            else
            {
                 tam = Convert.ToInt32(textBox2.Text);
                Quadro_Inicio();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            objeto = true;
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            objeto = false;
          
        }

        private void quadro_CellClick(object sender, DataGridViewCellEventArgs e)
            {
            
            if (jogar == false)
            {

                if (objeto == true)
                {


                    quadro[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
                    MessageBox.Show("Obstáculo adicionado nas coordenadas: " + "[" + e.ColumnIndex + "]" + "[" + e.RowIndex + "]", "KnightsTour", MessageBoxButtons.OK);
                    countObs++;
                    obsX[i++] = e.ColumnIndex;
                    obsY[j++] = e.RowIndex;
                    quadro.CurrentCell.Style.BackColor = Color.Red;    
                    
                    //MessageBox.Show("[" + e.ColumnIndex + "]" + "[" + e.RowIndex + "]", "KnightsTour", MessageBoxButtons.OK);
                }
                else if (inicio == true)
                {
                    if (tam%2==0)
                    {
                        /*DataGridViewImageCell dgc = new DataGridViewImageCell();
                        var img = Image.FromFile(@"C:\Users\Vine\Pictures\hourse.png");
                        dgc.Value = img;
                        dgc.ImageLayout = DataGridViewImageCellLayout.Stretch;
                        quadro[e.ColumnIndex, e.RowIndex] = dgc;*/

                        MessageBox.Show("Inicio selecionado nas coordenadas:  " + "[" + e.ColumnIndex + "]" + "[" + e.RowIndex + "]", "KnightsTour", MessageBoxButtons.OK);
                                    

                        x = e.ColumnIndex;
                        y = e.RowIndex;
                        inicio = false;
                    }
                    else if(ValidPosition(e.ColumnIndex, e.RowIndex, tam, tam))
                    {
                        MessageBox.Show("Inicio selecionado nas coordenadas:  " + "[" + e.ColumnIndex + "]" + "[" + e.RowIndex + "]", "KnightsTour", MessageBoxButtons.OK);


                        x = e.ColumnIndex;
                        y = e.RowIndex;
                        inicio = false;
                    }
                    else
                        MessageBox.Show("Selecione outro lugar", "KnightsTour", MessageBoxButtons.OK);


                }             
                
                

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            kt = new KnightsTourGraph(tam, tam);

            if (x==-1 && y==-1)
                MessageBox.Show("Selecione o inicio", "KnightsTour", MessageBoxButtons.OK);

            else if(i>0 && j>0)
            {
                for(int z = 0;z < i;z++)
                {
                    kt.AddObstacle(obsX[z], obsY[z]);
                }
                MessageBox.Show("Programa iniciado nas coordenadas: " + "[" + x + "]" + "[" + y + "]", "KnightsTour", MessageBoxButtons.OK);
                kt.GenerateSumObs();
                int[,] resp = kt.GetSolution(x, y);
                mostraSolucaoObs(resp);
                jogar = true;

            }
            else
            {
                timer1.Start();
                MessageBox.Show("Programa iniciado nas coordenadas: " + "[" + x + "]" + "[" + y + "]", "KnightsTour", MessageBoxButtons.OK);
                
                int[,] resp = kt.GetSolution(x,y);
                mostraSolucao(resp);
                jogar = true;
                
            }
           
        }

        private async void mostraSolucao(int[,] sol)
        {
            for (int i = 1; i <= tam * tam; i++)
            {
                /*DataGridViewImageCell dgc = new DataGridViewImageCell();
                var img = Image.FromFile(@"C:\Users\Vine\Pictures\hourse.png");
                dgc.Value = img;
                dgc.ImageLayout = DataGridViewImageCellLayout.Stretch;
                quadro[procuraX(sol, i), procuraY(sol, i)] = dgc;*/                
                quadro[procuraX(sol, i), procuraY(sol, i)].Value = i;                  
                quadro[procuraX(sol, i), procuraY(sol, i)].Style.BackColor = Color.Blue;
                await Task.Delay(tam*tam);

            }
        }

        private async void mostraSolucaoObs(int[,] sol)
        {
            for (int i = 1; i <= (tam * tam)-countObs; i++)
            {
                quadro[procuraX(sol, i), procuraY(sol, i)].Value = i;
                quadro[procuraX(sol, i), procuraY(sol, i)].Style.BackColor = Color.Blue;
                await Task.Delay(tam * tam);

            }
        }



        private int procuraX(int[,] sol,int valor)
        {
            for(int x=0;x<tam;x++)
            {
                for(int y=0;y<tam;y++)
                {
                    if (sol[x, y] == valor)
                        return y;
                }
            }

            return -1;
        }

        private int procuraY(int[,] sol, int valor)
        {
            for (int x = 0; x < tam; x++)
            {
                for (int y = 0; y < tam; y++)
                {
                    if (sol[x, y] == valor)
                        return x;
                }
            }

            return -1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            inicio = true;
            objeto = false;
        }

        private void reiniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
            kt = new KnightsTourGraph(tam,tam);
            quadro.Columns.Clear();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Application.Restart();
            kt = new KnightsTourGraph(tam, tam);
            quadro.Columns.Clear();
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Knight's Tour é uma sequência de movimentos de um cavaleiro em um tabuleiro de xadrez, de modo que o cavaleiro visita cada quadrado apenas uma vez preenchendo o tabuleiro inteiro.", "KnightsTour", MessageBoxButtons.OK);


        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
      
        public static bool ValidPosition(int x, int y, int N, int M)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (i % 2 == 0 && j % 2 != 0 && i == y && j == x)
                    {
                        return false;
                    }

                    if (i % 2 != 0 && j % 2 == 0 && i == y && j == x)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
