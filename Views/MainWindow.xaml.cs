using CourseWork.Models;
using CourseWork.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace CourseWork
{
    public partial class MainWindow : Window
    {
        ApplicationContext db;

        public ObservableCollection<Product> ItemsList { get; set; } = new ObservableCollection<Product>();
        public ICollectionView ItemsView { get { return CollectionViewSource.GetDefaultView(ItemsList); } }

        public List<Categorie> CategoriesList { get; set; } = new List<Categorie>();

        private string search;
        public string Search
        {
            get { return search; }
            set { search = value; NotifyPropertyChanged("Search"); ItemsView.Refresh(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { 
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); 
            } 
        }

        public MainWindow(string userName, int userRole)
        {
            InitializeComponent();

            db = new ApplicationContext();

            ItemsView.Filter = new Predicate<object>(o => Filter(o as Product));

            UpdateItemsList();

            using (ApplicationContext context = new ApplicationContext())
            {
                var categories = context.Categories;

                foreach (Categorie categorie in categories)
                {
                    CategoriesList.Add(categorie);
                }
            }
        }       

        private void goBackBtn_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new LogInWindow();

            newWindow.Show();
            this.Close();
        }

        private void addProductBtn_Click(object sender, RoutedEventArgs e)
        {
            addProdPopup.IsOpen = true;
        }

        private void closePopupBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            string desc = descTextBox.Text;
            string manufact = manufactTextBox.Text;
            string categorie = categoriesComboBox.Text;
            string price = priceTextBox.Text;
            string quantity = quantityTextBox.Text;

            Product product = new Product(name, desc, manufact, categorie, price, quantity);

            ProductNameValidator productNameValidator = new ProductNameValidator();
            FluentValidation.Results.ValidationResult nameResults = productNameValidator.Validate(product);

            ProductDescValidator productDescValidator = new ProductDescValidator();
            FluentValidation.Results.ValidationResult descResults = productDescValidator.Validate(product);

            ProductManufactValidator productManufactValidator = new ProductManufactValidator();
            FluentValidation.Results.ValidationResult manufactResults = productManufactValidator.Validate(product);

            if (nameResults.IsValid == false)
            {
                SetInvalidUI(nameTextBox, nameResults);
            }
            else
            {
                SetValidUI(nameTextBox);
            }

            if(descResults.IsValid == false)
            {
                SetInvalidUI(descTextBox, descResults);
            }
            else
            {
                SetValidUI(descTextBox);
            }

            if (manufactResults.IsValid == false)
            {
                SetInvalidUI(manufactTextBox, manufactResults);
            }
            else
            {
                SetValidUI(manufactTextBox);
            }

            if (nameResults.IsValid && descResults.IsValid && manufactResults.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();

                ClosePopUp();

                UpdateItemsList();
            }
        }

        private void closePopUp_Click(object sender, RoutedEventArgs e)
        {
            ClosePopUp();
        }

        private void FilterTab_Click(object sender, RoutedEventArgs e)
        {
            FilterView((ToggleButton)sender);
        }

        private void UpdateItemsList()
        {
            ItemsList.Clear();

            using (ApplicationContext context = new ApplicationContext())
            {
                var products = context.Products;

                foreach (Product product in products)
                {
                    ItemsList.Add(product);
                }
            }
        }
        private bool Filter(Product product)
        {
            return Search == null
                || product.ProdName.IndexOf(Search, StringComparison.OrdinalIgnoreCase) != -1
                || product.ProdManufacturer.IndexOf(Search, StringComparison.OrdinalIgnoreCase) != -1
                || product.ProdDescription.IndexOf(Search, StringComparison.OrdinalIgnoreCase) != -1;
        }

        private void FilterView(ToggleButton sender)
        {
            if (sender.IsChecked == true)
            {
                ItemsView.Filter = product => ((Product)product).ProdCategorie == sender.Content.ToString();
            }
            else
            {
                ItemsView.Filter = null;
            }
        }

        private void SetInvalidUI(TextBox textBox, FluentValidation.Results.ValidationResult result)
        {
            ToolTip toolTip = new ToolTip
            {
                Content = result.Errors[0].ErrorMessage
            };

            textBox.Background = (Brush)(new BrushConverter().ConvertFrom("#59E1452C"));
            textBox.ToolTip = toolTip;
        }

        private void SetValidUI(TextBox textBox)
        {
            textBox.Background = Brushes.Transparent;
            textBox.ToolTip = null;
        }

        private void ClosePopUp()
        {
            nameTextBox.Background = Brushes.Transparent;
            nameTextBox.ToolTip = null;

            descTextBox.Background = Brushes.Transparent;
            descTextBox.ToolTip = null;

            manufactTextBox.Background = Brushes.Transparent;
            manufactTextBox.ToolTip = null;

            nameTextBox.Text = "";
            descTextBox.Text = "";
            manufactTextBox.Text = "";
            priceTextBox.Text = "";
            quantityTextBox.Text = "";

            addProdPopup.IsOpen = false;
        }
    }
}