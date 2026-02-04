namespace Domain;

public class GameListEntity
{
     public Guid Id {get; set;}
     public string UserId{get; set;} = string.Empty;
     public int IgdbGameId{get; set;} 
     public string Title{get; set;} = string.Empty;
     public string? CoverUrl{get; set;}

     public GameStatus Status {get; set;}
     public int? Score {get; set;}

     public DateTime AddedAt{get; set;} = DateTime.Now;
     public string? Genre{get; set;}
// 
// Конструктор для создание уникального Id при регистрации!
// 
     public GameListEntity()
     {
          Id = Guid.NewGuid();
          AddedAt = DateTime.UtcNow;
     }
}
