using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Models;

public class usersModel
{
    [Key]
    public int Id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string UserName { get; set; }

    public ICollection<OrderTableModel> orders { get; set; }
}
