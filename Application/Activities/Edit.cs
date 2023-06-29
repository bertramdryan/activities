using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Commmand : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Commmand>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Commmand request, CancellationToken cancellationToken)
            {
               var activity = await _context.Activities.FindAsync(request.Activity.Id);

               activity.Title = request.Activity.Title ?? activity.Title;

               _context.Activities.Update(activity);

               await _context.SaveChangesAsync();

               return Unit.Value;
            }
        }
    }
}