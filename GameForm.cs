using BallGames.Common;
using System.Drawing;
using Timer = System.Windows.Forms.Timer;

namespace FruitNinjaWinFormsApp
{
    public partial class GameForm : Form
    {
        private List<FruitBall> balls = new List<FruitBall>();
        private List<FruitBall> toDeleteBalls = new List<FruitBall>();

        private Timer shootBallTimer = new Timer();
        private Timer updateFormTimer = new Timer();
        private Timer yellowBallTimer = new Timer();
        private int yellowTimerCount;
        private const int startX = 700;

        private Random random = new Random();
        private Color backColor;
        private Brush normalBrush = Brushes.Silver;
        private Brush missedBrush = Brushes.Red;
        private int missedBallCount;
        private int catchedBallCount;

        public GameForm()
        {
            InitializeComponent();

            backColor = Color.FromArgb(244, 226, 184);
            BackColor = backColor;

            Width = 900;
            Height = 500;
            StartPosition = FormStartPosition.CenterScreen;

            Bitmap b = new Bitmap(Resources.cursor1);
            IntPtr ptr = b.GetHicon();
            Cursor = new Cursor(ptr);
        }

        public void InitializeUpdateFormTimer()
        {
            updateFormTimer.Interval = 30;
            updateFormTimer.Tick += UpdateFormTimer_Tick;
            updateFormTimer.Start();
        }

        public void InitializeShootBallTimer()
        {
            shootBallTimer.Interval = 3000;
            shootBallTimer.Tick += Timer_Tick;
            shootBallTimer.Start();
        }

        private void UpdateFormTimer_Tick(object? sender, EventArgs e)
        {
            Invalidate();

            foreach (var ball in balls.ToList())
            {
                if (ball.CenterY > Height)
                {
                    if (ball.IsBlack())
                        continue;

                    balls.Remove(ball);
                    missedBallCount++;
                    ballsLabel.Text = missedBallCount.ToString();

                    Invalidate();

                    if (missedBallCount == 3)
                    {
                        StopGame();
                        UserDialog();
                    }
                }      
            }
            if (catchedBallCount > 10 && yellowBallTimer.Enabled == false)
                SetTimerInterval(catchedBallCount);
        }

        private void SetTimerInterval(int count)
        {
            if (count < 15)
                updateFormTimer.Interval = 25;
            else if (count < 20)
                updateFormTimer.Interval = 20;
            else if (count < 25)
                updateFormTimer.Interval = 15;
            else
                updateFormTimer.Interval = 10;
        }

        private void UserDialog()
        {
            DialogResult dialogResult = MessageBox.Show($"Вы набрали {catchedBallCount} очков. Сыграете еще раз?", "Fruit Ninja", MessageBoxButtons.YesNo); ; ; ;

            if (dialogResult == DialogResult.Yes)
            {
                StartGame();
            }
            else if (dialogResult == DialogResult.No)
            {
                Application.Exit();
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            ShootFruit();
        }

        private void ShootFruit()
        {
            int[] fruitNumbers = { 1, 1, 1, 1, 2, 2, 2, 3 };
            var randomIndex = random.Next(fruitNumbers.Length);

            for (int i = 0; i < fruitNumbers[randomIndex]; i++)
            {
                var newFruit = new FruitBall(Width, Height);
                balls.Add(newFruit);
            }
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            InitializeUpdateFormTimer();
            InitializeShootBallTimer();
            missedBallCount = 0;
            catchedBallCount = 0;
            ballsLabel.Text = missedBallCount.ToString();
        }

        private void StartGame() 
        {
            missedBallCount = 0;
            catchedBallCount = 0;

            catchedBallsLabel.Text = catchedBallCount.ToString();
            ballsLabel.Text = missedBallCount.ToString();

            shootBallTimer.Start();

            updateFormTimer.Interval = 30;
            updateFormTimer.Start();

            Invalidate();
        }

        private void StopGame()
        {
            shootBallTimer.Stop();
            updateFormTimer.Stop();
            countdownLabel.Visible = false;

            balls.Clear();
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            var count = missedBallCount;

            for (int i = 0; i < 3; i++) 
            {
                if (count > 0)
                {
                    DrawStar(e, missedBrush, startX + 50 * i);
                    count--;
                }
                else
                    DrawStar(e, normalBrush, startX + 50 * i);
            }

            foreach (var ball in balls.ToList())
            {
                if (toDeleteBalls.Contains(ball))
                {
                    if (ball.IsBlack())
                    {
                        StopGame();
                        UserDialog();
                        break;
                    }  
                    
                    if (ball.IsYellow())
                    {
                        if (yellowBallTimer.Enabled == false)
                            StartYellowTimer();
                        else
                            yellowTimerCount += 10;
                    }

                    balls.Remove(ball);
                    toDeleteBalls.Remove(ball);
                    catchedBallCount++;
                    catchedBallsLabel.Text = catchedBallCount.ToString();
                }
                Move(e, ball);
            }
        }

        private void StartYellowTimer()
        {
            yellowBallTimer.Interval = 1000;
            yellowBallTimer.Tick += YellowTimer_Tick;
            yellowBallTimer.Start();

            updateFormTimer.Interval *= 2;
            yellowTimerCount = 10;
            
            countdownLabel.Text = $"Скорость замедлена на {yellowTimerCount} секунд";
            countdownLabel.Visible = true;
        }

        private void YellowTimer_Tick(object? sender, EventArgs e)
        {
            yellowTimerCount--;
            countdownLabel.Text = $"Скорость замедлена на {yellowTimerCount} секунд";

            if (yellowTimerCount <= 0)
            {
                yellowBallTimer.Stop();
                countdownLabel.Visible = false;
                updateFormTimer.Interval /= 2;
            }
        }

        private void Move(PaintEventArgs e, FruitBall ball)
        {
            Draw(e, ball, new SolidBrush(backColor));

            ball.Move();

            Draw(e, ball, ball.Brush);
        }

        private void Draw(PaintEventArgs e, FruitBall ball, Brush brush)
        {
            var rectangle = new RectangleF(ball.CenterX - ball.Radius, ball.CenterY - ball.Radius, 2 * ball.Radius, 2 * ball.Radius);
            var graphics = e.Graphics;
            graphics.FillEllipse(brush, rectangle);
        }

        private void DrawStar(PaintEventArgs e, Brush brush, int startPointX)
        {
            var point = new Point[]
            {
                new Point(startPointX, 20),
                new Point(startPointX - 10, 30),
                new Point(startPointX, 40),
                new Point(startPointX - 10, 50),
                new Point(startPointX, 60),
                new Point(startPointX + 10, 50),
                new Point(startPointX + 20, 60),
                new Point(startPointX + 30, 50),
                new Point(startPointX + 20, 40),
                new Point(startPointX + 30, 30),
                new Point(startPointX + 20, 20),
                new Point(startPointX + 10, 30)
            };

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddPolygon(point);

            Region r = new Region(gp);
            var graphics = e.Graphics;
            graphics.FillRegion(brush, r);
        }

        private void GameForm_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var ball in balls.ToList())
            {
                if (ball.IsOnClick(e.X, e.Y))
                {
                    toDeleteBalls.Add(ball);
                }
            }
        }
    }
}
