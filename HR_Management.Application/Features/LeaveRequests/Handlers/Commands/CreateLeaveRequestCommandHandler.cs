using AutoMapper;
using HR_Management.Application.Contract.Infrastructure;
using HR_Management.Application.Contract.Persistence;
using HR_Management.Application.DTOs.LeaveRequest.Validators;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Application.Models;
using HR_Management.Application.Responses;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IEmailSender _emailSender;

    public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository ,
        IMapper mapper, ILeaveTypeRepository leaveTypeRepository , IEmailSender emailSender)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _emailSender = emailSender;
    }
    public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request.CreateLeaveRequestDto);
        if (validationResult.IsValid == false)
        {
            // throw new ValidationExeption(validationResult);

            response.Success = false;
            response.Message = "Create Faild.";
            response.Errors = validationResult.Errors.Select(c=> c.ErrorMessage).ToList();
            
        }

        var leaveRequest = _mapper.Map<LeaveRequest>(request.CreateLeaveRequestDto);
        leaveRequest = await _leaveRequestRepository.Add(leaveRequest);

        response.Success = true;
        response.Message = "Creattion successfull.";
        response.Id = leaveRequest.Id;


        var email = new Email()
        {
            To = "amir.m.n.1380@gmail.com",
             Subject = "Leave Request Submitted",
             Body = $"Your Leave Request For {request.CreateLeaveRequestDto.StartDate} " +
             $"To {request.CreateLeaveRequestDto.EndDate} has been submitted."
        };

        try
        {
            await _emailSender.SendEmail(email);
        }
        catch
        {
            // log
        }


        return response;
    }
}
