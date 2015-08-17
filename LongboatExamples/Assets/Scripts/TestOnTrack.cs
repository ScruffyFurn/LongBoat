using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class TestOnTrack : MonoBehaviour
{
#if UNITY_IPHONE || UNITY_XBOX360
    [DllImport ("__Internal")]
    private static extern void Connect(string StorageAccount, string ConnectionKey);
    [DllImport ("__Internal")]
    private static extern void CreateContainer(string ContainerName);
#else
    [DllImport("Longboat")]
    private static extern bool Connect(string Storage_Account, string Connection_Key);
    [DllImport("Longboat")]
    private static extern bool CreateContainer(string Container_Name);
#endif

    // Name of Azure storage account
    private string accountName = "bamalam";
    // Primary connection key Azure storage account
    private string connectionKey = "2kvenZZVbF7/ia8U46VN3Y0X6cToAOYeecmN85lyzoRK9p4hDbelU7OzNOZls5deFcZ+D7rL57DKibCsx7gNkw==";

    // Use this for initialization
    void Start()
    {
        Debug.Log("Connected " + Connect(accountName, connectionKey));

        Debug.Log("Container Created: " + CreateContainer("bamalam-container-1"));
    }
}
