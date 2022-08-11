using System.Data;

namespace DataLayer_LandingPage.Data;

public class ProviderInsuranceData
{
    private uint providerID;
    private uint practiceID;
    public Dictionary<string,List<int>> CarrierVsPlanIDs { get; set; }
    
    public uint ProviderID
    {
        get => providerID;
        set
        {
            if(value > 0)
                providerID = value;
            else
            {
                throw new ConstraintException("Provider ID can't be negative");
            }
        }
    }

    public uint PracticeID
    {
        get => practiceID;
        set
        {
            if (value > 0)
                practiceID = value;
            else
            {
                throw new ConstraintException("Practice ID can't be negative");
            }
        }
    }
}