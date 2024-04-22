using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private void OnDestroy()
    {
        GameRoot.Instance.bossKilledCount += 1;
    }
}
