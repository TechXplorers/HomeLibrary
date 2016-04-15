using System;
using System.Linq;
using HomeLibrary.Common.Contracts;
using HomeLibrary.Common.Dto;

namespace HomeLibrary.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly string jsonFile = "loans.json";
        private readonly JsonHelper jsonHelper;

        public LoanRepository(JsonHelper jsonHelper)
        {
            this.jsonHelper = jsonHelper;
        }

        public Loan GetCurrent(int bookId)
        {
            return
                jsonHelper.ReadFromJson<Loan>(jsonFile)
                .FirstOrDefault(x => x.BookId == bookId && !x.IsReturned);
        }

        public Loan Create(Loan loan)
        {
            var loans = jsonHelper.ReadFromJson<Loan>(jsonFile).ToList();
            var maxId = loans.Any() ? loans.Max(x => x.Id) : 0;

            loan.Id = maxId + 1;
            loans.Add(loan);

            jsonHelper.WriteAsJson(jsonFile, loans.ToArray());

            return loan;
        }

        public Loan Update(int id, Loan loan)
        {
            var loans = jsonHelper.ReadFromJson<Loan>(jsonFile);

            if (loans.Any(x => x.Id == id) == false)
                return null;

            loan.Id = id;

            loans =
                loans.Where(x => x.Id != id)
                    .Union(new[] {loan})
                    .OrderBy(x => x.Id)
                    .ToArray();

            jsonHelper.WriteAsJson(jsonFile, loans);

            return loan;
        }
    }
}
