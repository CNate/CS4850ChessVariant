using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
namespace Piece
{

// abstract class representing the additional duties a commander piece has 
// over the standard piece
public abstract class CommanderPiece : GamePieceBase {
    // the list of soliders this commander owns
    public List<GameObject> soldiers_ { get; protected set; }

    // a list that is used on object creation to indicate which soliders 
    // this piece will own, and is used to spawn each of those soliders in
    protected List<(GameObject, Definitions.BoardPosition)> spawnList_;

    public uint energy { get; protected set; } 

    public Control.PlayerBase owner { get; protected set; } 

    // Object init specific to commanders, allowing each of them to 
    // specify what pieces they will command and spawn them in
    public virtual List<GameObject> commander_init(
        bool is_white, 
        Definitions.BoardPosition starting_position,
        Definitions.PrefabCollection prefabs,
        BoardController controller,
        Control.PlayerBase owner
    ) {
        // do the standard piece init as well
        this.init(is_white, starting_position, controller, prefabs);

        this.energy = 1;

        this.owner = owner;

        // required, but does not return anything
        return soldiers_;
    }

    // given the spawnList set during commander init, spawn in each soldier 
    // specified
    protected void spawn_units(BoardController controller) {
        // reset the soldier list and reserve enough space for our units
        soldiers_ = new List<GameObject>(spawnList_.Count);

        // for each unit thats been defined, spawn it in and call its init function 
        // with the board position specified
        foreach((GameObject piece, Definitions.BoardPosition pos) in spawnList_)
        {
            // spawn the soldier
            soldiers_.Add(
                GameObject.Instantiate(piece)
            );

            // call its init function to sync it with the board
            soldiers_[soldiers_.Count - 1].GetComponent<Piece.SoldierPiece>().soldier_init(
                is_white,
                pos,
                this.gameObject,
                controller,
                prefabs_
            );
        }
    }

    // function dealing with delegating out a command for each of its soldiers to explore its space
    // also calls its own explore function itself
    public void commander_explore(ref HashSet<Definitions.Action> results) {
        // only search if we haven't moved our corp yet
        if(energy <= 0) {
            Debug.Log($"{this} corp is out of energy!");

            foreach(var soldier in soldiers_)
            {
                soldier.GetComponent<GamePieceBase>().Select(
                    new Color(1f, 0f, 0f)
                );
            }

            this.Select(
                new Color(1f, 0f, 0f)
            );
 
            return;
        }

        // search the entire corp
        foreach(var soldier in soldiers_)
            soldier.GetComponent<GamePieceBase>().Explore(ref results);

        // search ourself as well
        this.Explore(ref results);

    }

    public override bool expend_energy(uint cost) {
        if(cost <= energy) {
            energy -= cost;
            return true;
        }
        return false;
    }

    public override void set_inactive()
    {
        // cause this piece to become inactive
        base.set_inactive();
    }

    public override void kill()
    {
        owner.remove_commander(this.gameObject);
    }

    public void end_turn() {
        foreach(var soldier in soldiers_)
        {
            soldier.GetComponent<GamePieceBase>().Deselect();
        }
        this.energy = 1;

        this.Deselect();
    }

    public void begin_turn() {
        Debug.Log($"{this} refreshing energy");
    }

    public void remove_soldier(GameObject soldier) {
        Debug.Log($"{this}: killing soldier {soldier.GetComponent<Piece.GamePieceBase>()}");

        soldiers_.Remove(
            soldier
        );

        Destroy(soldier);
    }

    // forwarding the abstract piece explore function
    public override abstract void Explore(ref HashSet<Definitions.Action> results);
}


} // Piece
} // Chess
