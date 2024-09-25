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
    public class ArtistsController : ControllerBase
    {

        private ArtistService _artistService;

        public ArtistsController(ArtistService artistService)
        {
            _artistService = artistService;
        }

        // GET: api/<ArtistsController>
        [HttpGet]
        public async Task<ActionResult<List<Artist>>> Get(int id)
        {
            return Ok(await _artistService.GetArtists());
        }

        // GET api/<ArtistsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetById(int id)
        {
            return Ok(await _artistService.GetArtist(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Artist artist)
        {
            var message = "";
            if (!_artistService.IsValidArtist(artist, out message))
            {
                return BadRequest(message);
            }

            await _artistService.CreateArtist(artist);
            return Ok();
        }

        // PUT api/<ArtistsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Artist artist)
        {
            if (id != artist.Id)
            {
                return NotFound();
            }

            var message = "";
            if (!_artistService.IsValidArtist(artist, out message))
            {
                return BadRequest(message);
            }

            await _artistService.EditArtist(artist);

            return Ok();

        }

        // DELETE api/<ArtistsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _artistService.DeleteArtist(id);
            return Ok();
        }
    }
}
