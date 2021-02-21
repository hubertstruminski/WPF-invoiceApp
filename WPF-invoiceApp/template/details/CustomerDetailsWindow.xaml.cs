using ClassLibrary;
using System.Windows.Controls;
using WPF_invoiceApp.service;

namespace WPF_invoiceApp.template.details
{
    /// <summary>
    /// Logika interakcji dla klasy CustomerDetailsWindow.xaml
    /// </summary>
    public partial class CustomerDetailsWindow : UserControl
    {
        private readonly CustomerService service = new CustomerService();

        public CustomerDetailsWindow()
        {
            InitializeComponent();
        }

        public CustomerDetailsWindow(Customer selectedItem) : this()
        {
            nameLabel.Text = selectedItem.Name;
            emailValueLabel.Text = selectedItem.Email;
            addressValueLabel.Text = selectedItem.Address.AddressName;
            phoneNumberValueLabel.Text = selectedItem.PhoneNumber;
            countryValueLabel.Text = selectedItem.Address.Country;
            nipValueLabel.Text = selectedItem.Nip;
            commentValueLabel.Text = selectedItem.Note;
            websiteValueLabel.Text = selectedItem.Website;

            scrollViewer.Children.Clear();

            foreach (Invoice invoice in selectedItem.Invoices)
            {
                scrollViewer.Children.Add(service.CreateInvoiceGrid(invoice));
            }
        }
    }
}
