using UnityEngine;
using UnityEngine.Audio;

public class ExerciseManager : MonoBehaviour
{
    private FPController player;
    public AudioMixer mixer;

    public float RoomTransitionValue {get; private set;}

    private float minX = -2.68f;
    private float maxX = 6.25f;

    private float maxdB = 0;
    private float mindB = -40;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<FPController>();

    }

    void Update()
    {
        float playerX = player.transform.position.x;
        RoomTransitionValue = Util.RemapRange(playerX, minX, maxX, 0, 1);

        float deciblesB = Mathf.Clamp(Util.RemapRange(RoomTransitionValue, 0, 1, mindB, maxdB), mindB, maxdB);
        float deciblesA = Mathf.Clamp(Util.RemapRange(RoomTransitionValue, 1, 0, mindB, maxdB), mindB, maxdB);

        if (mixer == null) return;



        mixer.SetFloat("RoomBVolume", deciblesB);
        mixer.SetFloat("RoomAVolume", deciblesA);


        if (player.CurrentSurface.CompareTag("Rug"))
        {
            mixer.SetFloat("footstepCutoff", 200);
        }
        else
        {
            mixer.SetFloat("footstepCutoff", 5000);
        }

    }


}
