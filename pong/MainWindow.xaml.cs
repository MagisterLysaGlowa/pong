using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace pong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer game_timer;
        private Ball ball;
        private Player player_mouse;
        private Player player_keyboard;

        public MainWindow()
        {
            InitializeComponent();
            //Utworzenie podstawowych obiektów

            ball = 
                new Ball(10, 10, MainCanavs);
            player_mouse = 
                new Player(MainCanavs, 10, 100, new SolidColorBrush(Colors.White), false);
            player_keyboard = 
                new Player(MainCanavs, 10, 100, new SolidColorBrush(Colors.White), true);

            //Obsługa FPS
            game_timer = new DispatcherTimer();
            game_timer.Interval = TimeSpan.FromMilliseconds(16);
            game_timer.Tick += Frame_Tick;
            game_timer.Start();
        }

        private void UpdatePlayerScores()
        {
            KeyboardPlayer.Content = player_keyboard.Points.ToString();
            MousePlayer.Content = player_mouse.Points.ToString();
        }

        private void Frame_Tick(object? sender, EventArgs e)
        {
            if (ball.X <= 0)
            {
                player_mouse.Points += 1;
                UpdatePlayerScores();
                ball.Reset();
            }
            if (ball.X >= ball.Canvas.Width)
            {
                player_keyboard.Points += 1;
                UpdatePlayerScores();
                ball.Reset();
            }

            //Obliczenia pozycji piłki dla gracza na klawiaturze
            if (ball.Y >= player_keyboard.Y 
                && ball.Y <= player_keyboard.Y + player_keyboard.Height 
                && ball.X <= player_keyboard.X + player_keyboard.Width 
                && ball.X >= player_keyboard.X)
            {
                ball.DirectionX *= -1;
            }
            //Obliczenia pozycji piłki dla gracza na myszce
            if (ball.Y >= player_mouse.Y 
                && ball.Y <= player_mouse.Y + player_mouse.Height 
                && ball.X >= player_mouse.X - ball.Width 
                && ball.X <= player_mouse.X + player_mouse.Width)
            {
                ball.DirectionX *= -1;
            }
            ball.Move();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    this.Close();
                    break;
                case Key.W:
                    if (player_keyboard.Y <= 0)
                        return;
                    player_keyboard.Y -= 10;
                    player_keyboard.Draw();
                    break;
                case Key.S:
                    if (player_keyboard.Y + player_keyboard.Height >= MainCanavs.Height)
                        return;
                    player_keyboard.Y += 10;
                    player_keyboard.Draw();
                    break;
                case Key.R:
                    player_mouse.Reset();
                    player_keyboard.Reset();
                    ball.Reset();
                    UpdatePlayerScores();
                    break;
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.GetPosition(this).Y + player_mouse.Height >= MainCanavs.Height)
                return;
            player_mouse.Y = Mouse.GetPosition(this).Y;
            player_mouse.Draw();
        }
    }
}