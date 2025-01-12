using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Igrica
{
    public partial class gameForm : Form
    {

        Player player;
        bool goLeft, goRight, goUp, goDown, gameOver;
        int mouseX = 0, mouseY = 0;
        Pen laserPen = new Pen(Color.Red, 2);

        public gameForm()
        {
            InitializeComponent();
            this.Size = new Size(960, 600);
            player = new Player(this.Width / 2, this.Height / 2);
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {

            // igrac ziv?
            if (!player.isAlive() && !gameOver)
            {
                gameOver = true;
            }
            else if (!gameOver)
            {
                pbHealth.Value = player.getHealth();
                // kretanje igraca
                int dx = 0;
                int dy = 0;
                if (goLeft && player.x > player.speed)                                              // Left
                {
                    dx = -1 * player.speed;
                }
                if (goRight && player.x < this.Width - player.speed - player.image.Width)           // Right
                {
                    dx = player.speed;
                }
                if (goUp && player.y > 50)                                                          // Up
                {
                    dy = -1 * player.speed;
                }
                if (goDown && player.y < this.Height - player.speed - player.image.Height)          // Down
                {
                    dy = player.speed;
                }
                player.Move(dx, dy);

                int newPx = -1 * (player.x - player.image.Width/2 - this.Width / 2 + 20);
                int newPy = (player.y - player.image.Height / 2 - this.Height / 2 - 2);
                int newMx = -1 * (mouseX - this.Width / 2 - player.image.Width / 2);
                int newMy = (mouseY - this.Height / 2 - player.image.Height);
                double dist = (double)Math.Sqrt((newMx  - newPx)*(newMx  - newPx) + (newMy - newPy) * (newMy - newPy));
                int s = 1;
                txtTest.Text = newPx.ToString() + " " + newPy.ToString() + " " + newMx.ToString() + " " + newMy.ToString() + ", " + dist.ToString();

                if (newPx != newMx)
                {
                    if (newMy > newPy) {
                        s = -1;
                    }

                    player.angle = s * (float)(180 + Math.Acos((newMx - newPx) / dist) * 180 / Math.PI);
                }
                else if (newPy != newMy)
                {
                    player.angle = (float)(Math.Asin((newMy - newPy) / dist) * 180 / Math.PI);
                }
                else
                {
                    player.angle = (float)-90;
                }

                // stats display
                txtStats.Text = this.Width.ToString() + ", " +this.Height.ToString() + ",  " + 
                    player.x.ToString() + ", " + player.y.ToString() + ", " + player.angle.ToString();
            }

            this.Invalidate();

        }

        private void DrawForm(object sender, PaintEventArgs e)
        {
            if(player.image != null)
            {

                float bw2 = player.x + player.image.Width / 2f - 20;    
                float bh2 = player.y + player.image.Height / 2f + 2;
                e.Graphics.DrawLine(laserPen , bw2, bh2, mouseX, mouseY);
                e.Graphics.TranslateTransform(bw2, bh2);
                e.Graphics.RotateTransform(player.angle);
                e.Graphics.TranslateTransform(-bw2, -bh2);
                e.Graphics.DrawImage(player.image, player.x, player.y);
                e.Graphics.ResetTransform();
                //e.Graphics.DrawLine(new Pen(Color.Green, 2) , bw2, bh2, mouseX, bh2);
                //e.Graphics.DrawLine(new Pen(Color.Blue, 2), bw2, bh2, bw2, mouseY);
            }
        }

        private void PlayerShoot(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                player.Shoot();
            }
        }

        private void PlayerAim(object sender, MouseEventArgs e)
        {
            mouseX = e.X; 
            mouseY = e.Y;

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (gameOver)
            {
                return;
            }

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                goLeft = true;
            }

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                goRight = true;
            }

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                goUp = true;
            }

            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                goDown = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (gameOver)
            {
                return;
            }

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                goUp = false;
            }

            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                goDown = false;
            }
        }
    }
}
