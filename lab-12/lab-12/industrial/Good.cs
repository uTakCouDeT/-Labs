using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace lab12New;

public partial class Good
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int IdCompany { get; set; }

    public int Count { get; set; }

    public double Cost { get; set; }

    public int IdRecipient { get; set; }

    [ForeignKey("IdCompany")]
    [InverseProperty("Goods")]
    public virtual Company IdCompanyNavigation { get; set; } = null!;

    [ForeignKey("IdRecipient")]
    [InverseProperty("Goods")]
    public virtual Recipient IdRecipientNavigation { get; set; } = null!;
}
