유니티에서의 HTTP통신을 연습해 보고자 만들었습니다.
만드는 김에 에셋화시켜서 다른 프로젝트에서도 쓸 수 있도록 해봤습니다.

## Features
### Request
* GET method
* POST method 준비중

### Response
* 성공시, 네트워크 장애시, 기타 에러시 3가지로 분기

### Deserialization
* to raw string
* JSON object to custom class
* JSON array to custom class
* to Texture2D

## Tech Stack
* UnityWebRequest
* Unity Coroutine
* Unity JsonUtility

## Instructions
* 클라이언트 초기화
```csharp
MaratangHttpClient httpClient = 
    MaratangHttpClient.GetInstance(monoBehaviour);
```

* GET request + string response
```csharp
httpClient.GetString(url, success => {
    // Dictionary<string, string> Success.Headers
    // string Success.Body
    
}, failure => { 
    // Dictionary<string, string> Failure.Headers
    // string failure.Code
    // string failure.Message

}, error => { 
    // string error
});
```

* GET request + JSON object response
```csharp
// T: 역직렬화할 클래스
httpClient.GetJson<T>(url, success => {
    // Dictionary<string, string> Success.Headers
    // T Success.Body

}, failure => { 
    // Dictionary<string, string> Failure.Headers
    // string failure.Code
    // string failure.Message

}, error => { 
    // string error
});
```

* GET request + JSON array response
```csharp
// T: 역직렬화할 클래스
httpClient.GetJson<T[]>>(url, success => {
    // Dictionary<string, string> success.Headers
    foreach(T item in success.Body){
        // do something with data
    }
    
}, failure => { 
    // Dictionary<string, string> failure.Headers
    // string failure.Code
    // string failure.Message

}, error => { 
    // string error
});
```

* GET request + Texture2D response
```csharp
httpClient.GetTexture(url, success => {
    // Dictionary<string, string> Success.Headers
    // Texture2D Success.Body
    
}, failure => { 
    // Dictionary<string, string> failure.Headers
    // string failure.Code
    // string failure.Message

}, error => { 
    // string error
});
```

## Sample
GET request + JSON array response
웹 API에서 노래방 신곡 리스트를 받아와 UI에 표시하는 예제입니다.

[API 정보](https://pureani.tistory.com/4997)