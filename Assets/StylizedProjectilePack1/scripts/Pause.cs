class Pause
{
    private static Pause _instance = null;   //静态私有成员变量，存储唯一实例

    private Pause()    //私有构造函数，保证唯一性
    {
    }

    private bool pause;

    public static Pause GetInstance()    //公有静态方法，返回一个唯一的实例
    {
        if (_instance == null)
        {
            _instance = new Pause();
        }
        return _instance;
    }
    public void StopGame()
    {
        pause = true;
    }

    public void ContinueGame()
    {
        pause = false;
    }

    public bool GetState()
    {
        return pause;

    }
}