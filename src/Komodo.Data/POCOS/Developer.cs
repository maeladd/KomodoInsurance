﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Developer 
{
    public Developer(){}
    public Developer(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName; 
    } 
    public int DevID { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public bool PSAccess { get; set; }
}
