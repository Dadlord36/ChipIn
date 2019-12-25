using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Assertions;

public interface ISomething
{
    
    int FirstInteger { get; set; }
    
    string FirstString { get; set; }
}


public class Something : ISomething
{
    [JsonProperty("fist-integer")]
    public int FirstInteger { get; set; } = 4;
    [JsonProperty("first-string")]
    public string FirstString { get; set; } = "Hello";
    
    public List<KeyValuePair<string, string>> GetRequestHeaders()
    {
        var headersInterface = this as ISomething;
        
        var properties = headersInterface.GetType().GetProperties();
        var headers = new List<KeyValuePair<string, string>>(properties.Length);

        for (int i = 0; i < properties.Length; i++)
        {
            foreach (var attribute in properties[i].CustomAttributes)
            {
                Assert.IsTrue(attribute.AttributeType == typeof(JsonPropertyAttribute));

                headers.Add(new KeyValuePair<string, string>(attribute.ConstructorArguments[0].Value.ToString(),
                    properties[i].GetValue(headersInterface).ToString()));
            }
        }

        return headers;
    }

    public string GetRequestHeadersAsString()
    {
        var keyValuePairs = GetRequestHeaders();
        var stringBuilder = new StringBuilder();

        foreach (var valuePair in keyValuePairs)
        {
            stringBuilder.Append($"{valuePair.Key} : {valuePair.Value}\n");
        }

        return stringBuilder.ToString();
    }
} 


public class PropertiesSerialization : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var something = new Something() as ISomething;
        Debug.Log( JsonConvert.SerializeObject(something));
    }
    
}
