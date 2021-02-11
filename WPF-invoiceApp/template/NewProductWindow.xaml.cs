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

namespace WPF_invoiceApp.template
{
    /// <summary>
    /// Logika interakcji dla klasy NewProductWindow.xaml
    /// </summary>
    public partial class NewProductWindow : Window
    {
        public NewProductWindow()
        {
            InitializeComponent();

            unitTextField.GotFocus += UnitTextField_GotFocus;
            unitTextField.LostFocus += UnitTextField_LostFocus;

            discountTextField.GotFocus += DiscountTextField_GotFocus;
            discountTextField.LostFocus += DiscountTextField_LostFocus;

            priceTextField.GotFocus += PriceTextField_GotFocus;
            priceTextField.LostFocus += PriceTextField_LostFocus;
        }

        private void PriceTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(priceTextField.Text))
            {
                priceTextField.Text = "7412,36";
            }
        }

        private void PriceTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(priceTextField.Text.Equals("7412,36"))
            {
                priceTextField.Text = "";
            }
        }

        private void DiscountTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(discountTextField.Text))
            {
                discountTextField.Text = "6";
            }
        }

        private void DiscountTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(discountTextField.Text.Equals("6"))
            {
                discountTextField.Text = "";
            }
        }

        private void UnitTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(unitTextField.Text))
            {
                unitTextField.Text = "TIME";
            }
        }

        private void UnitTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(unitTextField.Text.Equals("TIME"))
            {
                unitTextField.Text = "";
            }
        }

        private void OnNewProductSaveButton(object sender, RoutedEventArgs e)
        {

        }
    }
}
