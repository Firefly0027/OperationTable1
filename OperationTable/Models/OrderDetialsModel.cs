using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Models
{
    public class OrderDetialsModel
    {
        [Key]
        public int ID { get; set; }

        public int OrderID { get; set; }

        public int ItemID { get; set; }

        public decimal Price { get; set; }

        public int? Quantity { get; set; }

        public decimal? GrossTotal { get; set; }

        public decimal? discount { get; set; }

        public decimal? tax { get; set; }

        public decimal? Total { get; set; }

        public string company { get; set; }
    }
}
