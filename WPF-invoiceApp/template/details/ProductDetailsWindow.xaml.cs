using ClassLibrary;
using System.Windows.Controls;

namespace WPF_invoiceApp.template.details
{
    /// <summary>
    /// Logika interakcji dla klasy ProductDetailsWindow.xaml
    /// </summary>
    public partial class ProductDetailsWindow : UserControl
    {
        public ProductDetailsWindow()
        {
            InitializeComponent();
        }

        public ProductDetailsWindow(Product selectedItem) : this()
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
