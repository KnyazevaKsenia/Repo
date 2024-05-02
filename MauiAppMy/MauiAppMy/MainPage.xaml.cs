using FloidAlgoritm;
using System.Reflection;
using static System.Type;
using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
namespace MauiAppMy
{
    public partial class MainPage : ContentPage
    {
        int buttomCount;
        string sborkaAdress;
        bool assemblyStatus;
        private Assembly assembly;
        int vertexAmount;
        int[,] matrix;
        Microsoft.Maui.Controls.Grid matrixGrid;

        public MainPage()
        {
            InitializeComponent();
        }

        public void SborkaButtom_Clicked(object sender, EventArgs e)
        {
            sborkaAdress=SborkaLabel.Text;
            
            InstallAssembly();
        }

        private void InstallAssembly()
        {
            try
            {
                UploadNewDll();
                CheckForContract(assembly);
                if (assemblyStatus)
                {
                    SborkaInformation.Text = "Получилось!!!";
                    SborkaLabel.BackgroundColor = Colors.Green;
                }
                else
                {
                    SborkaInformation.Text = "Сборка не реализует требуемый интерфейс";
                    SborkaLabel.BackgroundColor = Colors.Red;
                }
            }
            catch
            {
                SborkaInformation.Text = "Не удалось загрузить сборку";
                SborkaLabel.BackgroundColor = Colors.Red;
            }
        }
        
        private void UploadNewDll()
        {
            Assembly asm = Assembly.LoadFrom(SborkaLabel.Text);
            assembly = asm;
            CheckForContract(asm);
        }

        private void CheckForContract(Assembly asm)
        {
            Type[] types = asm.GetTypes();

            bool hasImplementation = types.Any(t => typeof(IFloid).IsAssignableFrom(t) && t.IsClass);

            assemblyStatus = hasImplementation;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            buttomCount++;
            if (buttomCount > 1) 
            {
                stack.Children.RemoveAt(stack.Children.Count - 2);
                stack.Children.RemoveAt(stack.Children.Count - 1);
            }

            int.TryParse(VercEntry.Text, out vertexAmount);
            Microsoft.Maui.Controls.Grid grid = new Microsoft.Maui.Controls.Grid();
            matrixGrid = grid;

            for (int i = 0; i < vertexAmount; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = 40});

                for (int j = 0; j < vertexAmount; j++)
                {
                    if (i == 0)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                    }

                    Entry entry = new Entry();
                    grid.SetRow(entry, i); 
                    grid.SetColumn(entry, j); 
                    grid.Children.Add(entry);
                }
            }

            grid.HorizontalOptions = LayoutOptions.Center;
            grid.VerticalOptions = LayoutOptions.Center;

            stack.Children.Add(grid);

            Button floidButton=new Button();
            floidButton.HorizontalOptions = LayoutOptions.Center;
            floidButton.VerticalOptions = LayoutOptions.Center;
            floidButton.Background = Colors.BlueViolet;
            floidButton.Text = "Запустить алгоритм Флойда";
            floidButton.Clicked += OnButtonClicked;

            stack.Children.Add(floidButton);
        }
        void ButtonClickedEventHandler(object floidBotton, EventArgs e)
        {
            ReadMatrix();
        }

        public  void  ReadMatrix()
        {
            matrix = new int[vertexAmount, vertexAmount];
            foreach (var child in matrixGrid.Children)
            {
                // Проверяем, является ли элемент Entry
                if (child is Entry entry)
                {
                    // Получаем строку и столбец элемента
                    int row = Microsoft.Maui.Controls.Grid.GetRow(entry);
                    int column = Microsoft.Maui.Controls.Grid.GetColumn(entry);

                    // Парсим значение элемента и записываем его в массив
                    if (int.TryParse(entry.Text, out int value))
                    {
                        matrix[row, column] = value;
                    }
                }
            }
        }

        private void OnButtonClicked(object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            ReadMatrix();


        }
    }
}
