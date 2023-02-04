using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Models;

public class itemsTableModel
{
    [Key]
    public int ItemID { get; set; }

    public string company { get; set; }

    public string availability { get; set; }

    public decimal price { get; set; }

    public decimal discount { get; set; }

    public decimal tax { get; set; }

    public string description { get; set; }

    public int categoryid { get; set; }

    public string? categoryName { get; set; }

    public categoryModel? category { get; set; }
}
