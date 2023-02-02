using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Models;

public class OrderTableModel
{
    [Key]

    public int Id { get; set; }

    public string OperationType { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }

    public string Address { get; set; }

    public string Custname { get; set; }

    public int NetTotal { get; set; }

    public int GrossTotal { get; set; }

    public int DiscountTotal { get; set; }

    public int TaxTotal { get; set; }

    public int QuantityTotal { get; set; }

    public int UserID { get; set; }

    public ICollection<OrderDetialsModel> orderDetials { get; set; }
}
