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
		// Ŭ���̾�Ʈ �ʱ�ȭ
		client = MaratangHttpClient.GetInstance(this);
    }

	// 1. Coroutine ���
	// SendRequestForTexture()
	public void OnFetchButtonClicked()
    {
        // GET ������� �̹����� �ٿ�ޱ� ���� request ��ü ����� 
        UnityWebRequest request = new RequestBuilder(baseUrl)
            .AddPath(path)
            .BuildTexture();

		// HTTP������� �̹��� �����͸� �޾� Texture2D Ÿ������ ������ȭ���ִ� ���
		// ��� ���¿� ���� success, failure, error 3���� �ݹ����� �б�
		client.SendRequestForTexture(request, success => {
            Texture2D downloadedPicture = success.Body;
            rawImage.texture = downloadedPicture;

            // ����� Ȯ���� ���� �α�
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

	// 2. async / await ���
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
