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
using WPF_invoiceApp.context;
using WPF_invoiceApp.template.dashboards;

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

        }
    }
}
