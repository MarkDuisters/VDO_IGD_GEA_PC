using System;
using UnityEngine;

public class ToggleCameraMode : MonoBehaviour
{
    enum CameraMode { FirstPerson, ThirdPerson }
    [SerializeField] CameraMode currentCameraMode = CameraMode.FirstPerson;
    [SerializeField] GameObject[] firstPersonObjects;//Enable alle objecten DIE bij de fps camera horen.
    [SerializeField] GameObject[] thirdPersonObjects;// visa versa
    PlayerController getPlayerController => GetComponent<PlayerController>();

    void OnToggleCamera()
    {
        switch (currentCameraMode)
        {
            case CameraMode.FirstPerson:
                print("Switch to Third Person");
                currentCameraMode = CameraMode.ThirdPerson;
                SwapObjects(false);
                getPlayerController.SetMoveMode(PlayerController.MoveMode.ThirdPersonMove);
                break;
            case CameraMode.ThirdPerson:
                print("Switch to First Person");
                currentCameraMode = CameraMode.FirstPerson;
                SwapObjects(true);
                getPlayerController.SetMoveMode(PlayerController.MoveMode.FirstPersonMove);
                break;
        }
    }
    //Door een bool paramater te gebruiken. Kunnen we zorgen dat we maar 1 functie nodig hebben. 
    //We bekijken het van het perspectief van onze first person objects of deze aan of uitgezet moeten worden.
    //En onze third person objects doen het tegenovergestelde. Dit komt omdat we de bool inverten met !.
    void SwapObjects(bool fspObjectsEnabled)
    {
        foreach (GameObject obj in firstPersonObjects)
        {
            obj.SetActive(fspObjectsEnabled);
        }
        foreach (GameObject obj in thirdPersonObjects)
        {
            obj.SetActive(!fspObjectsEnabled);
        }
    }

    /*//Originele methodes die de loops nog apart hadden. 
    // Deze zijn vervangen door de SwapObjects() methode hierboven.
        void SwitchToFirstPerson()
        {
            foreach (GameObject obj in firstPersonObjects)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in thirdPersonObjects)
            {
                obj.SetActive(false);
            }
        }

        void SwitchToThirdPerson()
        {
            foreach (GameObject obj in firstPersonObjects)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in thirdPersonObjects)
            {
                obj.SetActive(true);
            }
        }
    */
}
