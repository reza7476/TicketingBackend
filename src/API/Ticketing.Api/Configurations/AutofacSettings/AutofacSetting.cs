using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;
using Ticketing.Application;
using Ticketing.Infrastructure;
using Ticketing.Shared;
using Ticketing.Shared.DTOS;

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

            var a = "";
            var b = "";
            builder.Register(ctx =>
            {
                var settings = ctx.Resolve<IOptions<MiIoSettingsDto>>().Value;
                a = settings.AccessKey; b = settings.SecretKey;
                return new MinioClient()
                .WithEndpoint(settings.Endpoint)
                .WithCredentials(settings.AccessKey, settings.SecretKey)
                .Build();
            })
           .As<IMinioClient>()
           .SingleInstance();

            base.Load(builder);
        }

    }
}



