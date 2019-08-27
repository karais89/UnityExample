using UniRx.Async;
using UnityEngine;
using UnityEngine.Networking;

public class Example01_UnityWebRequest : MonoBehaviour
{
    private async void Start()
    {
        // 기초: 구글에서 다운로드
        var google = await DownloadGoogle();
        Debug.Log(google.Substring(0, 100));

        // LINQ 쿼리 식을 사용한 선형 패턴
        // 구글 이후 다운로드, 빙 다운로드 시작
        DownloadGoogleAndBingWithLINQ();
    }

    // 비동기 웹 요청 받기
    private async UniTask<string> GetTextAsync(UnityWebRequest req)
    {
        var op = await req.SendWebRequest();
        return op.downloadHandler.text;
    }

    private async UniTask<string> GetTextAsync(string url)
    {
        return await GetTextAsync(UnityWebRequest.Get(url));
    }

    private UniTask<string> DownloadGoogle()
    {
        var task1 = GetTextAsync(UnityWebRequest.Get("http://google.com"));
        return task1;
    }

    private void DownloadGoogleAndBingWithLINQ()
    {
        // TODO: ObservableWWW.Get을 사용하지 않고, 쿼리식만으로 할 수 있는 방법이 없는지
        //var query = from google in ObservableWWW.Get("http://google.com/")
        //            from bing in ObservableWWW.Get("http://bing.com/")
        //            select new { google, bing };

        //var cancel = query.Subscribe(x => Debug.Log(x.google.Substring(0, 100) + ":" + x.bing.Substring(0, 100)));

        //// Call Dispose is cancel downloading.
        //cancel.Dispose();
    }

    //// You can return type as struct UniTask<T>(or UniTask), it is unity specialized lightweight alternative of Task<T>
    //// no(or less) allocation and fast excution for zero overhead async/await integrate with Unity
    //async UniTaskVoid DemoAsync()
    //{
    //    // get async webrequest
    //    async UniTask<string> GetTextAsync(UnityWebRequest req)
    //    {
    //        var op = await req.SendWebRequest();
    //        return op.downloadHandler.text;
    //    }

    //    var task1 = GetTextAsync(UnityWebRequest.Get("http://google.com"));
    //    var task2 = GetTextAsync(UnityWebRequest.Get("http://bing.com"));
    //    var task3 = GetTextAsync(UnityWebRequest.Get("http://yahoo.com"));

    //    // concurrent async-wait and get result easily by tuple syntax
    //    var (google, bing, yahoo) = await UniTask.WhenAll(task1, task2, task3);

    //    // You can handle timeout easily
    //    await GetTextAsync(UnityWebRequest.Get("http://unity.com")).Timeout(TimeSpan.FromMilliseconds(300));
    //}
}
