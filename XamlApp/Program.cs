using System;
using System.Collections.Generic;
using System.Text;

namespace SewingPatternBuilder
{
    class Program
    {


        //static void Main(string[] args)
        //{




            ////Для теста выведем список доступных мерок
            //Console.WriteLine("Список мерок:");
            //foreach (KeyValuePair<int, Measure> kvp in RawMeasureList)
            //{
            //    Console.WriteLine(kvp.Value.Name);
            //}
            //Console.ReadKey();


            ////Получим от пользователя основные параметры выкройки
            //Console.WriteLine("ПАРАМЕТРЫ ВЫКРОЙКИ");

            //Console.WriteLine("Выберите тип выкройки:");
            //foreach (KeyValuePair<int, string> kvp in basePattern.PatternTypes)
            //{
            //    Console.WriteLine(kvp.Key + ". " + kvp.Value);
            //}

            //Console.Write("Тип выкройки: ");
            //string choiсedPatternType = Console.ReadLine();
            //int choiсedPatternTypeIndex = Convert.ToInt32(choiсedPatternType);
            //basePattern.PatternType = choiсedPatternTypeIndex;
            //Console.WriteLine("");

            //Console.WriteLine("Выберите тип облегания:");
            //foreach (KeyValuePair<int, string> kvp in basePattern.FittingTypes)
            //{
            //    Console.WriteLine(kvp.Key + ". " + kvp.Value);
            //}

            //Console.Write("Тип облегания: ");
            //string choiсedFittingType = Console.ReadLine();
            //int choiсedFittingTypeIndex = Convert.ToInt32(choiсedFittingType);
            //basePattern.FittingType = choiсedFittingTypeIndex;
            //Console.WriteLine("");

            //Console.WriteLine("Определите эластичность ткани:\n" +
            //    "1. Эластичная" +
            //    "2. Не эластичная\n");
            //Console.Write("Тип эластичности: ");
            //string choicedElasticity = Console.ReadLine();

            //switch (choicedElasticity)
            //{
            //    case "1":
            //        basePattern.Elasticity = true;
            //        break;
            //    case "2":
            //        basePattern.Elasticity = false;
            //        break;
            //    default:
            //        break;
            //}

            //Console.WriteLine("Выберите методику построения:");
            //foreach (KeyValuePair<int, string> kvp in basePattern.BuildingMethods)
            //{
            //    Console.WriteLine(kvp.Key + ". " + kvp.Value);
            //}

            //Console.Write("Методика построения: ");
            //string choiсedMethodType = Console.ReadLine();
            //int choiсedMethodTypeIndex = Convert.ToInt32(choiсedMethodType);
            //basePattern.BuildingMethod = choiсedMethodTypeIndex;
            //Console.WriteLine("");


            ////Для теста выведем список актуальных мерок по выбранным настрокам
            //Console.WriteLine("Список первичных мерок:");
            //foreach (KeyValuePair<int, Measure> kvp in RawMeasureList)
            //{
            //    Console.WriteLine(kvp.Value.Name);
            //}

            ////Для теста выведем список актуальных мерок по выбранным настрокам
            //Console.WriteLine("Список производных мерок:");
            //foreach (KeyValuePair<int, Measure> kvp in DerivativeMeasureList)
            //{
            //    Console.WriteLine(kvp.Value.Name);
            //}


            ////Получим от пользователя значение для каждой первичной мерки будущей выкройки
            //Console.WriteLine("ЗАЧЕНИЯ МЕРОК\n" +
            //    "(для отделения десятых долей сантиметра используйте точку\n");
            //foreach (KeyValuePair<int, Measure> kvp in RawMeasureList)
            //{
            //    Console.Write(kvp.Value.Name + ": ");
            //    kvp.Value.SetSize(Convert.ToInt32(Console.ReadLine()));
            //}


            ////Для теста выведем полученные значения производных мерок
            //Console.WriteLine("Список производных мерок и их значения:");
            //foreach (KeyValuePair<int, Measure> kvp in DerivativeMeasureList)
            //{
            //    Console.WriteLine(kvp.Value.Name + ": " + kvp.Value.GetSize());
            //}


            //Console.ReadKey();

        //}
    }
}
