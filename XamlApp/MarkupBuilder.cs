using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Controls;
using System.IO;


namespace SewingPatternBuilder
{
    public class MarkupBuilder
    {
        private int gluingPointIndent;
  
        private int printablePageWidth;
        private int printablePageHeight;

        public int GluingPointIndent { get => gluingPointIndent; set => gluingPointIndent = value; }
        public int PrintablePageWidth { get => printablePageWidth; set => printablePageWidth = value; }
        public int PrintablePageHeith { get => printablePageHeight; set => printablePageHeight = value; }


        public MarkupBuilder()
        {
            printablePageHeight = PrintablePageHeith;
            printablePageWidth = PrintablePageWidth;
            GluingPointIndent = gluingPointIndent;

        }

        //Метод строит разметку по предоставленному обрезанному изображению и возвращает его в виде Bitmap, чтобы далее его объединить непосредственно с обрезком
        public Bitmap BuildMarkup(MainWindow.CroppedImage croppedImage)
        {

            //Подготовим необходимые нам точки для потроения разметки
            var startPoint = new System.Windows.Point(0, 0); //Общиая для всех фигур точка

            //Сначала подготовим точки для обводки самого изображения выкройки
            var boarderPoint1 = new System.Windows.Point(startPoint.X + croppedImage.CroppedBitmap.Width, startPoint.Y);
            var boarderPoint2 = new System.Windows.Point(boarderPoint1.X, boarderPoint1.X - croppedImage.CroppedBitmap.Height);
            var boarderPoint3 = new System.Windows.Point(boarderPoint2.X - croppedImage.CroppedBitmap.Width, boarderPoint2.Y);
            var boarderPoint4 = new System.Windows.Point(boarderPoint3.X, boarderPoint3.Y + croppedImage.CroppedBitmap.Height);

            //Далее подготовим верхнюю часть разметки для склейки
            var upGluingPlacePoint1 = new System.Windows.Point(startPoint.X + GluingPointIndent, startPoint.Y);
            var upGluingPlacePoint2 = new System.Windows.Point(upGluingPlacePoint1.X, upGluingPlacePoint1.Y + croppedImage.CroppedBitmap.Width);
            var upGluingPlacePoint3 = new System.Windows.Point(upGluingPlacePoint2.X - GluingPointIndent, upGluingPlacePoint2.Y);
            var upGluingPlacePoint4 = new System.Windows.Point(upGluingPlacePoint3.X - croppedImage.CroppedBitmap.Width, upGluingPlacePoint3.Y);

            //Левая часть разметки для склейки
            var leftGluingPlacePoint1 = new System.Windows.Point(startPoint.X - gluingPointIndent, startPoint.Y);
            var leftGluingPlacePoint2 = new System.Windows.Point(leftGluingPlacePoint1.X, leftGluingPlacePoint1.Y - croppedImage.CroppedBitmap.Height);
            var leftGluingPlacePoint3 = new System.Windows.Point(leftGluingPlacePoint2.X + gluingPointIndent, leftGluingPlacePoint2.Y);
            var leftGluingPlacePoint4 = new System.Windows.Point(leftGluingPlacePoint3.X, leftGluingPlacePoint3.Y + croppedImage.CroppedBitmap.Height);

            //Создаем геометрию в которую запишем полученную в построении разметку
            var markupGeometry = new GeometryDrawing
            {
                Pen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.Black, 1)
            };

            //Путь в который соединим все фигуры нашей разметки
            var markupPathGeometry = new PathGeometry();

            //Нарисуем обводку для изображения выкройки
            #region BoarderLine Figure Drawing
            var markupBoarderFigure = new PathFigure
            {
                StartPoint = startPoint
            };

            markupPathGeometry.Figures.Add(markupBoarderFigure);

            var markupBoarderLine1 = new LineSegment
            {
                Point = boarderPoint1
            };
            markupBoarderFigure.Segments.Add(markupBoarderLine1);

            var markupBoarderLine2 = new LineSegment
            {
                Point = boarderPoint2
            };
            markupBoarderFigure.Segments.Add(markupBoarderLine2);

