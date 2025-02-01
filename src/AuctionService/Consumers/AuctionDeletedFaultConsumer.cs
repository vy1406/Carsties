using Contracts;
using MassTransit;

namespace AuctionService;

public class AuctionDeletedFaultConsumer : IConsumer<Fault<AuctionDeleted>>
{
    public async Task Consume(ConsumeContext<Fault<AuctionDeleted>> context)
    {
        Console.WriteLine("--> consuming auction deleted fault ");

        var exception = context.Message.Exceptions.First();

        if (exception.ExceptionType == "System.ArgumentException")
        {
            await context.Publish(context.Message.Message);
        }
        else
        {
            Console.WriteLine("Not an argument exception, update erro dashboard somewhere");
        }
    }
}