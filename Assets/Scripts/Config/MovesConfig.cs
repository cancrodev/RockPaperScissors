using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GlobalEnums;

[CreateAssetMenu(fileName = "MovesConfig", menuName = "Create MovesConfig")]
public class MovesConfig : ScriptableObject
{
    public List<MoveData> _allMovesList;

    public MoveData GetDataForMove(MoveType currMove)
    {
        return _allMovesList.Where(data => data._moveType.Equals(currMove)).FirstOrDefault();
    }

    public string GetEffectName(MoveType winningMove, MoveType losingMove)
    {
        return _allMovesList.Where(data => data._moveType.Equals(winningMove)).FirstOrDefault().
            _relations.Where(relation => relation._otherMoveType.Equals(losingMove)).Select(relation => relation._effectName).FirstOrDefault();
    }
}

[Serializable]
public class MoveData
{
    public MoveType _moveType;
    public Sprite _sprite;
    public List<MoveRelation> _relations;
}

[Serializable]
public class MoveRelation
{
    public string _effectName;
    public MoveType _otherMoveType;
}