using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace JogodaCobrinha
{
    class Game
    {
        public Keys Direction { get; set; }
        public Keys Arrow { get; set; }

        private Timer Frame { get; set; }

        private Label lblPontos { get; set; }

        private Panel PnTela { get; set; }

        private int pontos = 0;

        private Food Food;

        private Snake Snake;

        private Bitmap offScreambitmap;

        private Graphics bitmapGraph;

        private Graphics screenGraph;

        public Game(ref Timer timer, ref Label label, ref Panel panel)

        {
            PnTela = panel;
            Frame = timer;
            lblPontos = label;
            offScreambitmap = new Bitmap(428, 428);
            Snake = new Snake();
            Food = new Food();
            Direction = Keys.Left;
            Arrow = Direction;
        }

        public void StartGame()
        {
            Snake.Reset();
            Food.CreateFood();
            Direction = Keys.Left;
            bitmapGraph = Graphics.FromImage(offScreambitmap);
            screenGraph = PnTela.CreateGraphics();
            Frame.Enabled = true;
        }

        public void Tick()
        {
            if (((Arrow == Keys.Left) && (Direction != Keys.Right)) ||
              ((Arrow == Keys.Right) && (Direction != Keys.Left)) ||
              ((Arrow == Keys.Up) && (Direction != Keys.Down)) ||
              ((Arrow == Keys.Down) && (Direction != Keys.Up)))
            {
                Direction = Arrow;
            }
            switch (Direction)
            {
                case Keys.Left:
                    Snake.Left();
                    break;
                case Keys.Right:
                    Snake.Right();
                    break;
                case Keys.Up:
                    Snake.Up();
                    break;
                case Keys.Down:
                    Snake.Down();
                    break;
            }
            bitmapGraph.Clear(Color.White);

            bitmapGraph.DrawImage(Properties.Resources.png_transparent_apple_apple_apple_fruit_illustration_natural_foods_food_fruit_thumbnail, (Food.Location.X * 15), (Food.Location.Y * 15), 15, 15);
            bool gameOver = false;

            for (int i = 0; i < Snake.Lenght; i++) {
                if (i == 0)
                {
                    bitmapGraph.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#000000")), (Snake.Location[i].X * 15), (Snake.Location[i].Y * 15), 15, 15);

                }
                else {
                    bitmapGraph.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#4F4F4F")), (Snake.Location[i].X * 15), (Snake.Location[i].Y * 15), 15, 15);

                }

                if ((Snake.Location[i] == Snake.Location[0]) && (i > 0))
                {
                    gameOver = true;
                }
            }
            screenGraph.DrawImage(offScreambitmap, 0, 0);
            CheckColision();
            if (gameOver)
            {
                GameOver();
            }
        }
        public void CheckColision() { 
        if (Snake.Location[0] ==Food.Location)
            {
                Snake.Eat();
                Food.CreateFood();
                pontos += 9;
                lblPontos.Text = "PONTOS:" + pontos;
            }
        }
        public void GameOver() { 
        Frame.Enabled = false;
            bitmapGraph.Dispose();
            screenGraph.Dispose();
            lblPontos.Text = "PONTOS: 0";
            MessageBox.Show("GAME OVER");
                

        }
    }
}



