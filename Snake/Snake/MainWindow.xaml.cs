using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeGame
{
    public partial class MainWindow : Window
    {
        private readonly int cellSize = 20;
        private readonly int gridSize = 20;
        private List<Point> snake = new List<Point>();
        private Point food;
        private string direction = "Right";
        private DispatcherTimer timer = new DispatcherTimer();
        private Random rand = new Random();
        private Point? specialFood;
        private Brush snakeColor = Brushes.Green;


        public MainWindow()
        {
            InitializeComponent();
            InitGame();
            this.KeyDown += Window_KeyDown;
        }

        private void InitGame()
        {
            snake.Clear();
            snake.Add(new Point(5, 5));
            GenerateFood();
            direction = "Right";
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += GameLoop;
            timer.Start();
            GameCanvas.Children.Clear();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            MoveSnake();
            CheckCollision();
            DrawGame();
        }
        private void MoveSnake()
        {
            Point head = snake[0];
            Point newHead = head;

            switch (direction)
            {
                case "Up": newHead.Y -= 1; break;
                case "Down": newHead.Y += 1; break;
                case "Left": newHead.X -= 1; break;
                case "Right": newHead.X += 1; break;
            }

            snake.Insert(0, newHead);

            if (newHead == food)
            {
                GenerateFood();
                ScoreBoard.Text = $"Score: {snake.Count - 1}";
            }
            else if (newHead == specialFood)
            {
                specialFood = null;
                snakeColor = new SolidColorBrush(Color.FromRgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256)));
                GenerateFood();
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }

        private void GenerateFood()
        {
            Point position;
            do
            {
                position = new Point(rand.Next(gridSize), rand.Next(gridSize));
            }
            while (snake.Contains(position));

            food = position;


            if (rand.Next(5) == 0)
            {
                specialFood = position;
            }
            else
            {
                specialFood = null;
            }
        }

        private void CheckCollision()
        {
            Point head = snake[0];

            if (head.X < 0 || head.Y < 0 || head.X >= gridSize || head.Y >= gridSize || snake.GetRange(1, snake.Count - 1).Contains(head))
            {
                timer.Stop();
                MessageBox.Show("Game Over!");
                Application.Current.Shutdown();
            }
        }

        private void DrawGame()
        {
            GameCanvas.Children.Clear();

            foreach (Point part in snake)
            {
                DrawRectangle(part, snakeColor);
            }

            DrawEllipse(food, Brushes.Red);

            if (specialFood.HasValue)
            {
                DrawSpecial(specialFood.Value, new SolidColorBrush(Color.FromRgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256))));
            }
        }

        private void DrawRectangle(Point point, Brush color)
        {
            Rectangle rect = new Rectangle
            {
                Width = cellSize,
                Height = cellSize,
                Fill = color
            };
            Canvas.SetLeft(rect, point.X * cellSize);
            Canvas.SetTop(rect, point.Y * cellSize);
            GameCanvas.Children.Add(rect);

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up when direction != "Down": direction = "Up"; break;
                case Key.Down when direction != "Up": direction = "Down"; break;
                case Key.Left when direction != "Right": direction = "Left"; break;
                case Key.Right when direction != "Left": direction = "Right"; break;
            }
        }
        private void DrawEllipse(Point point, Brush color)
        {
            Ellipse ell = new Ellipse
            {
                Width = cellSize,
                Height = cellSize,
                Fill = color
            };
            Canvas.SetLeft(ell, point.X * cellSize);
            Canvas.SetTop(ell, point.Y * cellSize);
            GameCanvas.Children.Add(ell);
        }
        private void DrawSpecial(Point point, Brush color)
        {
            Ellipse ell = new Ellipse
            {
                Width = cellSize,
                Height = cellSize,
                Fill = color
            };
            Canvas.SetLeft(ell, point.X * cellSize);
            Canvas.SetTop(ell, point.Y * cellSize);
            GameCanvas.Children.Add(ell);

        }
    }
}