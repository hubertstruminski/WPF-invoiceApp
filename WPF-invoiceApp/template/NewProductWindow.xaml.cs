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
using WPF_invoiceApp.template.lists;

namespace WPF_invoiceApp.template
{
    /// <summary>
    /// Logika interakcji dla klasy NewProductWindow.xaml
    /// </summary>
    public partial class NewProductWindow : Window
    {
        private const string PRICE_TEXT = "7412,36";
        private const string DISCOUNT_TEXT = "6";
        private const string UNIT_TEXT = "TIME";

        private ProductWindow productWindow;
        private DatabaseContext context = new DatabaseContext();
        private Tax tax;
        private bool isUpdateFlag;
        private Product product;


        public NewProductWindow()
        {
            InitializeComponent();
            AssignPlaceholderHandlers();
            context.Database.EnsureCreated();
        }

        public NewProductWindow(ProductWindow productWindow) : this()
        {
            this.productWindow = productWindow;
        }

        public NewProductWindow(Product selectedItem, ProductWindow productWindow) : this()
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

        private void AssignPlaceholderHandlers()
        {
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
                priceTextField.Text = PRICE_TEXT;
            }
        }

        private void PriceTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(priceTextField.Text.Equals(PRICE_TEXT))
            {
                priceTextField.Text = string.Empty;
            }
        }

        private void DiscountTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(discountTextField.Text))
            {
                discountTextField.Text = DISCOUNT_TEXT;
            }
        }

        private void DiscountTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(discountTextField.Text.Equals(DISCOUNT_TEXT))
            {
                discountTextField.Text = string.Empty;
            }
        }

        private void UnitTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(unitTextField.Text))
            {
                unitTextField.Text = UNIT_TEXT;
            }
        }

        private void UnitTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(unitTextField.Text.Equals(UNIT_TEXT))
            {
                unitTextField.Text = string.Empty;
            }
        }

        private void OnNewProductSaveButton(object sender, RoutedEventArgs e)
        {
            bool isNameEmpty = false;
            bool isNameError = false;

            bool isDescriptionEmpty = false;

            bool isPriceEmpty = false;
            bool isPriceError = false;

            bool isDiscountError = false;

            bool isAmountEmpty = false;
            bool isAmountError = false;

            bool isUnitEmpty = false;
            bool isUnitError = false;

            bool isTaxEmpty = false;

            if (nameTextField.Text.Length == 0)
                isNameEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(nameTextField.Text))
                    isNameError = true;
            }

            if (!new Regex(".{0,255}").IsMatch(descriptionTextField.Text))
                isDescriptionEmpty = true;

            if (priceTextField.Text.Length == 0)
                isPriceEmpty = true;
            else
            {
                if (!new Regex("\\d+(\\,\\d{1,2})?").IsMatch(priceTextField.Text))
                    isPriceError = true;
            }

            if(discountTextField.Text.Length > 0)
            {
                if (!new Regex("[0-9]{1,3}").IsMatch(discountTextField.Text))
                    isDiscountError = true;
            }

            if (amountTextField.Text.Length == 0)
                isAmountEmpty = true;
            else
            {
                if (!new Regex("\\d+(\\.\\d{1,2})?").IsMatch(amountTextField.Text))
                    isAmountError = true;
            }

            if (unitTextField.Text.Length == 0)
                isUnitEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(amountTextField.Text))
                    isUnitError = true;
            }

            if (addTaxButton.Content.Equals(">"))
                isTaxEmpty = true;

            if (isNameEmpty)
            {
                nameErrorLabel.Visibility = Visibility.Visible;
                nameErrorLabel.Content = "Name field is required.";
            }
            else
            {
                if (isNameError)
                {
                    nameErrorLabel.Visibility = Visibility.Visible;
                    nameErrorLabel.Content = "Required length is from 1 to 255 characters.";
                }
                else
                    nameErrorLabel.Content = "";
            }

            if (isDescriptionEmpty)
            {
                descriptionErrorLabel.Visibility = Visibility.Visible;
                descriptionErrorLabel.Content = "Maximum length is to 255 characters.";
            }
            else
                descriptionErrorLabel.Content = "";

            if (isPriceEmpty)
            {
                priceErrorLabel.Visibility = Visibility.Visible;
                priceErrorLabel.Content = "Price field is required.";
            }
            else
            {
                if (isPriceError)
                {
                    priceErrorLabel.Visibility = Visibility.Visible;
                    priceErrorLabel.Content = "Value must contains dot or be integer number.";
                }
                else
                    priceErrorLabel.Content = "";
            }

            if (isDiscountError)
            {
                discountErrorLabel.Visibility = Visibility.Visible;
                discountErrorLabel.Content = "Integer number between 0 and 100 is required.";
            }
            else
                discountErrorLabel.Content = "";

            if (isAmountEmpty)
            {
                amountErrorLabel.Visibility = Visibility.Visible;
                amountErrorLabel.Content = "Amount field is required.";
            }
            else
            {
                if (isAmountError)
                {
                    amountErrorLabel.Visibility = Visibility.Visible;
                    amountErrorLabel.Content = "Value must contains dot or be integer number.";
                }
                else
                    amountErrorLabel.Content = "";
            }

            if (isUnitEmpty)
            {
                unitErrorLabel.Visibility = Visibility.Visible;
                unitErrorLabel.Content = "Unit field is required.";
            }
            else
            {
                if (isUnitError)
                {
                    unitErrorLabel.Visibility = Visibility.Visible;
                    unitErrorLabel.Content = "Maximum length is form 1 to 255 characters.";
                }
                else
                    unitErrorLabel.Content = "";
            }

            if (isTaxEmpty)
            {
                taxErrorLabel.Visibility = Visibility.Visible;
                taxErrorLabel.Content = "Tax field is required.";
            }
            else
                taxErrorLabel.Content = "";

            if(!isNameEmpty && !isNameError && !isDescriptionEmpty && 
                !isPriceEmpty && !isPriceError && !isDiscountError && 
                !isAmountEmpty && !isAmountError && !isUnitEmpty && !isUnitError)
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

                if (isUpdateFlag)
                    context.Products.Update(product);
                else
                    context.Products.Add(product);

                context.Taxes.Attach(tax);

                context.SaveChanges();
                productWindow.RefreshProductGridData();
                this.Close();
            }

        }

        public void SetTax(Tax newItem)
        {
            tax = newItem;
            addTaxButton.Content = newItem.Name + ", Amount: " + newItem.TaxAmount;
        }

        private void AddTaxButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseTaxWindow chooseTaxWindow = new ChooseTaxWindow(this);
            chooseTaxWindow.Owner = Application.Current.MainWindow;
            chooseTaxWindow.ShowDialog();
        }
    }
}
