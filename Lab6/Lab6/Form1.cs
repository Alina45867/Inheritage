using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class Form1 : Form
    {
        private Circle[] circle;
        private Rectangle[] rectangle;
        public Form1()
        {
            InitializeComponent();
            Random random = new Random();
            circle = new Circle[]
            {
                new Circle(this,random.Next(20,70),Color.FromArgb(random.Next(0, 255),random.Next(0, 255),random.Next(0, 255)),Color.FromArgb(random.Next(0, 255),random.Next(0, 255),random.Next(0, 255))),
                new Circle(this,random.Next(20,70),Color.FromArgb(random.Next(0, 255),random.Next(0, 255),random.Next(0, 255)),Color.FromArgb(random.Next(0, 255),random.Next(0, 255),random.Next(0, 255))),
                new Circle(this,random.Next(20,70),Color.FromArgb(random.Next(0, 255),random.Next(0, 255),random.Next(0, 255)),Color.FromArgb(random.Next(0, 255),random.Next(0, 255),random.Next(0, 255))),
                new Circle(this,random.Next(20,70),Color.FromArgb(random.Next(0, 255),random.Next(0, 255),random.Next(0, 255)),Color.FromArgb(random.Next(0, 255),random.Next(0, 255),random.Next(0, 255))),
            };
            rectangle = new Rectangle[]
            {
                new Rectangle(this,random.Next(30,150),random.Next(20,100),Color.FromArgb(random.Next(0, 255),random.Next(0, 255),random.Next(0, 255)),Color.FromArgb(random.Next(0, 255),random.Next(0, 255),random.Next(0, 255))),
                new Rectangle(this,random.Next(30,150),random.Next(20,100),Color.FromArgb(random.Next(0, 255),random.Next(0, 255),random.Next(0, 255)),Color.FromArgb(random.Next(0, 255),random.Next(0, 255),random.Next(0, 255))),
                new Rectangle(this,random.Next(30,150),random.Next(20,100),Color.FromArgb(random.Next(0, 255),random.Next(0, 255),random.Next(0, 255)),Color.FromArgb(random.Next(0, 255),random.Next(0, 255),random.Next(0, 255))),
            };
            label1.Text = "";
            label2.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < circle.Length; i++)
            {
                if (circle[i].Check(new Point(e.X, e.Y)))
                {
                    label1.Text = "Площадь= " + Math.Round(circle[i].Area(), 2).ToString();
                    circle[i].Draw(true);
                    label2.Text = "Вы попали в круг";
                }
            }
            for (int i=0;i<rectangle.Length;i++)
            {
                if (rectangle[i].Check(new Point(e.X,e.Y)))
                {
                    label1.Text = "Площадь= " + rectangle[i].Area().ToString();
                    rectangle[i].Draw(true);
                    label2.Text = "Вы попали в прямоугольник";
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i=0;i<circle.Length;i++)
            circle[i].Draw();
            for (int i = 0; i < rectangle.Length; i++)
             rectangle[i].Draw();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
    public class Figures
    {
        protected Point centre; //центр 
        protected float thickness;//толщина 
        private Color insidecolor;//цвет внутри
        private Color line;//цвет линии
        static protected Random randomizer = new Random();
        public Color Insidecolor 
        { 
            get
            {
                return insidecolor;
            }
        }
        public Color Line
        { 
            get
            {
                return line;
            }
        }
        public Figures(Color insidecolor, Color line)
        {
            this.insidecolor = insidecolor;
            this.line = line;
        }
        protected void ChangeColor (Color insidecolor, Color line)// метод смены цвета 
        {
            this.insidecolor = insidecolor;
            this.line = line;
        }
    }
    class Circle: Figures
    {
        private Form form;
        private int r;
        private Graphics graphics;
        public Circle(Form form, int r, Color insidecolor, Color line) : base(insidecolor, line)
        {
            this.form = form;
            this.r = r;
            centre = new Point()
            {
               X = randomizer.Next(r, form.ClientSize.Width - r),
               Y= randomizer.Next(r, form.ClientSize.Height - r)
            };
            thickness = 1;
            graphics = form.CreateGraphics();

        }
        public void Draw(bool fillRandom=false)
        {
            if (fillRandom)
            {
                int x = centre.X - r;
                int y = centre.Y - r;
                ChangeColor(Color.FromArgb(randomizer.Next(0, 255), randomizer.Next(0, 255), randomizer.Next(0, 255)), Color.FromArgb(randomizer.Next(0, 255), randomizer.Next(0, 255), randomizer.Next(0, 255)));
                thickness = randomizer.Next(2,15);
                graphics.FillEllipse(new SolidBrush(Insidecolor), x, y, r * 2, r * 2);
                graphics.DrawEllipse(new Pen(Line, thickness), x, y, r * 2, r * 2);
            }
            else
            {
                Draw();
            }
        }
        private void Draw()
        {
            int x = centre.X - r;
            int y = centre.Y - r;;
            graphics.FillEllipse(new SolidBrush(Insidecolor), x, y, r * 2, r * 2);
            graphics.DrawEllipse(new Pen(Line, thickness), x, y, r * 2, r * 2);
        }
        public double Area()
        {
            return Math.Pow(r, 2) * Math.PI;
        }
        public bool Check(Point point)
        {
            if (Math.Sqrt(Math.Pow(centre.X - point.X, 2) + Math.Pow(centre.Y - point.Y, 2)) <= r)
                return true;
            return false;
        }
    }
    class Rectangle : Figures
    {
        private Form form;
        private int wight;
        private int height;
        private Graphics graphics;
        public Rectangle(Form form, int wight, int height, Color insidecolor, Color line) : base(insidecolor, line)
        {
            this.form = form;
            this.wight = wight;
            this.height = height;
            centre = new Point()
            {
                X = randomizer.Next(wight/2, form.ClientSize.Width - wight/2),
                Y = randomizer.Next(height, form.ClientSize.Height - height/2)
            };
            thickness = 1;
            graphics = form.CreateGraphics();
        }
        public void Draw (bool fillRandom = false )
        {
            if (fillRandom)
            {
                int x = centre.X - wight / 2;
                int y = centre.Y - height / 2;
                ChangeColor(Color.FromArgb(randomizer.Next(0, 255), randomizer.Next(0, 255), randomizer.Next(0, 255)), Color.FromArgb(randomizer.Next(0, 255), randomizer.Next(0, 255), randomizer.Next(0, 255)));
                thickness = randomizer.Next(3,16);
                graphics.FillRectangle(new SolidBrush(Insidecolor), x + (int)(thickness / 2),y+(int)(thickness/2), wight, height);
                graphics.DrawRectangle(new Pen(Line, thickness), x, y, wight, height);
            }
            else
            {
                Draw();
            }
        }
        private void Draw()
        {
            int x = centre.X - wight / 2;
            int y = centre.Y - height / 2;
            graphics.FillRectangle(new SolidBrush(Insidecolor), x + (int)(thickness / 2), y + (int)(thickness / 2), wight, height);
            graphics.DrawRectangle(new Pen(Line, thickness), x, y, wight, height);
        }
        public int Area()
        {
            return wight * height;
        }
        public bool Check(Point point)
        {
            if ((point.X >= centre.X - wight / 2) && (point.X <= centre.X + wight / 2) && (point.Y >= centre.Y - height / 2) && (point.Y <= centre.Y+ height / 2))
            { 
                return true;
            }
            else 
            return false;
        }
    }
}
