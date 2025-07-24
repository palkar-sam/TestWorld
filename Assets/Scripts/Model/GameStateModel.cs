using Assets.Scripts;
using Model;
using System.Collections;
using UnityEngine;

namespace Model
{
    public class GameStateModel : BaseModel
    {
        public GameConstants.GameState GameState { get; set; }
    }
}