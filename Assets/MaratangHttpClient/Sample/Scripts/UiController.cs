using UnityEngine;
using MaratangHttp;

public class UiController : MonoBehaviour
{
    public GameObject verticalLayout;
    public GameObject itemView;

    MaratangHttpClient httpClient = null;

	// example API documentation: https://pureani.tistory.com/4997
    // There's no need to have an authorization key to use this API.
	string url = "https://api.manana.kr/karaoke/tj.json";

    private void Start()
    {
        httpClient = MaratangHttpClient.GetInstance(this);
    }

    public void FetchNewSongs()
    {
        // 불러오려는 데이터가 JSON 배열입니다.
        // 유니티 JsonUtility는 JSON 배열을 파싱하지 못하므로
        // 리턴받으려는 데이터 타입(배열 안에 있는 객체)을 JsonArrayWrapper로 감싸서 넘겨주세요.
        httpClient.GetJsonObject<Song[]>(url, success => {
            Debug.Log(success.Headers.ToString());
            UpdateUi(success.Body);

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