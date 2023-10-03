﻿using System.Collections.Generic;

namespace HelperPSR.MonoLoopFunctions
{
    public class LateUpdateManager : MonoLoopFunctionsManager<ILateUpdate>
    {
        public override void UpdateElement(HashSet<ILateUpdate>.Enumerator e)
        {
            e.Current.OnLateUpdate();
        }

        public void LateUpdate()
        {
            LaunchLoop();
        }
    }
}