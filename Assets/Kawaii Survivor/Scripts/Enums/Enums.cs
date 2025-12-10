public enum GameState
{
    MENU,
    WEAPON_SELECT,
    GAME,
    GAMEOVER,
    STAGE_COMPLETE,
    WAVETRANSITION,
    SHOP
}
public enum PlayerState
{
    Attack,
    AttackSpeed,
    CriticalChance,
    CriticalPercent, 
    MoveSpeed, 
    MaxHealth, 
    Range, 
    HealthRecoverySpeed, 
    Armor,
    Luck,
    Dodge,
    LifeSteal
}
public static class Enums
{
    public static string GetPlayerStateName(PlayerState gameState)
    {
        string stateName = "";
        string stateNameString= gameState.ToString();
        if(stateNameString.Length<=0)
            return "??????????????";

        for (int i = 0; i < stateNameString.Length; i++)
        {
            if (i > 0 && char.IsUpper(stateNameString[i]))
            {
                stateName += "  ";
            }
            stateName += stateNameString[i];
        }
        return stateName;
    }
    public static string GetPlayerStateName(GameState gameState)
    {
        string stateName = "";
        string stateNameString = gameState.ToString();
        if (stateNameString.Length <= 0)
            return "??????????????";

        for (int i = 0; i < stateNameString.Length; i++)
        {
            if (i > 0 && char.IsUpper(stateNameString[i]))
            {
                stateName += "  ";
            }
            stateName += stateNameString[i];
        }
        return stateName;
    }
}
