using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HE170292_SE1814_NET_A01.Models;

public partial class SystemAccount
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short AccountId { get; set; }

    [Required]
    [MaxLength(100)]
    public string? AccountName { get; set; }

    [Required]
    [EmailAddress]
    public string? AccountEmail { get; set; }

    [Required]
    public int? AccountRole { get; set; }

    public string? AccountPassword { get; set; }

    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}
