# CLI Meeting Planner - Kodehode backend task


## Optional
The following diagram (or its [unprocessed Mermaid markup](./README.template.md)) contains an optional step in the main task.

While the MVC pattern is *going* to be used extensively in future tasks, for a CLI application
it complicates development, slows down productivity, and does not guarantee flexibility.
The MVC pattern becomes more reasonable once business logic complexity grows. 
```mermaid
---
title: Meeting planner |  MVC diagram | Work In Progress
---
classDiagram
direction RL
namespace UserMVC {
    class UserModel["UserModel / User"] {
        -Guid GUID
        -List~Guid : Meeting GUID~ ParticipantTo : Upcoming meetings
        -Name Name

        +GUID.get() Guid
        +Name.get() Name
        +Name.set(Name: name) void
    }
    class UserView {
        +RenderAll() string
        +RenderUser() string
        +RenderMeeting(Guid: meetingGuid) string
    }
    class UserController {
        -UserModel Model
        -UserView View

        +LoadFromDatabase(Guid: userGuid) UserModel
        +UpdateInDB(UserModel: user) void

        +DisplayAll() void
        +DisplayUser() void
        +DisplayMeeting(Guid: meetingGuid) void
    }
}

UserModel <-- UserController
UserView <-- UserController

namespace Structs_and_Enums {
    class Name {
        -string[] Components
        +Display() string
    }
    class Meeting {
        +Guid GUID
        -Datetime OccursAt
        -TimeSpan Duration
        -List~Guid : UserModel GUID~ Participants
        -StatusEnum Status
        
        +TimeSpan Until()
        +OccursAt.get() DateTime
        +AllParticipants() List~UserModel~
    }

    class StatusEnum {
        Valid
        Invalid
        Cancelled
        Rescheduled
    }

}

StatusEnum *-- Meeting : Status Field
Name *-- UserModel : name field
Meeting *-- UserModel : ParticipantTo field
```