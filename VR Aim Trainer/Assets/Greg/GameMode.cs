using UnityEngine;
using System.Collections;


// This class designates an enum to keep track of the current game mode.
public class GameMode : MonoBehaviour {
    // Current game modes. Subject to change.
    public enum Mode {
        REACTION,
        TRACKING,
        UNASSIGNED
    };

    public const string ReactionID = "reaction", TrackingID = "tracking";
    [SerializeField] Mode currentMode;

    // Given a string representing a game mode, resolve this to the game mode and return that. 
    public Mode readMode(string modeString) {
        string fmodeString = modeString.Trim().ToLower();
        Mode resolvedMode = Mode.UNASSIGNED;
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