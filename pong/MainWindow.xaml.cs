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
        private DispatcherTimer timer;
        private Ball ball;
        private Player mousePlayer;
        private Player keyboardPlayer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdatePlayerScores()
        {
            KeyboardPlayer.Content = keyboardPlayer.Points.ToString();
            MousePlayer.Content = mousePlayer.Points.ToString();
        }

        private void Frame_Tick(object? sender, EventArgs e)
        {
            if (ball.X <= 0)
            {
                mousePlayer.Points += 1;
                UpdatePlayerScores();
                ball.Reset();
            }
            if (ball.X >= ball.Canvas.Width)
            {
                keyboardPlayer.Points += 1;
                UpdatePlayerScores();
                ball.Reset();
            }

            //Obliczenia pozycji piłki dla gracza na klawiaturze
            if (ball.Y >= keyboardPlayer.Y 
                && ball.Y <= keyboardPlayer.Y + keyboardPlayer.Height 
                && ball.X <= keyboardPlayer.X + keyboardPlayer.Width 
                && ball.X >= keyboardPlayer.X)
            {
                ball.DirectionX *= -1;
            }
            //Obliczenia pozycji piłki dla gracza na myszce
            if (ball.Y >= mousePlayer.Y 
                && ball.Y <= mousePlayer.Y + mousePlayer.Height 
                && ball.X >= mousePlayer.X - ball.Width 
                && ball.X <= mousePlayer.X + mousePlayer.Width)
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
                    if (keyboardPlayer.Y <= 0)
                        return;
                    keyboardPlayer.Y -= 10;
                    keyboardPlayer.Draw();
                    break;
                case Key.S:
                    if (keyboardPlayer.Y + keyboardPlayer.Height >= MainCanavs.Height)
                        return;
                    keyboardPlayer.Y += 10;
                    keyboardPlayer.Draw();
                    break;
                case Key.R:
                    mousePlayer.Reset();
                    keyboardPlayer.Reset();
                    ball.Reset();
                    UpdatePlayerScores();
                    break;
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.GetPosition(this).Y + mousePlayer.Height >= MainCanavs.Height)
                return;
            mousePlayer.Y = Mouse.GetPosition(this).Y;
            mousePlayer.Draw();
        }
    }
}