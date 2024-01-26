# SM Tasks

Owner: Said Mounji

## Github Repository

[https://github.com/TheSkyAboveTheSky/csharp-microservice-Said-Mounji-ZZ2-F2](https://github.com/TheSkyAboveTheSky/csharp-microservice-Said-Mounji-ZZ2-F2)

## Project Overview

![Untitled](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/Untitled.png)

![Untitled](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/Untitled%201.png)

## User Model

```csharp
- Id sous forme user-******
- Nom
- Prenom
- Email
- Password
- Username
- Gender = ["Male","Female"]
- Role = ["Admin","User","ProjectManager"]
- GroupId = [0,1,2,3,4] // 0 => No group
```

## Task Model

```csharp
- Id sous forme task-******
- Titre
- Description
- UserId sous forme user-******
- IsChecked
- Username
```

## Project Model

```csharp
- Id sous forme project-******
- Nom
- Description
- GroupId =[0,1,2,3,4] // 0 => Not Affected
- Status = ["UpComing","OnGoing","Completed"]
```

## SignIn and SignUp Forms

![signin page.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/signin_page.png)

![signup page.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/signup_page.png)

![alerts.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/alerts.png)

## Navbar

### Responsive

![responsive navbar.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/responsive_navbar.png)

### Admin

![navbar admin.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/navbar_admin.png)

### ProjectManager

![navbar projectmanager.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/navbar_projectmanager.png)

### User “Normale”

![navbar User.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/navbar_User.png)

# Pages

## My Tasks

```
- Only Authentificated User Can Access this page
- List only the tasks linked to the user ( by his id) ( Task.userId)
- Access = ["User","ProjectManager","Admin"]
```

![my tasks.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/my_tasks.png)

![Creation de nv task.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/Creation_de_nv_task.png)

![if completed strike through , change state.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/if_completed_strike_through__change_state.png)

![update task.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/update_task.png)

```
- Create New Task => Create A New Task ( to be linked with the user by his UserId from the localStorage )
- Delete All Tasks => Delete All The Tasks linked to the User
- Delete => Delete the Task by it's id
- Edit => Edit The Task (Can't Edit the UserId)
- Change State => (Completed => In Progress , In Progress => Completed ) if Completed strike Through
```

## All Tasks

```
- Only Authentificated User Can Access this page
- List All Tasks
- Access = ["Admin"]
```

```
- Create New Task => Create A New Task ( to be linked with the user by his UserId from the localStorage )
- Delete All Tasks => Delete All The Tasks
- Delete => Delete the Task by it's id
- Edit => Edit The Task
- Change State => (Completed => In Progress , In Progress => Completed ) if Completed strike Through
```

![all tasks.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/all_tasks.png)

## My Projects

```
- Only Authentificated User Can Access this page
- List The Projects linked to the User ( User.GroupId == Project.GroupId)
- Access = ["Admin","User","ProjectManager"]
```

```
- Create New Project => Create A New Project (will be linked with the user by the group)
- Delete All Projcts => Delete All The Projects linked to the user
- Delete => Delete the Task by the id
- Edit => Edit The Project ( Can't Edit the Group)
- Change State => ["UpComing","OnGoing","Completed"]
```

![my projects.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/my_projects.png)

![new project in my projects.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/new_project_in_my_projects.png)

![update project in myprojects.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/update_project_in_myprojects.png)

## All Projects

```
- Only Authentificated User Can Access this page
- List All The Projects 
- Access = ["Admin","ProjectManager"]
```

```
- Create New Project => Create A New Project 
- Delete All Projcts => Delete All The Projects 
- Delete => Delete the Task by the id
- Edit => Edit The Project ( Can Edit the Group)
- Change State => ["UpComing","OnGoing","Completed"]
```

![all projects.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/all_projects.png)

![Update projects in all projects.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/Update_projects_in_all_projects.png)

![new project in all projects.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/new_project_in_all_projects.png)

## Users

```
- Only Authentificated User Can Access this page
- Manage The Users
- Access = ["Admin"]
```

![users.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/users.png)

![Update User.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/Update_User.png)

![new User.png](SM%20Tasks%20919164f39b074c6c9c45ac024b248138/new_User.png)

## Logout() Function

```csharp
public async System.Threading.Tasks.Task Logout()
    {
      await _sessionStorage.DeleteAsync("jwt");
      await _sessionStorage.DeleteAsync("id");
      await _sessionStorage.DeleteAsync("role");
      await _sessionStorage.DeleteAsync("group");
    }
```

## Authentification & Registration Functions

```csharp
public async Task<User?> AuthenticateUser(string email, string password)
    {
      var login = new UserLogin() { Email = email, Password = password };

      HttpResponseMessage response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/user/login", login);
      if (response.IsSuccessStatusCode)
      {
        var result = await response.Content.ReadFromJsonAsync<UserToken>();
        if (result != null)
        {
          await _sessionStorage.SetAsync("jwt", result.Token);
          await _sessionStorage.SetAsync("id", result.User.Id.ToString());
          await _sessionStorage.SetAsync("role", result.User.Role.ToString());
          await _sessionStorage.SetAsync("group", result.User.GroupId.ToString());

          return result.User;
        }
      }
      return null;
    }

    public async Task<User?> RegisterUser(string prenom, string nom, string email, string password, string username, string gender)
    {
      var registerInfo = new User(nom, prenom, email, password, username, gender);
      HttpResponseMessage response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/User/register", registerInfo);
      if (response.IsSuccessStatusCode)
      {
        var result = await response.Content.ReadFromJsonAsync<UserToken>();
        if (result != null)
        {
          await _sessionStorage.SetAsync("jwt", result.Token);
          await _sessionStorage.SetAsync("id", result.User.Id.ToString());
          await _sessionStorage.SetAsync("role", result.User.Role.ToString());
          await _sessionStorage.SetAsync("group", result.User.GroupId.ToString());

          return result.User;
        }
      }
      return null;
    }
```