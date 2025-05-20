using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeitorNotasFiscais.Models;

public class Produto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;
    public decimal Valor { get; set; }

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;
}
