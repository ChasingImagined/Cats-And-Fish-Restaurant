using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputEvents 
{

   

    public struct E_OnclickStart :IEvent
    {
        public Vector2 firstDot;
    }

    public struct E_OncickStay : IEvent
    {
        public Vector2 firstDot;
        public Vector2 lastDot;
    }


    public struct E_OnclikStop : IEvent
    {
        public Vector2 firstDot;
        public Vector2 lastDot;
    }
}


