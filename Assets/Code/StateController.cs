using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController
{
    enum states
    {
        ISLAND_VIEW,
        HEX_VIEW,
        UI_VIEW
    }

    states state = states.ISLAND_VIEW;

    private static readonly StateController instance = new StateController();
    static StateController() { }
    public StateController() { }
    public static StateController Instance
    {
        get
        {
            return instance;
        }
    }

    public bool IsIslandView()
    {
        if(state == states.ISLAND_VIEW) { return true; }
        return false;
    }
    public bool IsHexView()
    {
        if (state == states.HEX_VIEW) { return true; }
        return false;
    }

    public void SetIslandView()
    {
        state = states.ISLAND_VIEW;
    }
    public void SetHexView()
    {
        state = states.HEX_VIEW;
    }
}
