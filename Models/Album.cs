namespace SpotifyAPI.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public bool? Deleted { get; set; }

        public ICollection<Song> Songs { get; set; }
    }


}
