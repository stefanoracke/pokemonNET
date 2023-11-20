using PokemonReviewsApp.Context;
using PokemonReviewsApp.Interfaces;
using PokemonReviewsApp.Models;

namespace PokemonReviewsApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _dataContext;
        public PokemonRepository(DataContext context) 
        {
            this._dataContext = context;
        }

        public Pokemon GetPokemon(int id)
        {
            return _dataContext.Pokemons.Where(p => p.Id == id).FirstOrDefault();    
        }

        public Pokemon GetPokemon(string name)
        {
            return _dataContext.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = _dataContext.Reviews.Where(p => p.Pokemon.Id == pokeId);

            if (review.Count() <= 0)
                return 0;
            return ((decimal)review.Sum(r => r.Rating)/review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _dataContext.Pokemons.OrderBy(p=> p.Id).ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            return _dataContext.Pokemons.Any(p => p.Id == pokeId);
        }
    }
}
