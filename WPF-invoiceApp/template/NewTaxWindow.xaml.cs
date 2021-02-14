using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_invoiceApp.context;
using WPF_invoiceApp.template.dashboards;

namespace WPF_invoiceApp.template
{
    /// <summary>
    /// Logika interakcji dla klasy NewTaxWindow.xaml
    /// </summary>
    public partial class NewTaxWindow : Window
    {
        private const string TAX_AMOUNT_TEXT = "23%";
        private const string DESCRIPTION_TEXT = "VAT - new amount for customers";
        private const string NAME_TEXT = "VAT";

        private TaxWindow taxWindow;
        private DatabaseContext context = new DatabaseContext();
        private Tax tax;
        private bool isUpdateFlag = false;

        public NewTaxWindow()
        {
            InitializeComponent();
            AssignPlaceholderHandlers();
            context.Database.EnsureCreated();
        }

        public NewTaxWindow(TaxWindow taxWindow) : this()
        {
            this.taxWindow = taxWindow;
        }

        public NewTaxWindow(Tax tax, DatabaseContext context, TaxWindow taxWindow) : this()
        {
            nameTextField.Text = tax.Name;
            descriptionTextField.Text = tax.Description;
            taxAmountTextField.Text = tax.TaxAmount;
            this.tax = tax;
            isUpdateFlag = true;
            //context.Dispose();
            this.context = context;
            this.taxWindow = taxWindow;
        }

        private void AssignPlaceholderHandlers()
        {
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
                taxAmountTextField.Text = TAX_AMOUNT_TEXT;
            }
        }

        private void TaxAmountTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(taxAmountTextField.Text.Equals(TAX_AMOUNT_TEXT))
            {
                taxAmountTextField.Text = string.Empty;
            }
        }

        private void DescriptionTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(descriptionTextField.Text))
            {
                descriptionTextField.Text = DESCRIPTION_TEXT;
            }
        }

        private void DescriptionTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(descriptionTextField.Text.Equals(DESCRIPTION_TEXT))
            {
                descriptionTextField.Text = string.Empty;
            }
        }

        private void NameTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(nameTextField.Text))
            {
                nameTextField.Text = NAME_TEXT;
            }
        }

        private void NameTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(nameTextField.Text.Equals(NAME_TEXT))
            {
                nameTextField.Text = string.Empty;
            }
        }

        private void NewTaxSaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool isTaxNameError = false;
            bool isTaxDescriptionError = false;
            bool isTaxAmountError = false;

            if (!new Regex(".{1,255}").IsMatch(nameTextField.Text) || new Regex(NAME_TEXT).IsMatch(nameTextField.Text))
                isTaxNameError = true;

            if (!new Regex(".{0,255}").IsMatch(descriptionTextField.Text) || new Regex(DESCRIPTION_TEXT).IsMatch(descriptionTextField.Text))
                isTaxDescriptionError = true;

            if (!new Regex("[0-9]+%$").IsMatch(taxAmountTextField.Text) || new Regex(TAX_AMOUNT_TEXT).IsMatch(taxAmountTextField.Text))
                isTaxAmountError = true;

            if (isTaxNameError)
            {
                taxNameErrorLabel.Visibility = Visibility.Visible;
                taxNameErrorLabel.Content = "Name length ust be from 1 to 255 characters.";
            }
            else
                taxNameErrorLabel.Content = "";

            if (isTaxDescriptionError)
            {
                taxDescriptionErrorLabel.Visibility = Visibility.Visible;
                taxDescriptionErrorLabel.Content = "Maximum length is 255 characters.";
            }
            else
                taxDescriptionErrorLabel.Content = "";

            if (isTaxAmountError)
            {
                taxAmountErrorLabel.Visibility = Visibility.Visible;
                taxAmountErrorLabel.Content = "Incorrect format, e.g. 23%";
            }
            else
                taxAmountErrorLabel.Content = "";

            if(!isTaxNameError && !isTaxDescriptionError && !isTaxAmountError)
            {
                if (!isUpdateFlag)
                    tax = new Tax();
                tax.Name = nameTextField.Text;
                tax.Description = descriptionTextField.Text;
                tax.TaxAmount = taxAmountTextField.Text;

                if (isUpdateFlag)
                    context.Taxes.Update(tax);
                else
                    context.Taxes.Add(tax);
                
                context.SaveChanges();
                taxWindow.RefreshTaxGridData();
                this.Close();
                
            }
        }
    }
}
