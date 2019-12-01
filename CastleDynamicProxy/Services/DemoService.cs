using CastleDynamicProxy.Requests;
using System.Threading.Tasks;

namespace CastleDynamicProxy.Services
{
    public class DemoService : IDemoService
    {
        public async Task<BaseResponse<string>> TestAsync(TestRequest request)
        {
            await Task.CompletedTask;

            return new BaseResponse<string>
            {
                Code = 1,
                Data = $"hi,{request.Name}"
            };
        }
    }
}
