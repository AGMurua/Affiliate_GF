# API CHALLENGE

## Local Configuration DB:
1. Begin by creating a local database in Microsoft SQL Server using SQL Server Management Studio (SSMS). You can name it as you like; for example, I will call it "CommissionsDB".
2. Next, update the database connection string in the API's app settings. The default connection string appears as follows:
Replace YourServerName with your database server's name and YourDatabaseName with your desired database name.
3. You can choose to use either Swagger or the provided Postman collection for testing purposes. The Swagger documentation is accessible by default when you run the API. The Postman collection is available in the following folder:
4. Once you've configured the database connection string, the API is ready to be launched. The migrations will automatically create the necessary tables and seed some initial data for convenient testing.

Make sure the default project to launch is AffiliateApi
![alt text](https://imgur.com/a/yqCtzYg)

## Testing
1. The Postman collection comes pre-configured, making it easy to start using the API with sample data that's ready to go. You can find it in : API\AffiliatesApi\PostmanCollection
2. Any endpoint that involves sending data will validate the input to ensure it only contains letters and spaces, and any unnecessary characters will be trimmed.
3. It's important to note that I've considered the requirement that "Each affiliate and customer has a unique identifier and a name." As a result, the IDs are unique; however, customer and affiliate names can be repeated.


# How to Run It

## Endpoints:

There are two controllers: `Affiliate` and `Customer` Controller.

### Affiliate Controller:

1. **GET** `api/Affiliate`: Retrieve a list of all affiliates.
2. **GET** `api/Affiliate/GetAllWithRelations`: Retrieve a list of all affiliates with their relations (customers). While this endpoint wasn't explicitly required, I've included it for easy data checking.
3. **POST** `api/Affiliate`: Create a new affiliate. Include the affiliate's name in the request body. A unique ID will be generated on the database side.
4. **GET** `api/Affiliate/{id}/Customers`: Retrieve a list of customers associated with a specific affiliate. Provide the affiliate's ID as a path parameter.
5. **GET** `api/Affiliate/{id}/Commissions`: Retrieve the count of customers associated with a specific affiliate. Provide the affiliate's ID as a path parameter.

### Customer Controller:
1. **POST** `api/Customer`: Create a new customer linked to an Affiliate. Include the customer name and the AffiliateID in the request body. A unique ID will be generated on the database side.

## Expected behavior Affiliate:

###  **GET** `api/Affiliate`  
| Expected http code  | Expected result                                        |
|---------------------|--------------------------------------------------------|
| 200                 | Returns all the affiliates in the database             |

###  **GET** `api/Affiliate/GetAllWithRelations` 
| Expected http code  | Expected result                                        |
|---------------------|--------------------------------------------------------|
| 200                 | Returns all the affiliates with nested customers       |

### **POST** `api/Affiliate`  
| Expected http code  | Expected result                                        |
|---------------------|--------------------------------------------------------|
| 201                 | Creates and returns the entity saved in the database   |
| 400                 | Returns a bad request with an error message            | 

### **POST** `api/Affiliate/{id}/Customers`  
| Expected http code  | Expected result                                        |
|---------------------|--------------------------------------------------------|
| 200                 | Returns all customers for the specified Affiliate      |
| 204                 | Returns no content if there are no customers           |
| 404                 | Returns not found with an explanatory message          |


### **POST** `api/Affiliate/{id}/Commissions`  
| Expected http code  | Expected result                                        |
|---------------------|--------------------------------------------------------|
| 200                 | Returns the number of customers associated             |
| 404                 | Returns not found with an explanatory message          |

If this affiliate doesn't have any customers, the returned number will be 0.

## Expected behavior Customer:

### **POST** `api/Customer`  
| Expected http code  | Expected result                                        |
|---------------------|--------------------------------------------------------|
| 201                 | Creates and returns the entity saved in the database   |
| 400                 | Returns a bad request with an error message            | 
