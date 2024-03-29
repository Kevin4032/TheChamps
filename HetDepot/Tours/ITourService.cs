﻿using System.Collections.ObjectModel;
using HetDepot.People.Model;
using HetDepot.Tours.Model;

namespace HetDepot.Tours
{
    public interface ITourService
    {
        ReadOnlyCollection<Tour> Tours { get; }
        bool AddTourReservation(Tour tour, Visitor visitor);
        bool RemoveTourReservation(Tour tour, Visitor visitor);
        bool AddTourAdmission(Tour tour, Visitor visitor);
        Tour? GetReservation(Visitor visitor);
        bool HasReservation(Visitor visitor);
        bool HasAdmission(Visitor visitor);
        bool StartTour(Tour tour);
        Tour? GetTourByStartTime(DateTime startTime);
        Tour? GetNextTour();
        List<List<Tour>> GetAllTours();
        List<Tour> GetOpenTours();

    }
}
