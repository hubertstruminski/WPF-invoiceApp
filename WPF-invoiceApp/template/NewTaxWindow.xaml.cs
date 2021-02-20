using ClassLibrary;
using System.Text.RegularExpressions;
using System.Windows;
using WPF_invoiceApp.context;
using WPF_invoiceApp.template.dashboards;

namespace WPF_invoiceApp.template
{
    /// <summary>
    /// Logika interakcji dla klasy NewTaxWindow.xaml
    /// </summary>
    public partial class NewTaxWindow : Window
    {
        private readonly TaxWindow taxWindow;
        private readonly DatabaseContext context;
        private readonly bool isUpdateFlag = false;

        private Tax tax;

        public NewTaxWindow(DatabaseContext context)
        {
            this.context = context;
            InitializeComponent();
            context.Database.EnsureCreated();
        }

        public NewTaxWindow(TaxWindow taxWindow, DatabaseContext context) : this(context)
        {
            this.taxWindow = taxWindow;
        }

        public NewTaxWindow(Tax tax, DatabaseContext context, TaxWindow taxWindow) : this(context)
        {
            nameTextField.Text = tax.Name;
            descriptionTextField.Text = tax.Description;
            taxAmountTextField.Text = tax.TaxAmount;
            this.tax = tax;
            isUpdateFlag = true;
            this.taxWindow = taxWindow;
        }

        private void NewTaxSaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool isTaxNameError = false;
            bool isTaxDescriptionError = false;
            bool isTaxAmountError = false;

            if (!new Regex(".{1,255}").IsMatch(nameTextField.Text))
                isTaxNameError = true;

            if (!new Regex(".{0,255}").IsMatch(descriptionTextField.Text))
                isTaxDescriptionError = true;

            if (!new Regex("[0-9]+%$").IsMatch(taxAmountTextField.Text))
                isTaxAmountError = true;

            if (isTaxNameError)
            {
                taxNameErrorLabel.Visibility = Visibility.Visible;
                taxNameErrorLabel.Content = "Name length must be from 1 to 255 characters.";
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
