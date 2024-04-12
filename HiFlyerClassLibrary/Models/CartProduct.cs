using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFlyerClassLibrary.Models
{
    public class CartProduct
    {
        private int _quantity = 1;
        public int MaxQuantity { get; set; }


        public string VariantId { get; set; }
        public string Title { get; set; }
        public string VariantTitle { get; set; }
        public string Price { get; set; }
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }
        public Uri ImageUrl { get; set; }
    }
}
