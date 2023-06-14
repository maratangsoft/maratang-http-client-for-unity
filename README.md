===========================
not properly working yet...
===========================

유니티에서의 간단한 HTTP통신을 연습해 보고자 만들었습니다.
UnityWebRequest를 기반으로 각종 편의 기능들을 추가했습니다.
만드는 김에 에셋화시켜서 다른 프로젝트에서도 쓸 수 있도록 해봤습니다.
**GET, POST 방식만 지원합니다.**
**파일 송신은 지원하지 않습니다.**

## Features
### RequestBuilder
* UnityWebRequest 객체를 빌더 패턴으로 쉽게 생성할 수 있습니다.

- AddHeader() : 커스텀 헤더 입력
- AddPath() : 서버 주소 (base URL) 뒤에 붙을 경로 입력
- AddUrlParameter() : GET방식 사용시 URL 뒤에 붙을 파라미터 입력
- AddPostData() : POST방식 사용시 키-값 데이터 입력
- BuildGET() : GET방식의 UnityWebRequest 객체를 리턴합니다.
- BuildPOST() : POST방식의 UnityWebRequest 객체를 리턴합니다.
- BuileTexture() : GET방식으로 Texture2D 파일을 받아오기 위한 UnityWebRequest 객체를 리턴합니다.

### MaratangHttpClient
* HTTP통신을 실행하고 응답을 받아오는 싱글턴 객체입니다.
* 내부에서 코루틴으로 실행되므로 따로 비동기 처리를 하지 않아도 됩니다.
* 응답을 받아오는 콜백은 통신 상태에 따라 success(성공시), failure(네트워크 장애시), error(기타 에러시) 3가지로 분기됩니다.

- GetInstance() : 클라이언트 인스턴스를 가져옵니다.
- SendRequestForString() : UnityWebRequest 객체를 받아서 통신 후 응답받은 데이터를 문자열로 콜백합니다.
- SendRequestForJson<T>() : UnityWebRequest 객체를 받아서 통신 후 응답받은 JSON 데이터를 T 객체로 파싱하여 콜백합니다.
- SendRequestForTexture() : UnityWebRequest 객체를 받아서 통신 후 응답받은 이미지 데이터를 Texture2D 객체로 파싱하여 콜백합니다.

## Tech Stack
* UnityWebRequest
* Unity Coroutine
* Unity JsonUtility

## Instructions
1. 클라이언트 초기화
```csharp
MaratangHttpClient httpClient = 
    MaratangHttpClient.GetInstance(monoBehaviour);
```

2-1. RequestBuilder를 이용한 GET request 객체 생성
```csharp
UnityWebRequest request = new RequestBuilder(baseUrl)
    .AddPath(path)
    .AddUrlParameter(key, value)
    .BuildGET();
```

2-2. RequestBuilder를 이용한 POST request 객체 생성
```csharp
UnityWebRequest request = new RequestBuilder(baseUrl)
    .AddPath(path)
    .AddPostData(key, value)
    .BuildPOST();
```

2-3. RequestBuilder를 이용한 이미지 request 객체 생성
```csharp
UnityWebRequest request = new RequestBuilder(baseUrl)
    .AddPath(path)
    .AddUrlParameter(key, value)
    .BuildTexture();
```

3-1. string response
```csharp
httpClient.SendRequestForString(request, success => {
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

3-2. JSON response
```csharp
// T: 역직렬화할 클래스
httpClient.SendRequestForJson<T>(request, success => {
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

3-3. Texture2D response
```csharp
httpClient.SendRequestForTexture(request, success => {
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
### GET JSON Array 씬
BuildGET() + GetJson<T>()
웹 API에서 노래방 신곡 리스트를 받아와 UI에 표시하는 예제입니다.

[API 정보](https://pureani.tistory.com/4997)

### GET Picture 씬
BuildTexture() + GetTexture()
이미지 사이트에서 특정 이미지를 받아와 UI에 표시하는 예제입니다.

[예제 이미지](https://cdn.pixabay.com/photo/2023/05/05/11/07/sweet-7972193_1280.jpg)