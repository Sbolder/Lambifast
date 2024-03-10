using Lambifast.Sample.Services;
using Lambifast.Sample.Services.Impl;

namespace Lambifast.Sample
{
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
            services.AddSingleton<IBookService, BookService>();

            #endregion
        }
    }
}