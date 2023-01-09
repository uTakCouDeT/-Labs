using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace lab12New;

public partial class Recipient
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Country { get; set; } = null!;

    [StringLength(50)]
    public string Address { get; set; } = null!;

    [StringLength(50)]
    public string FullName { get; set; } = null!;

    [InverseProperty("IdRecipientNavigation")]
    public virtual ICollection<Good> Goods { get; } = new List<Good>();
}
