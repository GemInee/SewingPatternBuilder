using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Windows;
using System.Text;

namespace SewingPatternBuilder
{
    public class BasePattern
    {
        public Dictionary<int, string> PatternTypes = new Dictionary<int, string>();
        public Dictionary<int, string> FittingTypes = new Dictionary<int, string>();
        public Dictionary<int, string> BuildingMethods = new Dictionary<int, string>();
        public Dictionary<int, Point> PatternPoints = new Dictionary<int, Point>();


        //public struct Line
        //{

        //}

        // Блок параметров выкройки
        private int patternType;
        private int fittingType;
        private bool elasticity;
        private bool femalePattern;
        private int buildingMethod;

        //Конструктор выкройки
        public BasePattern()
        {
            PatternTypes.Add(1, "Плечевое изделие");
            PatternTypes.Add(2, "Брюки");
            PatternTypes.Add(3, "Юбка");

            FittingTypes.Add(1, "Плотное облегание");
            FittingTypes.Add(2, "Среднее облегание");
            FittingTypes.Add(3, "Свободное облегание");

            BuildingMethods.Add(1, "Лин Жак");
            BuildingMethods.Add(2, "Мюллер и сын");
            BuildingMethods.Add(3, "Крой по Злачевской");
            BuildingMethods.Add(4, "ЛЮБАКС");
            BuildingMethods.Add(5, "Дриттель");
            BuildingMethods.Add(6, "Французская система");
            BuildingMethods.Add(7, "ЦНИИШП ЕМКО СЭВ");
            BuildingMethods.Add(8, "Наш метод");
        }

        public int BuildingMethod
        {
            get => buildingMethod;
            set => buildingMethod = value;
        }

        public bool FemalePattern
        {
            get => femalePattern;
            set => femalePattern = value;
        }

        public bool Elasticity
        {
            get => elasticity;
            set => elasticity = value;
        }

    public int FittingType
        {
            get => fittingType;
            set => fittingType = value;
        }

        public int PatternType
        {
            get => patternType;
            set => patternType = value;
        }

