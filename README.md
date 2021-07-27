# BPDTS Test API

An API built for the BPDTS Test. This task was completed by Richard Meara.

## Tools and Environments used

The language used for the task was C#, and the framework was .NET 5.0.

The IDE used was Visual Studio 2019.

The build and releases were produced using Azure DevOps, with the source code hosted by GitHub.

## Building, testing and deploying

The .NET solution contains 2 projects: An API and a test project. The test project contains basic unit tests to ensure the controller logic is correct. It does not use the live API, and instead uses fake data.

The build and test definition is in the file at the root directory:

```
azure-pipelines.yml
```
The build uses the .NET SDK, restores the solution, builds it in a deployable package, and then tests the binaries. Finally it uploads the produced deployable package as an artifact.

A release has been produced that will deploy to a free tier app service created by me on Azure. The link to which is below:

[https://bpdts-test-api.azurewebsites.net/](https://bpdts-test-api.azurewebsites.net/)

Below is a status badge for the build of the master branch. The project is public for the purpose of being transparent.

[![Build Status](https://dev.azure.com/richardmeara/BPDTS_Test_API/_apis/build/status/richardmeara.BPDTS_Test_API?branchName=master)](https://dev.azure.com/richardmeara/BPDTS_Test_API/_build/latest?definitionId=10&branchName=master)

## Using the API

As mentioned above, the API has been hosted on a free tier. There are 3 API methods written to satisfy the test.

/TestAnswer/users/london - This will provide a complete list of users who live in London and who are within 50 miles of the central London location.

/TestAnswer/users/london/citynameonly - This will provide a list of users who live in London by using the API call provided: 

```
/city/{city}/users
```

/TestAnswer/users/london/coordinatesonly - This will provide a list of users who are within 50 miles of the central London location.

This has been done by using the API method:


```
/users
```

Once a JSON list of users has been received, they are parsed into a list of objects defined as User.cs. From here they are iterated through, and those with coordinates within the bounds of 50 miles are added to a separate list. This new list is then returned by my API.

The calculation done first checked that the longitude and latitude coordinates are both valid, then uses the GeoCoordinate class to parse the coordinates and then use the GetDistanceTo method to calculate the distance in metres. This is then converted to miles and compared against the 50 mile limit.

The GeoCoordinate class is inherited from the publicly available NuGet package GeoCoordinate.NetCore by cormaltes.
The NuGet feed can be found below:
[https://www.nuget.org/packages/GeoCoordinate.NetCore/1.0.0.1?_src=template](https://www.nuget.org/packages/GeoCoordinate.NetCore/1.0.0.1?_src=template)