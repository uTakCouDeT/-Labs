using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace lab12New;

public partial class Company
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string Country { get; set; } = null!;

    [StringLength(50)]
    public string WayOfDelevery { get; set; } = null!;

    public double CostOfDelevery { get; set; }

    [InverseProperty("IdCompanyNavigation")]
    public virtual ICollection<Good> Goods { get; } = new List<Good>();
}
