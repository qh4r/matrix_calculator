using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc.Controls
{
    using System.Windows;

    public class BindableWindow : Window
    {
        public static readonly DependencyProperty ShouldCloseProperty = DependencyProperty.Register(
            "ShouldClose",
            typeof(bool),
            typeof(Window),
            new PropertyMetadata(default(bool), ShouldCloseChangedCallback));

        public bool ShouldClose
        {
            get
            {
                return (bool)this.GetValue(ShouldCloseProperty);
            }

            set
            {
                this.SetValue(ShouldCloseProperty, value);
            }
        }

        private static void ShouldCloseChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as BindableWindow;
            if (window != null && (bool)e.NewValue)
            {
                window.Close();
            }
        }
    }


}
