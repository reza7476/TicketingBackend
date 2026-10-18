using Hangfire;
using LawyerExam.Api.Configurations.ConnectionStrings;
using Npgsql;
using Ticketing.Application.Services.Entities.Tickets.Jobs;

namespace Ticketing.Api.BackgroundJobs;

public static class RequiringJob
{
    public static void RegisterHangfireRequiringJobs(
        this IApplicationBuilder app,
        IWebHostEnvironment env)
    {
   

        var recurringManager = app.ApplicationServices
            .GetRequiredService<IRecurringJobManager>();


        recurringManager.AddOrUpdate<TicketJobs>("ChangeTicketStatus",
            job => job.ChangeTicketStatus(),
            "*/5 * * * *");



       
    }
}