//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Collections.Generic;

    public sealed class QueryParameters : RetrievalParameters, IQueryParameters
    {
        public QueryParameters(
            string schemaIdentifier,
            string path,
            string tenantId,
            IFilter filter,
            IReadOnlyCollection<string> requestedAttributePaths,
            IReadOnlyCollection<string> excludedAttributePaths)
            : base(schemaIdentifier, path, requestedAttributePaths, excludedAttributePaths)
        {
            if (null == filter)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            this.TenantId = tenantId;
            this.AlternateFilters = filter.ToCollection();
        }

        public QueryParameters(
            string schemaIdentifier,
            string path,
            string tenantId,
            IReadOnlyCollection<IFilter> alternateFilters,
            IReadOnlyCollection<string> requestedAttributePaths,
            IReadOnlyCollection<string> excludedAttributePaths)
            : base(schemaIdentifier, path, requestedAttributePaths, excludedAttributePaths)
        {
            this.TenantId = tenantId;
            this.AlternateFilters = alternateFilters ?? throw new ArgumentNullException(nameof(alternateFilters));
        }

        public QueryParameters(
            string schemaIdentifier,
            string path,
            string tenantId,
            IPaginationParameters paginationParameters)
            : this(schemaIdentifier, path, tenantId, Array.Empty<IFilter>(), Array.Empty<string>(), Array.Empty<string>())
        {
            this.PaginationParameters = paginationParameters ?? throw new ArgumentNullException(nameof(paginationParameters));
        }

        [Obsolete("Use QueryParameters(string, string, IFilter, IReadOnlyCollection<string>, IReadOnlyCollection<string>) instead")]
        public QueryParameters(
            string schemaIdentifier,
            string tenantId,
            IFilter filter,
            IReadOnlyCollection<string> requestedAttributePaths,
            IReadOnlyCollection<string> excludedAttributePaths)
            : this(
                schemaIdentifier,
                tenantId,
                new SchemaIdentifier(schemaIdentifier).FindPath(),
                filter,
                requestedAttributePaths,
                excludedAttributePaths)
        {
        }

        [Obsolete("Use QueryParameters(string, string, IReadOnlyCollection<IFilter>, IReadOnlyCollection<string>, IReadOnlyCollection<string>) instead")]
        public QueryParameters(
            string schemaIdentifier,
            string tenantId,
            IReadOnlyCollection<IFilter> alternateFilters,
            IReadOnlyCollection<string> requestedAttributePaths,
            IReadOnlyCollection<string> excludedAttributePaths)
            : this(
                schemaIdentifier,
                tenantId,
                new SchemaIdentifier(schemaIdentifier).FindPath(),
                alternateFilters,
                requestedAttributePaths,
                excludedAttributePaths)
        {
        }

        public string TenantId { get; }

        public IReadOnlyCollection<IFilter> AlternateFilters
        {
            get;
            private set;
        }

        public IPaginationParameters PaginationParameters
        {
            get;
            set;
        }

        public override string ToString()
        {
            string result =
                new Query
                {
                    AlternateFilters = this.AlternateFilters,
                    RequestedAttributePaths = this.RequestedAttributePaths,
                    ExcludedAttributePaths = this.ExcludedAttributePaths,
                    PaginationParameters = this.PaginationParameters
                }.Compose();
            return result;
        }
    }
}