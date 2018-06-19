using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public CharacterController CharContr;

    public float Speed;
    public float JumpSpeed;
    public float FallSpeed;

    public float TurnSpeed;

    private bool Jumping;

    private bool CanMove;

    private float MaxSpeed;
    private float MinSpeed;

    private float Axis;

    private Vector3 LastPos;

	void Awake () {

        Speed = 60f;
        JumpSpeed = 30f;
        FallSpeed = 30f;

        TurnSpeed = 90f;

        Jumping = false;
        CanMove = false;

        MaxSpeed = 100f;
        MinSpeed = 60f;

        Axis = 0;

        LastPos = transform.position;

        CanMove = false;
	}
	
	void FixedUpdate () {

        if(!CanMove)
            return;

        if (!Jumping)
            CharContr.Move(-transform.up * FallSpeed * Time.fixedDeltaTime);

        if (!CharContr.isGrounded && !Jumping)
            FallSpeed += 1f ;

        if (CharContr.isGrounded)
        {
            Axis = Input.GetAxis("Vertical");
            Jumping = false;
            FallSpeed = 0f;
            if (Input.GetKey(KeyCode.Joystick1Button0))
            {
                StartCoroutine("Jump");
                Jumping = true;
            }
        }

        if (Vector3.Distance(transform.position, LastPos) / Time.fixedDeltaTime < .9 * Speed )
        {
            Speed -= 5;
            if (Speed < MinSpeed)
                Speed = MinSpeed;
        }

        else if (Input.GetAxis("Vertical") > .75)
        {
            Speed += Time.fixedDeltaTime * 25;
            if (Speed > MaxSpeed)
                Speed = MaxSpeed;
        }

        else if (CharContr.isGrounded)
        {
            Speed -= Time.fixedDeltaTime * 50;
            if(MinSpeed > Speed)
                Speed = MinSpeed;
        }

        if (Input.GetKey(KeyCode.Joystick1Button7))
        {
            Application.LoadLevel(Application.loadedLevel);
            Time.timeScale = 1f;
        }
     
        LastPos = transform.position;
        this.transform.Rotate(new Vector3(0, Input.GetAxis("XBox Horizontal") * TurnSpeed * Time.fixedDeltaTime, 0));
        CharContr.Move(transform.forward * Axis * Speed * Time.fixedDeltaTime);
    }

    public void SetMove(bool Move)
    {
        CanMove = Move;
    }

    IEnumerator Jump()
    {
        JumpSpeed = 30f;

        for (int i = 0; i < 60; i++)
        {
            CharContr.Move(Vector3.up * 0.015f * JumpSpeed );
            JumpSpeed -= .5f;
            yield return new WaitForSeconds(0.015f);
        }
        Jumping = false;
    }
}
