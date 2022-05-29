﻿using System.ComponentModel.DataAnnotations;

namespace CourseWork
{
    internal class Product
    {
        [Key]
        public int prodId { get; set; }

        private string prodName;
        private string prodDescription;
        private string prodManufacturer;
        private string prodPrice;
        private string prodQuantity;
        /*private string prodPicture;*/

        public string ProdName { get { return prodName; } set { prodName = value; } }
        public string ProdDescription { get { return prodDescription; } set { prodDescription = value; } }
        public string ProdManufacturer { get { return prodManufacturer; } set { prodManufacturer = value; } }
        public string ProdPrice { get { return prodPrice; } set { prodPrice = value; } }
        public string ProdQuantity { get { return prodQuantity; } set { prodQuantity = value; } }
        /*public string ProdPicture { get { return prodPicture; } set { prodPicture = value; } }*/

        public Product() { }

        public Product(string name, string desc, string manufacturer, string price, string quantity /*string picLink*/)
        {
            this.prodName = name;
            this.prodDescription = desc;
            this.prodManufacturer = manufacturer;
            this.prodPrice = price;
            this.prodQuantity = quantity;
            /*this.prodPicture = picLink;*/
        }
    }
}
