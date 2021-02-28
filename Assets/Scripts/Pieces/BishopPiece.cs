using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess 
{
namespace Piece
{
    
public class BishopPiece : CommanderPiece {
    // Start is called before the first frame update

    public override List<GameObject> commander_init(
        bool is_white, 
        Definitions.BoardPosition starting_position,
        Definitions.PrefabCollection prefabs,
        BoardController controller
    ) {
        // generic commander initialization details
        base.commander_init(
            is_white,
            starting_position,
            prefabs,
            controller
        );

        // determine which commander bishop this is
        bool is_left = (this.position.file == 3) ? true : false;

        spawnList_ = new List<(GameObject, Definitions.BoardPosition)>()
        {
            (prefabs_.Pawn,     new Definitions.BoardPosition(is_left ? 1 : 8, is_white ? 2 : 7)),
            (prefabs_.Pawn,     new Definitions.BoardPosition(is_left ? 2 : 7, is_white ? 2 : 7)),
            (prefabs_.Pawn,     new Definitions.BoardPosition(is_left ? 3 : 6, is_white ? 2 : 7)),
            (prefabs_.Knight,   new Definitions.BoardPosition(is_left ? 2 : 7, is_white ? 1 : 8))
        };

        this.spawn_units(controller); 
        return soldiers_;
    }

    // @TODO: IMPLEMENT ME
    public override List<Definitions.Action> Explore() {
        return Exploring.ForwardExplore.Explore(this.gameObject);
    }

    public override PieceType type { get; } = PieceType.Bishop;
}

}
}
