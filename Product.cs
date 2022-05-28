namespace CourseWork
{
    internal class Product
    {
        public int prodId { get; set; }

        private string prodName;
        private string prodDescription;
        private int prodManufacturer;
        private int prodPrice;
        private int prodQuantity;
        private string prodPicture;

        public string ProdName { get { return prodName; } set { prodName = value; } }
        public string ProdDescription { get { return prodDescription; } set { prodDescription = value; } }
        public int ProdManufacturer { get { return prodManufacturer; } set { prodManufacturer = value; } }
        public int ProdPrice { get { return prodPrice; } set { prodPrice = value; } }
        public int ProductQuantity { get { return prodQuantity; } set { prodQuantity = value; } }
        public string ProdPicture { get { return prodPicture; } set { prodPicture = value; } }

        public Product() { }

        public Product(string name, string desc, int manufacturer, int price, int quantity, string picLink)
        {
            this.prodName = name;
            this.prodDescription = desc;
            this.prodManufacturer = manufacturer;
            this.prodPrice = price;
            this.prodQuantity = quantity;
            this.prodPicture = picLink;
        }
    }
}
