﻿#nullable disable

using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class UserModel
    {
        public User Record { get; set; }
        
        [DisplayName("User ID")]
        public int Id => Record.Id;


        [DisplayName("UserName")]
        public string UserName => Record.UserName;

        [DisplayName("Password")]
        public string Password => Record.Password;
        
        [DisplayName("Is Active")]
        public bool IsActive => Record.IsActive;

        [DisplayName("Role ID")]
        public int? RoleId => Record.RoleId;

        [DisplayName("Role")]
        public string RoleName { get; set; }

        [DisplayName("Blogs")]
        public ICollection<BlogModel> Blogs
        {
            get
            {
                var blogModals = new List<BlogModel>();
                if (Record.Blogs != null)
                {
                    foreach (var blog in Record.Blogs)
                    {
                        blogModals.Add(new BlogModel { Record = blog });
                    }
                }
                return blogModals;
            }
        }

    }
}