﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SewingPatternBuilder;
using System.Printing;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace SewingPatternBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Создадим служебные словари с вариантами параметров выкорек и список возможных мерок

        public static Dictionary<int, Measure> RawMeasureList = new Dictionary<int, Measure>();
        public static Dictionary<int, Measure> DerivativeMeasureList = new Dictionary<int, Measure>();

        public static Measure waist = new Measure(0, "Обхват талии", 0, 100);
        public static Measure waist2 = new Measure(1, "Обхват талии 2", 0, 100);
        public static Measure hip = new Measure(3, "Обхват бедер", 0, 0);
        public static Measure hipHeight = new Measure(5, "Высота бедер", 0, 100);
        public static Measure sitHeight = new Measure(6, "Высота сидения", 0, 100);
        public static Measure productLenght = new Measure(7, "Длина изделия", 0, 100);
        public static Measure knee = new Measure(8, "Обхват колена", 0, 0);
        public static Measure gastrocnemius = new Measure(9, "Обхват икры", 0, 100);
        public static Measure ankle = new Measure(10, "Обхват щиколотки", 0, 100);

        public static Measure waistHalf = new Measure(2, "Полуобхват талии", 0, 0);
        public static Measure hipHalf = new Measure(4, "Полуобхват бедер", 0, 3);
        public static Measure hipAllowance = new Measure(5, "Припуск бедер", 0, 100);
        public static Measure waistAllowance = new Measure(6, "Припуск талия", 0, 100);
        public static Measure frontCloth = new Measure(7, "Переднее полотнище", 0, 4);
        public static Measure rearCloth = new Measure(8, "Заднее полотнище", 0, 4);
        public static Measure tuckSolutionSum = new Measure(9, "Суммарный раствор вытачек", 0, 100);
        public static Measure frontTuck = new Measure(10, "Передняя вытачка", 0, 9);
        public static Measure rearTuck = new Measure(11, "Задняя вытачка", 0, 9);
        public static Measure sideTuck = new Measure(12, "Боковая вытачка", 0, 9);
        public static BasePattern basePattern = new BasePattern();
        public static GeometryDrawing geometryResult = null;
        public static System.Drawing.Image imageResult = null;

        CreatePatternWindow createPatternWindow = new CreatePatternWindow();
        RealSizeView realSizeView = new RealSizeView();

        public MainWindow()
        {
            InitializeComponent();
            //Создадим объект выкройки
            BasePattern basePattern = new BasePattern();

            //Настроим объекты мерок, которые доступны в программе и добавим их в словари
            //Сначала настроим все объекты первичных мерок и добавим в словари
            InitRawMeasures();
            //Теперь настроим все объекты производных мерок и добавим в словарь
            InitDerivativeMeasures();

            //Когда объекты мерок инициализированы, выполним инструкции после загрузки
            this.Loaded += MainWindow_Loaded; //Назначим новому окну владельца, чтобы пользоваться методами владельца


        }

        private void InitRawMeasures()
        {
            waist.methodListForMeasure.Add(7);
            waist.patternListForMeasure.Add(2);
            RawMeasureList.Add(0, waist);

            waist.methodListForMeasure.Add(7);
            waist.patternListForMeasure.Add(1);
            RawMeasureList.Add(1, waist2);

            hip.methodListForMeasure.Add(7);
            hip.patternListForMeasure.Add(2);
            RawMeasureList.Add(3, hip);

            hipHeight.methodListForMeasure.Add(7);
            hipHeight.patternListForMeasure.Add(2);
            RawMeasureList.Add(5, hipHeight);

            sitHeight.methodListForMeasure.Add(7);
            sitHeight.patternListForMeasure.Add(1);
            RawMeasureList.Add(6, sitHeight);

            productLenght.methodListForMeasure.Add(7);
            productLenght.patternListForMeasure.Add(2);
            RawMeasureList.Add(7, productLenght);

            knee.methodListForMeasure.Add(7);
            knee.patternListForMeasure.Add(1);
            RawMeasureList.Add(8, knee);

            gastrocnemius.methodListForMeasure.Add(7);
            gastrocnemius.patternListForMeasure.Add(1);
            RawMeasureList.Add(9, gastrocnemius);

            ankle.methodListForMeasure.Add(7);
            ankle.patternListForMeasure.Add(1);
            RawMeasureList.Add(10, ankle);
        }

        private void InitDerivativeMeasures()
        {
            waistHalf.methodListForMeasure.Add(7);
            waistHalf.patternListForMeasure.Add(2);
            DerivativeMeasureList.Add(2, waistHalf);

            hipHalf.methodListForMeasure.Add(7);
            hipHalf.patternListForMeasure.Add(2);
            DerivativeMeasureList.Add(4, hipHalf);

            hipAllowance.methodListForMeasure.Add(7);
            hipAllowance.patternListForMeasure.Add(2);
            DerivativeMeasureList.Add(5, hipAllowance);

            waistAllowance.methodListForMeasure.Add(7);
            waistAllowance.patternListForMeasure.Add(2);
            DerivativeMeasureList.Add(6, waistAllowance);

            frontCloth.methodListForMeasure.Add(7);
            frontCloth.patternListForMeasure.Add(2);
            DerivativeMeasureList.Add(7, frontCloth);

            rearCloth.methodListForMeasure.Add(7);
            rearCloth.patternListForMeasure.Add(2);
            DerivativeMeasureList.Add(8, rearCloth);

            tuckSolutionSum.methodListForMeasure.Add(7);
            tuckSolutionSum.patternListForMeasure.Add(2);
            DerivativeMeasureList.Add(9, tuckSolutionSum);

            frontTuck.methodListForMeasure.Add(7);
            frontTuck.patternListForMeasure.Add(2);
            DerivativeMeasureList.Add(10, frontTuck);

            rearTuck.methodListForMeasure.Add(7);
            rearTuck.patternListForMeasure.Add(2);
            DerivativeMeasureList.Add(11, rearTuck);

            sideTuck.methodListForMeasure.Add(7);
            sideTuck.patternListForMeasure.Add(2);
            DerivativeMeasureList.Add(12, sideTuck);
        }

        public void RecalculateMeasures()
        {
            RecalculateRawMeasures();
        }

        private void RecalculateRawMeasures()
        {



            foreach (KeyValuePair<int, Measure> kvp in DerivativeMeasureList)
            {
                kvp.Value.SetSize(kvp.Value.Id, basePattern.FittingType, basePattern.Elasticity);
            }

            //После пересчета, удалим лишние мерки, которые не будем использовать в обоих листах
            FilterRawMeasures();
            FilterDerivativeMeasures();

        }

        //Фильтруем списки мерок, на нужные и ненужные для данного построения
        private void FilterRawMeasures()
        {
            //Отфильтруем список первичных мерок в соответствии с настройками
            foreach (KeyValuePair<int, Measure> kvp in RawMeasureList)
            {
                //Удалим все мерки, не соответствующие выбранному методу построения
                if (!kvp.Value.methodListForMeasure.Contains(basePattern.BuildingMethod))
                {
                    RawMeasureList.Remove(kvp.Key);
                }
                //Удалим все мерки не соответствующие выбранному изделию
                if (!kvp.Value.patternListForMeasure.Contains(basePattern.PatternType))
                {
                    RawMeasureList.Remove(kvp.Key);
                }
            }
        }

        private void FilterDerivativeMeasures()
        {
            //Отфильтруем список производных мерок в соответствии с настройками
            foreach (KeyValuePair<int, Measure> kvp in DerivativeMeasureList)
            {
                //Удалим все мерки, не соответствующие выбранному методу построения
                if (!kvp.Value.methodListForMeasure.Contains(basePattern.BuildingMethod))
                {
                    DerivativeMeasureList.Remove(kvp.Key);
                }
                //Удалим все мерки не соответствующие выбранному изделию
                if (!kvp.Value.patternListForMeasure.Contains(basePattern.PatternType))
                {
                    DerivativeMeasureList.Remove(kvp.Key);
                }
            }
        }

        private void UpButton1_Click(object sender, RoutedEventArgs e)
        {
            //Откроем новое окно для выбора пользователем настроек выкройки и ввода значений
            createPatternWindow.Show();
        }

        private void OpenInRealSizeButton_Click(object sender, RoutedEventArgs e)
        {
            realSizeView.Show();
        }

        private void UpButton2_Click(object sender, RoutedEventArgs e)
        {
            PrintPattern();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            createPatternWindow.Owner = this;
            realSizeView.Owner = this;
        }

        public void BuildPattern()
        {
            //foreach (KeyValuePair<int, Point> kvp in basePattern.PatternPoints)
            //{
            //    Line line = new Line();
            //    line.X1 = kvp.Value.X;
            //    line.Y1 = kvp.Value.Y + 50;
            //    line.X2 = line.X1 + 1;
            //    line.Y2 = line.Y1 + 1;
            //    line.Stroke = Brushes.Black;
            //    line.StrokeThickness = 3;
            //    line.ToolTip = kvp.Key;
            //    MainViewPort.Children.Add(line);
            //}

            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Pen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.Black, 1);
            geometryDrawing.Pen.Thickness = 1;

            geometryResult = geometryDrawing;

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure1 = new PathFigure();
            pathFigure1.StartPoint = basePattern.PatternPoints[1];
            pathGeometry.Figures.Add(pathFigure1);

            LineSegment p1p2 = new LineSegment();
            p1p2.Point = basePattern.PatternPoints[2];
            pathFigure1.Segments.Add(p1p2);

            LineSegment p2p18 = new LineSegment();
            p2p18.Point = basePattern.PatternPoints[18];
            pathFigure1.Segments.Add(p2p18);

            LineSegment p18p5 = new LineSegment();
            p18p5.Point = basePattern.PatternPoints[5];
            pathFigure1.Segments.Add(p18p5);

            LineSegment p5p14u = new LineSegment();
            p5p14u.Point = basePattern.PatternPoints[114];
            pathFigure1.Segments.Add(p5p14u);

            LineSegment p14up10r = new LineSegment();
            p14up10r.Point = basePattern.PatternPoints[210];
            pathFigure1.Segments.Add(p14up10r);

            LineSegment p10rp11 = new LineSegment();
            p10rp11.Point = basePattern.PatternPoints[11];
            pathFigure1.Segments.Add(p10rp11);

            LineSegment p11p10l = new LineSegment();
            p11p10l.Point = basePattern.PatternPoints[110];
            pathFigure1.Segments.Add(p11p10l);

            LineSegment p10lp1 = new LineSegment();
            p10lp1.Point = basePattern.PatternPoints[1];
            pathFigure1.Segments.Add(p10lp1);

            PathFigure pathFigure2 = new PathFigure();
            pathFigure2.StartPoint = basePattern.PatternPoints[7];
            pathGeometry.Figures.Add(pathFigure2);

            LineSegment p7p17r = new LineSegment();
            p7p17r.Point = basePattern.PatternPoints[217];
            pathFigure2.Segments.Add(p7p17r);

            LineSegment p17rp17d = new LineSegment();
            p17rp17d.Point = basePattern.PatternPoints[317];
            pathFigure2.Segments.Add(p17rp17d);

            LineSegment p17dp17l = new LineSegment();
            p17dp17l.Point = basePattern.PatternPoints[117];
            pathFigure2.Segments.Add(p17dp17l);

            LineSegment p17lp16r = new LineSegment();
            p17lp16r.Point = basePattern.PatternPoints[216];
            pathFigure2.Segments.Add(p17lp16r);

            LineSegment p16rp16d = new LineSegment();
            p16rp16d.Point = basePattern.PatternPoints[316];
            pathFigure2.Segments.Add(p16rp16d);

            LineSegment p16dp16l = new LineSegment();
            p16dp16l.Point = basePattern.PatternPoints[116];
            pathFigure2.Segments.Add(p16dp16l);

            LineSegment p16lp15u = new LineSegment();
            p16lp15u.Point = basePattern.PatternPoints[115];
            pathFigure2.Segments.Add(p16lp15u);

            LineSegment p15up5 = new LineSegment();
            p15up5.Point = basePattern.PatternPoints[5];
            pathFigure2.Segments.Add(p15up5);

            LineSegment p5p19 = new LineSegment();
            p5p19.Point = basePattern.PatternPoints[19];
            pathFigure2.Segments.Add(p5p19);

            LineSegment p19p9 = new LineSegment();
            p19p9.Point = basePattern.PatternPoints[9];
            pathFigure2.Segments.Add(p19p9);

            LineSegment p9p7 = new LineSegment();
            p9p7.Point = basePattern.PatternPoints[7];
            pathFigure2.Segments.Add(p9p7);

            PathFigure pathFigure3 = new PathFigure();
            pathFigure3.StartPoint = basePattern.PatternPoints[20];
            pathGeometry.Figures.Add(pathFigure3);

            LineSegment p20p9 = new LineSegment();
            p20p9.Point = basePattern.PatternPoints[9];
            pathFigure3.Segments.Add(p20p9);

            LineSegment p9p22 = new LineSegment();
            p9p22.Point = basePattern.PatternPoints[22];
            pathFigure3.Segments.Add(p9p22);

            LineSegment p22p21 = new LineSegment();
            p22p21.Point = basePattern.PatternPoints[21];
            pathFigure3.Segments.Add(p22p21);

            LineSegment p21p20 = new LineSegment();
            p21p20.Point = basePattern.PatternPoints[20];
            pathFigure3.Segments.Add(p21p20);

            PathFigure pathFigure4 = new PathFigure();
            pathFigure4.StartPoint = basePattern.PatternPoints[21];
            pathGeometry.Figures.Add(pathFigure4);

            LineSegment p21p22 = new LineSegment();
            p21p22.Point = basePattern.PatternPoints[22];
            pathFigure4.Segments.Add(p21p22);

            LineSegment p22p22r = new LineSegment();
            p22p22r.Point = basePattern.PatternPoints[122];
            pathFigure4.Segments.Add(p22p22r);

            LineSegment p22rp21r = new LineSegment();
            p22rp21r.Point = basePattern.PatternPoints[121];
            pathFigure4.Segments.Add(p22rp21r);

            LineSegment p21rp21 = new LineSegment();
            p21rp21.Point = basePattern.PatternPoints[21];
            pathFigure4.Segments.Add(p21rp21);
            
            geometryDrawing.Geometry = pathGeometry;

            DrawingImage geometryImage = new DrawingImage(geometryDrawing);
            geometryImage.Freeze();

            var image = new System.Windows.Controls.Image { Source = geometryImage };
            double scale;
            scale = 3.7938105;
            var width = geometryDrawing.Bounds.Width * scale;
            var height = geometryDrawing.Bounds.Height * scale;
            image.Arrange(new Rect(0, 0, width, height));

            var bitmap = new RenderTargetBitmap((int)width, (int)height, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(image);

            image.Source = geometryImage;
            image.Stretch = Stretch.None;

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (var stream = new FileStream("C:\\patternbuildertest\\PatternFull.png", FileMode.Create))
            {
                encoder.Save(stream);
            }

            //Выведем в основное окно превьюшку, где 1 пиксел изображения равен 1 мм реального размера выкройки
            MainViewPort.Children.Add(image);

        }

        public void PrintPattern()
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                //Смасштабируем изображение под область просмотра
                //imageResult.Stretch = Stretch.Uniform;

                //Получаем инфу о возможностях принтера
                PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);

                //Получаем масштаб изображения WPF
                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / geometryResult.Geometry.Bounds.Width, capabilities.PageImageableArea.ExtentHeight / geometryResult.Geometry.Bounds.Height);

                //Преобразуем изображение в нужный масштаб
                this.MainViewPort.LayoutTransform = new ScaleTransform(scale, scale);

                //Получим у принтера размер страницы
                System.Windows.Size size = new System.Windows.Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

                //Скомпануем изображение под размер страницы
                //this.MainViewPort.Measure(size);
                //this.MainViewPort.Arrange(new Rect(new System.Windows.Point(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight), size));

                //Выведем на печать изображение, заполнив им одну страницу
                printDialog.PrintVisual(this.MainViewPort, "Тестовая печать");
            }


        }

        private void CropAndSave_Click(object sender, RoutedEventArgs e)
        {

            System.Drawing.Size size = new System.Drawing.Size();
            size.Width = 300;
            size.Height = 300;
            Bitmap bitmapImageLocal = new Bitmap("C:\\patternbuildertest\\PatternFull.png", true);
            System.Drawing.Rectangle cloneRect = new System.Drawing.Rectangle(0, 0, 300, 300);

            System.Drawing.Imaging.PixelFormat format = bitmapImageLocal.PixelFormat;
            Bitmap cropImage = bitmapImageLocal.Clone(cloneRect, format);

            cropImage.Save("C:\\patternbuildertest\\PatternCrop.png", ImageFormat.Png);


            //var encoder = new PngBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(cropImage));

            //using (var stream = new FileStream("C:\\patternbuildertest\\PatternCrop.png", FileMode.Create))
            //{
            //    encoder.Save(stream);
            //}



        }

    }

}
