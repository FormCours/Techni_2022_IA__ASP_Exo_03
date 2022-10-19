using Exo_ASP_03.App.Data;
using Exo_ASP_03.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exo_ASP_03.App.Controllers
{
    public class GameController : Controller
    {
        [ViewData]
        public string Title { get; set; }


        public IActionResult Index()
        {
            // Récuperation des données de la « DB »
            IEnumerable<Game> games = FakeDB.GetGames();

            // Modification du titre de la page
            Title = "Liste";  // Equivalent à → ViewData["Title"] = "...";

            // Génération de la vue avec les données necessaires
            return View(games);
        }

        public IActionResult Detail(int id)
        {
            //Récuperation du jeu
            Game? game = FakeDB.GetGameById(id);

            // Envoi d'une erreur 404 si le jeu n'est pas trouvé
            if(game is null)
            {
                return NotFound();
            }

            // Définition du title de la page
            Title = "Detail de " + game.Name;

            // Génération d'une vue pour un jeu
            return View(game);
        }


        public IActionResult Add()
        {
            Title = "Ajouter un jeu";

            return View();
        }

        [HttpPost]
        public IActionResult Add(GameAdd game, IFormFile? image)
        {
            if(!ModelState.IsValid)
            {
                Title = "(╯°□°）╯︵ ┻━┻";

                return View();
            }

            // Sauvegarde du fichier
            string filename = saveImageInRoot(image);

            // Conversion de la liste de genre (string) en GameGenre
            // - v1
            List<GameGenre> genres = new List<GameGenre>();
            foreach(string? genre in game.Genres)
            {
                if(genre != null)
                {
                    GameGenre gg = FakeDB.GetOrInsertGenre(genre);

                    genres.Add(gg);
                }
            }

            // - v2
            List<GameGenre> genres2 = game.Genres
                .Where(g => g != null)
                .Select(g => FakeDB.GetOrInsertGenre(g))
                .ToList();

            // Ajout dans la FakeDB
            FakeDB.InsertGame(new Game()
            {
                Name = game.Name,
                Description = game.Description,
                ReleaseYear = (int)game.ReleaseYear,
                Price = (double)game.Price,
                PEGI = game.PEGI,
                Genres = genres,
                ImgSrc = filename
            });

            return RedirectToAction("Index");
        }

        private string saveImageInRoot(IFormFile image)
        {
            // Définition du répertoire pour sauvegarder les images
            string directory = Path.Combine(Environment.CurrentDirectory, "wwwroot", "images");

            // Définition du nom de fichier 
            string now = DateTime.UtcNow.ToString("yyyyMMdd-HHmmss");
            string rng = Guid.NewGuid().ToString();
            string ext = Path.GetExtension(image.FileName);
            string filename = now + "_" + rng + ext;

            // Création du chemin d'acces au fichier
            string file = Path.Combine(directory, filename);

            // Ouverture d'un flux d'ecriture pour le fichier
            using (FileStream fs = new FileStream(file, FileMode.CreateNew))
            {
                // Sauvegarder l'image via le flux
                image.CopyTo(fs);
            }

            // Renvoi du nom du fichier
            return "/images/" + filename;
        }
    }
}
