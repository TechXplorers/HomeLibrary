using System;
using System.Linq;
using HomeLibrary.Common.Contracts;
using HomeLibrary.Common.Dto;

namespace HomeLibrary.Repository
{
    public class LendeeRepository : ILendeeRepository
    {
        private readonly string jsonFile = "lendees.json";        
        private readonly JsonHelper jsonHelper;

        public LendeeRepository(JsonHelper jsonHelper)
        {
            this.jsonHelper = jsonHelper;
        }

        public Lendee GetById(int id)
        {
            return
                jsonHelper.ReadFromJson<Lendee>(jsonFile).FirstOrDefault(x => x.Id == id);
        }

        public Lendee[] GetAll()
        {
            return jsonHelper.ReadFromJson<Lendee>(jsonFile); 
        }

        public Lendee Create(Lendee lendee)
        {
            var lendees = jsonHelper.ReadFromJson<Lendee>(jsonFile).ToList();
            var maxId = lendees.Any() ? lendees.Max(x => x.Id) : 0;

            lendee.Id = maxId + 1;
            lendees.Add(lendee);

            jsonHelper.WriteAsJson(jsonFile, lendees.ToArray());

            return lendee;
        }
    }
}
