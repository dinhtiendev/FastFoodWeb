using System;
using System.Collections.Generic;

#nullable disable

namespace FastFoodWeb.Models
{
    public partial class Wish
    {
        public int Id { get; set; }
        public int? FoodId { get; set; }
        public int? AccountId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Food Food { get; set; }
    }
}
