using Autofac;
using Autofac.Extensions.DependencyInjection;
using Ticketing.Application;
using Ticketing.Infrastructure;
using Ticketing.Shared;

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
        protected override void Load(ContainerBuilder builder)
        {
            var applicationAssembly = typeof(ApplicationAssembly).Assembly;
            var infrastructureAssembly = typeof(InfrastructureAssembly).Assembly;
            var sharedAssembly = typeof(SharedAssembly).Assembly;
            var apiAssembly = typeof(AutofacSetting).Assembly;


            builder.RegisterType<HttpContextAccessor>()
                .As<IHttpContextAccessor>()
                .SingleInstance();


            builder.RegisterAssemblyTypes(
                applicationAssembly,
                infrastructureAssembly,
                sharedAssembly,
                apiAssembly
                )
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();



            base.Load(builder);
        }

    }
}



