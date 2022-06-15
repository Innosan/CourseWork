using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models
{
    public class Categorie
    {
        [Key]
        public int categorieId { get; set; }

        private string categorieName;

        public string CategorieName { get { return categorieName; } set { categorieName = value; } }

        public Categorie() { }

        public Categorie(string name)
        {
            this.categorieName = name;
        }
    }
}
