//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

using System;

namespace Microsoft.SCIM
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class TrustedJsonFactory : JsonFactory
    {
        /// <summary>
        ///     The default DateParseHandling results in a non-JSON compliant string as the date, DateParseHandling.None forces the
        ///     correct JSON date format.
        /// </summary>
        private static readonly JsonSerializerSettings JsonSerializerSettings = new() { DateParseHandling = DateParseHandling.None };

        public override Dictionary<string, object> Create(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(json, JsonSerializerSettings);
            }
            catch (InvalidCastException)
            {
                var result =
                    (Dictionary<string, object>)JsonConvert.DeserializeObject(
                        json, JsonSerializerSettings);
                return result;
            }
        }

        public override string Create(string[] json)
        {
            string result = JsonConvert.SerializeObject(json, JsonSerializerSettings);
            return result;
        }

        public override string Create(Dictionary<string, object> json)
        {
            string result = JsonConvert.SerializeObject(json, JsonSerializerSettings);
            return result;
        }

        public override string Create(IDictionary<string, object> json)
        {
            string result = JsonConvert.SerializeObject(json, JsonSerializerSettings);
            return result;
        }

        public override string Create(IReadOnlyDictionary<string, object> json)
        {
            string result = JsonConvert.SerializeObject(json, JsonSerializerSettings);
            return result;
        }
    }
}
