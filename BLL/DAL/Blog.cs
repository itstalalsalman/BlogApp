﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

[Table("Blog")]
public partial class Blog
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(500)]
    [Column(TypeName = "text")]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    [Column(TypeName = "decimal(3, 2)")]
    public decimal? Rating { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime PublishDate { get; set; }

    public int UserId { get; set; }

    [InverseProperty("Blog")]
    public virtual ICollection<BlogTag> BlogTags { get; set; } = new List<BlogTag>();

    [ForeignKey("UserId")]
    [InverseProperty("Blogs")]
    public virtual User User { get; set; }
}