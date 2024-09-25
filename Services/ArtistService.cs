using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using SpotifyAPI.Models;

namespace SpotifyAPI.Services
{
    public class ArtistService
    {
        private readonly SpotifyDbContext _context;

        public ArtistService(SpotifyDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateArtist(Artist user)
        {
            try
            {
                //foreach (var usuariosArtists in user.UsuariosArtists)
                //{
                //    usuariosArtists.Usuario = null;
                //}

                _context.Artists.Add(user);

                await _context.SaveChangesAsync(); // Salva as mudanças no banco de dados
                return user.Id; // Retorna o ID do user salvo
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw; // Re-lança a exceção para ser tratada no método chamador
            }
        }

        public async Task DeleteArtist(int id)
        {
            var user = await _context.Artists.FindAsync(id);
            if (user != null)
            {
                _context.Entry(user).State = EntityState.Modified;
                user.Deleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditArtist(Artist user)
        {

            //if (user != null && user.UsuariosArtists != null && user.UsuariosArtists.Any())
            //{
            //    foreach (var usuariosArtists in user.UsuariosArtists)
            //    {
            //        usuariosArtists.Usuario = null;
            //    }
            //}

            try
            {
                    _context.Artists.Update(user);
                    await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public async Task<Artist> GetArtist(int id)
        {
            try
            {
                var user = await _context.Artists
                    .Where(ana => ana.Id == id &&
                        ana.Deleted != true)
                    .FirstOrDefaultAsync();
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Artist>> GetArtists()
        {
            try
            {
                var items = await _context.Artists
                 .Where(u =>
                    u.Deleted != true
                  )
                 .AsNoTracking()
                 .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool IsValidArtist(Artist user, out string message)
        {
            message = "";

            //if (!user.Email.Contains("@"))
            //{
            //    message = "Email inválido";
            //    return false;
            //}

            return true;
        }

    }
}
