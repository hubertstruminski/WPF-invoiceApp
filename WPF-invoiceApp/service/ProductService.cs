using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WPF_invoiceApp.template.details;

namespace WPF_invoiceApp.service
{
    public class ProductService
    {
        public void OnSubViewDetailsShow(ProductDetailsWindow productDetailsWindow, Grid RightViewBox)
        {
            RightViewBox.Children.Clear();

            RightViewBox.VerticalAlignment = VerticalAlignment.Stretch;
            RightViewBox.HorizontalAlignment = HorizontalAlignment.Stretch;

            RightViewBox.Children.Add(productDetailsWindow);
        }

        public TextBlock CreateInvoiceTextBlock(InvoiceProduct ip, int count)
        {
            TextBlock text = new TextBlock();
            text.Text = count + ". Number: " + ip.Invoice.Number + ", Date: " + ip.Invoice.Date + ", Deadline: " + ip.Invoice.Deadline;
            text.FontSize = 16;

            return text;
        }
    }
}
