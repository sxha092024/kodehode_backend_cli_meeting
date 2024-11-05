namespace User;

public interface IUser
{
    public string Name { get; }

    public Guid Guid { get; }
}