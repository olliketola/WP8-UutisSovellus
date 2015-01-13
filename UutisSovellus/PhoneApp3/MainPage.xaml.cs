using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp3.Resources;

using PhoneApp3.Model;
using PhoneApp3.ViewModel;

namespace PhoneApp3
{
    public partial class MainPage : PhoneApplicationPage
    {

        ProductViewModel pvm = new ProductViewModel();
        // Constructor
        public MainPage()
        {

            InitializeComponent();
            // pvm.GetRssFeed("http://olliketola.arkku.net/datahaku.php?uusi=uusimmat");
            pvm.GetRssFeed("http://yle.fi/uutiset/rss/uutiset.rss");
            Listbox.ItemsSource = pvm.Products;

            // pvm.GetRssFeed("http://olliketola.arkku.net/datahaku.php?koti=kotimaa");
            pvm.GetRssFeed("http://yle.fi/uutiset/rss/uutiset.rss?osasto=kotimaa");
            Listbox2.ItemsSource = pvm.Products2;

            pvm.GetRssFeed("http://yle.fi/uutiset/rss/uutiset.rss?osasto=ulkomaat");
            // pvm.GetRssFeed("http://yle.fi/uutiset/rss/uutiset.rss?osasto=kotimaa");
            Listbox3.ItemsSource = pvm.Products3;


        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pivot.SelectedItem == kotimaa)
            {


            }

            if (pivot.SelectedItem == ulkomaa)
            {


            }
        }

        private void update(object sender, EventArgs e)
        {

            if (pivot.SelectedItem == uusimmat)
            {
                pvm.Products.Clear();
                pvm.GetRssFeed("http://yle.fi/uutiset/rss/uutiset.rss");
                Listbox2.ItemsSource = pvm.Products2;
            }

            if (pivot.SelectedItem == kotimaa)
            {
                pvm.Products2.Clear();
                pvm.GetRssFeed("http://yle.fi/uutiset/rss/uutiset.rss?osasto=kotimaa");
                Listbox2.ItemsSource = pvm.Products2;
            }


            if (pivot.SelectedItem == ulkomaa)
            {
                pvm.Products3.Clear();
                pvm.GetRssFeed("http://yle.fi/uutiset/rss/uutiset.rss?osasto=ulkomaat");
                Listbox3.ItemsSource = pvm.Products3;
            }
        }


    }   
}
