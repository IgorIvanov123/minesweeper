using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] mines = new int[10, 10];
        Button[,] buttons = new Button[10, 10];
        int AllFreeSectors = 0;
		int coutOfmines = 2;
		
        public MainWindow()
        {
            InitializeComponent();
        }
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
			map.Rows = 10;
			map.Columns = 10;
        }
		public void newgame_Click(object sender, RoutedEventArgs e)
		{
			RefreshMap(mines, buttons, map);
		}
		public void options_Click(object sender, RoutedEventArgs e)
		{
			var options = new Options();
			var Window = new optionsWindow(options);
			var result = Window.ShowDialog();
			if (result.HasValue && result.Value)
			{
				mines = new int[options.weight, options.weight];
				buttons = new Button[options.weight, options.weight];
				coutOfmines = options.countOfMines;
				map.Rows = options.weight;
				map.Columns = options.weight;
				RefreshMap(mines, buttons, map);
			}
		}
		private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int index = map.Children.IndexOf(button);
            int row = index / map.Columns;
            int column = index % map.Columns;
            if (button.Style == (Style)Resources["CommonStyle"])
            {
                if (mines[row, column] == -1)
                {
                    button.Style = (Style)Resources["ButtonStyleMine"];
                    for (int i = 0; i < map.Columns; i++)
                    {
                        for (int j = 0; j < map.Columns; j++)
                        {
                            if (mines[i, j] == -1)
                            {
                                buttons[i, j].Style = (Style)Resources["ButtonStyleMine"];
                            }
                        }
                    }
                    map.Children.Clear();
                    for (int i = 0; i < map.Columns; i++)
                    {
                        for (int j = 0; j < map.Columns; j++)
                        {
                            map.Children.Add(buttons[i, j]);
                        }
                    }
                    MessageBox.Show("Game over!");
                    RefreshMap(mines, buttons, map);
                }
                else
                {
                    button.Style = (Style)Resources["PressedStyle"];
                    AllFreeSectors--;
                    if (mines[row, column] == 0)
                    {
                        Open_Sector(buttons, row, column);
                    }
                    else
                    {
                        button.Content = mines[row, column];
						
					}
                }
                if (AllFreeSectors == 0)
                {
                    MessageBox.Show("You win!");
                    RefreshMap(mines, buttons, map);
                }
            }
        }
        public void RefreshMap(int[,] mines, Button[,] buttons, UniformGrid map)
        {
            map.Children.Clear();
			AllFreeSectors = 0;
            for (int i = 0; i < map.Rows; i++)
            {
                for (int j = 0; j < map.Columns; j++)
                {
                    mines[i, j] = 0;
                }
            }
            Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < map.Rows; i++)
            {
                int count = coutOfmines;
                if (count == 0)
                {
                    break;
                }
                for (int j = 0; j < map.Columns; j++)
                {
                    if (count == 0)
                    {
                        break;
                    }
                    if (random.Next() % 3 == 0)
                    {
                        mines[i, j] = -1;
                        count--;
                        AllFreeSectors++;
                    }
                }
            }

            for (int i = 0; i < map.Rows; i++)
            {
                for (int j = 0; j < map.Columns; j++)
                {
                    if (mines[i, j] != -1)
                    {
                        if (i - 1 >= 0 && mines[i - 1, j] == -1)
                        {
                            mines[i, j]++;
                        }
                        if (i + 1 < map.Rows && mines[i + 1, j] == -1)
                        {
                            mines[i, j]++;
                        }
                        if (j - 1 >= 0 && mines[i, j - 1] == -1)
                        {
                            mines[i, j]++;
                        }
                        if (j + 1 < map.Rows && mines[i, j + 1] == -1)
                        {
                            mines[i, j]++;
                        }
                        if (i - 1 >= 0 && j - 1 >= 0 && mines[i - 1, j - 1] == -1)
                        {
                            mines[i, j]++;
                        }
                        if (i - 1 >= 0 && j + 1 < map.Rows && mines[i - 1, j + 1] == -1)
                        {
                            mines[i, j]++;
                        }
                        if (i + 1 < map.Rows && j - 1 >= 0 && mines[i + 1, j - 1] == -1)
                        {
                            mines[i, j]++;
                        }
                        if (i + 1 < map.Rows && j + 1 < map.Rows && mines[i + 1, j + 1] == -1)
                        {
                            mines[i, j]++;
                        }

                    }

                }
            }
			map.Children.Capacity = map.Rows * map.Columns;
            for (int i = 0; i < map.Rows; i++)
            {
                for (int j = 0; j < map.Columns; j++)
                {
                    Button b = new Button();
                    b.Style = (Style)Resources["CommonStyle"];
                    b.Click += Button_Click;
                    b.PreviewMouseRightButtonDown += Add_Flag;
                    buttons[i, j] = b;
                    map.Children.Add(b);
                }
            }
            AllFreeSectors = map.Columns*map.Columns - AllFreeSectors;
			
		}
        public void Open_Sector(Button[,] btns, int Row, int Column)
        {
            Queue<int> q = new Queue<int>();
            q.Enqueue(Row * map.Rows + Column);
            while (q.Count != 0)
            {
                int index = q.Dequeue();
                int x = index / map.Rows;
                int y = index % map.Rows;
                if (x - 1 >= 0 && btns[x - 1, y].Style == (Style)Resources["CommonStyle"])
                {
                    if (mines[x - 1, y] != 0)
                    {
                        btns[x - 1, y].Style = (Style)Resources["PressedStyle"];
                        AllFreeSectors--;
                        btns[x - 1, y].Content = mines[x - 1, y];
                    }
                    else
                    {
                        q.Enqueue((x - 1) * map.Rows + y);
                        AllFreeSectors--;
                        btns[x - 1, y].Style = (Style)Resources["PressedStyle"];
                    }
                }
                if (x + 1 < map.Rows && btns[x + 1, y].Style == (Style)Resources["CommonStyle"])
                {
                    if (mines[x + 1, y] != 0)
                    {
                        btns[x + 1, y].Style = (Style)Resources["PressedStyle"];
                        AllFreeSectors--;
                        btns[x + 1, y].Content = mines[x + 1, y];
                    }
                    else
                    {
                        q.Enqueue((x + 1) * map.Rows + y);
                        AllFreeSectors--;
                        btns[x + 1, y].Style = (Style)Resources["PressedStyle"];
                    }
                }
                if (y - 1 >= 0 && btns[x, y - 1].Style == (Style)Resources["CommonStyle"])
                {

                    if (mines[x, y - 1] != 0)
                    {
                        btns[x, y - 1].Style = (Style)Resources["PressedStyle"];
                        AllFreeSectors--;
                        btns[x, y - 1].Content = mines[x, y - 1];
                    }
                    else
                    {
                        q.Enqueue(x * map.Rows + y - 1);
                        AllFreeSectors--;
                        btns[x, y - 1].Style = (Style)Resources["PressedStyle"];
                    }
                }
                if (y + 1 < map.Rows && btns[x, y + 1].Style == (Style)Resources["CommonStyle"])
                {

                    if (mines[x, y + 1] != 0)
                    {
                        btns[x, y + 1].Content = mines[x, y + 1];
                        AllFreeSectors--;
                        btns[x, y + 1].Style = (Style)Resources["PressedStyle"];
                    }
                    else
                    {
                        q.Enqueue(x * map.Rows + y + 1);
                        AllFreeSectors--;
                        btns[x, y + 1].Style = (Style)Resources["PressedStyle"];
                    }
                }
                if (x - 1 >= 0 && y - 1 >= 0 && btns[x - 1, y - 1].Style == (Style)Resources["CommonStyle"])
                {

                    if (mines[x - 1, y - 1] != 0)
                    {
                        btns[x - 1, y - 1].Style = (Style)Resources["PressedStyle"];
                        AllFreeSectors--;
                        btns[x - 1, y - 1].Content = mines[x - 1, y - 1];
                    }
                }
                if (x - 1 >= 0 && y + 1 < map.Rows && btns[x - 1, y + 1].Style == (Style)Resources["CommonStyle"])
                {
                    if (mines[x - 1, y + 1] != 0)
                    {
                        btns[x - 1, y + 1].Style = (Style)Resources["PressedStyle"];
                        AllFreeSectors--;
                        btns[x - 1, y + 1].Content = mines[x - 1, y + 1];
                    }
                }
                if (x + 1 < map.Rows && y - 1 >= 0 && btns[x + 1, y - 1].Style == (Style)Resources["CommonStyle"])
                {

                    if (mines[x + 1, y - 1] != 0)
                    {
                        btns[x + 1, y - 1].Style = (Style)Resources["PressedStyle"];
                        AllFreeSectors--;
                        btns[x + 1, y - 1].Content = mines[x + 1, y - 1];
                    }
                }
                if (x + 1 < map.Rows && y + 1 < map.Rows && btns[x + 1, y + 1].Style == (Style)Resources["CommonStyle"])
                {

                    if (mines[x + 1, y + 1] != 0)
                    {
                        btns[x + 1, y + 1].Style = (Style)Resources["PressedStyle"];
                        AllFreeSectors--;
                        btns[x + 1, y + 1].Content = mines[x + 1, y + 1];
                    }
                }				
            }
			
        }
        public void Add_Flag(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Style == (Style)Resources["CommonStyle"])
            {
                int index = map.Children.IndexOf(button);
                int row = index / map.Columns;
                int column = index % map.Columns;
                buttons[row, column].Style = (Style)Resources["ButtonStyleFlag"];
            }
            else if (button.Style == (Style)Resources["ButtonStyleFlag"])
            {
                int index = map.Children.IndexOf(button);
                int row = index / map.Columns;
                int column = index % map.Columns;
                buttons[row, column].Style = (Style)Resources["CommonStyle"];
            }
        }
    }
}
