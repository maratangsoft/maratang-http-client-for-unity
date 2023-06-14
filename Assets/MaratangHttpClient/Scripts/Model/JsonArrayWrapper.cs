using System;
using UnityEngine;

namespace MaratangHttp
{
	[Serializable]
	internal class JsonArrayWrapper<T>
	{
		[SerializeField] private T array;

		public T Array { get => array; }
	}
}