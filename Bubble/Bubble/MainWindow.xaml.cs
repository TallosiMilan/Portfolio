using System;
using System.Text;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Threading;

namespace PabloGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int score;
        private DispatcherTimer _dispatcherTimer;
        private int _counter = 20;
        private Random random = new Random();
        private Ellipse currentBubble;
        public MainWindow()
        {
            InitializeComponent();
            GenerateBubbles();
            OldTimler();
        }
        private void Vizsgalodas()
        {
            if (score >= 30)
            {
                GameCanvas.Children.Clear();
                _dispatcherTimer.Stop();
                MessageBox.Show($"Gratulálok! Sikeresen kinyomtad az összes buborékot.\n\n A pontszámod: {score}", "Nyertél!");
                Application.Current.Shutdown();
            }
            else if (_counter == 0)
            {
                _dispatcherTimer.Stop();
                MessageBox.Show($"A játéknak vége. A bubikat nem bírtad megölni időben.\n\n A pontszámod: {score}", "Vesztettél!");
                Application.Current.Shutdown();
            }
        }
        private void GenerateBubbles()
        {
            int rad = random.Next(30, 100);
            currentBubble = new Ellipse()
            {
                Width = rad,
                Height = rad,
                Fill = new SolidColorBrush(Color.FromRgb(200, 200, 200))
            };


            double left = random.Next(0, (int)(GameCanvas.Width - currentBubble.Width));
            double top = random.Next(0, (int)(GameCanvas.Height - currentBubble.Height));
            currentBubble.MouseLeftButtonDown += Bubble_MouseLeftButtonDown;
            Canvas.SetLeft(currentBubble, left);
            Canvas.SetTop(currentBubble, top);
            GameCanvas.Children.Add(currentBubble);
        }
        private void Bubble_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Ellipse bubble)
            {
                GameCanvas.Children.Remove(bubble);
                score += random.Next(1, 3);
                Score.Text = $"Score: {score}";
                GenerateBubbles();
            }

        }
        private void OldTimler()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
            _dispatcherTimer.Start();
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            _counter--; // Increment the counter every time the timer ticks
            Timler.Text = $"Time: {_counter}"; // Update the text block with the counter value
            Vizsgalodas();
        }
    }
}
