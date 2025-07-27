using Assets.Scripts;
using Model;
using System.Collections;
using UnityEngine;

namespace Model
{
    public class BoardCompleteModel : BaseModel
    {
        public int CollectedScore { get; set; }
        public int ClicksScore { get; set; }
    }
}