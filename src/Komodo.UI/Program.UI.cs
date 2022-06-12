using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    public class ProgramUI
{
    private readonly DeveloperRepo _dRepo = new DeveloperRepo();
    private readonly DevTeamRepo _dtRepo = new DevTeamRepo();

    public void Run()
    {
        SeedData();
        RunApp();
    }

    private void SeedData()
    {
        Developer ed = new Developer (1, "Edward", "Teach", Pluralsight.Access);
        Developer steed = new Developer (2, "Steed", "Bonnet", Pluralsight.No_Access);
        Developer villanelle = new Developer (3, "Oksana", "Astankova", Pluralsight.Access);
        Developer eve = new Developer (4, "Eve", "Polastri", Pluralsight.No_Access);
        Developer louis = new Developer (5, "Louis", "de Pointe du Lac", Pluralsight.Access);
        Developer gizmo = new Developer (6, "Guillermo", "de la Cruz", Pluralsight.No_Access);
    
        _dRepo.AddDeveloperToDatabase(ed);
        _dRepo.AddDeveloperToDatabase(steed);
        _dRepo.AddDeveloperToDatabase(villanelle);     
        _dRepo.AddDeveloperToDatabase(eve);
        _dRepo.AddDeveloperToDatabase(louis);
        _dRepo.AddDeveloperToDatabase(gizmo);    

        DevTeam team_1 = new DevTeam("Pirates");
        DevTeam team_2 = new DevTeam("Spies");            
        DevTeam team_3 = new DevTeam("Vamps");

        _dtRepo.AddTeamToDatabase(team_1);
        _dtRepo.AddTeamToDatabase(team_2);
        _dtRepo.AddTeamToDatabase(team_3);
    }

    public void RunApp()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            System.Console.WriteLine("\t~~~Komodo Insurance~~~");
            System.Console.WriteLine(
                "\t Please select your option: \n"+
                "Komodo Insurance Develope Teams \n"+
                "1. Add Developer Team \n"+
                "2. View All Developer Teams \n"+
                "3. View a Developer Team \n"+
                "4. Update a Developer Team \n"+
                "5. Delete a Developer Team \n"+
                "5. Delete a Developer Team \n"+
                "----------------------------\n"+
                "~~~Komodo Developers~~~\n"+
                "6. Add a Developer \n"+
                "7. View All Developers \n"+
                "8. View One Developer \n"+
                "---------------------------\n"+
                "Close Application\n");

            string managerInput = Console.ReadLine();

            switch(managerInput.ToLower())
            {
                case "1":
                    AddDeveloperTeamtoDatabase();
                    break;
                case "2":
                    ViewAllDeveloperTeams();
                    break;
                case "3":
                    ViewDeveloperTeambyID();
                    break;
                case "4":
                    UpdateDeveloperTeam();
                    break;
                case "5":
                    DeleteDeveloperTeam();
                    break;
                case "6":
                    AddDevelopertoDatabase();
                    break;
                case "7":
                    ViewAllDevelopers();
                    break;
                case "8":
                    ViewDeveloperbyID();
                    break;
                case "x":
                    isRunning = CloseApplication();
                    break;
                
                default: 
                System.Console.WriteLine("Please enter a valid selection.");
                break;
            }
        }
    }

    private bool CloseApplication()
    {
        Console.Clear();
        System.Console.WriteLine("Thank you.");
        PressAnyKey();
        return false;
    }
    
    private void PressAnyKey()
    {
        System.Console.WriteLine("Press any key to continue...");
        Console.ReadLine;
    }

    private void AddDeveloperTeamtoDatabase()
    {
        Console.Clear();

        DevTeam newDeveloperTeam = new DevTeam();
        var currentDevelopers = _dRepo.AllDevelopers();

        System.Console.WriteLine("Please enter a team name.");
        newDeveloperTeam.Name = System.Console.ReadLine();

        bool hasAssignedDevelopers = false;
        while(!hasAssignedDevelopers)
        {
            System.Console.WriteLine("Do you have any developers? Y/N");
            string hasDevelopers = System.Console.ReadLine().ToLower();

            if(hasDevelopers = "y")
            {
                foreach(var d in currentDevelopers)
                {
                    System.Console.WriteLine($"ID {d.DevID}: {d.FirstName} {d.LastName}");
                }
                System.Console.WriteLine("Please select a developer by their ID: \n");
                int developerSelection = int.Parse (Console.ReadLine());
                Developer selectedDeveloper = _dRepo.GetDeveloperByID(developerSelection);

                if(selectedDeveloper != null)
                    {
                        newDeveloperTeam.Developer.Add(selectedDeveloper);

                        currentDevelopers.Remove(selectedDeveloper);
                    }
                    else
                    {
                        System.Console.WriteLine($"Sorry, the Developer Team with ID: {selectedDeveloper} cannot be used.");
                    }
                }
                else
                {
                    hasAssignedDevelopers = true;
                }

            }


            bool isSuccessful = _dtRepo.AddDevTeamToDatabase(newDeveloperTeam);
            if(isSuccessful)
            {
                System.Console.WriteLine($"Developer Team:{newDeveloperTeam.Name} was added to the database!");
            }
            else
            {
                System.Console.WriteLine("Developer Team failed to be added to database.");
            }

            PressAnyKey();
        }        

        private void ViewAllDeveloperTeams()
        {
            Console.Clear();

            System.Console.WriteLine("~~~ Developer Teams ~~~");

            var teamsInDB = _dtRepo.GetAllTeams();
            foreach(DevTeam t in teamsInDB)
            {
                DisplayDevTeams(t);
            }

            PressAnyKey();
        }

        private void ViewDeveloperTeamByID()
        {
            Console.Clear();

            System.Console.WriteLine("~~~ Developer Team Info ~~~");
            var devTeams = _dtRepo.GetAllTeams();

            foreach(DevTeam t in devTeams)
            {
                DisplayDevTeamInfo(t);
            }
            try
            {
                System.Console.WriteLine("Please select a Developer Team by their ID: \n");
                int userInput = int.Parse(Console.ReadLine());
                var selectedDevTeam = _dtRepo.GetDevTeamByID(userInput);

                if(selectedDevTeam != null)
                {
                    DisplayDevTeams(selectedDevTeam);
                }
                else
                {
                    System.Console.WriteLine($"Sorry, the Developer Team with ID: {userInput} does not exist. Try again.");
                }
            }
            catch
            {
                System.Console.WriteLine("Sorry, invalid selection. Please try again.");
            }
            PressAnyKey();
        }

        private void DisplayDevTeamInfo(DevTeam thisDevTeam)
        {
            Console.Clear();

            System.Console.WriteLine("~~~ Team Developers ~~~ \n");
            System.Console.WriteLine($"Team ID: {thisDevTeam.TeamID} \n" + $"Team Name: {thisDevTeam.Name} \n");

            if(thisDevTeam.Developer.Count > 0)
            {
                foreach(var d in thisDevTeam.Developer)
                {
                    DisplayDeveloperInfo(d);
                }
            }
            else
            {
                System.Console.WriteLine("There are no developers on this team.");
            }
            PressAnyKey();
        }

        private void UpdateDeveloperTeam()
        {
            Console.Clear();

            var availableTeams = _dtRepo.GetAllTeams();
            foreach (var t in availableTeams)
            {
                DisplayDevTeamInfo(t);
            }

            System.Console.WriteLine("Please enter a Developer Team ID: \n");
            int userInput = int.Parse(Console.ReadLine());
            var selectedDevTeam = _dtRepo.GetDevTeamByID(userInput);

            if(selectedDevTeam != null)
            {
                Console.Clear();
                DevTeam newDevTeam = new DevTeam();
                var currentDeveloper = _dRepo.GetAllDevelopers();

                System.Console.WriteLine("Please enter a Team Name: \n");
                newDevTeam.Name = Console.ReadLine();

                bool hasAssignedDevelopers = false;
                while(!hasAssignedDevelopers)
                {
                    System.Console.WriteLine("Do you have any developers to add to this team? y/n \n");
                    string developerInput = Console.ReadLine().ToLower();

                    if(developerInput == "y")
                    {
                        foreach(var d in currentDeveloper)
                        {
                            System.Console.WriteLine($"{d.ID} {d.FirstName} {d.LastName}");
                        }
                        System.Console.WriteLine("Please choose a developer by ID: \n");
                        int developerSelected = int.Parse(Console.ReadLine());
                        var selectedDeveloper = _dRepo.GetDeveloperByID(developerSelected);
                        
                        if(selectedDeveloper != null)
                        {
                            newDevTeam.Developer.Add(selectedDeveloper);
                            currentDeveloper.Remove(selectedDeveloper);
                        }
                        else
                        {
                            System.Console.WriteLine("Sorry, that developer does not exist. Please try again.");
                        }
                    }
                    else
                    {
                        hasAssignedDevelopers = true;
                    }
                }

                bool isSuccessful = _dtRepo.UpdateDevTeamData(userInput, newDevTeam);
                if(isSuccessful)
                {
                    System.Console.WriteLine("Dev Team is updated!");
                }
                else
                {
                    System.Console.WriteLine("Dev Team failed to update. Please try again.");
                }
            }
            else
            {
                System.Console.WriteLine($"The team with ID: {userInput} isn't valid. Please try again.");
            }

            PressAnyKey();
        }

        private void DeleteDeveloperTeam()
        {
            Console.Clear();

            System.Console.WriteLine("~~~ Deletion ~~~");
            var devTeams = _dtRepo.GetAllTeams();
            foreach(DevTeam t in devTeams)
            {
                DisplayDevTeamInfo(t);
            }
            try
            {
                System.Console.WriteLine("Please enter the ID of the Dev Team you wish to delete: \n");
                int userSelection = int.Parse(Console.ReadLine());
                bool isSuccessful = _dtRepo.RemoveDevTeamFromDatabase(userSelection);

                if(isSuccessful)
                {
                    System.Console.WriteLine("Dev Team has been deleted.");
                }
                else
                {
                    System.Console.WriteLine("Team was not deleted. Please try again.");
                }
            }
            catch
            {
                System.Console.WriteLine("Please enter a valid Team ID.");
            }

            PressAnyKey();
        }

        private void AddDeveloperToDatabase()
        {
            Console.Clear();

            var newDeveloper = new Developer();
            System.Console.WriteLine("~~~ Add A New Developer ~~~");

            System.Console.WriteLine("First Name: ");
            newDeveloper.FirstName = Console.ReadLine();

            System.Console.WriteLine("Last Name: ");
            newDeveloper.LastName = Console.ReadLine();

            bool isSuccessful = _dRepo.AddDeveloperToDatabase(newDeveloper);

            if(isSuccessful)
            {
                System.Console.WriteLine($"{newDeveloper.FirstName} {newDeveloper.LastName} was added to the database. ");
            }
            else
            {
                System.Console.WriteLine("Unable to add developer. Please try again.");
            }
            PressAnyKey();
        }

        private void ViewAllDevelopers()
        {
            Console.Clear();

            List<Developer> developersinDB = _dRepo.GetAllDevelopers();
            if(developersinDB.Count > 0)
            {
                foreach (Developer d in developersinDB)
                {
                    DisplayDeveloperInfo(d);
                }
            }
            else
            {
                System.Console.WriteLine("There are no developers to display.");
            }

            PressAnyKey();
        }

        private void DisplayDeveloperInfo(Developer developer)
        {
            System.Console.WriteLine(
                $"ID: {developer.ID} \n" +
                $"First Name: {developer.FirstName} \n" +
                $"Last Name: {developer.LastName} \n" +
                $"Pluralsight Access: {developer.Pluralsight} \n" 
            );
        }

        private void ViewDeveloperByID()
        {
            Console.Clear();

            System.Console.WriteLine("~~~ Developer Information ~~~");
            System.Console.WriteLine("Enter employee ID: ");
            int inputDevID = int.Parse(Console.ReadLine());

            Developer developer = _dRepo.GetDeveloperByID(inputDevID);
            if(developer != null)
            {
                DisplayDeveloperInfo(developer);
            }
            else
            {
                System.Console.WriteLine($"{inputDevID} is not a valid developer ID. Please try again.");
            }

            PressAnyKey();
        }


        //Helper Methods
        private void DisplayDevTeams(DevTeam devTeam) 
        {
            System.Console.WriteLine($"Developer Team: {devTeam.TeamID} \n Team Name: {devTeam.Name}\n");
        }
    } 