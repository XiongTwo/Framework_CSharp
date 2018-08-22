using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManage {

    private static ControllerManage instance;
    public static ControllerManage Instance
    {
        get
        {
            if (instance == null)
                instance = new ControllerManage();
            return instance;
        }
    }

    public ExampleControll mExampleControll;

    public LoginControll mLoginControll;
    public LobbyControll mLobbyControll;
    public MarqueeController mMarqueeController;
    public SetControll mSetControll;
    public CombatController mCombatController;

    public void Init()
    {
        mExampleControll = new ExampleControll();

        mLoginControll = new LoginControll();
        mLobbyControll = new LobbyControll();
        mMarqueeController = new MarqueeController();
        mSetControll = new SetControll();
        mCombatController = new CombatController();
    }
}
