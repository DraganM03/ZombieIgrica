using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// moji importi
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace Igrica
{
    internal class Bullet
    {
        public float x; 
        public float y;
        public int s;
        public float angle;
        public bool destroy;

        private int speed = 20; //br ms za timer i brzina kretanja projektila
        private int maxW = 0;
        private int maxH = 0;

        public Pen bulletPen = new Pen(Color.White, 2);
        private Timer bulletTimer = new Timer();
        private Form form;

        // generisanje projektila
        public Bullet(float x, float y,float angle, int s, Form form)
        {

            this.maxH = form.Height;
            this.maxW = form.Width;
            this.form = form;
            this.x = x;
            this.y = y;
            this.s = s;
            this.angle = angle;
            this.destroy = false;

            this.bulletTimer.Interval = speed;
            this.bulletTimer.Tick += new EventHandler(BulletTimerEvent); // funkcija koju radi svakih {speed} ms
            this.bulletTimer.Start();

        }

        private void BulletTimerEvent(object sender, EventArgs e)
        {

            x = (float)(this.x + this.speed * Math.Cos(this.angle * Math.PI / 180));
            y = (float)(this.y + this.speed * Math.Sin(this.angle * Math.PI / 180));


            if (this.x < 0 || this.x > this.maxW || this.y < 50 || this.y > maxH - 10)
            {
                this.bulletTimer.Stop();
                this.bulletTimer.Dispose();  // uklanja timer
                this.bulletTimer = null;
                this.destroy = true;

            }
        }

        public int getSpeed()
        {
            return this.speed;
        }
    }

}
