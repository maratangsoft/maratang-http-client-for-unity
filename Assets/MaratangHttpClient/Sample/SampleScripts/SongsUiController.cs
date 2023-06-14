using UnityEngine;
using MaratangHttp;
using UnityEngine.Networking;
using System.Collections.Generic;

public class SongsUiController : MonoBehaviour
{
    public GameObject verticalLayout;
    public GameObject itemView;

    MaratangHttpClient httpClient = null;

	// API 스펙: https://pureani.tistory.com/4997
	string baseUrl = "https://api.manana.kr/";
    string path = "karaoke/tj.json";


	private void Start()
    {
        // 클라이언트 초기화
        httpClient = MaratangHttpClient.GetInstance(this);
    }

    public void OnFetchButtonClicked()
    {
        // GET 통신을 위한 request 객체 만들기
        UnityWebRequest request = new RequestBuilder(baseUrl)
            .AddPath(path)
            .BuildGET();

        // HTTP통신으로 JSON 데이터를 받아 <T> 타입으로 역직렬화해주는 기능
        // 통신 상태에 따라 success, failure, error 3가지 콜백으로 분기
        httpClient.SendRequestForJson<Song[]>(request, success => {
            Song[] downloadedSongs = success.Body;
            UpdateUi(downloadedSongs);

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