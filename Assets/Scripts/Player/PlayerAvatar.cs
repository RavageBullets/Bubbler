using UnityEngine;
using UnityEngine.U2D.IK;

public class PlayerAvatar : MonoBehaviour
{
    [SerializeField] private LimbSolver2D leftArmIK;
    [SerializeField] private LimbSolver2D rightArmIK;
    public SpriteRenderer LeftArm;
    public SpriteRenderer RightArm;
    public Transform LeftAnchor;
    public Transform RightAnchor;

    public Sprite staticSprite;

    public void UpdateInverseKinematics (Transform gunAnchorL, Transform gunAnchorR) {
        leftArmIK.GetChain(2).target = gunAnchorL;
        rightArmIK.GetChain(2).target = gunAnchorR;
    }
}

