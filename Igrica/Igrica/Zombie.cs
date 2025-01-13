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

        private int hp;
        private int dmg;

        public Image img;

        public Zombie(int x, int y, float angle)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
            this.speed = 3;
            this.hp = 20;
            this.dmg = 15;
            this.img = Properties.Resources.zombieR;
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
    }
}
