class PlayerInPlatform
{
    private static PlayerInPlatform _instance = null;

    private PlayerInPlatform()
    {
    }

    private int playerState = 1;

    public static PlayerInPlatform GetInstance()
    {
        if (_instance == null)
        {
            _instance = new PlayerInPlatform();
        }
        return _instance;
    }
    public void PlatformA()
    {
        playerState = 0;
    }

    public void PlatformB()
    {
        playerState = 1;
    }

    public void PlatformC()
    {
        playerState = 2;
    }

    public int GetPlatform()
    {
        return playerState;
    }
}