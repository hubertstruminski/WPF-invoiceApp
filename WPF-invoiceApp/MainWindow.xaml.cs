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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_invoiceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void btnMinimize_Click(object sender, RoutedEventArgs e)
        //{
        //    var window = (Window)((FrameworkElement)sender).TemplatedParent;
        //    window.WindowState = System.Windows.WindowState.Minimized;
        //}

        //private void btnRestore_Click(object sender, RoutedEventArgs e)
        //{
        //    var window = (Window)((FrameworkElement)sender).TemplatedParent;
        //    if (window.WindowState == System.Windows.WindowState.Normal)
        //    {
        //        window.WindowState = System.Windows.WindowState.Maximized;
        //    }
        //    else
        //    {
        //        window.WindowState = System.Windows.WindowState.Normal;
        //    }
        //}

        //private void btnClose_Click(object sender, RoutedEventArgs e)
        //{
        //    var window = (Window)((FrameworkElement)sender).TemplatedParent;
        //    window.Close();
        //}
    }
}
