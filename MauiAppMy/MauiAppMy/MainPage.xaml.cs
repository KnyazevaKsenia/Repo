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
        int buttomCount=0;
        string sborkaAdress;
        bool assemblyStatus;
        private Assembly assembly;
        int vertexAmount;
        string[,] matrix;
        Microsoft.Maui.Controls.Grid matrixGrid;
        Type t;
        MethodInfo methodInfo;
        Microsoft.Maui.Controls.Grid floidGrid;
        Microsoft.Maui.Controls.Grid labelGrid;


        public MainPage()
        {
            InitializeComponent();
            
        }

        public void SborkaButtom_Clicked(object sender, EventArgs e)
        {
            sborkaAdress=SborkaLabel.Text;
            InstallAssembly();
            t=assembly.GetType("Graph");
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

        private void Button_Clicked(object sender, EventArgs e) //конструктор матрицы введенного размеа
        {
            buttomCount++;
            if (buttomCount > 1) 
            {
                stack.Children.RemoveAt(stack.Children.Count - 3);
                stack.Children.RemoveAt(stack.Children.Count - 2);
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

        public  void  ReadMatrix()
        {
            labelGrid = new Microsoft.Maui.Controls.Grid();
            for (int i = 0; i < vertexAmount; i++)
            {
                labelGrid.RowDefinitions.Add(new RowDefinition { Height = 40 });

                for (int j = 0; j < vertexAmount; j++)
                {
                    if (i == 0)
                    {
                        labelGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
                    }

                    Label entry = new Label();

                    labelGrid.SetRow(entry, i);
                    labelGrid.SetColumn(entry, j);
                    labelGrid.Children.Add(entry);
                }
            }
            ReadToMatrix();
            stack.Add(labelGrid);
        }

        public void ReadToMatrix()
        {
            matrix = new string[vertexAmount, vertexAmount];
            foreach (var child in matrixGrid.Children)
            {
                // Проверяем, является ли элемент Entry
                if (child is Entry entry)
                {
                    // Получаем строку и столбец элемента
                    int row = Microsoft.Maui.Controls.Grid.GetRow(entry);
                    int column = Microsoft.Maui.Controls.Grid.GetColumn(entry);

                    foreach (Label label in labelGrid)
                    {
                        int labelrow = Microsoft.Maui.Controls.Grid.GetRow(entry);
                        int labelcolumn = Microsoft.Maui.Controls.Grid.GetColumn(entry);
                        if (row==labelrow && column==labelcolumn)
                        {
                            label.Text = entry.Text;
                        }
                    }
                }
            }
        }

        private void OnButtonClicked(object sender, System.EventArgs e)//кнопка запустить флойда
        {

            
            ReadMatrix();
            int vertices = vertexAmount;
            stack.Add(floidGrid);
            
            buttomCount++;
            /*if (buttomCount > 1)
            {
                stack.Children.RemoveAt(stack.Children.Count - 3);
                stack.Children.RemoveAt(stack.Children.Count - 2);
                stack.Children.RemoveAt(stack.Children.Count-1);
            }*/
        }

        public void FillAndStartAlgo()
        {
            
        }
    }


}
