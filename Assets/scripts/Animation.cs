using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private Animator anim;
    private CharacterController characterController;
    private Vector3 Velocity;
    public float JumpPower;
    
    public float MoveSpeed;

	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationX = 0f;
	float rotationY = 0f;

	public GameObject VerRot;
	public GameObject HorRot;

	public GameObject gun;
	public Transform muzzle;
	[SerializeField]
	private float bulletSpeed = 5000;
	private Vector3 force;

	public AudioSource shot;



	void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
	}

	void shoot()
    {
		if (Input.GetMouseButtonDown(0))// 左クリック
		{
			GameObject guns = Instantiate(gun) as GameObject;
			guns.transform.position = muzzle.transform.position;
			force = this.gameObject.transform.forward * bulletSpeed;
			guns.GetComponent<Rigidbody>().AddForce(force);
			Destroy(guns.gameObject, 4);
			shot.Play();
		}
	}
    
    void Update()
    {
		rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
		VerRot.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
		HorRot.transform.localEulerAngles = new Vector3(0, rotationX, 0);


		if (Input.GetKey(KeyCode.W))
        {
			characterController.Move(this.gameObject.transform.forward * MoveSpeed * 2 * Time.deltaTime);
			anim.SetBool("run", true);

			if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            {
				anim.SetBool("run", false);
				anim.SetBool("gun", true);
				shoot();
			}

		}

        else if (Input.GetKeyUp(KeyCode.W)|| (Input.GetKeyUp(KeyCode.W) && Input.GetKeyUp(KeyCode.LeftShift)))
        {
            anim.SetBool("run", false);
			anim.SetBool("gun", false);
		}

		if (Input.GetKey(KeyCode.S))
		{
			characterController.Move(this.gameObject.transform.forward * -1f * MoveSpeed * Time.deltaTime);
			anim.SetBool("run", true);

			if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
			{
				anim.SetBool("run", false);
				anim.SetBool("back", true);
				shoot();
			}
		}

		else if (Input.GetKeyUp(KeyCode.S) || (Input.GetKeyUp(KeyCode.S) && Input.GetKeyUp(KeyCode.LeftShift)))//②Sキーがはなされたら 
		{
			anim.SetBool("run", false);
			anim.SetBool("back", false);
		}

		if (Input.GetKey(KeyCode.A))
		{
			characterController.Move(this.gameObject.transform.right * -1 * MoveSpeed * Time.deltaTime);
			anim.SetBool("run", true);
		}

		else if (Input.GetKeyUp(KeyCode.A))
		{
			anim.SetBool("run", false);
		}

		if (Input.GetKey(KeyCode.Q))
		{
			characterController.Move(this.gameObject.transform.forward * 30 * Time.deltaTime);
			anim.SetBool("slid", true);
		}
		else if (Input.GetKeyUp(KeyCode.Q))
		{
			anim.SetBool("slid", false);
		}


		if (Input.GetKey(KeyCode.D))
		{
			characterController.Move(this.gameObject.transform.right * MoveSpeed * Time.deltaTime);
			anim.SetBool("run", true);
		}

		else if (Input.GetKeyUp(KeyCode.D))
		{
			anim.SetBool("run", false);
		}

		characterController.Move(Velocity);
		Velocity.y += Physics.gravity.y * Time.deltaTime;

		if (characterController.isGrounded)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Velocity.y = JumpPower;
			}
		}

		
	}
}
