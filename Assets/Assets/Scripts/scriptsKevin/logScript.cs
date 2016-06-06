using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class logScript : MonoBehaviour {

    Text text;
    public string title;
    public string textInLog;
        

	// Use this for initialization
	void Start () 
    {
        if (this.title == "log1")
        {
            textInLog = "Day: 3580" + "\n" + "Time: 16:37 – March 3rd – 2223 Earth Time" + "\n" + "I don't remember when was the last time I spoke with someone… Every day is harder than the last and since I realized we're never going home again it's even worse… I've tried to read the captain's LOGs to know what happened, but I've never found his password, so no clue of why we've been stuck in this desert planet for so much time, and I'll never know. At least I won't starve to death, there are enough supplies since most of the people are dead or stuck in the Cryogene machines.";
        }
        if (this.title == "log2")
        {
            textInLog = "Spaceship Status Report:" + "\n" + "Day: 392" + "\n" + "Time: 16:37 – March 3rd – 2217 Earth Time" + "\n"  + "\n" + "\n" + "Collision with unknown mass detected. Left side damaged. Low pressure in cabin detected. Electrical systems damaged. Cryogene machines damaged. Earth communication system damaged. Initializing power saving mode.";
        }
        if (this.title == "log3")
        {
            textInLog = "- Space Program: Icarus." + "\n" + "- Objective: Find habitable planets to perpetuate the human race." + "\n" + "- Program Information:" + "\n" +  "\n" + "The Earth is dramatically becoming dangerous to live in: the atmosphere pollution, species extinction and potable water shortage are now real problems that will make organic living impossible in a few years. Volunteers from around the world were selected and trained to travel to potentially habitable planets that may replace the Earth someday. Every volunteer, except the pilot and the technical staff, will remain frozen in a Cryogene machine until the spaceship lands on the assigned planet. Volunteers can feel sickness and may be amnesiac during some days until their body is fully recovered from being frozen for a long period of time. Make sure to contact the Earth once you complete your mission, we'll be waiting. Good luck and remember: you are humanity's last hope!";
        }
        if (this.title == "log4")
        {

        }
        if (this.title == "weaponLog")
        {
            textInLog = "This is the antigravitational tool. In order to use it, first you must equip (R button), and then click it on any environmental objects like boxes or pipes. " + "\n" + "You can rotate it with 'W' & 'S'";
        }

        text = GetComponent<Text>();	
	}		
}
