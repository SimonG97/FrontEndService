using FrontEndService.Models;

namespace FrontEndService.Services.IServices;

public interface IBaseService : IDisposable
{
    ResponseDto responseModel { get; set; }
    Task<T> SendAsync<T>(ApiRequest apiRequest);
}
