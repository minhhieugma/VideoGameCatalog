namespace VideoGameCatalog.API.DTOs;

public class CreateVideoGameDto
{
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
}