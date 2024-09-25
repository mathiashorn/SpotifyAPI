namespace SpotifyAPI.Models
{
    public class Album
    {
        public Album()
        {
            Songs = new HashSet<Song>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ArtistId { get; set; }
        public bool? Deleted { get; set; }

        public virtual Artist? Artist { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }


}
