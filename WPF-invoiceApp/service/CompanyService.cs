using System.Windows;
using System.Windows.Controls;
using WPF_invoiceApp.template.details;

namespace WPF_invoiceApp.service
{
    public class CompanyService
    {
        public void OnSubViewDetailsShow(CompanyDetailsWindow companyDetailsWindow, Grid RightViewBox)
        {
            RightViewBox.Children.Clear();

            RightViewBox.VerticalAlignment = VerticalAlignment.Stretch;
            RightViewBox.HorizontalAlignment = HorizontalAlignment.Stretch;

            RightViewBox.Children.Add(companyDetailsWindow);
        }
    }
}
