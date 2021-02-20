using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace WPF_invoiceApp.service
{
    public class MainWindowService
    {
        public void OnSubViewStartup(Grid RightViewBox, UserControl userControl)
        {
            RightViewBox.Children.Clear();
            RightViewBox.Children.Add(userControl);
        }

        public void OnNewWindowStartup(Type type, UserControl userControl, DbContext context)
        {
            ConstructorInfo[] constructorInfos = type.GetConstructors();
            ConstructorInfo constructor = null;
            foreach(ConstructorInfo cf in constructorInfos)
            {
                ParameterInfo[] parameterInfos = cf.GetParameters();
                if(parameterInfos.Length == 2)
                {
                    constructor = cf;
                    break;
                }
            }
            object instance = constructor.Invoke(new object[] { userControl, context });

            instance.GetType().GetMethod("ShowDialog", new Type[0]).Invoke(instance, new object[0]);
        }

        public TextBlock CreateTextBlock(StackPanel s1c1, string text, bool isBold = false)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            if (isBold)
                textBlock.FontWeight = FontWeights.Bold;
            s1c1.Children.Add(textBlock);

            return textBlock;
        }
    }
}
