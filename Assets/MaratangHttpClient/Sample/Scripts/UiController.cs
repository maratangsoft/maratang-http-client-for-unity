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
        // �ҷ������� �����Ͱ� JSON �迭�Դϴ�.
        // ����Ƽ JsonUtility�� JSON �迭�� �Ľ����� ���ϹǷ�
        // ���Ϲ������� ������ Ÿ��(�迭 �ȿ� �ִ� ��ü)�� JsonArrayWrapper�� ���μ� �Ѱ��ּ���.
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