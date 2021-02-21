using ClassLibrary;
using System.Windows;
using WPF_invoiceApp.context;
using WPF_invoiceApp.repository;
using WPF_invoiceApp.service;
using WPF_invoiceApp.template.dashboards;

namespace WPF_invoiceApp.template
{
    /// <summary>
    /// Logika interakcji dla klasy NewCompanyWindow.xaml
    /// </summary>
    public partial class NewCompanyWindow : Window
    {
        private readonly CompanyWindow companyWindow;
        private readonly DatabaseContext context;
        private readonly bool isUpdateFlag = false;

        private Company company;

        private readonly CompanyService service = new CompanyService();
        private readonly CompanyRepository repository = new CompanyRepository();

        public NewCompanyWindow(DatabaseContext context)
        {
            this.context = context;
            InitializeComponent();
        }

        public NewCompanyWindow(CompanyWindow companyWindow, DatabaseContext context) : this(context)
        {
            this.companyWindow = companyWindow;
        }

        public NewCompanyWindow(Company company, DatabaseContext context, CompanyWindow companyWindow) : this(context)
        {
            companyNameTextField.Text = company.CompanyName;
            addressTextField.Text = company.Address;
            cityTextField.Text = company.City;
            postalcodeTextField.Text = company.PostalCode;
            countryTextField.Text = company.Country;

            this.company = company;
            isUpdateFlag = true;
            this.companyWindow = companyWindow;
        }

        private void OnNewCompanySaveButton(object sender, RoutedEventArgs e)
        {
            (bool isCompanyNameEmpty, bool isCompanyNameError) = service.ValidateCompanyNameTextField(companyNameTextField, companyNameErrorLabel);
            (bool isAddressEmpty, bool isAddressError) = service.ValidateAddressTextField(addressTextField, addressErrorLabel);
            (bool isPostalCodeEmpty, bool isPostalCodeError) = service.ValidatePostalcodeTextField(postalcodeTextField, postalcodeErrorLabel);
            (bool isCityEmpty, bool isCityError) = service.ValidateCityTextField(cityTextField, cityErrorLabel);
            (bool isCountryEmpty, bool isCountryError) = service.ValidateCountryTextField(countryTextField, countryErrorLabel);

            if (isUpdateFlag)
            {
                isCompanyNameEmpty = false;
                isAddressEmpty = false;
                isPostalCodeEmpty = false;
                isCityEmpty = false;
                isCountryEmpty = false;
            }

            if(!isCompanyNameEmpty && !isCompanyNameError && !isAddressEmpty && 
                !isAddressError && !isPostalCodeEmpty && !isPostalCodeError && 
                !isCityEmpty && !isCityError && !isCountryEmpty && !isCountryError)
            {
                if (!isUpdateFlag)
                    company = new Company();

                company.CompanyName = companyNameTextField.Text;
                company.Address = addressTextField.Text;
                company.City = cityTextField.Text;
                company.Country = countryTextField.Text;
                company.PostalCode = postalcodeTextField.Text;

                repository.AddCompany(company, context, isUpdateFlag);

                companyWindow.RefreshCompanyGridData();
                Close();
            }
        }   

    }
}
