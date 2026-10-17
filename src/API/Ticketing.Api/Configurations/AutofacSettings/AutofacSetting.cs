using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace LawyerExam.Api.Configurations.AutofacSettings;
public static class AutofacSetting
{
    public static ConfigureHostBuilder AddAutoFac(this ConfigureHostBuilder builder)
    {
        builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
        {
            var env = context.HostingEnvironment.EnvironmentName;
            var contentRoot = context.HostingEnvironment.ContentRootPath;
            containerBuilder.RegisterModule(new AutoFacModule());
        });

        return builder;
    }

    public class AutoFacModule : Module
    {

        public AutoFacModule()
        {

        }


    }
}



