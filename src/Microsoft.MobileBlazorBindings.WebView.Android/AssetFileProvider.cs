// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Android.Content.Res;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Microsoft.MobileBlazorBindings.WebView.Android
{
    /// <summary>
    /// File provider that extracts the ZIP file inside the assets and then uses a regular file provider to provide the contents.
    /// </summary>
    public sealed class AssetFileProvider : IFileProvider, IDisposable
    {
        private readonly AssetManager _assetManager;

        private readonly string _extractionPath;

        private readonly PhysicalFileProvider _physicalFileProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetFileProvider"/> class.
        /// </summary>
        /// <param name="assetManager">The Android assets manager to get the zip file from.</param>
        /// <param name="contentRoot">The content root.</param>
        public AssetFileProvider(AssetManager assetManager, string contentRoot)
        {
            _assetManager = assetManager ?? throw new ArgumentNullException(nameof(assetManager));
            _extractionPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), GetType().Name, contentRoot);
            ExtractContents();

            // Create a physical file provider for the Extraction path.
            if (Directory.Exists(_extractionPath))
            {
                _physicalFileProvider = new PhysicalFileProvider(_extractionPath);
            }

        }

        private void ExtractContents()
        {
            if (!_assetManager.List(string.Empty).Contains("wwwroot.zip", StringComparer.Ordinal))
            {
                return;
            }

            var crc32 = new Crc32();

            if (!Directory.Exists(_extractionPath))
            {
                Directory.CreateDirectory(_extractionPath);
            }

            var toRemove = Directory.GetFiles(_extractionPath, "*", SearchOption.AllDirectories).ToHashSet();

            using (var asset = _assetManager.Open("wwwroot.zip"))
            {
                using var zipFile = new ZipArchive(asset);
                foreach (var entry in zipFile.Entries)
                {
                    var destination = new FileInfo(Path.Combine(_extractionPath, entry.FullName));
                    var directory = new DirectoryInfo(Path.GetDirectoryName(destination.FullName));

                    toRemove.Remove(destination.FullName);
                    toRemove.Remove(directory.FullName);

                    if (!directory.Exists)
                    {
                        directory.Create();
                    }

                    if (destination.Exists)
                    {
                        uint destinationCrc = 0;

                        using (var file = destination.OpenRead())
                        {
                            var buffer = new byte[destination.Length];
                            file.Read(buffer, 0, (int)destination.Length);

                            destinationCrc = crc32.ComputeChecksum(buffer);
                        }

                        if (destinationCrc == entry.Crc32)
                        {
                            // skip entry, it's equal.
                            continue;
                        }
                    }

                    if (!destination.FullName.EndsWith("/", StringComparison.Ordinal))
                    {
                        // When overwriting a file, ensure its contents are reset by specifying FileMode.Create
                        using var outputStream = new FileStream(destination.FullName, FileMode.Create, FileAccess.Write, FileShare.None);
                        using var inputStream = entry.Open();

                        inputStream.CopyTo(outputStream);
                    }
                }
            }

            if (toRemove.Any())
            {
                // Remove stale files.
                foreach (var fullName in toRemove)
                {
                    File.Delete(fullName);
                }

                // Remove empty directories.
                RecursiveDeleteEmpty(_extractionPath);
            }
        }
        private static void RecursiveDeleteEmpty(string startLocation)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                RecursiveDeleteEmpty(directory);
                if (!Directory.GetFiles(directory).Any() &&
                    !Directory.GetDirectories(directory).Any())
                {
                    Directory.Delete(directory, false);
                }
            }
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return _physicalFileProvider?.GetDirectoryContents(subpath);
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return _physicalFileProvider?.GetFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return _physicalFileProvider?.Watch(filter);
        }

        public void Dispose()
        {
            this._physicalFileProvider?.Dispose();
        }

        /// <summary>
        /// Crc32 implementation to check for equal files.
        /// </summary>
        private class Crc32
        {
            /// <summary>
            /// lookup table with memoization per byte.
            /// </summary>
            readonly uint[] _memoizationTable;

            /// <summary>
            /// Computes the Checksum of an array of bytes.
            /// </summary>
            /// <param name="bytes">The bytes.</param>
            /// <returns>The checksum.</returns>
            public uint ComputeChecksum(byte[] bytes)
            {
                uint crc = 0xffffffff;
                for (int i = 0; i < bytes.Length; ++i)
                {
                    byte index = (byte)(((crc) & 0xff) ^ bytes[i]);
                    crc = (crc >> 8) ^ _memoizationTable[index];
                }
                return ~crc;
            }

            /// <summary>
            /// Initializes a new Instance of the <see cref="Crc32"/> class.
            /// </summary>
            public Crc32()
            {
                uint poly = 0xedb88320;
                _memoizationTable = new uint[256];
                for (uint i = 0; i < _memoizationTable.Length; ++i)
                {
                    var temp = i;
                    for (int j = 8; j > 0; --j)
                    {
                        if ((temp & 1) == 1)
                        {
                            temp = (temp >> 1) ^ poly;
                        }
                        else
                        {
                            temp >>= 1;
                        }
                    }
                    _memoizationTable[i] = temp;
                }
            }
        }
    }
}
