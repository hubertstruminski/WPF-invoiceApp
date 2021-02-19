using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.template.details
{
    /// <summary>
    /// Logika interakcji dla klasy ProductDetailsWindow.xaml
    /// </summary>
    public partial class ProductDetailsWindow : UserControl
    {
        private DatabaseContext context;

        public ProductDetailsWindow(DatabaseContext context)
        {
            InitializeComponent();
            this.context = context;
        }

        public ProductDetailsWindow(Product selectedItem, DatabaseContext context) : this(context)
        {
            nameMainLabel.Text = selectedItem.Name;
            nameTextField.Text = selectedItem.Name;
            priceTextField.Text = selectedItem.Price.ToString();
            amountTextField.Text = selectedItem.Amount.ToString();
            discountTextField.Text = selectedItem.Discount.ToString();

            unitValueLabel.Text = selectedItem.Unit;
            taxValueLabel.Text = selectedItem.Tax.Name + ", Stawka: " + selectedItem.Tax.TaxAmount;
            descriptionTextField.Text = selectedItem.Description;

            scrollViewer.Children.Clear();

            int count = 1;
            foreach(InvoiceProduct ip in selectedItem.InvoiceProducts)
            {
                TextBlock text = new TextBlock();
                text.Text = count + ". Number: " + ip.Invoice.Number + ", Date: " + ip.Invoice.Date + ", Deadline: " + ip.Invoice.Deadline;
                text.FontSize = 16;
              
                scrollViewer.Children.Add(text);
                count++;
            }
        }
    }
}
