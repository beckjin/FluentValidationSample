using CastleDynamicProxy.Requests;
using System.Threading.Tasks;

namespace CastleDynamicProxy.Services
{
    public interface IDemoService
    {
        Task<BaseResponse<string>> TestAsync(TestRequest request);
    }
}
