using Realms;

public class HitCountEntity : RealmObject
{
    [PrimaryKey]
    public int PrimaryKey { get; set; }
    public int Value { get; set; }

    public HitCountEntity() { }

    public HitCountEntity(int primaryKey)
    {
        PrimaryKey = primaryKey;
    }
}