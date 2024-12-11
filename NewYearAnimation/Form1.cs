using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewYearAnimation
{
    public partial class Form1 : Form
    {
        private List<Tree> list = new List<Tree>();
        private List<Snowflake> snowflakes = new List<Snowflake>();
        private List<House> houses = new List<House>();
        private Timer timer;
        private Random rand = new Random();
        private Rink rink;
        private List <Character> character = new List<Character>();
        private Image imgCh1 = Image.FromFile("D:\\Veronika\\NewYearAnimation\\CharacterI.png");
        private Image imgCh2 = Image.FromFile("D:\\Veronika\\NewYearAnimation\\CharacterII.png");
        private Image imgCh3 = Image.FromFile("D:\\Veronika\\NewYearAnimation\\CharacterIII.png");

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            this.Size = new Size(700, 700);
            this.Paint += new PaintEventHandler(this.OnPaint);
            CreateObjects();

            timer = new Timer();
            timer.Interval = 30;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void CreateSnowflakes()
        {
            Random random = new Random();
            for (int i = 0; i < 50; i++)
            {
                snowflakes.Add(new Snowflake(
                    random.Next(ClientSize.Width),
                    random.Next(ClientSize.Height),
                    Color.White,
                    random.Next(5, 20),
                    random.Next(1, 5)
                ));
            }
        }

        private void AddCharacter()
        {
            character.Add(new Character(100, 400, 80, 3, imgCh1));
            character.Add(new Character(400, 500, 50, 3, imgCh2));
            character.Add(new Character(200, 600, 100, 3, imgCh3));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (Snowflake snowflake in snowflakes)
            {
                snowflake.Y += snowflake.Step;

                if (snowflake.Y > ClientSize.Height)
                {
                    snowflake.Y = -snowflake.Size;
                    snowflake.X = rand.Next(ClientSize.Width);
                }
            }
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (House house in houses)
            {
                house.Draw(g);
            }
            foreach (Tree tree in list)
            {
                tree.Draw(g);
            }
            rink.Draw(g);
            foreach (var item in character)
            {
                item.Draw(g);
                item.Move(Width);
            }
            foreach (Snowflake snowflake in snowflakes)
            {
                snowflake.Draw(g);
            }
        }

        private void CreateObjects()
        {
            AddCharacter();
            CreateSnowflakes();
            houses.Add(new House(400, 250, 100, 0, 4, Color.Red));
            houses.Add(new House(300, 250, 100, 0, 2, Color.Red));
            houses.Add(new House(200, 250, 100, 0, 3, Color.Red));
            rink = new Rink(0, 300, Color.CadetBlue, ClientSize.Width, 0);
            int x = 0;
            for (int i = 0; i < 10; i++)
            {
                list.Add(new Tree(
                   x,
                   300,
                   Color.White,
                   rand.Next(70, 105),
                   rand.Next(1, 5)
               ));
                x += 80;
            }
        }
    }
}
