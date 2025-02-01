using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;

namespace SearchService;

public class AuctionUpdatedConsumer : IConsumer<AuctionUpdated>
{
    private readonly IMapper _mapper;
    public AuctionUpdatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<AuctionUpdated> context)
    {
        Console.WriteLine("--> consuming auction updated " + context.Message.Id);

        var item = _mapper.Map<Item>(context.Message);

        if (item.Model == "Foo") throw new ArgumentException("Cannot sell cars with name of Foo");

        await DB.Update<Item>()
            .MatchID(context.Message.Id)
            .ModifyOnly(x => new { x.Model, x.Make, x.Year, x.Color, x.Mileage }, item)
            .ExecuteAsync();
    }
}