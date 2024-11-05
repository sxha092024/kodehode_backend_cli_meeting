using User;

namespace Meeting;


// TODO: Document IMeeting interface
public interface IMeeting
{
    // TODO: See if an interface can somehow enforce auto creation init
    Guid Guid { get; }
    HashSet<User.User> Participants { get; }
    DateTime OccursAt { get; }
    // TODO: Until(), Duration { get; }
}