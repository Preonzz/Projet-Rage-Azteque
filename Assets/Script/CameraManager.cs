using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Vector3 baseCameraPosition;
    Camera mainCamera;
    public GameObject player;
    public bool isCameraShaking;
    public float currentShakePower = 1;
    public float cameraReturnSpeed = 1;

    private void Start()
    {
        mainCamera = Camera.main;
        baseCameraPosition = new Vector3 (player.transform.position.x, player.transform.position.y, -13) ;
    }

    // Update is called once per frame
    void Update()
    {
        ShakeCameraUpdate();
        baseCameraPosition = new Vector3(player.transform.position.x, player.transform.position.y, -13);
    }

    public IEnumerator ShakeCamera(float power, float shakeTime)
    {
        isCameraShaking = true;
        currentShakePower = power;

        yield return new WaitForSeconds(shakeTime);

        isCameraShaking = false;
    }

    void ShakeCameraUpdate()
    {
        if (isCameraShaking)
        {
            mainCamera.transform.position = baseCameraPosition +
            new Vector3(Random.Range(-0.1f, 0.1f) * currentShakePower,
                        Random.Range(-0.1f, 0.1f) * currentShakePower,
                        0f);
        }

        else mainCamera.transform.position = Vector3.MoveTowards(
            mainCamera.transform.position,
            baseCameraPosition, Time.deltaTime * cameraReturnSpeed);
    }

}
