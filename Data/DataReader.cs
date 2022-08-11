namespace DataLayer_LandingPage.Data;

/// <summary>
/// Reads Data from CSV and populates various fields present in ProviderInsuranceData
/// </summary>
public class DataReader
{
    private Dictionary<string, int> attributeIndexMap;
    private string[] insuranceData;
    private List<ProviderInsuranceData> providerInsData;
    private List<uint> providers;

    public List<ProviderInsuranceData> ProviderInsData
    {
        get
        {
            if (providerInsData == null)
                throw new Exception("Data isn't populated yet");

            return providerInsData;
        } 
        
    }
    public void ReadData()
    {
        insuranceData = File.ReadAllLines("10kProviderInsuranceRecomendationsFinalAetCignaUHC.tsv");
    }

    public void PopulateData()
    {
        if (insuranceData == null)
            return;
        
        GetAttributes(insuranceData[0].Split('\t'));
        
        providerInsData = new List<ProviderInsuranceData>();
        providers = new List<uint>();
        
        
        // Data begins from line 1

        for (int line = 1; line < insuranceData.Length; line++)
        {
            string[] data = insuranceData[line].Split('\t');
            int practiceIDIndex = attributeIndexMap["Practice ID"];
            int providerIDIndex = attributeIndexMap["Provider ID"];
            int carrierNameIndex = attributeIndexMap["Carrier Name"];
            int planIDIndex = attributeIndexMap["Plan ID"];

            

            uint providerID = Convert.ToUInt32(data[providerIDIndex]);
            
            if (!providers.Contains(providerID)) 
            {
                ProviderInsuranceData provider = GetNewProvider(data, practiceIDIndex, providerIDIndex, carrierNameIndex, planIDIndex); 
                providerInsData.Add(provider);
                providers.Add(providerID);
            }
            else
            {
                foreach (ProviderInsuranceData providerData in providerInsData)
                {
                    if (providerID == providerData.ProviderID)
                    {
                        if (providerData.CarrierVsPlanIDs.ContainsKey(data[carrierNameIndex]))
                        {
                            providerData.CarrierVsPlanIDs[data[carrierNameIndex]].Add(Convert.ToInt32(data[planIDIndex]));
                        }
                        else
                        {
                            providerData.CarrierVsPlanIDs.Add(data[carrierNameIndex], new List<int> { Convert.ToInt32(data[planIDIndex]) });   
                        }
                        break;
                    }
                }
            }
        }
    }

    private ProviderInsuranceData GetNewProvider(string[] data,int practiceIDIndex,int providerIDIndex,int carrierNameIndex,int planIDIndex )
    {
        ProviderInsuranceData provider = new ProviderInsuranceData();
                
        provider.PracticeID = Convert.ToUInt32(data[practiceIDIndex]);
        provider.ProviderID = Convert.ToUInt32(data[providerIDIndex]);
        provider.CarrierVsPlanIDs = new Dictionary<string, List<int>>();
        provider.CarrierVsPlanIDs.Add(data[carrierNameIndex], new List<int>{Convert.ToInt32(data[planIDIndex])});

        return provider;
    }

    private void GetAttributes(string[] attributes)
    {
        int colIndex = 0;
        attributeIndexMap = new Dictionary<string, int>();
        foreach (string attribute in attributes)
        {
            attributeIndexMap.Add(attribute, colIndex++);
        }
    }
    
    
}