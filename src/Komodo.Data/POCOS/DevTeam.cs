using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class DevTeam 
{
    public DevTeam(){}
    public DevTeam(string teamName)
    {
        TeamName = teamName;
    }

    public DevTeam(string teamName, List<Developer> teamMembers, List<DevID> teamID)
    {
    TeamName = teamName;
    TeamMembers = teamMembers;
    TeamID = teamID;
    }
}