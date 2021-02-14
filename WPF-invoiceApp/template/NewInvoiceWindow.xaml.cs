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
using System.Windows.Shapes;
using WPF_invoiceApp.context;
using WPF_invoiceApp.template.dashboards;

namespace WPF_invoiceApp.template
{
    /// <summary>
    /// Logika interakcji dla klasy NewInvoiceWindow.xaml
    /// </summary>
    public partial class NewInvoiceWindow : Window
    {
        private InvoiceWindow invoiceWindow;
        private DatabaseContext context = new DatabaseContext();

        public NewInvoiceWindow()
        {
            InitializeComponent();
            context.Database.EnsureCreated();
        }

        public NewInvoiceWindow(InvoiceWindow invoiceWindow) : this()
        {
            this.invoiceWindow = invoiceWindow;
        }

        private void OnSaveInvoiceButtonAction(object sender, RoutedEventArgs e)
        {

        }
    }
}
