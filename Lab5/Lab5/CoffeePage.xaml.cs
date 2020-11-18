using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Lab5
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CoffeePage : Page
    {
        String Roast = "";
        String Sweetener = "";
        String Cream = "";

        public CoffeePage()
        {
            this.InitializeComponent();
        }

        private void UpdateDisplay()
        {
            String _Sweetener = " + ";
            String _Cream = " + ";

            if (Roast.Equals(""))
            {
                CoffeeParameters.Text = "";
                return;
            }
            if (Sweetener.Equals("")){
                _Sweetener = "";
            }
            else
            {
                _Sweetener = _Sweetener + Sweetener;
            }
            if (Cream.Equals("")){
                _Cream = "";
            }
            else
            {
                _Cream = _Cream + Cream;
            }

            CoffeeParameters.Text = Roast + _Sweetener + _Cream;
        }

        private void RoastNone(object sender, RoutedEventArgs e)
        {
            Roast = "";
            UpdateDisplay();
        }

        private void RoastDark(object sender, RoutedEventArgs e)
        {
            Roast = "Dark";
            UpdateDisplay();
        }

        private void RoastMedium(object sender, RoutedEventArgs e)
        {
            Roast = "Medium";
            UpdateDisplay();
        }

        private void SweetenerNone(object sender, RoutedEventArgs e)
        {
            Sweetener = "";
            UpdateDisplay();
        }

        private void SweetenerSugar(object sender, RoutedEventArgs e)
        {
            Sweetener = "Sugar";
            UpdateDisplay();
        }

        private void CreamNone(object sender, RoutedEventArgs e)
        {
            Cream = "";
            UpdateDisplay();
        }

        private void Cream2Percent(object sender, RoutedEventArgs e)
        {
            Cream = "2% Milk";
            UpdateDisplay();
        }

        private void CreamWhole(object sender, RoutedEventArgs e)
        {
            Cream = "Whole Milk";
            UpdateDisplay();
        }
    }
}
