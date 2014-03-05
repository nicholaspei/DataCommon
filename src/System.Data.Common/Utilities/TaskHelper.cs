// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Utilities
{
    internal static class TaskHelper
    {
        internal static Task<T> FromException<T>(Exception ex)
        {
            var completion = new TaskCompletionSource<T>();
            completion.SetException(ex);
            return completion.Task;
        }

        internal static Task<T> FromCancellation<T>()
        {
            var completion = new TaskCompletionSource<T>();
            completion.SetCanceled();
            return completion.Task;
        }

        internal static Task FromOperation(Action syncOperation, Action cancelCallback,
            CancellationToken cancellationToken)
        {
            return FromOperation(ExecuteAsBool, syncOperation, cancelCallback, cancellationToken);
        }

        private static bool ExecuteAsBool(Action operation)
        {
            operation();

            // return value is irrelevant as it won't be used
            return false;
        }

        internal static Task<TResult> FromOperation<TResult>(Func<TResult> syncOperation, Action cancelCallback,
            CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return FromCancellation<TResult>();
            }

            var tokenRegistration = new CancellationTokenRegistration();
            if (cancellationToken.CanBeCanceled && cancelCallback != null)
            {
                tokenRegistration = cancellationToken.Register(cancelCallback);
            }

            try
            {
                return Task.FromResult(syncOperation());
            }
            catch (Exception ex)
            {
                tokenRegistration.Dispose();
                return FromException<TResult>(ex);
            }
        }

        internal static Task<TResult> FromOperation<TParam, TResult>(Func<TParam, TResult> syncOperation, TParam parameter,
            Action cancelCallback, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return FromCancellation<TResult>();
            }

            var tokenRegistration = new CancellationTokenRegistration();
            if (cancellationToken.CanBeCanceled && cancelCallback != null)
            {
                tokenRegistration = cancellationToken.Register(cancelCallback);
            }

            try
            {
                return Task.FromResult(syncOperation(parameter));
            }
            catch (Exception ex)
            {
                tokenRegistration.Dispose();
                return FromException<TResult>(ex);
            }
        }
    }
}