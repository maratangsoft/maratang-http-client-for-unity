using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

namespace MaratangHttp
{
	internal class UnityWebRequestAwaiter : INotifyCompletion
	{
		private UnityWebRequestAsyncOperation asyncOperation;
		private Action continuation;

		internal UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOperation)
		{
			this.asyncOperation = asyncOperation;
			asyncOperation.completed += OnRequestCompleted;
		}

		internal bool IsCompleted { get { return asyncOperation.isDone; } }

		internal void GetResult() { }

		public void OnCompleted(Action continuation) 
		{
			Debug.Log("OnCompleted");
			this.continuation = continuation;
		}

		private void OnRequestCompleted(AsyncOperation obj)
		{
			Debug.Log("OnRequestCompleted");
			continuation.Invoke();
		}
	}

	internal static class ExtensionMethods
	{
		internal static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOperation)
		{
			return new UnityWebRequestAwaiter(asyncOperation);
		}
	}
}
