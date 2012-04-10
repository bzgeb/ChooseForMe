using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace ShuffleIt
{
    public partial class EditItemPage : PhoneApplicationPage
    {
        Item item;
        int itemIndex;

        public EditItemPage()
        {
            InitializeComponent();
            Loaded += PageLoaded;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!NavigationContext.QueryString.ContainsKey("itemIndex"))
            {
                return;
            }

            itemIndex = int.Parse(NavigationContext.QueryString["itemIndex"]);
            item = Data.items.Value[itemIndex];
            NameTextBox.Text = item.Name;
        }

        private void PageLoaded(Object sender, RoutedEventArgs e)
        {
            NameTextBox.Focus();
            NameTextBox.SelectAll();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            item.Name = NameTextBox.Text;
            Data.items.Value[itemIndex] = item;
            NavigationService.GoBack();
        }
    }
}