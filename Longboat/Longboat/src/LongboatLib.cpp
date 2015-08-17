#include "LongboatLib.h"
#include "was/storage_account.h"
#include "was/blob.h"

extern "C"
{
	// Uses Storage access key to Connect
	bool Connect(char *account_name, char *key)
	{
		try
		{
			// Builds Connection string for Azure
			storage_connection_string = U("DefaultEndpointsProtocol=https;AccountName=") + utility::conversions::to_string_t(account_name) + U(";AccountKey=") + utility::conversions::to_string_t(key) + U(";");

			// Retrieve storage account
			azure::storage::cloud_storage_account storage_account = azure::storage::cloud_storage_account::parse(storage_connection_string);

			// Create blob client
			blob_client = storage_account.create_cloud_blob_client();

			return true;
		}
		catch (const std::exception& e)
		{
			std::wcout << U("Error: ") << e.what() << std::endl;

			return false;
		}
		return false;
	}
	
	bool CreateContainer(char *container_name)
	{
		try
		{
			// Retrieve refference to a container
			azure::storage::cloud_blob_container container = blob_client.get_container_reference(utility::conversions::to_string_t(container_name));

			// Create container if it does not exist
			if (container.create_if_not_exists())
			{
				// Retrieve refference to blob data if exists
				azure::storage::cloud_block_blob blob = container.get_block_blob_reference(U("DataCount"));

				// Add Blob data to an existing container
				blob.upload_text(U("0"));
			}
			return true;
		}
		catch (const std::exception& e)
		{
			std::wcout << U("Error: ") << e.what() << std::endl;

			return false;
		}
		return false;
	}
	
	bool DeleteContainer(char *container_name)
	{
		try
		{
			// Retrieve refference to a container
			azure::storage::cloud_blob_container container = blob_client.get_container_reference(utility::conversions::to_string_t(container_name));

			// Deletes the retrieved container
			container.delete_container_if_exists();

			return true;
		}
		catch (const std::exception& e)
		{
			std::wcout << U("Error: ") << e.what() << std::endl;

			return false;
		}
		return false;
	}
	
	char* RetrieveContainers()
	{
		// Char Array we will be returning with blob data
		char *contents;
		try
		{
			// Pull down data into a result_segment of Containers
			azure::storage::result_segment<azure::storage::cloud_blob_container> containers = blob_client.list_containers_segmented(containers.continuation_token());

			// Define array to return
			contents = new char[containers.results().size() * 63];

			// container is numeber of container
			// j is char in string
			// i is for total in array
			int i = 0;
			for each (azure::storage::cloud_blob_container container in containers.results())
			{
				for (int j = 0; j < container.name().size(); j++)
				{
					contents[i] = container.name()[j];
					i++;
				}
				contents[i] = '#';
				i++;
			}
			return contents;
		}
		catch (const std::exception& e)
		{
			std::wcout << U("Error: ") << e.what() << std::endl;

			// returns char array of 0 if it fails
			contents = new char[0];
			return contents;
		}
	}
	
	int CreateData(char *container_name, char *blob_name, char *data)
	{
		try
		{
			// Retrieve refference to a container
			azure::storage::cloud_blob_container container = blob_client.get_container_reference(utility::conversions::to_string_t(container_name));

			// Retrieve refference to blob data if exists
			azure::storage::cloud_block_blob blob = container.get_block_blob_reference(utility::conversions::to_string_t(blob_name));

			// Add Blob data to an existing container
			blob.upload_text(utility::conversions::to_string_t(data));

			azure::storage::cloud_block_blob countBlob = container.get_block_blob_reference(U("DataCount"));
			utility::string_t countText(countBlob.download_text());
			int count(std::stoi(countText));
			count++;
			countBlob.upload_text(utility::conversions::to_string_t(std::to_string(count)));

			return count;
		}
		catch (const std::exception& e)
		{
			std::wcout << U("Error: ") << e.what() << std::endl;

			return 0;
		}
		return 0;
	}
	
	bool DeleteData(char *container_name)
	{
		try
		{
			// Retrieve refference to a container
			azure::storage::cloud_blob_container container = blob_client.get_container_reference(utility::conversions::to_string_t(container_name));

			// Create Iterator for loop
			azure::storage::list_blob_item_iterator end_of_blob_results;

			// Iterates through Blobs in the container
			// Deletes each blob
			for (azure::storage::list_blob_item_iterator iter = container.list_blobs(); iter != end_of_blob_results; ++iter)
			{
				if (iter->is_blob() && iter->as_blob().name() != U("DataCount"))
				{
					try
					{
						// Delete Blob
						iter->as_blob().delete_blob_if_exists();
					}
					catch (const std::exception& e)
					{
						std::wcout << U("Error: ") << e.what() << std::endl;
						return false;
					}
				}
			}
			// Retrieve refference to blob data if exists
			azure::storage::cloud_block_blob countBlob = container.get_block_blob_reference(U("DataCount"));

			// Add Blob data to an existing container
			countBlob.upload_text(U("0"));
			return true;
		}
		catch (const std::exception& e)
		{
			std::wcout << U("Error: ") << e.what() << std::endl;
			return false;
		}
		return false;
	}
	
	char* ListData(char *container_name)
	{
		// String will will be returning with blob data
		char *contents;

		// Retrieve refference to a container
		azure::storage::cloud_blob_container container = blob_client.get_container_reference(utility::conversions::to_string_t(container_name));

		// Get how many blobs we have stored in container from the DataCount Blob
		azure::storage::cloud_block_blob countBlob = container.get_block_blob_reference(U("DataCount"));
		utility::string_t countText(countBlob.download_text());
		int blobCount(std::stoi(countText));

		// Define size of array to return
		contents = new char[blobCount * 32];

		// Create dummy Iterator for loop to mark zero
		azure::storage::list_blob_item_iterator end_of_blob_results;

		// Iterates through Blobs in the container
		// Adds each set of data to a string_t to return
		int i = 0;
		for (azure::storage::list_blob_item_iterator iter = container.list_blobs(); iter != end_of_blob_results; iter++)
		{
			if (iter->is_blob())
			{
				if (iter->as_blob().name() != U("DataCount"))
				{
					// Retrieves current blob by name
					azure::storage::cloud_block_blob blob = container.get_block_blob_reference(iter->as_blob().name());
					
					// Get Current blobs data
					utility::string_t data = blob.download_text();

					// Add data to Array to return                // Possible if here to correct trailing char bug, if(data.size() > 4). this needs to be adressed
					for (int j = 0; j < data.size(); j++)
					{
						// Adds current blobs data to the return return string
						contents[i] = data[j];
						i++;
					}
					contents[i] = '#';
					i++;
				}
			}
		}
		return contents;
	}
}