using UnityEngine;
using System.Collections;



public class GameMode : MonoBehaviour {
    public enum Mode {
        REACTION,
        TRACKING
    };

    public const string ReactionID = "reaction", TrackingID = "tracking";
    [SerializeField] Mode currentMode = null;

    // Given a string representing a game mode, resolve this to the game mode and return that. 
    public Mode readMode(string modeString) {
        string fmodeString = modeString.Trim().ToLower();
        Mode resolvedMode = null;
        switch (fmodeString) {
            case ReactionID:
                resolvedMode = Mode.REACTION;
                break;
            case TrackingID:
                resolvedMode = Mode.TRACKING;
                break;
        }
        return resolvedMode;
    }
}