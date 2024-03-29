﻿using HetDepot.People.Model;
using HetDepot.Settings.Model;
using HetDepot.Tours.Model;

namespace HetDepot.Persistence
{
    public interface IRepository
    {
        List<Person> GetPeople();
        List<Tour> GetTours(string alternativeTourPath = "");
        List<List<Tour>> GetAllTours();
        Setting GetSettings();
        void Write<T>(T objectToWrite);
    }
}
