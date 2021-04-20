using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    // Song beats per minute.
    public float songBpm;

    // The number of seconds for each song beat.
    private float _secPerBeat;
    public float SecPerBeat
    {
        get { return _secPerBeat; }
        private set { _secPerBeat = value; }
    }

    // Current song position, in seconds.
    private float songPosition;

    // Current song position, in beats.
    private float _songPositionInBeats;

    // Property getter and setter for current songPositionInBeats. We use a property in order to access this
    // field in an external script.
    public float SongPositionInBeats
    {
        get { return _songPositionInBeats; }
        private set { _songPositionInBeats = value; }
    }

    // How many seconds have passed since the song started.
    private float dspSongTime;

    // An AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    //The offset to the first beat of the song in seconds.
    private float firstBeatOffset = 0;



    // Start is called before the first frame update.
    void Start()
    {
        //Calculate the number of seconds in each beat.
        SecPerBeat = 60f / songBpm;

        //Record the time when the music starts.
        dspSongTime = (float) AudioSettings.dspTime;

        //Start the music.
        musicSource.Play();
    }

    // Update is called once per frame.
    void Update()
    {
        // Determine how many seconds have passed since the song started.
        songPosition = (float) (AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        // Determine how many beats have passed since the song started.
        SongPositionInBeats = songPosition / SecPerBeat;
    }
}