        public void CalculatePoints()
        {
            //Здесь должны быть доступны все алгоритмы постороения с выбором нужного
            //Построение выкройки. Юбка, Наш метод
            //Номер точки в названии точки, соответствует номеру точки в исходном алгоритме построения

            //Point point = new Point(0, 0); // Заглушка, чтобы студия нумеровала переменные как мне надо с 1.

            Point point1 = new Point(0, 0); // Поставим начальную точку
            PatternPoints.Add(1, point1);

            Point point2 = new Point(point1.X, point1.Y + MainWindow.productLenght.GetSize());
            PatternPoints.Add(2, point2);

            Point point3 = new Point(point1.X, point1.Y + MainWindow.hipHeight.GetSize());
            PatternPoints.Add(3, point3);

            Point point4 = new Point(point1.X + MainWindow.frontCloth.GetSize(), point1.Y);
            PatternPoints.Add(4, point4);

            Point point5 = new Point(point4.X, point4.Y + MainWindow.hipHeight.GetSize()); //Здесь должна проходить линия бедер, на всю ширину выкройки
            PatternPoints.Add(5, point5);

            Point point6 = new Point(point4.X, point4.Y + MainWindow.productLenght.GetSize()); // Здесь должна проходить линия низа, на всю ширину выкройки
            PatternPoints.Add(6, point6);

            Point point7 = new Point(point4.X + MainWindow.rearCloth.GetSize(), point4.Y);
            PatternPoints.Add(7, point7);

            Point point8 = new Point(point7.X, point7.Y + MainWindow.hipHeight.GetSize());
            PatternPoints.Add(8, point8);

            Point point9 = new Point(point7.X, point7.Y + MainWindow.productLenght.GetSize());
            PatternPoints.Add(9, point9);

            //Построение передней вытачки

            double x1 = point1.X;
            double x4 = point4.X;
            double y1 = point1.Y;
            double y4 = point4.Y;
            int p1p4Length = Convert.ToInt32(Math.Sqrt(Math.Pow((y4 - y1), 2) + Math.Pow((x4 - x1), 2)));
            Point point10 = new Point(point1.X + (p1p4Length / 2 - 10), point1.Y);
            PatternPoints.Add(10, point10);


            Point point11 = new Point(point10.X, point10.Y + (MainWindow.hipHeight.GetSize() / 2));
            PatternPoints.Add(11, point11);

            Point point10L = new Point(point10.X - MainWindow.frontTuck.GetSize(), point10.Y); //Эта точка должна быть соединена линией с точкой 11
            PatternPoints.Add(110, point10L);

            Point point10R = new Point(point10.X + MainWindow.frontTuck.GetSize(), point10.Y); //Эта точка должна быть соединена линией с точкой 11
            PatternPoints.Add(210, point10R);


            //Построение боковой вытачки

            Point point14 = new Point();
            Point point15 = new Point();

            Point point14U = new Point(); // Эта точка должна быть соединена с точкой 5 лекалом Сабля, по выпуклой стороне

            Point point15U = new Point(); // Эта точка должна быть соединена с точкой 5 лекалом Сабля, по выпуклой стороне


            double x7 = point7.X;
            double y7 = point7.Y;
            double x15;
            double y15;
            Point point12 = new Point();

            //Тут нужна проверка, если суммарный раствор больше 120, то нужно делать две вытачки вместо одной
            if (Convert.ToDouble(MainWindow.tuckSolutionSum.GetSize()) <= 120) //В будущем сделать настраиваемым параметром или опцией, применять или нет
            {
                //Стандартный код, если вытачка одна (Если условие выполнено)

                int p4p7Length = Convert.ToInt32(Math.Sqrt(Math.Pow((y7 - y4), 2) + Math.Pow((x7 - x4), 2)));
                point12.X = point7.X - (p4p7Length / 2 - 10);
                point12.Y = point7.Y;
                PatternPoints.Add(12, point12);

                Point point13 = new Point(point12.X, point12.Y + 130); //Здесь нужна или формула для числа или способ управления им, так как по методике там 12-15 см
                PatternPoints.Add(13, point13);

                Point point12L = new Point
                {
                    X = point12.X - MainWindow.rearTuck.GetSize(),
                    Y = point12.Y //Эта точка должна быть соединена линией с точкой 13
                };
                PatternPoints.Add(112, point12L);

                Point point12R = new Point
                {
                    X = point12.X + MainWindow.rearTuck.GetSize(),
                    Y = point12.Y //Эта точка должна быть соединена линией с точкой 13
                };
                PatternPoints.Add(212, point12R);

                point14.X = point4.X - MainWindow.sideTuck.GetSize();
                point14.Y = point4.Y;
                PatternPoints.Add(14, point14);

                point14U.X = point14.X;
                point14U.Y = point14.Y - 10;
                PatternPoints.Add(114, point14U);

                point15.X = point4.X + MainWindow.sideTuck.GetSize();
                point15.Y = point4.Y;
                PatternPoints.Add(15, point15);

                point15U.X = point15.X;
                point15U.Y = point15.Y - 10;
                PatternPoints.Add(115, point15U);

                //Конец стандартного кода для одной вытачки
            }

            else
            {
                //Начало кода для двух задних вытачек (Если условие не выполнено)
                //Пересчет нужно сделать до начала построения
                MainWindow.sideTuck.SetSize(MainWindow.sideTuck.GetSize() - 10);
                MainWindow.rearTuck.SetSize((MainWindow.rearTuck.GetSize() + 10) / 2);

                point14.X = point4.X - MainWindow.sideTuck.GetSize();
                point14.Y = point4.Y;
                PatternPoints.Add(14, point14);

                point14U.X = point14.X;
                point14U.Y = point14.Y - 10;
                PatternPoints.Add(114, point14U);

                point15.X = point4.X + MainWindow.sideTuck.GetSize();
                point15.Y = point4.Y;
                PatternPoints.Add(15, point15);

                x15 = point15.X;
                y15 = point15.Y;

                point15U.X = point15.X;
                point15U.Y = point15.Y - 10;
                PatternPoints.Add(115, point15U);

                int p7p15Length = Convert.ToInt32(Math.Sqrt(Math.Pow((y15 - y7), 2) + Math.Pow((x15 - x7), 2)));

                point12.X = point7.X - (p7p15Length / 2);
                point12.Y = point7.Y;
                PatternPoints.Add(12, point12);

                Point point16 = new Point
                {
                    X = point12.X - 30,
                    Y = point12.Y
                };
                PatternPoints.Add(16, point16);

                Point point16L = new Point
                {
                    X = point16.X - MainWindow.rearTuck.GetSize(),
                    Y = point16.Y
                };
                PatternPoints.Add(116, point16L);

                Point point16R = new Point
                {
                    X = point16.X + MainWindow.rearTuck.GetSize()
                };
                ;
                point16R.Y = 0;
                PatternPoints.Add(216, point16R);

                Point point16D = new Point
                {
                    X = point16.X,
                    Y = point16.Y + 90
                };
                PatternPoints.Add(316, point16D);

                Point point17 = new Point
                {
                    X = point12.X + 30,
                    Y = point12.Y
                };
                PatternPoints.Add(17, point17);

                Point point17L = new Point
                {
                    X = point17.X - MainWindow.rearTuck.GetSize(),
                    Y = point17.Y
                };
                PatternPoints.Add(117, point17L);

                Point point17R = new Point
                {
                    X = point17.X + MainWindow.rearTuck.GetSize()
                };
                ;
                point17R.Y = 0;
                PatternPoints.Add(217, point17R);

                Point point17D = new Point
                {
                    X = point17.X,
                    Y = point17.Y + 120
                };
                PatternPoints.Add(317, point17D);

                //Конец кода для двух задних вытачек
            }

            //Оформление линии талии

            Point point1D = new Point(point1.X, point1.Y + 5);  //Тут нужен параметр. Если фигура обычная, то 5, если с отклонением назад, то 10
            PatternPoints.Add(101, point1D);                     //Точка 1D должна соединиться с точкой 14* по сабле, в точка 16 прямой угол

            Point point7D = new Point(point7.X, point7.Y + 10); //Нужен параметр. -5 если сутулый, -10 если норма, -15 если перегибистый
            PatternPoints.Add(107, point7D);                     //Точка 7D содеиняется с 15* по сабле

            //Оформление линии бокового среза

            Point point18 = new Point(point6.X - 20, point6.Y); //Тут нужен параметр. Если классика то 20, если зауженная то 30-50
            PatternPoints.Add(18, point18);                      //Соединяем с точкой 5 по прямой

            Point point19 = new Point(point6.X + 20, point6.Y); //Тут нужен параметр. Если классика то 20, если зауженная то 30-50
            PatternPoints.Add(19, point19);                     //Соединяем с тойчкой 5 по прямой
                                                                //Угол в точке 5 сглаживается по сабле

            //Построение шлицы

            Point point20 = new Point(point8.X, point8.Y + 75); //Тут нужен параметр, так как по методике интервал 50-100 (я поставил по средней 75) по Y для новой точки
            PatternPoints.Add(20, point20);

            Point point21 = new Point(point20.X + 50, point20.Y); // Тут нужен параметр, так как для классической 50, а для европейской 100
            PatternPoints.Add(21, point21);

            Point point21R = new Point(point21.X + 50, point21.Y);// отступ повторяется, переделать
            PatternPoints.Add(121, point21R);

            Point point22 = new Point(point21.X, point2.Y);
            PatternPoints.Add(22, point22);

            Point point22R = new Point(point21R.X, point2.Y);
            PatternPoints.Add(122, point22R);


        }

        //public void BuildFigures()
        //{

        //}

    }
}
