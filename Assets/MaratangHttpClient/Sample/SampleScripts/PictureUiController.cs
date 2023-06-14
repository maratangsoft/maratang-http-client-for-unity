using UnityEngine;
using UnityEngine.UI;
using MaratangHttp;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PictureUiController : MonoBehaviour
{
    public RawImage rawImage;
    MaratangHttpClient client;
    string baseUrl = "https://cdn.pixabay.com/";
    string path = "photo/2023/05/05/11/07/sweet-7972193_1280.jpg";

    void Start()
    {
		// 클라이언트 초기화
		client = MaratangHttpClient.GetInstance(this);
    }

	// 1. Coroutine 사용
	// SendRequestForTexture()
	public void OnFetchButtonClicked()
    {
        // GET 방식으로 이미지를 다운받기 위한 request 객체 만들기 
        UnityWebRequest request = new RequestBuilder(baseUrl)
            .AddPath(path)
            .BuildTexture();

		// HTTP통신으로 이미지 데이터를 받아 Texture2D 타입으로 역직렬화해주는 기능
		// 통신 상태에 따라 success, failure, error 3가지 콜백으로 분기
		client.SendRequestForTexture(request, success => {
            Texture2D downloadedPicture = success.Body;
            rawImage.texture = downloadedPicture;

            // 헤더값 확인을 위한 로그
			foreach (KeyValuePair<string, string> kvp in success.Headers)
			{
				Debug.Log(kvp.Key + ": " + kvp.Value);
			}

		}, failure => {
			Debug.Log(failure);
		}, error => {
			Debug.Log(error);
		});
    }

	// 2. async / await 사용
	// SendRequestForTextureAsync()
	public void OnFetchAsyncButtonClicked()
	{
		UnityWebRequest request = new RequestBuilder(baseUrl)
			.AddPath(path)
			.BuildTexture();

		client.SendRequestForTextureAsync(request, success => {
			Texture2D downloadedPicture = success.Body;
			rawImage.texture = downloadedPicture;

			foreach (KeyValuePair<string, string> kvp in success.Headers)
			{
				Debug.Log(kvp.Key + ": " + kvp.Value);
			}

		}, failure => {
			Debug.Log(failure);
		}, error => {
			Debug.Log(error);
		});
	}

	public void OnClearButtonClicked()
    {
        rawImage.texture = null;
    }
}
