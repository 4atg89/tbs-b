
using Profile.API.Model;
using Profile.Domain.Model;

namespace Profile.API.Extensions;

internal static class InventoryExtensions
{

    internal static InventoryResponse MapInventory(this InventoryModel model) =>
        new() { Coins = model.Coins, Gems = model.Gems };

}