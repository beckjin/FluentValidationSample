using FluentValidation;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace CastleDynamicProxy.Extensions
{
    public static class ValidatorExtension
    {
        private static readonly object Locker = new object();
        private static ConcurrentDictionary<string, IValidator> _cacheValidators;

        /// <summary>
        /// 初始化，指定 Validators 所在的命名空间
        /// </summary>
        /// <param name="assembly"></param>
        public static void Initialize(Assembly assembly)
        {
            lock (Locker)
            {
                if (_cacheValidators == null)
                {
                    _cacheValidators = new ConcurrentDictionary<string, IValidator>();
                    var results = AssemblyScanner.FindValidatorsInAssembly(assembly);
                    foreach (var result in results)
                    {
                        var modelType = result.InterfaceType.GenericTypeArguments[0];
                        _cacheValidators.TryAdd(modelType.FullName, (IValidator)Activator.CreateInstance(result.ValidatorType));
                    }
                }
            }
        }

        public static bool IsValid<T>(this T request, out string msg) where T : class
        {
            msg = string.Empty;

            if (_cacheValidators == null || !_cacheValidators.TryGetValue(request.GetType().FullName, out var validator))
                return true;

            var result = validator.Validate(request);
            if (!result.IsValid)
            {
                // 返回第一个错误信息
                msg = result.Errors[0].ErrorMessage;
                return false;
            }

            return true;
        }
    }
}
