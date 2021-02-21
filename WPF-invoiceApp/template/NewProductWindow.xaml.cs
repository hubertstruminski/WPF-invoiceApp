using ClassLibrary;
using System;
using System.Windows;
using WPF_invoiceApp.context;
using WPF_invoiceApp.repository;
using WPF_invoiceApp.service;
using WPF_invoiceApp.template.dashboards;
using WPF_invoiceApp.template.lists;

namespace WPF_invoiceApp.template
{
    /// <summary>
    /// Logika interakcji dla klasy NewProductWindow.xaml
    /// </summary>
    public partial class NewProductWindow : Window
    {
        private readonly ProductWindow productWindow;
        private readonly DatabaseContext context;
        private readonly bool isUpdateFlag;

        private Tax tax;
        private Product product;

        private readonly ProductService service = new ProductService();
        private readonly ProductRepository repository = new ProductRepository();

        public NewProductWindow(DatabaseContext context)
        {
            this.context = context;
            InitializeComponent();
        }

        public NewProductWindow(ProductWindow productWindow, DatabaseContext context) : this(context)
        {
            this.productWindow = productWindow;
        }

        public NewProductWindow(Product selectedItem, ProductWindow productWindow, DatabaseContext context) : this(context)
        {
            nameTextField.Text = selectedItem.Name;
            descriptionTextField.Text = selectedItem.Description;
            priceTextField.Text = selectedItem.Price.ToString();
            amountTextField.Text = selectedItem.Amount.ToString();
            unitTextField.Text = selectedItem.Unit;
            discountTextField.Text = selectedItem.Discount.ToString();
            addTaxButton.Content = selectedItem.Tax.Name + ", Amount: " + selectedItem.Tax.TaxAmount;

            this.productWindow = productWindow;
            product = selectedItem;
            tax = selectedItem.Tax;
            isUpdateFlag = true;
        }

        private void OnNewProductSaveButton(object sender, RoutedEventArgs e)
        {
            (bool isNameEmpty, bool isNameError) = service.ValidateNameTextField(nameTextField, nameErrorLabel);
            bool isDescriptionEmpty = service.ValidateDescriptionTextField(descriptionTextField, descriptionErrorLabel);
            (bool isPriceEmpty, bool isPriceError) = service.ValidatePriceTextField(priceTextField, priceErrorLabel);
            bool isDiscountError = service.ValidateDiscountTextField(discountTextField, discountErrorLabel);
            (bool isAmountEmpty, bool isAmountError) = service.ValidateAmountTextField(amountTextField, amountErrorLabel);
            (bool isUnitEmpty, bool isUnitError) = service.ValidateUnitTextField(unitTextField, unitErrorLabel);
            bool isTaxEmpty = service.ValidateAddTaxButton(addTaxButton, taxErrorLabel);

            if(!isNameEmpty && !isNameError && !isDescriptionEmpty && 
                !isPriceEmpty && !isPriceError && !isDiscountError && 
                !isAmountEmpty && !isAmountError && !isUnitEmpty && 
                !isUnitError && !isTaxEmpty)
            {
                if (!isUpdateFlag)
                    product = new Product();

                product.Name = nameTextField.Text;
                product.Description = descriptionTextField.Text;
                product.Price = Convert.ToDecimal(priceTextField.Text);
                product.Discount = Convert.ToInt32(discountTextField.Text);
                product.Amount = Convert.ToDouble(amountTextField.Text);
                product.Unit = unitTextField.Text;
                product.Tax = tax;

                repository.AddProduct(product, context, isUpdateFlag);
                productWindow.RefreshProductGridData();
                Close();
            }
        }

        public void SetTax(Tax newItem)
        {
            tax = newItem;
            addTaxButton.Content = newItem.Name + ", Amount: " + newItem.TaxAmount;
        }

        private void AddTaxButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseTaxWindow chooseTaxWindow = new ChooseTaxWindow(this, context);
            chooseTaxWindow.Owner = Application.Current.MainWindow;
            chooseTaxWindow.ShowDialog();
        }
    }
}
