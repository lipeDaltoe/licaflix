using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace LicaFlix.Models;

[Table("AppUser")]
public class AppUser
{
    [Key]
    public string AppUserId { get; set; }        
    [ForeignKey("AppUserId")]
    public IdentityUser UserAccount { get; set; }

    [Required]
    [StringLength(60)]
    public string Name { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime Birthday { get; set; }
    
    [StringLength(300)]
    public string Photo { get; set; }
}