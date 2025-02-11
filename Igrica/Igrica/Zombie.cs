using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// moji importi
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Igrica.Properties;

namespace Igrica
{
    internal class Zombie
    {
        public int x;
        public int y;
        public float angle;
        public int speed;
        public int lastHit;

        public int hp;
        public int dmg;
        private int px;
        private int py;
        public Image img;

        public Zombie(int x, int y, float angle)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
            this.speed = 1;
            this.hp = 20;
            this.dmg = 5;
            this.img = Properties.Resources.zombieR;
            this.lastHit = 0;

        }

        public bool takeDmg(int dmg)
        {
            this.hp -= dmg;
            if (this.hp < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void updatePXY(int x, int y)
        {
            this.px = x;
            this.py = y;
        }
        public void moveThowards(int px, int py, int pw, int ph)
        {
            int s = 1;
            if (px > this.x)
            {
                s = -1;
            }

            
            float dist = (float)Math.Sqrt((px+pw/2 - this.x+this.img.Width / 2) * (px+pw/2 - this.x+this.img.Width) + (py+ph/2 - this.y+this.img.Height / 2) * (py+ph/2 - this.y+ this.img.Height / 2));
            //if(dist == 0)
            //{
            //    dist += 0.01f;
            //}
            //this.angle = ((float)(Math.Acos((this.y+this.img.Height/2 - (py+ph/2))/dist ) / Math.PI) * 180.0f) + 90.0f;




            //this.x += (int)(this.speed * Math.Cos(this.angle));
            //this.y += (int)(this.speed * Math.Sin(this.angle));

            if (this.x > px && dist > 25)
            {
                this.x -= this.speed;
            }
            if (this.x < px && dist > 25)
            {
                this.x += this.speed;
            }
            if (this.y > py && dist > 25)
            {
                this.y -= this.speed;
            }
            if (this.y < py && dist > 25)
            {
                this.y += this.speed;
            }
        }

    }
}
