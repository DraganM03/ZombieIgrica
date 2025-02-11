using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
        float laserD = 0;
        List<Bullet> bullets = new List<Bullet>();
        List<Bullet> bulletsToDestroy = new List<Bullet>();
        Random rand = new Random();

        int[,] zombieSpawnAreas = {
                { 0, 50, 600},          //lijevo X, minY, maxY
                { 0, 800, 0 },          //gore   minX, maxX, Y
                { 800, 50, 600}         //desno  X, minY, maxY
            };
        List<Zombie> zombies = new List<Zombie>();
        List<Zombie> zombiesToRemove = new List<Zombie>();
        public gameForm()
        {
            InitializeComponent();  
            Cursor.Hide();  //vizuelni mis
            this.MaximizeBox = false;   //povecanje ekrana
            this.Size = new Size(821, 641); //rezolucija
            player = new Player(this.Width / 2, this.Height / 2);   //spawn igraca
            this.txtStats.Hide();
            this.txtTest.Hide();
        }
        
        private void MainTimerEvent(object sender, EventArgs e)
        {

            this.txtKills.Text = "Kills: " + player.kills.ToString();
            // igrac ziv?
            if (!player.isAlive() && !gameOver)
            {
                pbHealth.Value = 0;
                Cursor.Show();
                gameOver = true;
            }
            else if (!gameOver)
            {
                //zombiji spawn
                if (zombies.Count() == 0)
                {
                    for (int i = 0; i< player.numOfZombies(); i++)
                    {
                        int randX, randY;
                        int pos = rand.Next(0,2);
                        if(pos%2 == 0) //sa strane
                        {
                            randX = zombieSpawnAreas[pos,0];
                            randY = rand.Next(zombieSpawnAreas[pos,1],zombieSpawnAreas[pos,2]);
                        }
                        else
                        {
                            randX = rand.Next(zombieSpawnAreas[pos, 0], zombieSpawnAreas[pos, 1]);
                            randY = zombieSpawnAreas[pos, 2];
                        }
                        Zombie zomb = new Zombie(randX, randY, 0);
                        zomb.updatePXY(player.x, player.y);
                        zombies.Add(zomb);

                        //txtTest.Text += randX.ToString() + " " + randY.ToString() + "\n";
                    }
                }

                txtAmmo.Text = "Ammo: " + player.getAmmoCount();

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

                //pomjeranje imaginarnog koordinatnog sistema na centar 
                int newPx = -1 * (player.x - player.image.Width/2 - this.Width / 2 + 20);  
                int newPy = (player.y - player.image.Height / 2 - this.Height / 2 - 2);
                int newMx = -1 * (mouseX - this.Width / 2 - player.image.Width / 2);
                int newMy = (mouseY - this.Height / 2 - player.image.Height);
                double dist = (double)Math.Sqrt((newMx  - newPx)*(newMx  - newPx) + (newMy - newPy) * (newMy - newPy));
                //txtTest.Text = newPx.ToString() + " " + newPy.ToString() + " " + newMx.ToString() + " " + newMy.ToString() + ", " + dist.ToString();
                this.laserD = (float)dist;
                if (newPx != newMx)
                {
                    if (newMy > newPy) {
                        player.s = -1;
                    }
                    else
                    {
                        player.s = 1;
                    }

                    player.angle = player.s * (float)(180 + Math.Acos((newMx - newPx) / dist) * 180 / Math.PI);
                }
                else if (newPy != newMy)
                {
                    player.angle = (float)(Math.Asin((newMy - newPy) / dist) * 180 / Math.PI);
                }
                else
                {
                    player.angle = 270.0f;
                }

                foreach (Zombie zombie in zombies)
                {
                    if(Math.Abs(player.x - (zombie.x + zombie.img.Height / 2)) < zombie.img.Width && Math.Abs(player.y - (zombie.y + zombie.img.Width / 2)) < zombie.img.Height && DateTime.Now.ToUniversalTime().Second - zombie.lastHit > 0)
                    {
                        player.TakeDamage(zombie.dmg);
                        zombie.lastHit = DateTime.Now.ToUniversalTime().Second;
                    }
                }

                // stats display
                //txtStats.Text = this.Width.ToString() + ", " +this.Height.ToString() + ",  " + 
                //    player.x.ToString() + ", " + player.y.ToString() + ", " + player.angle.ToString();

            }

            this.Refresh();
            
        }


        // crtanje
        private void DrawForm(object sender, PaintEventArgs e)
        {

            if(player.image != null)
            {

                float bw2 = player.x + player.image.Width / 2f - 20;    
                float bh2 = player.y + player.image.Height / 2f + 2;
                //laser
                float lx = (float)(bw2 + this.laserD * Math.Cos(player.angle * Math.PI / 180));
                float ly = (float)(bh2 + this.laserD * Math.Sin(player.angle * Math.PI / 180));
                e.Graphics.DrawLine(laserPen , bw2, bh2, lx, ly);
                e.Graphics.DrawEllipse(laserPen, lx - 3f, ly - 3f, 6f, 6f);

                //projektili crtanje
                //String bulletsStr = "";
                foreach (Bullet bullet in bullets)
                {
                    if(bullet == null)
                    {
                        bulletsToDestroy.Add(bullet);
                        continue;
                    }

                    foreach (Zombie zombie in zombies)
                    {
                        if (Math.Abs(bullet.x + bullet.dx - (zombie.x + zombie.img.Height/2)) < zombie.img.Width && Math.Abs(bullet.y + bullet.dy - (zombie.y + zombie.img.Width / 2)) < zombie.img.Height && bullet.canDealDmg)
                        {
                            zombie.takeDmg(player.dealDmg());

                            if(zombie.hp <= 0)
                            {
                                player.GainXP(10);
                                player.kills++;
                                zombiesToRemove.Add(zombie);
                            }

                            bullet.canDealDmg = false;
                        }
                    }

                    if (! bullet.canDealDmg)
                    {
                        //bullets.Remove(bullet);
                        bulletsToDestroy.Add(bullet);
                        continue;
                    }

                    e.Graphics.DrawLine(bullet.bulletPen, bullet.x, bullet.y,
                        (float)(bullet.x +  bullet.dx),
                        (float)(bullet.y +  bullet.dy));
                    //bulletsStr += bullet.angle.ToString() + " " + (Math.Cos(bullet.angle)).ToString() + " " + (Math.Sin(bullet.angle)).ToString() + "\n";
                }

                foreach (Bullet bullet in bulletsToDestroy)
                {
                    bullets.Remove(bullet);
                }
                bulletsToDestroy.Clear();

                foreach (Zombie zombie in zombiesToRemove)
                {
                    zombies.Remove(zombie);
                }
                zombiesToRemove.Clear();

                //zombiji crtanje
                //txtTest.Text = "";
                foreach (Zombie zombie in zombies)
                { 



                    float dZ1P = (float)Math.Sqrt((player.x - zombie.x) * (player.x - zombie.x) + (player.y - zombie.y) * (player.y - zombie.y));


                    e.Graphics.TranslateTransform(zombie.x + zombie.img.Width / 2.0f, zombie.y + zombie.img.Height / 2.0f);
                    //e.Graphics.RotateTransform(zombie.angle);
                    e.Graphics.TranslateTransform( -1 * (zombie.x + zombie.img.Width / 2.0f), -1 * (zombie.y + zombie.img.Height / 2.0f));
                    e.Graphics.DrawImage(zombie.img, zombie.x, zombie.y);
                    e.Graphics.ResetTransform();


                    bool canMove = true;
                    int distX = 0;
                    int distY = 0;
                    foreach (Zombie zombie2 in zombies)
                    {
                        if(zombie2 == zombie)
                        {
                            continue;
                        }
                        distX = zombie2.x - zombie.x;
                        distY = zombie2.y - zombie.y;
                        if (Math.Abs(distX) <= 50 && Math.Abs(distY) <= 50)
                        {
                            float dZ2P = (float)Math.Sqrt((player.x - zombie2.x) * (player.x - zombie2.x) + (player.y - zombie2.y) * (player.y - zombie2.y));

                            if (dZ1P > dZ2P)
                            {
                                canMove = false;    
                            }
                        }
                    }

                    if (canMove)
                    {
                        zombie.moveThowards(player.x, player.y, 
                            player.image.Width, player.image.Height);
                    }
                    else
                    {
                        //zombie.moveThowards(zombie.x + distX/2, zombie.y + distY/2, 50, 50);
                    }
                    //zombie.moveThowards(player.x, player.y);

                    //txtTest.Text += zombie.x.ToString() + " " + zombie.y.ToString() + " " + zombie.angle + "\n";
                
                
                }

                //txtTest.Text = bulletsStr;

                //igrac
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
                bullets.Add(player.Shoot(this, player.angle));
                //txtTest.Text = player.angle.ToString();
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
                this.gameTimer.Stop();
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

            if (e.KeyCode == Keys.R && player.ammo == 0)
            {
                player.reload();
            }

            if (e.KeyCode == Keys.Enter && gameOver)
            {
                RestartGame();
            }
        }

        private void RestartGame()
        {

            zombies.Clear();

            goUp = false;
            goDown = false;
            goRight = false;
            goLeft = false;

            player.x = this.Width/2;
            player.y = this.Height/2;
            player.speed = 10;
            player.angle = -90;
            player.health = 100;
            player.xp = 0;
            player.level = 1;
            player.dmg = 10;
            player.gunCap = 10;
            gameOver = false;
            player.image = Properties.Resources.playerR;
            //txtGameOver1.Visible = false;
            //txtGameOver2.Visible = false;
            this.gameTimer.Start();
        }
    }
}
