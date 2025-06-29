namespace VideoGameCatalog.API.DTOs;

public class VideoGameDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
}