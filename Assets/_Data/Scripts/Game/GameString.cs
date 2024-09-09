
using System;

public static class GameString 
{

    public enum WeaponName
    {
        Null,

        Hammer,
        Candy,
        Axe,
        TagetSpawnPos,

    }

    public static string ToString(WeaponName strings)
    {
        return strings switch
        {
            WeaponName.Null => "Null",

            WeaponName.Hammer => "Hammer",
            WeaponName.Candy => "Candy",
            WeaponName.Axe => "Axe",   
            WeaponName.TagetSpawnPos => "mixamorig:RightHandThumb2",   
            _ => throw new ArgumentOutOfRangeException(nameof(strings), strings, null),
        };
    }

    internal static T ToEnum<T>(string newWeaponName)
    {
        throw new NotImplementedException();
    }
}
