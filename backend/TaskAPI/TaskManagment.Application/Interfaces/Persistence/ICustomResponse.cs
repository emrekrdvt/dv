using MediatR;

namespace TaskManagment.Application.Interfaces.Persistence;

public class CustomResponse<T> : IRequest<Unit>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public static CustomResponse<T> SuccessResponse(T data, string? message = null)
        => new() { Success = true, Message = message, Data = data };

    public static CustomResponse<T> FailResponse(string message)
        => new() { Success = false, Message = message };
}
