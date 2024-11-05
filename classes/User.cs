using cli_meeting_planner;
namespace User;

public class User
{
    public string Name { get; private set; }

    public Guid Guid { get; private set; } = Guid.NewGuid();

    public User(string name)
    {
        if (AllowedName(name))
        {
            Name = name;
        }
        else
        {
            throw new ArgumentException($"`{name}` is not a valid name");
        }
    }

    public User(Guid guid, string name)
    {
        Guid = guid;
        if (AllowedName(name))
        {
            Name = name;
        }
        else
        {
            throw new ArgumentException($"`{name}` is not a valid name");
        }
    }

    public void UpdateName(string name)
    {
        throw new NotImplementedException();
        using (var reader = File.OpenText(Program.DataFilePath))
        {
        }
    }

    private bool AllowedName(string name)
    {
        return !string.IsNullOrEmpty(name);
    }

    public override string ToString()
    {
        return $"{Name}";
    }

}