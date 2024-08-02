using MediatR;

namespace FluentTest.Identity.Jobs
{
    public class SyncDataRequest : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
