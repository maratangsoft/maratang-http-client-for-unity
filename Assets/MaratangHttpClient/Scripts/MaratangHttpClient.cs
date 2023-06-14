using System;
using UnityEngine;
using UnityEngine.Networking;

namespace MaratangHttp
{
	public class MaratangHttpClient
	{
		private static MonoBehaviour _monobehaviour;

		private CoroutineOperations coroutine = new CoroutineOperations();
		private AsyncOperations async = new AsyncOperations();

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
				coroutine.SendRequestForString(request, onSuccess, onFailure, onError)
			);
		}

		public void SendRequestForStringAsync(UnityWebRequest request,
											  Action<Success<string>> onSuccess,
											  Action<Failure> onFailure,
											  Action<string> onError)
		{
			_ = async.SendRequestForString(request, onSuccess, onFailure, onError);
		}

		public void SendRequestForJson<T>(UnityWebRequest request,
										  Action<Success<T>> onSuccess,
										  Action<Failure> onFailure,
										  Action<string> onError)
		{
			_monobehaviour.StartCoroutine(
				coroutine.SendRequestForJson(request, onSuccess, onFailure, onError)
			);
		}

		public void SendRequestForJsonAsync<T>(UnityWebRequest request,
											   Action<Success<T>> onSuccess,
											   Action<Failure> onFailure,
											   Action<string> onError)
		{
			_ = async.SendRequestForJson(request, onSuccess, onFailure, onError);
		}

		public void SendRequestForTexture(UnityWebRequest request,
										  Action<Success<Texture2D>> onSuccess,
										  Action<Failure> onFailure,
										  Action<string> onError)
		{
			_monobehaviour.StartCoroutine(
				coroutine.SendRequestForTexture(request, onSuccess, onFailure, onError)
			);
		}

		public void SendRequestForTextureAsync(UnityWebRequest request,
											   Action<Success<Texture2D>> onSuccess,
											   Action<Failure> onFailure,
											   Action<string> onError)
		{
			_ = async.SendRequestForTexture(request, onSuccess, onFailure, onError);
		}
	}
}