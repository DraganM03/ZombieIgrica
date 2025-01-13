using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// moji importi
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;

namespace Igrica
{
    internal class Player
    {
        //stats
        public int x;
        public int y;
        public int speed;
        public float angle;
        public int s = 1;
        public int ammo;
        public int mags;
        private int gunCap;
        private int health;
        private int xp;
        private int level;
        private int dmg;

        //assets
        public Image image;

        public Player(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
            this.speed = 10;
            this.angle = -90;
            this.health = 100;
            this.xp = 0;
            this.level = 1;
            this.dmg = 10;
            this.image = Properties.Resources.playerR;

            this.gunCap = 10;
            this.ammo = this.gunCap;
            this.mags = 3; 
        }

        public void TakeDamage(int dmg)
        {
            this.health -= dmg;
        }

        public void GainXP(int xp)
        {
            this.xp += xp;
            if(xp > 10*this.level)
            {
                xp = xp % (10*this.level);
                this.level += 1;
            }
        }

        public void Move(int dx, int dy)
        {
            this.x += dx;
            this.y += dy;
        }

        public bool isAlive()
        {
            return this.health > 0;
        }

        public int getHealth()
        {
            return this.health;
        }
        
        public string getAmmoCount()
        {
            return this.ammo.ToString() + " / " + this.mags.ToString();
        }

        public Bullet Shoot(Form form, float angle)
        {

            if (ammo > 0)
            {
                this.ammo -= 1;
                Bullet b = new Bullet(this.x + this.image.Width / 2f - 20, 
                                        this.y + this.image.Height/2f + 2, angle, this.s, form); 
                return b;
            }
            CallResuply();
            return null;
        }

        public void CallResuply()
        {
            if (mags > 0)
            {
                this.ammo = this.gunCap;
                this.mags -= 1;
            }
        }

    }
}
