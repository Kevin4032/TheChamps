﻿using HetDepot.Errorlogging;
using HetDepot.People.Model;
using HetDepot.Persistence;

namespace HetDepot.People
{
    public class PeopleService : IPeopleService
    {
        private List<Person> _people;
        private IRepository _repository;
        private IDepotErrorLogger _errorLogger;

        public PeopleService(IRepository repository, IDepotErrorLogger errorLogger)
        {
            _repository = repository;
            _errorLogger = errorLogger;
            _people = _repository.GetPeople();
        }

        /// <exception cref="NullReferenceException">
        /// Thrown when no visitors found
        /// </exception>
        public IReadOnlyCollection<Visitor> GetVisitors()
        {
            var visitors = new List<Visitor>();
            var visitorsInPeople = _people.FindAll(p => p.GetType() == typeof(Visitor)) ?? throw new NullReferenceException("Geen bezoekers gevonden, zie ExampleVisitor.json");
            return visitorsInPeople.Select(visitor => (Visitor)visitor).ToList().AsReadOnly();
        }

        /// <exception cref="NullReferenceException">
        /// Thrown when no id found
        /// </exception>
        public Person GetById(string id) => _people.FirstOrDefault(p => p.Id == id) ?? throw new NullReferenceException("Geen bezoeker gevonden met ID {id}, zie ExampleVisitor.json, ExampleGuide.json, ExampleManager.json");
        /// <exception cref="NullReferenceException">
        /// Thrown when no id found for visitor
        /// </exception>
        public Visitor GetVisitorById(string id) => _people.FirstOrDefault(p => p.Id == id) as Visitor ?? throw new NullReferenceException($"Geen bezoeker gevonden met ID {id}, zie ExampleVisitor.json");
        /// <exception cref="NullReferenceException">
        /// Thrown when no id found
        /// </exception>
        public Guide GetGuide()
        {
            var guides = _people.Where(p => p.GetType() == typeof(Guide));

            if (guides.Count() > 1)
                LogError("Te veel Gidsen opgevoerd in ExampleGuide.json, er mag maar 1 Gids zijn opgevoerd.");

            return _people.FirstOrDefault(p => p.GetType() == typeof(Guide)) as Guide ?? throw new NullReferenceException("Geen gids gevonden, zie ExampleGuide.json");
        }
        /// <exception cref="NullReferenceException">
        /// Thrown when no id found
        /// </exception>
        public Manager GetManager()
        {
            var managers = _people.Where(p => p.GetType() == typeof(Manager));

            if (managers.Count() > 1)
                LogError("Te veel Afdelingshoofden opgevoerd in ExampleManager.json, er mag maar 1 Afdelingshoofd zijn opgevoerd.");

            return _people.FirstOrDefault(p => p.GetType() == typeof(Manager)) as Manager ?? throw new NullReferenceException("Geen afdelingshoofd gevonden, zie ExampleManager.json");
        }

        private void LogError(string eMsg)
        {
            _errorLogger.LogError(eMsg);
            throw new NullReferenceException(eMsg);
        }
    }
}