            var markupBoarderLine3 = new LineSegment
            {
                Point = boarderPoint3
            };
            markupBoarderFigure.Segments.Add(markupBoarderLine3);

            var markupBoarderLine4 = new LineSegment
            {
                Point = boarderPoint4
            };
            markupBoarderFigure.Segments.Add(markupBoarderLine4);
            #endregion

            //Нарисуем верхнюю часть разметки для места склейки
            #region UpGluingPlace Figure Drawing
            var markupUpGluingPlaceFigure = new PathFigure
            {
                StartPoint = startPoint
            };

            markupPathGeometry.Figures.Add(markupUpGluingPlaceFigure);

            var upGluingPlaceLine1 = new LineSegment
            {
                Point = upGluingPlacePoint1
            };
            markupBoarderFigure.Segments.Add(upGluingPlaceLine1);

            var upGluingPlaceLine2 = new LineSegment
            {
                Point = upGluingPlacePoint2
            };
            markupBoarderFigure.Segments.Add(upGluingPlaceLine2);

            var upGluingPlaceLine3 = new LineSegment
            {
                Point = upGluingPlacePoint3
            };
            markupBoarderFigure.Segments.Add(upGluingPlaceLine3);

            var upGluingPlaceLine4 = new LineSegment
            {
                Point = upGluingPlacePoint4
            };
            markupBoarderFigure.Segments.Add(upGluingPlaceLine4);
            #endregion

            //Нарисуем левую часть разметки для места склейки
            #region LeftGluingPlace Figure Drawing
            var markupLeftGluingPlaceFigure = new PathFigure
            {
                StartPoint = startPoint
            };

            markupPathGeometry.Figures.Add(markupLeftGluingPlaceFigure);

            var leftGluingPlaceLine1 = new LineSegment
            {
                Point = leftGluingPlacePoint1
            };
            markupBoarderFigure.Segments.Add(leftGluingPlaceLine1);

            var leftGluingPlaceLine2 = new LineSegment
            {
                Point = leftGluingPlacePoint2
            };
            markupBoarderFigure.Segments.Add(leftGluingPlaceLine2);

            var leftGluingPlaceLine3 = new LineSegment
            {
                Point = leftGluingPlacePoint3
            };
            markupBoarderFigure.Segments.Add(leftGluingPlaceLine3);

            var leftGluingPlaceLine4 = new LineSegment
            {
                Point = leftGluingPlacePoint4
            };
            markupBoarderFigure.Segments.Add(leftGluingPlaceLine4);
            #endregion

            //Передаем путь с фигурами в геометрию
            markupGeometry.Geometry = markupPathGeometry;

            //Конвертация геометрии в Bitmap через DrawingImage. РЕФАКТОРИНГ. Запилить это в метод и вызывать везде после построений.
            var markupDrawingImage = new DrawingImage(markupGeometry);
            markupDrawingImage.Freeze();

            var markupImage = new System.Windows.Controls.Image { Source = markupDrawingImage };
            double scale;
            scale = 1; //scale = 3.7938105; Здесь масштабировать не нужно, так как работаем с уже реальным масштабом
            var width = markupGeometry.Bounds.Width * scale; //Пока сохраним это. Пригодится при рефакторинге
            var height = markupGeometry.Bounds.Height * scale; // Пока сохраним это. Пригодится при рефакторинге
            markupImage.Arrange(new Rect(0, 0, width, height));

            var markupRTBitmap = new RenderTargetBitmap((int)width, (int)height, 96, 96, PixelFormats.Pbgra32);
            markupRTBitmap.Render(markupImage);

            var mStream = new MemoryStream();
            var encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(markupRTBitmap));
            encoder.Save(mStream);

            using var markupBitmap = new Bitmap(mStream); //Продуем применить using, чтобы после выполнения освобождать память и повторно использовать метод
            markupBitmap.Dispose(); //Это может привести к исключению... нужно проверить.
            return markupBitmap;
        }
    }
}
