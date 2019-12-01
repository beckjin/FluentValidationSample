using Castle.DynamicProxy;
using System;
using System.Threading.Tasks;
using CastleDynamicProxy.Extensions;
using CastleDynamicProxy.Requests;

namespace CastleDynamicProxy.Interceptors
{
    public class ServiceInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var request = invocation.Arguments[0];

            var isValid = request.IsValid(out var message);

            if (!isValid)
            {
                var resultType = invocation.Method.ReturnType.GenericTypeArguments[0];
                invocation.ReturnValue = GetParamsErrorValueAsync((dynamic)Activator.CreateInstance(resultType), message);
                return;
            }

            invocation.Proceed();
            invocation.ReturnValue = GetReturnValueAsync((dynamic)invocation.ReturnValue);
        }


        private async Task<BaseResponse<T>> GetParamsErrorValueAsync<T>(BaseResponse<T> result, string message)
        {
            await Task.CompletedTask;
            return new BaseResponse<T> { Code = 0, Message = message };
        }

        private async Task<BaseResponse<T>> GetReturnValueAsync<T>(Task<BaseResponse<T>> task)
        {
            try
            {
                return await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return new BaseResponse<T> { Code = 0, Message = ex.Message };
            }
        }
    }
}
