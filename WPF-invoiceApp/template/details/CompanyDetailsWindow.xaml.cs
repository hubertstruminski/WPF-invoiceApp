using ClassLibrary;
using System.Windows.Controls;

namespace WPF_invoiceApp.template.details
{
    /// <summary>
    /// Logika interakcji dla klasy CompanyDetailsWindow.xaml
    /// </summary>
    public partial class CompanyDetailsWindow : UserControl
    {
        public CompanyDetailsWindow()
        {
            InitializeComponent();
        }

        public CompanyDetailsWindow(Company selectedItem) : this()
        {
            companyNameMainLabel.Text = selectedItem.CompanyName;
            companyNameTextField.Text = selectedItem.CompanyName;
            addressTextField.Text = selectedItem.Address;
            postalCodeTextField.Text = selectedItem.PostalCode;
            cityTextField.Text = selectedItem.City;
            countryTextField.Text = selectedItem.Country;
        }
    }
}
