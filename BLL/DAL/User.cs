﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

[Table("User")]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string UserName { get; set; }

    [Required]
    [StringLength(255)]
    public string Password { get; set; }

    public bool IsActive { get; set; }

    public int RoleId { get; set; }

    [InverseProperty("User")]
    public ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; }
}