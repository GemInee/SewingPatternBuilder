using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Drawing;


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

        public Bitmap BuildMarkup(MainWindow.CroppedImage croppedImage, out Bitmap markup)
        {

            //Подготовим необходимые нам точки для потроения разметки
            System.Windows.Point startPoint = new System.Windows.Point(0, 0); //Общиая для всех фигур точка

            //Сначала подготовим точки для обводки самого изображения выкройки
            System.Windows.Point boarderPoint1 = new System.Windows.Point(startPoint.X + croppedImage.CroppedBitmap.Width, startPoint.Y);
            System.Windows.Point boarderPoint2 = new System.Windows.Point(boarderPoint1.X, boarderPoint1.X - croppedImage.CroppedBitmap.Height);
            System.Windows.Point boarderPoint3 = new System.Windows.Point(boarderPoint2.X - croppedImage.CroppedBitmap.Width, boarderPoint2.Y);
            System.Windows.Point boarderPoint4 = new System.Windows.Point(boarderPoint3.X, boarderPoint3.Y + croppedImage.CroppedBitmap.Height);

            //Далее подготовим верхнюю часть разметки для склейки
            System.Windows.Point upGluingPlacePoint1 = new System.Windows.Point(startPoint.X + GluingPointIndent, startPoint.Y);
            System.Windows.Point upGluingPlacePoint2 = new System.Windows.Point(upGluingPlacePoint1.X, upGluingPlacePoint1.Y + croppedImage.CroppedBitmap.Width);
            System.Windows.Point upGluingPlacePoint3 = new System.Windows.Point(upGluingPlacePoint2.X - GluingPointIndent, upGluingPlacePoint2.Y);
            System.Windows.Point upGluingPlacePoint4 = new System.Windows.Point(upGluingPlacePoint3.X - croppedImage.CroppedBitmap.Width, upGluingPlacePoint3.Y);

            //Левая часть разметки для склейки
            System.Windows.Point leftGluingPlacePoint1 = new System.Windows.Point(startPoint.X - gluingPointIndent, startPoint.Y);
            System.Windows.Point leftGluingPlacePoint2 = new System.Windows.Point(leftGluingPlacePoint1.X, leftGluingPlacePoint1.Y - croppedImage.CroppedBitmap.Height);
            System.Windows.Point leftGluingPlacePoint3 = new System.Windows.Point(leftGluingPlacePoint2.X + gluingPointIndent, leftGluingPlacePoint2.Y);
            System.Windows.Point leftGluingPlacePoint4 = new System.Windows.Point(leftGluingPlacePoint3.X, leftGluingPlacePoint3.Y + croppedImage.CroppedBitmap.Height);

            //Создаем геометрию в которую запишем полученную в построении разметку
            GeometryDrawing markupGeometry = new GeometryDrawing
            {
                Pen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.Black, 1)
            };

            //Путь в который соединим все фигуры нашей разметки
            PathGeometry markupPathGeometry = new PathGeometry();

            //Нарисуем обводку для изображения выкройки
            #region BoarderLine Figure Drawing
            PathFigure markupBoarderFigure = new PathFigure
            {
                StartPoint = startPoint
            };

            markupPathGeometry.Figures.Add(markupBoarderFigure);
            
            LineSegment markupBoarderLine1 = new LineSegment
            {
                Point = boarderPoint1
            };
            markupBoarderFigure.Segments.Add(markupBoarderLine1);
            
            LineSegment markupBoarderLine2 = new LineSegment
            {
                Point = boarderPoint2
            };
            markupBoarderFigure.Segments.Add(markupBoarderLine2);

            LineSegment markupBoarderLine3 = new LineSegment
            {
                Point = boarderPoint3
            };
            markupBoarderFigure.Segments.Add(markupBoarderLine3);

            LineSegment markupBoarderLine4 = new LineSegment
            {
                Point = boarderPoint4
            };
            markupBoarderFigure.Segments.Add(markupBoarderLine4);
            #endregion

            //Нарисуем верхнюю часть разметки для места склейки
            #region UpGluingPlace Figure Drawing
            PathFigure markupUpGluingPlaceFigure = new PathFigure
            {
                StartPoint = startPoint
            };

            markupPathGeometry.Figures.Add(markupUpGluingPlaceFigure);

            LineSegment upGluingPlaceLine1 = new LineSegment
            {
                Point = upGluingPlacePoint1
            };
            markupBoarderFigure.Segments.Add(upGluingPlaceLine1);

            LineSegment upGluingPlaceLine2 = new LineSegment
            {
                Point = upGluingPlacePoint2
            };
            markupBoarderFigure.Segments.Add(upGluingPlaceLine2);

            LineSegment upGluingPlaceLine3 = new LineSegment
            {
                Point = upGluingPlacePoint3
            };
            markupBoarderFigure.Segments.Add(upGluingPlaceLine3);

            LineSegment upGluingPlaceLine4 = new LineSegment
            {
                Point = upGluingPlacePoint4
            };
            markupBoarderFigure.Segments.Add(upGluingPlaceLine4);
            #endregion

            //Нарисуем левую часть разметки для места склейки
            #region LeftGluingPlace Figure Drawing
            PathFigure markupLeftGluingPlaceFigure = new PathFigure
            {
                StartPoint = startPoint
            };

            markupPathGeometry.Figures.Add(markupLeftGluingPlaceFigure);

            LineSegment leftGluingPlaceLine1 = new LineSegment
            {
                Point = leftGluingPlacePoint1
            };
            markupBoarderFigure.Segments.Add(leftGluingPlaceLine1);

            LineSegment leftGluingPlaceLine2 = new LineSegment
            {
                Point = leftGluingPlacePoint2
            };
            markupBoarderFigure.Segments.Add(leftGluingPlaceLine2);

            LineSegment leftGluingPlaceLine3 = new LineSegment
            {
                Point = leftGluingPlacePoint3
            };
            markupBoarderFigure.Segments.Add(leftGluingPlaceLine3);

            LineSegment leftGluingPlaceLine4 = new LineSegment
            {
                Point = leftGluingPlacePoint4
            };
            markupBoarderFigure.Segments.Add(leftGluingPlaceLine4);
            #endregion

            //Передаем путь с фигурами в геометрию
            markupGeometry.Geometry = markupPathGeometry;



            DrawingImage markupImage = new DrawingImage(markupGeometry);
            markupImage.Freeze();

            System.Drawing.Image

            markup = new Bitmap(markupImage);

            return markup;
        }


  
        //private GeometryDrawing geometryDrawing;
        //= new GeometryDrawing
        //    {
        //        Pen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.Black, 1)
        //    };

    }
}
