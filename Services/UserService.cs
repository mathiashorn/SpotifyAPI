using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using SpotifyAPI.Models;

namespace SpotifyAPI.Services
{
    public class UserService
    {
        private readonly SpotifyDbContext _context;

        public UserService(SpotifyDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateUser(User user)
        {
            try
            {
                //foreach (var usuariosUsers in user.UsuariosUsers)
                //{
                //    usuariosUsers.Usuario = null;
                //}

                _context.Users.Add(user);

                await _context.SaveChangesAsync(); // Salva as mudanças no banco de dados
                return user.Id; // Retorna o ID do user salvo
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw; // Re-lança a exceção para ser tratada no método chamador
            }
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Entry(user).State = EntityState.Modified;
                user.Deleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditUser(User user)
        {

            //if (user != null && user.UsuariosUsers != null && user.UsuariosUsers.Any())
            //{
            //    foreach (var usuariosUsers in user.UsuariosUsers)
            //    {
            //        usuariosUsers.Usuario = null;
            //    }
            //}

            try
            {
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public async Task<User> GetUser(int id)
        {
            try
            {
                var user = await _context.Users
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

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                var items = await _context.Users
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

        public bool IsValidUser(User user, out string message)
        {
            message = "";

            if (!user.Email.Contains("@"))
            {
                message = "Email inválido";
                return false;
            }

            return true;
        }

    }
}
