using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Bird
{
    public partial class Form1 : Form
    {
        int boruHızı = 8;
        int gravity = 10;
        int score = 0;
       
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true; // Klavye olaylarını yakala
            this.KeyDown += new KeyEventHandler(this.gamekeyisdown);
            this.KeyUp += new KeyEventHandler(this.gamekeyisup);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;
            BoruAlt.Left -= boruHızı;
            BoruUst.Left -= boruHızı;
            scoreText.Text = "Score: " + score;

    



            if (BoruAlt.Left < -150)
            {
                BoruAlt.Left = 500;
                score++;
            }
            if (BoruUst.Left < -180)
            {
                BoruUst.Left = 650;
                score++;
            }
            if (flappyBird.Bounds.IntersectsWith(BoruAlt.Bounds) || flappyBird.Bounds.IntersectsWith(BoruUst.Bounds) || flappyBird.Bounds.IntersectsWith(Zemin.Bounds))
            {
                endGame();
            }
            if (score > 5)
            {
                boruHızı = 15;
            }
            if (flappyBird.Top < -25)
            {
                endGame();
            }
        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = -10;
            }
        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 10;
            }
        }

        private void endGame()
        {
            gametimer.Stop();
            scoreText.Text = "Game Over!!! Your Score: " + score;

            scoreText.BringToFront();
            scoreText.Left = (this.Width / 2) - (scoreText.Width / 2);
            scoreText.Top = (this.Height / 3) - (scoreText.Height / 3);

            DialogResult result = MessageBox.Show("Yeniden oynamak ister misin?", "Oyun Bitti", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                restartGame(); // Oyunu yeniden başlatan metodu çağır
            }

        }
        private void restartGame()
        {
            // Kuşun başlangıç pozisyonunu ayarla
            flappyBird.Location = new Point(12, 12); // veya başlangıç konumunuzu girin

            // Boruların başlangıç pozisyonlarını ayarla
            BoruUst.Left = 650;
            BoruAlt.Left = 500;

            // Skor ve hız değişkenlerini sıfırla
            score = 0;
            boruHızı = 8;
            gravity = 10;

            // Skor yazısını sıfırla
            scoreText.Text = "Score: 0";
            scoreText.Left = 10;
            scoreText.Top = 10;

            // Timer'ı tekrar başlat
            gametimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gametimer.Start();
        }
    }
}