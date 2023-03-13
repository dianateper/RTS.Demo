using System.Collections.Generic;
using Photon.Realtime;

namespace CodeBase.PlayerLogic
{
    public interface IPlayerBase
    {
        List<Player> Alliance { get; }
        
        List<Player> Enemies { get; }
        
        void MakeAlliance(Player player);
        void Attack(Player player);
        void SetPlayerMono(PlayerMono player);
    }
}