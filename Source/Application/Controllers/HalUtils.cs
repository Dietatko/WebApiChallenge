using System;
using Halcyon.HAL;

namespace CheckoutChallenge.Application.Controllers
{
    internal static class HalUtils
    {
        public const string SelfRel = "self";
        public const string CuriesRel = "curies";
        public const string ItemRel = "item";

        private const string CheckoutCuriesPrefix = "checkout";
        private const string CheckoutRelsPrefix = "http://schemas.checkout.com/docs/rels/";
        private const string CheckoutRelsTemplate = CheckoutRelsPrefix + "{rel}";

        public static HALResponse AddSelfLink(this HALResponse halResponse, Uri selfUrl)
        {
            return halResponse.AddLinks(new Link(SelfRel, selfUrl.ToString()));
        }

        public static HALResponse AddCurieLink(this HALResponse halResponse, string prefix, string template)
        {
            return halResponse.AddLinks(new Link(CuriesRel, template, replaceParameters: false, isRelArray: true)
            {
                Name = prefix
            });
        }

        public static HALResponse AddCheckoutLink(this HALResponse halResponse, string rel, Uri url)
        {
            rel = rel.Trim();
            if (rel.StartsWith(CheckoutRelsPrefix))
            {
                // Needs to be improved if multiple curies are needed
                if (!halResponse.HasLink(CuriesRel))
                    halResponse.AddCurieLink(CheckoutCuriesPrefix, CheckoutRelsTemplate);

                rel = $"{CheckoutCuriesPrefix}:{rel.Substring(CheckoutRelsPrefix.Length)}";
            }

            return halResponse.AddLinks(new Link(rel, url.ToString()));
        }

        public static HALResponse AddCheckoutEmbeddedCollectionLink(this HALResponse halResponse, string rel, Uri url)
        {
            rel = rel.Trim();
            if (rel.StartsWith(CheckoutRelsPrefix))
            {
                // Needs to be improved if multiple curies are needed
                if (!halResponse.HasLink(CuriesRel))
                    halResponse.AddCurieLink(CheckoutCuriesPrefix, CheckoutRelsTemplate);

                rel = $"{CheckoutCuriesPrefix}:{rel.Substring(CheckoutRelsPrefix.Length)}";
            }

            return halResponse.AddLinks(new Link(rel, url.ToString()));
        }
    }
}
