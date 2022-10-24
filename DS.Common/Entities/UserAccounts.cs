
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

using DS.Common.Models;

namespace DS.Common.Entities;

[Table("UserAccounts")]
public class UserAccount : EntityBase
{
    public UserAccount()
    {
        this.ShortName = "";
        this.FullName = "";
        this.Email = "";
        this.PasswordHash = "";
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserID { get; set; }

    public string ShortName { get; set; }

    public string FullName { get; set; }

    public short Classification { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public bool Privacy { get; set; }

    public DateTime StartAt { get; set; }

    public DateTime EndAt { get; set; }

    public bool IsActive { get; set; }

    public override void ModelBuilder(ModelBuilder modelBuilder)
    {
        Console.WriteLine("UserAccount");
    }

}