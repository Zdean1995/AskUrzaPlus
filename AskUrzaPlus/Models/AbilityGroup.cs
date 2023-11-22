using SQLite;

namespace AskUrzaPlus.Models;

[Table("abiltyGroup")]
public class AbilityGroup
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [Unique]
    public string Name { get; set; }
}
