using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Client_MilkAndMeat.UserControls
{
  public class Product
  {
        public uint ID { get; set; }
        public uint ID_manufacture { get; set; }
        public string Name_manufacture { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sort { get; set; }
        public string Groupe { get; set; }
        public double Price { get; set; }

        public ImageSource image { get; set; }

        public Product()
        {
            this.ID = 0;
            this.ID_manufacture = 0;
            this.Name_manufacture = "";
            this.Name = "";
            this.Description = "";
            this.Sort = "";
            this.Groupe = "";
            this.Price = 0;
            this.image = null;
        }
        public Product(uint ID = 0, uint ID_manufacture=0, string Name_manufacture="", string Name="", string Description="", string Sort="", string Groupe="", double Price=0, ImageSource image=null)
        {
            this.ID = ID;
            this.ID_manufacture = ID_manufacture;
            this.Name_manufacture = Name_manufacture;
            this.Name = Name;
            this.Description = Description;
            this.Sort = Sort;
            this.Groupe = Groupe;
            this.Price = Price;
            this.image = image;
        }
        public Product(Product product)
        {
            this.ID = product.ID;
            this.ID_manufacture = product.ID_manufacture;
            this.Name_manufacture = product.Name_manufacture;
            this.Name = product.Name;
            this.Description = product.Description;
            this.Sort = product.Sort;
            this.Groupe = product.Groupe;
            this.image = product.image;
            this.Price = product.Price;
        }
    }
}
