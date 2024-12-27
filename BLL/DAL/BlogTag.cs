﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

[Table("BlogTag")]
public partial class BlogTag
{
    public int BlogId { get; set; }

    public int TagId { get; set; }

    [ForeignKey("BlogId")]
    [InverseProperty("BlogTags")]
    public virtual Blog Blog { get; set; }

    [ForeignKey("TagId")]
    [InverseProperty("BlogTags")]
    public virtual Tag Tag { get; set; }
}