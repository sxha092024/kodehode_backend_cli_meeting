using Tui;

public class MeetingApp
{
    private TUIDriver Tui = new();

    private bool finished = false;

    enum Phase
    {
        AppStarted,
        SelectOption,
        NewMeeting, EditMeeting,
        MeetingSelectDate,
        MeetingAddParticipants,
        ParticipantName,
        MeetingCreated,
        AppExit,
    }

    private Meeting.Meeting? CurrentMeeting { get; set; } // TODO: Wrap with Option<T>?
    private List<Meeting.Meeting> Meetings { get; set; } = [];

    private User.User? CurrentUser { get; set; } // TODO: Wrap with Option<T>?
    private List<User.User> Participants { get; set; } = [];

    private Phase CurrentPhase { get; set; } = Phase.AppStarted;
    public void Run(out List<Meeting.Meeting> meetings)
    {
        // TODO: load previous items from file
        while (!finished)
        {
            switch (CurrentPhase)
            {
                case Phase.AppStarted:
                    PresentApp();
                    break;
                case Phase.SelectOption:
                    SelectOption();
                    break;
                case Phase.NewMeeting:
                    NewMeeting();
                    break;
                case Phase.EditMeeting:
                    EditMeeting();
                    break;
                case Phase.MeetingSelectDate:
                    SelectDate();
                    break;
                case Phase.MeetingAddParticipants:
                    AddParticipant();
                    break;
                case Phase.ParticipantName:
                    ParticipantName();
                    break;
                case Phase.MeetingCreated:
                    MeetingCreated();
                    break;
                case Phase.AppExit:
                    Exit();
                    break;
            }
        }
        meetings = Meetings;
    }

    private void PresentApp()
    {
        CurrentPhase = Phase.SelectOption;

        Tui.Clear();
        Tui.Header("Meeting Planner");
        Tui.Blank();
        Tui.Present("Welcome!");
    }

    private void SelectOption()
    {
        var edit = false; // TODO: tui request option, allow editing
        if (!edit)
        {
            CurrentPhase = Phase.NewMeeting;
        }
        else
        {
            // TODO: allow editing
            Tui.Present("Editing is not implemented yet");
            // CurrentPhase = Phase.EditMeeting;
            throw new NotImplementedException();
        }
    }

    private void NewMeeting()
    {
        Tui.Blank();
        Tui.Present("Creating new meeting");
        CurrentPhase = Phase.MeetingSelectDate;
    }

    // TODO: implement editing
    private void EditMeeting()
    {
        throw new NotImplementedException();
    }

    private void SelectDate()
    {
        if (Tui.RequestDateTime(out var date))
        {
            CurrentMeeting = new();
            try
            {
                CurrentMeeting.SetDate(date);
                CurrentPhase = Phase.MeetingAddParticipants;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        else
        {
            Tui.Present("Sorry, could not understand the entered date");
            Tui.Present("Is it in the past?");
        }
    }

    private void AddParticipant()
    {
        if (Participants.Count == 0)
        {
            // TODO: Different flow?
            CurrentPhase = Phase.ParticipantName;
        }
        else
        {
            Tui.Present("Current participants");
            Tui.Present(Participants);

            // TODO: tui request
            Tui.Present("Add more participants?");
            Tui.RequestBool(out var addMore);
            if (addMore)
            {
                CurrentPhase = Phase.ParticipantName;
            }
            else
            {
                {
                    CurrentPhase = Phase.MeetingCreated;

                    // Control flow should guarantee that CurrentMeeting is non-null
                    Participants.ForEach((user) => { CurrentMeeting.AddParticipant(user); });
                    Tui.Present("Participants added");
                    Participants = new();
                }
            }
        }
    }

    private void ParticipantName()
    {
        Tui.Present("Participant's Name");
        if (Tui.RequestString(out var name))
        {
            CurrentUser = new(name);
            Participants.Add(CurrentUser);
            CurrentUser = null;
            CurrentPhase = Phase.MeetingAddParticipants;
        }
        else
        {
            // TODO: better feedback? Remove branch entirely?
            Tui.Present("????????????? how even");
        }
    }

    private void MeetingCreated()
    {
        // TODO: Present meeting details. Write file. Exit flow
        // TODO: fix unix clear issue
        Meetings.Add(CurrentMeeting);
        Tui.Blank();
        Tui.Clear();
        Tui.Present($"Meeting created: {CurrentMeeting}");
        Tui.Present("Participants:");
        Tui.Present(CurrentMeeting.Participants);
        Tui.Present("Done?");
        Tui.RequestBool(out var finishedCond);
        CurrentPhase = Phase.SelectOption;
        if (finishedCond)
        {
            finished = true;
            CurrentPhase = Phase.AppExit;
        }
        // Clean slate for the next loop through
        CurrentMeeting = null;
        CurrentUser = null;
        Participants = [];

    }

    private void Exit()
    {
        // TODO: any neccessary cleanup
    }
}