using System;

namespace HomeLibrary.Common.Dto
{
    public class Loan
    {
        public int Id { get; set; }
        public int LendeeId { get; set; }
        public int BookId { get; set; }
        public DateTime LentOn { get; set; }

        public bool IsReturned { get; set; }
    }
}
