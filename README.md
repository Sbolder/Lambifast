# Lambifast 
<br />

Lambifast is an extension of the framework provided by AWS Amazon.Lambda.AspNetCoreServer.Hosting (https://www.nuget.org/packages/Amazon.Lambda.AspNetCoreServer.Hosting/#readme-body-tab).

Lambifast provides you with a simple extension to develop microservices on serverless architecture.
It allows you to have tracing and logging capabilities integrated with AWS CloudWatch service.
Input dto validation using FluentValidator and errors in standard RFC.

GitHub Repository: https://github.com/Sbolder/Lambifast


### Table of Contents

 - [Install](#install)
 - [Example](#example)
 - [License](#license)




### Install

To install Lambifast in an existing project as a dependency:

Install with nuget:
```sh
dotnet add package Lambifast --version 1.0.0
```

In your Program.cs:

    public class Program : BaseProgram<Program>
    {
        public static async Task Main(String[] args)
        {
            var program = new Program();
            await program.RunAsync(args);
        }


        protected override void InnerConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            #region Services
            //Register your Services
            services.AddSingleton<IPeopleService, PeopleService>();

            #endregion
        }
    }

Edit your Controller:

    public class PeopleController : ControllerBase{
        ...
    }

### Example

Find simple example in example folder on github repo: https://github.com/Sbolder/Lambifast/tree/main/example

https://github.com/Sbolder/Lambifast/tree/main/example/build contain a sam template for test sample on your aws account.
buildRelease.ps1 script build and deploy the sample on your aws account, and print che url avaialable for test.
!!! you need to create a s3 bucket and replace the name in templae.yaml !!! s3 bucket are global unique.

Logging and traicing are automatically avaialable on cloudWatch console, no action need.



### License

Licensed under [MIT](./LICENSE).

For your convenience, here is a list of all the licenses of our production
dependencies:

- MIT
- Apache License 2.0