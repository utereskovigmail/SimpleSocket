using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleServer;

[Table("messages")]
public class Message
{
    [Key]
    public int id { get; set; }
    
    [Required]
    public string content { get; set; }
}