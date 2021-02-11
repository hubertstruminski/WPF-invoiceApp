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
    /// Logika interakcji dla klasy NewTaxWindow.xaml
    /// </summary>
    public partial class NewTaxWindow : Window
    {
        public NewTaxWindow()
        {
            InitializeComponent();

            nameTextField.GotFocus += NameTextField_GotFocus;
            nameTextField.LostFocus += NameTextField_LostFocus;

            descriptionTextField.GotFocus += DescriptionTextField_GotFocus;
            descriptionTextField.LostFocus += DescriptionTextField_LostFocus;

            taxAmountTextField.GotFocus += TaxAmountTextField_GotFocus;
            taxAmountTextField.LostFocus += TaxAmountTextField_LostFocus;
        }

        private void TaxAmountTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(taxAmountTextField.Text)) {
                taxAmountTextField.Text = "23%";
            }
        }

        private void TaxAmountTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(taxAmountTextField.Text.Equals("23%"))
            {
                taxAmountTextField.Text = "";
            }
        }

        private void DescriptionTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(descriptionTextField.Text))
            {
                descriptionTextField.Text = "VAT - new amount for customers";
            }
        }

        private void DescriptionTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(descriptionTextField.Text.Equals("VAT - new amount for customers"))
            {
                descriptionTextField.Text = "";
            }
        }

        private void NameTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(nameTextField.Text))
            {
                nameTextField.Text = "VAT";
            }
        }

        private void NameTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(nameTextField.Text.Equals("VAT"))
            {
                nameTextField.Text = "";
            }
        }
    }
}
