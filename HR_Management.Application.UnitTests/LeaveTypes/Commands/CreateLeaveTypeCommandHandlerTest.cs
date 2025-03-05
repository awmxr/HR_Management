using AutoMapper;
using HR_Management.Application.Contract.Persistence;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveTypes.Handlers.Commands;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Application.Profiles;
using HR_Management.Application.UnitTests.Mock;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.UnitTests.LeaveTypes.Commands;

public class CreateLeaveTypeCommandHandlerTest
{
    IMapper _mapper;
    Mock<ILeaveTypeRepository> _mockrepository;
    CreateLeaveTypeDto _leaveTypeDto;
    public CreateLeaveTypeCommandHandlerTest()
    {
        _mockrepository = MockLeaveTypeRepository.GetLeaveTypeRepository();
        var mapperConfig = new MapperConfiguration(m =>
        {
            m.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _leaveTypeDto = new CreateLeaveTypeDto
        {
            DefaultDay = 10,
            Name = "Test Added"
        };

    }

    [Fact]
    public async Task CreateLeaveTypeTest()
    {
        var handler = new CreateLeaveTypeCommandHandler(_mockrepository.Object , _mapper);

        var result = await handler.Handle(new CreateLeaveTypeCommand() { CreateLeaveTypeDto = _leaveTypeDto}, CancellationToken.None);

        result.ShouldBeOfType<int>();
        var leaveTypes = await _mockrepository.Object.GetAll();
        leaveTypes.Count.ShouldBe(3);


    }
}
