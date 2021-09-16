
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Employees.ValidationRules;

namespace Business.Handlers.Employees.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateEmployeeCommand : IRequest<IResult>
    {

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


        public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, IResult>
        {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMediator _mediator;
            public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMediator mediator)
            {
                _employeeRepository = employeeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateEmployeeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var isThereEmployeeRecord = _employeeRepository.Query().Any(u => u.ReportsTo == request.ReportsTo);

                if (isThereEmployeeRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedEmployee = new Employee
                {
                    ReportsTo = request.ReportsTo,
                    LastName = request.LastName,
                    FirstName = request.FirstName,
                    Title = request.Title,
                    TitleOfCourtesy = request.TitleOfCourtesy,
                    BirthDate = request.BirthDate,
                    BirtHireDatehDate = request.BirtHireDatehDate,
                    Address = request.Address,
                    City = request.City,
                    Region = request.Region,
                    PostalCode = request.PostalCode,
                    Country = request.Country,
                    HomePhone = request.HomePhone,
                    Extension = request.Extension,
                    Notes = request.Notes,
                    PhotoPath = request.PhotoPath,

                };

                _employeeRepository.Add(addedEmployee);
                await _employeeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}