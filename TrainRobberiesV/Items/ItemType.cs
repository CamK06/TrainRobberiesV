using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TrainRobberiesV.Items
{
    [JsonConverter(typeof(StringEnumConverter))]
    internal enum ItemType
    {
        Pawn, Weapon, Ammo, Junk
    }
}
