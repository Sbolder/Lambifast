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

TODO

### License

Licensed under [MIT](./LICENSE).

For your convenience, here is a list of all the licenses of our production
dependencies:

- MIT
- Apache License 2.0