using System.Diagnostics;
using System.Text;
using User;

namespace Meeting;

public class Meeting : IMeeting
{
    // As far as I've understood C#, this ensures a wellformed construction 
    // has an auto-init Guid set to the private guid field with 
    // the Guid.get() accessor
    public Guid Guid { get; private init; } = Guid.NewGuid();

    public DateTime OccursAt { get; private set; }

    public HashSet<User.User> Participants { get; private set; } = [];

    public bool AddParticipant(User.User user)
    {
        return Participants.Add(user);
    }

    public TimeSpan Until()
    {
        throw new NotImplementedException();
    }

    public void SetDate(DateTime time)
    {
        if (time < DateTime.Now)
        {
            throw new ArgumentException("Time is in the past");
        }
        OccursAt = time;
    }

    public Meeting()
    {
    }

    public override string ToString()
    {
        var str = new StringBuilder();
        str.Append($"Meeting: {OccursAt} ({Guid})");
        return str.ToString();
    }
}