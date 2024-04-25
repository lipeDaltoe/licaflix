using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicaFlix.Models;

[Table("Genre")]
public class Genre

{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Byte Id { get; set; }

    [Display(Name = "Nome")]
    public string Name { get; set; }
}