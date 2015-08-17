#ifdef LONGBOATLIB_EXPORT
#define LONGBOATLIB_API __declspec(dllexport) 
#else
#define LONGBOATLIB_API __declspec(dllimport) 
#endif

#include "was/storage_account.h"
#include "was/blob.h"

extern "C"
{
	// Connection String, Used for authentication with azure
	utility::string_t storage_connection_string;

	// Current connected blob client
	azure::storage::cloud_blob_client blob_client;
	
	// Uses Storage access key to Connect to the storage account name given
	// Returns a boolean, true if successful
	LONGBOATLIB_API bool Connect(char *account_name, char *key);
	
	// Creates a Blob Container with provided string for the name
	// Returns a boolean, true if successful
	LONGBOATLIB_API bool CreateContainer(char *container_name);
	
	// Deletes the Blob Container with name matching string provided
	// Returns a boolean, true if successful
	LONGBOATLIB_API bool DeleteContainer(char *container_name);
	
	// Retrieves all current Blob Containers
	// Returns a char array which will decode to a string of Container Names, the end of a name is marked by # (Container-1#Container-2#) 
	LONGBOATLIB_API char* RetrieveContainers();
	
	// Creates Blob data 
	// Returns a boolean, true if successful
	LONGBOATLIB_API int CreateData(char *container_name, char *blob_name, char *data);
	
	// Deletes all Blob data in Container name matching string provided
	// Returns a boolean, true if successful
	LONGBOATLIB_API bool DeleteData(char *container_name);

	// Lists all Blob data within a Container with name matching given string
	// Returns a char array which will decode to data, the end of a piece Data is marked by # (Data-1#Data-2#)
	LONGBOATLIB_API char* ListData(char *container_name);
}
