using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerConroller : MonoBehaviour
{
    [SerializeField] private float angleTargetPos = 50f;
    [SerializeField] private float damper = 15f;
    [SerializeField] private float spring = 20000f;

    private bool isChanged = false;
    private HingeJoint leftJointFlipper;
    private HingeJoint rightJointFlipper;
    private AudioSource sound;
    private void Start()
    {
        leftJointFlipper = GameObject.Find("LFlipper").GetComponent<HingeJoint>();
        rightJointFlipper = GameObject.Find("RFlipper").GetComponent<HingeJoint>();
        sound = leftJointFlipper.GetComponentInChildren<AudioSource>();
    }
    public void LeftFlipperUp()
    {
        JointSpring spring = new JointSpring
        {
            targetPosition = angleTargetPos,
            damper = damper,
            spring = this.spring
        };
        sound.Play();
        leftJointFlipper.spring = spring;
        isChanged = true;
    }
    public void RightFlipperUp()
    {
        JointSpring spring = new JointSpring
        {
            targetPosition = -angleTargetPos,
            damper = damper,
            spring = this.spring
        };
        sound.Play();
        rightJointFlipper.spring = spring;
        isChanged = true;
    }

    public void ReturnToNormal()
    {
        if (isChanged) 
        {
            JointSpring spring = new JointSpring
            {
                targetPosition = 0f,
                damper = damper,
                spring = this.spring
            };
            rightJointFlipper.spring = spring;
            leftJointFlipper.spring = spring;
        }
    }
}
