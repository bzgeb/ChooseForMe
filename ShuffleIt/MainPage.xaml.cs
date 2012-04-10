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
    public partial class MainPage : PhoneApplicationPage
    {
        Setting<List<Item>> items;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            items = Data.items;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Item newItem = new Item("New Item");
            this.ItemListBox.Items.Add(newItem);
            items.Value.Add(newItem);
            RefreshItemBox();

            int itemIndex = items.Value.IndexOf(newItem);
            NavigationService.Navigate(new Uri("/EditItemPage.xaml?itemIndex=" + itemIndex.ToString(), UriKind.Relative));
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            items.Value.Clear();
            this.ItemListBox.Items.Clear();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (ItemListBox.Items.Count < 1)
            {
                foreach (Item item in items.Value)
                {
                    this.ItemListBox.Items.Add(item);
                }
            }
            else
            {
                RefreshItemBox();
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            this.items.Value = this.ItemListBox.Items.OfType<Item>().ToList<Item>();
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (ItemListBox.Items.Count < 1)
            {
                MessageBox.Show("No items to choose");
            }
            else
            {
                Random random = new Random();

                MessageBox.Show((ItemListBox.Items[random.Next(ItemListBox.Items.Count)] as Item).Name);
            }
        }

        private void ShuffleButton_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int i = items.Value.Count;
            int j = 0;
            Item temp;
            while (i > 1)
            {
                --i;
                j = random.Next(i);
                temp = items.Value[i] as Item;
                items.Value[i] = items.Value[j];
                items.Value[j] = temp;
            }

            RefreshItemBox();
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            Item item = (sender as MenuItem).Tag as Item;
            int itemIndex = items.Value.IndexOf(item);
            NavigationService.Navigate(new Uri("/EditItemPage.xaml?itemIndex=" + itemIndex.ToString(), UriKind.Relative));
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Item item = (sender as MenuItem).Tag as Item;
            ItemListBox.Items.Remove(item);
            items.Value.Remove(item);
        }

        private void RefreshItemBox()
        {
            foreach (Item item in items.Value)
            {
                int index = items.Value.IndexOf(item);
                ItemListBox.Items[index] = item;
            }
        }
    }
}