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
        private static readonly SolidColorBrush FOOD = new SolidColorBrush(Color.FromRgb(255, 130, 50));

        public static readonly int row_num = 8;
        public static readonly int col_num = 8;

        private int counter = 0;
        //Previsto per un frutto la volta. Cambiare con un array di posizioni per piu' frutti
        private int xFood;
        private int yFood;
        private Boolean foodSpawned = false;

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
            Vector2D direction = this.snake.direction;
            
         
            if (debug_console.Content.ToString().Equals("S") && !this.snake.strDirection.Equals("A"))
            {
                this.snake.strDirection = "G"; //Giu
                direction = new Vector2D(0, 1); //Giu' = 0,1
            }
            else if (debug_console.Content.ToString().Equals("W") && !this.snake.strDirection.Equals("G"))
            {
                this.snake.strDirection = "A"; //Alto
                direction = new Vector2D(0, -1); //Sopra = 0, -1
            }
            else if (debug_console.Content.ToString().Equals("A") && !this.snake.strDirection.Equals("D"))
            {
                this.snake.strDirection = "S"; //Sinistra
                direction = new Vector2D(-1, 0); //Sinistra = -1, 0
            }
            else if (debug_console.Content.ToString().Equals("D") && !this.snake.strDirection.Equals("S"))
            {
                this.snake.strDirection = "D"; //Destra
                direction = new Vector2D(1, 0); //Destra = 1,0
            }
            this.snake.move(direction);
            foreach (Rectangle tile in this.tiles)
            {
                if (((SolidColorBrush)tile.Fill).Color != FOOD.Color) //Evito di cancellare il cibo
                    tile.Fill = EMPTY_TILE_COLOR;
            }
            foreach (Vector2D point in this.snake.position)
            {
                //Normalizzo la direzione in base alla griglia.
                //Se il serpente esce fuori dal bordo, coloro la parte opposta
                if (point.y == -1)
                {
                    point.y = 7;
                }
                if (point.x == -1)
                {
                    point.x = 7;
                }
                this.tiles[point.y, point.x].Fill = SNAKE_TILE_COLOR;
            }

            if (!foodSpawned)
                spawnFood();
            eatFood();
        }

        private void spawnFood()
        {
            Random rnd = new Random();
            Boolean ok;
            int x;
            int y;

            do
            {
                x = rnd.Next(8); // Genera un numero casuale da 0 a 8
                y = rnd.Next(8); // Genera un numero casuale da 0 a 8

                ok = checkPositionFood(x, y);
            } while (!ok);

            xFood = x;
            yFood = y;
            this.tiles[y, x].Fill = FOOD;
            foodSpawned = true;

        }

        private Boolean checkPositionFood(int x, int y)
        {
            Boolean b = true;
            foreach (Vector2D point in this.snake.position)
            {
                if (point.x == x && point.y == y)
                {
                    b = false;
                    break;
                }
            }

            return b;
        }

        private void eatFood()
        {
            if (this.snake.position[^1].x == xFood && this.snake.position[^1].y == yFood)
            {
                this.tiles[yFood, xFood].Fill = SNAKE_TILE_COLOR;

                if (this.snake.position[0].y == this.snake.position[1].y)
                {
                    if (((SolidColorBrush)this.tiles[this.snake.position[0].y, this.snake.position[0].x - 1].Fill).Color != SNAKE_TILE_COLOR.Color)
                        this.snake.position.Insert(0, new Vector2D(this.snake.position[0].x - 1, this.snake.position[0].y));
                    else
                        this.snake.position.Insert(0, new Vector2D(this.snake.position[0].x + 1, this.snake.position[0].y));
                }
                else if (this.snake.position[0].x == this.snake.position[1].x)
                { //Sara' sempre vero altrimenti
                    if (((SolidColorBrush)this.tiles[this.snake.position[0].y - 1, this.snake.position[0].x].Fill).Color != SNAKE_TILE_COLOR.Color)
                        this.snake.position.Insert(0, new Vector2D(this.snake.position[0].x, this.snake.position[0].y - 1));
                    else
                        this.snake.position.Insert(0, new Vector2D(this.snake.position[0].x, this.snake.position[0].y + 1));
                }


                foodSpawned = false;
                counter++;
            }
            if (!foodSpawned)
                spawnFood();
            eatFood();


        private void read_keyboard_input(object sender, KeyEventArgs e)
        {
            debug_console.Content = e.Key;
        }
    }
}
