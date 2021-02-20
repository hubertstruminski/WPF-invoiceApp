using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WPF_invoiceApp.template.details;

namespace WPF_invoiceApp.service
{
    public class InvoiceService
    {
        private readonly MainWindowService service = new MainWindowService();

        public void OnSubViewDetailsShow(InvoiceDetailsWindow invoiceDetailsWindow, Grid RightViewBox)
        {
            RightViewBox.Children.Clear();

            RightViewBox.VerticalAlignment = VerticalAlignment.Stretch;
            RightViewBox.HorizontalAlignment = HorizontalAlignment.Stretch;

            RightViewBox.Children.Add(invoiceDetailsWindow);
        }

        public StackPanel CreateProductStackPanel(InvoiceProduct ip, int count)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;

            service.CreateTextBlock(stackPanel, count + ". " + ip.Product.Name, true);
            service.CreateTextBlock(stackPanel, "Amount: " + ip.Product.Amount);
            service.CreateTextBlock(stackPanel, "Price: " + ip.Product.Price + "$");

            return stackPanel;
        }
    }
}
