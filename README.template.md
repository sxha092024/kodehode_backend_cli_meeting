# CLI Meeting Planner - Kodehode backend task

## Datamodell
```mermaid
---
title: Data models
---
classDiagram
direction RL
namespace Classes {
    class Document {
        +List~Meeting~ Meetings
        +HashSet~User~ Users

        +Document() Document
        // TODO: FromFile() Document
    }
    class User {
        -Guid Guid // auto initialized
        -string Name

        +User(string: name) User
        +Guid.get() Guid
        -AllowedName() bool
        // TODO: +ParticipantTo() IEnumerable~Meeting~
        // TODO: Deserialize +User(Guid: guid, string: name) User
        // TODO: Deserialize +UpdateName()
    }

    class Meeting {
        -Guid Guid // auto initialized
        -DateTime OccursAt
        -HashSet~User~ Participants
        // TODO: -TimeSpan Duration

        +Meeting() Meeting
        +Guid.get() Guid
        +Participants.get() HashSet~User~
        +OccursAt.get() DateTime
        // TODO: +Until() TimeSpan
        // TODO: +TimeSpan.get()
    }
}
```

## Optional - WIP
The following diagram (or its [unprocessed Mermaid markup](./README.template.md)) contains an optional step in the main task.

**N.B** While this diagram may exist it does not guarantee that the underlying implementation adheres to it.
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

        +LoadUserFromStorage(Guid: userGuid) UserModel

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
        -Guid guid
        -Datetime OccursAt
        -TimeSpan Duration
        -List~Guid : UserModel GUID~ Participants
        -StatusEnum Status
        
        -Guid.init();
        +Guid Guid.get()
        +TimeSpan Until()
        +OccursAt.get() DateTime
        +AllParticipants() List~UserModel~
    }

    class StatusEnum {
        Planned
        Confirmed
        Concluded
        Rescheduled
        Cancelled
    }

}

StatusEnum *-- Meeting : Status Field
Name *-- UserModel : name field
Meeting *-- UserModel : ParticipantTo field
```
### Notes
While Github is able to render embedded mermaid diagrams, a template is still used for non github hosted repositories