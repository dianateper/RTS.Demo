using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

namespace CodeBase.PlayerLogic
{
    public interface IPlayerBase
    {
        List<Player> Alliance { get; }
        
        List<Player> Enemies { get; }
        IPlayerStats PlayerStats { get; }

        void MakeAlliance(Player player);
        void Attack(Player player);
        void SetPlayerNetwork(PlayerNetwork player);
    }
}