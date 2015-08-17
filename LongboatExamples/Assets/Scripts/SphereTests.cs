using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class SphereTests : MonoBehaviour 
{
    // Name of Azure storage account
    private string accountName = "bamalam";
    // Primary connection key Azure storage account
    private string connectionKey = "2kvenZZVbF7/ia8U46VN3Y0X6cToAOYeecmN85lyzoRK9p4hDbelU7OzNOZls5deFcZ+D7rL57DKibCsx7gNkw==";

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

    [SerializeField]
    private GameObject spherePrefab;

    private string data;
    private List<Vector3> locations;
    private List<GameObject> balls;

    // Colors to learn between
    [SerializeField]
    private Color Red = new Color(1, 0, 0, 1);
    [SerializeField]
    private Color Blue = new Vector4(0, 0, 1, 0.2f);

    void Awake()
    {
        locations = new List<Vector3>();
        balls = new List<GameObject>();
    }
        
	void Start () 
    {
        Debug.Log("Connected " + Connect(accountName, connectionKey));

        data = ListData("bamalam-container-1");

        DecodeData();

        PlaceBalls();
        // Calculate ball densities
        // Color balls based on densities

        /* The 3 things we will need to do for each point, I will have to solve the sorting layer later (sorting layer can be a % from 0.0 to 1, Use same calculation as for density)
        Balls[0] = Instantiate(spherePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        Balls[0].GetComponent<Renderer>().materials[0].color = Red; // Red
        Balls[0].GetComponent<Renderer>().sortingLayerName = "Red";
        */
	}

    private void PlaceBalls()
    {
        for(int i = 0 ; i < locations.Count ; i++)
        {
            balls.Add(Instantiate(spherePrefab, locations[i], Quaternion.identity) as GameObject);
        }
    }

    private void DecodeData() // Needs testing
    {
        for(int i = 0 ; i < data.Length ; i++)
        {
            string loc = "";
            for ( ; i < data.Length ; i++)
            {   
                if(data[i] == '#')
                {
                    break;
                }

                loc = loc + data[i];
            }

            if (loc.Length > 4) // temp fix till the bug is fixed in DLL6
            {
                locations.Add(DataToVector(loc));
            }
        }
    }

    private Vector3 DataToVector(string locString)  // Needs testing
    {
        Vector3 product = Vector3.zero;

        //string[] xyz = new string[3];
        int place = 0;
        for (int i = 0; i < locString.Length; i++ )
        {
            string number = "";
            for ( ; i < locString.Length ; i++)
            {
                if(locString[i] == ',')
                {
                    //xyz[place] = number;
                    break;
                }

                number = number + locString[i];
            }

            product[place] = float.Parse(number);
            place++;
        }

        //product.x = float.Parse(xyz[0]);
        //product.y = float.Parse(xyz[1]);
        //product.z = float.Parse(xyz[2]);

            // break down the string ending at each , and when we null out
            // each of X, Y, Z will be there own string in an array of 3 strings
            // make all up till first , X
            // from first , to second ,  Y
            // from second , to null Z
            // convert the 3 strings to floats and place in product

        return product;
    }
}
