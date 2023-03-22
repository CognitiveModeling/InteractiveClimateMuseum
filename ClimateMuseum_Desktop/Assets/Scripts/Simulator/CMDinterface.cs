using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class CMDinterface : MonoBehaviour
{
    public EnvironmentUpdate environmentUpdate;

    public string CMDArgs; // the arguments we want to pass, they will be written to a file and the file path is then the actual argument for the c binary

    //public string absolutePath; // the location of the c binary as an absolute path
    private string relativePath;
    private string absolutePath;

    // thread control variables
    private bool running = false;
    private Thread currentThread;
    private string modelOutput = "empty";
    private float temp2100 = 10f;

    public void Start()
    {
        //UnityEngine.Debug.Log(UnityEngine.Application.dataPath);
        relativePath = "Scripts/Simulator/en_roads.exe";
        absolutePath = Path.Combine(UnityEngine.Application.dataPath, relativePath);
    }

    // initiates the model calculation, this creates a thread that runs the binary and collects the results
    public void getTemp2100(String sliderValues)
    {
        CMDArgs = sliderValues;
        if (!this.running)
        {
            if (this.absolutePath.Length > 0)
            {
                if (System.IO.File.Exists(this.absolutePath))
                {
                    this.running = true;
                    StartCoroutine(ThreadMonitor());
                    //UnityEngine.Debug.LogWarning("output is:" + modelOutput);
                }
                else
                {
                    UnityEngine.Debug.LogError("file at " + this.absolutePath + " does not exist...");
                }
            }
        }
    }

    // this method performs the actual call to the binary, it will run in a thread, that is controlled through a coroutine
    private void RunProcess()
    {
        try
        {
            // UnityEngine.Debug.Log("process is starting with [" + this.CMDArgs + "]...");
            string args = "";
            // here we write the command line arguments into a file; the file path is then passed
            // to the c binary as the actual command line parameter
            if (this.CMDArgs.Length > 0)
            {
                System.IO.DirectoryInfo directoryInfo = System.IO.Directory.GetParent(this.absolutePath);
                string[] paths = { directoryInfo.FullName, "input_args.txt" };
                string fullCMDArgsPath = Path.Combine(paths);
                //UnityEngine.Debug.Log(fullCMDArgsPath);

                using (var writer = new StreamWriter(fullCMDArgsPath)) // we overwrite
                {
                    writer.Write(this.CMDArgs);
                }

                args = fullCMDArgsPath;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = this.absolutePath,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                Arguments = args
            };
            Process enroadsProcess = new Process
            {
                StartInfo = startInfo
            };
            enroadsProcess.Start();
            string output = enroadsProcess.StandardOutput.ReadToEnd();
            enroadsProcess.WaitForExit();
            //UnityEngine.Debug.Log("process finished...");
            this.running = false;
            this.modelOutput = output;
            //UnityEngine.Debug.Log("actual output: " + modelOutput);
        }
        catch (Exception e)
        {
            print(e);
            this.modelOutput = null;
        }
    }
    // this coroutine starts and controls the thread and handles the
    // data afterwards
    private IEnumerator ThreadMonitor()
    {
        // create the thread...
        this.currentThread = new Thread(new ThreadStart(RunProcess));
        this.currentThread.Start();
        // wait until it's finished...
        while (this.currentThread.IsAlive)
        {
            yield return 0;
        }
        this.currentThread.Join();

        this.running = false;
        // see the docu or the header_indices file for the indices
        //int yearIndex = 0;
        int temperatureIndex = 2;
        bool parseData = false;
        List<float> temperatures = new List<float>();
        // fetch the data

        string[] lines = this.modelOutput.Split('\n');
        //string[] lines = this.modelOutput.Split("\n");
        foreach (string line in lines)
        {
            // right now, we only fetch year and temperature
            if (parseData)
            {
                string[] tokens = line.Split('\t');
                if (tokens.Length < temperatureIndex)
                {
                    continue;
                }
                float temperature = -1.0f;
                string value = tokens[temperatureIndex];
                if (Single.TryParse(value, out temperature))
                {// the try parse used the german locale that expects a , instead of a .
                 // however its still a working check if we have a number or not,
                 // but it should be replaced, or forced to work with the invariant locale
                    temperature = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                }
                temperatures.Add(temperature);
                /*
                int year = -1;
                value = tokens[yearIndex];
                int.TryParse(value, out year);
                years.Add(year);
                */
            }
            //UnityEngine.Debug.Log("temperatures: " + temperatures);
            if (line.StartsWith("Time\t"))
            {
                parseData = true;
            }
        }
        if (temperatures.Count > 0)
        {
            // Using float.Parse()

            //string numberStr = temperatures[temperatures.Count - 1];

            //temp2100 = float.Parse(numberStr);
            temp2100 = temperatures[temperatures.Count - 1];
            //UnityEngine.Debug.LogWarning("Temp2100: " + temp2100);
            environmentUpdate.apply(temp2100);
            // Do something with lastTemperature
        }
        //UnityEngine.Debug.Log("temp2100: " + temp2100);
    }
        }
