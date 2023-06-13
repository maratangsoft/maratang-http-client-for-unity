using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace MaratangHttp
{
	public class RequestBuilder
	{
		private StringBuilder urlBuilder;
		private WWWForm postData;
		private Dictionary<string, string> headers;

		private int parameterCount = 0;

		public RequestBuilder(string baseUrl)
		{
			urlBuilder = new StringBuilder(baseUrl);
			postData = new WWWForm();
			headers = new Dictionary<string, string>();
		}

		public RequestBuilder SetHeader(string key, string value)
		{
			headers.Add(key, value);
			return this;
		}
		public RequestBuilder AddUrlParameter(string key, string value)
		{
			if (parameterCount == 0) urlBuilder.Append("?");
			else urlBuilder.Append("&");

			urlBuilder.Append(key).Append("=").Append(value);

			return this;
		}
		public RequestBuilder AddPostData(string key, string value)
		{
			postData.AddField(key, value);
			return this;
		}

		public UnityWebRequest BuildGET()
		{
			string url = urlBuilder.ToString();
			UnityWebRequest request = UnityWebRequest.Get(url);

			foreach (KeyValuePair<string, string> kvp in headers)
			{
				request.SetRequestHeader(kvp.Key, kvp.Value);
			}

			return request;
		}

		public UnityWebRequest BuildPOST()
		{
			string url = urlBuilder.ToString();
			UnityWebRequest request = UnityWebRequest.Post(url, postData);

			foreach (KeyValuePair<string, string> kvp in headers)
			{
				request.SetRequestHeader(kvp.Key, kvp.Value);
			}

			return request;
		}
	}
}
