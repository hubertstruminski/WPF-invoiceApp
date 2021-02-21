using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Windows;
using WPF_invoiceApp.context;
using WPF_invoiceApp.repository;
using WPF_invoiceApp.service;
using WPF_invoiceApp.template.dashboards;
using WPF_invoiceApp.template.lists;

namespace WPF_invoiceApp.template
{
    /// <summary>
    /// Logika interakcji dla klasy NewInvoiceWindow.xaml
    /// </summary>
    public partial class NewInvoiceWindow : Window
    {
        private readonly InvoiceWindow invoiceWindow;
        private readonly DatabaseContext context;
        private readonly Invoice invoice;
        private readonly List<Product> products;
        private readonly bool isUpdateFlag = false;

        private Customer customer;

        private readonly InvoiceService service = new InvoiceService();
        private readonly InvoiceRepository repository = new InvoiceRepository();

        public NewInvoiceWindow(DatabaseContext context)
        {
            this.context = context;
            InitializeComponent();
            customer = new Customer();
            invoice = new Invoice();
            products = new List<Product>();
        }

        public NewInvoiceWindow(InvoiceWindow invoiceWindow, DatabaseContext context) : this(context)
        {
            this.invoiceWindow = invoiceWindow;
        }

        public NewInvoiceWindow(Invoice selectedItem, InvoiceWindow invoiceWindow, DatabaseContext context) : this(context)
        {
            numberTextField.Text = selectedItem.Number;
            datePicker.SelectedDate = selectedItem.Date;
            deadlinePicker.SelectedDate = selectedItem.Deadline;
            addCustomerButton.Content = selectedItem.Customer.Name;
            listProductsButton.Content = selectedItem.InvoiceProducts.Count + " Item(s)";
            commentTextField.Text = selectedItem.Comment;

            customer = selectedItem.Customer;

            invoice = selectedItem;
            isUpdateFlag = true;
            this.invoiceWindow = invoiceWindow;
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseProductWindow chooseProductWindow = new ChooseProductWindow(this, context);
            chooseProductWindow.Owner = Application.Current.MainWindow;
            chooseProductWindow.ShowDialog();
        }

        private void OnAddProductClearButton_Click(object sender, RoutedEventArgs e)
        {
            products.Clear();
            listProductsButton.Content = "";
        }

        private void OnSaveInvoiceButtonAction(object sender, RoutedEventArgs e)
        {
            bool isNumberEmpty = service.ValidateNumberTextField(numberTextField, numberErrorLabel);
            bool isCustomerEmpty = service.ValidateCustomerButton(addCustomerButton, customerErrorLabel);
            bool isProductEmpty = service.ValidateProductButton(listProductsButton, productErrorLabel);

            if(!isNumberEmpty && !isCustomerEmpty && !isProductEmpty)
            {
                invoice.Number = numberTextField.Text;
                invoice.Date = datePicker.SelectedDate == null ? DateTime.Now : datePicker.SelectedDate.Value;
                invoice.Deadline = deadlinePicker.SelectedDate == null ? DateTime.Now : deadlinePicker.SelectedDate.Value;
                invoice.Customer = customer;
                invoice.Comment = commentTextField.Text;
                invoice.Status = Status.NOT_SENT;

                invoice.InvoiceProducts = new List<InvoiceProduct>();


                foreach(Product product in products)
                {
                    InvoiceProduct ip = new InvoiceProduct();
                    ip.Invoice = invoice;
                    ip.Product = product;

                    invoice.InvoiceProducts.Add(ip);
                }

                repository.AddInvoice(invoice, context, isUpdateFlag);
                invoiceWindow.RefreshInvoiceGridData();
                Close();
            }
        }

        /// <summary>
        /// Sets Customer object
        /// </summary>
        /// <param name="selectedItem">Customer object</param>
        public void SetCustomer(Customer selectedItem)
        {
            customer = selectedItem;
            addCustomerButton.Content = selectedItem.Name;
        }

        /// <summary>
        /// Adds Product object to List and assign value to button
        /// </summary>
        /// <param name="selectedItem">Selected Product object from DataGrid</param>
        public void SetProduct(Product selectedItem)
        {
            products.Add(selectedItem);
            listProductsButton.Content = products.Count + " Item(s)"; 
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseCustomerWindow chooseCustomerWindow = new ChooseCustomerWindow(this, context);
            chooseCustomerWindow.Owner = Application.Current.MainWindow;
            chooseCustomerWindow.ShowDialog();
        }
    }
}
