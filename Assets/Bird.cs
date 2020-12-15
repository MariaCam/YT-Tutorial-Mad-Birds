using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour { //the : MonoBehavoir is like "implements MonoBehaviour" in Java

    Vector3 _initialPosition;
    bool _birdWasLaunched = false;
    float _timeSittingAround;

    [SerializeField] float _launchPower = 500; //the serializeField is so that it shows up in the unity interface


    private void Awake(){
        _initialPosition = transform.position; //record the bird's initial position
    }

    private void Update(){ //it's called once per frame

    //the trail starts where the bird is
    GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
    //when you move the bird the line moves as well
    GetComponent<LineRenderer>().SetPosition(0, transform.position);

        //this check whether your bird is completely still or rocking, if it is, it starts counting how much time it's been like that
        if(_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1){
            _timeSittingAround += Time.deltaTime; //Time.deltaTime is the amount of time since the last frame
        }


        //if the bird leaves the view then reset the scene to how it was at the beginning
        if(transform.position.y > 7 || transform.position.x < -25 || transform.position.x > 25 || _timeSittingAround > 3){
            string currentSceneName = SceneManager.GetActiveScene().name;

            //load scene accepts the name of the scene which you want to load
            SceneManager.LoadScene(currentSceneName);
        }

    }

    private void OnMouseDown(){ //called everytime we click on the bird
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp(){ //called everytime we release the click on the bird
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 directionToInitialPosition = _initialPosition - transform.position;

        //this is what lets the bird move in the desired direction, it does not stop when it reaches the initial position
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);

        //this is what lets the bird fall down in an arc, otherwise it would travel in a straight line
        GetComponent<Rigidbody2D>().gravityScale = 1;

        //indicates the bird was launched 
        _birdWasLaunched = true;
        
        //turns the path trail off
        GetComponent<LineRenderer>().enabled = false;
    }

    private void OnMouseDrag(){
        // transform.position = Input.mousePosition; this doesn't work because the 
        //screenspace coordinates and the worldspace coordinates are different

        Vector3 newPosition =  Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(newPosition.x, newPosition.y); //the new vector
        //part is so that the bird z position doesn't chnage, otherwise the bird would become invisible

    }

}
