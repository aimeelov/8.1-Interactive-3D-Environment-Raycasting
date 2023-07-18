using UnityEngine;
using UnityEngine.Video;

public class TV : MonoBehaviour
{
    VideoPlayer videoPlayer;
    Renderer renderer; // to handle the blank screen when the TV is off

    // Specify the blank screen color here
    public Color offColor = Color.black;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        renderer = GetComponent<Renderer>();  // Assuming the TV screen is the cube's material

        TurnOffTV();
    }


    void Update()
    {
        // Check if the 'R' key was pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleVideo();
        }
    }

    public void ToggleVideo()
    {
        if (videoPlayer.isPlaying)
        {
            TurnOffTV();
        }
        else
        {
            TurnOnTV();
        }
    }

    private void TurnOffTV()
    {
        videoPlayer.Pause();  // Pause the video
        videoPlayer.enabled = false;  // Disables the VideoPlayer component
        renderer.material.color = offColor;  // Set the material color to offColor
    }
       
    private void TurnOnTV()
    {
        renderer.material.color = Color.white;  // Reset the material color
        videoPlayer.enabled = true;  // Enables the VideoPlayer component
        videoPlayer.Play();  // Play the video
       
    }
}

