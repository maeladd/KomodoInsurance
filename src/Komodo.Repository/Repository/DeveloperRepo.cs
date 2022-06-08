using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class DeveloperRepo
{
    private readonly List<Developer>
    _developerList = new List<Developer>();
    private int _devID;

    public bool AddDeveloperToTeam
    (Developer developer)
    {
        if(developer != null)
        {
            _count++;
            developer.DevID = _count;
            _developerList.Add(developer);
            return true;
        }
        else
        {
            return false;
        }
    }

}
