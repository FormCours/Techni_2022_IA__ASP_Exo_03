using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Exo_ASP_03.App.Models
{
    public class Game
    {
        public int Id { get; set; }

        [DisplayName("Nom du jeu")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [DisplayFormat(NullDisplayText = "Pas de description! O_o")]
        public string? Description { get; set; }

        [DisplayName("Description")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string? DescriptionShort
        {
            get
            {
                if (Description != null && Description.Length > 50)
                {
                    return Description.Substring(0, 50) + "…";
                }
                return Description;
            }
        }

        [DisplayName("Genre(s)")]
        public List<GameGenre> Genres { get; set; }

        [DisplayName("Année de sortie")]
        public int ReleaseYear { get; set; }

        [DisplayName("Prix")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [DisplayName("PEGI")]
        public string PEGI { get; set; }

        [DisplayName("Couverture du jeu")]
        public string? ImgSrc { get; set; }


    }

    public class GameAdd
    {
        [Required(ErrorMessage = "Le champs est requis !")]
        [DisplayName("Nom du nouveau jeu")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Le champs est requis !")]
        [Range(1900, 3000, ErrorMessage = "Valeur autorisé entre 1900 et 3000.")]
        [DisplayName("Année de sortie")]
        public int? ReleaseYear { get; set; }

        [Required(ErrorMessage = "Le champs est requis !")]
        [Range(0, 1_000_000, ErrorMessage = "Valeur autorisé entre 0 et 1m.")]
        [DisplayName("Prix officiel")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Le champs est requis !")]
        [DisplayName("Limite d'age (PEGI)")]
        public string PEGI { get; set; }

        [Required(ErrorMessage = "Le champs est requis !")]
        [MinLength(1, ErrorMessage = "Au moins un genre ‍🐱‍👓")]
        [DisplayName("Liste des genres")]
        public List<string> Genres { get; set; }
    }
}
