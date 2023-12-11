using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// This script is assigned to the RestAPIHub in the ditor.
// It starts the Rest API.
// It gets the slider values as dictionary, sends a web request to the API containing the slider values
// and brings the reveived temperature values in the correct format to pass it to EnvironmentUpdate.cs where the actual changes in the environment happen.

public class RestAPICallTest : MonoBehaviour
{
  public delegate void GenericCoroutineDelegate(); // we can use delegates to use methods as parameters, we can use this to pass methods that should be called at the end of requests
  public GenericCoroutineDelegate _test_method_to_call; // we call this method in the test case
  
  public string TestURL;
  public string GetURL;
  public string PostURL;

  public EnvironmentUpdate environmentUpdate;
  private float temp2100 = 10f;
  
  // these are all available graphs that are part of the json response
  private List<string> _graph_ids = new List<string> {
    "_time",
    "_net_cumulative_emissions_avoided",
    "_temperature_change_from_19th_century", // we will use this graph
    "_temperature_change_from_19th_century_deg_f",
    "_atm_conc_co2",
    "_slr_from_2000_in_meters",
    "__slr___ft_",
    "_net_cumulative_emissions",
    "_present_value_of_cumulative_gdp_loss",
    "_probability_of_ice_free_arctic_summer_in_percentage",
    "__2010_population_exposed_to_permanent_inundation_",
    "__2010_population_exposed_to_coastal_flooding_",
    "_change_in_global_crop_yield[_wheat]",
    "_change_in_global_crop_yield[_rice]",
    "_change_in_global_crop_yield[_maize]",
    "_change_in_global_crop_yield[_soybean]",
    "_fraction_of_species_losing_more_than_50_pct_of_climatic_range_in_percentage[_invertebrates]",
    "_fraction_of_species_losing_more_than_50_pct_of_climatic_range_in_percentage[_vertebrates]",
    "_fraction_of_species_losing_more_than_50_pct_of_climatic_range_in_percentage[_plants]",
    "_fraction_of_species_losing_more_than_50_pct_of_climatic_range_in_percentage[_insects]",
    "_fraction_of_species_losing_more_than_50_pct_of_climatic_range_in_percentage[_mammals]",
    "_fraction_of_species_losing_more_than_50_pct_of_climatic_range_in_percentage[_birds]",
    "_fraction_of_species_losing_more_than_50_pct_of_climatic_range_in_percentage[_butterflies_and_moths]",
    "_fraction_of_species_losing_more_than_50_pct_of_climatic_range_in_percentage[_dragonflies_and_damselflies]",
    "_heat_related_excess_mortality[_north_america]", "_heat_related_excess_mortality[_central_america]",
    "_heat_related_excess_mortality[_south_america]", "_heat_related_excess_mortality[_north_europe]",
    "_heat_related_excess_mortality[_south_europe]", "_heat_related_excess_mortality[_east_asia]",
    "_heat_related_excess_mortality[_southeast_asia]", "_heat_related_excess_mortality[_australia]",
    "_primary_energy_demand_of_coal", "_primary_energy_demand_of_oil", "_primary_energy_demand_of_gas",
    "_primary_energy_demand_of_renew_and_hydro", "_primary_energy_demand_of_bio", "_primary_energy_demand_of_nuclear",
    "_primary_energy_demand_of_new_tech", "_primary_elec_energy_by_elec_path[_ecoal_ccs]", "_primary_elec_energy_by_elec_path[_egas_ccs]",
    "_primary_elec_energy_by_elec_path[_ebio_ccs]", "_energy_capacity_in_gw[_ecoal]", "_energy_capacity_in_gw[_ecoal_ccs]",
    "_energy_capacity_in_gw[_eoil]", "_energy_capacity_in_gw[_egas]", "_energy_capacity_in_gw[_egas_ccs]", "_energy_capacity_in_gw[_hydro]",
    "_elec_energy_capacity_by_primary_sources_in_gw[_primary_renewables]", "_energy_capacity_in_gw[_ebio]", "_energy_capacity_in_gw[_ebio_ccs]",
    "_energy_capacity_in_gw[_nuclear]", "_energy_capacity_in_gw[_new]", "_total_primary_energy_demand", "_carbon_intensity_of_primary_energy",
    "_carbon_intensity_of_primary_electric_energy", "_primary_nonelec_energy_by_fuel[_pcoal]", "_primary_elec_energy_by_elec_path[_ecoal]",
    "_primary_elec_energy_by_elec_path[_eoil]", "_primary_nonelec_energy_by_fuel[_pgas]", "_primary_elec_energy_by_elec_path[_egas]",
    "_primary_elec_energy_by_elec_path[_ebio]", "_energy_capacity_in_gw[_wind]", "_energy_capacity_in_gw[_solar]",
    "_energy_capacity_in_gw[_geothermal]", "_energy_capacity_in_gw[_other_renew]", "_carbon_intensity_of_final_energy",
    "_total_final_energy_consumption", "_final_energy_demand_by_coal", "_final_energy_demand_by_oil", "_final_energy_demand_by_gas",
    "_final_energy_demand_by_renew_and_hydro", "_final_energy_demand_by_bio", "_final_energy_demand_by_nuclear", "_final_energy_demand_by_new_tech",
    "_percent_of_final_energy_demand_by_source[_primary_coal]", "_percent_of_final_energy_demand_by_source[_primary_oil]",
    "_percent_of_final_energy_demand_by_source[_primary_gas]", "_percent_of_final_energy_demand_by_renewables_and_hydro",
    "_percent_of_final_energy_demand_by_source[_primary_bio]", "_percent_of_final_energy_demand_by_source[_primary_nuclear]",
    "_percent_of_final_energy_demand_by_source[_primary_new]", "_final_elec_energy_by_path_in_twh[_ecoal]", "_final_elec_energy_by_path_in_twh[_eoil]",
    "_final_elec_energy_by_path_in_twh[_egas]", "_final_energy_demand_by_renewables_and_hydro_in_twh", "_final_elec_energy_by_path_in_twh[_ebio]",
    "_final_elec_energy_by_path_in_twh[_nuclear]", "_final_elec_energy_by_path_in_twh[_new]", "_final_energy_demand_by_ccs_paths_in_twh",
    "_total_nonelectric_carrier_for_stationary", "_total_electric_carrier_for_stationary", "_total_nonelectric_carrier_for_each_end_use[_transport]",
    "_final_electric_carrier_for_each_end_use[_transport]", "_carbon_intensity_of_final_electric_energy", "_percent_of_electricity_generated_by_renew_and_hydro",
    "_percent_electricity_from_low_carbon_energy", "_percent_share_of_stationary_capital_that_is_electric", "_percent_share_of_capital_that_is_electric_by_end_use[_transport]",
    "_global_population_in_billions", "_population_in_billions[_us_6r]", "_population_in_billions[_eu_6r]", "_population_in_billions[_china_6r]",
    "_population_in_billions[_india_6r]", "_population_in_billions[_other_developed_6r]", "_population_in_billions[_other_developing_6r]",
    "_global_gdp_per_capita", "_semi_agg_gdp_per_capita[_us_6r]", "_semi_agg_gdp_per_capita[_eu_6r]", "_semi_agg_gdp_per_capita[_china_6r]",
    "_semi_agg_gdp_per_capita[_india_6r]", "_semi_agg_gdp_per_capita[_other_developed_6r]", "_semi_agg_gdp_per_capita[_other_developing_6r]",
    "_gdp_per_capita_rate_of_change", "_average_total_final_energy_intensity_of_gdp", "_global_gdp", "_reduction_in_gdp_vs_temperature",
    "_damage", "_global_gdp_loss", "_present_value_of_global_gdp_loss", "_us_gdp_in_2019", "_global_gdp_in_2019", "_yaxis_equal_zero",
    "_lulucf_net_emissions_with_af_cdr", "_nonaf_net_cdr_as_removals", "_co2_emissions_by_source[_primary_coal]", "_co2_emissions_by_source[_primary_oil]",
    "_co2_emissions_by_source[_primary_gas]", "_co2_emissions_by_source[_primary_bio]", "_co2_emissions", "_cumulative_emissions",
    "_cumulative_co2_from_1870_for_66_pct_chance_below_2_deg", "_cumulative_co2_from_1870_for_66_pct_chance_below_1point5_deg", "_net_co2_emissions",
    "_co2_emissions_per_capita", "_co2_emissions_per_gdp", "_co2_emissions_from_energy", "_co2_equivalent_net_emissions", "_co2eq_emissions_from_f_gases",
    "_co2eq_emissions_from_total_ch4", "_co2eq_emissions_from_n2o", "_co2_equivalent_emissions", "_total_nonco2_ghg_emissions", "_total_ch4_emissions",
    "_global_n2o_anthro_emissions", "_co2eq_emissions_from_sf6", "_co2eq_emissions_from_pfc", "_total_co2eq_emissions_from_hfc", "_net_uptake_and_net_sequestration",
    "_net_cdr_total", "_net_cdr_by_type[_afforestation]", "_net_cdr_by_type[_beccs]", "_net_cdr_by_type[_biochar]", "_net_cdr_by_type[_agricultural_soil_carbon]",
    "_net_cdr_by_type[_direct_air_capture]", "_net_cdr_by_type[_mineralization]", "_total_land_required_for_cdr", "_land_area_of_india", "_total_land_required_for_cdr_mac",
    "_land_area_of_india_mac", "_land_required_for_afforestation", "_land_required_for_bioenergy", "_land_required_for_biochar", "_land_required_for_afforestation_mac",
    "_land_required_for_bioenergy_mac", "_land_required_for_biochar_mac", "_land_for_mineralization", "_land_required_for_agricultural_soil_carbon",
    "_land_for_mineralization_mac", "_land_required_for_agricultural_soil_carbon_mac", "_material_required_for_mineralization", "_coal_scale_plot",
    "_cumulative_cdr_by_type[_afforestation]", "_cumulative_cdr_by_type[_beccs]", "_cumulative_cdr_by_type[_biochar]", "_cumulative_cdr_by_type[_agricultural_soil_carbon]",
    "_cumulative_cdr_by_type[_direct_air_capture]", "_cumulative_cdr_by_type[_mineralization]", "_volume_flow_from_beccs", "_volume_flow_from_dac",
    "_volume_flow_from_coal_ccs", "_volume_flow_from_gas_ccs", "_gas_scale_plot_volume", "_cumulative_co2_flow_to_storage", "_co2_on_afforested_land",
    "_flux_of_co2_from_atm_to_afforested_land", "_net_co2_sequestered_by_afforestation", "_adjusted_cost_of_energy_per_gj", "_carbon_tax_per_tonco2",
    "_government_net_revenue_from_adjustments", "_government_cost_for_subsidies", "_government_revenue_from_taxes", "_market_price_of_electricity_in_kwh",
    "_cost_of_elec_supply_in_kwh[_ecoal]", "_cost_of_elec_supply_in_kwh[_eoil]", "_cost_of_elec_supply_in_kwh[_egas]", "_cost_of_elec_supply_in_kwh_avg_renewables",
    "_cost_of_elec_supply_in_kwh[_hydro]", "_cost_of_elec_supply_in_kwh[_ebio]", "_cost_of_elec_supply_in_kwh[_nuclear]", "_cost_of_elec_supply_in_kwh[_new]",
    "_adjusted_source_cost_kwh[_ecoal]", "_adjusted_source_cost_kwh[_eoil]", "_adjusted_source_cost_kwh[_egas]", "_adjusted_source_internal_cost_kwh_avg_renewables",
    "_adjusted_source_cost_kwh[_hydro]", "_adjusted_source_cost_kwh[_ebio]", "_adjusted_source_cost_kwh[_nuclear]", "_adjusted_source_cost_kwh[_new]",
    "_supplier_cost_for_fuel[_pcoal]", "_supplier_cost_for_fuel[_poil]", "_supplier_cost_for_fuel[_pgas]", "_supplier_cost_for_fuel[_pbio]", "_cost_of_elec_supply_in_kwh[_wind]",
    "_cost_of_elec_supply_in_kwh[_solar]", "_cost_of_elec_supply_in_kwh[_geothermal]", "_cost_of_elec_supply_in_kwh[_other_renew]", "_adjusted_source_cost_kwh[_wind]",
    "_adjusted_source_cost_kwh[_solar]", "_adjusted_source_cost_kwh[_geothermal]", "_adjusted_source_cost_kwh[_other_renew]",
    "_renewables_adjusted_source_internal_cost_without_storage_mwh", "_storage_cost_in_mwh", "_total_cost_of_energy", "_limit_for_temperature",
    "_goal_of_1point5_for_temperature", "_limit_for_temperature_f", "_goal_of_1point5_for_temperature_deg_f", "_equivalent_co2", "_ph",
    "_pm25_emissions_total_from_energy", "_pm25_emissions_by_source[_pcoal]", "_pm25_emissions_by_source[_poil]", "_pm25_emissions_by_source[_pgas]",
    "_pm25_emissions_by_source[_pbio]", "_temperature_change_from_1850", "_electricity_production[_wind]", "_electricity_production[_solar]" };

