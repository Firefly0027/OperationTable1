using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Models
{
    public class categoryModel
    {
        [Key]
        public int categoryID { get; set; }

        public string categoryName { get; set; }

        public ICollection<itemsTableModel> items { get; set; }
    }
}
