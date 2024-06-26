﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Drawing.Drawing2D;
using Brush = System.Drawing.Brush;
using Pen = System.Drawing.Pen;

namespace SewingPatternBuilder
{
    public class MarkupBuilder
    {
        private int gluingPointIndent;
  
        private int printablePageWidth;
        private int printablePageHeight;

        public int GluingPointIndent { get => gluingPointIndent; set => gluingPointIndent = value; }
        public int PrintablePageWidth { get => printablePageWidth; set => printablePageWidth = value; }
        public int PrintablePageHeigth { get => printablePageHeight; set => printablePageHeight = value; }


        public MarkupBuilder(int PrintablePageHeight, int PrintablePageWidth, int GluingPointIndent)
        {
            printablePageHeight = PrintablePageHeight;
            printablePageWidth = PrintablePageWidth;
            gluingPointIndent = GluingPointIndent;

        }

        //Метод строит разметку по предоставленному обрезанному изображению и возвращает его в виде Bitmap, чтобы далее его объединить непосредственно с обрезком
        public Bitmap BuildMarkup(MainWindow.CroppedImage croppedImage)
        {

            //Подготовим необходимые нам точки для потроения разметки
            var startPoint = new System.Windows.Point(0, 0); //Общая для всех фигур точка

            //Сначала подготовим точки для обводки самого изображения выкройки
            var boarderPoint1 = new System.Windows.Point(startPoint.X + croppedImage.CroppedBitmap.Width, startPoint.Y);
            var boarderPoint2 = new System.Windows.Point(boarderPoint1.X, boarderPoint1.Y - croppedImage.CroppedBitmap.Height);
            var boarderPoint3 = new System.Windows.Point(boarderPoint2.X - croppedImage.CroppedBitmap.Width, boarderPoint2.Y);
            var boarderPoint4 = new System.Windows.Point(boarderPoint3.X, boarderPoint3.Y + croppedImage.CroppedBitmap.Height);

            //Далее подготовим верхнюю часть разметки для склейки
            var upGluingPlacePoint1 = new System.Windows.Point(startPoint.X, startPoint.Y + gluingPointIndent);
            var upGluingPlacePoint2 = new System.Windows.Point(upGluingPlacePoint1.X + croppedImage.CroppedBitmap.Width, upGluingPlacePoint1.Y);
            var upGluingPlacePoint3 = new System.Windows.Point(upGluingPlacePoint2.X, upGluingPlacePoint2.Y - gluingPointIndent);
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
            markupGeometry.Geometry.Transform = new ScaleTransform(1, -1); // Почему-то точка построения ставится вниз влево, а не вверх влево, как обычно - потому просто перевернем результат. Потом починим.

            //Конвертация геометрии в Bitmap через DrawingImage. РЕФАКТОРИНГ. Запилить это в метод и вызывать везде после построений.
            var markupDrawingImage = new DrawingImage(markupGeometry);
            markupDrawingImage.Freeze();

            //Конвертируем изображение в битмап для дальнейшей обработки
            var markupImage = new System.Windows.Controls.Image { Source = markupDrawingImage };
            double scale;
            scale = 1; //scale = 3.7938105; Здесь масштабировать не нужно, так как работаем с уже реальным масштабом
            double scaleSquare = 3.7938105;
            var width = markupGeometry.Bounds.Width * scale; //Пока сохраним это. Пригодится при рефакторинге
            var height = markupGeometry.Bounds.Height * scale; // Пока сохраним это. Пригодится при рефакторинге
            markupImage.Arrange(new Rect(startPoint.X, startPoint.Y, width, height));

            var markupRTBitmap = new RenderTargetBitmap((int)width, (int)height, 96, 96, PixelFormats.Pbgra32);
            markupRTBitmap.Render(markupImage);

            //// Тестовый код, для вывода результата рендеринга в файл. Проверяем, что у нас в мемористрим пишется нормальый файл, а не черное полотно.
            //string markupFilePath = "C:\\patternbuildertest\\PatternCropMarkupFileBeforeMStream_" + croppedImage.XPageID + croppedImage.YPageID + ".png"; // вывлим файлы сразу после построения для проверки
            //
            //var encoderFileStream = new PngBitmapEncoder();
            //encoderFileStream.Frames.Add(BitmapFrame.Create(markupRTBitmap));
            //using (var fStream = new FileStream(markupFilePath, FileMode.Create))
            //{
            //    encoderFileStream.Save(fStream);
            //}

            var encoderMemStream = new PngBitmapEncoder();
            encoderMemStream.Frames.Add(BitmapFrame.Create(markupRTBitmap));
            using var mStream = new MemoryStream();
            encoderMemStream.Save(mStream);
            var markupBitmap = new Bitmap(mStream);

            //RectangleF upTextRect = new RectangleF(GluingPointIndent, 0, croppedImage.CroppedBitmap.Width, -GluingPointIndent);
            RectangleF upTextRect = new RectangleF(GluingPointIndent, 0, Convert.ToSingle(markupRTBitmap.Width), GluingPointIndent);
            var xUpChar = croppedImage.XPageID;
            var yUpChar = croppedImage.YPageID;
            string upText = "" + xUpChar + --yUpChar + "";
            Graphics graphUpText = Graphics.FromImage(markupBitmap);
            graphUpText.SmoothingMode = SmoothingMode.AntiAlias;
            graphUpText.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphUpText.PixelOffsetMode = PixelOffsetMode.HighQuality;
            StringFormat upStringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            graphUpText.DrawString(upText, new Font("Arial", 16), System.Drawing.Brushes.Gray, upTextRect, upStringFormat);


            //RectangleF leftTextRect = new RectangleF(0, -GluingPointIndent, GluingPointIndent, croppedImage.CroppedBitmap.Height);
            RectangleF leftTextRect = new RectangleF(0, GluingPointIndent, GluingPointIndent, Convert.ToSingle(markupRTBitmap.Height));

            var xLeftChar = croppedImage.XPageID;
            var yLeftChar = croppedImage.YPageID;
            string leftText = "" + --xLeftChar + yLeftChar + "";

            Graphics graphLeftText = Graphics.FromImage(markupBitmap);
            graphLeftText.SmoothingMode = SmoothingMode.AntiAlias;
            graphLeftText.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphLeftText.PixelOffsetMode = PixelOffsetMode.HighQuality;

            StringFormat leftStringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            graphLeftText.DrawString(leftText, new Font("Arial", 16), System.Drawing.Brushes.Gray, leftTextRect, leftStringFormat);


            Rectangle centerTextRect = new Rectangle((Convert.ToInt32(markupRTBitmap.Width) / 2) - Convert.ToInt32(50 * scaleSquare), (Convert.ToInt32(markupRTBitmap.Height) / 2) - Convert.ToInt32(50 * scaleSquare), Convert.ToInt32(100 * scaleSquare), Convert.ToInt32(100 * scaleSquare));
            var xCenterChar = croppedImage.XPageID;
            var yCenterChar = croppedImage.YPageID;
            string centerText = "" + xCenterChar + yCenterChar + "";

            Graphics graphCenterText = Graphics.FromImage(markupBitmap);
            graphCenterText.SmoothingMode = SmoothingMode.AntiAlias;
            graphCenterText.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphCenterText.PixelOffsetMode = PixelOffsetMode.HighQuality;

            StringFormat centerStringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            graphCenterText.DrawString(centerText, new Font("Arial", 36), System.Drawing.Brushes.LightGray, centerTextRect, centerStringFormat);
            Pen pen = new Pen(System.Drawing.Color.DarkGray);
            graphCenterText.DrawRectangle(pen, centerTextRect);

            return markupBitmap;

        }
    }
}
