# Gym Booking Manager
## System Requirement Specification

A gym wants help with the creation of a booking management system for their business. Today, the gym offers various activities, including:

- Group fitness session for plural participants, directed by a coach from the staff.
- Individual workout session, reserving some equipment/space in advance.
- Individual workout session with a supervising personal trainer.
- Consultation with a personal trainer.

 However, operation and audit is a messy affair with current protocols. They want a new system to reduce overhead in daily operation, which should speed up business and simplify the management system training for new staff.
***
### 1. Users
- (?) Admin
- Staff
- Customers
    - Paying
        - Members
        - Day pass (non-members)
    - Non-paying
        - Non-members
- Service/Repairs
***
### 2. User Stories
Staff:
> <a id="us-5"></a>*We need to be able to register new stuff in the system, so that the team and customers can include it in their routines. Also, restricting things that is not fit for use.*

> <a id="us-1"></a>*I would like to be able to use template scheduling to schedule many weekly sessions with one entry.*

> <a id="us-2"></a>*I would like to be able to preview my scheduling changes before committing them to the system. This would be especially useful when I'm making large sweeping updates.*

Customers:
> <a id="us-6"></a> *I have fairly limited time and a very specific exercise routine that requires reserving specific equipment. The staff has kindly tried to accommodate, but the existing system can barely be called a system. I would very much like the ability to make reservations in a digital system.*

> <a id="us-3"></a>*It'd be great if I could be told in advance when a group session has been cancelled. Maybe you could use the phone number or e-mail you've collected at registration for something other than advertising your service to already paying customers.*

Admin:
> <a id="us-4"></a>*I am relieved about the prospects of reduced overhead, but also a bit worried about lacking oversight with an automated system that does not require as much micro-level meddling. Maybe we could log activity?*
***
### 3. Functional Requirements

- The system can display and manage availability of items (equipment, space, trainers).

Group Schedule
- The system manages an open group schedule for group fitness activities.
- A group activity in the group schedule may reserve equipment and space, and **must** allocate at least one coach.
- Staff can make entries in the group schedule, so called group activities.
- Staff can change existing group activities.
    - When this affects customers (e.g. cancellation), send [impacted users](#us-3) a text message notification on their registered phone number (**NOTE:** Creating a local file log entry is sufficient for the prototype).
- Booking for group fitness is thus divided in two steps:
    - Staff creates the group activity.
    - Once the entry is in the schedule, paying customers can sign up, reserving their spot.

Reservations
- ~~Customer reservations can be made both at the reception (in-person and via phone) or online.~~ **_Note that at this stage we're making the core of the management system. A prototype to run in the terminal with a simple command-line interface, CLI._**
- Staff can make reservations on behalf of customers.
- Staff can change existing reservations.
    - When this affects customers (e.g. cancellation), send [impacted users](#us-3) a text message notification on their registered phone number (**NOTE:** Creating a local file log entry is sufficient for the prototype).
- Non-members can purchase a day pass, granting them temporary elevated privileges as those enjoyed by paying customers (group fitness, gym access, equipment reservation).
- Paying customers can reserve a spot for **scheduled group fitness sessions**.
- Paying customers can reserve **large equipment** in advance (machines, gym, some stationary items).
- All customers can reserve **sports equipment** (smaller items such as rackets or floorball sticks) or **space/halls/lanes** for a **fee** corresponding to the reserved items.
- All customers can book a **consultation** with a **personal trainer** for a **fee**.
- All customers can book a **supervised training session** with a **personal trainer** for a **fee**.

Service
- Broken equipment can be registered by staff for **service** or **planned-purchase**, restricting its availability.
- Space can be registered by staff as **unavailable**, restricting its availability.

Optional
- Functionality that may satisfy additional [user stories](#2-user-stories) is considered a plus at this stage, but not a requirement.

### 4. Non-functional requirements (quality requirements)
- A first stage, **single-user**, **console-application** **prototype** that handles the business logic of the [functional requirements](#3-functional-requirements).
- Persistence of data between runs.






