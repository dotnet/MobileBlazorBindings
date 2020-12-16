// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Microsoft.MobileBlazorBindings.ProtectedStorage
{
    /// <summary>
    /// An implementation of <see cref="IProtectedStorage"/> using Xamarin Essentials.
    /// </summary>
    /// <remarks>
    /// The Xamarin Essentials implementation on iOS, macOS, and Android
    /// is synchronous and blocking. Better to run threadpool threads.
    /// </remarks>
    public class XamarinProtectedStorage : IProtectedStorage
    {
        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            // Exceptions should bubble up because of the await.
            return await Task.Run(() => SecureStorage.Remove(key)).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task SetAsync(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or empty", nameof(key));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var serializedValue = JsonSerializer.Serialize(value, value.GetType());

            // Exceptions should bubble up because of the await.
            await Task.Run<Task>(async () => await SecureStorage.SetAsync(key, serializedValue).ConfigureAwait(false)).Unwrap().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<TValue> GetAsync<TValue>(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            // Exceptions should bubble up because of the await.
            var result = await Task.Run<Task<string>>(async () => await SecureStorage.GetAsync(key).ConfigureAwait(false)).Unwrap().ConfigureAwait(false);
            if (result == default)
            {
                return default;
            }

            return JsonSerializer.Deserialize<TValue>(result);
        }
    }
}
