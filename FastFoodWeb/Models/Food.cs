using System;
using System.Collections.Generic;

#nullable disable

namespace FastFoodWeb.Models
{
    public partial class Food
    {
        public Food()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
            Wishes = new HashSet<Wish>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string FoodDescription { get; set; }
        public bool? IsActive { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Wish> Wishes { get; set; }
    }
}
