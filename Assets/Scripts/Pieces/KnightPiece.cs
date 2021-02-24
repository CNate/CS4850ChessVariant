using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess 
{
namespace Piece
{

public class KnightPiece : SoldierPiece
{
    public override List<Definitions.Action> Explore()
    {
        return new List<Definitions.Action>();
    }

    void Start()
    {
        this.type_ = PieceType.Knight;
    }

}

}
}
