using UnityEngine;

public class SlalomFlag : MonoBehaviour
{
    private enum Direction
    {
        Left,
        Right
    };

    [SerializeField] private Direction direction;

    private bool passed = false;
    
    // Update is called once per frame
    void Update()
    {
        if (PlayerControl.Instance.transform.position.z < transform.position.z && !passed)
        {
            passed = true;
            Debug.Log("player passed flag");

            Direction passingDir = Direction.Left;
            if (PlayerControl.Instance.transform.position.x > transform.position.x)
            {
                passingDir = Direction.Right;
            }
            Debug.Log("player passed flag on " + passingDir);
            if (passingDir != direction)
            {
                Debug.Log("penalty");
            }
        }
    }
}
