#region License
/*
 * Copyright 2021 AutoCore
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * Authors: AutoCore Members
 *
 */
#endregion
using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class WebRequestServer : MonoBehaviour
    {
        bool loop = true;
        public string postAddress;
        public string getAddress;
        public Action<string> OnGetRequest;
        public Action<string> OnPostRequest;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(GetJsonData(new Uri(Path.Combine(Application.streamingAssetsPath, "AddressConfig.json"))));
        }
        void OnDestroy()
        {
            loop = false;
        }

        public IEnumerator GetWebRequest()
        {
            while (loop)
            {
                using (UnityWebRequest webRequest = UnityWebRequest.Get(getAddress))
                {
                    yield return webRequest.SendWebRequest();
                    if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
                    {
                        Debug.LogError(webRequest.error + "\n" + webRequest.downloadHandler.text);
                    }
                    else
                    {
                        OnGetRequest(webRequest.downloadHandler.text);
                    }
                }
            }
        }
        public IEnumerator GetJsonData(Uri uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                yield return webRequest.SendWebRequest();
                if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogError(webRequest.error + "\n" + webRequest.downloadHandler.text);
                }
                else
                {
                    Debug.Log(webRequest.downloadHandler.text);
                    string jsonStr = webRequest.downloadHandler.text;
                    CVManager.Instance.addressNode.Config = JsonConvert.DeserializeObject<AddressConfig>(jsonStr);
                    StartCoroutine(GetWebRequest());
                }
            }
        }

        public IEnumerator PostWebRequest(TrafficLightData data)
        {
            string content = JsonConvert.SerializeObject(data);
            UnityWebRequest webRequest = UnityWebRequest.Post(postAddress, content);
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(webRequest.error + "\n" + webRequest.downloadHandler.text);
            }
            else
            {
                Debug.Log(OnGetRequest + webRequest.downloadHandler.text);
                OnPostRequest(webRequest.downloadHandler.text);
            }
        }
        public IEnumerator PostWebRequest_Form(TrafficLightData data)
        {
            WWWForm form = new WWWForm();
            form.AddField("light_id", data.id);
            form.AddField("color", data.color);
            form.AddField("remain", data.remain);
            UnityWebRequest webRequest = UnityWebRequest.Post(postAddress, form);
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(webRequest.error + "\n" + webRequest.downloadHandler.text);
            }
            else
            {

                Debug.Log(OnGetRequest + webRequest.downloadHandler.text);
                OnPostRequest(webRequest.downloadHandler.text);
            }
        }
        public IEnumerator PostWebRequest(string data)
        {
            byte[] databyte = Encoding.UTF8.GetBytes(data);

            UnityWebRequest webRequest1 = UnityWebRequest.Post(postAddress, data);
            yield return webRequest1.SendWebRequest();
            if (webRequest1.result == UnityWebRequest.Result.ProtocolError || webRequest1.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(webRequest1.error + "\n" + webRequest1.downloadHandler.text);
            }
            else
            {

                Debug.Log(OnGetRequest + webRequest1.downloadHandler.text);
                OnPostRequest(webRequest1.downloadHandler.text);
            }
        }
    }
}
