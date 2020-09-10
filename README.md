# Store Map

## About
Are you looking for something in the store but can't find it? This is the app you need!

### Main concept
1. Search for items in the store that you're in
1. Add them to shopping list
1. Follow the map of the store to find everything you need
1. Profit

### Feature list
- [x] Manage stores
- [x] Authentication for admins and moderators
- [x] Manage store items
- [x] Display store map for client
  - [ ] Improve scaling on different screens
- [ ] Allow users to add items if they are not in the list
  - [ ] Allow users to up-vote/down-vote new items to prevent misleading items
- [ ] Shopping cart

### Tech stack
- Front-end: Blazor Client-Side (WASM)
  - [Ant Design components for blazor](https://github.com/ant-design-blazor/ant-design-blazor)
- Back-end: .NET Azure Functions
- Database: MongoDB
- Authentication: [Auth0](https://auth0.com/)

## Setup

- `StoreMap/wwwroot/appsettings.json` structure:
  ```
  {
    "Local": {
      "Authority": "{Authentication authority}",
      "ClientId": "{Authentication service client id}",
      "ResponseType": "code"
    },
    "API_URL": "{Url of StoreMap.Backend api / http://localhost:7071/api/}"
  }
  ```
- `StoreMap.Backend/local.settings.json` stucture:
  ```
  {
    "IsEncrypted": false,
    "Values": {
      "AzureWebJobsStorage": "",
      "FUNCTIONS_WORKER_RUNTIME": "dotnet",
      "MongoDBConnectionString": "{MongDB connection string}",
      "FUNCTIONS_V2_COMPATIBILITY_MODE": true
    },
    "Host": {
      "CORS": "*"
    }
  }
  ```
