using ClassLibrary;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.template.details
{
    /// <summary>
    /// Logika interakcji dla klasy CompanyDetailsWindow.xaml
    /// </summary>
    public partial class CompanyDetailsWindow : UserControl
    {
        private DatabaseContext context;

        public CompanyDetailsWindow(DatabaseContext context)
        {
            InitializeComponent();
            this.context = context;
        }

        public CompanyDetailsWindow(Company selectedItem, DatabaseContext context) : this(context)
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
