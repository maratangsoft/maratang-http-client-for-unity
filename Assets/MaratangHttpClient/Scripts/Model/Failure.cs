using System.Collections.Generic;
using UnityEngine.Networking;

namespace MaratangHttp
{
	public class Failure
	{
		private Dictionary<string, string> headers;
		private int code;
		private string message;

		public Failure(Dictionary<string, string> headers, int code, string message)
		{
			this.headers = headers;
			this.code = code;
			this.message = message;
		}

		public Dictionary<string, string> Headers { get => headers; }
		public int Code { get => code; }
		public string Message { get => message; }

		public static Failure Create(UnityWebRequest request)
		{
			Dictionary<string, string> headers = request.GetResponseHeaders();
			int code = (int)request.responseCode;
			string message = request.error;

			return new Failure(headers, code, message);
		}
	}
}
