using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour
{

    //need to fix lighting bug upon level reload
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    public int Health = 100;
    public Slider healthSlider;
    public int Wildness = 100;
    public Slider WildnessSlider;
    public Rigidbody rigidBody;
    public AudioSource audioSource;
    public AudioSource damageClip;

    enum State { Alive, Dying, Transcending }

    private State state = State.Alive;

	// Use this for initialization
	private void Start ()
	{
	    rigidBody = GetComponent<Rigidbody>();
	    audioSource = GetComponent<AudioSource>();
        damageClip = GetComponent<AudioSource>();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;
            case "Treat":
                state = State.Transcending;
                print("Yum");
                Invoke("LoadNextScene", 3f);
                break;
            case "Enemy":
                damageClip.Play();
                print("Dang");
                Health -= 25;
                //healthSlider.value = Health;
                if(Health == 0)
                {
                    state = State.Dying;
                    print("RIP Henry");
                    Invoke("RestartGame", 1f);
                    }
                //kill
                break;
            case "Default":
                break;
           
        }
        
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
