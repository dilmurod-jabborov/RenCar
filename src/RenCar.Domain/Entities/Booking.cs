using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenCar.Domain.Entities;

public class Booking : AudiTable
{
    public int UserId { get; set; }
    public int CarId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    public User User { get; set; }
    public Car Car { get; set; }
    public BookDetails BookDetails { get; set; }
    public Payment Payment { get; set; }
}



