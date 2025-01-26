using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAvatarList", menuName = "Data/PlayerAvatarList")]
public class PlayerAvatarList : ScriptableObject
{
    [SerializeField] public List <PlayerAvatar> playerAvatars = new List<PlayerAvatar>();
}
