using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings {

    public static bool p1, p2, p3, p4;

    public static bool IsActive(int id)
    {
        if (!p1 && !p2 && !p3 && !p4){
            return true;
        }
        switch (id)
        {
            case 1:
                return p1;
            case 2:
                return p2;
            case 3:
                return p3;
            case 4:
                return p4;
            default:
                return false;
        }
    }
}
