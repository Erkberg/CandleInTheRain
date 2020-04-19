using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCams : MonoBehaviour
{
    public Camera mainCam;
    public Camera candleCam;

    public List<GameObject> vCams;
    public CamState currentState;

    public void SetCamState(CamState state)
    {
        DisableAllvCams();
        currentState = state;
        GetvCamByCurrentState().SetActive(true);
    }

    private void DisableAllvCams()
    {
        foreach (GameObject vCam in vCams)
            vCam.SetActive(false);
    }

    private GameObject GetvCamByCurrentState()
    {
        switch(currentState)
        {
            case CamState.ThirdPersonFollow:
                return vCams[0];

            case CamState.CandleFocus:
                return vCams[1];

            case CamState.PlantInteractionArea:
                return vCams[2];

            case CamState.WallInteractionArea:
                return vCams[3];

            case CamState.TreeInteractionArea:
                return vCams[4];
        }

        return null;
    }

    public enum CamState
    {
        ThirdPersonFollow,
        CandleFocus,
        PlantInteractionArea,
        WallInteractionArea,
        TreeInteractionArea,
        DoggyInteractionArea
    }
}
