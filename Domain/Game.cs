namespace Domain;
public class Game {
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateTime AddedAt { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}