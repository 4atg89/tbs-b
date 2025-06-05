using Grpc.Core;
using HeroesService.Grpc;

namespace HeroesService.GRPC;

public class HeroesGRPCService : HeroService.HeroServiceBase
{
    public override Task<HeroesResponse> GetHeroes(HeroesRequest request, ServerCallContext context)
    {
        var image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcThTxG3RDV98g63Ujjqst0LvYej8cywueL_RSnq5ku4ft_vMeNXOb0se6gjeeZrbqdZqE4&usqp=CAU";
        return Task.FromResult(new HeroesResponse
        {
            Heroes = {
                 new HeroResponseDto { HeroId = 1, Image = image, NextLevelPriceCoins = 10 },
                 new HeroResponseDto { HeroId = 2, Image = image, NextLevelPriceCoins = 11 },
                 new HeroResponseDto { HeroId = 3, Image = image, NextLevelPriceCoins = 12 },
                 new HeroResponseDto { HeroId = 4, Image = image, NextLevelPriceCoins = 15 },
            }
        });
    }
    
}