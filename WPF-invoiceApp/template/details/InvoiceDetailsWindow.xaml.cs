using ClassLibrary;
using System.Windows.Controls;
using WPF_invoiceApp.service;

namespace WPF_invoiceApp.template.details
{
    /// <summary>
    /// Logika interakcji dla klasy InvoiceDetailsWindow.xaml
    /// </summary>
    public partial class InvoiceDetailsWindow : UserControl
    {
        private readonly InvoiceService service = new InvoiceService();

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
                scrollViewer.Children.Add(service.CreateProductStackPanel(ip, count));
                count++;
            }
        }
    }
}
