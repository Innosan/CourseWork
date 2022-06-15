using CourseWork.Models;
using CourseWork.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        public ObservableCollection<Product> itemsList { get; set; } = new ObservableCollection<Product>();
        public ICollectionView ItemsView { get { return CollectionViewSource.GetDefaultView(itemsList); } }

        public List<Categorie> categoriesList { get; set; } = new List<Categorie>();

        public string[] publicUserName;
        public int publicUserRole;

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

            publicUserName = userName.Split(' ');
            publicUserRole = userRole;

            if (userRole == 1)
            {
                MessageBox.Show("Вы вошли как админ!");
            }
            else
            {
                MessageBox.Show("Ты бедный пользователь, ухади!(");
                addProductBtn.Visibility = Visibility.Collapsed;
            }

            UpdateItemsList();

            using (ApplicationContext context = new ApplicationContext())
            {
                var categories = context.Categories;

                foreach (Categorie categorie in categories)
                {
                    categoriesList.Add(categorie);
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

        private void UpdateItemsList()
        {
            itemsList.Clear();

            using (ApplicationContext context = new ApplicationContext())
            {
                var products = context.Products;

                foreach (Product product in products)
                {
                    itemsList.Add(product);
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
                var tt = new ToolTip();

                tt.Content = nameResults.Errors[0].ErrorMessage;

                nameTextBox.Background = (Brush)(new BrushConverter().ConvertFrom("#59E1452C"));
                nameTextBox.ToolTip = tt;
            }
            else
            {
                nameTextBox.Background = Brushes.Transparent;
                nameTextBox.ToolTip = null;
            }

            if(descResults.IsValid == false)
            {
                var tt = new ToolTip();
                tt.Content= descResults.Errors[0].ErrorMessage;

                descTextBox.Background = (Brush)(new BrushConverter().ConvertFrom("#59E1452C"));
                descTextBox.ToolTip = tt;
            }
            else
            {
                descTextBox.Background = Brushes.Transparent;
                descTextBox.ToolTip = null;
            }

            if (manufactResults.IsValid == false)
            {
                var tt = new ToolTip();
                tt.Content = manufactResults.Errors[0].ErrorMessage;

                manufactTextBox.Background = (Brush)(new BrushConverter().ConvertFrom("#59E1452C"));
                manufactTextBox.ToolTip = tt;
            }
            else
            {
                manufactTextBox.Background = Brushes.Transparent;
                manufactTextBox.ToolTip = null;
            }

            if (nameResults.IsValid && descResults.IsValid && manufactResults.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();

                ClosePopUp();

                MessageBox.Show("Продукт добавлен успешно!");

                UpdateItemsList();
            }
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

        private void closePopUp_Click(object sender, RoutedEventArgs e)
        {
            ClosePopUp();
        }

        private void FilterTab_Click(object sender, RoutedEventArgs e)
        {
            FilterView((ToggleButton)sender);
        }
    }
}