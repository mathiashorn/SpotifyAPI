using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Models;
using SpotifyAPI.Services;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpotifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {

        private AlbumService _albumService;

        public AlbumsController(AlbumService albumService)
        {
            _albumService = albumService;
        }

        // GET: api/<AlbumsController>
        [HttpGet]
        public async Task<ActionResult<List<Album>>> Get(int id)
        {
            return Ok(await _albumService.GetAlbums());
        }

        // GET api/<AlbumsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Album>> GetById(int id)
        {
            return Ok(await _albumService.GetAlbum(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(Album album)
        {
            var message = "";
            if (!_albumService.IsValidAlbum(album, out message))
            {
                return BadRequest(message);
            }

            await _albumService.CreateAlbum(album);
            return Ok();
        }

        // PUT api/<AlbumsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Album album)
        {
            if (id != album.Id)
            {
                return NotFound();
            }

            var message = "";
            if (!_albumService.IsValidAlbum(album, out message))
            {
                return BadRequest(message);
            }

            await _albumService.EditAlbum(album);

            return Ok();

        }

        // DELETE api/<AlbumsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _albumService.DeleteAlbum(id);
            return Ok();
        }
    }
}
