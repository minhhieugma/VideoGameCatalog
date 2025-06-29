namespace VideoGameCatalog.API.DTOs;

public class UpdateVideoGameDto
{
    public int Id { get; set; }  // Required for PUT update
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
}