// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    internal class ScopeConverter : JsonConverter<IList<string>>
    {
        public override IList<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString().Split(' ').ToList();
        }

        public override void Write(Utf8JsonWriter writer, IList<string> value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(string.Join(" ", value));
        }
    }
}
