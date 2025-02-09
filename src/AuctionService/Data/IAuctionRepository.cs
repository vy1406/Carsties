using AuctionService.Data;

namespace AuctionService;

public interface IAuctionRepository
{
    Task<List<AuctionDto>> GetAllAuctionsAsync(string date);
    Task<AuctionDto> GetAuctionByIdAsync(Guid id);
    Task<Auction> GetAuctionEntityById(Guid id);

    void AddAuction(Auction auction);
    void RemoveAuction(Auction auction);
    Task<bool> SaveChangesAsync();
}