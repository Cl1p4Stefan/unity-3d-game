using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("ms^-1")][SerializeField] float controlSpeed = 20f;
    [Tooltip("m")][SerializeField] float xRange = 5.1f;
    [Tooltip("m")][SerializeField] float yRange = 2.8f;
    [SerializeField] GameObject[] guns;

    [Header("Screen position Based")]
    [SerializeField] float positionPitchFactor = -1.8f;
    [SerializeField] float positionYawFactor = 1f;
 

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Start is called before the first frame update

    /*private*/ /*void OnCollisionEnter(Collision collision)
    {
        print("Player collided with something");
    }
    */
    /*private*/ 

    // Update is called once per frame
    void Update()
    {
        if(isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }     
    }

    void OnPlayerDeath() // called by string reference
    {
        isControlEnabled = false;
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControl;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * controlSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos/*rawXPos*/, clampedYPos/*transform.localPosition.y*/, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if(CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach(GameObject gun in guns)
        {
            //gun.SetActive(isActive);
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

    /*private void DeactivateGuns()
    {
        foreach(GameObject gun in guns)
        {
            gun.SetActive(false);
        }
    }
    */

}
