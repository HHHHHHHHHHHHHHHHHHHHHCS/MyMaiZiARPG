using UnityEngine;
using System.Collections;

public class GameDefine
{


    public const string startScene = "Start";
    public const string CreateCharaScene = "CreateCharacter";

    public const string playerName = "playerName";
    public const string playerRole = "playerRole";

    public const string warriorPath = "Prefabs/Character/Warrior";
    public const string archerPath = "Prefabs/Character/Archer";
    public const string enemyPath = "Prefabs/Character/Enemy1";

    public const string animIdle = "Idle";
    public const string animRun = "Run";
    public const string animDeath = "Death";
    public const string animHit = "Hit";
    public const string animAttack1 = "Attack1";
    public const string animAttack2 = "Attack2";

    public const string warriorSkill1 = "Prefabs/FX/WarriorSkill1";
    public const string warriorSkill2 = "Prefabs/FX/WarriorSkill2";

    public const string archerSkill1 = "Prefabs/FX/ArcherSkill1";
    public const string archerSkill2 = "Prefabs/FX/ArcherSkill2";
    public const string atcherSkillHit = "Prefabs/FX/ArrowHit";

    public const string uiCamera = "UICanvas/UICamera";
    public const string bloodSliderPath = "UICanvas/HpBars";
    public const string bloodSlider = "Prefabs/UI/BloodSlider";

    public const float skillFXForeverLifeTime = -100f;
}
public class PlayerSaveDefine
{
    public const string playerSaveName = "playerInfoData";

    public const string key_playerName = "playerName";
    public const string key_playerRole = "playerRole";

}

public class SceneNameDefine
{
    public const string StartGame = "StartGame";

    public const string CreateCharacter = "CreateCharacter";

    public const string Scene_Forest = "Scene_Forest";
}


public enum CharacterType
{
    none = -1,
    warrior = 0,
    archer = 1
}
