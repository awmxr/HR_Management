using AutoMapper;
using HR_Management.Application.Contract.Persistence;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveTypes.Handlers.Queries;
using HR_Management.Application.Features.LeaveTypes.Requests.Queries;
using HR_Management.Application.Profiles;
using HR_Management.Application.UnitTests.Mock;
using Moq;
using Shouldly;

namespace HR_Management.Application.UnitTests.LeaveTypes.Queries;

public class GetLeaveTypeListRequestHandlerTests
{
    IMapper _mapper;
    Mock<ILeaveTypeRepository> _mockrepository;
    public GetLeaveTypeListRequestHandlerTests()
    {
        _mockrepository = MockLeaveTypeRepository.GetLeaveTypeRepository();
        var mapperConfig = new MapperConfiguration(m =>
        {
            m.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        var handler = new GetLeaveTypeListRequestHandler(_mockrepository.Object,_mapper);

        var result = await handler.Handle(new GetLeaveTypeListRequest(), CancellationToken.None);

        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(2);
    }
}
