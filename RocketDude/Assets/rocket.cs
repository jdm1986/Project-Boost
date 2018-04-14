using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    private Rigidbody rigidBody;
    private AudioSource audioSource;

	// Use this for initialization
	private void Start ()
	{
	    rigidBody = GetComponent<Rigidbody>();
	    audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	private void Update ()
	{
	    ProcessInput();
	}

    private void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; //take manual control of rotation
        
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector2.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector2.right);
        }

        rigidBody.freezeRotation = false; //resume physics control of rotation
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;
            case "Treat":
                print("Yum");
                //add 1 to wildness
                break;
            case "Enemy":
                print("Dead");
                //kill
                break;
            case "Default":
                break;
           
        }
    }
}