  private string target_graph_id = "_temperature_change_from_19th_century"; // temperature graph, see above

  private string _get_result;

  private bool _running = false;

  private Dictionary<string, string> _slider_dictionary;

  public bool ready = false;

  private String currURL;

  public GetSliderValues getSliderValues;

  public Process restAPIProcess;

    // test the connection to Rest-API when museum is started
  void Start()
  {
    this._test_method_to_call = this.TestCallback;
    this.TestConnection();

    // run the electron-app.exe to establish rest api connection
    RunFile();
  }

  // run the electron-app.exe to establish rest api connection
  public static void RunFile()
  {
    Process restAPIProcess = new Process();
    restAPIProcess.StartInfo.CreateNoWindow = true;
    restAPIProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
    restAPIProcess.StartInfo.FileName = UnityEngine.Application.streamingAssetsPath + @"/electron-app-win32-x64/electron-app.exe";
    restAPIProcess.Start();
    UnityEngine.Debug.Log("Rest API process started...");
    
    // or with pop up window of exe
    //Process.Start(UnityEngine.Application.streamingAssetsPath + @"/electron-app-win32-x64/electron-app.exe");
  }

// test the connection to Rest-API when museum is started
  public void TestConnection()
  {
    StartCoroutine(this.FetchData(this.TestURL, this._test_method_to_call));
  }


// test the connection to Rest-API when museum is started
  private void TestCallback()
  {
    UnityEngine.Debug.Log(this._get_result);
    if (this._get_result != "{\"Status\":\"Running\"}") // a simpler test string might be a good idea
    {
      this.ready = false;
    }
    else
    {
      this.ready = true;
    }
  }


// get data from the url by starting FetchData()
  public void GetData()
  {
    StartCoroutine(this.FetchData(this.GetURL, null));
  }


// send the request to rest api
  private IEnumerator FetchData(string target_url, GenericCoroutineDelegate callback)
  {
    this._running = true;
    using (UnityWebRequest request = UnityWebRequest.Get(target_url))
    {
      yield return request.SendWebRequest();
      if (request.isHttpError)
      {
        UnityEngine.Debug.Log(request.error);
        this._get_result = "failed";
      }
      else
      {
        string json = request.downloadHandler.text;
        this._get_result = json;
        UnityEngine.Debug.Log(json);
      }
    }
    // call the function if its not null
    callback?.Invoke();

    this._running = false;
  }


// here the real slider to api stuff happens:

// is called from EnvironmentUpdate.read&apply()
  public void RunModelAction()
  {
    if (this.ready)
    {
      if (!this._running)
      {
        //this.GetData();
        this.PostData();
      }
    }
    else
    {
      UnityEngine.Debug.LogError("node server not available...");
      // might be worthwile to implement something to start the node server; since we use yarn its a bit more complicated, a batch file would be the easiest solution, but it
      // should be generated dynamically to avoid issues with changing paths
    }
  }


// call coroutine to upload slider values
  public void PostData()
  {
    StartCoroutine(this.UploadData());
  }


// upload slider values and request json, filled with lots of stuff, especially temperatures and years
  private IEnumerator UploadData()
  {
    this._slider_dictionary = environmentUpdate.sliderValuesQuery; //getSliderValues.GetQueryParameters(currURL);

    this._running = true;

    WWWForm form = new WWWForm();
    foreach (KeyValuePair<string, string> silder_value in this._slider_dictionary)
    {
      //UnityEngine.Debug.Log(silder_value.Key + " : " + silder_value.Value);
      form.AddField(silder_value.Key, silder_value.Value);
    }

    using (UnityWebRequest request = UnityWebRequest.Post(this.PostURL, form))
    {
      UnityEngine.Debug.Log("call...");
      yield return request.SendWebRequest();
      UnityEngine.Debug.Log("response received...");

      if (request.isHttpError || request.isNetworkError)
      {
        UnityEngine.Debug.Log(request.error);
      }
      else
      {
        // we obtain the response as json, parse the relevant data and visualize it
        string json = request.downloadHandler.text;
        // Debug
        /*
        string path = "Assets/json_response.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(json);
        writer.Close();
        */
        // Debug End
        // working with JSON in unity is still a pain in the ass, the System.Text.Json namespace has not been ported yet to Mono, hence, we can either
        // use third party APIs or do some ugly string parsing...
        string substring = json.Split(new string[] { "\"varId\":\"" + this.target_graph_id + "\",\"points\":" }, StringSplitOptions.None)[1];
        substring = substring.Split(new string[] { "[" }, StringSplitOptions.None)[1];
        substring = substring.Split(new string[] { "]" }, StringSplitOptions.None)[0];

        // Debug.Log(substring);

        // now we can parse the data, the single tuples have the form {"x":1991,"y":0.7017767901730128}, so we have the year a x and the temperature at y
        List<float> temperatures = new List<float>();
        List<int> years = new List<int>();

        string[] tuples = substring.Split(new string[] { "},{" }, StringSplitOptions.None); // if we split by comma right away, we would also split the tuples themselves, this would also work, but I wanted a simple data structure

        // for each tuple of the form {"x":1991,"y":0.7017767901730128}
        foreach (string tuple in tuples)
        {
          string[] tuple_tokens = tuple.Split(',');

          // get temperatures
          string year_token = tuple_tokens[0].Split(':')[1];
          string temperature_token = tuple_tokens[1].Split(':')[1];
          
          // for the last item
          if (temperature_token.EndsWith("}"))
          {
            temperature_token = temperature_token.Substring(0, temperature_token.Length - 1);
          }

          float temperature = -1.0f;
          if (Single.TryParse(temperature_token, out temperature))
          {// the try parse used the german locale that expects a , instead of a . however its still a working check if we have a number or not, but it should be replaced, or forced to work with the invariant locale
            temperature = float.Parse(temperature_token, System.Globalization.CultureInfo.InvariantCulture);
          }

          temperatures.Add(temperature);

          // get years
          int year = -1;
          int.TryParse(year_token, out year);
          years.Add(year);
        }

        //UnityEngine.Debug.Log("temperatures:" + temperatures);
        foreach (float temperat in temperatures)
        {
            UnityEngine.Debug.Log("Temperature: " + temperat);
        }

        UnityEngine.Debug.Log("2100: " + years[temperatures.Count - 1]);
        UnityEngine.Debug.Log("2080: " + years[temperatures.Count - 21]);
        UnityEngine.Debug.Log("2060: " + years[temperatures.Count - 41]);
        UnityEngine.Debug.Log("2040: " + years[temperatures.Count - 61]);


        if (temperatures.Count > 0)
        {
            // get last temp (2100) out of list
            temp2100 = temperatures[temperatures.Count - 1];
            temp2080 = temperatures[temperatures.Count - 21];
            temp2060 = temperatures[temperatures.Count - 41];
            temp2040 = temperatures[temperatures.Count - 61];
            //UnityEngine.Debug.Log("temp2100: " + temp2100);
            
            // Do something with lastTemperature (change environment, ...)
            environmentUpdate.apply(temp2100);
        }
      }
                
        UnityEngine.Debug.Log("upload complete...");
    }

    this._running = false; 
  }

}