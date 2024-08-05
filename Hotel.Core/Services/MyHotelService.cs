using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Services;

public class MyHotelService : BackgroundService
{
    public MyHotelService(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger<MyHotelService>();
    }

    public ILogger Logger { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation("MyHotelService is starting.");

        stoppingToken.Register(() => Logger.LogInformation("MyHotelService is stopping."));

        while (!stoppingToken.IsCancellationRequested)
        {
            Logger.LogInformation("MyHotelService is doing background work.");

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }

        Logger.LogInformation("MyHotelService has stopped.");
    }
}