using DataModels;
using HttpRequests;
using HttpRequests.RequestsProcessors;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private UserLoginModel defaultLoginModel;

    // Start is called before the first frame update
    private void Start()
    {
        ApiHelper.InitializeClient();
        Testing();
    }

    private async void Testing()
    {

    }

    #region Temp

    private async void TryToRegisterOnTestApi()
    {
        var processor = new TestApiRegistrationProcessor();

        TestApiUserProfileModel profileModel = await processor.SendRequest(new TestApiUserRegistrationModel
        {
            name = "Joe", job = "Worker"
        });

        Debug.Log(profileModel.ToString());
    }

    #endregion
}