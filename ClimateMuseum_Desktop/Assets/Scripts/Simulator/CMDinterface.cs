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

public class CMDInterface : MonoBehaviour
{

    public void Start()
    {
        UnityEngine.Debug.Log(UnityEngine.Application.dataPath);
        string relativePath = "Scripts/Simulator/en_roads.exe";
        string absolutePath = Path.Combine(UnityEngine.Application.dataPath, relativePath);
        
    }
    // the location of the c binary as an absolute path
    public string AbsolutePath;
    
    // the arguments we want to pass, they will be written to a file and the
    // file path is then the actual argument for the c binary
    public string CMDArgs;

    public Tester tester;

    // thread control variables
    private bool running = false;
    private Thread currentThread;
    private string modelOutput = "empty";
    private float temp2100 = 10f;

    // event listener for the slider value change, if the value changes we adapt the command line parameter accordingly
    public void CoalTaxSliderValueChange()
    {
        // we map the slider percentage to the actual value range of -20 to 110, which is hardcoded here
        //float coalTaxValue = -20.0f + (110.0f - -20.0f) * CoalTaxSlider.value;
        // the respective index of the variable is 6, see the docu or the variable_index_mapping file
        //this.CMDArgs = "6:" + (coalTaxValue).ToString(System.Globalization.CultureInfo.InvariantCulture); // to avoid the fucking comma
    }

    // initiates the model calculation, this creates a thread that runs the binary and collects the results
    public float getTemp2100(String sliderValues)
    {
        CMDArgs = sliderValues;
        if (!this.running)
        {
            if (this.AbsolutePath.Length > 0)
            {
                if (System.IO.File.Exists(this.AbsolutePath))
                {
                    this.running = true;
                    StartCoroutine(ThreadMonitor());
                    UnityEngine.Debug.LogWarning("output is:" + modelOutput);
                    //while ()
                    return temp2100;
                }
                else
                {
                    UnityEngine.Debug.LogError("file at " + this.AbsolutePath + " does not exist...");
                    return 0f;
                }
            }
            else return 0f;
        }
        else return 0f;
    }

    // this method performs the actual call to the binary, it will run in a thread, that is controlled
    // through a coroutine
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
                System.IO.DirectoryInfo directoryInfo = System.IO.Directory.GetParent(this.AbsolutePath);
                string[] paths = { directoryInfo.FullName, "input_args.txt" };
                string fullCMDArgsPath = Path.Combine(paths);
                UnityEngine.Debug.Log(fullCMDArgsPath);

                using (var writer = new StreamWriter(fullCMDArgsPath)) // we overwrite
                {
                    writer.Write(this.CMDArgs);
                }

                args = fullCMDArgsPath;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = this.AbsolutePath,
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
            UnityEngine.Debug.Log("process finished...");
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
        int yearIndex = 0;
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
            temp2100 = temperatures[temperatures.Count - 1];
            // Do something with lastTemperature
        }
        tester.correctTemperature = true;
        UnityEngine.Debug.Log("temp2100: " + temp2100);
    }
        }
