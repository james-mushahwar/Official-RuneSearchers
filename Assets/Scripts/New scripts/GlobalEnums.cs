using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global
{
    public class GlobalEnums
    {
        public enum RuneType { None, Red, Yellow, Blue }
        public enum RuneSize { Small, Medium, Large }
        public enum RaceType { None, Wild, Royal, Outcast, Beserk, Ethereal, Underworld }
    }

    public static class GlobalExtensions
    {
        public static bool IsRuneTypeEffective(GlobalEnums.RuneType a, GlobalEnums.RuneType b)
        {
            return ((a == GlobalEnums.RuneType.Red && b == GlobalEnums.RuneType.Blue) ||
                (a == GlobalEnums.RuneType.Yellow && b == GlobalEnums.RuneType.Red) ||
                (a == GlobalEnums.RuneType.Blue && b == GlobalEnums.RuneType.Yellow));
        }

        public static bool IsRuneTypeIneffective(GlobalEnums.RuneType a, GlobalEnums.RuneType b)
        {
            return ((a == GlobalEnums.RuneType.Red && b == GlobalEnums.RuneType.Yellow) ||
                (a == GlobalEnums.RuneType.Yellow && b == GlobalEnums.RuneType.Blue) ||
                (a == GlobalEnums.RuneType.Blue && b == GlobalEnums.RuneType.Red));
        }
    }
}

