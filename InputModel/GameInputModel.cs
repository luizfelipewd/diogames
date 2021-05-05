using System.ComponentModel.DataAnnotations;

namespace diogames.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 3 e 100 caracteres")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da produtora deve conter entre 3 e 100 caracteres")]
        public string Company { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "O preço deve ser no mínimo 1 e no máximo 1000")]
        public double Price { get; set; }
    }
}