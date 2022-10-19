using Exo_ASP_03.App.Models;

namespace Exo_ASP_03.App.Data
{
    public static class FakeDB
    {
        private static IList<GameGenre> _Genres = new List<GameGenre>()
        {
            new GameGenre()
            {
                Id = 1,
                Name = "FPS",
                Description = "First-person shooter"
            },
            new GameGenre()
            {

                Id = 2,
                Name = "Action-RPG",
                Description = "Action role-playing"
            },
            new GameGenre()
            {
                Id = 3,
                Name = "Plates-formes",
                Description = null
            },
            new GameGenre()
            {
                Id = 5,
                Name = "Sport",
                Description = null
            }
        };
        private static IList<Game> _Games = new List<Game>()
        {
            new Game()
            {
                Id = 1,
                Name = "Borderlands 2",
                Description = "Borderlands 2 is a 2012 first-person shooter video game developed by Gearbox Software and published by 2K Games. Taking place five years following the events of Borderlands (2009), the game is once again set on the planet of Pandora. The story follows a new group of Vault Hunters who must ally with the Crimson Raiders, a resistance group made up of civilian survivors and guerrilla fighters, to defeat the tyrannical Handsome Jack before he can unlock the power of a new Vault. The game features the ability to explore the in-game world and complete both main missions and optional side quests, either in offline splitscreen, single-player or online cooperative gameplay. Like its predecessor, the game features a procedurally generated loot system which is capable of generating numerous combinations of weapons and other gear.",
                Genres = _Genres.Where(g => g.Name == "FPS" || g.Name == "Action-RPG" ).ToList(),
                Price = 9.99,
                ReleaseYear = 2012,
                PEGI = "18",
                ImgSrc = "/images/Borderlands_2.png"
            },
            new Game()
            {
                Id = 2,
                Name = "Call of Duty: Vanguard",
                Description = null,
                Genres = _Genres.Where(g => g.Name == "FPS").ToList(),
                ReleaseYear = 2021,
                Price = 79.99,
                PEGI = "18",
                ImgSrc = "/images/call-of-duty-vanguard.jpg"
            },
            new Game()
            {
                Id = 3,
                Name = "Super Mario Galaxy",
                Description = "Un jeu avec Mario :o",
                Genres = _Genres.Where(g => g.Name == "Plates-formes").ToList(),
                ReleaseYear = 2007,
                Price = 25.99,
                PEGI = "3+",
                ImgSrc = "/images/mario_galaxy.jpg"
            },
            new Game()
            {
                Id = 4,
                Name = "Pong",
                Description = "Pong est un des premiers jeux vidéo d'arcade et le premier jeu vidéo d'arcade de sport. Il a été imaginé par l'Américain Nolan Bushnell et développé par Allan Alcorn, et la société Atari le commercialise à partir de novembre 1972. Bien que d'autres jeux vidéo aient été inventés précédemment, comme Computer Space, Pong est le premier à devenir populaire.",
                Genres = _Genres.Where(g => g.Name == "Sport").ToList(),
                ReleaseYear = 1972,
                Price = 0,
                PEGI = "0+",
                ImgSrc = null
            }
        };
        private static int _LastGameId = 4;
        private static int _LastGenreId = 5;

        public static IEnumerable<Game> GetGames()
        {
            // Récuperation en lecteur seul de la liste des jeux
            return _Games.Select(game => new Game()
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Genres = game.Genres,
                Price = game.Price,
                ReleaseYear = game.ReleaseYear,
                PEGI = game.PEGI,
                ImgSrc = game.ImgSrc
            });
        }

        public static Game? GetGameById(int id)
        {
            // Tentative de récupation d'un jeu par son Id
            Game? result = _Games.SingleOrDefault(g => g.Id == id);

            if (result == null)
                return null;

            // Renvoi d'un copie du jeu (Nouvelle référence mémoire)
            return new Game()
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Genres = result.Genres,
                Price = result.Price,
                ReleaseYear = result.ReleaseYear,
                PEGI = result.PEGI,
                ImgSrc = result.ImgSrc
            };
        }

        public static GameGenre GetOrInsertGenre(string genreName)
        {
            // Récuperation du genre dans la FakeDB
            GameGenre? genre = _Genres.SingleOrDefault(g => g.Name == genreName);

            // Si le genre n'existe pas. Ajout dans la FakeDB
            if(genre is null)
            {
                // Incrementation de l'id des genre 
                _LastGenreId++;

                // Création et ajout d'un nouveau Genre
                genre = new GameGenre()
                {
                    Id = _LastGenreId,
                    Name = genreName,
                    Description = null
                };
                _Genres.Add(genre);
            }

            // Renvoie du genre
            return genre;
        }

        public static Game? InsertGame(Game game)
        {
            // Erreur si le nom du jeu existe déjà
            if (_Games.FirstOrDefault(g => string.Compare(g.Name, game.Name, true) == 0) != null)
            {
                return null;
            }

            // Incrementation de l'id des jeux
            _LastGameId++;

            // Création et ajout d'un nouveau jeu
            Game newGame = new Game()
            {
                Id = _LastGameId,
                Name = game.Name,
                Description = game.Description,
                Genres = game.Genres,
                Price = game.Price,
                ReleaseYear = game.ReleaseYear,
                PEGI = game.PEGI,
                ImgSrc = game.ImgSrc
            };
            _Games.Add(newGame);

            // Renvoie du jeu
            return newGame;
        }
    }
}
