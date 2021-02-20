using ClassLibrary;
using System.Windows;
using System.Windows.Controls;

namespace WPF_invoiceApp.template.details
{
    /// <summary>
    /// Logika interakcji dla klasy InvoiceDetailsWindow.xaml
    /// </summary>
    public partial class InvoiceDetailsWindow : UserControl
    {
        public InvoiceDetailsWindow()
        {
            InitializeComponent();
        }

        public InvoiceDetailsWindow(Invoice selectedItem) : this()
        {
            numberInvoiceValueLabel.Text = selectedItem.Number;
            dateValueLabel.Text = selectedItem.Date.ToString();
            deadlineValueLabel.Text = selectedItem.Deadline.ToString();
            customerValueLabel.Text = selectedItem.Customer.Name + ", Address: " + selectedItem.Customer.Address.AddressName + ", " + selectedItem.Customer.Address.Country;
            commentValueLabel.Text = selectedItem.Comment;

            scrollViewer.Children.Clear();

            int count = 1;
            foreach(InvoiceProduct ip in selectedItem.InvoiceProducts)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Vertical;

                TextBlock name = new TextBlock();
                name.Text = count + ". " + ip.Product.Name;
                name.FontWeight = FontWeights.Bold;
                stackPanel.Children.Add(name);

                TextBlock amount = new TextBlock();
                amount.Text = "Amount: " + ip.Product.Amount;
                stackPanel.Children.Add(amount);

                TextBlock price = new TextBlock();
                price.Text = "Price: " + ip.Product.Price + "$";
                stackPanel.Children.Add(price);

                scrollViewer.Children.Add(stackPanel);
                count++;
            }
        }
    }
}
