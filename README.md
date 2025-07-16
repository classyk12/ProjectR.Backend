
**Project Information:**

This is a simple aapplication that helps businesses with managing service appointments. This backend will be powering another simple mobile application.

**Language:** C# and .NET

**Architecture:** Monolith with Clean Architecture

**Database:** Postgres and MongoDb

**Events Handling:** SignalR

**Logging:** Serilog to File

**Testing:** XUnit and moq 

**Containarization:** Docker

---------------------------------------------------------

**Dev Rules:**

When a Dev is assigned a task, do the following:
1. Create your branch locally using this format depending on if you are fixing, adding a new feature or doing a chore such as code cleanup. We will use the <features> tag to represent a new feature, <fix> tag to represent if you are making a fix and <chore> to if you are carrying out a routine cleanup.
This will follow the format of: <actionType>-<yourinitials>-<AssignedTaskTitle>. For example, If I was given a task to create CRUD operation for AppTheme, my branch name would be:
**features-FS-AppThemeCRUD**
2. Pull from the **develop** branch which is usually where all recent approved changes get merged into.
3. Make your changes and commit them to your local branch, after publishing the local branch to the remote one.
4. When you are satisfied with your changes, Ensure to create a pull request here other developers can then review, add commentd and approve as seen fit.

**NB: Unfortunately, Devs that dont follow these rules wont have their work accepted (This is just to enforce uniformity and establish a pattern of work)**
