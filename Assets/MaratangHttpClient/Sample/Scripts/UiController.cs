using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text idText;
    public Text nameText;
    public Text emailText;
    public RawImage profileImage;

    MaratangHttpClient httpClient = null;
    string testUrl = "https://httpbin.org/anything?name=aaa";
    string userInfoUrl = "https://httpbin.org/anything?name=aaa";

    private void Start()
    {
        httpClient = new MaratangHttpClient(this);
        ConnectionTest();
    }

    private void ConnectionTest()
    {
        httpClient.GetString(testUrl, success => {
            Debug.Log(success);
        }, failure => {
            Debug.Log(failure);
        }, error => {
            Debug.Log(error);
        });
    }

    public void FetchUserInfo()
    {
        httpClient.GetJsonObject<UserInfo>(userInfoUrl, success => {
            FetchProfileImage(success.ProfileImageUrl, success);

        }, failure => { 
            Debug.Log(failure); 
        }, error => { 
            Debug.Log(error); 
        });
    }

    private void FetchProfileImage(string imageUrl, UserInfo userInfo)
    {
        httpClient.GetTexture(imageUrl, success => {
            UpdateUi(userInfo, success);

        }, failure => { 
            Debug.Log(failure); 
        }, error => { 
            Debug.Log(error); 
        });
    }

    private void UpdateUi(UserInfo userInfo, Texture2D userTexture)
    {
        idText.text = userInfo.Id.ToString();
        nameText.text = userInfo.Name;
        emailText.text = userInfo.Email;
        profileImage.texture = userTexture;
    }
}