using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroSnaker
{
    public partial class Form1 : Form
    {
        private Form gb;
        private string keyName = "start";
        private Label[] snakeBody = new Label[3000];
        private Random r = new Random();
        private int snakeBodyContentX = 0, snakeBodyContentY = 0;
        private int score;

        private void Form1_Load(object sender, EventArgs e)
        {
            Top = 120;
            Left = 120;
            Width = 518;
            Height = 560;
            BackColor = Color.Blue;
            for (int i = 0; i < 8; i++)
            {
                Label snakeBodyContent = new Label();
                snakeBodyContent.Height = 10;
                snakeBodyContent.Width = 10;
                snakeBodyContent.Top = 400;
                snakeBodyContent.Left = 400 - i * 10;
                BackColor = Color.White;
                snakeBodyContent.ForeColor = Color.Green;
                snakeBodyContent.Text = "❤";
                snakeBodyContent.Tag = i;
                snakeBody[i] = snakeBodyContent;
                Controls.Add(snakeBodyContent);
            }
            timer1.Interval = GameBegin.timeInterval;
            timer1.Tick += new EventHandler(timer1_Tick);
            KeyDown += new KeyEventHandler(Form1_KeyDown);
            SnakeFood();
            timer1.Start();
        }

        public void SnakeFood()
        {
            Label food = new Label();
            food.Width = 20;
            food.Height = 20;
            food.Top = r.Next(1, 20) * 20;
            food.Left = r.Next(1, 20) * 20;
            food.ForeColor = Color.Red;
            food.Text = "🍎";
            food.Tag = "food";
            food.BackColor = Color.Transparent;
            Controls.Add(food);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int x, y;
            x = snakeBody[0].Left;
            y = snakeBody[0].Top;
            keyName = e.KeyCode.ToString();
            if (keyName == "Right")
            {
                snakeBody[0].Left = x + 10;
                SnakeMove(x, y);
                SnakeOver();
            }
            if (keyName == "Up")
            {
                snakeBody[0].Top = y - 10;
                SnakeMove(x, y);
                SnakeOver();
            }
            if (keyName == "Down")
            {
                snakeBody[0].Top = y + 10;
                SnakeMove(x, y);
                SnakeOver();
            }
            if (keyName == "Left")
            {
                snakeBody[0].Left = x - 10;
                SnakeMove(x, y);
                SnakeOver();
            }
            EatTime();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int x, y;
            x = snakeBody[0].Left;
            y = snakeBody[0].Top;
            if (keyName == "start")
            {
                snakeBody[0].Left = x + 10;
                SnakeMove(x, y);
                SnakeOver();
            }
            if (keyName == "Right")
            {
                snakeBody[0].Left = x + 10;
                SnakeMove(x, y);
                SnakeOver();
            }
            if (keyName == "Up")
            {
                snakeBody[0].Top = y - 10;
                SnakeMove(x, y);
                SnakeOver();
            }
            if (keyName == "Down")
            {
                snakeBody[0].Top = y + 10;
                SnakeMove(x, y);
                SnakeOver();
            }
            if (keyName == "Left")
            {
                snakeBody[0].Left = x - 10;
                SnakeMove(x, y);
                SnakeOver();
            }
            if (x > 518)
            {
                snakeBody[0].Left = 0;
            }
            if (x < 0)
            {
                snakeBody[0].Left = 518;
            }
            if (y > 480)
            {
                snakeBody[0].Top = 0;
            }
            if (y < 0)
            {
                snakeBody[0].Top = 480;
            }
            EatTime();
        }

        public void SnakeMove(int x, int y)
        {
            int tempX = 0, tempY = 0;
            for (int i = 1; snakeBody[i] != null; i++)
            {
                if (i >= 3)
                {
                    tempX = snakeBodyContentX;
                    tempY = snakeBodyContentY;
                }
                if (i == 1)
                {
                    tempX = snakeBody[i].Left;
                    tempY = snakeBody[i].Top;
                    snakeBody[i].Left = x;
                    snakeBody[i].Top = y;
                } else
                {
                    snakeBodyContentX = snakeBody[i].Left;
                    snakeBodyContentY = snakeBody[i].Top;
                    snakeBody[i].Left = tempX;
                    snakeBody[i].Top = tempY;
                }
            }
        }

        public void EatTime()
        {
            int x1 = 20, y1 = 20, x2 = 20, y2 = 20;
            foreach (Label lb in Controls)
            {
                if (lb.Tag.ToString() == "food")
                {
                    x2 = lb.Left;
                    y2 = lb.Top;
                }
                if (lb.Tag.ToString() == "0")
                {
                    x1 = lb.Left;
                    y1 = lb.Top;
                }
            }
            if (x2 == x1 && y2 == y1)
            {
                SnakeEat();
                foreach (Label lb in Controls)
                {
                    if (lb.Tag.ToString() == "food")
                    {
                        lb.Top = r.Next(1, 20) * 20;
                        lb.Left = r.Next(1, 20) * 20;
                    }
                }
            }
        }

        public void SnakeEat()
        {
            int i = 0;
            for (; snakeBody[i] != null; i++);
            Label snakeBodyContent = new Label();
            snakeBodyContent.Width = 10;
            snakeBodyContent.Height = 10;
            snakeBodyContent.Top = snakeBodyContentY;
            snakeBodyContent.Left = snakeBodyContentX;
            snakeBodyContent.ForeColor = Color.Green;
            snakeBodyContent.BackColor = Color.White;
            snakeBodyContent.Text = "❤";
            snakeBodyContent.Tag = i;
            snakeBody[i] = snakeBodyContent;
            Controls.Add(snakeBodyContent);
            score++;
            label1.Text = string.Format("分数：{0}", score);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int col = 50;
            int row = 50;
            int drawRow = 0;
            int drawCol = 0;
            Pen black = new Pen(Color.Gray, 1);
            Graphics g = CreateGraphics();
            for (int i = 0; i <= row; i++)
            {
                g.DrawLine(black, 0, drawCol, 500, drawCol);
                drawCol += 10;
            }
            for (int j = 0; j <= col; j++)
            {
                g.DrawLine(black, drawRow, 0, drawRow, 500);
                drawRow += 10;
            }
        }

        public void SnakeOver()
        {
            int x, y;
            x = snakeBody[0].Left;
            y = snakeBody[0].Top;
            foreach (Label lb in Controls)
            {
                if (lb.Tag.ToString() != "food")
                {
                    if ((lb.Left == x && lb.Top == y) && lb.Tag.ToString() != "0")
                    {
                        Close();
                        MessageBox.Show(string.Format("GAME OVER!\n分数：{0}", score), "提示！");
                        gb.Show();
                    }
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            gb.Show();
        }

        public Form1(Form gb)
        {
            this.gb = gb;
            score = 0;
            InitializeComponent();
            label1.Text = string.Format("分数：{0}", score);
        }
    }
}
