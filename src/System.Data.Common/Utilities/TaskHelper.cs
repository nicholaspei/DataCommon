// Copyright (c) Microsoft Open Technologies, Inc.
// All Rights Reserved
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING
// WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF
// TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR
// NON-INFRINGEMENT.
// See the Apache 2 License for the specific language governing
// permissions and limitations under the License.

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