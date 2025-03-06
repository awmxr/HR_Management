using HR_Management.MVC.Contracts;
using System.Net.Http.Headers;

namespace HR_Management.MVC.Services.Base
{
    public class BaseHttpService
    {
        protected readonly IClient _client;
        protected readonly ILocalStorageService _storage;

        public BaseHttpService(IClient client , ILocalStorageService localStorageService)
        {
             _client = client;
            _storage = localStorageService;
        }

        protected Response<Guid> ConverApiExeptions<Guid>(ApiException Exception)
        {
            if(Exception.StatusCode == 400)
            {
                return new Response<Guid> { Message = "Validation errors occured.", Success = false, ValidationErrors = Exception.Response };
            }
            else if(Exception.StatusCode == 404)
            {
                return new Response<Guid> { Message = "NotFound! try again", Success = false,  };
            }
            else
            {
                return new Response<Guid> { Message = "Somthing went wrong, try Again ... ", Success = false };

            }
        }

        protected void AddBearerToken()
        {
            if (_storage.Exist("token"))
            {
                var x = _storage.GetStorageValue<string>("token");
                _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", x);
            }
        }
    }
}
