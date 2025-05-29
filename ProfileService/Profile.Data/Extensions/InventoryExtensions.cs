using Profile.Data.Data.Entities;
using Profile.Domain.Model;

namespace Profile.Data.Extensions;

internal static class InventoryExtensions
{

    internal static InventoryModel MapInventory(this ProfileEntity model) =>
        new() { Coins = model.Coins, Gems = model.Gems };

}