using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Documents;
using System;
using System.Windows.Markup;
using Color = System.Windows.Media.Color;

namespace SewingPatternBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //Создадим служебные словари с вариантами параметров выкорек и список возможных мерок
        //Словаь для хранения списка первичных мерок
        public static Dictionary<int, Measure> RawMeasureList = new Dictionary<int, Measure>();
        
        //Словарь для хранения производных мерок
        public static Dictionary<int, Measure> DerivativeMeasureList = new Dictionary<int, Measure>();
        
        //Словарь для хранения нарезанных изображений
        public static Dictionary<int, CroppedImage> CropedFullSizeImages = new Dictionary<int, CroppedImage>();

        //Словарь для хранения нарезанных изображений с разметкой
        public static Dictionary<int, Bitmap> MarkedupCroppedImages = new Dictionary<int, Bitmap>();
        
        //Словарь для хранения оверлеев для нарезанных изображений
        public static Dictionary<int, Bitmap> OverlayImagesForCropedImages = new Dictionary<int, Bitmap>();

        //Сформируем объекты мероr для работы (в будущем надо брать из базы и формировать только те, что нужны, а не все сразу)
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
        public static BasePattern basePattern = new BasePattern(); //Сразу подготовим главный рабочий объект программы. В будущем надо работать с множетсвом таких и хранить их в базе.
        
        public static GeometryDrawing geometryResult = null; //Запишем сюда результат построения, чтобы использовать его где потребуемся
        public static System.Drawing.Image imageResult = null; //Запишем сюда результат преобразования геометрии в изображения, чтобы использовать его где потребуется
        public static Bitmap fullSizePatternBitmap = null; //Здесь будем хранить и получать полноразмерный битмап текущей выкройки. (потом надо переделать для подержки многократного построения)
                
        public MemoryStream memoryStreamForImageConvertation = new MemoryStream(); //Мемористрим для конвертации изображений через файлы
        public XpsDocument patternSet;
        public FlowDocument patternSetPreview;

        //Заготовим переменные для ширины и высоты печатной области. Целочисленное, 1 = пиксель
        public int pagePrintableWidth = 0;
        public int pagePrintableHeight = 0;

        readonly CreatePatternWindow createPatternWindow = new CreatePatternWindow(); //Создадим окно ввода параметров выкройки
        readonly RealSizeView realSizeView = new RealSizeView(); //Создадим окно для отображения полноразмерной выкройки

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
            this.Loaded += MainWindow_Loaded; //Назнmачим новому окну владельца, чтобы пользоваться методами владельца

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

        private void OpenInRealSizeButton_Click(object sender, RoutedEventArgs e)
        {
            realSizeView.Show();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            createPatternWindow.Owner = this;
            realSizeView.Owner = this;
        }


        // Построение плечевого исделия
        public void BuildShoulderPattern()
        {
            GeometryDrawing geometryDrawing = new GeometryDrawing
            {
                Pen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.Black, 1)
            };

            geometryResult = geometryDrawing;

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure1 = new PathFigure
            {
                ///StartPoint = 
            };
        }
        
        // Построение Юбки
        public void BuildSkirtPattern()
        {
 
            GeometryDrawing geometryDrawing = new GeometryDrawing
            {
                Pen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.Black, 1)
            };

            geometryResult = geometryDrawing;

            // РЕФАКТОРИНГ. Нужно заменить на универсальный построитель выкроек
            #region Начало построения геометрии выкройки
            PathGeometry pathGeometry = new PathGeometry();
            #region Фигура 1 Переднее полотнище
            PathFigure pathFigure1 = new PathFigure
            {
                StartPoint = basePattern.PatternPoints[1]
            };
            pathGeometry.Figures.Add(pathFigure1);

            LineSegment p1p2 = new LineSegment
            {
                Point = basePattern.PatternPoints[2]
            };
            pathFigure1.Segments.Add(p1p2);

            LineSegment p2p18 = new LineSegment
            {
                Point = basePattern.PatternPoints[18]
            };
            pathFigure1.Segments.Add(p2p18);

            LineSegment p18p5 = new LineSegment
            {
                Point = basePattern.PatternPoints[5]
            };
            pathFigure1.Segments.Add(p18p5);

            LineSegment p5p14u = new LineSegment
            {
                Point = basePattern.PatternPoints[114]
            };
            pathFigure1.Segments.Add(p5p14u);

            LineSegment p14up10r = new LineSegment
            {
                Point = basePattern.PatternPoints[210]
            };
            pathFigure1.Segments.Add(p14up10r);

            LineSegment p10rp11 = new LineSegment
            {
                Point = basePattern.PatternPoints[11]
            };
            pathFigure1.Segments.Add(p10rp11);

            LineSegment p11p10l = new LineSegment
            {
                Point = basePattern.PatternPoints[110]
            };
            pathFigure1.Segments.Add(p11p10l);

            LineSegment p10lp1 = new LineSegment
            {
                Point = basePattern.PatternPoints[1]
            };
            pathFigure1.Segments.Add(p10lp1);
            #endregion

            #region Фигура 2 Заднее полотнище
            PathFigure pathFigure2 = new PathFigure
            {
                StartPoint = basePattern.PatternPoints[7]
            };
            pathGeometry.Figures.Add(pathFigure2);

            LineSegment p7p17r = new LineSegment
            {
                Point = basePattern.PatternPoints[217]
            };
            pathFigure2.Segments.Add(p7p17r);

            LineSegment p17rp17d = new LineSegment
            {
                Point = basePattern.PatternPoints[317]
            };
            pathFigure2.Segments.Add(p17rp17d);

            LineSegment p17dp17l = new LineSegment
            {
                Point = basePattern.PatternPoints[117]
            };
            pathFigure2.Segments.Add(p17dp17l);

            LineSegment p17lp16r = new LineSegment
            {
                Point = basePattern.PatternPoints[216]
            };
            pathFigure2.Segments.Add(p17lp16r);

            LineSegment p16rp16d = new LineSegment
            {
                Point = basePattern.PatternPoints[316]
            };
            pathFigure2.Segments.Add(p16rp16d);

            LineSegment p16dp16l = new LineSegment
            {
                Point = basePattern.PatternPoints[116]
            };
            pathFigure2.Segments.Add(p16dp16l);

            LineSegment p16lp15u = new LineSegment
            {
                Point = basePattern.PatternPoints[115]
            };
            pathFigure2.Segments.Add(p16lp15u);

            LineSegment p15up5 = new LineSegment
            {
                Point = basePattern.PatternPoints[5]
            };
            pathFigure2.Segments.Add(p15up5);

            LineSegment p5p19 = new LineSegment
            {
                Point = basePattern.PatternPoints[19]
            };
            pathFigure2.Segments.Add(p5p19);

            LineSegment p19p9 = new LineSegment
            {
                Point = basePattern.PatternPoints[9]
            };
            pathFigure2.Segments.Add(p19p9);

            LineSegment p9p7 = new LineSegment
            {
                Point = basePattern.PatternPoints[7]
            };
            pathFigure2.Segments.Add(p9p7);
            #endregion

            #region Фигура 3 Прямоугольная хрень на заднем полотнище
            PathFigure pathFigure3 = new PathFigure
            {
                StartPoint = basePattern.PatternPoints[20]
            };
            pathGeometry.Figures.Add(pathFigure3);

            LineSegment p20p9 = new LineSegment
            {
                Point = basePattern.PatternPoints[9]
            };
            pathFigure3.Segments.Add(p20p9);

            LineSegment p9p22 = new LineSegment
            {
                Point = basePattern.PatternPoints[22]
            };
            pathFigure3.Segments.Add(p9p22);

            LineSegment p22p21 = new LineSegment
            {
                Point = basePattern.PatternPoints[21]
            };
            pathFigure3.Segments.Add(p22p21);

            LineSegment p21p20 = new LineSegment
            {
                Point = basePattern.PatternPoints[20]
            };
            pathFigure3.Segments.Add(p21p20);
            #endregion

            #region Фигура 4 вторая прямоугольная хрень на заднем полотнище
            PathFigure pathFigure4 = new PathFigure
            {
                StartPoint = basePattern.PatternPoints[21]
            };
            pathGeometry.Figures.Add(pathFigure4);

            LineSegment p21p22 = new LineSegment
            {
                Point = basePattern.PatternPoints[22]
            };
            pathFigure4.Segments.Add(p21p22);

            LineSegment p22p22r = new LineSegment
            {
                Point = basePattern.PatternPoints[122]
            };
            pathFigure4.Segments.Add(p22p22r);

            LineSegment p22rp21r = new LineSegment
            {
                Point = basePattern.PatternPoints[121]
            };
            pathFigure4.Segments.Add(p22rp21r);

            LineSegment p21rp21 = new LineSegment
            {
                Point = basePattern.PatternPoints[21]
            };
            pathFigure4.Segments.Add(p21rp21);
            #endregion

            

            #endregion

            geometryDrawing.Geometry = pathGeometry;

            DrawingImage geometryImage = new DrawingImage(geometryDrawing);
            geometryImage.Freeze();


            //РЕФАКТОРИНГ. Нужно вытащить это в отдельный метод.
            var image = new System.Windows.Controls.Image { Source = geometryImage };
            double scale;
            scale = 3.7938105;
            var width = geometryDrawing.Bounds.Width * scale;
            var height = geometryDrawing.Bounds.Height * scale;
            image.Arrange(new Rect(0, 0, width, height));

            var bitmap = new RenderTargetBitmap((int)width, (int)height, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(image);
            
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (var mStream = new MemoryStream())
            {
                encoder.Save(mStream);
                fullSizePatternBitmap = new Bitmap(mStream);
            }


            ////Устарел. Теперь это тестовый код для проверки результата построения и рендеринга
            //using (var fStream = new FileStream("C:\\patternbuildertest\\PatternFull.Png", FileMode.Create))
            //{
            //    encoder.Save(fStream);
            //}
            

            image.Source = geometryImage;
            image.Stretch = Stretch.None;

            //Выведем в основное окно превьюшку, где 1 пиксел изображения равен 1 мм реального размера выкройки
            MainViewPort.Children.Add(image);

            var realSizeImage = new System.Windows.Controls.Image { Source = bitmap };
            realSizeView.RealSizeViewPort.Children.Add(realSizeImage);

        }

        //Обработка кнопки "Печать набора"
        public void PrintPattern()
        {
            // Создадим диалог печати и установим некоторые свойства
            PrintDialog pDialog = new PrintDialog();
            pDialog.PageRangeSelection = PageRangeSelection.AllPages;
            pDialog.UserPageRangeEnabled = true;
            
            // Покажем окно диалога печати, а потом проверим, нажал ли пользователь Печать. Если да, то выполним печать документа
            Nullable<Boolean> print = pDialog.ShowDialog();
            if (print == true)
            {
                XpsDocument xpsDocument = new XpsDocument("C:\\FixedDocumentSequence.xps", FileAccess.ReadWrite);
                FixedDocumentSequence fixedDocSeq = xpsDocument.GetFixedDocumentSequence();
                pDialog.PrintDocument(fixedDocSeq.DocumentPaginator, "Test print job");
            }


        }

        //Метод конвертатор из Bitmap в Image для отображения в интерфейсе
        public System.Windows.Controls.Image ConvertImage(Bitmap bitmap)
        {
            using var memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, ImageFormat.Bmp);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();

            var drawingImage = new System.Windows.Controls.Image
            {
                Source = bitmapImage
            };
            bitmap.Dispose();
            return drawingImage;
        }// Конец:ConvertImage(Bitmap bitmap)

        //Метод конвертатор из Bitmap в BitmapImage
        public BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)bitmap).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
        //Обработчик нажатия на кнопку "Нарезать и сохранить"
        private void CropAndSave_Click(object sender, RoutedEventArgs e)
        {
            //PrintDialog printDialog = new PrintDialog();
            //Выясним какого размера печатная область у нашего принтера
            pagePrintableWidth = 794; //Для теста укажем ручками, так как принтер не выбран и мы не знаем его область печати
            pagePrintableHeight = 1123; //Для теста укажем ручками, так как принтер не выбран и мы не знаем его область печати

            //Сделаем локально объект и считаем в него полноразмерное изображение выкройки из файла. В будущем нужно использовать память.
            //Bitmap bitmapImageLocal = new Bitmap("C:\\patternbuildertest\\PatternFull.png", true);
            Bitmap bitmapImageLocal = (Bitmap)fullSizePatternBitmap.Clone();

            //Зададим переменные для управления параметрами нарезочного прямоугольника
            int x = 0;
            int y = 0;
            int cloneRectWidth = pagePrintableWidth;
            int cloneRectHeight = pagePrintableHeight;
            int widthResidue = bitmapImageLocal.Width;
            int heightResidue;
            int bitmapImageKey = 0; //Переменная для отслеживания инкремента ключа с которым обрезанное изображение помещаем в словарик

            //Подготовим прямоугольник для нарезки изображения
            Rectangle cloneRect = new Rectangle(x, y, cloneRectWidth, cloneRectHeight);
            
            //Подготовим символы для кодирования результатов нарезки, чтобы потом можно было их собрать в бумаге
            char CurrentXPageID = '\u0041'; //Используем буквы для нумерации "колонок" при нарезке изображения          
            
            //В цикле последовательно нарежем полноразмерную выкройку на части для печати на листе А4
            //Сначала будем проходить изоражение по вертикали, затем сдвигаться вправо и снова проходить по вертикали, пока не пройдём его целиком по ширине и вертикали
            while (widthResidue >= 0) //Проверяем, есть ли ещё отрезать в ширину. Остаток ширины должен быть больше нуля, если меньше, значит картинка закончилась
            {
                //Перед нарезкой новой колонки, сбросим номер строки. Каждый раз начинаем со строки номер 1
                char CurrentYPageID = '\u0031'; //Используем цифры для нумерации "строк" при нарезке изображения

                //Установим прямоугольник нарезки на верхний край изображения в начале каждой новой колонки
                y = 0;

                if (cloneRectWidth >= widthResidue) 
                { 
                    cloneRect.Width = cloneRectWidth - (cloneRectWidth - widthResidue); 
                }
                else
                { 
                    cloneRect.Width = pagePrintableWidth; 
                }

                cloneRect.Height = pagePrintableHeight; //Восстановим высоту области обрезки перед новым циклом
                heightResidue = bitmapImageLocal.Height; //Восстановим запас высоты изображения для нового цикла

                while (heightResidue >= 0) //Проверяем, есть ли ещё что отрезать в высоту. остаток высоты должен быть больше нуля, если меньше, значит картинка закончилась
                {
                    cloneRect.X = x;
                    
                    if (cloneRectHeight >= heightResidue)
                    { 
                        cloneRect.Height = cloneRectHeight - (cloneRectHeight - heightResidue); //Проверить логику, кажется можно просто приравнять высоту остаточной высоте
                    }
                    else
                    { 
                        cloneRect.Height = pagePrintableHeight; 
                    }

                    cloneRect.Y = y;

                    System.Drawing.Imaging.PixelFormat format = bitmapImageLocal.PixelFormat;
                    string filename = "C:\\patternbuildertest\\PatternCrop_" + CurrentXPageID + CurrentYPageID + ".png";
                    //string markupfFilename = "C:\\patternbuildertest\\PatternCropMarkup_" + CurrentXPageID + CurrentYPageID + ".png"; //Часть тестового кода для вывода разметки в файл
                    string markedupCroppFilename = "C:\\patternbuildertest\\PatternMarkedCrop_" + CurrentXPageID + CurrentYPageID + ".png";

                    using (var cropImage = new Bitmap(bitmapImageLocal.Clone(cloneRect, format)))
                    {
                        try {
                            CroppedImage croppedImage = new CroppedImage(CurrentXPageID, CurrentYPageID, cropImage);
                            CropedFullSizeImages.Add(bitmapImageKey, croppedImage);

                            

                            cropImage.Save(filename, ImageFormat.Png);

                            MarkupBuilder markupBuilder = new MarkupBuilder(pagePrintableHeight, pagePrintableWidth, 40); //Пока хардкодом для теста зададим размер области печати принтера и ширину области склейки
                            var markupBitmap = new Bitmap(markupBuilder.BuildMarkup(croppedImage));

                            //Объединяем нарезку с разметкой. РЕФАКТОРИНГ. Вытащить этот код в отдельный метод или даже объект
                            var markedupCroppBitmap = new Bitmap(markupBitmap.Width, markupBitmap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                            var graphics = Graphics.FromImage(markedupCroppBitmap);
                            graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                            graphics.DrawImage(markupBitmap, 0, 0);
                            graphics.DrawImage(croppedImage.CroppedBitmap, 40, 40);

                            markedupCroppBitmap.Save(markedupCroppFilename, ImageFormat.Png);
                            MarkedupCroppedImages.Add(bitmapImageKey, markedupCroppBitmap);

                            bitmapImageKey++;

                            //// Тестовый код для проверки вывода разметки как конечного результата. Мбыть переделать в юнит-тест? (как тестить картиночки?) 
                            //testBitmap.Save(markupfFilename, ImageFormat.Png);

                        } finally
                        {
                            //cropImage.UnlockBits(cropImage.LockBits(cloneRect, ImageLockMode.ReadWrite, cropImage.PixelFormat)); //Возможно лишнее, мы же ничего не лочили
                            cropImage.Dispose(); //Типа деструктор, чтобы в результате using мы точно убили объект и могли содать его снова

                        }
                    }

                    y += pagePrintableHeight; //Сдвигаемся вниз на высоту листа

                    heightResidue -= cloneRectHeight; //Уменьшим остаток высоты

                    CurrentYPageID++; //Увеличим номер строки
                }

                //Сдвигаемся в правок по картинке на ширину листа
                x += pagePrintableWidth;

                widthResidue -= cloneRectWidth;

                //Берем следующую букву колонки
                CurrentXPageID++;

            }





        }// Конец:CropAndSave_Click(object sender, RoutedEventArgs e)

        //Функция формирования и записи страницы XPS документа с изображением части выкройки
        private void WritePatternToXPS()
        {
            FixedDocument fixedDocument = new FixedDocument();
            PageContent pageContent = new PageContent();
            FixedPage fixedPage = new FixedPage();
            ImageSource imageSource;

            var bitmap = MarkedupCroppedImages[0];
            using (var mStream = new MemoryStream())
            {
                bitmap.Save(mStream, System.Drawing.Imaging.ImageFormat.Bmp);
                mStream.Position = 0;
                imageSource = BitmapFrame.Create(mStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
            }

            fixedPage.Children.Add(new System.Windows.Controls.Image { Source = imageSource });
            pageContent.Child = fixedPage;
            fixedDocument.Pages.Add(pageContent);


        }// Конец:WritePatternToXPS()

        //Обработчик кнопки "Ввод параметров выкройки"
        private void InputPatternSettings_Click(object sender, RoutedEventArgs e)
        {
            //Откроем новое окно для выбора пользователем настроек выкройки и ввода значений
            createPatternWindow.Show();
        }// Конец:InputPatternSettings_Click(object sender, RoutedEventArgs e)

        //Орбаботчик нажатия на кнопку "Печать комплекта
        private void PrintPatternSet_Click(object sender, RoutedEventArgs e)
        {

        }// Конец:PrintPatternSet_Click(object sender, RoutedEventArgs e)

        //Обработчик нажатия на кнопку "Предпросмотр комплекта"
        private void PreviewPatternSet_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.ShowDialog();
            FixedDocument fixedDocument = new FixedDocument();
            PageContent pageContent = new PageContent();
            
            FixedPage fixedPage = new FixedPage();
            fixedPage.Background = System.Windows.Media.Brushes.White;

            UIElement visual = new UIElement();

            FixedPage.SetLeft(visual, 0);
            FixedPage.SetTop(visual, 0);

            fixedPage.Width = printDialog.PrintableAreaWidth;
            fixedPage.Height = printDialog.PrintableAreaHeight;

            fixedPage.Children.Add((UIElement)visual);

            System.Windows.Size size = new System.Windows.Size(Convert.ToInt32(printDialog.PrintableAreaWidth), Convert.ToInt32(printDialog.PrintableAreaHeight));
            fixedPage.Measure(size);

            fixedPage.Arrange(new System.Windows.Rect(new System.Windows.Point(), size));

            fixedPage.UpdateLayout();
            //fixedPage.Width = ;
            //fixedPage.Height = ;
            ImageSource imageSource;

            var bitmap = MarkedupCroppedImages[0];
            using (var mStream = new MemoryStream())
            {
                bitmap.Save(mStream, System.Drawing.Imaging.ImageFormat.Png);
                mStream.Position = 0;
                imageSource = BitmapFrame.Create(mStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
            }

            fixedPage.Children.Add(new System.Windows.Controls.Image { Source = imageSource });
            //pageContent.Child = fixedPage;
            ((IAddChild)pageContent).AddChild(fixedPage);

            fixedDocument.Pages.Add(pageContent);

            DocumentViewer documentViewer = new DocumentViewer();
            documentViewer.Document = fixedDocument;

            printDialog.PrintDocument(fixedDocument.DocumentPaginator, "TestPatternPrint");
            //Короче походу документ получается.
            //Надо теперь вывести окно с контролами и вьюшкой документа.
            //Сделал само окно, но пока не понятно как прикрутить контролы и нужно ли само окно.
            //В общем гуглим как делать превью с фиксированным документом (возможно по аналогии с флоу).

        }// Конец:PreviewPatternSet_Click(object sender, RoutedEventArgs e)
    }

}
