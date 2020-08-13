using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SewingPatternBuilder
{
    delegate void MeasureSizeChanged();
    public class Measure : IMeasure
    {
        private int id;
        private string name;
        private int size;
        private int parentMeasure;
        public List<int> patternListForMeasure = new List<int>();
        public List<int> methodListForMeasure = new List<int>();

        public string Name { get => name; set => name = value; }

        public event EventHandler MeasureSizeChanged;

        public int GetSize()
        {
            return size;
        }

        public void SetSize(int id, int fittingType, bool elasticity)
        {
            switch (id)
            {
                case 2: //полуобхват талии
                case 4: //полуобхват бедер
                    size = MainWindow.RawMeasureList[this.ParentMeasure].GetSize() / 2;
                    break;

                case 5: // Припуск бёдер
                    if (fittingType == 0)
                    {
                        size = elasticity == true ? 5 : 7;
                    }
                    else if (fittingType == 1)
                    {
                        size = elasticity == true ? 7 : 10;
                    }
                    else if (fittingType == 2)
                    {
                        size = elasticity == true ? 15 : 20;
                    }
                    break;

                case 6: // Припуск талии
                    if (fittingType == 0)
                    {
                        size = elasticity == true ? 0 : 5;
                    }
                    else if (fittingType == 1)
                    {
                        size = elasticity == true ? 5 : 10;
                    }
                    else if (fittingType == 2)
                    {
                        size = elasticity == true ? 10 : 15;
                    }
                    break;
                case 7: //переднее полотнище
                    size = MainWindow.DerivativeMeasureList[4].GetSize() / 2 + MainWindow.DerivativeMeasureList[5].GetSize();
                    break;

                case 8: //заднее полотнище
                    size = MainWindow.DerivativeMeasureList[4].GetSize() / 2 - 1 + MainWindow.DerivativeMeasureList[5].GetSize();
                    break;

                case 9: //сумарный раствор вытачек
                    size = (MainWindow.DerivativeMeasureList[4].GetSize() + MainWindow.DerivativeMeasureList[5].GetSize()) - (MainWindow.DerivativeMeasureList[2].GetSize() + MainWindow.DerivativeMeasureList[6].GetSize());
                    break;

                case 10: //передняя вытачка
                    double x = Convert.ToDouble(MainWindow.DerivativeMeasureList[this.ParentMeasure].GetSize());
                    size = Convert.ToInt32((x / 6 / 2));
                    break;

                case 11: //задняя вытачка
                    double y = Convert.ToDouble(MainWindow.DerivativeMeasureList[this.ParentMeasure].GetSize());
                    size = Convert.ToInt32((y / 3 / 2));
                    break;

                case 12: //боковая вытачка
                    double z = Convert.ToDouble(MainWindow.DerivativeMeasureList[this.ParentMeasure].GetSize());
                    size = Convert.ToInt32((z / 2 / 2));
                    break;
            }
            
            if (MeasureSizeChanged != null)
            {
                MeasureSizeChanged(this, new EventArgs());
            }

        }
        public void SetSize(int value)
        {
            size = value;

            if (MeasureSizeChanged != null)
            {
                MeasureSizeChanged(this, new EventArgs());
            }
        }

        public int Id { get => id; set => id = value; }
        public int ParentMeasure { get => parentMeasure; set => parentMeasure = value; }

        public Measure(int Id, string Name, int Size, int ParentMeasure)
        {
            id = Id;
            name = Name;
            parentMeasure = ParentMeasure;

            if (ParentMeasure != 100)
            {
                size = Size / 2;
            }
            else
            {
                size = Size;
            }

        }

    }
}
