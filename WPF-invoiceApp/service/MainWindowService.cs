using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace WPF_invoiceApp.service
{
    public class MainWindowService
    {
        /// <summary>
        /// Shows UserControl view for specific entity
        /// </summary>
        /// <param name="RightViewBox">Grid object which content is UserControl</param>
        /// <param name="userControl">UserControl object</param>
        public void OnSubViewStartup(Grid RightViewBox, UserControl userControl)
        {
            RightViewBox.Children.Clear();
            RightViewBox.Children.Add(userControl);
        }

        /// <summary>
        /// Shows New Window type object on the screen
        /// </summary>
        /// <param name="type">Class name type</param>
        /// <param name="userControl">UserControl object for show</param>
        /// <param name="context">Db Context</param>
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

        /// <summary>
        /// Creates TextBlock object with content
        /// </summary>
        /// <param name="s1c1">StackPanel which takes TextBlock object</param>
        /// <param name="text">String content object</param>
        /// <param name="isBold">Specify whether label is bold or regular font</param>
        /// <returns>Returns TextBlock object</returns>
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
