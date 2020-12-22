// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// Equality comparer for a claims identity.
    /// </summary>
    internal class ClaimsPrincipalEqualityComparer : IEqualityComparer<ClaimsPrincipal>
    {
        /// <inheritdoc />
        public bool Equals(ClaimsPrincipal x, ClaimsPrincipal y)
        {
            if (x.Identity == null && y.Identity != null ||
                y.Identity != null && y.Identity == null)
            {
                return false;
            }

            if (x.Identity == null && y.Identity == null)
            {
                return true;
            }

            if (x.Identity.Name != y.Identity.Name)
            {
                return false;
            }

            if (x.Claims.Count() != y.Claims.Count())
            {
                return false;
            }

            var enumerator = y.Claims.GetEnumerator();

            foreach (var claimX in x.Claims)
            {
                enumerator.MoveNext();
                var claimY = enumerator.Current;

                if (!ClaimEquals(claimX, claimY))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool ClaimEquals(Claim x, Claim y)
        {
            return x.Type == y.Type &&
                x.Value == y.Value &&
                x.Issuer == y.Issuer &&
                x.OriginalIssuer == y.OriginalIssuer &&
                x.ValueType == y.ValueType;
        }

        /// <inheritdoc />
        public int GetHashCode(ClaimsPrincipal obj)
        {
            var claimsHashcode = 0;

            foreach (var claim in obj.Claims)
            {
                claimsHashcode = HashCode.Combine(claimsHashcode, GetClaimHashCode(claim));
            }

            return HashCode.Combine(obj.Identity?.Name, claimsHashcode);
        }

        private static int GetClaimHashCode(Claim obj)
        {
            return HashCode.Combine(obj.Type, obj.Value, obj.Issuer, obj.OriginalIssuer, obj.ValueType);
        }
    }
}
