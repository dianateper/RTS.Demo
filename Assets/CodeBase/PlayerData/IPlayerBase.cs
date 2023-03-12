using System.Collections.Generic;
using Photon.Realtime;

namespace CodeBase.PlayerData
{
    public interface IPlayerBase
    {
        List<Player> Alliance { get; }
        
        List<Player> Enemies { get; }
        
        void MakeAlliance(Player player);
        void Attack(Player player);
    }
}