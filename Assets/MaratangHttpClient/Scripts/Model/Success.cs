using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MaratangHttp
{
	public class Success<T>
	{
		private Dictionary<string, string> headers;
		private T body;

		public Success(Dictionary<string, string> headers, T body)
		{
			this.headers = headers;
			this.body = body;
		}

		public Dictionary<string, string> Headers { get => headers; }
		public T Body { get => body; }
	}
}