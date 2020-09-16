using System.Drawing;


namespace SewingPatternBuilder
{
    public partial class MainWindow
    {
        //Подготовим структуру для объединения обрезков с их координатами для последующего складирования в словаре
        public struct CroppedImage
        {
            public CroppedImage(char xPageID, char yPageID, Bitmap bitmap)
            {
                XPageID = xPageID;
                YPageID = yPageID;
                CroppedBitmap = bitmap;
            }
            public char XPageID { get; set; }
            public char YPageID { get; set; }
            public Bitmap CroppedBitmap { get; set; }

            public override bool Equals(object obj)
            {
                throw new System.NotImplementedException();
            }

            public override int GetHashCode()
            {
                throw new System.NotImplementedException();
            }

            public static bool operator ==(CroppedImage left, CroppedImage right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(CroppedImage left, CroppedImage right)
            {
                return !(left == right);
            }
        }

    }

}
