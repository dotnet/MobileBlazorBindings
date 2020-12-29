// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Concurrent;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.ProtectedStorage
{
    /// <summary>
    /// An implementation of <see cref="IProtectedStorage"/> for Windows.
    /// </summary>
    /// <remarks>
    /// The entire dictionary is (de)serialized for a single key
    /// read / write. Try to keep the amount of items in protected storage limited.
    /// </remarks>
    public class WindowsProtectedStorage : IProtectedStorage
    {
        private ConcurrentDictionary<string, string> _values;
        private Task<Task> _writeTask;
        private Task<Task> _readTask;

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or empty", nameof(key));
            }

            if (!_values.TryRemove(key, out _))
            {
                return false;
            }

            await SerializeDictionary().ConfigureAwait(false);

            return true;
        }

        /// <inheritdoc/>
        public async Task<TValue> GetAsync<TValue>(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Task<Task> readTask;

            if ((readTask = Interlocked.CompareExchange(ref _readTask, new Task<Task>(Read), null)) == null)
            {
                readTask = _readTask;
                readTask.Start();
            }

            await readTask.Unwrap().ConfigureAwait(false);

            if (_values.TryGetValue(key, out var json))
            {
                return JsonSerializer.Deserialize<TValue>(json);
            }

            return default;
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

            var stringValue = JsonSerializer.Serialize(value, value.GetType());
            _values.AddOrUpdate(key, stringValue, (key, oldValue) => stringValue);

            await SerializeDictionary().ConfigureAwait(false);
        }

        private string ProtectedStorageFileName => $"{GetType().FullName}.json";

        /// <summary>
        /// Asynchronously read the local storage from disk.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private async Task Read()
        {
            try
            {
                using var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
                if (isolatedStorage.FileExists(ProtectedStorageFileName))
                {
                    using var stream = isolatedStorage.OpenFile(ProtectedStorageFileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    using var dataProtectionStream = new DataProtectionStream(stream, System.Security.Cryptography.DataProtectionScope.CurrentUser);
                    _values = await JsonSerializer.DeserializeAsync<ConcurrentDictionary<string, string>>(dataProtectionStream).ConfigureAwait(false);
                    dataProtectionStream.Close();
                    stream.Close();
                }

                if (_values == null)
                {
                    _values = new ConcurrentDictionary<string, string>();
                }
            }
            finally
            {
                _readTask = null;
            }
        }

        /// <summary>
        /// Serializes the dictionary until no more changes were written.
        /// </summary>
        private async Task SerializeDictionary()
        {
            Task<Task> writeTask;
            bool moreData;

            do
            {
                moreData = false;

                if ((writeTask = Interlocked.CompareExchange(ref _writeTask, new Task<Task>(Write), null)) == null)
                {
                    writeTask = _writeTask;
                    writeTask.Start();
                }
                else
                {
                    moreData = true;
                }

                await writeTask.Unwrap().ConfigureAwait(false);
            } while (moreData);
        }

        /// <summary>
        /// Asynchronously write the local storage to disk.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private async Task Write()
        {
            try
            {
                using var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
                using var stream = isolatedStorage.OpenFile(ProtectedStorageFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                using var dataProtectionStream = new DataProtectionStream(stream, System.Security.Cryptography.DataProtectionScope.CurrentUser);
                await JsonSerializer.SerializeAsync(dataProtectionStream, _values).ConfigureAwait(false);
                dataProtectionStream.FlushFinalBlock();
                dataProtectionStream.Close();
                stream.Close();
            }
            finally
            {
                _writeTask = null;
            }
        }
    }
}
