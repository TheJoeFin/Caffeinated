using System;

namespace Caffeinated;

public class Duration : IComparable {
    public int Minutes { get; set; }
    public string Description {
        get {
            return Duration.ToDescription(Minutes);
        }
    }

    public static string ToDescription(int time) {
        if (time == 0) {
            return "Indefinitely";
        }

        string returnDescription = "";

        if (time >= 60) {
            int hours = time / 60;
            if (hours == 1) {
                returnDescription = "1 hr ";
            }
            else {
                returnDescription = string.Format("{0} hrs ", hours);
            }
        }
        int mins = time % 60;
        if (mins == 1) {
            returnDescription += string.Format("{0} min", mins);
        }
        if (mins > 1) {
            returnDescription += string.Format("{0} mins", mins);
        }

        return returnDescription;
    }

    public int CompareTo(object? obj) {
        if (obj == null) {
            return 1;
        }

        if (obj is Duration otherDuration) {
            if (otherDuration.Minutes > Minutes) {
                return 1;
            }

            if (otherDuration.Minutes < Minutes) {
                return -1;
            }

            return 0;
        }
        else {
            return 1;
        }
    }
}
