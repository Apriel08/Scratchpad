using System.Collections.ObjectModel;
using MauiApp2;

namespace MauiApp2
{
    public partial class MainPage : ContentPage
    {
        // ObservableCollection to hold items
        private ObservableCollection<Item> items = new ObservableCollection<Item>();

        // Nullable field for the selected item
        private Item? selectedItem;  // Made nullable by adding '?'

        // Property to bind the Items collection to the ListView
        public ObservableCollection<Item> Items { get => items; set => items = value; }

        public MainPage()
        {
            InitializeComponent();
            ItemsListView.ItemsSource = Items; // Bind ListView to the ObservableCollection
        }

        // Add item to the collection
        private void OnAddItemClicked(object sender, EventArgs e)
        {
            var name = NameEntry.Text?.Trim();
            var description = DescriptionEntry.Text?.Trim();

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(description))
            {
                // Add a new item to the collection
                Items.Add(new Item { Name = name, Description = description });

                // Clear the input fields after adding
                NameEntry.Text = string.Empty;
                DescriptionEntry.Text = string.Empty;
            }
            else
            {
                // Display a validation error
                DisplayAlert("Validation Error", "Both Name and Description are required.", "OK");
            }
        }

        private void DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }

        // Handle item selection for editing
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Item item)
            {
                selectedItem = item;
                EditNameEntry.Text = item.Name;
                EditDescriptionEntry.Text = item.Description;
                EditSection.IsVisible = true;
            }
        }

        // Update selected item
        private void OnUpdateItemClicked(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                var newName = EditNameEntry.Text?.Trim();
                var newDescription = EditDescriptionEntry.Text?.Trim();

                if (!string.IsNullOrEmpty(newName) && !string.IsNullOrEmpty(newDescription))
                {
                    selectedItem.Name = newName;
                    selectedItem.Description = newDescription;

                    // Refresh the ListView to show updated item
                    ItemsListView.ItemsSource = null;
                    ItemsListView.ItemsSource = Items;

                    // Hide the edit section
                    EditSection.IsVisible = false;
                }
                else
                {
                    DisplayAlert("Validation Error", "Both Name and Description are required.", "OK");
                }
            }
        }

        // Delete selected item
        private void OnDeleteItemClicked(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                Items.Remove(selectedItem);
                selectedItem = null;

                // Hide the edit section
                EditSection.IsVisible = false;
            }
        }
    }

    // Item class to represent the data model
    public class Item
    {
        // Making Description nullable so it's not required
        public required string Name { get; set; }
        public string? Description { get; set; } // Nullable type
    }
}
