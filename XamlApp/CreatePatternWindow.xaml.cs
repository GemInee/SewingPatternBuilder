using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SewingPatternBuilder
{
    /// <summary>
    /// Логика взаимодействия для CreatePatternWindow.xaml
    /// </summary>
    public partial class CreatePatternWindow : Window
    {
        public CreatePatternWindow()
        {
            InitializeComponent();
            //Подпишемся на события изменения значения размера мерки
            MainWindow.hipHalf.MeasureSizeChanged += MeasureSizeChanged;
 
         
            //Привяжем содержимое комбобоксов к словарям
            PatternType_Box.ItemsSource = MainWindow.basePattern.PatternTypes.Values;
            MethodsList_Box.ItemsSource = MainWindow.basePattern.BuildingMethods.Values;
            FittingType_Box.ItemsSource = MainWindow.basePattern.FittingTypes.Values;

            //Создадим привязки текстовых блоков к значениям полей соответствующих им объектов
            #region
            Binding bindingWaistHalf = new Binding
            {
                Source = MainWindow.waistHalf,
                Path = new PropertyPath("Size")
            };
            WaistHalf_TextBox.SetBinding(TextBlock.TextProperty, bindingWaistHalf);

            Binding bindingHipHalf = new Binding
            {
                Source = MainWindow.hipHalf,
                Path = new PropertyPath("Size")
            };
            HipHalf_TextBox.SetBinding(TextBlock.TextProperty, bindingHipHalf);

            Binding bindingHipAllowance = new Binding
            {
                Source = MainWindow.hipAllowance,
                Path = new PropertyPath("Size")
            };
            HipAllowance_TextBox.SetBinding(TextBlock.TextProperty, bindingHipAllowance);

            Binding bindingWaistAllowance = new Binding
            {
                Source = MainWindow.waistAllowance,
                Path = new PropertyPath("Size")
            };
            WaistAllowance_TextBox.SetBinding(TextBlock.TextProperty, bindingWaistAllowance);

            Binding bindingFrontCloth = new Binding
            {
                Source = MainWindow.frontCloth,
                Path = new PropertyPath("Size")
            };
            FrontCloth_TextBox.SetBinding(TextBlock.TextProperty, bindingFrontCloth);

            Binding bindingWRearCloth = new Binding
            {
                Source = MainWindow.rearCloth,
                Path = new PropertyPath("Size")
            };
            RearCloth_TextBox.SetBinding(TextBlock.TextProperty, bindingWRearCloth);

            Binding bindingTuckSolutionSum = new Binding
            {
                Source = MainWindow.tuckSolutionSum,
                Path = new PropertyPath("Size")
            };
            TuckSolutionSum_TextBox.SetBinding(TextBlock.TextProperty, bindingTuckSolutionSum);

            Binding bindingFrontTuck = new Binding
            {
                Source = MainWindow.frontTuck,
                Path = new PropertyPath("Size")
            };
            FrontTuck_TextBox.SetBinding(TextBlock.TextProperty, bindingFrontTuck);

            Binding bindingRearTuck = new Binding
            {
                Source = MainWindow.rearTuck,
                Path = new PropertyPath("Size")
            };
            RearTuck_TextBox.SetBinding(TextBlock.TextProperty, bindingRearTuck);

            Binding bindingSideTuck = new Binding
            {
                Source = MainWindow.sideTuck,
                Path = new PropertyPath("Size")
            };
            SideTuck_TextBox.SetBinding(TextBlock.TextProperty, bindingSideTuck);

            //Создадим подписки на события изменения значений производных мерок


            #endregion
        }

        private void MeasureSizeChanged(object sender, EventArgs e)
        {

        }

        private void Hip_changed(object sender, TextChangedEventArgs e)
        {

        }

        private void Waist_changed(object sender, TextChangedEventArgs e)
        {

        }

        private void HipHeight_changed(object sender, TextChangedEventArgs e)
        {

        }

        private void SitHeight_changed(object sender, TextChangedEventArgs e)
        {

        }

        private void ProductLenght_changed(object sender, TextChangedEventArgs e)
        {

        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.basePattern.BuildingMethod = MethodsList_Box.SelectedIndex; //Надо разобраться как получить из комбобокса элемент словаря с ключем.
            MainWindow.basePattern.PatternType = PatternType_Box.SelectedIndex; //Надо разобраться как получить из комбобокса элемент словаря с ключем.
            MainWindow.basePattern.FittingType = FittingType_Box.SelectedIndex; //Надо разобраться как получить из комбобокса элемент словаря с ключем.
            MainWindow.basePattern.Elasticity = Elasticity_CheckBox.IsChecked ?? true;
            MainWindow.basePattern.FemalePattern = FemalePattern_CheckBox.IsChecked ?? true;

            MainWindow.hip.SetSize(int.Parse(Hip_TextBox.Text)*10);
            MainWindow.waist.SetSize(int.Parse(Waist_TextBox.Text)*10);

            MainWindow.hipHeight.SetSize(int.Parse(HipHeight_TextBox.Text)*10);
            MainWindow.sitHeight.SetSize(int.Parse(SitHeight_TextBox.Text)*10);
            MainWindow.productLenght.SetSize(int.Parse(ProductLenght_TextBox.Text)*10);

            ((MainWindow)this.Owner).RecalculateMeasures();

            WaistHalf_TextBox.Text = Convert.ToString(MainWindow.waistHalf.GetSize());
            HipHalf_TextBox.Text = Convert.ToString(MainWindow.hipHalf.GetSize());
            HipAllowance_TextBox.Text = Convert.ToString(MainWindow.hipAllowance.GetSize());
            WaistAllowance_TextBox.Text = Convert.ToString(MainWindow.waistAllowance.GetSize());
            FrontCloth_TextBox.Text = Convert.ToString(MainWindow.frontCloth.GetSize());
            RearCloth_TextBox.Text = Convert.ToString(MainWindow.rearCloth.GetSize());
            TuckSolutionSum_TextBox.Text = Convert.ToString(MainWindow.tuckSolutionSum.GetSize());
            FrontTuck_TextBox.Text = Convert.ToString(MainWindow.frontTuck.GetSize());
            RearTuck_TextBox.Text = Convert.ToString(MainWindow.rearTuck.GetSize());
            SideTuck_TextBox.Text = Convert.ToString(MainWindow.sideTuck.GetSize());

        }

        private void WaistHalf_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void HipHalf_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void HipAllowance_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void WaistAllowance_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void FrontCloth_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RearCloth_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TuckSolutionSum_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void FrontTuck_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RearTuck_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SideTuck_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BuildButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.basePattern.CalculatePoints();
            ((MainWindow)this.Owner).BuildPattern();
        }

        //Заполним форму тестовыми значениями для более быстрой проверки результата
        private void SetTestValues_Click(object sender, RoutedEventArgs e)
        {
            PatternType_Box.SelectedIndex = 2;
            MethodsList_Box.SelectedIndex = 7;
            FittingType_Box.SelectedIndex = 2;

            Elasticity_CheckBox.IsChecked = true;
            FemalePattern_CheckBox.IsChecked = true;

            Waist_TextBox.Text = Convert.ToString(65);
            Hip_TextBox.Text = Convert.ToString(95);
            HipHeight_TextBox.Text = Convert.ToString(20);
            SitHeight_TextBox.Text = Convert.ToString(30);
            ProductLenght_TextBox.Text = Convert.ToString(40);
        }
    }
}
