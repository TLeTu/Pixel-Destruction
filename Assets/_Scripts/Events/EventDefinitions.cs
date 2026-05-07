using UnityEngine;

/// <summary>
/// Interface cơ sở cho tất cả các sự kiện trong game.
/// </summary>
public interface IGameEvent { }

// --- Game Flow Events ---

// Phát ra bởi ScoreManager khi đạt ngưỡng điểm.
public struct ScoreThresholdReachedEvent : IGameEvent { }

// Phát ra bởi ScoreManager khi đạt mục tiêu điểm của level.
public struct LevelWinEvent : IGameEvent { }

// Phát ra bởi ObstacleManager khi hoàn thành việc đặt vũ khí.
public struct WeaponPlacementFinishedEvent : IGameEvent { }

// Phát ra bởi UpgradeBtnController khi người chơi chọn một nâng cấp.
public struct UpgradeSelectedEvent : IGameEvent
{
    public readonly WeaponUpgrade Upgrade;
    public UpgradeSelectedEvent(WeaponUpgrade upgrade) { Upgrade = upgrade; }
}

// --- UI Interaction Events ---
public struct WeaponSlotClickedEvent : IGameEvent { public readonly GameObject Obstacle; public WeaponSlotClickedEvent(GameObject obstacle) { Obstacle = obstacle; } }
public struct PlayButtonPressedEvent : IGameEvent { }
public struct NextLevelButtonPressedEvent : IGameEvent { }
public struct BackToMenuButtonPressedEvent : IGameEvent { }
public struct ReplayLevelButtonPressedEvent : IGameEvent { }