using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using SpotifyAPI.Models;

namespace SpotifyAPI.Services
{
    public class AlbumService
    {
        private readonly SpotifyDbContext _context;

        public AlbumService(SpotifyDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAlbum(Album album)
        {
            try
            {
                //foreach (var usuariosAlbums in album.UsuariosAlbums)
                //{
                //    usuariosAlbums.Usuario = null;
                //}

                _context.Albums.Add(album);

                await _context.SaveChangesAsync(); // Salva as mudanças no banco de dados
                return album.Id; // Retorna o ID do album salvo
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw; // Re-lança a exceção para ser tratada no método chamador
            }
        }

        public async Task DeleteAlbum(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Entry(album).State = EntityState.Modified;
                album.Deleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditAlbum(Album album)
        {

            //if (album != null && album.UsuariosAlbums != null && album.UsuariosAlbums.Any())
            //{
            //    foreach (var usuariosAlbums in album.UsuariosAlbums)
            //    {
            //        usuariosAlbums.Usuario = null;
            //    }
            //}

            try
            {
                    _context.Albums.Update(album);
                    await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public async Task<Album> GetAlbum(int id)
        {
            try
            {
                var album = await _context.Albums
                    .Where(ana => ana.Id == id &&
                        ana.Deleted != true)
                    .FirstOrDefaultAsync();
                return album;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Album>> GetAlbums()
        {
            try
            {
                var items = await _context.Albums
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

        public bool IsValidAlbum(Album album, out string message)
        {
            message = "";

            //if (!album.Email.Contains("@"))
            //{
            //    message = "Email inválido";
            //    return false;
            //}

            return true;
        }

    }
}
