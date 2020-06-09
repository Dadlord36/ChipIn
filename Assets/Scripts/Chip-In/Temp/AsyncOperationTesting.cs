using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityWeld.Binding;

namespace Temp
{
    [Binding]
    public class AsyncOperationTesting : MonoBehaviour
    {
        private CancellationTokenSource _cancellationSource;

        private void OnEnable()
        {
            ApiHelper.InitializeClient();
        }

        private void OnDisable()
        {
            ApiHelper.Dispose();
        }

        private const string SmallFileUrl = "http://212.183.159.230/5MB.zip";

        public async void OnGUI()
        {
            if (GUI.Button(new Rect(100, 100, 100, 100), "Start Task"))
            {
                /*try
                {*/
                    var tasks = new List<Task>(10);
                    _cancellationSource = new CancellationTokenSource();
                    for (int i = 0; i < 10; i++)
                    {
                        tasks.Add(ApiHelper.MakeARequest(HttpMethod.Get, SmallFileUrl, _cancellationSource.Token));
                    }

                    await Task.WhenAll(tasks);
                    Debug.Log("Images was loaded");
                    /*var bites = await result.Content.ReadAsByteArrayAsync();
                    Debug.Log(bites.Length.ToString());*/
                /*}
                catch (OperationCanceledException e)
                {
                    Debug.Log("Operation was cancelled");   
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }*/
            }

            if (GUI.Button(new Rect(100, 300, 100, 100), "Cancel Task"))
            {
                _cancellationSource.Cancel();
            }
        }

        IEnumerator RunTask()
        {
            yield return RunTaskAsync();
        }

        Task CallAnotherTask()
        {
            return RunTaskAsync();
        }

        async Task RunTaskAsync()
        {
            Debug.Log("Started task...");
            await Task.Delay(TimeSpan.FromSeconds(1));
            throw new Exception();
        }
    }
}