using System.Diagnostics;
using Tui;
using User;
using Meeting;
using System.Text.Json;

namespace cli_meeting_planner;

class Program
{
    static string storageFileName = "meeting_cli.json";
    static string storagePath = Environment.CurrentDirectory;

    static public string DataFilePath = Path.Join(storagePath, storageFileName);
    static void Main(string[] args)
    {
        Document.Document document;
        if (File.Exists(DataFilePath))
        {
            var loaded = Document.Document.FromFile(DataFilePath);
            if (loaded is not null)
                document = loaded;
            else
            {
                throw new Exception("Could not load document from file");
            }
        }
        else
        {
            document = new Document.Document();
        }

        AssertModelsWorking();

        Console.WriteLine("App init");
        var app = new MeetingApp();
        Console.WriteLine("App Run");
        Thread.Sleep(1000);
        try
        {
            app.Run(out var meetings);

            document.Meetings = meetings;
            foreach (var meeting in meetings)
            {
                foreach (var user in meeting.Participants)
                {
                    document.Users.Add(user);
                }
            }
            var jsonString = JsonSerializer.Serialize(document, new JsonSerializerOptions { WriteIndented = true, IndentSize = 4 });

            Console.WriteLine(jsonString);
            // TODO: Don't always overwrite
            using (var fs = File.Open(DataFilePath, FileMode.OpenOrCreate))
            {
                fs.SetLength(0);
                fs.Position = 0;

                using (var stream = new StreamWriter(fs))
                {

                    stream.Write(jsonString);
                }
            }
            Environment.Exit(0);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Environment.Exit(-1);
        }
    }

    private static void AssertModelsWorking()
    {
        var meetingOne = new Meeting.Meeting();
        var meetingTwo = new Meeting.Meeting();

        Debug.Assert(meetingOne.Guid != meetingTwo.Guid);

        Console.WriteLine("Auto init produces unique Guids");
        Console.WriteLine($"M1: ({meetingOne.Guid}) M2: ({meetingTwo.Guid})");

        var userOne = new User.User("name One");
        var userTwo = new User.User("name Two");

        Debug.Assert(userOne.Guid != userTwo.Guid);

        Console.WriteLine("Auto init produces unique Guids");
        Console.WriteLine($"U1: ({userOne.Guid}) U2: ({userTwo.Guid})");
    }
}
