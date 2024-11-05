using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Document;
public class Document
{
    // find a better way to guarantee correct (de)serializsation that doesn't enforce exposing fields that should be private
    required public List<Meeting.Meeting> Meetings { get; set; }
    required public HashSet<User.User> Users { get; set; }

    [SetsRequiredMembers]
    public Document()
    {
        Meetings = [];
        Users = [];

        Console.WriteLine($"[{DateTime.Now}] Started new meeting document");
    }

    public static Document? FromFile(string path)
    {
        using (var reader = File.OpenText(path))
        {
            var document = JsonSerializer.Deserialize<Document>(reader.ReadToEnd());
            Console.WriteLine($"[{DateTime.Now}] Meeting Document loaded");
            return document;
        }
    }
}