using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class LongboatTests : MonoBehaviour {
#if UNITY_IPHONE || UNITY_XBOX360
    [DllImport ("__Internal")]
    private static extern void Connect(string StorageAccount, string ConnectionKey);
    [DllImport ("__Internal")]
    private static extern void CreateContainer(string ContainerName);
    [DllImport ("__Internal")]
    private static extern bool DeleteContainer(string Container_Name);
    [DllImport ("__Internal")]
    private static extern string RetrieveContainers();
    [DllImport ("__Internal")]
    private static extern bool CreateData(string Container_Name, string Data_Name, string Data);
    [DllImport ("__Internal")]
    private static extern bool DeleteData(string Container_Name);
    [DllImport ("__Internal")]
    private static extern string ListData(string Container_Name);
#else
    [DllImport("Longboat")]
    private static extern bool Connect(string Storage_Account, string Connection_Key);
    [DllImport("Longboat")]
    private static extern bool CreateContainer(string Container_Name);
    [DllImport("Longboat")]
    private static extern bool DeleteContainer(string Container_Name);
    [DllImport("Longboat")]
    private static extern string RetrieveContainers();
    [DllImport("Longboat")]
    private static extern int CreateData(string Container_Name, string Data_Name, string Data);
    [DllImport("Longboat")]
    private static extern bool DeleteData(string Container_Name);
    [DllImport("Longboat")]
    private static extern string ListData(string Container_Name);
#endif

    // Name of Azure storage account
    private string accountName = "bamalam";
    // Primary connection key Azure storage account
    private string connectionKey = "2kvenZZVbF7/ia8U46VN3Y0X6cToAOYeecmN85lyzoRK9p4hDbelU7OzNOZls5deFcZ+D7rL57DKibCsx7gNkw==";

    // Use this for initialization
	void Start () {
        Debug.Log("Connected " + Connect(accountName, connectionKey));

        //Debug.Log("Container Created: " + CreateContainer("bamalam-container-1"));

        //Debug.Log("Container Deleted: " + DeleteContainer("bamalam-container-1"));

        //Debug.Log("String of container name data: " + RetrieveContainers());

        //Debug.Log("Data Created. Count at: " + CreateData("bamalam-container-1", "Location: " + "0,1,2", "0,1,2"));
        //Debug.Log("Data Created. Count at: " + CreateData("bamalam-container-1", "Location: " + "1,2,3", "1,2,3"));
        //Debug.Log("Data Created. Count at: " + CreateData("bamalam-container-1", "Location: " + "2,3,4", "2,3,4"));
        //Debug.Log("Data Created. Count at: " + CreateData("bamalam-container-1", "Location: " + "3,4,5", "3,4,5"));
        //Debug.Log("Data Created. Count at: " + CreateData("bamalam-container-1", "Location: " + "4,5,6", "4,5,6"));
        /*
        for(int i = 0; i < 100; ++i)
        {
            float randX = Random.Range(-50, 50);
            float randY = Random.Range(-50, 50);
            float randZ = Random.Range(-50, 50);
            string loc = System.Math.Round(randX, 4).ToString() + "," + System.Math.Round(randY, 4).ToString() + "," + System.Math.Round(randZ, 4).ToString();
            Debug.Log("Data Created. Count at: " + CreateData("bamalam-container-1", "Location: " + loc, loc));
        }
        */
        //Debug.Log("Data Deleted: " + DeleteData("bamalam-container-1"));

        //Debug.Log("String of Blob Data:" + ListData("bamalam-container-1"));


        //Timing tests for george

        //Debug.Log("Container Created: " + CreateContainer("bamalam-container-2"));

        System.Threading.Thread newThread = new System.Threading.Thread(Push);
        newThread.Start();

        //Debug.Log("String of Blob Data:" + ListData("bamalam-container-2"));

        //Debug.Log("Data Deleted: " + DeleteData("bamalam-container-2"));
    }

    private void Push()
    {
        //System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        //timer.Start();

        for (int i = 0; i < 1000; ++i)
        {
            System.Random r = new System.Random();
            float x = r.Next(-50, 50);
            float y = r.Next(-50, 50);
            float z = r.Next(-50, 50);
            string loc = x + "," + y + "," + z;
            CreateData("bamalam-container-2", "Location: " + loc, loc);
            //Debug.Log("Data Created. Count at: " + CreateData("bamalam-container-2", "Location: " + loc, loc));
            //Debug.Log(i + " time " + timer.ElapsedMilliseconds);
        }
        //Debug.Log("Total " + timer.ElapsedMilliseconds);
    }
}
