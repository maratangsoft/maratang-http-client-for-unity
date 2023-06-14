using UnityEngine;
using MaratangHttp;
using UnityEngine.Networking;
using System.Collections.Generic;

public class SongsUiController : MonoBehaviour
{
    public GameObject verticalLayout;
    public GameObject itemView;

    MaratangHttpClient httpClient = null;

	// API ����: https://pureani.tistory.com/4997
	string baseUrl = "https://api.manana.kr/";
    string path = "karaoke/tj.json";


	private void Start()
    {
        // Ŭ���̾�Ʈ �ʱ�ȭ
        httpClient = MaratangHttpClient.GetInstance(this);
    }

    // 1. Coroutine ���
    // SendRequestForJson()
    public void OnFetchButtonClicked()
    {
        Debug.Log("Clicked");
        // GET ����� ���� request ��ü �����
        UnityWebRequest request = new RequestBuilder(baseUrl)
            .AddPath(path)
            .BuildGET();

        // HTTP������� JSON �����͸� �޾� <T> Ÿ������ ������ȭ���ִ� ���
        // ��� ���¿� ���� success, failure, error 3���� �ݹ����� �б�
        httpClient.SendRequestForJson<Song[]>(request, success => {
            Song[] downloadedSongs = success.Body;
            UpdateUi(downloadedSongs);

            // ����� Ȯ���� ���� �α�
            foreach (KeyValuePair<string, string> kvp in success.Headers)
            {
                Debug.Log(kvp.Key + ": " + kvp.Value);
            }

        }, failure => { 
            Debug.Log(failure.Code); 
            Debug.Log(failure.Message); 
        }, error => { 
            Debug.Log(error); 
        });
	}

    // 2. async / await ���
    // SendRequestForJsonAsync()
    public void OnFetchAsyncButtonClicked()
	{
		Debug.Log("Clicked");
		UnityWebRequest request = new RequestBuilder(baseUrl)
			.AddPath(path)
			.BuildGET();

		httpClient.SendRequestForJsonAsync<Song[]>(request, success => {
			Song[] downloadedSongs = success.Body;
			UpdateUi(downloadedSongs);

			foreach (KeyValuePair<string, string> kvp in success.Headers)
			{
				Debug.Log(kvp.Key + ": " + kvp.Value);
			}

		}, failure => {
			Debug.Log(failure.Code);
			Debug.Log(failure.Message);
		}, error => {
			Debug.Log(error);
		});
	}

    private void UpdateUi(Song[] newSongs)
    {
        foreach (Transform child in verticalLayout.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Song song in newSongs) 
        {
            GameObject go = Instantiate(itemView, verticalLayout.transform);
            go.GetComponent<ItemView>().Bind(song);
        }
    }
}