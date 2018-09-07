using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptdrive
{
    class RestManager
    {
        public AccountInfo Account_Info()
        {
            var restClient = new RestClient("http://api.dropbox.com");
            OAuthBase oAuth = new OAuthBase();
            string nonce = oAuth.GenerateNonce();
            string timeStamp = oAuth.GenerateTimeStamp();
            string normalizedUrl;
            string normalizedRequestParameters;
            string sig = oAuth.GenerateSignature(new Uri(string.Format("{0}/{1}/account/info", restClient.BaseUrl, _version)),
                _apiKey, _appsecret,
                _userLogin.Token, _userLogin.Secret,
                "GET", timeStamp, nonce, out normalizedUrl, out normalizedRequestParameters);
            sig = HttpUtility.UrlEncode(sig);

            var request = new RestRequest(Method.GET);
            request.Resource = string.Format("{0}/account/info", _version);
            request.AddParameter("oauth_consumer_key", _apiKey);
            request.AddParameter("oauth_token", _userLogin.Token);
            request.AddParameter("oauth_nonce", nonce);
            request.AddParameter("oauth_timestamp", timeStamp);
            request.AddParameter("oauth_signature_method", "HMAC-SHA1");
            request.AddParameter("oauth_version", "1.0");
            request.AddParameter("oauth_signature", sig);

            var response = restClient.Execute<AccountInfo>(request);

            return response.Data;
        }
    }
}
