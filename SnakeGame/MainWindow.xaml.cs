using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Input;

namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static readonly SolidColorBrush EMPTY_TILE_COLOR = new SolidColorBrush(Color.FromRgb(100, 100, 150));
        private static readonly SolidColorBrush SNAKE_TILE_COLOR = new SolidColorBrush(Color.FromRgb(100, 150, 100));

        public static readonly int row_num = 8;
        public static readonly int col_num = 8;

        private int counter = 0;

        private Rectangle[,] tiles = new Rectangle[row_num, col_num];

        private Snake snake = new Snake(row_num, col_num);

        public MainWindow()
        {
            InitializeComponent();
            this.init_game_grid();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(update);
            timer.Start();
        }


        private void update(object? sender, EventArgs? e)
        {
            this.snake.move();
            foreach (Rectangle tile in this.tiles) { tile.Fill = EMPTY_TILE_COLOR; }
            foreach (Vector2D point in this.snake.position) { this.tiles[point.y, point.x].Fill = SNAKE_TILE_COLOR; }
        }


        private void init_game_grid()
        {
            for (int row = 0; row < row_num; row++)
            {
                for (int col = 0; col < col_num; col++)
                {
                    this.tiles[row, col] = new Rectangle();
                    this.tiles[row, col].Margin = new Thickness(1);
                    this.tiles[row, col].Fill = EMPTY_TILE_COLOR;
                    main_grid.Children.Add(tiles[row, col]);
                }
            }
        }

        private void read_keyboard_input(object sender, KeyEventArgs e)
        {
            debug_console.Content = e.Key;
        }
    }
}
