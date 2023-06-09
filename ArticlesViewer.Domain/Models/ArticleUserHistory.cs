﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArticlesViewer.Domain;

public class ArticleUserHistory
{
    [Key]
    public Guid ArticleId { get; set; }
    [ForeignKey(nameof(ArticleId))]
    public Article Article { get; set; } = null!;

    [Key]
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    public DateTime DateTime { get; set; } = DateTime.Now;
}