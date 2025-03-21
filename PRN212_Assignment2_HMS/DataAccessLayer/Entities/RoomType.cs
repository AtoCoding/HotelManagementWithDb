using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public partial class RoomType
{
    public int RoomTypeId { get; set; }

    [Required(ErrorMessage = "Type's name is required.")]
    [StringLength(50, ErrorMessage = "Room Type Name cannot exceed 50 characters.")]
    public string RoomTypeName { get; set; } = null!;

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(500, ErrorMessage = "Type Description cannot exceed 500 characters.")]
    public string? TypeDescription { get; set; }

    [StringLength(200, ErrorMessage = "Type Note cannot exceed 200 characters.")]
    public string? TypeNote { get; set; }

    public virtual ICollection<RoomInformation> RoomInformations { get; set; } = new List<RoomInformation>();
}
