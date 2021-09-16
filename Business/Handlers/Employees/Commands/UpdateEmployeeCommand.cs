
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Employees.ValidationRules;


namespace Business.Handlers.Employees.Commands
{


    public class UpdateEmployeeCommand : IRequest<IResult>
    {
        public int EmployeeId { get; set; }
        public int ReportsTo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public System.DateTime BirthDate { get; set; }
        public System.DateTime BirtHireDatehDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public string Notes { get; set; }
        public string PhotoPath { get; set; }

        public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, IResult>
        {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMediator _mediator;

            public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMediator mediator)
            {
                _employeeRepository = employeeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateEmployeeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var isThereEmployeeRecord = await _employeeRepository.GetAsync(u => u.EmployeeId == request.EmployeeId);


                isThereEmployeeRecord.ReportsTo = request.ReportsTo;
                isThereEmployeeRecord.LastName = request.LastName;
                isThereEmployeeRecord.FirstName = request.FirstName;
                isThereEmployeeRecord.Title = request.Title;
                isThereEmployeeRecord.TitleOfCourtesy = request.TitleOfCourtesy;
                isThereEmployeeRecord.BirthDate = request.BirthDate;
                isThereEmployeeRecord.BirtHireDatehDate = request.BirtHireDatehDate;
                isThereEmployeeRecord.Address = request.Address;
                isThereEmployeeRecord.City = request.City;
                isThereEmployeeRecord.Region = request.Region;
                isThereEmployeeRecord.PostalCode = request.PostalCode;
                isThereEmployeeRecord.Country = request.Country;
                isThereEmployeeRecord.HomePhone = request.HomePhone;
                isThereEmployeeRecord.Extension = request.Extension;
                isThereEmployeeRecord.Notes = request.Notes;
                isThereEmployeeRecord.PhotoPath = request.PhotoPath;


                _employeeRepository.Update(isThereEmployeeRecord);
                await _employeeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

