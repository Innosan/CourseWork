using CourseWork.Models;
using CourseWork.Validators;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CourseWork
{
    public partial class MainWindow : Window
    {
        ApplicationContext db;

        public ObservableCollection<Product> ItemsList { get; set; } = new ObservableCollection<Product>();
        public ICollectionView ItemsView { get { return CollectionViewSource.GetDefaultView(ItemsList); } }

        public List<Categorie> CategoriesList { get; set; } = new List<Categorie>();

        public int UserRole;

        public string productImageLink;

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

            UserRole = userRole;

            db = new ApplicationContext();

            DateTime time = DateTime.Now;

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

            if (userRole == 1)
            {
                roleLable.Text = "админ!";
            }
            else
            {
                addProductBtn.Visibility = Visibility.Collapsed;

                roleLable.Text = "юзер(";
            };

            if (time.Hour >= 18) accountGreetingTextBox.Text = $"Добрый вечер,\n{userName}!";                            // заменить кринж чем-нибудь
            else
                if (time.Hour < 6) accountGreetingTextBox.Text = $"Доброй ночи,\n{userName}!";
            else
                    if (time.Hour < 18 && time.Hour >= 12) accountGreetingTextBox.Text = $"Добрый день,\n{userName}!";
                    else accountGreetingTextBox.Text = $"Доброе утро,\n{userName}!";
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
            string image = productImageLink;

            Product product = new Product(name, desc, manufact, categorie, price, quantity, image);

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
                try
                {
                    db.Products.Add(product);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

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
            FilterView((RadioButton)sender);
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

        private void FilterView(RadioButton sender)
        {
            if (sender.IsChecked == true)
            {
                ItemsView.Filter = product => ((Product)product).ProdCategorie == sender.Content.ToString() && Filter(product as Product);
            }
            else
            {
                ItemsView.Filter = new Predicate<object>(o => Filter(o as Product));
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

        private void deleteItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UserRole == 1)
            {
                var currentItem = ((ListBoxItem)listView.ContainerFromElement((Button)sender)).Content;

                Product deleteItem = (Product)currentItem;

                using (ApplicationContext context = new ApplicationContext())
                {
                    var prod = context.Products.Single(o => o.prodId == deleteItem.prodId);
                    context.Products.Remove(prod);
                    context.SaveChanges();
                }

                UpdateItemsList();
            }

            else
            {
                MessageBox.Show("Братишка, не туда ты нажал, за тобой выехали уже.");

                ((Button)sender).IsEnabled = false;              
            }

            
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            ItemsView.Filter = new Predicate<object>(o => Filter(o as Product));
        }

        private void addImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = true,

                Filter = "img files (*.jpg,.*png,*.jpeg)|*.jpg;.*png;*.jpeg"
            };

            dialog.ShowDialog();

            if (dialog.FileName.ToString() == "")
            {
                return;
            }

            productImageLink = dialog.FileName;
        }
    }
}