using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

struct Commands {

    string Command;
    string Output;

    public Commands(string x, string y)
    {
        Command = x;
        Output = y;
    }

    public string getOutput(string selec)
    {
        if (selec == Command)
        {
            return Output;
        } else
        {
            return null;
        }
    }

    public string getCommand()
    {
        return Command; 
    }

    
}

public class ConsoleController : MonoBehaviour {

    public Canvas menu; // Assign in inspector
    public InputField Inputfield;
    public Text logTextArea;
    private const int scrollbackSize = 20;
    public string[] log { get; private set; }
    private List<Commands> commands = new List<Commands>();
    private Queue<string> scrollback = new Queue<string>(scrollbackSize);
	
    void Start()
    {
        commands.Add(new Commands("background", "Select Background Color"));
        commands.Add(new Commands("help", "background, clone, or escape key"));
        commands.Add(new Commands("clone", "Ball cloned"));
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("c"))
        {
            
            if (!menu.enabled)
            {
                Time.timeScale = 0;
                menu.enabled = true;
            }
            
        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.enabled)
            {
                Time.timeScale = 1;
                menu.enabled = false;
            }
        }
    }

    public void ButtonClick()
    {
        string output;
        foreach (Commands comm in commands)
        {
            if ((output = comm.getOutput(Inputfield.text)) != null)
            {
                if (comm.getCommand() == "background")
                {
                    GameObject plane = GameObject.Find("Background");
                    Renderer rend = plane.GetComponent<Renderer>();
                    rend.material.color = new Color(255, 0, 0, 255);

                }
                else if (comm.getCommand() == "clone")
                {
                    GameObject ball = GameObject.Find("Ball");
                    Instantiate(ball);
                }
                if (scrollback.Count >= scrollbackSize)
                {
                    scrollback.Dequeue();
                }
                scrollback.Enqueue(output);
                log = scrollback.ToArray();
                logTextArea.text = string.Join("\n", log);
            }
        }
    }
}
