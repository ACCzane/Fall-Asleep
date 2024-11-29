
public enum FaceDir
{
    up,
    right,
    down,
    left
}

public enum EnemyState{
    Move,
    Alert,
    ResetView,
    FoundPlayer,
    FoundFixedTank
}

public enum InteractType{
    Hide,
    Sleep,
    Null            //表示当前按“E”无效
}

public enum NightStat{
    Normal,
    Tired,
    Drunk
}