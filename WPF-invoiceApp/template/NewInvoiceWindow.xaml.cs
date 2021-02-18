﻿using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using WPF_invoiceApp.template.lists;

namespace WPF_invoiceApp.template
{
    /// <summary>
    /// Logika interakcji dla klasy NewInvoiceWindow.xaml
    /// </summary>
    public partial class NewInvoiceWindow : Window
    {
        private const string BUTTON_TEXT = ">";

        private InvoiceWindow invoiceWindow;
        private DatabaseContext context = new DatabaseContext();
        private Invoice invoice;
        private Customer customer;
        private List<Product> products;
        private bool isUpdateFlag = false;

        public NewInvoiceWindow()
        {
            InitializeComponent();
            context.Database.EnsureCreated();
            customer = new Customer();
            invoice = new Invoice();
            products = new List<Product>();
        }

        public NewInvoiceWindow(InvoiceWindow invoiceWindow) : this()
        {
            this.invoiceWindow = invoiceWindow;
        }

        public NewInvoiceWindow(Invoice selectedItem, InvoiceWindow invoiceWindow) : this()
        {
            numberTextField.Text = selectedItem.Number;
            datePicker.SelectedDate = selectedItem.Date;
            deadlinePicker.SelectedDate = selectedItem.Deadline;
            addCustomerButton.Content = selectedItem.Customer.Name;
            listProductsButton.Content = selectedItem.Products.Count + " Item(s)";
            commentTextField.Text = selectedItem.Comment;

            customer = selectedItem.Customer;
            products = selectedItem.Products;
            
            invoice = selectedItem;
            isUpdateFlag = true;
            this.invoiceWindow = invoiceWindow;
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseProductWindow chooseProductWindow = new ChooseProductWindow(this);
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
            bool isNumberEmpty = false;
            bool isCustomerEmpty = false;
            bool isProductEmpty = false;

            if (numberTextField.Text.Length == 0)
                isNumberEmpty = true;

            if (addCustomerButton.Content.Equals(""))
                isCustomerEmpty = true;

            if (listProductsButton.Content.Equals(""))
                isProductEmpty = true;

            if(isNumberEmpty)
            {
                numberErrorLabel.Visibility = Visibility.Visible;
                numberErrorLabel.Content = "Number is required.";
            } 
            else
            {
                numberErrorLabel.Content = "";
            }

            if(isCustomerEmpty)
            {
                customerErrorLabel.Visibility = Visibility.Visible;
                customerErrorLabel.Content = "Customer is required.";
            }
            else
            {
                customerErrorLabel.Content = "";
            }

            if(isProductEmpty)
            {
                productErrorLabel.Visibility = Visibility.Visible;
                productErrorLabel.Content = "Product is required.";
            }
            else
            {
                productErrorLabel.Content = "";
            }

            if(!isNumberEmpty && !isCustomerEmpty && !isProductEmpty)
            {
                context.Addresses.Load();
                invoice.Number = numberTextField.Text;
                invoice.Date = datePicker.SelectedDate == null ? DateTime.Now : datePicker.SelectedDate.Value;
                invoice.Deadline = deadlinePicker.SelectedDate == null ? DateTime.Now : deadlinePicker.SelectedDate.Value;
                invoice.Customer = customer;
                invoice.Comment = commentTextField.Text;
                invoice.Status = Status.NOT_SENT;
                invoice.Products = products;

                foreach (Product x in products)
                {
                    context.Taxes.Attach(x.Tax);
                    context.Products.Attach(x);
                }
                Address address = context.Addresses.Include("Customer").Where(x => x.Id == customer.Address.Id).Single();
                context.Entry<Address>(address).State = EntityState.Detached;

                context.Customers.Attach(customer);

                if (isUpdateFlag)
                    context.Invoices.Update(invoice);
                else
                    context.Invoices.Add(invoice);

                context.SaveChanges();
                invoiceWindow.RefreshInvoiceGridData();

                this.Close();
            }

        }

        public void SetCustomer(Customer selectedItem)
        {
            customer = selectedItem;
            addCustomerButton.Content = selectedItem.Name;
        }

        public void SetProduct(Product selectedItem)
        {
            products.Add(selectedItem);
            listProductsButton.Content = products.Count + " Item(s)"; 
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseCustomerWindow chooseCustomerWindow = new ChooseCustomerWindow(this);
            chooseCustomerWindow.Owner = Application.Current.MainWindow;
            chooseCustomerWindow.ShowDialog();
        }

        public async Task<Address> GetNoTrackingAddress(long id)
        {
            return await context.Addresses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
