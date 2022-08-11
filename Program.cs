// See https://aka.ms/new-console-template for more information

using System.Threading.Channels;
using DataLayer_LandingPage;
using DataLayer_LandingPage.Data;
using DataLayer_LandingPage.SQL;

Console.WriteLine("Hello, World!");

DataReader reader = new DataReader();
reader.ReadData();
reader.PopulateData();

//Collect output
List<ProviderInsuranceData> insData= reader.ProviderInsData;

foreach (ProviderInsuranceData data in insData)
{
    string recommendationListJSON = DataUtils.GetJSON(data.CarrierVsPlanIDs);
    Insert.InsertToSQL(data.ProviderID, data.PracticeID, recommendationListJSON);
}
