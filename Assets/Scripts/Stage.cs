using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stage {
    public FlyMode flyMode;
    public List<PathConfigSO> pathConfigSOList;
    public ShapeConfigSO shapeConfigSO;
    public int enemyAmount;
}
