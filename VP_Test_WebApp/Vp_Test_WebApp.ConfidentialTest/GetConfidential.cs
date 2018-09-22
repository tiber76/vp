using System;
using System.Collections.Generic;
using System.Text;
using Vp_Test_WebApp.ConfidentialTest.AWS_Tools.Signers;
using Vp_Test_WebApp.ConfidentialTest.AWS_Tools.Util;

namespace Vp_Test_WebApp.ConfidentialTest
{
    public class GetConfidential
    {
        /// <summary>
        /// Request the content of an object from the specified bucket using virtual hosted-style 
        /// object addressing.
        /// </summary>
        public static void Run(string objectKey, string email, string awsAccessKey, string awsSecretKey)
        {
            Console.WriteLine("GetConfidential");

            var endpointUri = $"https://localhost:44397/{objectKey}/{email}";

            var uri = new Uri(endpointUri);

            // for a simple GET, we have no body so supply the precomputed 'empty' hash
            var headers = new Dictionary<string, string>
            {
                {AWS4SignerBase.X_Amz_Content_SHA256, AWS4SignerBase.EMPTY_BODY_SHA256},
                {"content-type", "text/plain"}
            };

            var signer = new AWS4SignerForAuthorizationHeader
            {
                EndpointUri = uri,
                HttpMethod = "GET",
                Service = "",
                Region = ""
            };

            var authorization = signer.ComputeSignature(headers,
                                                        "",   // no query parameters
                                                        AWS4SignerBase.EMPTY_BODY_SHA256,
                                                        awsAccessKey,
                                                        awsSecretKey);

            // place the computed signature into a formatted 'Authorization' header 
            // and call S3
            headers.Add("Authorization", authorization);

            HttpHelpers.InvokeHttpRequest(uri, "GET", headers, null);
        }
    }
}
