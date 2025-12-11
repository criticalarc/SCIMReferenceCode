//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

    internal class JsonSerializer : IJsonSerializable
    {
        public static DataContractJsonSerializerSettings GetDataContractJsonSerializerSettings() 
            => new ()
        {
            EmitTypeInformation = EmitTypeInformation.Never,
            // SCIM uses the ISO 8601 which is the round trip - https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#Roundtrip
            DateTimeFormat = new DateTimeFormat("O")
        };

        private static readonly Lazy<DataContractJsonSerializerSettings> SerializerSettings =
            new Lazy<DataContractJsonSerializerSettings>(GetDataContractJsonSerializerSettings);

        private readonly object dataContractValue;

        public JsonSerializer(object dataContract)
        {
            this.dataContractValue = dataContract ??
                throw new ArgumentNullException(nameof(dataContract));
        }

        public string Serialize()
        {
            IDictionary<string, object> json = this.ToJson();
            string result = JsonFactory.Instance.Create(json, true);
            return result;
        }

        public Dictionary<string, object> ToJson()
        {
            Type type = this.dataContractValue.GetType();
            DataContractJsonSerializer serializer =
                new DataContractJsonSerializer(type, JsonSerializer.SerializerSettings.Value);

            string json;
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream();
                serializer.WriteObject(stream, this.dataContractValue);
                stream.Position = 0;
                StreamReader streamReader = null;
                try
                {
                    streamReader = new StreamReader(stream);
                    stream = null;
                    json = streamReader.ReadToEnd();
                }
                finally
                {
                    if (streamReader != null)
                    {
                        streamReader.Close();
                        streamReader = null;
                    }
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }
            }

            Dictionary<string, object> result = JsonFactory.Instance.Create(json, true);
            return result;
        }
    }
}