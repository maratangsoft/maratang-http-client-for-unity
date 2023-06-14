using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Networking;

namespace MaratangHttp
{
	public class MaratangHttpClient
	{
		private static MonoBehaviour _monobehaviour;

		private MaratangHttpClient(MonoBehaviour monobehaviour)
		{
			_monobehaviour = monobehaviour;
		}

		private static MaratangHttpClient _instance = null;

		public static MaratangHttpClient GetInstance(MonoBehaviour monoBehaviour)
		{
			if (_instance == null) _instance = new MaratangHttpClient(monoBehaviour);
			else _monobehaviour = monoBehaviour;

			return _instance;
		}

		public void SendRequestForString(UnityWebRequest request,
						 Action<Success<string>> onSuccess,
						 Action<Failure> onFailure,
						 Action<string> onError)
		{
			_monobehaviour.StartCoroutine(
				ResponseString(request, onSuccess, onFailure, onError)
			);
		}

		IEnumerator ResponseString(UnityWebRequest request,
									 Action<Success<string>> onSuccess,
									 Action<Failure> onFailure,
									 Action<string> onError)
		{
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.Success)
			{
				Dictionary<string, string> headers = request.GetResponseHeaders();
				string body = request.downloadHandler.text;

				Success<string> success = new Success<string>(headers, body);
				onSuccess.Invoke(success);
			}
			else if (request.result == UnityWebRequest.Result.ConnectionError)
			{
				Failure failure = Failure.Create(request);
				onFailure.Invoke(failure);
			}
			else
			{
				string error = request.error;
				onError.Invoke(error);
			}
		}

		public void SendRequestForJson<T>(UnityWebRequest request,
							Action<Success<T>> onSuccess,
							Action<Failure> onFailure,
							Action<string> onError)
		{
			_monobehaviour.StartCoroutine(
				RequestGetJson(request, onSuccess, onFailure, onError)
			);
		}

		IEnumerator RequestGetJson<T>(UnityWebRequest request,
									  Action<Success<T>> onSuccess,
									  Action<Failure> onFailure,
									  Action<string> onError)
		{
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.Success)
			{
				try
				{
					Dictionary<string, string> headers = request.GetResponseHeaders();

					string originalJson = request.downloadHandler.text;
					T body = default;

					// if the JSON is a nameless array
					if (originalJson.StartsWith("["))
					{
						string wrappedJson = "{\"array\":" + originalJson + "}";
						Debug.Log("wrappedJson: " + wrappedJson);

						JsonArrayWrapper<T> wrappedObject = 
							JsonUtility.FromJson<JsonArrayWrapper<T>>(wrappedJson);

						body = wrappedObject.Array;
					}
					else
					{
						body = JsonUtility.FromJson<T>(originalJson);
					}

					Success<T> success = new Success<T>(headers, body);
					onSuccess.Invoke(success);
				}
				catch (Exception e)
				{
					onError.Invoke(e.Message);
				}
			}
			else if (request.result == UnityWebRequest.Result.ConnectionError)
			{
				Failure failure = Failure.Create(request);
				onFailure.Invoke(failure);
			}
			else
			{
				onError.Invoke(request.error);
			}
		}

		public void SendRequestForTexture(UnityWebRequest request,
						 Action<Success<Texture2D>> onSuccess,
						 Action<Failure> onFailure,
						 Action<string> onError)
		{
			_monobehaviour.StartCoroutine(
				RequestTexture(request, onSuccess, onFailure, onError)
			);
		}

		IEnumerator RequestTexture(UnityWebRequest request,
								   Action<Success<Texture2D>> onSuccess,
								   Action<Failure> onFailure,
								   Action<string> onError)
		{
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.Success)
			{
				Dictionary<string, string> headers = request.GetResponseHeaders();

				Texture2D body =
						((DownloadHandlerTexture)request.downloadHandler).texture;

				Success<Texture2D> success = new Success<Texture2D>(headers, body);
				onSuccess.Invoke(success);
			}
			else if (request.result == UnityWebRequest.Result.ConnectionError)
			{
				Failure failure = Failure.Create(request);
				onFailure.Invoke(failure);
			}
			else
			{
				onError.Invoke(request.error);
			}
		}
	}
}