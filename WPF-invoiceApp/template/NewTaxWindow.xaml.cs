using ClassLibrary;
using System.Windows;
using WPF_invoiceApp.context;
using WPF_invoiceApp.repository;
using WPF_invoiceApp.service;
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

        private readonly TaxService service = new TaxService();
        private readonly TaxRepository repository = new TaxRepository();

        public NewTaxWindow(DatabaseContext context)
        {
            this.context = context;
            InitializeComponent();
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
            bool isTaxNameError = service.ValidateTaxNameTextField(nameTextField, taxNameErrorLabel);
            bool isTaxDescriptionError = service.ValidateTaxDescriptionTextField(descriptionTextField, taxDescriptionErrorLabel);
            bool isTaxAmountError = service.ValidateTaxAmountTextField(taxAmountTextField, taxAmountErrorLabel);

            if(!isTaxNameError && !isTaxDescriptionError && !isTaxAmountError)
            {
                if (!isUpdateFlag)
                    tax = new Tax();
                tax.Name = nameTextField.Text;
                tax.Description = descriptionTextField.Text;
                tax.TaxAmount = taxAmountTextField.Text;

                repository.AddTax(tax, context, isUpdateFlag);
                taxWindow.RefreshTaxGridData();
                Close();   
            }
        }
    }
}
