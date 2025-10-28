## Prefect Voting App 

Hey there! I'm the developer behind this Prefect Voting Application. Let me walk you through what I built.

**Overview**

This project is my Prefect Voting Application, a web-based system for running school prefect elections. It’s built using ASP.NET Core MVC, Entity Framework Core, and SQL Server.

The goal was to create something practical that schools could use. Students and teachers can participate in elections online, while staff and admins manage everything from the website.

I designed this project not just to learn ASP.NET Identity and MVC architecture, but also to practice thinking through real user roles, permissions, and data relationships. I wanted it to feel like a real-world application, where students can vote, teachers can have weighted votes, and staff/admins can oversee the whole system securely.


**The Problem That Bugged Me**

You know how school elections usually work? Someone sets up a Google Form, students just see a list of names, and everyone votes for their friends? Yeah, that never felt right to me.

The popular kids always win, teachers have to manually count everything, and nobody really learns about the candidates.


**What I Built**

I created a proper voting platform where:

* Students can actually see who they're voting for, with photos, bios, the works
* Teachers' votes are 20 points worth because that's how its weighted in Avondale College
* Everything's automated, the total votes, top 60 students, etc.
* It's transparent, everyone can trust the results


## 
<br><br> 


**Technologies Used**

I chose ASP.NET Core MVC with Entity Framework Core because it provides a structured, maintainable framework and built-in support for authentication and role-based access. The technologies I used include:

