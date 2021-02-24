using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess 
{
namespace Piece
{
    
public class KingPiece : CommanderPiece {
    public override List<Definitions.Action> Explore() {
        return new List<Definitions.Action>();
    }

    public override List<GameObject> commander_init(
        bool is_white, 
        Definitions.BoardPosition starting_position,
        Definitions.PrefabCollection prefabs,
        ref BoardController controller
    ) {
        // generic commander initialization details
        base.commander_init(
            is_white,
            starting_position,
            prefabs,
            ref controller
        );

        spawnList_ = new List<(GameObject, Definitions.BoardPosition)>()
        {
            (prefabs_.Rook,     new Definitions.BoardPosition(1, is_white ? 1 : 8)),
            (prefabs_.Queen,    new Definitions.BoardPosition(4, is_white ? 1 : 8)),
            (prefabs_.Pawn,     new Definitions.BoardPosition(4, is_white ? 2 : 7)),
            (prefabs_.Pawn,     new Definitions.BoardPosition(5, is_white ? 2 : 7)),
            (prefabs_.Rook,     new Definitions.BoardPosition(8, is_white ? 1 : 8))
        };

        this.spawn_units(ref controller); 
        return soldiers_;

    }
    void Start() {
        this.type = PieceType.King;
    }
}

} // Piece
} // Chess
