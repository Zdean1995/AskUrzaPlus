using SQLite;

namespace AskUrzaPlus.Models;

[Table("ability")]
public class Ability
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int AbilityGroupId { get; set; }
    public string AbilityText { get; set; }
    public int AbilityType {  get; set; }
}