* ASP.NET Core MVC (C#) for the application structure
* Entity Framework Core as the ORM to handle database operations
* SQL Server / LocalDB as the backend database
* ASP.NET Identity to manage authentication and roles
* Bootstrap and Razor Views for front-end styling




**Key Features**

The application includes:

* Role-based login for Admin, Staff, Teacher, and Student
* Secure authentication and authorization using ASP.NET Identity
* Voting system with safeguards:

  * Students can vote up to 60 times
  * Teachers’ votes are weighted (20 points each)
  * Duplicate votes are blocked automatically
* Personal "Your Votes" page to view or remove votes
* Election creation and management for Staff and Admins
* Audit log to track actions performed by users and admins
* Responsive layout using Bootstrap so it looks decent on different screen sizes
* Seeded user roles and automatic creation of admin account for initial setup

These features were included not just to make the app functional, but to mimic how a real election system might work in a school — including the idea of weighted votes, audit logging, and limiting user actions to prevent abuse.

<br><br>

## Setting Up the Project

If you want to run this project locally, here’s a step-by-step:

1. **Clone the repository** to your computer
2. **Open the solution file (`.sln`)** in Visual Studio 2022 or later
3. **(Optional step)Configure the database** by editing `appsettings.json`. This is where the application knows where to store and retrieve data.

Example for local setup:

```json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PrefectVotingApp;Trusted_Connection=True;MultipleActiveResultSets=true"
```

Example for remote SQL Server:

```json
"DefaultConnection": "Server=YOUR_SERVER_NAME;Database=PrefectVotingApp;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
```

4. **Apply migrations** to create all necessary tables (AspNetUsers, Votes, Elections, etc.):

```powershell
Update-Database
```

5. **Seed roles and admin account or just run the program once**. The system automatically creates roles for Admin, Staff, Teacher, and Student. An admin account is also created on first run. If it doesn’t appear, you can add one manually in SQL Server or assign roles through the Roles page once logged in as any admin.

This setup ensures that when you run the app, everything works from day one, users, roles, and database tables are already in place. 

6. **Seed data using SQL scripts or dummy data.** Instead of manually entering everything, I set up my application so it can be preloaded with SQL scripts or dummy data for testing. This includes accounts with predefined roles such as Staff, Teacher, and Student, as well as a few sample users for each role.
Here’s where everything is located:

```
Areas/
 └── Identity/
      └── Data/
           ├── DbInitializer.cs
           └── SQL Scripts/
                ├── Users Data-INSERT.sql
                └── Votes Data-INSERT.sql
```
* **Users Data-INSERT.sql**: Inserts all the dummy users (students, teachers, staff, and admin).
  This script **must be run first**, since it creates the users that other tables reference.
* **Votes Data-INSERT.sql**: Inserts sample votes for testing after the user data is in place.

I separated the scripts intentionally so that you can test the system in two different states:

1. **With only users inserted** - lets you test registration, login, and role-based pages without any votes yet.
2. **With both users and votes inserted** - lets you test the voting logic, audit logs, and statistics once the database has relationships established.

The idea is that a real school would already have a database of existing users (students, teachers, and staff with email addresses and details). The SQL scripts simulate that scenario by adding dummy users directly into the AspNetUsers table.



<br><br>

## Using the Website

Unfortunately, my current build automatically assigns all new registered users the Student role by default. Because of that, the system assumes that Teacher, Staff, and Admin accounts are already registered or created by an Admin beforehand.
So, Teachers and Staff don’t go through the registration process, they simply log in using the credentials that the Admin gives them. Admins are responsible for creating those accounts or updating user roles inside the Roles Management section.
Here’s how each role works in the system:



### For Students - Your Voting Experience

Students are the default role and can vote immediately after registering.

Steps:

1. Click “Register” to create a new account
2. Login and open the **Students** or the main voting page
3. Browse the list of candidates (list/grid views)
4. Click **Vote** to cast your vote
5. You can vote up to 60 times across all candidates
6. Visit **Your Votes** to see all the votes you’ve made or remove one

The system will prevent voting twice for the same candidate and will show a warning message if you try to exceed your limit. This teaches students about the limits of their voting power and prevents accidental or malicious over-voting.

---

### For Teachers

Teacher accounts are created by Admins. Their votes are more powerful, each counts as 20 points.

Steps:

1. Login with the assigned Teacher account registered by admin
2. Browse the list of candidates (list/grid views)
3. Go to **Voting Page** and cast votes (20 points each)
4. View **All Votes** page to see all the votes
5. Go to **Your Votes** to see all the votes you've made or remove one
   

Teachers cannot edit elections or manage users. Their access is about participation and supervising the election process, rather than administration.

---

### For Staff

Staff accounts are also created by Admins. Their main responsibility is **managing elections**.

Steps:

1. Login with the Staff account
2. Browse the list of candidates (list/grid views)
3. Go to the **Elections** page
4. Create, edit, or end elections
5. Fill in details like title, start date, and end date
6. View **All Votes** page to see all the votes
7. View **Top 60** page to see the current top 60 students with the most votes

Staff cannot access roles, audit logs, or votes. They focus solely on running elections smoothly.

---

### For Admins

Admins have full control over the system.

Steps:

1. Login with the Admin account
2. Access additional links & CRUDs: Roles, Audit Logs, Users, and Elections
3. Admin tasks include:

   * Assigning or changing user roles
   * Viewing audit logs to track user activity
   * Creating, editing, or deleting elections
   * Registering accounts, Managing users (edit details or remove accounts)
   * Everything all the roles can, with no weight on votes

Admins are also responsible for creating Teacher and Staff accounts since registrations default to Student. This gives full oversight and control over the application, similar to how a school IT administrator would manage a real voting system.

---

## Role Permissions Summary

| Role    | Permissions                                                       |
| ------- | ----------------------------------------------------------------- |
| Admin   | Full control, manage users and roles, view logs, manage elections |
| Staff   | Create, edit, delete elections                                    |
| Teacher | Vote (worth 20 points), view results                              |
| Student | Vote up to 60 times, view/remove personal votes                   |

This table is a quick reference for understanding what each user can and cannot do. It's especially useful for developers exploring the app.

---

## Known Limitations

* All new accounts default to Student
* Admins must manually promote users to Teacher or Staff
* Remote SQL connections may require additional permissions
* System is primarily designed for desktop; mobile support is partial
* For testing, a local database is recommended

---

## Developer Reflection

Working on this project taught me a lot about **real-world ASP.NET development**.

Some key lessons:

* Integrating **ASP.NET Identity** with a custom model can create cascade issues, foreign key mismatches, and connection problems. Debugging these issues taught me how EF Core works behind the scenes.
* **Naming consistency** and proper **partial views** (_LoginPartial.cshtml) are essential for keeping a large project organized.
* Connecting **backend logic** with frontend views made me realize it’s not just about making a page look good — it’s about making sure it actually works under different user roles and permissions.
* Designing the voting system helped me think about **user experience, fairness, and safeguards**, like limiting votes and weighting teacher votes.

This project bridged the gap between academic exercises and building something that could be used in a real school setting.

---

## License

This project was built as part of my NCEA/TPI coursework.
Free to use or build upon for learning purposes.


