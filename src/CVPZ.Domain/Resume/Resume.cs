using CVPZ.Domain.Resume.Events;
using System.Collections.Generic;
using Tactical.DDD;

namespace CVPZ.Domain.Resume
{
    public class Resume : Tactical.DDD.EventSourcing.AggregateRoot<ResumeId>
    {
        public override ResumeId Id { get; protected set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Resume(IEnumerable<IDomainEvent> events) : base(events)
        { }

        private Resume()
        { }

        public static Resume CreateNewResume(
            string firstName,
            string lastName)
        {

            var resume = new Resume();
            var @event = new ResumeCreated(new ResumeId().ToString(), firstName, lastName);
            resume.Apply(@event);

            return resume;
        }

        public void ModifyResume(string firstName, string lastName)
        {
            Apply(new ResumeModified(firstName, lastName));
        }

        public void On(ResumeCreated @event)
        {
            Id = new ResumeId(@event.Id);
            FirstName = @event.FirstName;
            LastName = @event.LastName;
        }

        public void On(ResumeModified @event)
        {
            FirstName = @event.FirstName;
            LastName = @event.LastName;
        }
    }
}
