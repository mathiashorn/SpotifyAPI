namespace SpotifyAPI.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public int ArtistId { get; set; }
        public string Genre { get; set; }
        public Artist Artist { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public bool? Deleted { get; set; }
    }


}
